using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AoC2018
{
    public class Day4
    {
        public static void TestDay4()
        {
            Console.WriteLine("-------------------DAY 4-------------------");

            StreamReader sr = new StreamReader("Day4Input.txt");
            //StreamReader sr = new StreamReader("Day4Input.1.txt");
            string line;
            List<LogEntry> log = new List<LogEntry>();

            while ((line = sr.ReadLine()) != null)
            {
                LogEntry tempLE = new LogEntry();
                bool dateParse = DateTime.TryParse(line.Substring(1,16), out tempLE.eventTime);
                string remainder = line.Substring(19);

                switch(remainder[0])
                {
                    case 'G':
                        string s = remainder.Substring(7);
                        //now split on space and take array 0
                        string[] sa = s.Split(' ');
                        tempLE.eventType = "guard";
                        tempLE.guardNumber = Int32.Parse(sa[0]);
                        break;
                    case 'f':
                        tempLE.eventType = "sleep";
                        break;
                    case 'w':
                        tempLE.eventType = "wake";
                        break;
                }

                log.Add(tempLE);
            }

            List<LogEntry> sortedLog = log.OrderBy(o=>o.eventTime).ToList();
            int lastGuard = 0;
            string lastSeen = "";
            
            List<Tuple<int,int,int,int>> sleepingLog = new List<Tuple<int, int,int,int>>();

            int sleepingMinute = 0;

            foreach (LogEntry l in sortedLog)
            {
                
                switch (l.eventType)
                {
                    case "guard":
                        if (lastSeen=="sleep")
                        {
                            Console.WriteLine("Error condition");
                        }
                        lastGuard = l.guardNumber;
                        lastSeen = "guard";
                        break;
                    case "sleep":            
                        if (lastSeen=="sleep")
                        {
                            Console.WriteLine("Error condition");
                        }
                        else
                        {
                            sleepingMinute = l.eventTime.Minute;
                        }
                        lastSeen = "sleep";
                        break;
                    case "wake":
                        if (lastSeen!="sleep")
                        {
                            Console.WriteLine("Error condition");
                        }
                        else
                        {
                            int wakeMinute = l.eventTime.Minute;
                            int sleepingDuration = wakeMinute-sleepingMinute;
                            sleepingLog.Add(Tuple.Create(lastGuard, sleepingDuration,sleepingMinute,wakeMinute));
                        }
                        lastSeen = "wake";
                        break;
                }
            }

            //find max guard number
            int maxGuard = 0;
            foreach (Tuple<int,int,int,int> t in sleepingLog)
            {
                if (t.Item1>maxGuard)
                {
                    maxGuard = t.Item1;
                }
            }

            int maxGuardSleeping = 0;
            int sleepiestGuard = 0;
            int sleepingTotalTracker = 0;

            for (int i = 0;i<maxGuard+1;i++)
            {
                int sleepingTotal = 0;;
                foreach (Tuple<int,int,int,int> t in sleepingLog)
                {
                    if (t.Item1==i)
                    {
                        sleepingTotal+=t.Item2;
                    }
                }
                if (sleepingTotal>maxGuardSleeping)
                {
                    maxGuardSleeping = sleepingTotal;
                    sleepiestGuard = i;
                    sleepingTotalTracker = sleepingTotal;
                }
            }

            //Need to go look at log entries now because we don't just need durations, we need specific minutes
            List<Tuple<int,int,int,int>> sleepiestGuardLog = new List<Tuple<int, int,int,int>>();
            foreach (Tuple<int,int,int,int> t in sleepingLog)
            {
                if (t.Item1==sleepiestGuard)
                { 
                    sleepiestGuardLog.Add(Tuple.Create(sleepiestGuard, t.Item2,t.Item3,t.Item4));
                }
            }

            int[] sleepingMinutes = new int[sleepingTotalTracker];
            int sIndex = 0;
            foreach (Tuple<int,int,int,int> t in sleepiestGuardLog)
            {
                for (int s = t.Item3; s<t.Item4;s++)
                {
                    sleepingMinutes[sIndex] = s;
                    sIndex++;
                }
            }

            int sleepiestMinute = 0;
            int maxSleepMinute = sleepingMinutes.Max();
            int sleepiestMinuteCount = 0;

            foreach (int i in sleepingMinutes)
            {
                int currentSleepMinuteCount = 0;
                
                foreach (int j in sleepingMinutes)
                {
                    if (i==j){
                        currentSleepMinuteCount++;
                        }
                }
                if (currentSleepMinuteCount > sleepiestMinuteCount)
                {
                    sleepiestMinute = i;
                    sleepiestMinuteCount = currentSleepMinuteCount;
                }
                
            }

            int multFactor = 0;
            multFactor = sleepiestGuard * sleepiestMinute;
            Console.WriteLine("Sleepiest Guard - "+sleepiestGuard);
            Console.WriteLine("Minutes Sleeping - "+maxGuardSleeping);
            Console.WriteLine("Guard Sleepiness Factor - "+multFactor);
            

            //Part 2
            //SleepingLog has all entries of guards, minutes asleep, starting minute, ending minute
            //Go through all logs and 

            List<GuardEntry> guardLog = new List<GuardEntry>();
            foreach (Tuple<int,int,int,int> t in sleepingLog)
            {
                GuardEntry ge = new GuardEntry();
                ge.guardNo = t.Item1;
                for (int s = t.Item3;s<t.Item4;s++)
                {
                    ge.sleepingMinuteTracker.Add(s);
                }
                guardLog.Add(ge);
            }

            List<GuardEntry> unifiedGuardLog = new List<GuardEntry>();
            List<int> guardsSeen = new List<int>();            
            foreach (GuardEntry ge in guardLog)
            {
                if (!guardsSeen.Contains(ge.guardNo))
                {
                    guardsSeen.Add(ge.guardNo);
                    GuardEntry newGe = new GuardEntry();
                    newGe.guardNo = ge.guardNo;
                    foreach (GuardEntry ige in guardLog)
                    {
                        if (ge.guardNo==ige.guardNo)
                        {
                            foreach (int i in ige.sleepingMinuteTracker)
                            {
                                newGe.sleepingMinuteTracker.Add(i);
                            }
                        }
                    }
                    unifiedGuardLog.Add(newGe);
                }
            }

            int targetGuardNumber = 0;
            int maxMinuteFreq = 0;
            int targetMinute = 0;
            int trackingMinuteFreq = 0;
            int previousMinute = 0;

            foreach (GuardEntry uge in unifiedGuardLog)
            {

                
                //sort list
                uge.sleepingMinuteTracker.Sort();
                foreach (int i in uge.sleepingMinuteTracker)
                {
                    if (i==previousMinute)
                    {
                        //increment trackingminutefreq
                        trackingMinuteFreq++;
                    }
                    else
                    {
                        //means we are on new number, nneed to back compare
                        if (trackingMinuteFreq>maxMinuteFreq)
                        {
                            targetMinute = previousMinute;
                            maxMinuteFreq = trackingMinuteFreq;
                            targetGuardNumber = uge.guardNo;
                        }
                        previousMinute = i;
                        trackingMinuteFreq = 0;
                    }
                }
            }

            Console.WriteLine("Part 2 answer");
            Console.WriteLine("Answer - " + targetGuardNumber*targetMinute);

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

    public class GuardEntry
    {
        public GuardEntry()
        {
            sleepingMinuteTracker = new List<int>();
        }
        public int guardNo;
        public List<int> sleepingMinuteTracker;
    }
}


