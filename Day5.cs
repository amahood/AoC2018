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

            Console.WriteLine("Final string - " + rawPolymer + " / length - "+rawPolymer.Count());
            
        }
    }

}



