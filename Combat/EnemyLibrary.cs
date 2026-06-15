namespace RulerOfTheTomb.Combat
{
    /// <summary>
    /// Static factory methods for the game's enemies. Each method returns a fresh
    /// Enemy or FinalBoss instance with that enemy's fixed stats.
    /// 
    /// Partially made with the help of AI. (Made more efficient for the sake of making new / resetting enemy instances.)
    /// </summary>
    public static class EnemyLibrary
    {
        /// <summary>
        /// Scene 2's enemy. Heavy armor, powerful mace, no legs.
        /// </summary>
        public static Enemy CreateLeglessFellow() => new Enemy(
            name: "Legless Fellow",
            maxHp: 100, 
            maxMp: 10,
            strength: 38, 
            magic: 1,
            defense: 30, 
            magicDefense: 26,
            speed: 5
        );

        /// <summary>
        /// Scene 3's enemy. A treasure chest that animates when its riddles are failed.
        /// </summary>
        public static Enemy CreateRiddlingTrove() => new Enemy(
            name: "Riddling Trove",
            maxHp: 500, 
            maxMp: 20,
            strength: 20, 
            magic: 10,
            defense: 12, 
            magicDefense: 12,
            speed: 12
        );

        /// <summary>
        /// One of Scene 5's twin guards. Drops the Blessed Cube when killed first.
        /// </summary>
        public static Enemy CreateRadiantWarrior() => new Enemy(
            name: "Radiant Warrior",
            maxHp: 750, 
            maxMp: 0,
            strength: 30, 
            magic: 5,
            defense: 28, 
            magicDefense: 24,
            speed: 20);

        /// <summary>
        /// One of Scene 5's twin guards. Drops the Cursed Cube when killed first.
        /// </summary>
        public static Enemy CreateDullScholar() => new Enemy(
            name: "Dull Scholar",
            maxHp: 450, 
            maxMp: 300,
            strength: 18, 
            magic: 22,
            defense: 6, 
            magicDefense: 12,
            speed: 9
        );

        /// <summary>
        /// The final boss. Defeating or losing to him ends the game.
        /// </summary>
        public static FinalBoss CreateLegendKing() => new FinalBoss(
            name: "Legend King",
            maxHp: 1000, 
            maxMp: 150,
            strength: 38, 
            magic: 38,
            defense: 32, 
            magicDefense: 30,
            speed: 18
        );
    }
}