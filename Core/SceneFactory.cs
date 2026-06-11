using RulerOfTheTomb.Scenes;

namespace RulerOfTheTomb.Core
{
    /// <summary>
    /// Constructs the game's scenes with their events, choices, and loot tables.
    /// Each method returns a fully populated Scene ready to run.
    /// Content is filled in during the content phase; for now these are stubs
    /// so GameSession and the framework can compile and be tested.
    /// </summary>
    public static class SceneFactory
    {
        /// <summary>
        /// Constructs Scene 1: waking up in the tomb. No combat.
        /// </summary>
        public static Scene CreateScene1()
        {
            var scene = new NonCombatScene("Scene 1 — The Awakening");
            // TODO: populate events (grave-digging choices, pouch pickup, exit).
            return scene;
        }

        /// <summary>
        /// Constructs Scene 2: the Legless Fellow encounter. Combat scene.
        /// </summary>
        public static Scene CreateScene2()
        {
            var scene = new NormalScene("Scene 2 — The Legless Fellow");
            // TODO: populate exploration events (floor/walls/ceiling), combat, post-battle loot.
            return scene;
        }

        /// <summary>
        /// Constructs Scene 3: the Riddling Trove encounter. Combat scene with riddle skip.
        /// </summary>
        public static Scene CreateScene3()
        {
            var scene = new NormalScene("Scene 3 — The Riddling Trove");
            // TODO: populate riddle event, combat fallback, post-battle loot.
            return scene;
        }

        /// <summary>
        /// Constructs Scene 4: the underground river interlude. No combat.
        /// </summary>
        public static Scene CreateScene4()
        {
            var scene = new NonCombatScene("Scene 4 — The Underground River");
            // TODO: populate three NPC dialogues (blessed hint, cursed hint, spell drop).
            return scene;
        }

        /// <summary>
        /// Constructs Scene 5: the Radiant Warrior and Dull Scholar. Combat scene with kill-order event.
        /// </summary>
        public static Scene CreateScene5()
        {
            var scene = new NormalScene("Scene 5 — The Twin Guards");
            // TODO: populate combat with kill-order event-skill drops.
            return scene;
        }

        /// <summary>
        /// Constructs the Final Scene: the Legend King. Ends in an Ending regardless of outcome.
        /// </summary>
        public static Scene CreateFinalScene()
        {
            var scene = new FinalScene("Final Scene — The Legend King");
            // TODO: populate boss combat with phase transition and fireball charge event.
            return scene;
        }
    }
}
