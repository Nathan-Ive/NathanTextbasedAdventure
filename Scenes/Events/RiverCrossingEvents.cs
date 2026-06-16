using System.Collections.Generic;
using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Skills;
using RulerOfTheTomb.Utility;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// Scene 4 — The Underground River. A neutral waiting line of adventurers. You only have
    /// time to speak with one of them before the boat comes: the kind robed fellow explains
    /// Blessed gear, the rugged armored fellow explains Cursed gear, and the hooded fellow
    /// gifts you a spell decided by a coin flip (Fireball if you guess right, Heal if wrong).
    /// Crossing the river fully restores you for the trials ahead.
    /// </summary>
    public class RiverCrossingEvent : Event
    {
        public RiverCrossingEvent() : base("The line at the underground river") { }

        public override void Run(Player player, Scene scene)
        {
            SceneHelpers.Narrate(
                "You enter a massive cavern split by a wide river, where a crowd waits in a queue for a " +
                "ferry. Each wears wildly distinct gear, some impressive, some menacing, some plain. However, they are " +
                "all clearly on journeys of their own. There's time to speak with one of them before the " +
                "boat arrives. Who knows what they might know?");

            Choices.Add(new Choice(
                "Talk with the {kind} fellow in robes",
                new List<List<string>> { new List<string> { "kind", "robe", "robes", "robed", "peaceful", "pleasant" } },
                () => TalkKind()));

            Choices.Add(new Choice(
                "Talk with the {rugged} fellow in armor",
                new List<List<string>> { new List<string> { "rugged", "armor", "armored", "menacing", "sword" } },
                () => TalkRugged()));

            Choices.Add(new Choice(
                "Talk with the {hooded} fellow",
                new List<List<string>> { new List<string> { "hooded", "hood", "obscured", "suspicious", "cloak", "cloaked" } },
                () => TalkHooded(player)));

            // You only get one conversation — a single-shot prompt, then the boat comes.
            SceneHelpers.RunEventInput(scene);

            SceneHelpers.Narrate(
                "Eventually the line moves and you're waved onto the boat. As it carries you across, you " +
                "rest, your wounds and weariness fading. After a time a gigantic door comes into view, " +
                "the very depths of this place. The boat reaches the dock, and you step " +
                "off as the ferryman turns back for the next passenger.");
            player.FullHeal();
            SceneHelpers.PrintHint("[Fully rested — HP and MP restored.]");
        }

        private void TalkKind()
        {
            SceneHelpers.Narrate(
                "You approach the man with the peaceful visage and tap his shoulder. He turns with a " +
                "pleasant expression. You mouth a greeting, nothing comes out, of course. However, he reads " +
                "your intent anyway. \"Can't speak yet? I understand completely. Let me share something I've " +
                "learned. Would you care to listen?\"");

            if (!SceneHelpers.AskYesNo("Listen to the kind fellow?"))
            {
                SceneHelpers.Narrate(
                    "\"Ah, you already know, don't you? Then next time, speak to that suspicious fellow " +
                    "instead. Far more valuable for one as knowledgeable as you.\" He gives a reassuring " +
                    "nod and melts back into the crowd.");
                return;
            }

            SceneHelpers.Narrate(
                "\"Throughout this tomb you'll find equipment with a radiant feel, as if blessed by a god. " +
                "I call it Blessed gear, and it's no mere feeling. My robe alone makes me far luckier than " +
                "I should be. Wield my weapon alongside it and that luck becomes something stranger: I " +
                "survive odds I shouldn't. If you find such gear, wear it, and see what it grants you.\"");
            SceneHelpers.Narrate(
                "\"There's word of a legendary warrior in blindingly radiant armor, they say his body " +
                "moves so swiftly he can act twice in a single motion. Defeat him, perhaps, and you might " +
                "learn how.\" He waves and departs for the boat.");
            SceneHelpers.PrintHint("[Hint: equipping 2+ Blessed items makes you Blessed. Granting you a one-time revive in battle.]");
        }

        private void TalkRugged()
        {
            SceneHelpers.Narrate(
                "You approach the armored man. He levels his sword at you, holds, then lowers it. \"Trying " +
                "to talk? Curious lad. Fine. You can't speak, so you'll just hear me ramble.\"");
            SceneHelpers.Narrate(
                "\"You know the powerful gear spread through this tomb? I wear it now. The first piece felt " +
                "like it would eat me alive, but every fight since, I've been quicker and stronger. It burned " +
                "at first. As I gathered more, the constant pain faded and left only power; the force behind " +
                "my blows doubled, tripled. But my body still can't fully bear it, every swing risks " +
                "tearing me apart. Cursed gear, I call it. Weigh that price if you find it.\"");
            SceneHelpers.Narrate(
                "\"There's talk of a cloaked man who embodied the curse so fully he grew stronger " +
                "nearing the strength of his own ruler. Kill him and you might claim that for " +
                "yourself.\" He glares once more. \"Next time we meet, one of us dies. Avoid me.\" Then he " +
                "vanishes into the crowd.");
            SceneHelpers.PrintHint("[Hint: equipping 2+ Cursed items makes you Cursed. Dealing more damage with recoil each hit.]");
        }

        private void TalkHooded(Player player)
        {
            SceneHelpers.Narrate(
                "You move toward the hooded man, but he raises a hand of pure bone to stop you. Without " +
                "moving his mouth, a cold voice reaches you anyway. \"I have no information worth your " +
                "growth. But I will tip the scales. A parting gift, let's leave it to fate.\"");
            SceneHelpers.Narrate(
                "He flips a coin into the air, catches it underfoot. \"Heads or tails? Guess right and I " +
                "grant you a powerful offensive spell. Guess wrong, and a healing one. " +
                "Either has value. Now... guess.\"");

            bool guessHeads = SceneHelpers.AskGuessHeads();
            bool actualHeads = RandomSource.CoinFlip();
            SceneHelpers.Narrate($"He lifts his foot. It's {(actualHeads ? "heads" : "tails")}.");

            if (guessHeads == actualHeads)
            {
                player.Skills.Add(new LearnedSkill(SkillLibrary.FireBall));
                SceneHelpers.Narrate(
                    "\"Well done. The offensive spell, as promised. Don't underestimate a little fireball " +
                    "its power is unmatched.\"");
                SceneHelpers.PrintHint("[Learned: Fireball.]");
            }
            else
            {
                player.Skills.Add(new LearnedSkill(SkillLibrary.Heal));
                SceneHelpers.Narrate(
                    "\"This one isn't fit for a ruler, but it will keep you breathing. Don't write it off.\"");
                SceneHelpers.PrintHint("[Learned: Heal.]");
            }
        }
    }
}
