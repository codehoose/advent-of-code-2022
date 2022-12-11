using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Monkey> monkeys = MonkeySerializer.Deserialize("input.txt");
            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.HasItems)
                    {
                        monkey.Inspect();
                        monkey.Worry();
                        monkey.Bored();
                        int nextMonkey = monkey.ChuckTo();
                        monkeys[nextMonkey].Receive(monkey.CurrentItem);
                    }
                }
            }

            var sorted = monkeys.OrderBy(m => m.Inspected)
                                .ToList();
            foreach (var m in sorted)
            {
                Console.WriteLine($"Monkey {m.Id} inspected items {m.Inspected} times.");
            }

            uint top = sorted[sorted.Count - 1].Inspected;
            uint nextTop = sorted[sorted.Count - 2].Inspected;

            Console.WriteLine($"The level of monkey business in this situation can be found by multiplying these together: {top*nextTop}");

            Console.ReadLine();

        }
    }
}
