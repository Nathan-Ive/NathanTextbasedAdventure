using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure
{
    internal class MainProgram
    {
        Cat kitty1 = new Cat("Maya", 2);
        Lion lion1 = new Lion("Bob");

        public void Start() 
        {

            kitty1.Meow();
            kitty1.Sleep();
            kitty1.SayName();

            lion1.SayName();
            lion1.ROAR();
        
        
        
        }

    }
}
