using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using WPF_Music_Player.General.Constants;
using WPF_Music_Player.State;

namespace WPF_Music_Player.Widgets
{
    /// <summary>
    /// music list is a scroll viewer containing a list of music track. Click on music tracks plays them
    /// </summary>
    public partial class MusicList : ScrollViewer
    {
        List<Song> songs; //all the songs in the viewer

        StackPanel songStack;

        private static readonly int SEPARATOR_HEIGHT = 1;

        public MusicList()
        {
            songs = new List<Song>();
            songStack = new StackPanel();
            this.songStack.Margin = new System.Windows.Thickness(15, 0, 15, 0);
            this.Content = songStack;
            this.Background = Colors.SECONDARY_COLOR_BRUSH;
            this.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
   
        }


        /// <summary>
        /// adds a song to the music list
        /// </summary>
        /// <param name="song">path of the song</param>
        public void AddSong(string song)
        {
            Song newSong = new Song(song);
            this.songs.Add(newSong);
            this.songStack.Children.Add(newSong.CreateSongListElement());
            this.songStack.Children.Add(GetSeparator());
            
        }

        private Rectangle GetSeparator() {
            Rectangle outRect = new Rectangle()
            {
                Height = SEPARATOR_HEIGHT,
                Stretch = System.Windows.Media.Stretch.UniformToFill,
                Fill = Colors.WHITE_COLOR_BRUSH

            };


            return outRect;
        }


        /// <summary>
        /// adds multiple songs to the song stack
        /// </summary>
        /// <param name="song">list of paths to the songs</param>
        public void AddSongs(IEnumerable<string> songs)
        {
            foreach(string song in songs)
            {
                AddSong(song);
            }
        }
    }
}
