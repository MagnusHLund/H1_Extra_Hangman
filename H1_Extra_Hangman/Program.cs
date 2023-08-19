using Microsoft.VisualBasic.FileIO;

namespace H1_Extra_Hangman
{
    internal class Program
    {
        static string WordCache;

        static void Main()
        {
            Controller();
        }

        static void Controller()
        {
            // Gets the word file
            string directory = WordListModel();

            // Reads each line from the word file and creates a random variable
            string[] lines = File.ReadAllLines(directory);
            Random random = new Random();

            // Runs an infinite loop, for repeatablity
            while (true)
            {
                // Picks a random word from the word list
                string word = lines[random.Next(lines.Length)];
                char[] charWord = word.ToCharArray();

                WordCache = string.Concat(Enumerable.Repeat("_ ", word.Length));

                // Keeps track of when the game is over
                int health = 0;
                bool declineHealth = false;
                

                // Calls the View method before user input, to display the hang man before input has been given
                View(health, word.Length, charWord, "".ToCharArray());

                // Creates an infinite loop for hangman gameplay
                while (true)
                {
                    declineHealth = false;
                    char[] input = Console.ReadLine().ToCharArray();

                    if (input.Length != 1)
                    {
                        Console.WriteLine("Max 1 character!");
                        continue;
                    }

                    //Console.Clear();
                    View(health, word.Length, charWord, input);

                    foreach (char c in input)
                    {
                        if (!charWord.Contains(c))
                        {
                            declineHealth = true;

                        }
                    }

                    if(declineHealth)
                    {
                        health++;
                        declineHealth = false;
                    }

                    if (health == 6)
                    {
                        break;
                    }
                }
            }
        }

        static string Underscores(int length, char[] word, char[] input)
        {

            for (int i = 0; i < word.Length; i++)
            {
                if (input.Contains(word[i]))
                {
                    Console.WriteLine("yes");
                }
            }



            return WordCache;
        }

        /// <summary>
        /// Creates some hang man ascii art, based on the health parameter
        /// </summary>
        /// <param name="health"></param>
        static void View(int health, int length, char[] word, char[] input)
        {
            if (health == 0)
            {
                Console.WriteLine("   +---+");
                Console.WriteLine("   |   |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("============");
                Console.WriteLine(Underscores(length, word, input));
                Console.WriteLine("============");
            }
            else if (health == 1)
            {
                Console.WriteLine("   +---+");
                Console.WriteLine("   |   |");
                Console.WriteLine("   O   |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("============");
                Console.WriteLine(Underscores(length, word, input));
                Console.WriteLine("============");
            }
            else if (health == 2)
            {
                Console.WriteLine("   +---+");
                Console.WriteLine("   |   |");
                Console.WriteLine("   O   |");
                Console.WriteLine("   |   |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("============");
                Console.WriteLine(Underscores(length, word, input));
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
                Console.WriteLine(Underscores(length, word, input));
                Console.WriteLine("============");
            }
            else if (health == 4)
            {
                Console.WriteLine("   +---+");
                Console.WriteLine("   |   |");
                Console.WriteLine("   O   |");
                Console.WriteLine(@"  /|\  |");
                Console.WriteLine("       |");
                Console.WriteLine("       |");
                Console.WriteLine("============");
                Console.WriteLine(Underscores(length, word, input));
                Console.WriteLine("============");

            }
            else if (health == 5)
            {
                Console.WriteLine("   +---+");
                Console.WriteLine("   |   |");
                Console.WriteLine("   O   |");
                Console.WriteLine(@"  /|\  |");
                Console.WriteLine("  /    |");
                Console.WriteLine("       |");
                Console.WriteLine("============");
                Console.WriteLine(Underscores(length, word, input));
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
                Console.WriteLine(Underscores(length, word, input));
                Console.WriteLine("============");
                Console.WriteLine("  YOU LOST\nPress enter to replay!");
            }

            Console.ResetColor();
            Thread.Sleep(1000);
        }

        static string WordListModel()
        {
            // Points to the word list, located in ..\H1_Extra_Hangman\H1_Extra_Hangman\bin\Debug\net6.0\WordList.txt
            string directory = Directory.GetCurrentDirectory();
            directory = Path.Combine(directory + @"\WordList.txt");
            return directory;
        }
    }
}