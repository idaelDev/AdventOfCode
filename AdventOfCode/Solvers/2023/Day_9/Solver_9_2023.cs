using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2023/day/9
    internal class Solver_9_2023 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            List<int[]> lines = Tools.LineSplit(data).Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray()).ToList();

            int sum = 0;
            foreach (var seq in lines)
            {
                List<int[]> sequences = new List<int[]>();
                sequences.Add(seq);
                List<int> s = new List<int>(seq);
                while(!s.All(i => i == 0))
                {
                    s = s.Zip(s.Skip(1), (a, b) => b - a).ToList();
                    sequences.Add(s.ToArray());
                }
                sum += GetNextValue(sequences, 0);
            }
            return sum.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            List<int[]> lines = Tools.LineSplit(data).Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray()).ToList();

            int sum = 0;
            foreach (var seq in lines)
            {
                List<int[]> sequences = new List<int[]>();
                sequences.Add(seq);
                List<int> s = new List<int>(seq);
                while (!s.All(i => i == 0))
                {
                    s = s.Zip(s.Skip(1), (a, b) => b - a).ToList();
                    sequences.Add(s.ToArray());
                }
                sum += GetPreviousValue(sequences, 0);
            }
            return sum.ToString();
        }

        public int GetNextValue(List<int[]> sequences, int index)
        {
            if(index == sequences.Count -1)
            {
                return 0;
            }
            return sequences[index][^1] + GetNextValue(sequences, index+1);
        }

        public int GetPreviousValue(List<int[]> sequences, int index)
        {
            if (index == sequences.Count - 1)
            {
                return 0;
            }
            return sequences[index][0] - GetPreviousValue(sequences, index+1);
        }

    }
}
