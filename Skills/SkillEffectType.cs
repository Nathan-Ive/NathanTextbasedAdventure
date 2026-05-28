namespace RulerOfTheTomb.Skills
{
    /// <summary>
    /// What kind of effect a skill produces when used.
    /// Drives how damage/healing is calculated against the target.
    /// </summary>
    public enum SkillEffectType
    {
        PhysicalDamage,     // Damage that is dealt based on physical defense
        MagicDamage,        // Damage that is dealt based on magic defense
        Heal,               // Recovers health based on case sensitive statistics
        Passive,            // Skills that cannot be used (0 PP), but are constantly active or activate every turn.
        Event               // Triggers an enemy-only event instead of dealing damage or healing. (Do not give to Player)
    }
}
