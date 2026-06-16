using System.Collections.Generic;
using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Items;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// What happened at the end of a Scene's execution. Drives GameSession's
    /// decision on whether to advance, restart, or display an ending.
    /// </summary>
    public enum SceneOutcome
    {
        Continue,   // Move to NextScene
        GameOver,   // Player died outside the final boss
        Ending      // Player reached an ending
    }

    /// <summary>
    /// The result of running a scene, passed back to GameSession for next-step decisions.
    /// </summary>
    public class SceneResult
    {
        /// <summary>What kind of outcome occurred.</summary>
        public SceneOutcome Outcome { get; }

        /// <summary>The next scene to run, when Outcome is Continue.</summary>
        public Scene NextScene { get; }

        /// <summary>The ending earned, when Outcome is Ending.</summary>
        public Ending Ending { get; }

        /// <summary>
        /// Constructs a scene result.
        /// </summary>
        /// <param name="outcome">The kind of outcome that occurred.</param>
        /// <param name="nextScene">The next scene, if continuing.</param>
        /// <param name="ending">The earned ending, if ending the game.</param>
        public SceneResult(SceneOutcome outcome, Scene nextScene = null, Ending ending = default)
        {
            Outcome = outcome;
            NextScene = nextScene;
            Ending = ending;
        }
    }

    /// <summary>
    /// Base class for any room in the game. Each scene is a chain of Events that
    /// run in sequence (or branch based on player choice), produce loot, and either
    /// hand off to the next scene, trigger a game-over, or trigger an ending.
    /// </summary>
    public abstract class Scene
    {
        /// <summary>
        /// The scene's display name, used in transitions and help output.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Items available to be picked up during this scene. Cleared on scene exit, missed items are gone forever.
        /// </summary>
        public List<Item> AvailablePickups { get; protected set; } = new List<Item>();

        /// <summary>
        /// The chain of events that make up this scene. Run in order, though branching
        /// is allowed, an event can activate a non-sequential next event.
        /// </summary>
        public List<Event> Events { get; protected set; } = new List<Event>();

        /// <summary>
        /// The currently active event, used for routing player input and Help output.
        /// </summary>
        public Event ActiveEvent => Events.Find(e => e.IsActive);

        /// <summary>
        /// The scene that follows this one in the chain. Set by SceneFactory when the
        /// game is built. NonCombatScene and NormalScene return this from GetNextScene;
        /// FinalScene ignores it (the game ends there).
        /// </summary>
        public Scene Next { get; set; }

        /// <summary>
        /// Constructs a scene with the given name.
        /// </summary>
        /// <param name="name">The scene's display name.</param>
        protected Scene(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Runs the scene from start to finish. Returns a SceneResult indicating
        /// whether to advance, game-over, or trigger an ending.
        /// </summary>
        /// <param name="player">The player character.</param>
        /// <returns>The result describing how this scene ended.</returns>
        public abstract SceneResult Run(Player player);
    }
}
