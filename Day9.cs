using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AoC2018
{
    public class Day9
    {
        public static void TestDay9()
        {
            Console.WriteLine("-------------------DAY 9-------------------");

            //Game Parameters
            int numPlayers = 430;
            //int numPlayers = 10;
            int lastMarbleValue = 71588*100;
            //int lastMarbleValue = 1618;


            Dictionary<int,long> scores = new Dictionary<int, long>();
            for (int n = 1;n<numPlayers+1;n++)
            {
                scores.Add(n,0);
            }
            List<int> marbleCircle = new List<int>();

            //SEt up initial rounds
            marbleCircle.Add(0);
            marbleCircle.Add(1);
            int currentMarbleValue = 1;
            int currentMarblePosition = 1;
            int currentPlayer = 2;
            int nextMarbleUp = 2;

            while (nextMarbleUp < (lastMarbleValue+1))
            {
                
                if (nextMarbleUp%23==0)
                {
                    scores[currentPlayer] += nextMarbleUp;
                    
                    if (currentMarblePosition<7)
                    {
                        currentMarblePosition = marbleCircle.Count-(7-currentMarblePosition);
                    }
                    else
                    {
                        currentMarblePosition -= 7;    
                    }
                    scores[currentPlayer] += marbleCircle[currentMarblePosition];
                    marbleCircle.RemoveAt(currentMarblePosition);
                    currentMarbleValue = marbleCircle[currentMarblePosition];
                }
                else
                {
                    //Case when you are just adding to end
                    if (currentMarblePosition+1==marbleCircle.Count-1)
                    {
                        currentMarblePosition = marbleCircle.Count;
                        marbleCircle.Add(nextMarbleUp);
                        currentMarbleValue = nextMarbleUp;
                    }
                    //CAse when you are at end and need to insert after first element
                    else if (currentMarblePosition+1==marbleCircle.Count)
                    {
                        currentMarblePosition = 1;
                        marbleCircle.Insert(currentMarblePosition, nextMarbleUp);
                        currentMarbleValue = nextMarbleUp;
                    }
                    else
                    {
                        marbleCircle.Insert(currentMarblePosition+2,nextMarbleUp);
                        currentMarblePosition = currentMarblePosition +2;
                        currentMarbleValue = nextMarbleUp;
                    }
                }

                //increment player
                if (currentPlayer==numPlayers)
                {
                    currentPlayer = 1;
                }
                else
                {
                    currentPlayer++;
                }

                //Increment nextMarbleUp
                nextMarbleUp++;
            }
            
            long maxValue = scores.Max(KeyValuePair=>KeyValuePair.Value);
            int maxIndex = scores.Where(kvp=> kvp.Value == maxValue).First().Key;

            long doubleCheckMax = 0;
            foreach (KeyValuePair<int,long> kvp in scores)
            {
                if (kvp.Value > doubleCheckMax)
                {
                    doubleCheckMax = kvp.Value;
                }
            }

            Console.WriteLine("Elf "+maxIndex+" scored highest with "+maxValue);
            
        }

       
    }
    

}



