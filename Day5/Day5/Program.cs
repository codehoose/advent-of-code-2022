using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day5
{
    internal class Program
    {
        /// <summary>
        /// https://adventofcode.com/2022/day/5
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Notes:
            // Parse out the header to find the number of columns. Re-read the lines stored to generate the lists.
            // Use stacks??? - Yes :) 

            using (Stream s = File.OpenRead("input.txt"))
            {
                using (TextReader reader = new StreamReader(s))
                {
                    // Read the header
                    List<string> columns = new List<string>();
                    string line;
                    while ((line = reader.ReadLine()) != "")
                    {
                        columns.Add(line);
                    }

                    List<Stack<char>> stacks = GetStacks(columns);

                    // Now until the end of the file, read the numbers and shuffle the decks.
                    Regex digitRegex = new Regex(@"\d+");
                    while ((line = reader.ReadLine()) != null)
                    {
                        Instruction instruction = GetInstruction(digitRegex, line);
                        for (int n = 0; n < instruction.count; n++)
                        {
                            char value = stacks[instruction.source - 1].Pop();
                            stacks[instruction.destination - 1].Push(value);
                        }
                    }

                    string str = "";
                    foreach(var stack in stacks)
                    {
                        str += stack.Peek();
                    }

                    Console.WriteLine($"The values at the top of the stacks are {str}");
                    Console.ReadLine();
                }
            }
        }

        static Instruction GetInstruction(Regex regex, string line)
        {
            MatchCollection matches = regex.Matches(line);
            return new Instruction
            {
                count = int.Parse(matches[0].Value),
                source = int.Parse(matches[1].Value),
                destination = int.Parse(matches[2].Value)
            };
        }

        static List<Stack<char>> GetStacks(List<string> columns)
        {
            List<Stack<char>> stacks = new List<Stack<char>>();

            int maxCol = GetMaxColumn(columns[columns.Count - 1]);
            columns.RemoveAt(columns.Count - 1);

            for (int s = 0; s < maxCol; s++)
            {
                stacks.Add(new Stack<char>());
            }

            for (int i = columns.Count - 1; i >= 0; i--)
            {
                char[] chars = GetColumnValue(columns[i]);
                for (int ci = 0; ci < chars.Length; ci++)
                {
                    if (chars[ci] != ' ')
                    {
                        stacks[ci].Push(chars[ci]);
                    }
                }
            }

            return stacks;
        }

        static char[] GetColumnValue(string columnValues)
        {
            List<char> chars = new List<char>();
            int columns = columnValues.Length / 3;
            for (int i = 0; i < columnValues.Length; i += 4)
            {
                string sub = columnValues.Substring(i, 3);
                sub = sub.Substring(1, sub.Length - 2);
                chars.Add(sub[0]);
            }

            return chars.ToArray();
        }

        static int GetMaxColumn(string columnIndices)
        {
            Regex digits = new Regex(@"\d+");
            MatchCollection columnIndexes = digits.Matches(columnIndices);
            return int.Parse(columnIndexes[columnIndexes.Count - 1].Value);
        }
    }

    struct Instruction
    {
        public int count;
        public int source;
        public int destination;
    }
}
