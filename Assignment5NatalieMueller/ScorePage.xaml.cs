using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Assignment5NatalieMueller
{
    /// <summary>
    /// Interaction logic for ScorePage.xaml
    /// </summary>
    public partial class ScorePage : Page
    {
        /// <summary>
        /// Plays the background music.
        /// </summary>
        private MediaPlayer MusicPlayer;

        /// <summary>
        /// Plays sound effects.
        /// </summary>
        private MediaPlayer SoundPlayer;

        /// <summary>
        /// Shows the player's score and other game stats.
        /// </summary>
        public ScorePage(bool IsHighScore)
        {
            InitializeComponent();

            MusicPlayer = new MediaPlayer();
            SoundPlayer = new MediaPlayer();

            StartMusic();

            if (IsHighScore)
            {
                ScoreBorder.Style = (Style)FindResource("OrangeBorder");
                ScoreLabel.Style = (Style)FindResource("OrangeLabel");
                ScoreLabel.Content = "Congratulations! New High Score!";
            }

            PlayerName.Content = App.Game.Player.Name;
            PlayerAge.Content = App.Game.Player.Age;
            GameType.Content = App.Game.GameType;
            HeroImage.Source = App.Game.GetHeroImage();
            Time.Content = $"{(App.Game.Time / 60):D2}:{(App.Game.Time % 60):D2}";

            Image[] Hearts = new Image[10]
            {
                heart0, heart1, heart2, heart3, heart4, heart5, heart6, heart7, heart8, heart9
            };

            for (int i = 0; i < App.Game.Score; i++)
            {
                Hearts[i].Source = new BitmapImage(new Uri("/Images/fullheart.png", UriKind.Relative));
            }
        }

        /// <summary>
        /// Returns to the menu screen.
        /// </summary>
        private void NewGame(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Open(new Uri("Sounds/start.wav", UriKind.RelativeOrAbsolute));
            SoundPlayer.Play();

            MusicPlayer.Stop();

            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.ShowMenuPage();
        }

        /// <summary>
        /// Shows the high score page.
        /// </summary>
        private void ShowScores(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Open(new Uri("Sounds/scores.wav", UriKind.RelativeOrAbsolute));
            SoundPlayer.Play();

            MusicPlayer.Stop();

            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.ShowHighScorePage();
        }

        /// <summary>
        /// Sets the background music and starts it.
        /// </summary>
        private void StartMusic()
        {
            MusicPlayer.Open(new Uri("Sounds/3.wav", UriKind.RelativeOrAbsolute));
            MusicPlayer.MediaEnded += new EventHandler(RestartMusic);
            MusicPlayer.Volume = 0.3;
            MusicPlayer.Play();
        }

        /// <summary>
        /// Restarts the background music when it ends.
        /// </summary>
        private void RestartMusic(object sender, EventArgs e)
        {
            MusicPlayer.Position = TimeSpan.Zero;
            MusicPlayer.Play();
        }
    }
}
