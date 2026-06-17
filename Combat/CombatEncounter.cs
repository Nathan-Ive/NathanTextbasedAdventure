using System;
using System.Collections.Generic;
using RulerOfTheTomb.Items;
using RulerOfTheTomb.Scenes;
using RulerOfTheTomb.Skills;
using RulerOfTheTomb.Utility;

namespace RulerOfTheTomb.Combat
{
    /// <summary>
    /// The outcome of a CombatEncounter, returned to the calling scene.
    /// 
    /// Partially made with the help of AI. (Specifically how turns are handeled.)
    /// </summary>
    public enum CombatResult
    {
        PlayerWon,          // Enemy reached 0 HP through normal damage.
        PlayerLost,         // Player reached 0 HP and revive didn't trigger.
        ResolvedByEvent     // An event skill ended the fight (always counts as player win for scene flow).
    }

    /// <summary>
    /// Manages a single fight between the Player and one Enemy. Handles turn order,
    /// status hooks (Hexed drain, Cursed recoil, Lucky/Blessed crit and revive),
    /// player input, enemy AI, and event-skill mid-fight resolution.
    /// </summary>
    public class CombatEncounter
    {
        // --- Tuning constants ---
        private const double BaseCritChance = 0.05;
        private const double LuckyCritBonus = 0.05;
        private const double HexedDodgeChance = 0.40;
        private const int HexedDrainPerTurn = 3;
        private const double CursedDamageMultiplier = 1.75;
        private const double CursedRecoilFraction = 0.15;
        private const int CursedRecoilCap = 10;
        private const double CritMultiplier = 2.0;
        private const double DefendMitigation = 0.5;

        // --- Combat state ---
        private readonly Player _player;
        private readonly Enemy _enemy;
        private bool _playerDefendingThisTurn;
        private bool _isResolved;

        /// <summary>
        /// True if an event skill or scripted condition has ended the fight prematurely.
        /// Once set, the combat loop exits at the next safe point.
        /// </summary>
        public bool IsResolved => _isResolved;

        /// <summary>
        /// Marks the fight as resolved (event-driven end). Used by event-skill OnUse callbacks.
        /// </summary>
        public void ResolveByEvent() => _isResolved = true;

        /// <summary>
        /// Constructs a combat encounter between the given player and enemy.
        /// </summary>
        /// <param name="player">The player character.</param>
        /// <param name="enemy">The enemy being fought.</param>
        public CombatEncounter(Player player, Enemy enemy)
        {
            _player = player;
            _enemy = enemy;
            _playerDefendingThisTurn = false;
            _isResolved = false;
        }

        /// <summary>
        /// Runs the fight loop until someone dies or the fight is resolved by event.
        /// Returns the outcome for the scene to react to.
        /// </summary>
        /// <returns>The CombatResult describing how the fight ended.</returns>
        public CombatResult Run()
        {
            bool playerFirst = _player.EffectiveSpeed >= _enemy.EffectiveSpeed;

            while (true)
            {
                // Reset per-turn flags at the start of each round.
                _playerDefendingThisTurn = false;

                if (playerFirst)
                {
                    if (!RunPlayerTurn(out CombatResult earlyP1)) return earlyP1;
                    if (!RunEnemyTurn(out CombatResult earlyE1)) return earlyE1;
                }
                else
                {
                    if (!RunEnemyTurn(out CombatResult earlyE2)) return earlyE2;
                    if (!RunPlayerTurn(out CombatResult earlyP2)) return earlyP2;
                }

                // End-of-round status ticks.
                if (_player.Status == PlayerStatus.Hexed)
                {
                    _player.TakeRawDamage(HexedDrainPerTurn);
                    SceneHelpers.Narrate($"The hex saps {HexedDrainPerTurn} HP from your bones.");
                    if (!_player.IsAlive) return CombatResult.PlayerLost;
                }

                if (_isResolved) return CombatResult.ResolvedByEvent;
            }
        }

