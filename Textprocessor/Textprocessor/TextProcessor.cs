using System.Text.RegularExpressions;

namespace TextProcessor
{
    public class TextProcessor
    {
        public static void Main(string[] args)
        {
            string input = InputPrompt();
            Process(input);
        }

        public static void Process(string inputText)
        {
            bool isRunning = true;
            WordCounter wc = new();

            while (isRunning)
            {
                Console.WriteLine($"\nText: {inputText}");
                Console.WriteLine("\nPlease input action: \n\n1 = Single word frequency \n2 = Highest word frequency \n3 = N most frequent words \n4 = Quit");
                string input = Console.ReadLine();
                bool valid = Int32.TryParse(input, out int choice);

                if (valid) 
                {
                    switch (choice)
                    {
                        case 1: //Return word count of input word
                            string word = InputPrompt();
                            Console.WriteLine("\nSingle word  : " + wc.CalculateWordCount(inputText, word));
                            break;
                        case 2: //Return highest word count
                            Console.WriteLine("\nHighest count: " + wc.CalculateHighestWordCount(inputText));
                            break;
                        case 3: //Return top N most counted words
                            Console.WriteLine("\nPlease enter a number:");
                            bool done = false;
                            while (!done)
                            {
                                input = Console.ReadLine();
                                if (Int32.TryParse(input, out int number))
                                {
                                    Console.WriteLine("\nMost frequent:\n");
                                    wc.PrintList(wc.GetMostCountedWords(inputText, number));
                                    done = true;
                                }
                                else
                                    Console.WriteLine("\nInvalid input");
                            }
                            break;
                        case 4: //Terminate program
                            isRunning = false;
                            break;
                        default: //In case invalid input is given
                            Console.WriteLine($"\n\"{choice} is not a valid command");
                            break;

                    }
                }
            }
        }

        /// <summary>
        /// Prompts the user to input something in the console.
        /// </summary>
        /// <returns>String representing the input by the user</returns>
        public static string InputPrompt()
        {
            string input = "";
            while (input == "")
            {
                Console.WriteLine("Please enter input:");
                input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Invalid input");
                    input = "";
                }
                else if (input.Any(char.IsDigit))
                {
                    Console.WriteLine("Invalid input");
                    input = "";
                }
            }
            return input;
        }
    }
}