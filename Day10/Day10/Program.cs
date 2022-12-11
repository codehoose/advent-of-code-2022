using System;
using System.IO;

namespace Day10
{
    internal class Program
    {
        /// <summary>
        /// https://adventofcode.com/2022/day/10
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");


            int signalStrength = 0;
            Machine machine = new Machine(lines);
            while (machine.IsRunning)
            {
                machine.Tick();
                switch(machine.Cycle)
                {
                    case 20:
                    case 60:
                    case 100:
                    case 140:
                    case 180:
                    case 220:
                        signalStrength += machine.X * machine.Cycle;
                        break;
                }
            }

            Console.WriteLine($"RegX = {machine.X}, Cycles = {machine.Cycle}, Signal Strength = {signalStrength}");
            Console.ReadLine();
        }
    }
}