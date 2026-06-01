using System;

namespace RulerOfTheTomb.Combat
{
    /// <summary>
    /// An AI-controlled Actor that fights the Player. Adds a decision-making hook
    /// for choosing actions in combat, plus a flag for whether this enemy ends the
    /// game on defeat (the final boss).
    /// </summary>
    public class Enemy : Actor
    {
        /// <summary>
        /// Whether defeating this enemy ends the game with an ending rather than
        /// just returning the player to the scene flow. True only for the Legend King.
        /// </summary>
        public bool IsFinalBoss { get; protected set; }

        /// <summary>
        /// Constructs an enemy with the given stats. By default, enemies are not the final boss.
        /// </summary>
        public Enemy(string name, int maxHp, int maxMp,
                     int strength, int magic, int defense, int magicDefense, int speed,
                     bool isFinalBoss = false)
            : base(name, maxHp, maxMp, strength, magic, defense, magicDefense, speed)
        {
            IsFinalBoss = isFinalBoss;
        }

        /// <summary>
        /// Chooses what action this enemy takes on its turn. Default behavior is a basic attack,
        /// but specific enemies (Legless Fellow, Riddling Trove, Legend King) will override this
        /// to add patterns, item use, or event-triggering skills.
        /// </summary>
        /// <param name="target">The actor this enemy is fighting, typically the Player.</param>
        public virtual void TakeTurn(Actor target)
        {
            int dealt = target.TakeDamage(Strength);
            Console.WriteLine($"{Name} attacks {target.Name} for {dealt} damage.");
        }
    }
}
