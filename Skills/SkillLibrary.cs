using System.ComponentModel;

namespace RulerOfTheTomb.Skills
{
    /// <summary>
    /// Static catalog of skill definitions used throughout the game.
    /// Centralizes skill creation so the same definition is reused everywhere.
    /// </summary>
    public static class SkillLibrary
    {
        // --- Standard Skills --- //

        /// <summary>
        /// The basic attack every actor knows. Unlimited PP, scales with Strength. Calculates damage based on Defense.
        /// </summary>
        public static readonly Skill BasicAttack = new Skill(
            name: "Attack",
            description: "A standard physical attack.",
            maxPp: -1,
            effectType: SkillEffectType.PhysicalDamage,
            power: 0
        );

        /// <summary>
        /// A basic magic attack unique to certain actors. Unlimited PP, scales with Magic. Deals damage based on Magic Defense.
        /// </summary>
        public static readonly Skill BasicMagicAttack = new Skill(
            name: "Attack",
            description: "A standard magical attack. Primarly used by spell-casters",
            maxPp: -1,
            effectType: SkillEffectType.MagicDamage,
            power: 0
        );

        /// <summary>
        /// A heavy attack every actor knows. Limited PP, high damage, scales with Strength. Calculates damage based on Defense.
        /// </summary>
        public static readonly Skill HeavyAttack = new Skill(
            name: "Attack",
            description: "A powerful physical attack. Swung with a greater level of effort.",
            maxPp: 25,
            effectType: SkillEffectType.PhysicalDamage,
            power: 3
        );



        // --- Weapon Skills --- //

        /// <summary>
        /// A skill made for players who obtain the Stellar-Mass Greatsword. An improved version of the HeavyAttack, scales with Strength. calculates damage based on Defense.
        /// </summary>
        public static readonly Skill ZeroMassSlash = new Skill(
            name: "Zero Mass Slash",
            description: "A heavy physical blow that uses the trait unique to the Stellar-Mass Greatsword to deal a swift heavy blow to the enemy.",
            maxPp: 20,
            effectType: SkillEffectType.PhysicalDamage,
            power: 18
        );

        /// <summary>
        /// A skill made for players who obtain the Marrow-Splitter Mace. A defense ignoring attack. Scales with Strength. Ignores defense.
        /// </summary>
        public static readonly Skill BoneCrushingBlow = new Skill(
            name: "Bone Crushing Blow",
            description: "A blow unique to the menacing Marrow-Splitter Mace. Destroy's the opponents bones on impact, inficting extreme levels of damage.",
            maxPp: 10,
            effectType: SkillEffectType.TrueDamage,
            power: 12
        );



        // --- Skill Scroll Skills --- //

        /// <summary>
        /// The unmastered version of the ultimate skill. This skill is unique to the player, as it's only obtained from the Skill Scroll provided in scene 5.
        /// </summary>
        public static readonly Skill FireBall = new Skill(
            name: "Fireball",
            description: "A powerful magic technique. Once mastered, it is an attack with no equal, fit for a king.",
            maxPp: 5,
            effectType: SkillEffectType.MagicDamage,
            power: 26
        );

        public static readonly Skill Heal = new Skill(
            name: "Heal",
            description: "A healing technique that revitalizes the user down to the marrow of their bones. An esoteric technique not even the most legendary of figures know.",
            maxPp: -1,
            effectType: SkillEffectType.Heal,
            power: 4
        );



        // --- Enemy Skills --- //

        public static readonly Skill SolarSlash = new Skill(
            name: "Solar Slash",
            description: "The strongest skill belonging to a legendary warrior. Used to express his respect to a powerful opponent.",
            maxPp: 2,
            effectType: SkillEffectType.PhysicalDamage,
            power: 6
        );

        public static readonly Skill LunarBlast = new Skill(
            name: "Lunar Blast",
            description: "The strongest skill belonging to a legendary scholar. He deems this his greatest invention.",
            maxPp: 2,
            effectType: SkillEffectType.MagicDamage,
            power: 6
        );

        public static readonly Skill CorrodingPuke = new Skill(
            name: "Corroding Puke",
            description: "A last ditch effort from an enraged treasure chest. How dare you waste my time!",
            maxPp: 5,
            effectType: SkillEffectType.PhysicalDamage,
            power: 4
        );

        public static readonly Skill LeglessStrike = new Skill(
            name: "Legless Strike",
            description: "An all out attack used by someone who has a deep understanding of combat. If I could still walk... I'd own this tomb!",
            maxPp: 1,
            effectType: SkillEffectType.PhysicalDamage,
            power: 10
        );

        public static readonly Skill MasteredFireBall = new Skill(
            name: "O-Fireball",
            description: "The ultimate skill. Incinerates its target on contact. Few could survive this attack. I dare you to overthrow me!",
            maxPp: 5,
            effectType: SkillEffectType.MagicDamage,
            power: 40
        );



    }
}
