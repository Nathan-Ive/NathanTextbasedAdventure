namespace RulerOfTheTomb.Skills
{
    /// <summary>
    /// An Actor's instance of a known Skill, tracking that actor's remaining PP.
    /// Wraps a shared Skill definition with per-actor state.
    /// </summary>
    public class LearnedSkill
    {
        /// <summary>
        /// The skill definition this learned skill is based on.
        /// </summary>
        public Skill Skill { get; }

        /// <summary>
        /// The actor's current remaining uses of this skill.
        /// Ignored if the underlying skill is unlimited.
        /// </summary>
        public int CurrentPp { get; private set; }

        /// <summary>
        /// True if this skill is currently usable (unlimited, or has PP remaining).
        /// </summary>
        public bool CanUse => Skill.IsUnlimited || CurrentPp > 0;

        /// <summary>
        /// Constructs a learned skill, initializing CurrentPp to the skill's max.
        /// Unlimited skills store 0 for CurrentPp; CanUse handles them via IsUnlimited.
        /// </summary>
        /// <param name="skill">The skill definition to learn.</param>
        public LearnedSkill(Skill skill)
        {
            Skill = skill;
            CurrentPp = skill.IsUnlimited ? 0 : skill.MaxPp;
        }

        /// <summary>
        /// Attempts to spend one PP. Returns whether the skill was usable.
        /// Unlimited skills always succeed without spending.
        /// </summary>
        /// <returns>True if the skill could be used, false if out of PP.</returns>
        public bool TrySpend()
        {
            if (Skill.IsUnlimited) return true;
            if (CurrentPp <= 0) return false;
            CurrentPp--;
            return true;
        }
    }
}
