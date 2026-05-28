using RulerOfTheTomb.Combat;

namespace RulerOfTheTomb.Skills
{
    /// <summary>
    /// Definition of a skill an Actor can use in combat. Shared across all actors
    /// who learn it; per-actor PP is tracked separately via LearnedSkill.
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// The display name shown in the combat menu and dialogue.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// A short description of what the skill does, shown when inspecting it.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// The maximum number of times this skill can be used per playthrough.
        /// Use -1 to indicate unlimited use (basic attack).
        /// </summary>
        public int MaxPp { get; protected set; }

        /// <summary>
        /// True if this skill has no use limit (e.g. the basic attack).
        /// </summary>
        public bool IsUnlimited => MaxPp < 0;

        /// <summary>
        /// What kind of effect this skill produces when executed.
        /// </summary>
        public SkillEffectType EffectType { get; protected set; }

        /// <summary>
        /// The base power of the skill: damage dealt for damage skills,
        /// HP restored for heal skills. Ignored for Event skills.
        /// </summary>
        public int Power { get; protected set; }

        /// <summary>
        /// Optional event hook invoked when this skill is used. Used by enemy
        /// event skills (Legless Fellow's trap, Legend King's fireball, etc.)
        /// to advance scene state. Null for most skills.
        /// </summary>
        public System.Action<Actor, Actor> OnUse { get; protected set; }

        /// <summary>
        /// Constructs a skill definition.
        /// </summary>
        /// <param name="name">Display name.</param>
        /// <param name="description">Flavor description.</param>
        /// <param name="maxPp">Max use count; -1 for unlimited.</param>
        /// <param name="effectType">The category of effect this skill produces.</param>
        /// <param name="power">Damage or heal amount; ignored for Event skills.</param>
        /// <param name="onUse">Optional callback fired when the skill is used.</param>
        public Skill(string name, string description, int maxPp,
                     SkillEffectType effectType, int power = 0,
                     System.Action<Actor, Actor> onUse = null)
        {
            Name = name;
            Description = description;
            MaxPp = maxPp;
            EffectType = effectType;
            Power = power;
            OnUse = onUse;
        }
    }
}
