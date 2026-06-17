using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Items;
using RulerOfTheTomb.Scenes;

namespace TextbasedAdventure.Items.UsableItemLibrary
{
    /// <summary>
    /// A consumable that restores a fixed chunk of the user's HP when used in combat.
    /// </summary>
    internal class HPPotion : Usable
    {
        private const int HealAmount = 80;

        public HPPotion(string name, string description) : base(name, description)
        {
        }

        public override void Use(Actor user, Actor target)
        {
            user.Heal(HealAmount);
            SceneHelpers.Narrate($"You drink the {Name}, restoring {HealAmount} HP.");
        }
    }
}
