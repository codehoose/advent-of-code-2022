using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day9
{
    internal class Program
    {
        /// <summary>
        /// https://adventofcode.com/2022/day/9
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<Instruction> instructions = Deserialize("input3.txt");
            List<Knot> tail = new List<Knot>();
            Knot head = new Knot();

            // Part 1
            Walk(instructions, head, tail);
            DrawWalk(tail[0].visited);
            Console.WriteLine($"There were {tail[0].visited.Count} cell visited by the tail");
            Console.ReadLine();
        }

        private static void DrawWalk(List<Cell> cells)
        {
            int maxX = 36; // cells.Select(c => c.x).Max() + 1;
            int maxY = 36; // cells.Select(c => c.y).Max() + 1;

            char[] grid = new char[maxX * maxY];
            for (int x = 0; x < grid.Length; x++) grid[x] = '.';

            foreach (var c in cells)
            {
                int charIndex = c.y * maxX + c.x;
                grid[charIndex] = '#';
            }

            string tmp = "";

            for (int index = grid.Length - maxX; index >= 0; index -= maxX)
            {
                for (int n = 0; n < maxX; n++)
                {
                    tmp += grid[n + index];
                }
                Console.WriteLine(tmp);
                tmp = "";
            }
        }

        private static void Walk(List<Instruction> instructions, Knot head, List<Knot> tail, int numKnots = 10)
        {
            Cell cell = new Cell { x = 0, y = 0 };
            head.visited.Add(cell);

            for (int i = 0; i < numKnots; i++)
            {
                Knot knot = new Knot();
                knot.current = cell;
                knot.visited.Add(cell);
                tail.Add(knot);
            }

            tail.Add(head);

            foreach (var instruction in instructions)
            {
                int dx = GetDXFor(instruction.direction);
                int dy = GetDYFor(instruction.direction);

                int x = head.X;
                int y = head.Y;

                //Console.WriteLine($"\r\n== {instruction} ==\r\n");

                for (int i = 0; i < instruction.steps; i++)
                {
                    x += dx;
                    y += dy;

                    Cell newCell = new Cell { x = x, y = y };
                    head.visited.Add(newCell);

                    // Now check the current head cell from the current tail cell. If it's > 1 then
                    // move the tail towards the head.
                    //MoveKnot(head, tail[tail.Count - 1]);
                    for (int j = tail.Count - 1; j >= 1; j--)
                    {
                        MoveKnot(tail[j], tail[j - 1]);
                    }

                    //DrawWalk(headVisited[headVisited.Count - 1], 'H', currentTail, 'T');
                    //Console.WriteLine();
                }
            }
        }

        private static void MoveKnot(Knot knot1, Knot knot2)
        {
            // Now check the current head cell from the current tail cell. If it's > 1 then
            // move the tail towards the head.
            if (IsTooFarAway(knot1.Top, knot2.current))
            {
                Cell previousHead = FindClosest(knot1.visited, knot2.current);
                if (!knot2.visited.Contains(previousHead))
                {
                    knot2.visited.Add(previousHead);
                }

                knot2.current = previousHead;
            }
        }

        private static void DrawWalk(Cell cell, char ch, Cell cell2, char ch2)
        {
            int maxX = 6;
            int maxY = 6;

            char[] grid = new char[maxX * maxY];
            for (int x = 0; x < grid.Length; x++) grid[x] = '.';

            int charIndex = cell.y * maxX + cell.x;
            grid[charIndex] = ch;

            int charIndex2 = cell2.y * maxX + cell2.x;
            grid[charIndex2] = ch2;

            string tmp = "";

            for (int index = grid.Length - maxX; index >= 0; index -= maxX)
            {
                for (int n = 0; n < maxX; n++)
                {
                    tmp += grid[n + index];
                }
                Console.WriteLine(tmp);
                tmp = "";
            }
        }

        static Cell FindClosest(List<Cell> headVisited, Cell currentTail)
        {
            for (int i = headVisited.Count - 1; i >= 0; i--)
            {
                if (!IsTooFarAway(headVisited[i], currentTail))
                {
                    return headVisited.First(c => c.x == headVisited[i].x && c.y == headVisited[i].y);
                }
            }

            return null;
        }

        static bool IsTooFarAway(Cell c1, Cell c2)
        {
            int xd = Math.Abs(c2.x - c1.x);
            int yd = Math.Abs(c2.y - c1.y);
            return xd > 1 || yd > 1;
        }

        static int GetDXFor(char direction)
        {
            if (direction == 'L') return -1;
            if (direction == 'R') return 1;
            return 0;
        }

        static int GetDYFor(char direction)
        {
            if (direction == 'U') return 1;
            if (direction == 'D') return -1;
            return 0;
        }

        static List<Instruction> Deserialize(string input)
        {
            List<Instruction> instructions = new List<Instruction>();
            string[] lines = File.ReadAllLines(input);
            foreach (var line in lines)
            {
                string[] split = line.Split(" ".ToCharArray());
                char direction = split[0][0];
                int steps = int.Parse(split[1]);
                instructions.Add(new Instruction { direction = direction, steps = steps });
            }

            return instructions;
        }
    }

    public class Cell
    {
        public int x;
        public int y;

        public override string ToString()
        {
            return $"{x}, {y}";
        }
    }

    public class Instruction
    {
        public char direction;
        public int steps;

        public override string ToString()
        {
            return $"{direction} {steps}";
        }
    }

    public class Knot
    {
        public Cell current;

        public List<Cell> visited = new List<Cell>();

        public Cell Top => visited[visited.Count - 1];

        public int X => visited[visited.Count - 1].x;

        public int Y => visited[visited.Count - 1].y;

        public override string ToString()
        {
            return $"Count: {visited.Count}, Top: {Top}";
        }
    }
}
