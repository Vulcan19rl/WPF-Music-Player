using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using WPF_Music_Player.General.Constants;
using WPF_Music_Player.State;

namespace WPF_Music_Player.Widgets
{
    public partial class MusicProgressBar : Grid, IMusicElement
    {
        //a grid which displays the current progress of a song horizontally


        private ColumnDefinition progressColumn;
        private ColumnDefinition emptyColumn;
        Rectangle progressRectangle;
        Rectangle emptyRectangle;
        public MusicProgressBar() : base()
        {
            this.Background = Colors.TRANSPARENT_COLOR_BRUSH;

            //set columns
            this.progressColumn = new ColumnDefinition();
            this.progressColumn.Width = System.Windows.GridLength.Auto;
            this.emptyColumn = new ColumnDefinition();
            this.ColumnDefinitions.Add(progressColumn);
            this.ColumnDefinitions.Add(emptyColumn);

            //create rectangle to fill progress column
            progressRectangle = new Rectangle();
            progressRectangle.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            progressRectangle.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            progressRectangle.Fill = Colors.WHITE_COLOR_BRUSH;
            Grid.SetColumn(progressRectangle, 0);
            this.Children.Add(progressRectangle);

            //create empty rectangle to fill empty column
            emptyRectangle = new Rectangle();
            emptyRectangle.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            emptyRectangle.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            emptyRectangle.Fill = Colors.SECONDARY_COLOR_BRUSH;
            Grid.SetColumn(emptyRectangle, 1);
            this.Children.Add(emptyRectangle);

            //connect to current music player
            StateHolder.Current.GetMusicPlayer().AddElement(this);

            //onclick should change point in song
            this.MouseDown += MusicProgressBarOnClick;

        }


        //updates the current progress bar with a given percentage as an int - max value is 100
        private void updateProgress(int percentage)
        {

        
            progressRectangle.Width = (this.ActualWidth / 100) * percentage;
            emptyRectangle.Width = Math.Max(0, (this.ActualWidth / 100) * (100 - percentage));


        }

        public void OnTimeChange(int time, int total)
        {
         
            updateProgress((int)(time / (total / 100.00)));
        }


        /**
         *changes the progress of the song based on where the click is positioned
         **/
        public void MusicProgressBarOnClick(Object sender, MouseEventArgs e)
        {
            
            double xPos = e.GetPosition(this).X;
            int widthPercent = (int)Math.Floor(xPos / (this.ActualWidth / 100.00));
            StateHolder.Current.GetMusicPlayer().SetSongPercent(widthPercent);


        }
    }
}
