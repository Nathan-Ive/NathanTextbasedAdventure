using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure
{
    /// <summary>
    /// Actors have the following things:
    /// 
    /// Statistics: Health, MP, Attack, Defense, Magic Attack, Magic Defense, Speed
    /// Commands: Basic Attack, Skills, Defend, Items/Inventory
    /// 
    /// Inventories: Containers of what items they own and how many of that item they own.
    ///     Inventories have two states (Whether they own or don't own a pouch). Pouchless inventories are limited. Pouched inventories are not.
    ///     
    /// Equipment: An extention of inventories. What the actor is currently wearing or holding. Specific slots have specific functions. 
    ///     A sword cannot be put on the head slot, for example. Or a usable item, which cannot be equipped at all.
    /// 
    /// Skills: A list of passive or active commands that have a variety of effects. Certain events provide the player with skills to use.
    ///     The skill library for this project will be small. I just want to test if I can get it working the way I want it to.
    /// </summary>


    internal class Actors
    {
        private int _health;
        private int _manaPoints;
        private int _attack;
        private int _defense;
        private int _magicAttack;
        private int _magicDefense;
        private int _speed;

        //private List<> _inventory
        //private List<> _commands
        //private List<> _skills


    }
}
