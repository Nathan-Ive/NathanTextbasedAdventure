namespace RulerOfTheTomb.Combat
{
    /// <summary>
    /// The non-standard status states a Player can be in based on equipped alignment items.
    /// Drives passive effects in combat.
    /// </summary>
    public enum PlayerStatus
    {
        Normal,
        Lucky,    // 1 blessed equipped: +crit chance
        Blessed,  // 2 blessed equipped: removes crit bonus, grants one free revive
        Hexed,    // 1 cursed equipped: +dodge chance, HP drain each turn
        Cursed    // 2 cursed equipped: removes dodge/drain, big damage boost with recoil
    }
}
