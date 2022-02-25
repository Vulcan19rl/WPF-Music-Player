using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TagLib;
using WPF_Music_Player.General;
using WPF_Music_Player.General.Constants;

namespace WPF_Music_Player.State
{
    public class Song
    {

        private string path;
        private File tagLibFile;

        private static readonly int SONG_LIST_ELEMENT_HEIGHT = 50;

        /// <summary>
        /// the song object holds data about a specific song 
        /// </summary>
        /// <param name="path"> path of song</param>
        public Song(string path)
        {
            this.path = path;
            ReadData();
           
            
        }

        /// <summary>
        /// reads the data from the path file
        /// </summary>
        private void ReadData()
        {
            tagLibFile = File.Create(path);
        }



        public string GetTitle()
        {
            return this.tagLibFile.Tag.Title;
        }

        public string[] GetArtists()
        {
            return this.tagLibFile.Tag.AlbumArtists;
        }

        public TimeSpan GetDuration()
        {
            return this.tagLibFile.Properties.Duration;
        }


        /// <summary>
        /// creates a song list element from the song data
        /// </summary>
        /// <returns></returns>
        public Grid CreateSongListElement()
        {
            SongListElement grid = new SongListElement(path);
            ColumnDefinition titleColumn = new ColumnDefinition(){ Width = new GridLength(0.3, GridUnitType.Star)};
            grid.ColumnDefinitions.Add(titleColumn);
            ColumnDefinition artistColumn = new ColumnDefinition() { Width = new GridLength(0.3, GridUnitType.Star) };
            grid.ColumnDefinitions.Add(artistColumn);
            ColumnDefinition durationColumn = new ColumnDefinition() { Width = new GridLength(0.3, GridUnitType.Star) };
            grid.ColumnDefinitions.Add(durationColumn);


            Label titleLabel = new Label() { 
                Content = GetTitle(), 
                VerticalAlignment = VerticalAlignment.Center, 
                Foreground = Colors.PRIMARY_TEXT_COLOR_BRUSH,
                FontFamily = Fonts.PRIMARY_FONT_FAMILY, 
                FontWeight = FontWeights.Bold
            };
            Grid.SetColumn(titleLabel, 0);
            grid.Children.Add(titleLabel);

            Label aristLabel = new Label() { Content = String.Join(", ", GetArtists()),
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Colors.PRIMARY_TEXT_COLOR_BRUSH,
                FontFamily = Fonts.PRIMARY_FONT_FAMILY,
                FontWeight = FontWeights.Bold
            };
            Grid.SetColumn(aristLabel, 1);
            grid.Children.Add(aristLabel);


            Label durationLabel = new Label() { Content = GetDuration().StripMilliseconds(),
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Colors.PRIMARY_TEXT_COLOR_BRUSH,
                FontFamily = Fonts.PRIMARY_FONT_FAMILY,
                FontWeight = FontWeights.Bold
            };
            Grid.SetColumn(durationLabel, 2);
            grid.Children.Add(durationLabel);

            grid.Height = SONG_LIST_ELEMENT_HEIGHT;


            

            return grid ;
        }


    }

    public class SongListElement : Grid, IMusicWidget
    {
        string path;
        bool isHighlighted = false;

        public SongListElement(string path) : base()
        {
            this.path = path;

            //set events
            this.MouseEnter += SongListElementHover;
            this.MouseLeave += SongListElementLeave;
            this.MouseUp += SongListElementClick;

        }

        public void Highlight()
        {
            this.Background = Colors.SECONDARY_HIGHLIGHT_COLOR_BRUSH;
            this.isHighlighted = true;
        }

        public void UnHighlight()
        {
            this.Background = Colors.TRANSPARENT_COLOR_BRUSH;
            this.isHighlighted = false;
        }

        private void SongListElementHover(Object sender, EventArgs e)
        {
            Grid senderGrid = (Grid)sender;
            senderGrid.Background = Colors.PRIMARY_HIGHLIGHT_COLOR_BRUSH;
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void SongListElementLeave(Object sender, EventArgs e)
              
        {
            Grid senderGrid = (Grid)sender;
            if (this.isHighlighted)
            {
                  senderGrid.Background = Colors.SECONDARY_HIGHLIGHT_COLOR_BRUSH;
            }
            else{
              
                senderGrid.Background = Colors.TRANSPARENT_COLOR_BRUSH;
              
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void SongListElementClick(Object sender, EventArgs e)
        {
            StateHolder.Current.GetMusicPlayer().ChangeSong(this.path, this);
        }
    }
}
