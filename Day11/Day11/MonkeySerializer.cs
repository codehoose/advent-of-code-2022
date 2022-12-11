using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day11
{
    internal static class MonkeySerializer
    {
        public static List<Monkey> Deserialize(string file)
        {
            List<Monkey> monkeys = new List<Monkey>();
            Regex regex = new Regex(@"\d+");

            string[] lines = File.ReadAllLines(file);
            int numMonkeys = (lines.Length + 1) / 7;
            for (int i = 0; i < numMonkeys; i++)
            {
                int index = i * 7;
                int monkeyID = int.Parse(regex.Match(lines[index++]).Value);
                MatchCollection matches = regex.Matches(lines[index++]);
                Match[] items = new Match[matches.Count];
                matches.CopyTo(items, 0);
                List<int> initialItems = items.Select(m => int.Parse(m.Value))
                                              .ToList();
                Func<int, int> operation = GetOperation(lines[index++]);
                string testLine = lines[index++];
                int iftrue = int.Parse(regex.Match(lines[index++]).Value);
                int iffalse = int.Parse(regex.Match(lines[index++]).Value);
                Func<int, int> test = GetTest(testLine, iftrue, iffalse);


                monkeys.Add(new Monkey(monkeyID, initialItems, operation, test));
            }

            return monkeys;
        }

        private static Func<int, int> GetTest(string line, int ifTrue, int ifFalse)
        {
            Regex regex = new Regex(@"\d+");
            int value = int.Parse(regex.Match(line).Value);

            return (i) => (i % value) == 0 ? ifTrue : ifFalse;
        }

        private static Func<int, int> GetOperation(string line)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(line);
            int value = (match.Success) ? int.Parse(match.Value) : 0;

            if (line.Contains("*"))
            {
                if (value == 0)
                    return (i) => i * i;

                return (i) => i * value;
            }
            else if (line.Contains("+"))
            {
                return (i) => i + value;
            }
            else if (line.Contains("/"))
            {
                return (i) => i / value;
            }

            return (i) => i;
        }
    }
}
