using System.Windows;

namespace Assignment5NatalieMueller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// MenuPage object to keep track of the player's information if they quit a game.
        /// </summary>
        private MenuPage Menu;

        /// <summary>
        /// Shows the menu page.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Menu = new MenuPage();
            Menu.StartMusic();
            MainFrame.Navigate(Menu);
        }

        /// <summary>
        /// Shows a new menu page.
        /// </summary>
        public void ShowMenuPage()
        {
            Menu = new MenuPage();
            Menu.StartMusic();
            MainFrame.Navigate(Menu);
        }

        /// <summary>
        /// Shows the game page.
        /// </summary>
        public void ShowGamePage()
        {
            MainFrame.Navigate(new GamePage());
        }

        /// <summary>
        /// Quits the game and returns to the previous menu page.
        /// </summary>
        public void ShowPreviousMenuPage()
        {
            Menu.StartMusic();
            MainFrame.Navigate(Menu);
        }

        /// <summary>
        /// Shows the score page.
        /// </summary>
        /// <param name="IsHighScore">If the finished game is the high score.</param>
        public void ShowScorePage(bool IsHighScore)
        {
            MainFrame.Navigate(new ScorePage(IsHighScore));
        }

        /// <summary>
        /// Shows the high scores page.
        /// </summary>
        public void ShowHighScorePage()
        {
            MainFrame.Navigate(new HighScorePage());
        }
    }
}
