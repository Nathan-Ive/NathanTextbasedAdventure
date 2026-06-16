using System.Collections.Generic;
using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Items;
using TextbasedAdventure.Items.UsableItemLibrary;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// Scene 1 — The Awakening. A single non-linear exploration room: every choice is
    /// available from the moment you wake, including leaving. You can dig your own grave,
    /// rob the other graves, rattle your bones, and check behind the graves in any order,
    /// as many times as you like, and only leave when you decide you're done. Each pickup
    /// only pays out once and narrates differently if you come back to it.
    /// </summary>
    public class AwakeningEvent : Event
    {
        // Per-room state: which one-time payouts have already been collected.
        private bool _ownGraveDug;
        private bool _otherGravesRobbed;
        private bool _behindChecked;
        private bool _left;

        /// <summary>Constructs the awakening exploration event.</summary>
        public AwakeningEvent() : base("The tomb you woke in") { }

        /// <summary>
        /// Narrates the awakening, then hands control to a looping prompt where the
        /// player explores freely until they choose to leave.
        /// </summary>
        /// <param name="player">The player character.</param>
        /// <param name="scene">The scene this event belongs to.</param>
        public override void Run(Player player, Scene scene)
        {
            SceneHelpers.Narrate(
                "You awaken once again, seeing the ceiling of a dark room, despite having no eyes. " +
                "You take a moment, trying to move your body, but remember you don't have muscles... " +
                "Whatever. You've heard others in the same state wake and move around, so you pull " +
                "together your willpower and haul your brittle self upright.");
            SceneHelpers.Narrate(
                "The room is empty, and every other grave already abandoned, their occupants gone ahead " +
                "of you. You feel an insatiable urge to reach the bottom of this place and fight " +
                "whatever waits there. But you can't go unprepared. No one's around. The room is " +
                "yours to search.");

            BuildChoices(player);
            SceneHelpers.RunEventLoop(scene, () => _left);
        }

        /// <summary>
        /// Builds the full set of always-available choices for the room.
        /// </summary>
        private void BuildChoices(Player player)
        {
            // --- Dig your own grave: the Pouch (unlocks the inventory bag). ---
            var ownGrave = new Choice(
                "Dig through your {own grave}",
                new List<List<string>>
                {
                    new List<string> { "dig", "search", "check", "examine", "grave", "graves" },
                    new List<string> { "own", "your", "my", "mine", "myself" }
                },
                () => DigOwnGrave(player));

            // --- Dig the other graves: a Spear and Leather Armor. ---
            var otherGraves = new Choice(
                "Dig through the {other graves}",
                new List<List<string>>
                {
                    new List<string> { "dig", "search", "check", "examine", "grave", "graves" },
                    new List<string> { "other", "others", "rest", "their", "theirs" }
                },
                () => DigOtherGraves(player));

            // Negation: "dig the graves that aren't mine" flips own -> other and vice versa.
            ownGrave.Opposite = otherGraves;
            otherGraves.Opposite = ownGrave;

            // --- Rattle your bones: pure flavour, repeatable. ---
            var rattle = new Choice(
                "{Rattle} your bones",
                new List<List<string>>
                {
                    new List<string> { "rattle", "shake", "bones", "body", "yourself" }
                },
                Rattle);

            // --- Check behind the graves: Rocks (throwables). ---
            var behind = new Choice(
                "Check {behind} the graves",
                new List<List<string>>
                {
                    new List<string> { "behind", "back", "pull", "push", "move" },
                    new List<string> { "grave", "graves", "headstone", "headstones" }
                },
                () => CheckBehindGraves(player));

            // --- Leave: available from the very start. ---
            var leave = new Choice(
                "{Leave} the room",
                new List<List<string>>
                {
                    new List<string> { "leave", "exit", "door", "out", "go", "continue", "ahead", "forward" }
                },
                Leave);

            Choices.Add(ownGrave);
            Choices.Add(otherGraves);
            Choices.Add(rattle);
            Choices.Add(behind);
            Choices.Add(leave);
        }

        // ---------- Choice handlers ----------

        private void DigOwnGrave(Player player)
        {
            if (_ownGraveDug)
            {
                SceneHelpers.Narrate("Your own grave is already dug out. There's nothing left in it.");
                return;
            }

            _ownGraveDug = true;
            player.Inventory.Add(ItemLibrary.CreatePouch());
            SceneHelpers.Narrate(
                "You dig through your own grave. Something was left with you while you \"slept\", " +
                "a small pouch. Good thing you checked; without it you couldn't carry much at all. " +
                "You tie its string around one of your hip crevices and secure it.");
            SceneHelpers.PrintHint("[Inventory unlocked - you can now carry items in your pouch.]");
        }

        private void DigOtherGraves(Player player)
        {
            if (_otherGravesRobbed)
            {
                SceneHelpers.Narrate("You've already stripped the other graves of anything useful.");
                return;
            }

            _otherGravesRobbed = true;
            SceneHelpers.Narrate(
                "You dig through every grave except your own, finding supplies the departed left behind " +
                "and the withered remains of those who never rose. A worn spear and some flaky leather " +
                "armor are the only things still usable. They're at their limit, but durable enough to last.");

            Equipment spear = ItemLibrary.CreateSpear();
            Equipment armor = ItemLibrary.CreateLeatherArmor();

            if (player.Inventory.HasPouch)
            {
                // With a pouch, the gear just goes in the bag for you to equip when you like.
                player.Inventory.Add(spear);
                player.Inventory.Add(armor);
                SceneHelpers.PrintHint("[Obtained: Worn Spear, Flaky Leather Armor — stored in your pouch.]");
            }
            else
            {
                // No pouch: you have to wear them right now or leave them behind.
                List<Equipment> displaced = player.Inventory.Equip(spear, EquipmentSlot.RightHand);
                displaced.AddRange(player.Inventory.Equip(armor, EquipmentSlot.Armor));
                player.RecalculateStatus();

                SceneHelpers.PrintHint("[No pouch — you equip the Worn Spear (two-handed) and Flaky Leather Armor.]");
                foreach (Equipment dropped in displaced)
                    SceneHelpers.Narrate($"With no room to store it, you let the {dropped.Name} fall to the floor.");
            }
        }

        private void Rattle()
        {
            SceneHelpers.Narrate(
                "You rattle your bones. You're a skeleton. Good to know. Bones this flimsy would " +
                "be a bad thing to ignore.");
        }

        private void CheckBehindGraves(Player player)
        {
            if (_behindChecked)
            {
                SceneHelpers.Narrate("You've already pried behind the graves. Nothing more rolls loose.");
                return;
            }

            SceneHelpers.Narrate(
                "You try to pull the graves out, but lack the muscle to budge them. Something does " +
                "clatter to the floor, though a couple of rocks. Not amazing, but good for stunning " +
                "an enemy, especially something made of brittle bone.");

            if (player.Inventory.HasPouch)
            {
                _behindChecked = true;
                for (int i = 0; i < 5; i++)
                    player.Inventory.Add(new Pebble("Rock", "A blunt rock. Good for a stunning throw, especially against bone."));
                SceneHelpers.PrintHint("[Obtained: 5x Rock — stored in your pouch.]");
            }
            else
            {
                // The equipment slots only hold wearable gear, so loose rocks need a pouch to carry.
                // (The design doc imagined holding a single rock by hand; with no bag and the slot
                // system reserved for equipment, the rocks have nowhere to go.)
                SceneHelpers.Narrate(
                    "Without a pouch, you've nowhere to keep them, and fumbling loose rocks in your " +
                    "hands would only slow you down. You leave them where they fell.");
                SceneHelpers.PrintHint("[You need a pouch to carry the rocks. Try checking your own grave first.]");
            }
        }

        private void Leave()
        {
            _left = true;
            SceneHelpers.Narrate(
                "You head to the door opposite your own grave, deciding you've had enough of this room. " +
                "Leaving, you enter a long hallway. It's empty, but you hear the distant rattling and " +
                "cracking of bones, an ominous sound in such a dark place. With no eyes to trouble, the " +
                "darkness doesn't concern you. You walk forward until you reach the next room of the tomb.");
        }
    }
}
