using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/2
    internal class Solver_2_2015 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            return Tools.LineSplit(data).ToList()
                .Select(s => s.Split('x').Select(p => int.Parse(p)).ToArray())
                .Select(v => new int[] { v[0] * v[1], v[1] * v[2], v[2] * v[0] })
                .Select(s => s.Min() + s.Sum() * 2)
                .Sum().ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            return Tools.LineSplit(data).ToList()
                .Select(s => s.Split('x').Select(p => int.Parse(p)).ToArray().OrderBy(s => s))
                .Select(s => s.First() * 2 + s.Skip(1).First() * 2 + s.Aggregate(1, (a, b) => a * b))
                .Sum().ToString();
        }
    }
}
