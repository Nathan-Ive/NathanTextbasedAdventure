using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Scenes;
using RulerOfTheTomb.Skills;

namespace TextbasedAdventure.Items.UsableItemLibrary
{
    /// <summary>
    /// A skill scroll that teaches the user the Fireball spell when used.
    /// </summary>
    internal class FireBallSkillScroll : SkillScroll
    {
        public FireBallSkillScroll(string name, string description) : base(name, description)
        {
        }

        public override void Use(Actor user, Actor target)
        {
            user.Skills.Add(new LearnedSkill(SkillLibrary.FireBall));
            SceneHelpers.Narrate($"You study the {Name} and learn Fireball.");
        }
    }
}
