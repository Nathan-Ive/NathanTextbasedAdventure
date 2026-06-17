using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Items;
using RulerOfTheTomb.Scenes;

namespace TextbasedAdventure.Items.UsableItemLibrary
{
    /// <summary>
    /// A throwable rock. Deals a flat amount of damage that ignores armor, which makes it the
    /// best option against heavily armored, brittle foes (like the Legless Fellow). A thrown
    /// rock also interrupts an enemy that is charging a scripted attack.
    /// </summary>
    internal class Pebble : Usable
    {
        private const int ThrowDamage = 25;

        public Pebble(string name, string description) : base(name, description)
        {
        }

        public override void Use(Actor user, Actor target)
        {
            target.TakeRawDamage(ThrowDamage);
            SceneHelpers.Narrate(
                $"You hurl the {Name}, cracking {target.Name} for {ThrowDamage} damage that ignores armor.");

            if (target is Enemy enemy)
                enemy.Interrupt();
        }
    }
}
