using System.Collections.Generic;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// The set of words the parser treats as negating the keyword that follows them.
    /// </summary>
    internal static class NegationTokens
    {
        /// <summary>
        /// All recognized negation words. Lowercased; the parser lowercases input
        /// before checking against this set.
        /// </summary>
        public static readonly HashSet<string> Tokens = new HashSet<string>
        {
            "not", "no", "isn't", "isnt", "aren't", "arent", "don't", "dont",
            "doesn't", "doesnt", "won't", "wont", "never"
        };
    }
}
