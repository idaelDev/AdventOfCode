using AdventOfCode.API;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2023/day/3
    internal class Solver_3_2023 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string numberPattern = @"\b(-?\d+)\b";
            string symbolPattern = @"[^0-9.]";
            string[] s = Tools.LineSplit(data);
            List<MatchCollection> numbers = s.Select(l => Regex.Matches(l, numberPattern)).ToList();
            List<MatchCollection> symbols = s.Select(l => Regex.Matches(l, symbolPattern)).ToList();

            int sum = 0;

            for (int i = 0; i < s.Length; i++)
            {
                foreach (Match n in numbers[i])
                {
                    if (symbols[i].Any() && symbols[i].Any(m => m.Index == n.Index - 1 || m.Index == n.Index + n.Length))
                    {
                        sum += int.Parse(n.Value);
                        continue;
                    }

                    for (int j = n.Index-1; j <= n.Index+n.Length; j++)
                    {
                        if (i > 0 && symbols[i-1].Any() && symbols[i-1].Any(m => m.Index == j))
                        {
                            sum += int.Parse(n.Value);
                            break;
                        }
                        if (i < s.Length-1 && symbols[i + 1].Any() && symbols[i + 1].Any(m => m.Index == j))
                        {
                            sum += int.Parse(n.Value);
                            break;
                        }
                    }

                }
            }

            return sum.ToString();
        }

        Dictionary<(int, int), List<int>> gears;

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string numberPattern = @"\b(-?\d+)\b";
            string symbolPattern = @"\*";
            string[] s = Tools.LineSplit(data);
            List<MatchCollection> numbers = s.Select(l => Regex.Matches(l, numberPattern)).ToList();
            List<MatchCollection> symbols = s.Select(l => Regex.Matches(l, symbolPattern)).ToList();
            gears = new Dictionary<(int, int), List<int>>();
            for (int i = 0; i < s.Length; i++)
            {
                foreach (Match n in numbers[i])
                {
                    if (symbols[i].Any())
                    {
                        if(symbols[i].Any(m => m.Index == n.Index - 1))
                        {
                            AddValue(i, symbols[i].First(m => m.Index == n.Index - 1).Index, int.Parse(n.Value));
                        }
                        if (symbols[i].Any(m => m.Index == n.Index + n.Length))
                        {
                            AddValue(i, symbols[i].First(m => m.Index == n.Index + n.Length).Index, int.Parse(n.Value));
                        }
                    }

                    for (int j = n.Index - 1; j <= n.Index + n.Length; j++)
                    {
                        if (i > 0 && symbols[i - 1].Any() && symbols[i - 1].Any(m => m.Index == j))
                        {
                            AddValue(i-1, symbols[i - 1].First(m => m.Index == j).Index, int.Parse(n.Value));
                        }
                        if (i < s.Length - 1 && symbols[i + 1].Any() && symbols[i + 1].Any(m => m.Index == j))
                        {
                            AddValue(i+1, symbols[i + 1].First(m => m.Index == j).Index, int.Parse(n.Value));
                        }
                    }
                }
            }

            return gears.Where(kvp => kvp.Value.Count == 2).SelectMany(kvp => kvp.Value.Zip(kvp.Value.Skip(1), (a, b) => a * b)).Sum().ToString();
        }

        private void AddValue(int line, int gearIndex, int number)
        {
            if (!gears.ContainsKey((line, gearIndex)))
            {
                gears[(line, gearIndex)] = new List<int>();
            }
            gears[(line, gearIndex)].Add(number);
        }
    }
}