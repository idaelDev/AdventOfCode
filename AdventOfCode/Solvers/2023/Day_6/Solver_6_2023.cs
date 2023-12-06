using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2023/day/6
    internal class Solver_6_2023 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data);
            int[] times = lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
            int[] distances = lines[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

            int result = 1;
            for (int i = 0; i < times.Length; i++)
            {
                int vCount = 0;
                for (int j = 0; j <= times[i]; j++)
                {
                    int d = j * (times[i] - j);
                    if(d > distances[i])
                    {
                        vCount++;
                    }
                }
                result *= vCount; ;
            }

            return result.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data);
            double time = double.Parse(lines[0].Split(':')[1].Trim().Replace(" ", string.Empty));
            double distance = double.Parse(lines[1].Split(':')[1].Trim().Replace(" ", string.Empty));

            double d = (time * time) - (4 * -1 * -distance);

            double x1 = (-time + Math.Sqrt(d)) / -2;
            double x2 = (-time - Math.Sqrt(d)) / -2;

            double min = Math.Floor(Math.Min(x1, x2));
            double max = Math.Floor(Math.Max(x1, x2));

            int result = (int)(max - min);

            return result.ToString();
        }
    }
}
