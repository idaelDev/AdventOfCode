using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/10
    internal class Solver_10_2015 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string input = data;
            for (int i = 0; i< 40; i++)
            {
                input = GetLookAndSay(input);
                Console.WriteLine(i);
            }

            return input.Length.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string input = data;
            for (int i = 0; i < 50; i++)
            {
                input = GetLookAndSay(input);
                Console.WriteLine(i);
            }

            return input.Length.ToString();
        }

        string GetLookAndSay(string input)
        {
            StringBuilder sb = new StringBuilder();

            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                count++;
                if (i == input.Length-1 || input[i] != input[i+1])
                {
                    sb.Append($@"{count}{input[i]}");
                    count = 0;
                }
            }

            return sb.ToString();
        }
    }
}
