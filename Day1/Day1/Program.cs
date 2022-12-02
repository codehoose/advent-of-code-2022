using System;
using System.Collections.Generic;
using System.IO;

namespace Day1
{
    internal class Program
    {
        /// <summary>
        /// https://adventofcode.com/2022/day/1
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SortedList<int, int> topThree = new SortedList<int, int>();

            int elf = 0;
            int totalElves = 0;
            int elfMaxCalories = 0;
            int elfFound = -1;
            int elfCalories = 0;
            int lines = 0;

            using (Stream stream = File.OpenRead("input.txt"))
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    do
                    {
                        string line = reader.ReadLine();
                        lines++;

                        if (string.IsNullOrEmpty(line))
                        {
                            if (elfCalories > elfMaxCalories)
                            {
                                elfMaxCalories = elfCalories;
                                elfFound = elf;
                                Console.WriteLine($"{elfFound:000} {elfCalories}");
                                
                            }

                            if (!topThree.ContainsKey(elfCalories))
                                topThree.Add(elfCalories, elfCalories);

                            elfCalories = 0;
                            elf++;
                            totalElves++;
                        }
                        else
                        {
                            int tmp = int.Parse(line);
                            elfCalories += tmp;
                        }
                    }
                    while (reader.Peek() > -1);

                    if (elfCalories > elfMaxCalories)
                    {
                        elfMaxCalories = elfCalories;
                        elfFound = elf;
                        Console.WriteLine($"{elfFound:000} {elfCalories}");
                    }
                }
            }

            Console.WriteLine($"{elfMaxCalories} calories held by elf {elfFound + 1} of {totalElves} in {lines} line(s)");

            int totalTopThree = 0;
            for (int i = topThree.Count-1; i >= topThree.Count - 3; i--)
            {
                totalTopThree += topThree.Values[i];
            }

            Console.WriteLine($"Total top three {totalTopThree}");
            Console.ReadLine();
        }
    }
}
