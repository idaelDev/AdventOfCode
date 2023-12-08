using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2023/day/8
    internal class Solver_8_2023 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data);
            string directions = lines[0];

            Dictionary<string, (string, string)> nodes = new Dictionary<string, (string, string)>();
            for (int i = 1; i < lines.Length; i++)
            {
                string[] l = lines[i].Split('=');
                nodes[l[0].Trim()] = (l[1].Trim().Split(',')[0].Trim('('), l[1].Trim().Split(',')[1].Trim().Trim(')'));
            }

            string currentStep = "AAA";
            int count = 0;
            while(currentStep != "ZZZ")
            {
                char d = directions[count % directions.Length];
                currentStep = d == 'L' ? nodes[currentStep].Item1 : nodes[currentStep].Item2;
                count++;
            }
            
            return count.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data);
            string directions = lines[0];

            Dictionary<string, (string, string)> nodes = new Dictionary<string, (string, string)>();
            for (int i = 1; i < lines.Length; i++)
            {
                string[] l = lines[i].Split('=');
                nodes[l[0].Trim()] = (l[1].Trim().Split(',')[0].Trim('('), l[1].Trim().Split(',')[1].Trim().Trim(')'));
            }

            string[] currentNodes = nodes.Keys.Where(s => s[2] == 'A').ToArray();
            long[] counters = new long[currentNodes.Length];
            long[] cycles = new long[currentNodes.Length];

            for (int i = 0; i < currentNodes.Length; i++)
            {
                string currentStep = currentNodes[i];
                long count = 0;
                while (currentStep[2] != 'Z')
                {
                    char d = directions[(int)(count % directions.Length)];
                    currentStep = d == 'L' ? nodes[currentStep].Item1 : nodes[currentStep].Item2;
                    count++;
                }
                currentNodes[i] = currentStep;
                cycles[i] = count;
            }

            long ppcm = cycles[0];
            for (int i = 1; i < cycles.Length; i++)
            {  
                ppcm = (ppcm * cycles[i]) / GetPGCD(ppcm, cycles[i]);
            }

            return ppcm.ToString();
        }

        static long GetPGCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

    }

}
