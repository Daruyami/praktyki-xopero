using System;

namespace Guess99Test
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Random rng = new Random();
            Console.Out.WriteLine("Guess99! Guess a number between 1 and 99!");
            while (true)
            {
                int number = rng.Next(1, 100);
                int tries = 0;
                bool guessed = false;
                string input;
                while (!guessed)
                {
                    tries++;
                    input = Console.ReadLine();
                    Console.Out.WriteLine("You guessed: " + input);
                    int userNum;
                    try
                    {
                        userNum = int.Parse(input ?? string.Empty);
                    }
                    catch (FormatException)
                    {
                        Console.Out.WriteLine("Wrong type of input, try again!");
                        continue;
                    }

                    if (userNum == number)
                    {
                        Console.Out.WriteLine("You guessed the right number!");
                        guessed = true;
                        Console.Out.WriteLine("Number of tries: "+tries);
                    }
                    else if (userNum < number)
                    {
                        Console.Out.WriteLine("The number you guessed is too small!");
                    }
                    else if (userNum > number)
                    {
                        Console.Out.WriteLine("The number you guessed is too big!");
                    }
                }

                Console.Out.WriteLine("Do you want to continue? y/n");
                input = Console.ReadLine();
                if ((input ?? "n").Equals("n"))
                {
                    break;
                }
                Console.Clear();
            }
            Console.Out.WriteLine("bye bye!");
        }
    }
}