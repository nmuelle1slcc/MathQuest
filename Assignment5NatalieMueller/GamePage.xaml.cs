using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Assignment5NatalieMueller
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
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
        /// Tracks the elapsed game time.
        /// </summary>
        private int Time;

        /// <summary>
        /// Keeps track of the current score heart.
        /// </summary>
        private int HeartIndex;

        /// <summary>
        /// Holds the score heart images.
        /// </summary>
        private Image[] Hearts;

        /// <summary>
        /// Creates a new game page.
        /// </summary>
        public GamePage()
        {
            InitializeComponent();

            MusicPlayer = new MediaPlayer();
            SoundPlayer = new MediaPlayer();

            StartMusic();

            PlayerName.Content = App.Game.Player.Name;
            PlayerAge.Content = $"{App.Game.Player.Age} Years Old";
            HeroImage.Source = App.Game.GetHeroImage();

            HeartIndex = 0;
            Hearts = new Image[10]
            {
                heart0, heart1, heart2, heart3, heart4, heart5, heart6, heart7, heart8, heart9
            };

            QuestionLabel.Content = App.Game.GetQuestion();

            InitializeTimer();
        }

        /// <summary>
        /// Initializes a timer that tracks how long the player takes to finish the game.
        /// </summary>
        private void InitializeTimer()
        {
            Time = 0;
            DispatcherTimer dispatcherTimer;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Shows the time elapsed in minutes and seconds (00:00).
        /// </summary>
        private void Tick(object sender, EventArgs e)
        {
            Time++;

            //format the time elapsed in minutes and seconds 00:00
            Timer.Content = $"{(Time / 60):D2}:{(Time % 60):D2}";
        }

        /// <summary>
        /// Answers the current question. If the question answered was the final question, proceeds to the score page.
        /// </summary>
        private void AnswerQuestion(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(Answer.Text, out int AnswerInt)) //the answer is not a number
            {
                SetAnswerErrors();
                return;
            }
            else //the answer is a number
            {
                try
                {
                    if (App.Game.AnswerQuestion(AnswerInt)) //the question was answered correctly
                    {
                        SoundPlayer.Open(new Uri("Sounds/correct.wav", UriKind.RelativeOrAbsolute));
                        SoundPlayer.Play();

                        Hearts[HeartIndex++].Source = new BitmapImage(new Uri("Images/fullheart.png", UriKind.Relative));
                    }
                    else //the question was answered incorrectly
                    {
                        SoundPlayer.Open(new Uri("Sounds/incorrect.wav", UriKind.RelativeOrAbsolute));
                        SoundPlayer.Play();
                    }
                }
                catch (ArgumentOutOfRangeException) //the answer is outside the accepted range
                {
                    SetAnswerErrors();
                    return;
                }

                //check to see if the game is over
                IsGameOver();
            }
        }

        /// <summary>
        /// Checks to see if the game is over, and if so shows the score board.
        /// </summary>
        private void IsGameOver()
        {
            if (!App.Game.IsOver()) //the game is not over
            {
                QuestionLabel.Content = App.Game.GetQuestion();
                Answer.Text = "";
                Answer.Focus();
            }
            else //the game is over
            {
                App.Game.Time = Time;

                MusicPlayer.Stop();

                MainWindow window = (MainWindow)Window.GetWindow(this);
                window.ShowScorePage(HighScore.Add(App.Game));
            }
        }

        /// <summary>
        /// Checks to see if return was pressed, and if so answers the question.
        /// </summary>
        private void IsReturnPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                AnswerQuestion(sender, e);
            }
        }

        /// <summary>
        /// Quits the current game and returns to the previous menu page.
        /// </summary>
        private void QuitGame(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Open(new Uri("Sounds/quit.wav", UriKind.RelativeOrAbsolute));
            SoundPlayer.Play();

            MusicPlayer.Stop();

            MainWindow window = (MainWindow)Window.GetWindow(this);
            window.ShowPreviousMenuPage();
        }

        /// <summary>
        /// Sets answer error validation and plays an error sound.
        /// </summary>
        private void SetAnswerErrors()
        {
            SoundPlayer.Open(new Uri("Sounds/error.wav", UriKind.RelativeOrAbsolute));
            SoundPlayer.Play();

            Answer.Text = "";
            AnswerError.Content = "Enter a number between 1 and 100.";
            AnswerError.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Clears answer validation errors.
        /// </summary>
        private void ClearAnswerErrors()
        {
            AnswerError.Content = "";
            AnswerError.Visibility = Visibility.Hidden;
            Answer.Focus();
        }
        
        /// <summary>
        /// Clears answer validation errors.
        /// </summary>
        private void ClearAnswerErrors(object sender, KeyEventArgs e)
        {
            ClearAnswerErrors();
        }

        /// <summary>
        /// Clears answer validation errors.
        /// </summary>
        private void ClearAnswerErrors(object sender, MouseButtonEventArgs e)
        {
            ClearAnswerErrors();
        }

        /// <summary>
        /// Sets the background music and starts it.
        /// </summary>
        private void StartMusic()
        {
            MusicPlayer.Open(new Uri("Sounds/2.wav", UriKind.RelativeOrAbsolute));
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
