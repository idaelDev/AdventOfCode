using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/12
    internal class Solver_12_2015 : ISolver
    {

        string pattern = @"-?\b\d+\b";

        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            MatchCollection mc = Regex.Matches(data, pattern);

            int sum = mc.ToList().Select(m => int.Parse(m.Value)).Sum();

            return sum.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            JsonDocument jd = JsonDocument.Parse(data);
            return "unsolved";
        }
    }
}
