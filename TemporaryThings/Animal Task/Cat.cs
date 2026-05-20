using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure
{
    internal class Cat : Animals
    {
        private int _lengthOfTail;

        public Cat(string name, int lengthOfTail) : base(name)
        {

            _lengthOfTail = lengthOfTail;

        }


        public void Meow() 
        {
            Console.WriteLine($"MEEEEOOOOOOOOOOW");
            Console.WriteLine($"I have a {_lengthOfTail} long tail");
        }

        public override void ROAR()
        {
            Console.WriteLine("I may be a feline, but I'm no feLION. I cannot roar.");
        }


    }
}
