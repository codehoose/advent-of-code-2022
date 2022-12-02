using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    internal class Program
    {
        enum RPS
        {
            Rock,
            Paper,
            Scissors
        }

        /// <summary>
        /// https://adventofcode.com/2022/day/2
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int totalScore = 0;

            using (Stream stream = File.OpenRead("input.txt"))
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        RPS opponent = ToIndex(line[0]);
                        RPS player = ToIndex(line[2]);

                        // shape you selected (1 for Rock, 2 for Paper, and 3 for Scissors)
                        int playerScore = 1 + (int)player;
                        playerScore += DetermineWin(opponent, player);
                        totalScore += playerScore;
                    }
                }
            }

            Console.WriteLine($"Total score: {totalScore}");
            Console.ReadLine();
        }

        private static int DetermineWin(RPS opponent, RPS player)
        {
            if (opponent == player)
                return 3; // DRAW

            int diff = (int)player - (int)opponent;
            if (diff == 1 || diff == -2)
                return 6; // PLAYER WIN

            return 0; // PLAYER LOSES
        }

        static RPS ToIndex(char ch)
        {
            int output = ch - 65; // 65 is ordinal for 'A'
            if (output > 2)
                output -= 23; // 23 is the difference between 'A' and 'X'

            return (RPS)output;
        }
    }
}
