using System.Collections.Generic;
using RulerOfTheTomb.Combat;

namespace RulerOfTheTomb.Scenes
{
    /// <summary>
    /// A single event within a Scene. Events chain together to form the scene's flow.
    /// Each event has its own choices, keyword groups, and active state.
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// A short name for this event, used in Help output to label its choice list.
        /// </summary>
        public string Label { get; protected set; }

        /// <summary>
        /// Whether this event is the one currently accepting player input.
        /// Only one event in a scene is active at a time.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The choices currently available within this event. Each choice has its own
        /// keyword groups for the parser and a handler that fires when matched.
        /// </summary>
        public List<Choice> Choices { get; protected set; } = new List<Choice>();

        /// <summary>
        /// Constructs an event with the given label. Starts inactive.
        /// </summary>
        /// <param name="label">The display label for this event (used in Help output).</param>
        protected Event(string label)
        {
            Label = label;
            IsActive = false;
        }

        /// <summary>
        /// Runs this event's narration and waits for the player to make a choice.
        /// Children override to define what the event actually does.
        /// </summary>
        /// <param name="player">The player character.</param>
        /// <param name="scene">The scene this event belongs to, used for advancing scene state.</param>
        public abstract void Run(Player player, Scene scene);
    }
}
