using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Music_Player.State
{

    /// <summary>
    /// handles state control
    /// </summary>
    public class StateHolder
    {
        public static StateHolder Current;
        

        /// <summary>
        /// initializes the current state holder in a static reference
        /// </summary>
        public static void Init()
        {
            Current = new StateHolder();
        }


        //music player object
        private MusicPlayer musicPlayer;

        public StateHolder()
        {
            this.musicPlayer = new MusicPlayer();

        }

        /// <summary>
        /// returns the current music player
        /// </summary>
        public MusicPlayer GetMusicPlayer()
        {
            return this.musicPlayer;
        }


    }
}
