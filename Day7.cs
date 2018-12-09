using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AoC2018
{
    public class Day7
    {
        public static void TestDay7()
        {
            Console.WriteLine("-------------------DAY 7-------------------");

            StreamReader sr = new StreamReader("Day7Input.txt");
            //StreamReader sr = new StreamReader("Day7Input.1.txt");

            string line;
            List<Tuple<char,char>> instructions = new List<Tuple<char, char>>();

            while ((line = sr.ReadLine()) !=null)
            {
                instructions.Add(Tuple.Create(line[5],line[36]));
            }

            //Get into list of nodes and pre-requisites
            List<Node> instructionNodes = new List<Node>();
            HashSet<char> preReqsSeen = new HashSet<char>();
            HashSet<char> instructionsSeen = new HashSet<char>();
            foreach (Tuple<char,char> i in instructions)
            {
                if ( (instructionNodes.Count(n=>n.nodeName==i.Item2))>0 )
                {
                    foreach (Node n in instructionNodes)
                    {
                        if (n.nodeName==i.Item2)
                        {
                            n.nodePreReqs.Add(i.Item1);
                        }
                    }
                }
                else
                {
                    Node newNode = new Node(i.Item2);
                    newNode.nodePreReqs.Add(i.Item1);
                    instructionNodes.Add(newNode);
                }
                preReqsSeen.Add(i.Item1);
                instructionsSeen.Add(i.Item2);
            }

            //Find starting point, item with no pre-preqs - Won't be in instructionNodes
            //Need to find item that is referenced as a pre-req but doesn't show up in intructionsList
            List<char> readySteps = new List<char>();
            foreach (char c in preReqsSeen)
            {
                if ( (instructionNodes.Count(n=>n.nodeName==c))==0 )
                {
                    readySteps.Add(c);
                }
            }

            //Need to find end - I THINK THIS IS WRONG, NOT NECESSARILY THE ONLY END POINMT (seems to be for my input)
            char endNode ='0';
            foreach (char c in instructionsSeen)
            {
                if (!preReqsSeen.Contains(c))
                {
                    endNode = c;
                }
            }

            //Now Traverse map
            StringBuilder sb = new StringBuilder();
            HashSet<char> haveProcessed = new HashSet<char>();
            while (readySteps.Count>0)
            {
                //pick lowest from available and add to string builder
                char lowestChar = FindLowestWithAllPreReqsMet( haveProcessed, readySteps, instructionNodes);
                sb.Append(lowestChar);
                //remove from available
                readySteps.Remove(lowestChar);
                haveProcessed.Add(lowestChar);
                //find available from what we just removed and add
                foreach (char c in FindAvailable(lowestChar,instructionNodes,endNode))
                {
                    if (!readySteps.Contains(c) && !haveProcessed.Contains(c)){ readySteps.Add(c);}
                }
            }
            sb.Append(endNode);

            Console.WriteLine("Order - " + sb.ToString());
        }

        public static List<char> FindAvailable(char c,List<Node> instructionNodes, char endNode)
        {
            List<char> availableSteps = new List<char>();
            //Add available steps - things for which c is a pre-requisite
            foreach (Node n in instructionNodes)
            {
                if (n.nodePreReqs.Contains(c) && n.nodeName!=endNode)
                {
                    availableSteps.Add(n.nodeName);
                }
            }
            return availableSteps;

        }

        public static char FindLowestWithAllPreReqsMet(HashSet<char> haveProcessed, List<char> readySteps, List<Node> instructionNodes)
        {
            char ch = '0';
            readySteps.Sort();
            bool returnFound = false;
            int loopTracker = 0;
            List<char> nodePreReqs = new List<char>();
            while (!returnFound)
            {
                bool localReturnFound = true;
                Node n = instructionNodes.Find(i=>i.nodeName==readySteps[loopTracker]);

                if (n==null)
                {
                    ch = readySteps[loopTracker];
                    localReturnFound = true; returnFound = true;
                }
                else
                {
                    foreach (char p in n.nodePreReqs)
                    {
                        if (!haveProcessed.Contains(p))
                        {
                            localReturnFound = false;
                        }
                    }
                    if (localReturnFound){returnFound=true;ch = readySteps[loopTracker];}
                    loopTracker++;
                }
            }
            
            return ch;
        }
    }

    public class Node
    {
        public Node(char name)
        {
            nodeName = name;
            nodePreReqs = new List<char>();
        }
        public char nodeName;
        public List<char> nodePreReqs;
    }

}



