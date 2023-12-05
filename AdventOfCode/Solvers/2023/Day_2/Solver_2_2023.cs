using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2023/day/2
    internal class Solver_2_2023 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            var games = Parse(data);
            Vector3 maxes = new Vector3(12, 13, 14);
            int result = 0;
            for (int i = 0; i < games.Count; i++)
            {
                bool possible = true;
                foreach (var item in games[i])
                {
                    if(item.X > maxes.X || item.Y > maxes.Y || item.Z > maxes.Z)
                    {
                        possible = false;
                    }
                }
                result += possible ? i + 1 : 0;
            }
            return result.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            var games = Parse(data);
            return games.Select(xTab => (int)(xTab.MaxBy(v => v.X).X * xTab.MaxBy(v => v.Y).Y * xTab.MaxBy(v => v.Z).Z)).Sum().ToString();
        }

        private List<List<Vector3>> Parse(string data)
        {
            string[] lines = Tools.LineSplit(data).Select(s => s.Split(':')[1]).ToArray();
            List<List<Vector3>> games = new List<List<Vector3>>();
            for(int i = 0; i<lines.Length; i++)
            {
                games.Add(new List<Vector3>());
                string[] subs = lines[i].Split(';');
                foreach (string sub in subs)
                {
                    string[] vs = sub.Trim().Split(',');
                    Vector3 v = new Vector3();
                    foreach (string item in vs)
                    {
                        string[] x = item.Trim().Split();
                        if (x[1] == "red")
                        {
                            v.X = int.Parse(x[0]);
                        }
                        if (x[1] == "green")
                        {
                            v.Y = int.Parse(x[0]);
                        }
                        if (x[1] == "blue")
                        {
                            v.Z = int.Parse(x[0]);
                        }
                    }
                    games[i].Add(v);
                }
            }
            return games;
        }

    }
}
