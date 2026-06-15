using System;
using System.Collections.Generic;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// A single player-selectable choice within an Event. Holds the keyword groups
    /// the parser uses to recognize the choice and the handler that fires on match.
    /// 
    /// Partially made by AI. (Since AI completely made the edge cases for input, I decided I should also use it to help with the input itself.)
    /// </summary>
    public class Choice
    {
        /// <summary>
        /// Display text shown to the player, with {keyword} markers highlighting parser keywords.
        /// </summary>
        public string DisplayText { get; }

        /// <summary>
        /// Keyword groups. The parser matches this choice only if every group has at least one hit.
        /// </summary>
        public List<List<string>> KeywordGroups { get; }

        /// <summary>
        /// Optional opposite-choice reference for negation handling.
        /// If the player negates a keyword in this choice ("not mine"), the parser
        /// substitutes this choice instead. Null if no opposite exists.
        /// </summary>
        public Choice Opposite { get; set; }

        /// <summary>
        /// The action to run when this choice is selected.
        /// </summary>
        public Action OnSelected { get; }

        /// <summary>
        /// Constructs a choice with display text, parser keywords, and an action.
        /// </summary>
        /// <param name="displayText">Text shown to the player with {keyword} markers.</param>
        /// <param name="keywordGroups">Keyword groups required for the parser to match.</param>
        /// <param name="onSelected">The callback to fire when this choice is selected.</param>
        public Choice(string displayText, List<List<string>> keywordGroups, Action onSelected)
        {
            DisplayText = displayText;
            KeywordGroups = keywordGroups;
            OnSelected = onSelected;
        }
    }
}
