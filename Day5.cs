using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AoC2018
{
    public class Day5
    {
        public static void TestDay5()
        {
            Console.WriteLine("-------------------DAY 4-------------------");

            StreamReader sr = new StreamReader("Day5Input.txt");
            //StreamReader sr = new StreamReader("Day5Input.1.txt");
            string rawPolymer = sr.ReadLine();
            string rawPolymer2 = rawPolymer;

            string part1Polymer = Day5.ReducePolymer(rawPolymer);

            Console.WriteLine("Final string - " + part1Polymer + " / length - "+part1Polymer.Count());

            //PART 2
            int currentSmallestLength = rawPolymer2.Count();
            int currentRemoveUnit = 0;
            for (int remove = 0;remove<26;remove++)
            {
                //go through and remove all instances of lower and upper - THIS IS GOOD
                string rawPolymerCopy = rawPolymer2;
                int removeTracker = 0;
                while (removeTracker<rawPolymerCopy.Count())
                {
                    if ( (rawPolymerCopy[removeTracker]==65+remove) || (rawPolymerCopy[removeTracker]==97+remove))
                    {
                        rawPolymerCopy = rawPolymerCopy.Remove(removeTracker,1);
                    }
                    else{
                        removeTracker++;
                    }
                }

                //process and capture smallest length
                string processedPolymer = Day5.ReducePolymer(rawPolymerCopy);
                if (processedPolymer.Count()<currentSmallestLength)
                {
                    currentSmallestLength = processedPolymer.Count();
                    currentRemoveUnit = remove;
                }
            }

            Console.WriteLine("Best option is - "+(char)(currentRemoveUnit+65)+" - shortest result - "+currentSmallestLength);
            
        }

        public static string ReducePolymer(string rawPolymer)
        {
            bool wasTouched = true;

            while (wasTouched)
            {
                wasTouched = false;
                char previousChar = rawPolymer[0];
                int i = 1;
                while (i<rawPolymer.Count())
                {
                    char nextChar = rawPolymer[i];

                    //if opposite cases
                    if ( (previousChar+32==nextChar) || (previousChar-32==nextChar) )
                    {
                        wasTouched = true;
                        //Console.WriteLine("Found match");
                        if (i==1)
                        {
                            rawPolymer = rawPolymer.Remove(0,1);
                            //index might change after remove
                            rawPolymer = rawPolymer.Remove(0,1);
                            previousChar = rawPolymer[0];   
                        }
                        else
                        {
                            rawPolymer = rawPolymer.Remove(i-1,1);
                            rawPolymer = rawPolymer.Remove(i-1,1);
                            previousChar = rawPolymer[i-2];
                            i-=1;
                        }
                    }
                    else
                    {
                        previousChar = nextChar;
                        i++;
                    }
                }
            }

            return rawPolymer;
        }
    }

}



