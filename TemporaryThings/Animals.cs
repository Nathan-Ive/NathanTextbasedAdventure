using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure
{
    internal class Animals
    {
        public Animals(string name)
        {
            _name = name;

        }

        private string _eyeColor;
        //public string EyeColor;
        protected string EyeColor2 
        {
            get => _eyeColor;
            private set 
            {
                _eyeColor = value;
            }
        }

        protected string SkinColor;
        private string _name;


        public void Sleep()
        {
            Console.WriteLine($"SNORE");
        }


        public void SayName() 
        {
            Console.WriteLine($"My name is {_name}");
        
        }

        public virtual void ROAR()
        {
            Console.WriteLine("GAO");
        }


        //Name is private because it's either the only value that isn't always set, 
        //or it's the only value that's always unique and is unlikely to be shared between species
    }
}
