using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace AoC2018
{
    public class Day4
    {
        public static void TestDay4()
        {
            Console.WriteLine("-------------------DAY 4-------------------");

            StreamReader sr = new StreamReader("Day4Input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                
            }


            DateTime dt;
            //bool test = DateTime.TryParse("1518-03-31 00:50", out dt);
            
        }
    }

    public class LogEntry
    {
        public LogEntry()
        {

        }

        public DateTime eventTime;
        public string eventType;
        public int guardNumber;
    }
}


