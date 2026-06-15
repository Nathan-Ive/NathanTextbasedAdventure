using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextbasedAdventure.TemporaryThings.Tower_Defense_Task;

namespace TextbasedAdventure
{
    internal class MainProgram
    {
        Cat kitty1 = new Cat("Maya", 2);
        Lion lion1 = new Lion("Bob");

        MonkeyTower attackTower = new MonkeyTower("Dart Monkey");
        HealMonkey supportTower = new HealMonkey("Healer Monkey");

        public void Start() 
        {
            attackTower.PlaceTower();
            attackTower.Shoot();
            supportTower.PlaceTower();
            supportTower.Heal();        
        }

    }
}
