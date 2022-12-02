using System;
using System.IO;

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
            //Day2Part1();
            Day2Part2();
        }

        private static void Day2Part2()
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
                        int playerState = (int)ToIndex(line[2]); // 0 lose, draw, win

                        // Figure out the player's actual strategy based on the state
                        RPS player = GetPlay(opponent, playerState);


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

        private static void Day2Part1()
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


        private static RPS GetPlay(RPS opponent, int playerState)
        {
            if (playerState == 1)
                return opponent; // DRAW

            if (playerState == 0)
                return (RPS)((3 + ((int)opponent) - 1) % 3);

            return (RPS)(((int)opponent + 1) % 3);
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
