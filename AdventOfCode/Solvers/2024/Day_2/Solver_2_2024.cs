using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2024/day/2
    internal class Solver_2_2024 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            List<int[]> reports = Tools.LineSplit(data).Select(x => x.Split(' ').Select(x => int.Parse(x)).ToArray()).ToList();

            //var result = reports.Select(r => r.Zip(r.Skip(1), (x, y) => x - y))
            //    .Where(r => r.Count(x => x > 0) == r.Count() || r.Count(x => x<0) == r.Count())
            //    .Where(r => r.Count(x => Math.Abs(x) <= 3) == r.Count()).Count();

            var result = reports.Where(r => IsValid(r)).Count();

            return result.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            List<int[]> reports = Tools.LineSplit(data).Select(x => x.Split(' ').Select(x => int.Parse(x)).ToArray()).ToList();

            int count = 0;
            foreach (var r in reports)
            {
                bool pass = false;
                for (int i = 0; i < r.Length; i++)
                {
                    var subReport = r.Where((x, j) => j != i).ToArray();
                    if(IsValid(subReport))
                    {
                        pass = true;
                    }
                }
                if (pass)
                {
                    count++;
                }
            }

            return count.ToString();
        }
        
        private bool IsValid(int[] r)
        {
            return IsIncreasing(r) && IsProgressive(r);
        }

        private bool IsIncreasing(int[] r)
        {
            var sum = r.Zip(r.Skip(1), (x, y) => x - y).ToList();
            return sum.Count(x => x > 0) == sum.Count() || sum.Count(x => x < 0) == sum.Count();
        }

        private bool IsProgressive(int[] r)
        {
            var sum = r.Zip(r.Skip(1), (x, y) => x - y).ToList();
            return sum.Count(x => Math.Abs(x) <= 3) == sum.Count();
        }


    }
}
