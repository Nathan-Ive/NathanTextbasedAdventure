using System;

namespace RulerOfTheTomb.Utility
{
    /// <summary>
    /// Random number generator script that the entire game will use. Dictates the results of certain events.
    /// Also dictates the chances of RNG in combat, like critical hits, dodge chances, etc.
    /// Centralizing the RNG allows reproducible playthroughs for testing and means rapid successive calls don't generate similar results.
    /// </summary>
    public static class RandomSource
    {
        private static Random _rng = new Random();
        ///<summary>
        /// Reseeds the RNG with a specific value. Intended for scene and event use.
        /// </summary>
        /// <param name="seed">The seed value to initialize the RNG with.</param>
        public static void Seed(int seed) => _rng = new Random(seed);


        /// <summary>
        /// Returns true with the given probability (0.0 to 1.0 | Basically 0% to 100%). Intended for combat use.
        /// </summary>
        /// <param name="chance">Probability of success, where 1.0 is always and 0.0 is never.</param>
        public static bool Roll(double chance) => _rng.NextDouble() < chance;

        /// <summary>
        /// Returns a random integer in [min, max) — min inclusive, max exclusive.
        /// </summary>
        public static int Range(int minInclusive, int maxExclusive) => _rng.Next(minInclusive, maxExclusive);

        /// <summary>
        /// Returns a random index for a list of the given length.
        /// </summary>
        public static int Index(int listLength) => _rng.Next(0, listLength);


        /// <summary>
        /// 50/50 coin flip. Used for things like the random spell drop in Scene 4, it being a 1 in 2 result.
        /// </summary>
        public static bool CoinFlip() => _rng.NextDouble() < 0.5;
    }
}
