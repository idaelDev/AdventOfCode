using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static AdventOfCode.Solver_6_2015;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/14
    internal class Solver_14_2015 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            int target = 2503;

            Parse(data, out int[] speeds, out int[] runTimes, out int[] restTimes);
            var cycles = runTimes.Zip(restTimes, (rnt, rst) => rnt + rst).ToArray();

            int max = 0;
            for(int i =0; i < speeds.Length; i++)
            {
                int cycle = runTimes[i] + restTimes[i];

                int distancePerCycle = speeds[i] * runTimes[i];
                int cycleCount = target/cycle;
                int cycleStep = target%cycle;

                int distance = cycleCount * distancePerCycle;
                if(cycleStep >= runTimes[i])
                {
                    distance += distancePerCycle;
                }
                else
                {
                    distance += speeds[i] * cycleStep;
                }
                max = Math.Max(max, distance);
            }

            return max.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            Parse(data, out int[] speeds, out int[] runTimes, out int[] restTimes);
            int[] points = new int[speeds.Length];

            for (int s = 1; s <= 2503; s++)
            {
                int[] results = new int[speeds.Length];
                for (int i = 0; i < speeds.Length; i++)
                {
                    int cycle = runTimes[i] + restTimes[i];

                    int distancePerCycle = speeds[i] * runTimes[i];
                    int cycleCount = s / cycle;
                    int cycleStep = s % cycle;

                    int distance = cycleCount * distancePerCycle;
                    if (cycleStep >= runTimes[i])
                    {
                        distance += distancePerCycle;
                    }
                    else
                    {
                        distance += speeds[i] * cycleStep;
                    }
                    results[i] = distance;
                }

                int max = results.Max();
                for (int i = 0; i < results.Length; i++)
                {
                    if (results[i] == max)
                    {
                        points[i]++;
                    }
                }
            }

            return points.Max().ToString();
        }

        public void Parse(string data, out int[] speeds, out int[] runTimes, out int[] restTimes)
        {
            string[] lines = data.Split('\n');
            speeds = new int[lines.Length];
            runTimes = new int[lines.Length];
            restTimes = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] s = lines[i].Split();
                speeds[i] = int.Parse(s[3]);
                runTimes[i] = int.Parse(s[6]);
                restTimes[i] = int.Parse(s[13]);
            }

        }

    }
}
