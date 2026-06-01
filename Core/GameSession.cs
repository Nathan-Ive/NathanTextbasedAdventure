using RulerOfTheTomb.Combat;
using RulerOfTheTomb.Scenes;

namespace RulerOfTheTomb.Core
{
    /// <summary>
    /// The top-level container for a single playthrough. Owns the Player,
    /// tracks the current scene, and runs the main game loop until an ending fires.
    /// </summary>
    public class GameSession
    {
        /// <summary>
        /// The player character. Persists across scenes and resets on game-over
        /// only in stats and HP — inventory and progress carry over within one run.
        /// </summary>
        public Player Player { get; private set; }

        /// <summary>
        /// The scene the player is currently in.
        /// </summary>
        public Scene CurrentScene { get; private set; }

        /// <summary>
        /// Whether the game loop should continue running. Set to false when an ending fires.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Constructs a new session with a fresh Player and starts at Scene 1.
        /// </summary>
        /// <param name="playerName">The name to give the player character.</param>
        public GameSession(string playerName)
        {
            Player = new Player(playerName);
            CurrentScene = SceneFactory.CreateScene1();
            IsRunning = true;
        }

        /// <summary>
        /// Runs the main game loop. Each iteration executes the current scene,
        /// receives the next scene (or null on game-over / ending), and advances.
        /// </summary>
        public void Run()
        {
            while (IsRunning)
            {
                SceneResult result = CurrentScene.Run(Player);

                switch (result.Outcome)
                {
                    case SceneOutcome.Continue:
                        CurrentScene = result.NextScene;
                        break;
                    case SceneOutcome.GameOver:
                        HandleGameOver();
                        break;
                    case SceneOutcome.Ending:
                        HandleEnding(result.Ending);
                        break;
                }
            }
        }

        /// <summary>
        /// Resets the player to starting condition and sends them back to Scene 1.
        /// Inventory carries over per design — only HP and combat state reset.
        /// </summary>
        private void HandleGameOver()
        {
            Player.FullHeal();
            CurrentScene = SceneFactory.CreateScene1();
        }

        /// <summary>
        /// Displays the final ending narration and stops the game loop.
        /// </summary>
        /// <param name="ending">The ending the player earned.</param>
        private void HandleEnding(Ending ending)
        {
            EndingDisplay.Show(ending);
            IsRunning = false;
        }
    }
}
