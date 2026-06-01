using RulerOfTheTomb.Combat;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// A scene with both exploration and combat. Examples: Scene 2 (Legless Fellow),
    /// Scene 3 (Riddling Trove), Scene 5 (Radiant Warrior + Dull Scholar).
    /// </summary>
    public class NormalScene : Scene
    {
        /// <summary>
        /// Constructs a normal scene with the given name.
        /// </summary>
        /// <param name="name">The scene's display name.</param>
        public NormalScene(string name) : base(name) { }

        /// <summary>
        /// Runs each event in order. If the player dies during a combat event, returns GameOver.
        /// Otherwise returns Continue with the next scene.
        /// </summary>
        /// <param name="player">The player character.</param>
        /// <returns>A Continue or GameOver result.</returns>
        public override SceneResult Run(Player player)
        {
            foreach (var ev in Events)
            {
                ev.IsActive = true;
                ev.Run(player, this);
                ev.IsActive = false;

                // If the player died during a combat event, propagate game-over upward.
                if (!player.IsAlive)
                    return new SceneResult(SceneOutcome.GameOver);
            }

            return new SceneResult(SceneOutcome.Continue, GetNextScene());
        }

        /// <summary>
        /// Returns the scene that follows this one. Override or set during construction.
        /// </summary>
        protected virtual Scene GetNextScene() => null;
    }
}
