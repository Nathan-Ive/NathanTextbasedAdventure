using RulerOfTheTomb.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure.Items.UsableItemLibrary
{
    internal abstract class SkillScroll : Usable
    {
        protected SkillScroll(string name, string description) : base(name, description)
        {
        }
    }
}
