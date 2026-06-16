using RulerOfTheTomb.Scenes;

namespace RulerOfTheTomb.Core
{
    /// <summary>
    /// Builds the game's scenes, wires each one's events, and links them into a chain.
    /// CreateScene1 is the single entry point used by GameSession. 
    /// It constructs the whole run and returns the first scene, so a game-over (which rebuilds from Scene 1) 
    /// gets a fresh chain while the player's inventory carries over.
    /// </summary>
    public static class SceneFactory
    {
        /// <summary>
        /// Builds the entire scene chain and returns the opening scene (Scene 1).
        /// </summary>
        public static Scene CreateScene1()
        {
            // Scene 1 — The Awakening (non-combat, fully non-linear exploration).
            var scene1 = new NonCombatScene("Scene 1 — The Awakening");
            scene1.Events.Add(new AwakeningEvent());

            // Scene 2 — The Legless Fellow (explore, then an unavoidable fight).
            var scene2 = new NormalScene("Scene 2 — The Legless Fellow");
            scene2.Events.Add(new LeglessFellowEvent());

            // Scene 3 — The Riddling Trove (riddles, or a fight on failure).
            var scene3 = new NormalScene("Scene 3 — The Riddling Trove");
            scene3.Events.Add(new RiddlingTroveEvent());

            // Scene 4 — The Underground River (one conversation, then a restful crossing).
            var scene4 = new NonCombatScene("Scene 4 — The Underground River");
            scene4.Events.Add(new RiverCrossingEvent());

            // Scene 5 — The Twin Guards (loot, then a two-stage fight; kill order matters).
            var scene5 = new NormalScene("Scene 5 — The Twin Guards");
            scene5.Events.Add(new TwinGuardsEvent());

            // Final Scene — The Legend King (boss fight that always resolves to an ending).
            var finalScene = new FinalScene("Final Scene — The Legend King");
            finalScene.Events.Add(new LegendKingEvent());

            // Link the chain.
            scene1.Next = scene2;
            scene2.Next = scene3;
            scene3.Next = scene4;
            scene4.Next = scene5;
            scene5.Next = finalScene;

            return scene1;
        }
    }
}
