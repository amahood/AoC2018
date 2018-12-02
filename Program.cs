using System;

namespace AoC2018
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to Advent of Code 2017! Please pass in which Day you would like to evaluate as a command line argument!");
            Console.WriteLine();

            int dayChallenge = Int32.Parse(args[0]);
            if (dayChallenge <1 || dayChallenge > 25)
            {
                Console.WriteLine("Please pass in a valid day challenge between 1 and 25 :(");
                Console.WriteLine();
                return;
            }

            switch (dayChallenge)
            {
                case 1:
                    Day1.TestDay1();
                    break;
                default:
                    break; 
            }

            return;
        }
    }
}
