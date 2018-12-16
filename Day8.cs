using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AoC2018
{
    public class Day8
    {
        public static void TestDay8()
        {
            Console.WriteLine("-------------------DAY 8-------------------");

            StreamReader sr = new StreamReader("Day8Input.txt");
            //StreamReader sr = new StreamReader("Day8Input.1.txt");
            
            string[] inputTree = sr.ReadLine().Split(' ');
            List<TreePoint> tree = ParseTree(inputTree);

            int runningSum = 0;
            foreach (TreePoint tp in tree)
            {
                foreach (int i in tp.metadataEntries)
                {
                runningSum += i;
                }
            }

            Console.WriteLine("Total Metadata Sum = " + runningSum);
        }

        public static List<TreePoint> ParseTree(string[] tree)
        {
            List<TreePoint> outputTree = new List<TreePoint>();

            TreePoint currentNode = new TreePoint();
            currentNode.numChildNodes = Int32.Parse(tree[0]);
            currentNode.numMetadataEntries = Int32.Parse(tree[1]);
            currentNode.metadataEntries = new int [currentNode.numMetadataEntries];
            int sizeOfInner = tree.Count()-2-currentNode.numMetadataEntries;
            currentNode.innerPayload = new string [sizeOfInner];

            if (currentNode.numChildNodes!=0)
            {

                int innerTracker = 0;
                for (int i = 2;i<tree.Count()-currentNode.numMetadataEntries;i++)
                {
                    currentNode.innerPayload[innerTracker] = tree[i];
                    innerTracker++;
                }

                string[] copyOfInnerPayload = currentNode.innerPayload;
                int indextracker = 2;
                //while (sizeTracker<sizeOfInner)
                while (copyOfInnerPayload.Count()>0)
                {
                    //NOT WORKING, I'm GETTING GOOD STUFF BUT GOING TOO LONG
                    List<TreePoint> returnList = ParseTree(copyOfInnerPayload);
                    foreach (TreePoint tp in returnList)
                    {
                        outputTree.Add(tp);
                        indextracker += (2+tp.numMetadataEntries);
                        int sizeTracker = tree.Length - currentNode.numMetadataEntries - indextracker;

                        //Will have to trim innerpayload here
                        copyOfInnerPayload = new string [sizeTracker];
                        int copyTracker = 0;
                        for (int inner = indextracker; inner<tree.Length-currentNode.numMetadataEntries; inner++)
                        {
                            copyOfInnerPayload[copyTracker] = tree[inner];
                            copyTracker++;
                        }
                    }
                }

                //PArse metadat nodes
                int mdCounter = 0;
                for (int md = tree.Count()-currentNode.numMetadataEntries;md<tree.Count();md++)
                {
                    currentNode.metadataEntries[mdCounter] = Int32.Parse(tree[md]);
                    mdCounter++;
                }
                outputTree.Add(currentNode);
            }

            //TRY TO HANDLE LEAF NODE
            else if (currentNode.numChildNodes==0)
            {
                //Process leaf node
                TreePoint tp = new TreePoint();
                tp.numChildNodes = 0;
                tp.numMetadataEntries = currentNode.numMetadataEntries;
                tp.metadataEntries = new int [currentNode.numMetadataEntries];
                tp.innerPayload = new string [0];
                int mdCounter = 0;
                for (int md = 2;md<2+tp.numMetadataEntries;md++)
                {
                    tp.metadataEntries[mdCounter] = Int32.Parse(tree[md]);
                    mdCounter++;
                }
                outputTree.Add(tp);
                return outputTree;
            }

            return outputTree;
        }

    }

    public class TreePoint
    {
        public TreePoint(){}

        public int numChildNodes;
        public int numMetadataEntries;
        public int[] metadataEntries;

        public string[] innerPayload;
    }

    

}



