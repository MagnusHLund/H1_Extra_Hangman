namespace H1_Extra_Hangman
{
    internal class Program
    {
        // String that contains the characters already guessed
        static string wordCache;

        static void Main()
        {
            Controller();
        }

        #region Controllers

        static void Controller()
        {
            // Gets the word file
            string directory = WordListModel();

            // Reads each line from the word file and creates a random variable
            string[] lines = File.ReadAllLines(directory);
            Random random = new Random();

            string alreadySaid = "";

            // Runs an infinite loop, for repeatablity
            while (true)
            {
                // Picks a random word from the word list
                string word = lines[random.Next(lines.Length)];
                char[] charWord = word.ToCharArray();

                // Set the default value of wordCache to be underscores, same amount as the letters in the word string
                wordCache = string.Concat(Enumerable.Repeat("_", word.Length));

                // Keeps track of when the game is over
                int health = 6;

                string input = Console.ReadLine();

                // Creates an infinite loop for hangman gameplay
                while (true)
                {
                    // Clears the console
                    Console.Clear();

                    // Calls the view which shows the hangman
                    View(health, charWord, input);

                    // If the word is the same as the wordCache then the word has been guessed and game is won
                    if (wordCache == word)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Messages("You won!\nPress enter to try again!");
                        break;
                    }

                    // Input becomes user input
                    input = Console.ReadLine();

                    // Checks if the input is 1, if its not then output an error. If yes, output an error and repeat the loop
                    if (input.Length != 1)
                    {
                        Messages("Max 1 character!");

                        // Sets input to an empty string, so it wont call View with more than 1 character inside input
                        input = "";
                        continue;
                    }
                    // Checks if the string called "alreadySaid" has the character that the user just input. If yes, then output an error and start the loop over
                    else if (alreadySaid.Contains(input[0]))
                    {
                        Messages("You already said this character!");
                        continue;
                    }

                    // Puts the user input into the alreadySaid string
                    alreadySaid = string.Join(" ", input);

                    // health declines, if charWord does not contain the user input character
                    if (!charWord.Contains(input[0]))
                    {
                        health--;
                    }

                    // if health is 0, then game is over and repeats once the player presses enter
                    if (health == 0)
                    {
                        Messages($"Game over! word was {word}\nPress enter to try again!");
                        break;
                    }
                }
            }
        }
        
        /// <summary>
        /// This method determines the outputted underscores. 
        /// If a correct character has been guessed then the same position underscore gets replaced with the letter
        /// example if the word is book and o has been guessed:
        /// _oo_
        /// </summary>
        /// <param name="word"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        static string Underscores(char[] word, string input)
        {
            // Creates a char array which takes in the wordCache
            char[] wordCacheChar = wordCache.ToCharArray();

            // Runs a loop the same amount of times as the word length
            for (int i = 0; i < word.Length; i++)
            {
                // Checks if the input contains any of the characters in the word and then puts that letter into the wordChacheChar
                if (input.Contains(word[i]))
                {
                    wordCacheChar[i] = word[i];
                }
            }

            // wordChace string gets updated with the wordChacheChar value and then returns the wordCache
            wordCache = string.Join("", wordCacheChar);
            return wordCache;
        }

        #endregion

        #region Views

        /// <summary>
        /// Creates some hang man ascii art, based on the health parameter
        /// The View method also passes word and input to another method call, Underscores()
        /// </summary>
        /// <param name="health"></param>
        /// <param name="word"></param>
        /// <param name="input"></param>
        static void View(int health, char[] word, string input)
        {
            if (health == 6)
            {
                Console.WriteLine("   +---+");
                Console.WriteLine("   |   |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("============");
                Console.WriteLine(Underscores(word, input));
                Console.WriteLine("============");
            }
            else if (health == 5)
            {
                Console.WriteLine("   +---+");
                Console.WriteLine("   |   |");
                Console.WriteLine("   O   |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("============");
                Console.WriteLine(Underscores(word, input));
                Console.WriteLine("============");
            }
            else if (health == 4)
            {
                Console.WriteLine("   +---+");
                Console.WriteLine("   |   |");
                Console.WriteLine("   O   |");
                Console.WriteLine("   |   |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("============");
                Console.WriteLine(Underscores(word, input));
                Console.WriteLine("============");
            }
            else if (health == 3)
            {
                Console.WriteLine("   +---+");
                Console.WriteLine("   |   |");
                Console.WriteLine("   O   |");
                Console.WriteLine("  /|   |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("============");
                Console.WriteLine(Underscores(word, input));
                Console.WriteLine("============");
            }
            else if (health == 2)
            {
                Console.WriteLine("   +---+");
                Console.WriteLine("   |   |");
                Console.WriteLine("   O   |");
                Console.WriteLine(@"  /|\  |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("============");
                Console.WriteLine(Underscores(word, input));
                Console.WriteLine("============");

            }
            else if (health == 1)
            {
                Console.WriteLine("   +---+");
                Console.WriteLine("   |   |");
                Console.WriteLine("   O   |");
                Console.WriteLine(@"  /|\  |");
                Console.WriteLine("  /    |");
                Console.WriteLine("       |");
                Console.WriteLine("============");
                Console.WriteLine(Underscores(word, input));
                Console.WriteLine("============");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("   +---+");
                Console.WriteLine("   |   |");
                Console.WriteLine("   O   |");
                Console.WriteLine(@"  /|\  |");
                Console.WriteLine(@"  / \  |");
                Console.WriteLine("       |");
                Console.WriteLine("============");
                Console.WriteLine(Underscores(word, input));
                Console.WriteLine("============");
                Messages("  YOU LOST\nPress enter to replay!");
            }

            Console.ResetColor();
        }

        static void Messages(string message)
        {
            // Writes a message, which is determined, whenever this method gets called
            Console.WriteLine(message);
            Console.ReadLine();
        }

        #endregion

        #region Models

        static string WordListModel()
        {
            // Points to the word list, located in ..\H1_Extra_Hangman\H1_Extra_Hangman\bin\Debug\net6.0\WordList.txt
            // Or just in Code, on github
            string directory = Directory.GetCurrentDirectory();
            directory = Path.Combine(directory + @"\WordList.txt");
            return directory;
        }

        #endregion
    }
}