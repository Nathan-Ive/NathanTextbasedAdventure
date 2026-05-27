namespace RulerOfTheTomb.Items
{
    /// <summary>
    /// The alignment of an item. Drives the player's Status when equipped
    /// (Blessed/Cursed only) and the game's ending (all owned items).
    /// </summary>
    public enum Alignment
    {
        Neutral,
        Blessed,
        Cursed
    }
}
