namespace RulerOfTheTomb.Skills
{
    /// <summary>
    /// Static catalog of skill definitions used throughout the game.
    /// Centralizes skill creation so the same definition is reused everywhere.
    /// </summary>
    public static class SkillLibrary
    {
        /// <summary>
        /// The basic attack every actor knows. Unlimited PP, scales with Strength.
        /// </summary>
        public static readonly Skill BasicAttack = new Skill(
            name: "Attack",
            description: "A standard physical strike.",
            maxPp: -1,
            effectType: SkillEffectType.PhysicalDamage,
            power: 0
        );
    }
}
