namespace RulerOfTheTomb.Combat
{
    /// <summary>
    /// The player-controlled Actor. Extends Actor with a status system driven by
    /// equipped alignment items, and the ending-determination logic that reads
    /// from the inventory.
    /// </summary>
    public class Player : Actor
    {
        public Player(string name, int maxHp, int maxMp, 
                      int strength, int magic, int defense, int magicDefense, int speed) 
            : base(name, maxHp, maxMp, strength, magic, defense, magicDefense, speed)
        {
        }
    }
}