        /// <summary>
        /// Runs the player's turn. Returns false (and sets the result) if the fight ends during this turn.
        /// </summary>
        private bool RunPlayerTurn(out CombatResult result)
        {
            result = default;
            SceneHelpers.Narrate($"--- Your turn ({_player.CurrentHp}/{_player.MaxHp} HP) ---");

            while (true)
            {
                int choice = PromptMainMenu();
                bool actionTaken = false;

                switch (choice)
                {
                    case 1: actionTaken = PerformAttack(); break;
                    case 2: actionTaken = PerformSkill(); break;
                    case 3: actionTaken = PerformDefend(); break;
                    case 4: actionTaken = PerformItem(); break;
                    case 5: actionTaken = PerformEquipment(); break;
                    default: SceneHelpers.PrintHint("Pick 1-5."); break;
                }

                if (actionTaken) break;
            }

            if (_isResolved) { result = CombatResult.ResolvedByEvent; return false; }
            if (!_enemy.IsAlive) { result = CombatResult.PlayerWon; return false; }
            return true;
        }

        /// <summary>
        /// Runs the enemy's turn via its TakeTurn method. Applies defend mitigation if active.
        /// </summary>
        private bool RunEnemyTurn(out CombatResult result)
        {
            result = default;
            if (!_enemy.IsAlive) { result = CombatResult.PlayerWon; return false; }

            if (_playerDefendingThisTurn) _player.DefenseMultiplier = DefendMitigation;
            _enemy.TakeTurn(_player);
            _player.DefenseMultiplier = 1.0;

            if (_isResolved) { result = CombatResult.ResolvedByEvent; return false; }
            if (!_player.IsAlive) { result = CombatResult.PlayerLost; return false; }
            return true;
        }

        // ---------- Player action handlers ---------- //

        private int PromptMainMenu()
        {
            SceneHelpers.Narrate("1. Attack   2. Skills   3. Defend   4. Items   5. Equipment");
            Console.Write("> ");
            string input = Console.ReadLine() ?? "";
            return int.TryParse(input.Trim(), out int n) ? n : 0;
        }

        private bool PerformAttack()
        {
            int damage = CalculatePlayerDamage(_player.EffectiveStrength, isPhysical: true);
            _enemy.TakeDamage(damage);
            SceneHelpers.Narrate($"You strike {_enemy.Name} for {damage} damage.");
            ApplyCursedRecoil(damage);
            return true;
        }

        private bool PerformSkill()
        {
            var usable = _player.Skills.FindAll(s => s.CanUse);
            if (usable.Count == 0)
            {
                SceneHelpers.PrintHint("No skills available.");
                return false;
            }

            for (int i = 0; i < usable.Count; i++)
            {
                var s = usable[i].Skill;
                string pp = s.IsUnlimited ? "∞" : usable[i].CurrentPp + "/" + s.MaxPp;
                SceneHelpers.Narrate($"{i + 1}. {s.Name} ({pp})");
            }
            SceneHelpers.Narrate("0. Back");

            Console.Write("> ");
            if (!int.TryParse(Console.ReadLine(), out int pick) || pick < 0 || pick > usable.Count)
                return false;
            if (pick == 0) return false;

            LearnedSkill chosen = usable[pick - 1];
            _player.UseSkill(chosen, _enemy);

            if (chosen.Skill.EffectType == SkillEffectType.PhysicalDamage ||
                chosen.Skill.EffectType == SkillEffectType.TrueDamage)
                ApplyCursedRecoil(chosen.Skill.Power + _player.EffectiveStrength);

            return true;
        }

        private bool PerformDefend()
        {
            _playerDefendingThisTurn = true;
            SceneHelpers.Narrate("You brace yourself, halving the damage of the next incoming attack.");
            return true;
        }

