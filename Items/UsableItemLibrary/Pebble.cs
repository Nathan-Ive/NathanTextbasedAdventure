using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure.Items.UsableItemLibrary
{
    internal class Pebble : Usable
    {
        public Pebble(string name, string description) : base(name, description)
        {
        }

        public override void Use(Actor user, Actor target)
        {
        }
    }
}
