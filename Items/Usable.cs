using RulerOfTheTomb.Combat;

namespace RulerOfTheTomb.Items
{
    /// <summary>
    /// An item that can be consumed in combat for an instant effect.
    /// Cannot be equipped, cannot be aligned. Removed from inventory after use.
    /// </summary>
    public abstract class Usable : Item
    {
        /// <summary>
        /// Constructs a usable item. Alignment is always Neutral for usables.
        /// </summary>
        /// <param name="name">Display name.</param>
        /// <param name="description">Flavor description.</param>
        protected Usable(string name, string description)
            : base(name, description, Alignment.Neutral) { }

        /// <summary>
        /// Applies this item's effect when used in combat.
        /// Children override to define what the item actually does (damage, heal, etc.).
        /// </summary>
        /// <param name="user">The actor using the item.</param>
        /// <param name="target">The actor being targeted (may be the user themselves).</param>
        public abstract void Use(Actor user, Actor target);
    }
}
