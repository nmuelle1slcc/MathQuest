using System;
using System.Windows.Media.Imaging;

namespace Assignment5NatalieMueller
{
    internal class Game
    {
        /// <summary>
        /// The player of the current game.
        /// </summary>
        private readonly Player player;

        /// <summary>
        /// The current game's type.
        /// </summary>
        private readonly string gameType;

        /// <summary>
        /// The player's selected hero for the current game.
        /// </summary>
        private readonly string hero;

        /// <summary>
        /// The game questions.
        /// </summary>
        private readonly Question[] questions;

        /// <summary>
        /// The current question.
        /// </summary>
        private int currentQuestion;

        /// <summary>
        /// The player's score for the current game.
        /// </summary>
        private int score;

        /// <summary>
        /// The game's time elapsed.
        /// </summary>
        private int time;

        /// <summary>
        /// Creates an empty game.
        /// </summary>
        public Game()
        {
            player = new Player();
            gameType = "";
            hero = "";
            currentQuestion = 0;
            questions = Array.Empty<Question>();
            score = 0;
            time = 0;
        }

        /// <summary>
        /// Creates a game.
        /// </summary>
        /// <param name="PlayerName">The player's name.</param>
        /// <param name="PlayerAge">The player's age.</param>
        /// <param name="GameType">The game type.</param>
        /// <param name="Hero">The player's selected hero.</param>
        public Game(string PlayerName, int PlayerAge, string GameType, string Hero)
        {
            try
            {
                player = new Player(PlayerName, PlayerAge);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }

            gameType = string.IsNullOrEmpty(GameType) ? "Addition" : GameType;
            hero = string.IsNullOrEmpty(Hero) ? "Fighter" : Hero;

            questions = new Question[10];
            for (int i = 0; i < 10; i++)
            {
                questions[i] = new Question(GameType);
            }
            currentQuestion = 0;

            score = 0;
            time = 0;
        }

        /// <summary>
        /// The player of the current game.
        /// </summary>
        public Player Player { get { return player; } }

        /// <summary>
        /// The type of game (addition, subtraction, multiplication, or division.)
        /// </summary>
        public string GameType { get { return gameType; } }

        /// <summary>
        /// The selected hero.
        /// </summary>
        public string Hero { get { return hero; } }

        /// <summary>
        /// The game score.
        /// </summary>
        public int Score { get { return score; } set { score = value; } }

        /// <summary>
        /// The duration of the game.
        /// </summary>
        public int Time { get { return time; } set { time = value; } }

        /// <summary>
        /// Returns whether the game is over or ongoing.
        /// </summary>
        /// <returns>True if the game is over, false if it's ongoing.</returns>
        public bool IsOver()
        {
            return currentQuestion == 10;
        }

        /// <summary>
        /// Shows the current question in a user-friendly format.
        /// </summary>
        /// <returns></returns>
        public string GetQuestion()
        {
            return $"{GetQuestionNumber()}: What is {questions[currentQuestion].Num1} {GetOperator(gameType)} {questions[currentQuestion].Num2}?";
        }

        /// <summary>
        /// Shows the current question number in a user-friendly format.
        /// </summary>
        /// <returns></returns>
        private string GetQuestionNumber()
        {
            if (currentQuestion < 9)
            {
                return $"Question {currentQuestion + 1}";
            }
            else
            {
                return "Final Question";
            }
        }

        /// <summary>
        /// Answers the current question and returns whether the answer was correct.
        /// </summary>
        /// <param name="answer">The player's answer to the current question.</param>
        /// <returns>Whether the given answer was correct or not.</returns>
        public bool AnswerQuestion(int answer)
        {
            questions[currentQuestion].PlayerAnswer = answer;
            if (questions[currentQuestion++].IsCorrect)
            {
                score++;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the operation format of the specified math operator.
        /// </summary>
        /// <param name="type">The math operator in { Addition, Subtraction, Multiplication, Division }.</param>
        /// <returns>A string in { +, - ×, ÷ }.</returns>
        private static string GetOperator(string type)
        {
            return type switch
            {
                "Addition" => "+",
                "Subtraction" => "-",
                "Multiplication" => "×",
                "Division" => "÷",
                _ => "?",
            };
        }

        /// <summary>
        /// Gets the image associated with the hero.
        /// </summary>
        /// <returns></returns>
        public BitmapImage GetHeroImage()
        {
            return hero switch
            {
                "Fighter" => new BitmapImage(new Uri("/Images/fighterfull.png", UriKind.Relative)),
                "BlackMage" => new BitmapImage(new Uri("/Images/blackmagefull.png", UriKind.Relative)),
                "WhiteMage" => new BitmapImage(new Uri("/Images/whitemagefull.png", UriKind.Relative)),
                "RedMage" => new BitmapImage(new Uri("/Images/redmagefull.png", UriKind.Relative)),
                _ => new BitmapImage(new Uri("/Images/fighterfull.png", UriKind.Relative)),
            };
        }

        /// <summary>
        /// Gets the icon associated with the hero.
        /// </summary>
        /// <returns></returns>
        public BitmapImage GetHeroIcon()
        {
            return hero switch
            {
                "Fighter" => new BitmapImage(new Uri("/Images/fighter.png", UriKind.Relative)),
                "BlackMage" => new BitmapImage(new Uri("/Images/blackmage.png", UriKind.Relative)),
                "WhiteMage" => new BitmapImage(new Uri("/Images/whitemage.png", UriKind.Relative)),
                "RedMage" => new BitmapImage(new Uri("/Images/redmage.png", UriKind.Relative)),
                _ => new BitmapImage(new Uri("/Images/fighter.png", UriKind.Relative)),
            };
        }
    }
}
