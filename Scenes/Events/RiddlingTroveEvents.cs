using System.Collections.Generic;
using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Items;
using RulerOfTheTomb.Skills;
using TextbasedAdventure.Items.UsableItemLibrary;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// Scene 3 — The Riddling Trove. A talking treasure chest. Answer its three riddles and
    /// it rewards you with the Blessed Stellar-Mass Greatsword and supplies; fail one and it
    /// transforms into a monster you must fight, its contents corrupting into the Cursed
    /// Tormentor Shield. You can also just leave the room untouched.
    /// </summary>
    public class RiddlingTroveEvent : Event
    {
        private bool _done;

        public RiddlingTroveEvent() : base("The room with the treasure chest") { }

        public override void Run(Player player, Scene scene)
        {
            SceneHelpers.Narrate(
                "You enter a far simpler room, no one blocking the exit, no obvious traps, no gimmick. " +
                "Just a treasure chest pressed against the rightmost wall. How nice. It could be a " +
                "trap trigger, of course, but nothing here suggests arrows or spikes. You could leave... " +
                "but exploration is how you grow stronger.");

            Choices.Add(new Choice(
                "Inspect the {treasure chest}",
                new List<List<string>> { new List<string> { "chest", "treasure", "trove", "open", "inspect" } },
                () => InspectChest(player)));

            Choices.Add(new Choice(
                "{Leave} the room",
                new List<List<string>> { new List<string> { "leave", "exit", "out", "go", "continue", "ahead" } },
                () => { LeaveRoom(); }));

            SceneHelpers.RunEventLoop(scene, () => _done);
        }

        private void InspectChest(Player player)
        {
            SceneHelpers.Narrate(
                "You reach for the chest. It won't open. The instant you wonder if it needs a key, " +
                "its mouth springs wide, full of sharp teeth. You jump back. But it doesn't lunge. " +
                "It... speaks.");
            SceneHelpers.Narrate(
                "\"Oi! Don't think you can open me and take my contents just like that, good sir! Answer " +
                "my riddles, and my contents are yours. Fail, and you die, though if you somehow beat me " +
                "after, you may take what's left... So? Up for the challenge?\"");

            if (!SceneHelpers.AskYesNo("Accept the Riddling Trove's challenge?"))
            {
                SceneHelpers.Narrate(
                    "You shake your head. The Trove deflates, grumbling, and you decide there are easier " +
                    "ways to die. You leave it be.");
                LeaveRoom();
                return;
            }

            SceneHelpers.Narrate("\"Yes! Finally, someone accepts. Three riddles. Three chances each. Begin!\"");

            bool solved =
                AskRiddle(
                    "What walks on four legs in the morning, two legs in the afternoon, and three legs in the evening?",
                    "man", "human", "person", "people", "child", "baby", "adult", "mankind", "us")
                && AskRiddle(
                    "To fix something I need straw; to cut straw, a knife; to sharpen the knife, a stone; " +
                    "to wet the stone, water, which I cannot draw. What am I fixing?",
                    "bucket", "pail")
                && AskRiddle(
                    "And the last, most personal of all... What are you?",
                    "skeleton", "bones", "bone", "dead", "corpse", "undead");

            if (solved)
                ResolveSuccess(player);
            else
                ResolveFailure(player);
        }

        /// <summary>
        /// Asks a single riddle, giving the player up to three attempts.
        /// Returns true if answered correctly, false if all attempts are used.
        /// </summary>
        private bool AskRiddle(string question, params string[] accepted)
        {
            SceneHelpers.PrintHighlighted("Riddle: " + question);
            for (int attempt = 3; attempt > 0; attempt--)
            {
                string answer = SceneHelpers.ReadLine();
                if (SceneHelpers.ContainsAny(answer, accepted))
                {
                    SceneHelpers.Narrate("\"Well done! On we go.\"");
                    return true;
                }

                if (attempt > 1)
                    SceneHelpers.PrintHint($"\"Wrong... but I'm generous. {attempt - 1} chance(s) left.\"");
            }

            return false;
        }

        private void ResolveSuccess(Player player)
        {
            SceneHelpers.Narrate(
                "\"Thank you, bone man! For succeeding, a most glorious prize. Here, take it, and let me hear " +
                "your cries of joy!\" The Trove spits out a greatsword nearly your own size. Too heavy to " +
                "carry, surely? But to your surprise, it weighs nothing in your hands. It then coughs up a few " +
                "more useful things and, for once, forgets to be annoyed at its own rhyming.");

            // The blessed weapon, plus the skill that exploits its weightlessness.
            Equipment greatsword = ItemLibrary.CreateStellarMassGreatsword();
            if (player.Inventory.HasPouch) player.Inventory.Add(greatsword);
            else player.Inventory.Equip(greatsword, greatsword.Slot);
            player.RecalculateStatus();
            player.Skills.Add(new LearnedSkill(SkillLibrary.ZeroMassSlash));
            SceneHelpers.PrintHint("[Obtained: Stellar-Mass Greatsword (Blessed). Learned: Zero Mass Slash.]");

            if (player.Inventory.HasPouch)
            {
                for (int i = 0; i < 3; i++)
                    player.Inventory.Add(new HPPotion("Health Potion", "Restores a chunk of HP in battle."));
                for (int i = 0; i < 2; i++)
                    player.Inventory.Add(new MPPotion("MP Potion", "Restores a chunk of MP in battle."));
                SceneHelpers.PrintHint("[Obtained: 3x Health Potion, 2x MP Potion — stored in your pouch.]");

                // The "Selective Skill Book" lets you learn one technique of your choosing.
                SceneHelpers.Narrate(
                    "Among the spoils is a Selective Skill Book it will teach you one technique.");
                if (SceneHelpers.AskYesNo("Learn Heal from the skill book? (choosing 'no' learns a Heavy Attack instead)"))
                {
                    player.Skills.Add(new LearnedSkill(SkillLibrary.Heal));
                    SceneHelpers.PrintHint("[Learned: Heal.]");
                }
                else
                {
                    player.Skills.Add(new LearnedSkill(SkillLibrary.HeavyAttack));
                    SceneHelpers.PrintHint("[Learned: Heavy Attack.]");
                }
            }
            else
            {
                SceneHelpers.Narrate(
                    "The potions and skill book tumble out too, but with no pouch you can't carry them. " +
                    "You leave them behind, taking only the weightless sword.");
            }

            LeaveRoom();
        }

        private void ResolveFailure(Player player)
        {
            SceneHelpers.Narrate(
                "The Trove goes silent, then erupts: \"HOW DARE A FOOL SUCH AS YOU TAKE ON MY RIDDLES! " +
                "You will die for this, and whatever you wrench from me shall ROT in your hands!\" It " +
                "sprouts arms and legs, its mouth widening into something far more monstrous.");

            var fight = new CombatEncounter(player, EnemyLibrary.CreateRiddlingTrove());
            CombatResult result = fight.Run();

            if (result == CombatResult.PlayerLost || !player.IsAlive)
                return; // NormalScene handles the death.

            SceneHelpers.Narrate(
                "With the final blow the Trove collapses, coughing up its hoard. Most of it has rotted, " +
                "as promised, even a pitifully rotted greatsword of former greatness lies among it. But " +
                "one thing stands out, untouched and humming with malice: an ominous shield. You remember " +
                "his words. \"Endless torment.\" Is it worth the risk?");

            if (SceneHelpers.AskYesNo("Take the Tormentor Shield from the spoils?"))
            {
                Equipment shield = ItemLibrary.CreateTormentorShield();
                if (player.Inventory.HasPouch) player.Inventory.Add(shield);
                else player.Inventory.Equip(shield, shield.Slot);
                player.RecalculateStatus();
                SceneHelpers.PrintHint("[Obtained: Tormentor Shield (Cursed).]");
            }
            else
            {
                SceneHelpers.Narrate("You leave the cursed shield among the rot and move on.");
            }

            LeaveRoom();
        }

        private void LeaveRoom()
        {
            _done = true;
            SceneHelpers.Narrate(
                "You leave the room behind, pressing deeper into the tomb toward whatever waits next.");
        }
    }
}
