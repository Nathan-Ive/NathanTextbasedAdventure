namespace RulerOfTheTomb.Items
{
    /// <summary>
    /// A non-equippable, non-usable item kept in inventory for event checks
    /// or alignment tiebreaking. The Pouch and the Scene 5 blessed/cursed drops
    /// are the main examples.
    /// </summary>
    public class KeyItem : Item
    {
        /// <summary>
        /// Whether this key item is the Pouch, which unlocks Inventory function.
        /// </summary>
        public bool IsPouch { get; protected set; }

        /// <summary>
        /// Constructs a key item with optional alignment (for Scene 5 tiebreaker items)
        /// and an optional pouch flag.
        /// </summary>
        /// <param name="name">Display name.</param>
        /// <param name="description">Flavor description.</param>
        /// <param name="alignment">The item's alignment. Neutral by default.</param>
        /// <param name="isPouch">True only for the Pouch item that unlocks the inventory bag.</param>
        public KeyItem(string name, string description,
                       Alignment alignment = Alignment.Neutral,
                       bool isPouch = false)
            : base(name, description, alignment)
        {
            IsPouch = isPouch;
        }
    }
}
