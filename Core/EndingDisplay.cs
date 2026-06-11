using System;
using RulerOfTheTomb.Combat;

namespace RulerOfTheTomb.Core
{
    /// <summary>
    /// Static helper for displaying the final ending narration based on which Ending the player earned.
    /// Called by GameSession once the FinalScene returns an Ending result.
    /// </summary>
    public static class EndingDisplay
    {
        /// <summary>
        /// Prints the narration for the given ending, then waits for the player to acknowledge before exiting.
        /// </summary>
        /// <param name="ending">The Ending to display.</param>
        public static void Show(Ending ending)
        {
            Console.WriteLine();
            Console.WriteLine("======================================");

            switch (ending)
            {
                case Ending.Ruler:
                    PrintRulerEnding();
                    break;
                case Ending.Tyrant:
                    PrintTyrantEnding();
                    break;
                case Ending.Doomed:
                    PrintDoomedEnding();
                    break;
            }

            Console.WriteLine("======================================");
            Console.WriteLine();
            Console.WriteLine("Press any key to close.");
            Console.ReadKey(true);
        }

        /// <summary>
        /// The Good Ending. Triggered by owning more Blessed items than Cursed,
        /// or by tying with a Blessed key item, and defeating the Legend King.
        /// </summary>
        private static void PrintRulerEnding()
        {
            Console.WriteLine("[Ruler of the Tomb — Good Ending]");
            Console.WriteLine();
            Console.WriteLine("[Ending narration placeholder.]");
        }

        /// <summary>
        /// The Evil Ending. Triggered by owning more Cursed items than Blessed,
        /// or by tying with a Cursed key item, and defeating the Legend King.
        /// </summary>
        private static void PrintTyrantEnding()
        {
            Console.WriteLine("[Tyrant of the Tomb — Evil Ending]");
            Console.WriteLine();
            Console.WriteLine("[Ending narration placeholder.]");
        }

        /// <summary>
        /// The Bad Ending. Triggered by losing the final fight against the Legend King.
        /// </summary>
        private static void PrintDoomedEnding()
        {
            Console.WriteLine("[Doomed to the Tomb — Bad Ending]");
            Console.WriteLine();
            Console.WriteLine("[Ending narration placeholder.]");
        }
    }
}
