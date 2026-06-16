using RulerOfTheTomb.Combat;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// Final Scene — The Legend King. The last fight. His signature move is a fireball that
    /// takes several turns to charge, dropping his guard while he winds it up. Survive or
    /// out-damage that window and the ruler falls. The ending the player earns is decided by
    /// FinalScene from their owned gear (and the Scene 5 cube as tiebreaker); losing here
    /// yields the Doomed ending.
    /// </summary>
    public class LegendKingEvent : Event
    {
        public LegendKingEvent() : base("The ruler's throne") { }

        public override void Run(Player player, Scene scene)
        {
            SceneHelpers.Narrate(
                "Beyond the door, a vast hall opens, and at its end a throne. Upon it sits the ruler of " +
                "the tomb, the Legend King, the source of the urge that dragged you down through every " +
                "room and every fight. He rises. There is nowhere left to go but through him.");
            SceneHelpers.PrintHint(
                "[Watch for his fireball: while he charges it he can't strike back. Out-damage it, " +
                "or outlast it.]");

            var fight = new CombatEncounter(player, EnemyLibrary.CreateLegendKing());
            fight.Run();

            // Win or lose, FinalScene.Run inspects the player afterward to pick the ending:
            // survive -> Ruler/Tyrant by alignment; fall -> Doomed.
            if (player.IsAlive)
                SceneHelpers.Narrate("The Legend King collapses. The tomb is yours.");
            else
                SceneHelpers.Narrate(
                    "Your bones give out before his. The ruler stands over you, and the tomb claims " +
                    "another challenger.");
        }
    }
}
