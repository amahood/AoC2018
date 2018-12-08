using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AoC2018
{
    public class Day6
    {
        public static void TestDay6()
        {
            Console.WriteLine("-------------------DAY 6-------------------");

            StreamReader sr = new StreamReader("Day6Input..txt");
            //StreamReader sr = new StreamReader("Day6Input.1.txt");

            string line;
            List<Tuple<int,int>> orderedPairs = new List<Tuple<int, int>>();
            while ((line = sr.ReadLine()) !=null)
            {
                string[] pair = line.Split(',');
                orderedPairs.Add(Tuple.Create(Int32.Parse(pair[0]),Int32.Parse(pair[1])));
            }

            //Find perimeter
            int left = orderedPairs.Select(p=>p.Item1).ToList().Min();
            int right = orderedPairs.Select(p=>p.Item1).ToList().Max();
            int top  = orderedPairs.Select(p=>p.Item2).ToList().Max();
            int bottom  = orderedPairs.Select(p=>p.Item2).ToList().Min();

            //Find closest marker to all coordinates inside square
            List<Tuple<int,int,int>> gridLocations = new List<Tuple<int, int, int>>(); //schema is x,y,closest as index of original ordered pairs
            for (int x = left;x<right+1;x++)
            {
                for (int y = bottom;y<top+1;y++)
                {
                    List<int> distancesSeen = new List<int>();
                    int currentClosestManhattanDistance = (right-left+1)*(top-bottom+1); //just putting placeholder as max possible to have a comparison
                    
                    int currentClosestPotentialPlot = 0;
                    int markerCounter = 0;

                    foreach (Tuple<int,int> pp in orderedPairs)
                    {
                        int tempManDistance = CalculateManhattanDistance(orderedPairs[markerCounter], Tuple.Create(x,y));
                        distancesSeen.Add(tempManDistance);

                        if (tempManDistance<currentClosestManhattanDistance)
                        {
                            currentClosestManhattanDistance = tempManDistance;
                            currentClosestPotentialPlot = markerCounter;

                        }
                        markerCounter++;
                    }

                    //SEt to -1 to indicate it is a mutually owned square - if the min manahttan distance is in the list of distnaces seen more than one time
                    int minMan = distancesSeen.Min();
                    if (distancesSeen.FindAll(t=>t==minMan).Count>1)
                    {
                        currentClosestPotentialPlot = -1;
                    }
                    gridLocations.Add(Tuple.Create(x,y,currentClosestPotentialPlot));
                }
            }

            //Try to remove anything that has a point on the edge
            HashSet<int> moreItemsToRemove = new HashSet<int>();
            foreach (Tuple<int,int,int> glToRemove in gridLocations)
            {
                if (glToRemove.Item1==left || glToRemove.Item1==right || glToRemove.Item2==top || glToRemove.Item2==bottom)
                {
                    moreItemsToRemove.Add(glToRemove.Item3);
                }
            }

            //Throw out anything that is on the boundary as they are by definition infinite
            List<Tuple<int,int>> potentialMarkers = new List<Tuple<int, int>>();
            int throwOutCounter = 0;
            foreach (Tuple<int,int> p in orderedPairs)
            {
                if (moreItemsToRemove.Contains(throwOutCounter)==false)
                {
                    if ( (p.Item1 != left && p.Item1 != right) && (p.Item2 != top && p.Item2 != bottom)) 
                    {
                        potentialMarkers.Add(p);
                    }
                }
                throwOutCounter++;
            }

            //Now sum squares assigned to each marker
            int currentHighestArea = 0;
            int markerLoopIndex = 0;
            foreach (Tuple<int,int> pp in orderedPairs)
            {
                if (potentialMarkers.Contains(pp))
                {
                    int areaTracker = gridLocations.FindAll(gl=>gl.Item3==markerLoopIndex).Count;
                    if (areaTracker>currentHighestArea) currentHighestArea = areaTracker;
                }
                markerLoopIndex++;
            }

            Console.WriteLine("Highest non-infinite area - "+currentHighestArea);

            //--------------------------PART 2---------------------------------------------------
            int safeAreaCounter = 0;
            for (int x = left;x<right+1;x++)
            {
                for (int y = bottom;y<top+1;y++)
                {
                    bool stillGoodManhattan = true;
                    int orderedPairsCounter = 0;
                    int runningManhattan = 0;

                    while (stillGoodManhattan && orderedPairsCounter<orderedPairs.Count)
                    {
                        int tempManDistance = CalculateManhattanDistance(orderedPairs[orderedPairsCounter], Tuple.Create(x,y));
                        runningManhattan += tempManDistance;
                        if (runningManhattan>=10000)
                        {
                            stillGoodManhattan =false;
                        }
                        orderedPairsCounter++;
                    }
                    if (stillGoodManhattan)safeAreaCounter++;
                }
            }

            Console.WriteLine("Safe area counter - "+safeAreaCounter);
            
        }

        public static int CalculateManhattanDistance(Tuple<int,int> point1, Tuple<int,int> point2)
        {
            int manhattanDistance = 0;
            manhattanDistance = Math.Abs(point2.Item1-point1.Item1) + Math.Abs(point2.Item2-point1.Item2);
            return manhattanDistance;
        }
    }

}



