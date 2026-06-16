using RulerOfTheTomb.Core;
using RulerOfTheTomb.Scenes;

namespace RulerOfTheTomb
{
    /// <summary>
    /// Entry point for the Ruler of the Tomb console application.
    /// Constructs a GameSession and runs the main loop until an ending fires.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Application entry point. Prompts for a player name and starts the session.
        /// </summary>
        /// <param name="args">Command-line arguments (unused).</param>
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Ruler of the Tomb");
            System.Console.WriteLine("-----------------");
            System.Console.Write("Enter your name: ");
            string name = System.Console.ReadLine() ?? "Skeleton";
            System.Console.WriteLine();

            // Quick how-to-play before the first room.
            SceneHelpers.ShowTutorial();

            var session = new GameSession(string.IsNullOrWhiteSpace(name) ? "Skeleton" : name);
            session.Run();
        }
    }
}
