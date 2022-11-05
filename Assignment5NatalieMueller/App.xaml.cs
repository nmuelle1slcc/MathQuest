using System.Windows;

namespace Assignment5NatalieMueller
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The math game object.
        /// </summary>
        internal static Game Game;

        /// <summary>
        /// Starts a new game.
        /// </summary>
        static App()
        {
            Game = new Game();
        }
    }
}