        private bool PerformItem()
        {
            if (!_player.Inventory.HasPouch)
            {
                SceneHelpers.PrintHint("You have no pouch to draw items from.");
                return false;
            }

            List<Usable> usables = _player.Inventory.GetBagUsables();
            if (usables.Count == 0)
            {
                SceneHelpers.PrintHint("Your pouch has no usable items.");
                return false;
            }

            for (int i = 0; i < usables.Count; i++)
                SceneHelpers.Narrate($"{i + 1}. {usables[i].Name}");
            SceneHelpers.Narrate("0. Back");

            Console.Write("> ");
            if (!int.TryParse(Console.ReadLine(), out int pick) || pick < 0 || pick > usables.Count)
                return false;
            if (pick == 0) return false;

            Usable chosen = usables[pick - 1];
            chosen.Use(_player, _enemy);
            _player.Inventory.RemoveFromBag(chosen);
            return true;
        }

        private bool PerformEquipment()
        {
            SceneHelpers.Narrate("Currently equipped:");
            bool anyWorn = false;
            foreach (var kv in _player.Inventory.WornEquipment())
            {
                SceneHelpers.Narrate($"  [{kv.Key}] {kv.Value.Name}");
                anyWorn = true;
            }
            if (!anyWorn) SceneHelpers.Narrate("  (nothing)");

            if (!_player.Inventory.HasPouch)
            {
                SceneHelpers.PrintHint("Without a pouch you can't swap gear mid-fight; there's nowhere to stow what you remove.");
                return false;
            }

            List<Equipment> bagged = _player.Inventory.GetBagEquipment();
            if (bagged.Count == 0)
            {
                SceneHelpers.PrintHint("You have no spare equipment in your pouch to swap to.");
                return false;
            }

            SceneHelpers.Narrate("Equip which item?");
            for (int i = 0; i < bagged.Count; i++)
                SceneHelpers.Narrate($"{i + 1}. {bagged[i].Name} ({bagged[i].Slot})");
            SceneHelpers.Narrate("0. Back");

            Console.Write("> ");
            if (!int.TryParse(Console.ReadLine(), out int pick) || pick < 0 || pick > bagged.Count)
                return false;
            if (pick == 0) return false;

            Equipment toWear = bagged[pick - 1];
            _player.Inventory.RemoveFromBag(toWear);
            List<Equipment> displaced = _player.Inventory.Equip(toWear, toWear.Slot);
            foreach (Equipment d in displaced)
                _player.Inventory.Add(d); // returns to the bag
            _player.RecalculateStatus();

            SceneHelpers.Narrate($"You equip the {toWear.Name}.");
            SceneHelpers.PrintHint($"[Status: {_player.Status}]");
            return true;
        }

        // ---------- Damage helpers ----------

        /// <summary>
        /// Calculates the player's outgoing damage, applying crit chance, Lucky bonus, and Cursed multiplier.
        /// </summary>
        private int CalculatePlayerDamage(int baseDamage, bool isPhysical)
        {
            double critChance = BaseCritChance + (_player.Status == PlayerStatus.Lucky ? LuckyCritBonus : 0);
            bool crit = RandomSource.Roll(critChance);
            double multiplier = crit ? CritMultiplier : 1.0;
            if (isPhysical && _player.Status == PlayerStatus.Cursed) multiplier *= CursedDamageMultiplier;

            int final = (int)Math.Round(baseDamage * multiplier);
            if (crit) SceneHelpers.PrintHint("Critical hit!");
            return final;
        }

        /// <summary>
        /// Applies Cursed-status recoil damage to the player based on damage dealt.
        /// </summary>
        private void ApplyCursedRecoil(int damageDealt)
        {
            if (_player.Status != PlayerStatus.Cursed) return;
            int recoil = Math.Min(CursedRecoilCap, (int)Math.Round(damageDealt * CursedRecoilFraction));
            _player.TakeRawDamage(recoil);
            SceneHelpers.Narrate($"Your brittle body recoils from the strike, costing you {recoil} HP.");
        }
    }
}
