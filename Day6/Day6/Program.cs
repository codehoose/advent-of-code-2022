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

            for (int i = 0; i < input.Length; i++)
            {
                int length = input.Length - i;
                if (length < 4) break;
                string testString = input.Substring(i, 4);
                if (IsUnique(testString))
                {
                    Console.WriteLine($"The first marker is at position {i + 4}");
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
