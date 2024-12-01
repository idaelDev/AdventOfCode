using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2024/day/1
    internal class Solver_1_2024 : ISolver
    {

        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data);
            List<int> rightList = new List<int>();
            List<int> leftList = new List<int>();
            
            foreach (string line in lines)
            {
                string[] l = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                leftList.Add(int.Parse(l[0]));
                rightList.Add(int.Parse(l[1]));
            }

            leftList.Sort();
            rightList.Sort();

            return leftList.Select((l,i) => Math.Abs(l - rightList[i])).Sum().ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data);
            List<int> rightList = new List<int>();
            List<int> leftList = new List<int>();

            foreach (string line in lines)
            {
                string[] l = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                leftList.Add(int.Parse(l[0]));
                rightList.Add(int.Parse(l[1]));
            }

            var dict = leftList.Distinct().ToDictionary(k => k, v => rightList.Count(x => x == v));

            return leftList.Select(x => dict[x] * x).Sum().ToString();
        }
    }
}
