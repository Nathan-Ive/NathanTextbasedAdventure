using System.Collections.Generic;
using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Items;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// Scene 5 — The Twin Guards. The last defense before the ruler: the Radiant Warrior
    /// (blessed) and the Dull Scholar (cursed). You can loot the corpses for neutral Plated
    /// Armor first. The order you kill them in decides which alignment key item you walk away
    /// with, the tiebreaker for the ending. The combat engine fights one foe at a time, so
    /// here that's expressed by choosing which guard to bring down first.
    /// </summary>
    public class TwinGuardsEvent : Event
    {
        private bool _looted;
        private bool _proceed;

        public TwinGuardsEvent() : base("The hall of the twin guards") { }

        public override void Run(Player player, Scene scene)
        {
            SceneHelpers.Narrate(
                "A vast space stretches toward a massive door wreathed in fire, the ruler's threshold. " +
                "Corpses litter the floor, laid out as warnings. Two figures stand before the door: a " +
                "knight whose armor radiates light like a small sun, and a robed man whose cloak seems to " +
                "erode the air, eclipsing that light. They won't move until you do. Until then, you could " +
                "look around, some of these corpses still carry worthwhile gear.");

            Choices.Add(new Choice(
                "{Loot} the surrounding corpses",
                new List<List<string>> { new List<string> { "loot", "corpses", "corpse", "search", "bodies", "scavenge" } },
                () => Loot(player)));

            Choices.Add(new Choice(
                "{Battle} the two guards",
                new List<List<string>> { new List<string> { "battle", "fight", "attack", "engage", "guards" } },
                () => _proceed = true));

            SceneHelpers.RunEventLoop(scene, () => _proceed);

            FightGuards(player);
        }

        private void Loot(Player player)
        {
            if (_looted)
            {
                SceneHelpers.Narrate("You've already stripped the corpses of anything worth taking.");
                return;
            }

            _looted = true;
            SceneHelpers.Narrate(
                "You pick through the dead. No usable weapon or shield among them, but you do find a set " +
                "of Plated Armor. Not amazing, but it's tough. " +
                "It'll help close the gap.");

            Equipment armor = ItemLibrary.CreatePlatedArmor();
            if (player.Inventory.HasPouch) player.Inventory.Add(armor);
            else player.Inventory.Equip(armor, EquipmentSlot.Armor);
            player.RecalculateStatus();
            SceneHelpers.PrintHint("[Obtained: Plated Armor.]");
        }

        private void FightGuards(Player player)
        {
            SceneHelpers.Narrate(
                "You make your move, and both guards spring to life at once. You'll have to decide who to " +
                "bring down first and the one you fell first will leave its power behind.");

            bool radiantFirst = SceneHelpers.AskYesNo(
                "Focus the Radiant Warrior first? (choosing 'no' focuses the Dull Scholar first)");

            if (radiantFirst)
            {
                if (!FightOne(player, EnemyLibrary.CreateRadiantWarrior())) return;
                SceneHelpers.Narrate(
                    "The Radiant Warrior falls first. A cube of pure light settles out of his remains, " +
                    "humming with overwhelming authority.");
                player.Inventory.Add(ItemLibrary.CreateBlessedCube());
                SceneHelpers.PrintHint("[Obtained: Radiant Cube (Blessed key item).]");

                if (!FightOne(player, EnemyLibrary.CreateDullScholar())) return;
            }
            else
            {
                if (!FightOne(player, EnemyLibrary.CreateDullScholar())) return;
                SceneHelpers.Narrate(
                    "The Dull Scholar falls first. A cube of dark stone settles out of his remains, and " +
                    "holding it makes you feel powerful.");
                player.Inventory.Add(ItemLibrary.CreateCursedCube());
                SceneHelpers.PrintHint("[Obtained: Dull Cube (Cursed key item).]");

                if (!FightOne(player, EnemyLibrary.CreateRadiantWarrior())) return;
            }

            SceneHelpers.Narrate(
                "Both guards lie broken. The fiery door stands unguarded. Before you step through, you take " +
                "one last quiet moment to sort your gear and equip whatever you trust most there will be " +
                "no second chance beyond that threshold.");
        }

        /// <summary>
        /// Runs a single guard fight. Returns true if the player survived, false if they died
        /// (in which case NormalScene will trigger the game-over).
        /// </summary>
        private static bool FightOne(Player player, Enemy guard)
        {
            SceneHelpers.Narrate($"You turn your full attention to the {guard.Name}.");
            var fight = new CombatEncounter(player, guard);
            CombatResult result = fight.Run();
            return result != CombatResult.PlayerLost && player.IsAlive;
        }
    }
}
