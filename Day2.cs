using System;
using System.IO;
using System.Collections.Generic;

namespace AoC2018
{
    public class Day2
    {
        public static void TestDay2()
        {
            Console.WriteLine("-------------------DAY 2-------------------");

            StreamReader sr = new StreamReader("Day2Input.txt");
            string line="";

            List<string> possibleIds = new List<string>();
           int numWithTwo = 0;
           int numWithThree = 0;

            while ((line = sr.ReadLine()) != null)
            {
                if (CheckForTwo(line)){numWithTwo++;possibleIds.Add(line);}
                if (CheckForThree(line)){numWithThree++;possibleIds.Add(line);}
            }

            int checksum = numWithTwo*numWithThree;
            Console.WriteLine("Checksum = "+checksum);

            for (int i=0;i<possibleIds.Count;i++)
            {
                string compA = possibleIds[i];
                Console.WriteLine("Checking for matches with "+compA);
                for (int j=i+1;j<possibleIds.Count;j++)
                {
                    string compB = possibleIds[j];
                    int compIndex = 0;
                    int difCounter = 0;
                    //Core comparison loop
                    foreach (char c in compA)
                    {
                        if (compA[compIndex]!=compB[compIndex])
                        {difCounter++;}
                        compIndex++;
                    }

                    if (difCounter==1)
                    {
                        Console.WriteLine("Box IDs = ");
                        Console.WriteLine(compA);
                        Console.WriteLine(compB);

                        //Process similar
                        int finalIndex = 0;
                        foreach (char c in compA)
                        {
                            if (compA[finalIndex]!=compB[finalIndex])
                            {
                                Console.WriteLine("Final string - " + compA.Remove(finalIndex,1));
                            }
                            finalIndex++;
                        }
                    }
                }
            }
        }

        public static bool CheckForTwo(string s)
        {
            
            HashSet<char> twosCharHash = new HashSet<char>();
            foreach (char c in s)
            {
                if (!twosCharHash.Contains(c))
                {
                    twosCharHash.Add(c);
                    int count = s.Split(c).Length - 1;
                    if (count==2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CheckForThree(string s)
        {
            HashSet<char> threesCharHash = new HashSet<char>();
            foreach (char c in s)
            {
                if (!threesCharHash.Contains(c))
                {
                    threesCharHash.Add(c);
                    int count = s.Split(c).Length - 1;
                    if (count==3)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }

}
