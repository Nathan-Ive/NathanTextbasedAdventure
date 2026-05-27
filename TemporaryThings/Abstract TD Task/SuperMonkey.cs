using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure.TemporaryThings.Abstract_TD_Task
{
    internal class SuperMonkey : AbstrTower
    {
        public override void Shoot()
        {
            Console.WriteLine("Super Monkey shoots.");
            Console.Write("Pew!");
            Console.Write("Pew!");
            Console.Write("Pew!");
            Console.WriteLine("Pew!");
        }
    }
}
