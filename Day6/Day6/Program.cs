using System;
using System.IO;

namespace Day6
{
    internal class Program
    {
        /// <summary>
        /// https://adventofcode.com/2022/day/6
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");
            //Search(input, 4); // Day 6, part 1
            Search(input, 14); //Day 6, part 2
        }

        static void Search(string text, int packetSize)
        {
            for (int i = 0; i < text.Length; i++)
            {
                int length = text.Length - i;
                if (length < packetSize) break;
                string testString = text.Substring(i, packetSize);
                if (IsUnique(testString))
                {
                    Console.WriteLine($"The first marker is at position {i + packetSize}");
                    Console.ReadLine();
                    break;
                }
            }
        }

        static bool IsUnique(string s)
        {
            for (int ch = 1; ch <= s.Length - 1; ch++)
            {
                char test = s[ch - 1];
                for (int i = ch; i < s.Length; i++)
                {
                    if (s[i] == test) return false;
                }
            }

            return true;
        }
    }
}
