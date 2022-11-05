using System.Collections.Generic;
using System.Linq;

namespace Assignment5NatalieMueller
{
    internal static class HighScore
    {
        /// <summary>
        /// The games with high scores.
        /// </summary>
        private static List<Game> games;

        /// <summary>
        /// The lowest high score.
        /// </summary>
        private static int lowScore;

        /// <summary>
        /// The highest time of the lowest high score.
        /// </summary>
        private static int highTime;

        /// <summary>
        /// Initializes high scores.
        /// </summary>
        static HighScore()
        {
            games = new List<Game>();
            lowScore = 0;
            highTime = 0;
        }

        /// <summary>
        /// The games with high scores.
        /// </summary>
        public static List<Game> Games { get { return games; } }

        /// <summary>
        /// Adds the game to the high scores if it's in the top 10 scores, and says whether it's the highest top score.
        /// </summary>
        /// <param name="game">The game to add to high scores.</param>
        /// <returns>True if the game is the new high score.</returns>
        public static bool Add(Game game)
        {
            if (games.Count < 10 || game.Score > lowScore || (game.Score == lowScore && game.Time <= highTime))
            {
                games.Add(game);
                Sort();
                lowScore = games.Last().Score;
                highTime = games.Last().Time;
                return Score(game) == 0;
            }

            return false;
        }

        /// <summary>
        /// Gets the position of the given game.
        /// </summary>
        /// <param name="game">The game to find the position of.</param>
        /// <returns>The position of the game.</returns>
        private static int Score(Game game)
        {
            return games.IndexOf(game);
        }

        /// <summary>
        /// Sorts the high scores and trims the game list to 10.
        /// </summary>
        private static void Sort()
        {
            games.Sort((a, b) => a.Time - b.Time);
            games.Sort((a, b) => b.Score - a.Score);
            if (games.Count > 10)
            {
                games.RemoveAt(10);
            }
        }
    }
}
