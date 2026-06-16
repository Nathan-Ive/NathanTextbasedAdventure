using System.Collections.Generic;
using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Items;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// Scene 2 — The Legless Fellow. A non-linear room you explore before an unavoidable
    /// fight. Inspecting the ceiling reveals a trap above the enemy; the lever on the wall
    /// only does anything once you know what it's connected to. Whether the trap is left
    /// active decides the fight's outcome and which aligned item you can take afterwards:
    /// deactivate it for a fair fight and the Blessed Solar-Eclipse Armor, or leave it and
    /// let it crush him for the Cursed Marrow-Splitter Mace.
    /// </summary>
    public class LeglessFellowEvent : Event
    {
        private bool _ceilingInspected;
        private bool _trapDeactivated;
        private int _lookAheadStage;
        private bool _ambushed;     // greed at the floor got an arm shattered before the fight
        private bool _proceed;      // player committed to the fight

        public LeglessFellowEvent() : base("The Legless Fellow's chamber") { }

        public override void Run(Player player, Scene scene)
        {
            SceneHelpers.Narrate(
                "After a long walk down a surprisingly large hallway, you enter the next room and " +
                "immediately sense danger, and sense that it knows you're here. Something heavy hits " +
                "the floor at the far end: a powerful skeleton in pristine armor, wielding an ominous " +
                "mace. It would be an impossible fight, if not for one thing, it has no legs. All it " +
                "can do is crawl toward you, swinging in a rage. It stays by the door, so a fight is " +
                "unavoidable if you want to progress.");
            SceneHelpers.Narrate(
                "Still, this is a chance to prepare. Tombs like this are full of traps meant for " +
                "grave-robbers, it'd be best to find them before they catch you off guard, or perhaps turn one against " +
                "your opponent.");

            BuildChoices(player);
            SceneHelpers.RunEventLoop(scene, () => _proceed);

            // The fight itself.
            if (_ambushed)
            {
                int dealt = player.TakeDamage(20);
                SceneHelpers.Narrate(
                    "Your reach for the rock comes a beat too late, the mace lands a perfect strike, " +
                    $"shattering one of your arms for {dealt} damage and forcing the fight.");
            }

            SceneHelpers.Narrate("The Legless Fellow drags himself forward, mace raised. The fight begins.");
            var fight = new CombatEncounter(player, EnemyLibrary.CreateLeglessFellow());
            CombatResult result = fight.Run();

            if (result == CombatResult.PlayerLost || !player.IsAlive)
                return; // NormalScene will see the death and trigger game-over.

            ResolveVictory(player);
        }

        private void BuildChoices(Player player)
        {
            Choices.Add(new Choice(
                "Inspect the {floor}",
                new List<List<string>> { new List<string> { "floor", "ground", "feet" } },
                InspectFloor));

            Choices.Add(new Choice(
                "Inspect the {walls}",
                new List<List<string>> { new List<string> { "wall", "walls", "lever" } },
                InspectWalls));

            Choices.Add(new Choice(
                "Inspect the {ceiling}",
                new List<List<string>> { new List<string> { "ceiling", "roof", "above", "up" } },
                InspectCeiling));

            Choices.Add(new Choice(
                "{Fight} the Legless Fellow",
                new List<List<string>> { new List<string> { "fight", "attack", "battle", "engage" } },
                () => _proceed = true));
        }

        private void InspectFloor()
        {
            if (_lookAheadStage == 0)
            {
                SceneHelpers.Narrate(
                    "You inspect the floor around your feet. Nothing. No traps, no pressure plates, " +
                    "no spikes.");
            }

            while (true)
            {
                if (!SceneHelpers.AskYesNo("Look further ahead?"))
                {
                    SceneHelpers.Narrate("You stop looking ahead and return to where you were standing.");
                    return;
                }

                _lookAheadStage++;
                if (_lookAheadStage == 1)
                {
                    SceneHelpers.Narrate(
                        "The floor ahead is littered with broken weapons, discarded junk and a " +
                        "single rock. A well-aimed throw could end this swiftly. It's further down, " +
                        "though, rather close to the skeleton.");
                }
                else if (_lookAheadStage == 2)
                {
                    SceneHelpers.Narrate(
                        "You creep forward for the rock. As you reach down, the mace swings. You barely " +
                        "dodge, dropping the rock in the scramble. Just a little closer and it's yours.");
                }
                else
                {
                    SceneHelpers.Narrate(
                        "Greed wins. You lunge for the rock one last time, and the mace catches you " +
                        "clean.");
                    _ambushed = true;
                    _proceed = true;
                    return;
                }
            }
        }

        private void InspectWalls()
        {
            SceneHelpers.Narrate(
                "The walls are quick to read. Nothing of note, except a lever on your side of the room. " +
                "An obvious trap, or something that disarms one. Worth the risk?");

            if (!SceneHelpers.AskYesNo("Flip the lever?"))
            {
                SceneHelpers.Narrate("You decide the lever isn't worth the trouble and step back.");
                return;
            }

            if (_ceilingInspected)
            {
                _trapDeactivated = true;
                SceneHelpers.Narrate(
                    "Knowing the trap hangs above the skeleton, you throw the lever. The grinding of a " +
                    "mechanism overhead stops, the trap is deactivated. A fair fight, harder for you, " +
                    "but honest. You step back into position.");
            }
            else
            {
                SceneHelpers.Narrate(
                    "You hesitate, with no idea what the lever controls. The nerve drains out of you. " +
                    "Too great a risk blind, you leave it alone and step back.");
            }
        }

        private void InspectCeiling()
        {
            if (_ceilingInspected)
            {
                SceneHelpers.Narrate("You've already spotted the trap above your opponent.");
                return;
            }

            _ceilingInspected = true;
            SceneHelpers.Narrate(
                "You look up. It's immediately obvious: a trap, right above your opponent. Now that " +
                "you've seen it, there's no way it can take you by surprise.");
        }

        private void ResolveVictory(Player player)
        {
            if (_trapDeactivated)
            {
                SceneHelpers.Narrate(
                    "You endure his devastating final attack, the effort shattering his own weapon. " +
                    "Disarmed, he takes your finishing blow. A battle well fought, uninterrupted by the " +
                    "trap you disabled. His mace is ruined, but his armor is intact, radiating a mighty, " +
                    "guiding presence. It feels like it comes with an oath. Can you carry that weight?");

                if (SceneHelpers.AskYesNo("Inspect and take the Solar-Eclipse Armor?"))
                {
                    GrantArmor(player, ItemLibrary.CreateSolarEclipseArmor());
                    SceneHelpers.PrintHint("[Obtained: Solar-Eclipse Armor (Blessed).]");
                }
                else
                {
                    SceneHelpers.Narrate("You leave the armor with its fallen owner and move on.");
                }
            }
            else
            {
                SceneHelpers.Narrate(
                    "Before you can land the last blow, the ceiling trap slams down, crushing the Legless " +
                    "Fellow and his armor instantly. His mace flies loose, skidding to your feet, a heavy, " +
                    "ominous thing. His armor's gone, but at least the weapon survived.");

                if (SceneHelpers.AskYesNo("Inspect and take the Marrow-Splitter Mace?"))
                {
                    GrantWeapon(player, ItemLibrary.CreateMarrowSplitterMace());
                    SceneHelpers.PrintHint("[Obtained: Marrow-Splitter Mace (Cursed).]");
                }
                else
                {
                    SceneHelpers.Narrate("You leave the cruel-looking mace where it lies and move on.");
                }
            }

            SceneHelpers.Narrate(
                "The skeleton blocking the way is gone. You move ahead into a hall that leads to a " +
                "staircase going straight down, and descend into the dark until you reach the bottom.");
        }

        // ---------- Loot helpers ----------

        private static void GrantArmor(Player player, Equipment armor)
        {
            if (player.Inventory.HasPouch) player.Inventory.Add(armor);
            else player.Inventory.Equip(armor, EquipmentSlot.Armor);
            player.RecalculateStatus();
        }

        private static void GrantWeapon(Player player, Equipment weapon)
        {
            if (player.Inventory.HasPouch) player.Inventory.Add(weapon);
            else player.Inventory.Equip(weapon, weapon.Slot);
            player.RecalculateStatus();
        }
    }
}
