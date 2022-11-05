using System;

namespace Assignment5NatalieMueller
{
    /// <summary>
    /// Holds the player's information.
    /// </summary>
    internal class Player
    {
        /// <summary>
        /// The player's Name.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The player's Age.
        /// </summary>
        private readonly int age;

        /// <summary>
        /// Creates a new empty player.
        /// </summary>
        public Player()
        {
            name = "";
            age = 0;
        }

        /// <summary>
        /// Creates a new player.
        /// </summary>
        /// <param Name="Name">The player's Name.</param>
        /// <param Name="Age">The player's Age between 3 and 10.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the player's Age is not between 3 and 10.</exception>
        public Player(string Name, int Age)
        {
            if (Age < 3 || Age > 10)
            {
                throw new ArgumentOutOfRangeException("Age must be between 3 and 10.");
            }
            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentOutOfRangeException("Name cannot be blank.");
            }

            name = Name;
            age = Age;
        }

        /// <summary>
        /// The player's Name.
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// The player's Age.
        /// </summary>
        public int Age { get { return age; } }
    }
}
