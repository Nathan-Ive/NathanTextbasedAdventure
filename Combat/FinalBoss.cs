namespace RulerOfTheTomb.Combat
{
    public class FinalBoss : Enemy
    {
        /// <summary>
        /// Whether defeating this enemy ends the game with an ending rather than
        /// just returning the player to the scene flow. True only for the Legend King.
        /// </summary>
        public bool IsFinalBoss { get; protected set; }

        public FinalBoss(string name, int maxHp, int maxMp,
                     int strength, int magic, int defense, int magicDefense, int speed)
            : base(name, maxHp, maxMp, strength, magic, defense, magicDefense, speed)
        {
        }
    }
}
