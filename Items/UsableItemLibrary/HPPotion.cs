using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure.Items.UsableItemLibrary
{
    internal class HPPotion : Usable
    {
        public HPPotion(string name, string description) : base(name, description)
        {
        }

        public override void Use(Actor user, Actor target)
        {
            //Recovers a certain amount of the selected Actor's HP.
        }
    }
}
