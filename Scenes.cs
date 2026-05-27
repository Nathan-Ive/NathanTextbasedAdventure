using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextbasedAdventure
{
    /// <summary>
    /// Scenes are the containers for events.
    /// 
    /// In between events choices have to be made, leading to more events, or eventually a game over.
    /// 
    /// A new scene basically sets up a new sequence of possible events. Each scene has a set of events.
    ///     These events range from combat, to exploration. Not every scene has both kinds of events, some of them only have one of the two.
    ///     
    /// Every scene follows a structure, but this structure isn't fully set. 
    ///     The only consistency is that a scene properly announces that it's a new scene.
    /// </summary>


    internal class Scenes
    {



        
        private void Event() { }

    }
}
