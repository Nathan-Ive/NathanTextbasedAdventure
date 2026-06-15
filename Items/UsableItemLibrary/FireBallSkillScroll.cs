using RulerOfTheTomb.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure.Items.UsableItemLibrary
{
    internal class FireBallSkillScroll : SkillScroll
    {
        public FireBallSkillScroll(string name, string description) : base(name, description)
        {
        }

        public override void Use(Actor user, Actor target)
        {
            //Adds the Fireball skill to the selected Actor.
        }
    }
}
