using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2024/day/8
    internal class Solver_8_2024 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data);
            Dictionary<char, List<(int x, int y)>> frequencies = new Dictionary<char, List<(int x, int y)>>();
            for(int y = 0; y < lines.Length; y++)
            {
                for(int x = 0; x < lines[y].Length; x++)
                {
                    if(lines[y][x] == '.')
                    {
                        continue;
                    }
                    if(!frequencies.ContainsKey(lines[y][x]))
                    {
                        frequencies[lines[y][x]] = new List<(int x, int y)>();
                    }
                    frequencies[lines[y][x]].Add((x, y));
                }
            }
            //..
            List<(int, int)> antinodes = new List<(int, int)>();
            foreach(var f in frequencies)
            {
                for(int i = 0; i < f.Value.Count -1; i++)
                {
                    for(int j = i+1; j < f.Value.Count; j++)
                    {
                        (int x, int y)[] nodes = GetAntinode(f.Value[i], f.Value[j]);
                        if(IsInMap(nodes[0].x, nodes[0].y, lines[0].Length, lines.Length))
                        {
                            antinodes.Add(nodes[0]);
                        }
                        if(IsInMap(nodes[1].x, nodes[1].y, lines[0].Length, lines.Length))
                        {
                            antinodes.Add(nodes[1]);
                        }
                    }
                }
            }

            return antinodes.Distinct().Count().ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data);
            Dictionary<char, List<(int x, int y)>> frequencies = new Dictionary<char, List<(int x, int y)>>();
            for(int y = 0; y < lines.Length; y++)
            {
                for(int x = 0; x < lines[y].Length; x++)
                {
                    if(lines[y][x] == '.')
                    {
                        continue;
                    }
                    if(!frequencies.ContainsKey(lines[y][x]))
                    {
                        frequencies[lines[y][x]] = new List<(int x, int y)>();
                    }
                    frequencies[lines[y][x]].Add((x, y));
                }
            }

            List<(int, int)> antinodes = new List<(int, int)>();
            foreach(var f in frequencies)
            {
                for(int i = 0; i < f.Value.Count - 1; i++)
                {
                    for(int j = i + 1; j < f.Value.Count; j++)
                    {
                        antinodes.AddRange(GetAllAntinodes(f.Value[i], f.Value[j], lines[0].Length, lines.Length));
                        antinodes.AddRange(GetAllAntinodes(f.Value[j], f.Value[i], lines[0].Length, lines.Length));
                    }
                }
            }

            return antinodes.Distinct().Count().ToString();
        }

        private (int, int)[] GetAntinode((int x, int y) a, (int x, int y) b)
        {
            (int x, int y) ab = (b.x - a.x, b.y - a.y);
            (int x, int y) ba = (a.x - b.x, a.y - b.y);

            (int x, int y)[] nodes = new (int x, int y)[2];
            nodes[0] = (a.x + ab.x * 2, a.y + ab.y * 2);
            nodes[1] = (b.x + ba.x * 2, b.y + ba.y * 2);

            return nodes;
        }

        private (int, int)[] GetAllAntinodes((int x, int y) a, (int x, int y) b, int width, int height)
        {
            (int x, int y) ab = (b.x - a.x, b.y - a.y);
            List<(int, int)> nodes = new List<(int, int)>();
            bool next = true;
            int i = 0;
            while(next)
            {
                i++;
                (int x, int y) n = (a.x + ab.x * i, a.y + ab.y * i);
                if(!IsInMap(n.x, n.y, width, height))
                {
                    next = false; break;
                }
                nodes.Add(n);
            }
            return nodes.ToArray();
        }

        private bool IsInMap(int x , int y, int width, int height)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }
 

    }
}
