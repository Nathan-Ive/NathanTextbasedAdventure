using RulerOfTheTomb.Combat;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// A scene with exploration but no combat. Examples: Scene 1 (waking up),
    /// Scene 4 (the underground river interlude).
    /// </summary>
    public class NonCombatScene : Scene
    {
        /// <summary>
        /// Constructs a non-combat scene with the given name.
        /// </summary>
        /// <param name="name">The scene's display name.</param>
        public NonCombatScene(string name) : base(name) { }

        /// <summary>
        /// Runs each event in order, then returns Continue with the next scene.
        /// Override GetNextScene to set the destination.
        /// </summary>
        /// <param name="player">The player character.</param>
        /// <returns>A Continue result with the next scene.</returns>
        public override SceneResult Run(Player player)
        {
            foreach (var ev in Events)
            {
                ev.IsActive = true;
                ev.Run(player, this);
                ev.IsActive = false;
            }

            return new SceneResult(SceneOutcome.Continue, GetNextScene());
        }

        /// <summary>
        /// Returns the scene that follows this one. Defaults to the linked Next scene,
        /// but can be overridden for branching.
        /// </summary>
        protected virtual Scene GetNextScene() => Next;
    }
}
