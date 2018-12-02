using System;
using System.IO;
using System.Collections.Generic;

namespace AoC2018
{
    public class Day1
    {
        public static void TestDay1()
        {
            Console.WriteLine("-------------------DAY 1-------------------");

            StreamReader sr = new StreamReader("Day1Input.txt");
            string line;
            
            int frequencyDrift = 0;
            HashSet<int> frequencyTracker = new HashSet<int>();
            bool repeatFound = false;
            frequencyTracker.Add(0);

            //Part 1 - For each line, determine if positive or negative and adjust running tally
            //Part 2 - Try HashSet usage for Part 2
            while (!repeatFound)
            {
                line = sr.ReadLine();
                if (line==null){
                    sr.DiscardBufferedData();
                    sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin); 
                    line = sr.ReadLine();
                }
                char sign = line[0];
                int numberToProcess = Int32.Parse(line.Substring(1));
                switch (sign)
                {
                    case '+':
                        frequencyDrift += numberToProcess;
                        if (frequencyTracker.Contains(frequencyDrift))
                        {
                            repeatFound = true;
                        }
                        else
                        {
                            frequencyTracker.Add(frequencyDrift);
                        }
                        break;
                    case '-':
                        frequencyDrift -= numberToProcess;
                        if (frequencyTracker.Contains(frequencyDrift))
                        {
                            repeatFound = true;
                        }
                        else
                        {
                            frequencyTracker.Add(frequencyDrift);
                        }
                        break;
                    default:
                        Console.WriteLine("Didn't get valid sign :(");
                        break;
                }
                if (repeatFound){Console.WriteLine("Repeat Found - "+frequencyDrift);}
            }

            //Console.WriteLine("Final frequency drift for calibration = "+frequencyDrift);
        }
    }
}
