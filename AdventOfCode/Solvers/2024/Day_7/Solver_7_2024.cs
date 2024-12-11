using AdventOfCode.API;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2024/day/7
    internal class Solver_7_2024 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data);
            ulong[] expecteds = new ulong[lines.Length];
            ulong[][] numbers = new ulong[lines.Length][];
            for(int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(":");
                expecteds[i] = ulong.Parse(parts[0]);

                numbers[i] = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(c => ulong.Parse(c)).ToArray();

            }

            ulong sum = 0;
            for(int i = 0; i < expecteds.Length; i++)
            {
                if(HasSolution(expecteds[i], numbers[i], false))
                {
                    sum += expecteds[i];
                }
            }

            return sum.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data);
            ulong[] expecteds = new ulong[lines.Length];
            ulong[][] numbers = new ulong[lines.Length][];
            for(int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(":");
                expecteds[i] = ulong.Parse(parts[0]);

                numbers[i] = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(c => ulong.Parse(c)).ToArray();

            }

            ulong sum = 0;
            for(int i = 0; i < expecteds.Length; i++)
            {
                if(HasSolution(expecteds[i], numbers[i], true))
                {
                    sum += expecteds[i];
                }
            }

            return sum.ToString();
        }

        private bool HasSolution(ulong result, ulong[] eq, bool useConcat)
        {
            return Solve(eq[0], 0, result, eq, useConcat);
        }

        private bool Solve(ulong currentResult, int currentIndex, ulong expected, ulong[] numbers, bool useConcat)
        {
            if(currentResult > expected)
            {
                return false;
            }

            if(currentIndex == numbers.Length - 1)
            {
                return currentResult == expected;
            }

            if(useConcat)
            {
                if(Solve(ulong.Parse(currentResult.ToString() + numbers[currentIndex + 1]), currentIndex + 1, expected, numbers, useConcat))
                {
                    return true;
                }
            }

            if(Solve(currentResult + numbers[currentIndex + 1], currentIndex + 1, expected, numbers, useConcat))
            {
                return true;
            }

            if(Solve(currentResult * numbers[currentIndex + 1], currentIndex + 1, expected, numbers, useConcat))
            {
                return true;
            }

            return false;
        }

    }
}
