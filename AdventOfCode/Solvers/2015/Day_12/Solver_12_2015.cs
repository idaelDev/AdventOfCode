using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/12
    internal class Solver_12_2015 : ISolver
    {

        string pattern = @"-?\b\d+\b";

        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            MatchCollection mc = Regex.Matches(data, pattern);

            int sum = mc.ToList().Select(m => int.Parse(m.Value)).Sum();

            return sum.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            JsonElement root = JsonDocument.Parse(data).RootElement;
            return GetSum(root).ToString();
        }

        private int GetSum(JsonElement e)
        {
            switch (e.ValueKind)
            {
                case JsonValueKind.Object:
                    JsonElement.ObjectEnumerator oe =  e.EnumerateObject();
                    if(oe.Any(j => j.Name == "red" || (j.Value.ValueKind == JsonValueKind.String && j.Value.ValueEquals("red"))))
                    {
                        return 0;
                    }
                    return oe.Select(j => GetSum(j.Value)).Sum();
                case JsonValueKind.Array:
                    JsonElement.ArrayEnumerator ae = e.EnumerateArray();
                    return ae.Select(j => GetSum(j)).Sum();
                    break;
                case JsonValueKind.Number:
                    return e.GetInt32();
                default:
                    return 0;
            }
        }

    }
}
