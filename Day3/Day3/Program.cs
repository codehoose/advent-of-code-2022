using System;
using System.IO;

namespace Day3
{
    internal class Program
    {
        /// <summary>
        /// https://adventofcode.com/2022/day/3
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Read the file, split into two lists. 
            // Test each list to see if it contains duplicate tokens.
            // Get those duplicate token(s) and apply an index value to them.
            // Total up the tokens
            int count = 0;

            using (Stream stream = File.OpenRead("input.txt"))
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        int containerSize = line.Length / 2; // always exactly half

                        // Brute force version
                        for (int i = 0; i < containerSize; i++)
                        {
                            if (line.IndexOf(line[i], containerSize) >= 0)
                            {
                                char duplicate = line[i];
                                int value = duplicate >= 'a' ? duplicate - 'a' + 1 : duplicate - 'A' + 27;
                                count += value;
                                break;
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Total count: {count}");
            Console.ReadLine();
        }
    }
}
