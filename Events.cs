using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure
{
    /// <summary>
    /// Events are components of scenes that display text
    /// 
    /// Dialogue events are events that only have text. They have no combat options, nor do they end or continue after a choice is given.
    ///     They simply start and end without specific player input changing the course of the dialogue.
    ///     
    /// Combat events are the primary gameplay points where the player can lose. The lose condition being that if the player actor's HP reaches 0.
    ///     An adtional category for combat events are dialogue events that happen within combat that change the state of one of the actors.
    /// 
    /// 
    /// Exploration events are the most elaborate, but are primarily handled by Scenes themselves. 
    ///     Coming with multiple smaller dialogue events that occur as a result of a choice to interact with something.
    /// 
    /// 
    /// 
    /// </summary>

    internal class Events
    {
    }
}
