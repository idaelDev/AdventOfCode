using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2024/day/3
    internal class Solver_3_2024 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string regex = @"mul\(\d{1,3},\d{1,3}\)";
            var result = Regex.Matches(data, regex)
                .Select(mc => int.Parse(mc.Value.Split(',')[0].Split('(')[1]) * int.Parse(mc.Value.Split(',')[1].Split(')')[0])).Sum();
            return result.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string regex = @"(mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\))";
            var matchs = Regex.Matches(data, regex);
            
            bool isEnabled = true;
            int sum = 0;
            foreach (Match match in matchs)
            {
                if(match.Value == "don't()")
                {
                    isEnabled = false;
                }
                else if(match.Value == "do()")
                {
                    isEnabled = true;
                }
                else if(isEnabled)
                {

                    sum += int.Parse(match.Value.Split(',')[0].Split('(')[1]) * int.Parse(match.Value.Split(',')[1].Split(')')[0]);
                }

            }

            return sum.ToString();
        }
    }
}
