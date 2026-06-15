using System;
using System.Text;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// Static helpers for scene presentation: narration printing, keyword highlighting,
    /// choice prompts, help command, and the main input loop for an event.
    /// 
    /// Partially made with the help of AI. (This exists to basically hint at the player what terms are choices.
    /// And in extreme enough cases, it outright tells what the player has to do. I came up with this idea myself,
    /// but I didn't know how to change the color of words in the console to make it clear that they're different from standard text.)
    /// </summary>
    public static class SceneHelpers
    {
        // --- Color scheme ---
        private const ConsoleColor KeywordColor = ConsoleColor.Yellow;
        private const ConsoleColor HintColor = ConsoleColor.Cyan;
        private const ConsoleColor SystemColor = ConsoleColor.DarkGray;

        /// <summary>
        /// Prints narration text with no special formatting. Used for descriptive prose.
        /// </summary>
        /// <param name="text">The narration to print.</param>
        public static void Narrate(string text)
        {
            Console.WriteLine(text);
            Console.WriteLine();
        }

        /// <summary>
        /// Prints text with {keyword} markers rendered in the highlight color.
        /// Example: "Dig through the {other graves} or your {own grave}."
        /// </summary>
        /// <param name="text">Text containing {keyword} markers.</param>
        public static void PrintHighlighted(string text)
        {
            int i = 0;
            while (i < text.Length)
            {
                if (text[i] == '{')
                {
                    int end = text.IndexOf('}', i);
                    if (end < 0)
                    {
                        // Malformed marker; just print the rest as-is.
                        Console.Write(text.Substring(i));
                        break;
                    }

                    string keyword = text.Substring(i + 1, end - i - 1);
                    Console.ForegroundColor = KeywordColor;
                    Console.Write(keyword);
                    Console.ResetColor();
                    i = end + 1;
                }
                else
                {
                    Console.Write(text[i]);
                    i++;
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// Prints a gameplay hint in the hint color (cyan).
        /// Used for mechanical guidance like "type 'help' to see your options."
        /// </summary>
        /// <param name="text">The hint to print.</param>
        public static void PrintHint(string text)
        {
            Console.ForegroundColor = HintColor;
            Console.WriteLine(text);
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Prints a system message in the dim color. Used for transitions and meta-info.
        /// </summary>
        /// <param name="text">The system message to print.</param>
        public static void PrintSystem(string text)
        {
            Console.ForegroundColor = SystemColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Runs the input loop for the scene's currently active event. Reads player input,
        /// handles the help command, parses choices, fires the matched choice's action.
        /// Returns once a choice has been selected.
        /// </summary>
        /// <param name="scene">The scene whose active event we're handling input for.</param>
        public static void RunEventInput(Scene scene)
        {
            Event active = scene.ActiveEvent;
            if (active == null)
            {
                PrintSystem("[No active event to handle input for.]");
                return;
            }

            // Display the available choices once when entering the event.
            DisplayChoices(active);

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(input))
                {
                    PrintHint("Try typing what you'd like to do, or 'help' to see your options.");
                    continue;
                }

                string trimmed = input.Trim().ToLower();

                if (trimmed == "help")
                {
                    PrintHelp(active);
                    continue;
                }

                Choice matched = InputParser.Match(input, active.Choices);

                if (matched == null)
                {
                    PrintHint("That doesn't seem to match anything you can do here. Try 'help' if you're stuck.");
                    continue;
                }

                matched.OnSelected();
                return;
            }
        }

        /// <summary>
        /// Prints the active event's choices to the player, with keywords highlighted.
        /// </summary>
        private static void DisplayChoices(Event active)
        {
            foreach (var choice in active.Choices)
            {
                PrintHighlighted("- " + choice.DisplayText);
            }
        }

        /// <summary>
        /// Prints the help text for the active event: a warning, then the keyword groups
        /// that the parser will accept for each choice.
        /// </summary>
        private static void PrintHelp(Event active)
        {
            PrintHint($"[Help — {active.Label}]");
            PrintHint("This will hint at the words the game is looking for. Continuing reduces the puzzle.");

            foreach (var choice in active.Choices)
            {
                StringBuilder hint = new StringBuilder("  Try referring to: ");
                for (int g = 0; g < choice.KeywordGroups.Count; g++)
                {
                    var group = choice.KeywordGroups[g];
                    hint.Append(string.Join("/", group));
                    if (g < choice.KeywordGroups.Count - 1)
                        hint.Append(" + ");
                }
                PrintHint(hint.ToString());
            }
        }
    }
}
