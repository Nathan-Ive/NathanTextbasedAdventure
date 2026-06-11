using RulerOfTheTomb.Items;
using System.Text;

namespace RulerOfTheTomb.Combat
{
    /// <summary>
    /// The player-controlled Actor. Extends Actor with a status system driven by
    /// equipped alignment items, and the ending-determination logic that reads
    /// from the inventory.
    /// </summary>
    public class Player : Actor
    {
        /// <summary>
        /// The player's current non-standard status, recalculated whenever equipment changes.
        /// </summary>
        public PlayerStatus Status { get; private set; }

        /// <summary>
        /// Whether the Blessed status's one-time auto-revive has already been consumed.
        /// </summary>
        public bool ReviveUsed { get; private set; }

        /// <summary>
        /// Multiplier applied to incoming damage. Set to 0.5 by CombatEncounter when defending,
        /// reset to 1.0 after the enemy's turn resolves.
        /// </summary>
        public double DefenseMultiplier { get; set; } = 1.0;

        /// <summary>
        /// Constructs the player with starting stats. Status begins at Normal.
        /// </summary>
        /// <param name="name">The player's display name.</param>
        public Player(string name)
            : base(name, maxHp: 200, maxMp: 30,
                   strength: 15, magic: 15, defense: 15, magicDefense: 15, speed: 15)
        {
            Status = PlayerStatus.Normal;
            ReviveUsed = false;
        }

        /// <summary>
        /// Recalculates the player's Status based on currently equipped alignment items.
        /// Should be called any time equipment is added, removed, or swapped.
        /// </summary>
        public void RecalculateStatus()
        {
            int blessed = Inventory.CountEquippedBlessed();
            int cursed = Inventory.CountEquippedCursed();

            if (blessed >= 2) Status = PlayerStatus.Blessed;
            else if (cursed >= 2) Status = PlayerStatus.Cursed;
            else if (blessed == 1 && cursed == 0) Status = PlayerStatus.Lucky;
            else if (cursed == 1 && blessed == 0) Status = PlayerStatus.Hexed;
            else Status = PlayerStatus.Normal; // includes the 1-and-1 case
        }

        /// <summary>
        /// Overrides damage handling to apply status-specific effects:
        /// Hexed grants dodge chance, Blessed grants a one-time revive at 0 HP.
        /// Also respects the DefenseMultiplier set during Defend.
        /// </summary>
        /// <param name="rawDamage">The pre-mitigation damage value.</param>
        /// <returns>The actual damage dealt after defense, dodge, and revive logic.</returns>
        public override int TakeDamage(int rawDamage)
        {
            // Hexed: chance to dodge entirely (placeholder %; tune later)
            if (Status == PlayerStatus.Hexed && RollDodge(0.4))
            {
                return 0;
            }

            int scaled = (int)System.Math.Round(rawDamage * DefenseMultiplier);
            int dealt = base.TakeDamage(scaled);

            // Blessed: if this hit would have killed us and revive is unused, restore to 1 HP.
            if (Status == PlayerStatus.Blessed && CurrentHp == 0 && !ReviveUsed)
            {
                CurrentHp = 1;
                ReviveUsed = true;
            }

            return dealt;
        }

        /// <summary>
        /// Determines which ending the player gets based on owned item alignment
        /// and whether the final boss fight was won.
        /// </summary>
        /// <param name="wonFinalFight">True if the player defeated the Legend King.</param>
        /// <returns>The Ending the player has earned.</returns>
        public Ending DetermineEnding(bool wonFinalFight)
        {
            if (!wonFinalFight) return Ending.Doomed;

            int blessed = Inventory.CountOwnedBlessed();
            int cursed = Inventory.CountOwnedCursed();

            if (blessed > cursed) return Ending.Ruler;
            if (cursed > blessed) return Ending.Tyrant;

            // Tie: the Scene 5 key item alignment is the tiebreaker.
            Alignment? key = Inventory.GetKeyItemAlignment();
            if (key == Alignment.Blessed) return Ending.Ruler;
            if (key == Alignment.Cursed) return Ending.Tyrant;

            // No tiebreaker exists (shouldn't happen given Scene 5 design, but safe fallback).
            return Ending.Tyrant;
        }

        /// <summary>
        /// Restores HP and MP to maximum. Called by GameSession on game-over reset.
        /// </summary>
        public void FullHeal()
        {
            CurrentHp = MaxHp;
            CurrentMp = MaxMp;
        }

        // ---------- Private helpers ----------

        private static readonly System.Random _rng = new System.Random();

        private bool RollDodge(double chance) => _rng.NextDouble() < chance;
    }
}
