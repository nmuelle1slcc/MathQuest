using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Assignment5NatalieMueller
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
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
        /// The selected game type.
        /// </summary>
        string SelectedGameType;

        /// <summary>
        /// The selected hero.
        /// </summary>
        string SelectedHero;

        /// <summary>
        /// Creates a new menu page.
        /// </summary>
        public MenuPage()
        {
            InitializeComponent();
            SelectedGameType = "";
            SelectedHero = "";

            FighterButton.IsChecked = true;
            AdditionButton.IsChecked = true;

            MusicPlayer = new MediaPlayer();
            SoundPlayer = new MediaPlayer();
        }

        /// <summary>
        /// Changes the selected game type.
        /// </summary>
        private void SelectGameType(object sender, RoutedEventArgs e)
        {
            SelectedGameType = ((RadioButton)sender).Uid;
        }

        /// <summary>
        /// Changes the selected hero.
        /// </summary>
        private void SelectHero(object sender, RoutedEventArgs e)
        {
            SelectedHero = ((RadioButton)sender).Uid;
            ((Image)((RadioButton)sender).Content).Opacity = 1;
        }

        /// <summary>
        /// Resets a hero image's opacity after another one has been selected.
        /// </summary>
        private void UnselectHero(object sender, RoutedEventArgs e)
        {
            ((Image)((RadioButton)sender).Content).Opacity = 0.5;
        }

        /// <summary>
        /// Starts a new game using the options the player selected.
        /// </summary>
        private void StartGame(object sender, RoutedEventArgs e)
        {
            if (ValidateGame())
            {
                SoundPlayer.Open(new Uri("Sounds/start.wav", UriKind.RelativeOrAbsolute));
                SoundPlayer.Play();

                MusicPlayer.Stop();

                MainWindow window = (MainWindow)Window.GetWindow(this);
                window.ShowGamePage();
            }
            else
            {
                SoundPlayer.Open(new Uri("Sounds/error.wav", UriKind.RelativeOrAbsolute));
                SoundPlayer.Play();
            }
        }

        /// <summary>
        /// Validate the player's name and age.
        /// </summary>
        /// <returns>Whether the player's name and age are valid.</returns>
        private bool ValidateGame()
        {
            bool valid = true;

            if (string.IsNullOrEmpty(PlayerName.Text))
            {
                valid = false;
                PlayerNameError.Content = "Please enter your name.";
                PlayerNameError.Visibility = Visibility.Visible;
            }

            if (string.IsNullOrEmpty(PlayerAge.Text))
            {
                valid = false;
                PlayerAgeError.Content = "Please enter your age.";
                PlayerAgeError.Visibility = Visibility.Visible;
            }

            if (int.TryParse(PlayerAge.Text, out int Age) && valid)
            {
                try
                {
                    App.Game = new Game(PlayerName.Text, Age, SelectedGameType, SelectedHero);
                }
                catch (ArgumentOutOfRangeException)
                {
                    valid = false;
                    PlayerAge.Text = "";
                    PlayerAgeError.Content = "Age must be 3 to 10.";
                    PlayerAgeError.Visibility = Visibility.Visible;
                }
            }

            return valid;
        }

        /// <summary>
        /// Checks to see if return was pressed, and if so starts the game.
        /// </summary>
        private void IsReturnPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                StartGame(sender, e);
            }
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
        /// Clear validation errors with the player's name.
        /// </summary>
        private void ClearNameErrors()
        {
            PlayerNameError.Content = "";
            PlayerNameError.Visibility = Visibility.Hidden;
            PlayerName.Focus();
        }

        /// <summary>
        /// Clear validation errors with the player's name.
        /// </summary>
        private void ClearNameErrors(object sender, RoutedEventArgs e)
        {
            ClearNameErrors();
        }

        /// <summary>
        /// Clear validation errors with the player's name.
        /// </summary>
        private void ClearNameErrors(object sender, KeyEventArgs e)
        {
            ClearNameErrors();
        }

        /// <summary>
        /// Clear validation errors with the player's age.
        /// </summary>
        private void ClearAgeErrors()
        {
            PlayerAgeError.Content = "";
            PlayerAgeError.Visibility = Visibility.Hidden;
            PlayerAge.Focus();
        }

        /// <summary>
        /// Clear validation errors with the player's age.
        /// </summary>
        private void ClearAgeErrors(object sender, RoutedEventArgs e)
        {
            ClearAgeErrors();
        }

        /// <summary>
        /// Clear validation errors with the player's age.
        /// </summary>
        private void ClearAgeErrors(object sender, KeyEventArgs e)
        {
            ClearAgeErrors();
        }

        /// <summary>
        /// Sets the background music and starts it.
        /// </summary>
        public void StartMusic()
        {
            MusicPlayer.Open(new Uri("Sounds/1.wav", UriKind.RelativeOrAbsolute));
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
