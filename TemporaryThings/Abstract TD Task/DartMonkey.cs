using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure.TemporaryThings.Abstract_TD_Task
{
    internal class DartMonkey : AbstrTower
    {
        public override void Shoot()
        {
            Console.WriteLine("Dart Monkey shoots.");
            Console.WriteLine("Pew!");
        }
    }
}
