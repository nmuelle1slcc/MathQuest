using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Assignment5NatalieMueller
{
    /// <summary>
    /// Interaction logic for HighScorePage.xaml
    /// </summary>
    public partial class HighScorePage : Page
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
        /// Shows the 10 games with the highest scores.
        /// </summary>
        public HighScorePage()
        {
            InitializeComponent();

            MusicPlayer = new MediaPlayer();
            SoundPlayer = new MediaPlayer();

            StartMusic();

            int row = 1;
            HighScore.Games.ForEach(game =>
            {
                AddHighScore(row++, game);
            });
        }

        /// <summary>
        /// Adds a game to the high score list.
        /// </summary>
        /// <param name="row">The grid row to add the game to.</param>
        /// <param name="game">The game to add to the high score list.</param>
        private void AddHighScore(int row, Game game)
        {
            int col = 0;

            AddToGrid(new Viewbox() { Child = new Image() { Source = game.GetHeroIcon() }, HorizontalAlignment = HorizontalAlignment.Center }, row, col++);
            AddToGrid(new Viewbox() { Child = new Label() { Content = game.Player.Name } }, row, col++);
            AddToGrid(new Viewbox() { Child = new Label() { Content = game.Player.Age } }, row, col++);
            AddToGrid(new Viewbox() { Child = new Label() { Content = game.GameType } }, row, col++);

            StackPanel heartPanel = new StackPanel { Orientation = Orientation.Horizontal };
            for (int i = 0; i < 10; i++)
            {
                Image heart = new()
                {
                    Source = i < game.Score 
                        ? new BitmapImage(new Uri("Images/fullheart.png", UriKind.Relative)) 
                        : new BitmapImage(new Uri("Images/emptyheart.png", UriKind.Relative))
                };
                heartPanel.Children.Add(heart);
            }

            AddToGrid(new Viewbox { Child = heartPanel }, row, col++);
            AddToGrid(new Viewbox() { Child = new Label() { Content = $"{(game.Time / 60):D2}:{(game.Time % 60):D2}" } }, row, col++);
        }

        /// <summary>
        /// Adds the specified object to the grid at the given row and column.
        /// </summary>
        /// <param name="obj">The object to add to the grid.</param>
        /// <param name="row">The grid row to add the object to.</param>
        /// <param name="col">The grid column to add the object to.</param>
        private void AddToGrid(FrameworkElement obj, int row, int col)
        {
            Grid.SetRow(obj, row);
            Grid.SetColumn(obj, col);
            ScoresGrid.Children.Add(obj);
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
        /// Sets the background music and starts it.
        /// </summary>
        private void StartMusic()
        {
            MusicPlayer.Open(new Uri("Sounds/4.wav", UriKind.RelativeOrAbsolute));
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
