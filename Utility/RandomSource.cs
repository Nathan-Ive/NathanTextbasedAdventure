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

        // Reseeds the RNG with a specific value. Intended for scene and event use.
        /// <param name="seed">The seed value to initialize the RNG with.</param>
        public static void Seed(int seed) => _rng = new Random(seed);

        // Returns true with the given probability (0.0 to 1.0 | Basically 0% to 100%). Intended for combat use.
        /// <param name="chance">Probability of success, where 1.0 is always and 0.0 is never.</param>
        public static bool Roll(double chance) => _rng.NextDouble() < chance;

        // 50/50 coin flip. Used for things like the random spell drop in Scene 4, it being a 1 in 2 result.
        public static bool CoinFlip() => _rng.NextDouble() < 0.5;
    }
}
