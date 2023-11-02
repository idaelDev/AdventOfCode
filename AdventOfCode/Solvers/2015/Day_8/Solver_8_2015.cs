using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/8
    internal class Solver_8_2015 : ISolver
    {
        private string patternOne = @"\\|\\""";
        private string patternX = @"\\x[0-9A-Fa-f]{2}";

        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] input = Tools.LineSplit(data);

            int memoryCount = 0;
            int litteralCount = 0;


            foreach (var item in input)
            {
                
                litteralCount += item.Length;
                memoryCount += item.Length-2;
                
                string s = item.Substring(1, item.Length - 2);
                MatchCollection mc1 = Regex.Matches(s, @"\\\\");
                MatchCollection mc2 = Regex.Matches(s, "\\\\\\\"");
                MatchCollection mc3 = Regex.Matches(s, @"\\x[0-9A-Fa-f]{2}");

                int count = mc1.Where(m => !(mc2.Any(a => a.Index == m.Index) || mc3.Any(a => a.Index == m.Index))).Count();
                count += mc2.Count() + mc3.Count() * 3;

                memoryCount -= count;
            }

            return (litteralCount- memoryCount).ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            return "unsolved";
        }
    }
}
