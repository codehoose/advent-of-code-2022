using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Day4
{
    internal class Program
    {
        private static Regex _rangeCollectionRegex = new Regex(@"\d+-\d+");
        private static Regex _rangeRegex = new Regex(@"\d+");

        /// <summary>
        /// https://adventofcode.com/2022/day/4
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int count = 0;

            using (Stream s = File.OpenRead("input.txt"))
            {
                using (TextReader reader = new StreamReader(s))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        Range[] ranges = GetRanges(line);
                        if (Contains(ranges[0], ranges[1]) || Contains(ranges[1], ranges[0]))
                        {
                            count++;
                        }
                    }
                }
            }

            Console.WriteLine($"There are {count} ranges that overlap");
            Console.ReadLine();
        }

        static Range[] GetRanges(string line)
        {
            MatchCollection matches = _rangeCollectionRegex.Matches(line);
            Range first = GetRange(matches[0].Value);
            Range second = GetRange(matches[1].Value);
            return new Range[] { first, second };
        }

        static Range GetRange(string range)
        {
            MatchCollection matches = _rangeRegex.Matches(range);
            int start = int.Parse(matches[0].Value);
            int end = int.Parse(matches[1].Value);
            return new Range { start = start, end = end };
        }

        static bool Contains(Range first, Range second)
        {
            return second.start >= first.start && second.end <= first.end;
        }
    }

    struct Range
    {
        public int start;
        public int end;
    }
}
