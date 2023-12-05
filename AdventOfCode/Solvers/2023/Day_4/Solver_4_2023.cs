using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2023/day/4
    internal class Solver_4_2023 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data).Select(s => s.Split(':')[1]).ToArray();
            int total = 0;
            foreach (string line in lines)
            {
                string[] s = line.Trim().Split('|');
                int[] winning = s[0].Trim().Split().Where(s => !string.IsNullOrEmpty(s)).Select(s => int.Parse(s.Trim())).ToArray();
                int[] nyh = s[1].Trim().Split().Where(s => !string.IsNullOrEmpty(s)).Select(s => int.Parse(s.Trim())).ToArray();

                int score = 0;
                foreach (int n in nyh)
                {
                    if(winning.Contains(n))
                    {
                        score = score == 0 ? 1 : score * 2;
                    }
                }
                total += score;
            }


            return total.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data).Select(s => s.Split(':')[1]).ToArray();
            Dictionary<int, int> cardsValues = new Dictionary<int, int>(lines.Length);
            List<int> cardsCopy = new List<int>(lines.Length);
            for(int i= 0; i < lines.Length; i++)
            {
                string[] s = lines[i].Trim().Split('|');
                int[] winning = s[0].Trim().Split().Where(s => !string.IsNullOrEmpty(s)).Select(s => int.Parse(s.Trim())).ToArray();
                int[] nyh = s[1].Trim().Split().Where(s => !string.IsNullOrEmpty(s)).Select(s => int.Parse(s.Trim())).ToArray();

                int score = 0;
                foreach (int n in nyh)
                {
                    score += winning.Contains(n) ? 1 : 0;
                }
                cardsValues[i] = score;
                cardsCopy.Add(1);
            }

            for (int i = 0; i < cardsCopy.Count; i++)
            {
                for (int j = i+1; j <= i+cardsValues[i]; j++)
                {
                    cardsCopy[j] += cardsCopy[i];
                }
            }

            return cardsCopy.Sum().ToString();
        }
    }
}
