using System;
using System.IO;

namespace Day8
{
    internal class Program
    {
        /// <summary>
        /// https://adventofcode.com/2022/day/8
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            // Part 1
            int count = GetVisible(lines);
            Console.WriteLine($"There are {count} trees visible");

            // Part 2
            int scenicScore = GetScenicScore(lines);
            Console.WriteLine($"Maximum scenic score is {scenicScore}");

            Console.ReadLine();
        }

        static int GetScenicScore(string[] lines)
        {
            int scenicScore = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (!(y == 0 || x == 0 || y == lines.Length - 1 || x == lines[y].Length - 1))
                    {
                        int cellScore = GetCellScore(x, y, lines, -1, 0) *
                                        GetCellScore(x, y, lines, 1, 0) *
                                        GetCellScore(x, y, lines, 0, -1) *
                                        GetCellScore(x, y, lines, 0, 1);

                        scenicScore = cellScore > scenicScore ? cellScore : scenicScore;
                    }
                }
            }

            return scenicScore;
        }

        static int GetVisible(string[] lines)
        {
            int count = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (y == 0 || x == 0 || y == lines.Length - 1 || x == lines[y].Length - 1)
                    {
                        count++;
                    }
                    else
                    {
                        bool hasLineOfSight = HasLineOfSight(x, y, lines, -1, 0) ||
                                              HasLineOfSight(x, y, lines, 1, 0) ||
                                              HasLineOfSight(x, y, lines, 0, -1) ||
                                              HasLineOfSight(x, y, lines, 0, 1);

                        count = hasLineOfSight ? count + 1 : count;
                    }
                }
            }

            return count;
        }

        static bool HasLineOfSight(int x, int y, string[] lines, int xdir, int ydir)
        {
            int treeValue = int.Parse(lines[y][x].ToString());
            while (x > 0 && y > 0 && y < lines.Length - 1 && x < lines[0].Length - 1)
            {
                x += xdir;
                y += ydir;

                int currentTreeValue = int.Parse(lines[y][x].ToString());
                if (currentTreeValue >= treeValue)
                    return false;
            }

            return true;
        }

        static int GetCellScore(int x, int y, string[] lines, int xdir, int ydir)
        {
            int sx = x;
            int sy = y;
            int treeValue = int.Parse(lines[y][x].ToString());
            while (x > 0 && y > 0 && y < lines.Length - 1 && x < lines[0].Length - 1)
            {
                x += xdir;
                y += ydir;

                int currentTreeValue = int.Parse(lines[y][x].ToString());
                if (currentTreeValue >= treeValue)
                {
                    break;
                }
            }

            return sx != x ? x - sx : y - sy;
        }
    }
}
