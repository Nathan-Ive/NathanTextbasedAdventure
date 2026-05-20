using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextbasedAdventure.TemporaryThings.Tower_Defense_Task
{
    internal class HealMonkey : Tower
    {
        private string _name;
        
        public HealMonkey(string name) : base(name) 
        {
            _name = name;
        }

        public void Heal()
        {
            Console.WriteLine($"{_name} heals.");
        }

    }
}
