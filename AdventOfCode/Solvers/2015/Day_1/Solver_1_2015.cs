using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/1
    internal class Solver_1_2015 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            return (data.Count(c => c == '(') - data.Count(c => c == ')')).ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            return "unsolved";
        }
    }
}
