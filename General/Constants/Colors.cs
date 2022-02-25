using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF_Music_Player.General.Constants
{

    /// <summary>
    /// general color constants for the music player
    /// </summary>
    public class Colors
    {
        public static readonly Color PRIMARY_COLOR = Color.FromRgb(50, 143, 168);
        public static readonly SolidColorBrush PRIMARY_COLOR_BRUSH = new SolidColorBrush(PRIMARY_COLOR);

        public static readonly Color SECONDARY_COLOR = Color.FromRgb(43, 151, 181);
        public static readonly SolidColorBrush SECONDARY_COLOR_BRUSH = new SolidColorBrush(SECONDARY_COLOR);

        public static readonly Color PRIMARY_HIGHLIGHT_COLOR = Color.FromArgb(50, 255, 255, 255);
        public static readonly SolidColorBrush PRIMARY_HIGHLIGHT_COLOR_BRUSH = new SolidColorBrush(PRIMARY_HIGHLIGHT_COLOR);

        public static readonly Color SECONDARY_HIGHLIGHT_COLOR = Color.FromArgb(30, 255, 255, 255);
        public static readonly SolidColorBrush SECONDARY_HIGHLIGHT_COLOR_BRUSH = new SolidColorBrush(SECONDARY_HIGHLIGHT_COLOR);

        public static readonly SolidColorBrush WHITE_COLOR_BRUSH = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        public static readonly SolidColorBrush BLACK_COLOR_BRUSH = new SolidColorBrush(Color.FromRgb(0,0,0));
        public static readonly SolidColorBrush TRANSPARENT_COLOR_BRUSH = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        public static readonly SolidColorBrush PRIMARY_TEXT_COLOR_BRUSH = WHITE_COLOR_BRUSH;
    }
}
