using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/6
    internal class Solver_6_2015 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] d = Tools.LineSplit(data);
            Dictionary<string, Func<int, int>> actions = new Dictionary<string, Func<int, int>>
            {
                {"on", (v) => 1 },
                {"off", (v) => 0 },
                {"toggle", (v) => v==0? 1 : 0 }
            };
            int[,] lights = new int[1000,1000];

            foreach (var item in d)
            {
                Data action = ParseData(item);
                for (int x = action.startX; x <= action.endX; x++)
                {
                    for (int y = action.startY; y <= action.endY; y++)
                    {
                        lights[x, y] = actions[action.action](lights[x, y]);
                    }
                }
            }
            return lights.Cast<int>().Sum().ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] d = Tools.LineSplit(data);
            Dictionary<string, Func<int, int>> actions = new Dictionary<string, Func<int, int>>
            {
                {"on", (v) => v+1 },
                {"off", (v) => v > 0 ? v-1 : 0 },
                {"toggle", (v) => v+2}
            };
            int[,] lights = new int[1000, 1000];

            foreach (var item in d)
            {
                Data action = ParseData(item);
                for (int x = action.startX; x <= action.endX; x++)
                {
                    for (int y = action.startY; y <= action.endY; y++)
                    {
                        lights[x, y] = actions[action.action](lights[x, y]);
                    }
                }
            }
            return lights.Cast<int>().Sum().ToString();
        }

        private Data ParseData(string item)
        {
            string act = "toggle";
            int c1 = 1;
            int c2 = 3;
            string[] row = item.Split(' ');
            if (row[0] == "turn")
            {
                act = row[1];
                c1 = 2;
                c2 = 4;
            }
            string[] c1Split = row[c1].Split(',');
            string[] c2Split = row[c2].Split(',');
            return new Data
            {
                action = act,
                startX = int.Parse(c1Split[0]),
                startY = int.Parse(c1Split[1]),
                endX = int.Parse(c2Split[0]),
                endY = int.Parse(c2Split[1])
            };
        }

        internal struct Data
        {
            public int startX;
            public int startY;
            public int endX;
            public int endY;
            public string action;
        }
    }
}
