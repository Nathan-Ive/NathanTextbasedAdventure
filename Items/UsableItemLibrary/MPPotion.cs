using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Items;
using RulerOfTheTomb.Scenes;

namespace TextbasedAdventure.Items.UsableItemLibrary
{
    /// <summary>
    /// A consumable that restores a fixed chunk of the user's MP when used in combat.
    /// </summary>
    internal class MPPotion : Usable
    {
        private const int RestoreAmount = 15;

        public MPPotion(string name, string description) : base(name, description)
        {
        }

        public override void Use(Actor user, Actor target)
        {
            user.RestoreMp(RestoreAmount);
            SceneHelpers.Narrate($"You drink the {Name}, restoring {RestoreAmount} MP.");
        }
    }
}
