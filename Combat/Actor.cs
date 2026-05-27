using System.Collections.Generic;

namespace RulerOfTheTomb.Combat
{
    /// <summary>
    /// Base class for any participant in combat (Player or Enemy).
    /// Holds shared stats, an inventory, learned skills, and the core combat methods that all actors use.
    /// </summary>
    public abstract class Actor
    {
        // --- Identity ---
        public string Name { get; protected set; }

        // --- Core stats ---
        public int MaxHp { get; protected set; }
        public int CurrentHp { get; protected set; }
        public int MaxMp { get; protected set; }
        public int CurrentMp { get; protected set; }

        public int Strength { get; protected set; }
        public int Magic { get; protected set; }
        public int Defense { get; protected set; }
        public int MagicDefense { get; protected set; }
        public int Speed { get; protected set; }

        /// <summary>
        /// Whether the actor is still alive and able to act in combat.
        /// </summary>
        public bool IsAlive => CurrentHp > 0;

        /// <summary>
        /// Constructs an actor with the given base stats and a fresh empty inventory.
        /// CurrentHp and CurrentMp start at their maximum values. The basic attack is
        /// learned by default.
        /// </summary>
        /// <param name="name">Display name.</param>
        /// <param name="maxHp">Maximum HP.</param>
        /// <param name="maxMp">Maximum MP.</param>
        /// <param name="strength">Physical attack stat.</param>
        /// <param name="magic">Magic attack stat.</param>
        /// <param name="defense">Physical damage mitigation.</param>
        /// <param name="magicDefense">Magic damage mitigation.</param>
        /// <param name="speed">Turn order priority.</param>
        protected Actor(string name, int maxHp, int maxMp,
                       int strength, int magic, int defense, int magicDefense, int speed)
        {
            Name = name;
            MaxHp = maxHp;
            CurrentHp = maxHp;
            MaxMp = maxMp;
            CurrentMp = maxMp;
            Strength = strength;
            Magic = magic;
            Defense = defense;
            MagicDefense = magicDefense;
            Speed = speed;
        }
    }
}
