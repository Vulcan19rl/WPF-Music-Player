using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_Music_Player.Widgets
{
    /// <summary>
    /// icons button is an image button that contains an icon
    /// </summary>
    public partial class IconButton : Button
    {
        Image iconImage;

        public IconButton(string src) : base()
        {
            //init colors
            this.Background = new SolidColorBrush(Colors.Transparent);
            this.BorderThickness = new System.Windows.Thickness(0);

            //create image
            iconImage = new Image();
            ChangeImage(src);

            this.Content = iconImage;



        }

        public IconButton() : base()
        {
            //init colors
            this.Background = new SolidColorBrush(Colors.Transparent);
            this.BorderThickness = new System.Windows.Thickness(0);

            //create image
            iconImage = new Image();
            this.Content = iconImage;
        }


        /// <summary>
        /// changes the image in the icon button
        /// </summary>
        /// <param name="path"></param>
        public void ChangeImage(string path)
        {
            BitmapImage imageSource = new BitmapImage();
            imageSource.BeginInit();

            imageSource.UriSource = new Uri(path, UriKind.Relative);
            imageSource.EndInit();

            this.iconImage.Source = imageSource;
        }
        
    }
}
