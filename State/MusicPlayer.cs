using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WPF_Music_Player.State
{
    public class MusicPlayer
    {
        private WaveOut player;
        private List<IMusicElement> elements; //list of elements to be controlled when something in the player changes
        private IMusicWidget currentlyPlayingSong; //the current playlist song widget
        private bool songLoaded = false;
        private Timer songTimer = null;
        private AudioFileReader currentReader;



        /// <summary>
        /// the music player handles, play and pausing songs
        /// </summary>
        public MusicPlayer()
        {

            //init player
            this.player = new WaveOut();
            this.elements = new List<IMusicElement>();



        }

        /// <summary>
        /// loads a song into the music player
        /// </summary>
        /// <param name="path">path of song file to load</param>
        public void LoadSong(string path)
        {
            Debug.WriteLine("Loading " + path);
            currentReader = new AudioFileReader(path);
            this.player.Init(currentReader);

            //set reader on time change interval
            if (this.songTimer != null)
            {
                this.songTimer.Stop();
            }
            this.songTimer = new Timer();
            this.songTimer.Interval = 1000;
            this.songTimer.Tick += TimeChanged;



            Debug.WriteLine("Loaded " + path);



        }

        //resets the player
        public void Reset()
        {
            this.Pause();
            this.songTimer = null;
            this.currentlyPlayingSong = null;
            this.currentReader = null;
            this.songLoaded = false;
        }

        /// <summary>
        /// plays the current loaded song if it's not already playing
        /// </summary>
        public void Play()
        {
            if (!this.IsPlaying() && songLoaded)
            {
                this.player.Play();
                foreach (IMusicElement element in this.elements)
                {
                    element.Play();
                }

                //start song timer
                if (this.songTimer != null)
                {
                    this.songTimer.Start();
                }

            }




        }

        /// <summary>
        /// pauses the current song
        /// </summary>
        public void Pause()
        {
            this.player.Pause();
            foreach (IMusicElement element in this.elements)
            {
                element.Pause();
            }

            //pause song timer
            if (this.songTimer != null)
            {
                this.songTimer.Stop();
            }

        }

        /// <summary>
        /// changes the current song playing
        /// </summary>
        public void ChangeSong(string path)
        {
            if (this.currentlyPlayingSong != null)
            {
                this.currentlyPlayingSong.UnHighlight();
            }
            this.songLoaded = true;
            this.currentlyPlayingSong = null;
            this.Pause();
            this.LoadSong(path);
            this.Play();
        }

        /// <summary>
        /// changes the current song playing and sets the current song widget
        /// </summary>
        /// <param name="path"></param>
        /// <param name="widget"></param>
        public void ChangeSong(string path, IMusicWidget widget)
        {
            this.ChangeSong(path);
            this.currentlyPlayingSong = widget;
            this.currentlyPlayingSong.Highlight();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if the music player is playing</returns>
        public bool IsPlaying()
        {
            return this.player.PlaybackState == PlaybackState.Playing;
        }

        /// <summary>
        /// adds a new element to the control list
        /// </summary>
        /// <param name="newElem">element to be controlled by the player</param>
        public void AddElement(IMusicElement newElem)
        {
            this.elements.Add(newElem);
        }


        /// <summary>
        /// true if a song is loaded into the player
        /// </summary>
        /// <returns></returns>
        public bool IsSongLoaded()
        {
            return songLoaded;
        }

        private void TimeChanged(object sender, EventArgs e)
        {
         
            if (this.currentReader != null)
            {
                foreach (IMusicElement element in this.elements)
                {
                    element.OnTimeChange((int)this.currentReader.CurrentTime.TotalSeconds, (int)this.currentReader.TotalTime.TotalSeconds);
                }
            }

            //end clause
            if (this.currentReader.TotalTime.TotalSeconds == this.currentReader.CurrentTime.TotalSeconds)
            {
                this.Reset();
            }
        }

        /// <summary>
        /// sets the current song positions to the given percent. 0 is start of song. 100 is end of song
        /// </summary>
        /// <param name="percent"></param>
        public void SetSongPercent(int percent)
        {
           
            if (IsSongLoaded())
            {

                double onePercent = (this.currentReader.TotalTime.TotalSeconds) / 100;
                double newSongTime = onePercent * percent;
                this.currentReader.CurrentTime = TimeSpan.FromSeconds(newSongTime);
            }
        }


    }

    public interface IMusicElement
    {
        ///a music element has parts which should be controlled by the player

        void Play()
        {
            //do nothing
        }
        void Pause()
        {
            //do nothing
        }

        //called when the time of the song changes. takes an int which is the seconds that have passed in the song
        void OnTimeChange(int time, int total)
        {
            //do nothing
        }
    }

    public interface IMusicWidget
    {
        ///a widget like a song or album
        ///

        void Highlight();
        void UnHighlight();
    }
}
