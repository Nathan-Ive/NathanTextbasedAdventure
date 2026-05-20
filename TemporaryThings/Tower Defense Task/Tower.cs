using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure
{
    abstract internal class Tower
    {
        private string _name;

        public Tower(string name) 
        {
            _name = name;

            Console.WriteLine($"New Tower, {_name} has been made");
        }

        public void PlaceTower() 
        {
            Console.WriteLine($"{_name} tower is placed.");
        }


    }
}
