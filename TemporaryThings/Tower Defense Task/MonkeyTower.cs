using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure
{
    internal class MonkeyTower : Tower
    {
        private string _name;

        public MonkeyTower (string name) : base(name) 
        {
            _name = name;
        }

        public void Shoot() 
        {
            Console.WriteLine($"{_name} shoots.");
        }

    }
}
