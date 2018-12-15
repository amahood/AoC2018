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
                case 2:
                    Day2.TestDay2();
                    break;
                case 3:
                    Day3.TestDay3();
                    break;
                case 4:
                    Day4.TestDay4();
                    break;
                    case 5:
                    Day5.TestDay5();
                    break;
                    case 6:
                    Day6.TestDay6();
                    break;
                    case 7:
                    Day7.TestDay7();
                    break;
                    case 8:
                    Day8.TestDay8();
                    break;
                default:
                    break; 
            }

            return;
        }
    }
}
