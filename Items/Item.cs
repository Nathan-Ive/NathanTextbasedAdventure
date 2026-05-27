namespace RulerOfTheTomb.Items
{
    /// <summary>
    /// Base class for any item that can exist in an Actor's inventory.
    /// Children are Equipment, Usable, and KeyItem.
    /// </summary>
    public abstract class Item
    {
        /// <summary>
        /// The display name of the item, shown to the player in menus and pickups.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// A short flavor description shown when the player inspects the item.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// The item's alignment. Neutral by default, overridden by Blessed/Cursed items.
        /// </summary>
        public Alignment Alignment { get; protected set; }

        /// <summary>
        /// Constructs an item with a name, description, and alignment.
        /// </summary>
        /// <param name="name">Display name.</param>
        /// <param name="description">Flavor description.</param>
        /// <param name="alignment">The item's alignment. Defaults to Neutral.</param>
        protected Item(string name, string description, Alignment alignment = Alignment.Neutral)
        {
            Name = name;
            Description = description;
            Alignment = alignment;
        }
    }
}
