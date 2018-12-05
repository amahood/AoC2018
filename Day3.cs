using System;
using System.IO;
using System.Collections.Generic;

namespace AoC2018
{
    public class Day3
    {
        public static void TestDay3()
        {
            Console.WriteLine("-------------------DAY 3-------------------");

            StreamReader sr = new StreamReader("Day3Input.txt");
            //StreamReader sr = new StreamReader("Day3Input.1.txt");
            string line;
            int claimNumber = 1;
            //int doubleClaimCount = 0;

            List<Claim> claims = new List<Claim>();
            List<Tuple<int,int>> takenClaims = new List<Tuple<int,int>>();
            List<Tuple<int,int>> doubleClaims = new List<Tuple<int,int>>();            

            //Parse input and make claims
            while ((line = sr.ReadLine()) != null)
            {
                string[] initialSplit = line.Split('@');
                string[] positionAndSize = initialSplit[1].Split(':');
                string[] coords = positionAndSize[0].Split(',');
                string[] dimensions = positionAndSize[1].Split('x');

                //make new claim
                claims.Add(new Claim(claimNumber, Int32.Parse(coords[0]), Int32.Parse(coords[1]), Int32.Parse(dimensions[0]), Int32.Parse(dimensions[1])));

                claimNumber++;
            }

            int claimCount = claims.Count;
            int untouchedClaim = 0;
            //Try to check boundaries

            bool foundIsolated = false;
            while (!foundIsolated)
            {
                for (int c = 0;c<claimCount;c++)
                {
                    Claim sourceClaim = claims[c];

                    foundIsolated = true;

                    for (int cc = 0;cc<claimCount;cc++)
                    {
                        if (c!=cc)
                        {
                            Claim targetClaim = claims[cc];
                            //now COmparing two claims
                            
                            //Now compare all points within claims, eject if hit condition
                            for (int x = sourceClaim.claimLeftIndent;x<sourceClaim.claimLeftIndent+sourceClaim.claimWidth;x++)
                            {
                                for (int y = sourceClaim.claimTopIndent;y<sourceClaim.claimTopIndent+sourceClaim.claimHeight;y++)
                                {
                                    if ( (targetClaim.claimLeftIndent<=x) && (x<=targetClaim.claimLeftIndent+targetClaim.claimWidth) && (targetClaim.claimTopIndent<=y) && (y<=targetClaim.claimTopIndent+targetClaim.claimHeight))
                                    {
                                        //point is inside another one, could throw out both, at least throw out source claim
                                        foundIsolated = false;
                                    }
                                }
                            }
                        }
                        else if (c==cc && foundIsolated==true && c==claimCount){
                            foundIsolated = true;
                        }
                    }
                    if (foundIsolated){
                        untouchedClaim = c+1;
                    }
                }
            }
                    
            
            
            /*//Process claims to make map
            foreach (Claim c in claims)
            {
                //bool untouchedClaim = true;

                for (int x = c.claimLeftIndent;x<c.claimLeftIndent+c.claimWidth;x++)
                {
                    for (int y = c.claimTopIndent;y<c.claimTopIndent+c.claimHeight;y++)
                    {
                        Tuple<int,int> p = Tuple.Create(x,y);

                        //check for exists in taken and promote
                        if (doubleClaims.Contains(p))
                        {
                            //do nothing as it is already doiuble counted and we just want to fall through to the else condition if it wasn't touched
                        }
                        else if (takenClaims.Contains(p))
                        {
                            doubleClaims.Add(p);
                            //takenClaims.Remove(p);
                            doubleClaimCount++;
                            //untouchedClaim = false;
                        }
                        else
                        {
                            takenClaims.Add(p);
                        }
                        
                    }
                }
            }
*/

            //Console.WriteLine("Final count of double claims - "+doubleClaimCount);
            //Console.WriteLine(lastUntouchedClaim);
        }

    }


    public class Claim
    {

        public Claim(int cN, int cLI, int cTI, int cW, int cH)
        {
            claimNumber = cN;
            claimLeftIndent = cLI;
            claimTopIndent = cTI;
            claimWidth = cW;
            claimHeight = cH;
            return;
        }

        public int claimNumber;
        public int claimLeftIndent;
        public int claimTopIndent;
        public int claimWidth;
        public int claimHeight;
    }


}

