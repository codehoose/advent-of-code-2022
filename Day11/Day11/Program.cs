using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    internal class Program
    {
        static int ROUNDS = 20;

        static void Main(string[] args)
        {
            List<Monkey> monkeys = MonkeySerializer.Deserialize("input2.txt");
            for (int i = 0; i < ROUNDS; i++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.HasItems)
                    {
                        int nextMonkey = monkey.DoAll();
                        monkeys[nextMonkey].Receive(monkey.CurrentItem);
                    }
                }

                //Console.WriteLine($"== After round {i} ==");

                //foreach (var m in monkeys)
                //{
                //    Console.WriteLine($"Monkey {m.Id} inspected items {m.Inspected} times.");
                //}
                //Console.WriteLine();
            }

            Console.WriteLine($"== Final result ==");

            var sorted = monkeys.OrderBy(m => m.Inspected)
                                .ToList();
            foreach (var m in sorted)
            {
                Console.WriteLine($"Monkey {m.Id} inspected items {m.Inspected} times.");
            }

            uint top = sorted[sorted.Count - 1].Inspected;
            uint nextTop = sorted[sorted.Count - 2].Inspected;

            // It's not 739821348 that's for sure :/ 
            Console.WriteLine($"The level of monkey business in this situation can be found by multiplying these together: {top*nextTop}");

            Console.ReadLine();

        }
    }
}
