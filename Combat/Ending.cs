namespace RulerOfTheTomb.Combat
{
    /// <summary>
    /// The three possible endings of the game, determined by owned item alignment
    /// and the result of the final boss fight.
    /// </summary>
    public enum Ending
    {
        Ruler,    // Good ending: more blessed than cursed owned
        Tyrant,   // Evil ending: more cursed than blessed owned
        Doomed    // Bad ending: lost to the final boss
    }
}
