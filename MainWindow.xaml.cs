using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Music_Player.State;
using WPF_Music_Player.Widgets;

namespace WPF_Music_Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static readonly string PLUS_ICON_PATH = "/Assets/Icons/plus.png";

   
        Dictionary<TextBlock, MusicList> textMusicListDict = new Dictionary<TextBlock, MusicList>();
        public MainWindow()
        {



            //init holders and handlers
            StateHolder.Init();

            InitializeComponent();

            ((Grid)this.Content).Background = General.Constants.Colors.SECONDARY_COLOR_BRUSH;

            //init libary list
            IconButton plusIconButton = new IconButton(PLUS_ICON_PATH);
            plusIconButton.Width = 60;
            plusIconButton.Click += AddMusicLibrary;
            libraryList.Children.Add(plusIconButton);

            //load music list
            LoadMusicLibaries();


        }

        private static readonly string LIBARIES_FILE = "libraries.player";

        private void LoadMusicLibaries()
        {
            if (!File.Exists(LIBARIES_FILE))
            {
                //create libaries file
                File.Create(LIBARIES_FILE);
            }
            else
            {
                //read in music libarires
                string[] libraries = File.ReadAllLines(LIBARIES_FILE);
                if(libraries.Length > 0)
                {

              

                    //add list button
                    TextBlock listButton = new TextBlock();
                    listButton.Text= new DirectoryInfo(libraries[0]).Name;
                    textMusicListDict.Add(listButton, musicList);
                    listButton.MouseEnter += (sender, e) => { ((TextBlock)sender).Background = General.Constants.Colors.PRIMARY_HIGHLIGHT_COLOR_BRUSH; };
                    listButton.MouseLeave += (sender, e) => { ((TextBlock)sender).Background = General.Constants.Colors.TRANSPARENT_COLOR_BRUSH; };
                    listButton.Foreground = General.Constants.Colors.PRIMARY_TEXT_COLOR_BRUSH;
                    listButton.TextWrapping = TextWrapping.WrapWithOverflow;
                    libraryList.Children.Add(listButton);
                    //onclick switch current view to approriate music list
                    listButton.MouseDown += (sender, e) =>
                    {
                        this.mainViewStack.Children.RemoveAt(1);
                        MusicList ls = textMusicListDict[listButton];
                        Grid.SetColumn(ls, 1);
                        this.mainViewStack.Children.Add(ls);
                    };


                    //get all file in directory
                    List<string> songs = walkForMusicFiles(libraries[0]);
                    foreach(string song in songs)
                    {
                        musicList.AddSong(song);
                    }

                }

                for(int i = 1; i<libraries.Length; i++)
                {

                    AddMusicLibaryToWindow(libraries[i]);

                   
                }


            }


        }

        private void AddMusicLibaryToWindow(string path)
        {
            //add directory to music libaries

            MusicList ls = new MusicList();
           
            //get all file in directory
            List<string> songs = walkForMusicFiles(path);
            foreach (string song in songs)
            {
                ls.AddSong(song);
            }

            //add list button
            TextBlock listButton = new TextBlock();
            listButton.Text = new DirectoryInfo(path).Name;
            textMusicListDict.Add(listButton, ls);
            listButton.MouseEnter += (sender, e) => { ((TextBlock)sender).Background = General.Constants.Colors.PRIMARY_HIGHLIGHT_COLOR_BRUSH; };
            listButton.MouseLeave += (sender, e) => { ((TextBlock)sender).Background = General.Constants.Colors.TRANSPARENT_COLOR_BRUSH; };
            listButton.Foreground = General.Constants.Colors.PRIMARY_TEXT_COLOR_BRUSH;
            listButton.TextWrapping = TextWrapping.WrapWithOverflow;
            libraryList.Children.Add(listButton);

            //onclick switch current view to approriate music list
            listButton.MouseDown += (sender, e) =>
            {
                this.mainViewStack.Children.RemoveAt(1);
                MusicList ls = textMusicListDict[listButton];
                Grid.SetColumn(ls, 1);
                this.mainViewStack.Children.Add(ls);
            };

            //add to libraries file
            //read in music libarires
            List<string> libraries = File.ReadAllLines(LIBARIES_FILE).ToList();
            if (!libraries.Contains(path))
            {
                libraries.Add(path);
                File.WriteAllLines(LIBARIES_FILE, libraries);
            }

        }

        /// <summary>
        /// walks the given directory for all music files
        /// </summary>
        /// <param name="rootDir"></param>
        /// <returns></returns>
        private List<string> walkForMusicFiles(string rootDir)
        {
            string[] files = Directory.GetFiles(rootDir);
            string[] folders = Directory.GetDirectories(rootDir);

            List<string> returnFiles = new List<string>();
            foreach(string file in files)
            {
                if(file.EndsWith(".m4a") || file.EndsWith(".mp3") || file.EndsWith(".wav"))
                {
                    returnFiles.Add(file);
                }
            }

            foreach(string folder in folders)
            {
                List<string> subFolderResults = walkForMusicFiles(folder);
                returnFiles.AddRange(subFolderResults);
            }


            return returnFiles;
        }

        /// <summary>
        /// prompts the user to add a new music libary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMusicLibrary(Object sender, EventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if(result == System.Windows.Forms.DialogResult.OK && Directory.Exists(dialog.SelectedPath))
                {
                    MusicList newMusicList = new MusicList();
                    AddMusicLibaryToWindow(dialog.SelectedPath);


                }
            }
        }
    }
}
