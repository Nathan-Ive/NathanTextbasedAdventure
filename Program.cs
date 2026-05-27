using RulerOfTheTomb.Core;
using TextbasedAdventure.TemporaryThings.Abstract_TD_Task;

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
            DartMonkey monkey1 = new DartMonkey();
            SuperMonkey monkey2 = new SuperMonkey();
        }
    }
}
