using System;
using System.Collections.Generic;
using System.Linq;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// Parses free-form player input against the active event's Choices,
    /// matching by keyword groups and respecting simple negation.
    /// 
    /// Made by AI. (Couldn't think of how to do it, so I consulted and had AI do it.)
    /// (From what I can tell, this uses for loops to check if the terms provided by the player matches what is in the Choice script.
    /// It then checks whether or not the player means the opposite of what they're saying or not 
    /// to account for situations like "Check the tombs that aren't mine")
    /// 
    /// I only partially understand what is in this script, so if there's a bug related to this, I'll have to research it more heavily.
    /// </summary>
    public static class InputParser
    {
        /// <summary>
        /// Attempts to match the player's input against one of the given choices.
        /// </summary>
        /// <param name="input">The raw text the player typed.</param>
        /// <param name="choices">The currently available choices to match against.</param>
        /// <returns>The matched Choice, or null if no unique match was found.</returns>
        public static Choice Match(string input, List<Choice> choices)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            string[] words = Normalize(input);
            var matches = new List<Choice>();

            foreach (var choice in choices)
            {
                if (ChoiceMatches(words, choice, out bool negated))
                {
                    // If the match was negated and the choice has an Opposite, use it.
                    Choice resolved = negated && choice.Opposite != null ? choice.Opposite : choice;
                    if (!matches.Contains(resolved)) matches.Add(resolved);
                }
            }

            // Exactly one unique match → return it. Otherwise ambiguous or none.
            return matches.Count == 1 ? matches[0] : null;
        }

        /// <summary>
        /// Lowercases the input and splits it into individual word tokens.
        /// Strips most punctuation so "the others, please" tokenizes cleanly.
        /// </summary>
        private static string[] Normalize(string input)
        {
            string cleaned = new string(input.ToLower()
                .Select(c => char.IsLetterOrDigit(c) || c == '\'' ? c : ' ')
                .ToArray());
            return cleaned.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Checks whether the player's words satisfy every keyword group in this choice.
        /// A group is satisfied if at least one of its keywords appears in the input.
        /// Sets `negated` to true if any matched keyword was preceded by a negation token.
        /// </summary>
        private static bool ChoiceMatches(string[] words, Choice choice, out bool negated)
        {
            negated = false;

            foreach (var group in choice.KeywordGroups)
            {
                bool groupHit = false;

                for (int i = 0; i < words.Length; i++)
                {
                    if (group.Contains(words[i]))
                    {
                        groupHit = true;

                        // Check the word immediately before this match for negation.
                        if (i > 0 && NegationTokens.Tokens.Contains(words[i - 1]))
                            negated = true;

                        break;
                    }
                }

                if (!groupHit) return false;
            }

            return true;
        }
    }
}
