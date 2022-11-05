using System;

namespace Assignment5NatalieMueller
{
    /// <summary>
    /// Contains different parts of a math question and the player's answer.
    /// </summary>
    internal class Question
    {
        /// <summary>
        /// Random number generator.
        /// </summary>
        private static readonly Random random = new();

        /// <summary>
        /// The first number in a math question.
        /// </summary>
        private int num1;

        /// <summary>
        /// The second number in a math question.
        /// </summary>
        private int num2;

        /// <summary>
        /// The answer to a math question.
        /// </summary>
        private int answer;

        /// <summary>
        /// The player's answer to the math question.
        /// </summary>
        private int playerAnswer;

        /// <summary>
        /// Generates a random math question of the given Type and keeps track of the player's answer.
        /// </summary>
        /// <param name="Type">The Type of question to generate between addition, subtraction, multiplication, and division.</param>
        public Question(string Type)
        {
            switch (Type)
            {
                case "Addition":
                    GenerateAdditionQuestion();
                    break;
                case "Subtraction":
                    GenerateSubtractionQuestion();
                    break;
                case "Multiplication":
                    GenerateMultiplicationQuestion();
                    break;
                case "Division":
                    GenerateDivisionQuestion();
                    break;
            }
        }

        /// <summary>
        /// The first number in the math question.
        /// </summary>
        public int Num1
        {
            get { return num1; }
        }

        /// <summary>
        /// The second number in the math question.
        /// </summary>
        public int Num2
        {
            get { return num2; }
        }

        /// <summary>
        /// The correct answer to the math question.
        /// </summary>
        public int Answer
        {
            get { return answer; }
        }

        /// <summary>
        /// The player's answer to the math question.
        /// </summary>
        public int PlayerAnswer
        {
            get { return playerAnswer; }
            set 
            { 
                if (value < 1 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("Answer must be between 1 and 100.");
                }
                playerAnswer = value;
            }
        }

        /// <summary>
        /// Property for whether the question was answered correctly.
        /// </summary>
        public bool IsCorrect
        {
            get { return playerAnswer == answer; }
        }

        /// <summary>
        /// Generates two random numbers between 1-10, or 2-10 if 1 is excluded.
        /// </summary>
        /// <param name="excludeOne">Whether to exclude 1 from the generated numbers.</param>
        private void GenerateRandomNumbers(bool excludeOne)
        {
            num1 = excludeOne ? random.Next(9) + 2 : random.Next(10) + 1;
            num2 = excludeOne ? random.Next(9) + 2 : random.Next(10) + 1; ;
        }

        /// <summary>
        /// Generates a new addition question using numbers between 1 and 10.
        /// </summary>
        private void GenerateAdditionQuestion()
        {
            GenerateRandomNumbers(false);
            answer = num1 + num2;
        }

        /// <summary>
        /// Generates a new subtraction question that will not result in a negative number.
        /// </summary>
        private void GenerateSubtractionQuestion()
        {
            GenerateAdditionQuestion();
            (num1, answer) = (answer, num1);
        }

        /// <summary>
        /// Generates a new multiplication question using numbers between 1 and 10.
        /// </summary>
        private void GenerateMultiplicationQuestion()
        {
            GenerateRandomNumbers(true);
            answer = num1 * num2;
        }

        /// <summary>
        /// Generates a new division question that will not result in a fraction.
        /// </summary>
        private void GenerateDivisionQuestion()
        {
            GenerateMultiplicationQuestion();
            (num1, answer) = (answer, num1);
        }
    }
}
