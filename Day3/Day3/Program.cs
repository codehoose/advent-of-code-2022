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
                    while (reader.Peek() != -1)
                    {
                        ulong[] lines = new ulong[3];
                        for (int i = 0; i < 3; i++)
                        {
                            string line = reader.ReadLine();

                            for (int c = 0; c < line.Length; c++)
                            {
                                char ch = line[c];
                                int value = ch >= 'a' ? ch - 'a' : ch - 'A' + 26;
                                lines[i] |= (ulong)Math.Pow(2, value);
                            }                            
                        }

                        // Used bit fields to determine the duplicate chars in each line.
                        // The AND function above ands the values together until we're left with a
                        // bit field that indexes to the a-z-A-Z values. Then I convert that to a 
                        // string and find the first occurence of '1'. Subtract from the length and
                        // you get the value we need.
                        ulong output = And(lines[0], lines[1], lines[2]);
                        string binary = Convert.ToString((long)output, 2);
                        int weight = binary.Length - binary.IndexOf('1');
                        count += weight;
                    }
                }
            }

            Console.WriteLine($"Total count: {count}");
            Console.ReadLine();
        }

        static ulong And(ulong u1, ulong u2, ulong u3)
        {
            ulong output = 0;
            for (int i = 0; i < 64; i++)
            {
                ulong bit = (ulong)Math.Pow(2, i);
                if ((u1 & bit) == bit && (u2 & bit) == bit && (u3 & bit) == bit)
                {
                    output |= bit;
                }
            }

            return output;
        }
    }
}
