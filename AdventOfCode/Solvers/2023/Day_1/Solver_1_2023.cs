using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2023/day/1
    internal class Solver_1_2023 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string pattern = @"\d";
            return Tools.LineSplit(data)
            .Select(s => Regex.Matches(s, pattern))
            .Select(mc => (int.Parse(mc[0].Value) * 10) + int.Parse(mc[mc.Count - 1].Value))
            .Sum().ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            Dictionary<string, int> wordToNumber = new Dictionary<string, int>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 },
                { "1", 1 },
                { "2", 2 },
                { "3", 3 },
                { "4", 4 },
                { "5", 5 },
                { "6", 6 },
                { "7", 7 },
                { "8", 8 },
                { "9", 9 }
            };
            return Tools.LineSplit(data)
            .Select(s => FindMatchingWords(s, wordToNumber.Keys.ToList())).ToList()
            .Select(mc => (wordToNumber[mc[0]] * 10) + wordToNumber[mc[mc.Count - 1]]).ToList()
            .Sum().ToString();
        }

        static List<string> FindMatchingWords(string input, List<string> words)
        {
            Dictionary<int, string> matchedWords = new Dictionary<int, string>();
            string s = "";
            foreach (string word in words)
            {
                int index = 0;
                while (index != -1)
                {
                    index = input.IndexOf(word, index);
                    if (index != -1)
                    {
                        matchedWords[index] = word;
                        index++;
                    }
                }
            }
            return matchedWords.OrderBy(kv => kv.Key)
                                               .Select(kv => kv.Value)
                                               .ToList();
        }

    }
}
