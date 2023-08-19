using Microsoft.VisualBasic.FileIO;
using System.Runtime.CompilerServices;

namespace H1_Extra_Hangman
{
    internal class Program
    {
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


                // Calls the View method before user input, to display the hang man before input has been given
                View(health, charWord, "");

                string input = Console.ReadLine();

                // Creates an infinite loop for hangman gameplay
                while (true)
                {

                    Console.Clear();
                    if(input != null)
                    {
                    View(health, charWord, input);
                    } else
                    {
                        View(health, charWord, "");
                    }

                    if (wordCache == word)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Messages("You won!\nPress enter to try again!");
                        break;
                    }

                    input = Console.ReadLine();

                    if (input.Length != 1)
                    {
                        Messages("Max 1 character!");
                        continue;
                    }
                    else if (alreadySaid.Contains(input[0]))
                    {
                        Messages("You already said this character!");
                        continue;
                    }

                        alreadySaid = string.Join(" ", input);

                    // health declines, if charWord does not contain the user input character
                    if (!charWord.Contains(input[0]))
                    {
                        health--;
                    }

                    // if health is 0, then game is over and repeats once the player presses enter
                    if (health == 0)
                    {
                        Messages("Game over!\nPress enter to try again!");
                        break;
                    }
                }
            }
        }

        static string Underscores(char[] word, string input)
        {
            char[] wordCacheChar = wordCache.ToCharArray();

            for (int i = 0; i < word.Length; i++)
            {
                if (input.Contains(word[i]))
                {
                    wordCacheChar[i] = word[i];
                }
            }

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
            Console.WriteLine(message);
            Console.ReadLine();
        }

        #endregion

        #region Models

        static string WordListModel()
        {
            // Points to the word list, located in ..\H1_Extra_Hangman\H1_Extra_Hangman\bin\Debug\net6.0\WordList.txt
            string directory = Directory.GetCurrentDirectory();
            directory = Path.Combine(directory + @"\WordList.txt");
            return directory;
        }

        #endregion
    }
}