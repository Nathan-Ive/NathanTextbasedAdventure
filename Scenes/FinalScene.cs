using RulerOfTheTomb.Combat;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// The final scene. Contains the boss combat and ending determination.
    /// Unlike NormalScene, a loss here triggers the Doomed ending rather than a game-over reset.
    /// </summary>
    public class FinalScene : Scene
    {
        /// <summary>
        /// Constructs the final scene with the given name.
        /// </summary>
        /// <param name="name">The scene's display name.</param>
        public FinalScene(string name) : base(name) { }

        /// <summary>
        /// Runs each event in order, then returns an Ending result based on whether
        /// the player survived and their owned alignment items.
        /// </summary>
        /// <param name="player">The player character.</param>
        /// <returns>An Ending result.</returns>
        public override SceneResult Run(Player player)
        {
            foreach (var ev in Events)
            {
                ev.IsActive = true;
                ev.Run(player, this);
                ev.IsActive = false;
            }

            // Final scene always ends in an ending — win, lose, or alignment-determined.
            bool wonFight = player.IsAlive;
            Ending ending = player.DetermineEnding(wonFight);
            return new SceneResult(SceneOutcome.Ending, ending: ending);
        }
    }
}
