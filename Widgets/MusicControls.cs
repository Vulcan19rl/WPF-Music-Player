using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_Music_Player.State;

namespace WPF_Music_Player.Widgets
{
    /// <summary>
    /// A widget to control the current playing music. 
    /// </summary>
    /// 
    public partial class MusicControls : Grid, IMusicElement
    {
        IconButton playButton;

        private static readonly string PLAY_ICON_PATH = "/Assets/Icons/play.png";
        private static readonly string PAUSE_ICON_PATH = "/Assets/Icons/pause.png";

        public MusicControls() : base()
        {
            //init colors
            this.Background = General.Constants.Colors.PRIMARY_COLOR_BRUSH;


            //init columns
            ColumnDefinition prevColumn = new ColumnDefinition(); //previous song column
            ColumnDefinition playColumn = new ColumnDefinition(); //play/pause song column
            ColumnDefinition nextColumn = new ColumnDefinition(); //next song column
            this.ColumnDefinitions.Add(prevColumn);
            this.ColumnDefinitions.Add(playColumn);
            this.ColumnDefinitions.Add(nextColumn);

            //init icons
            playButton = new IconButton(PLAY_ICON_PATH);
            playButton.HorizontalAlignment = HorizontalAlignment.Center;
            playButton.VerticalAlignment = VerticalAlignment.Center;

            playButton.Click += PlayPause;


            Grid.SetColumn(playButton, 1);
            this.Children.Add(playButton);

            //add to control list
            StateHolder.Current.GetMusicPlayer().AddElement(this);

        }

        /// <summary>
        /// plays or pauses the music
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayPause(Object sender, EventArgs e)
        {
            //turn music on or off

            MusicPlayer player = StateHolder.Current.GetMusicPlayer();
            if (player.IsPlaying())
            {
                Pause();
                player.Pause();
            }
            else if(player.IsSongLoaded())
            {
                Play();
                player.Play();
            }
        }


        /// <summary>
        /// updates into the playing state
        /// </summary>
        public void Play()
        {
            this.playButton.ChangeImage(PAUSE_ICON_PATH);
        }


        /// <summary>
        /// updates into the pausing state
        /// </summary>
        public void Pause()
        {
            this.playButton.ChangeImage(PLAY_ICON_PATH);
        }
    }
}
