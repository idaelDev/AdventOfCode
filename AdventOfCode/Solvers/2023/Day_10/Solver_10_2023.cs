using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2023/day/10
    internal class Solver_10_2023 : ISolver
    {
        Dictionary<char, ConnectionMap> connections = new Dictionary<char, ConnectionMap>()
        {
            {'|', new ConnectionMap(){North = true, East = false, South = true, West = false} },
            {'-', new ConnectionMap(){North = false, East = true, South= false, West = true} },
            {'L', new ConnectionMap(){North = true, East = true, South= false, West = false} },
            {'J', new ConnectionMap(){North = true, East = false, South= false, West = true} },
            {'7', new ConnectionMap(){North = false, East = false, South= true, West = true} },
            {'F', new ConnectionMap(){North = false, East = true, South= true, West = false} },
            {'S', new ConnectionMap(){North = true, East = true, South= true, West = true} },
            {'.', new ConnectionMap(){North = false, East = false, South= false, West = false} },
        };

        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] grid = Tools.LineSplit(data);
            //Find start
            (int, int) start = grid.Where(l => l.Contains('S')).Select(l => (grid.ToList().IndexOf(l), l.IndexOf('S'))).First();
            (int, int)[] connected = GetConnected(start, grid);
            List<(int, int)> loop = new List<(int, int)>();
            loop.Add(start);
            loop.AddRange(connected);

            (int, int)[] paths  = connected;

            while(paths[0] != paths[1])
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    connected = GetConnected(paths[i], grid);
                    paths[i] = connected.First(c => !loop.Contains(c));
                }
                loop.AddRange(paths);
            }

            return ((loop.Count()-1)/2).ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] grid = Tools.LineSplit(data);
            //Find start
            (int, int) start = grid.Where(l => l.Contains('S')).Select(l => (grid.ToList().IndexOf(l), l.IndexOf('S'))).First();
            (int, int)[] connected = GetConnected(start, grid);
            List<(int, int)> loop = new List<(int, int)>();
            loop.Add(start);
            (int, int) current = start;

            while (true)
            {
                var buf = GetConnected(current, grid).Where(p => !loop.Contains(p));
                if (!buf.Any())
                    break;
                current = buf.First();
                loop.Add(current);
            }

            loop = loop.Distinct().ToList();
            List<(int, int)> polygon = new List<(int, int)>();
            int index = 1;
            polygon.Add(loop[0]);
            //Create polygon
            while (index < loop.Count -1)
            {
                if (loop[index].Item2 == polygon.Last().Item2)
                {
                    while(loop[index].Item2 == polygon.Last().Item2 && index < loop.Count -1)
                    {
                        index++;
                    }
                    polygon.Add(loop[index - 1]);
                }
                else if (loop[index].Item1 == polygon.Last().Item1)
                {
                    while (loop[index].Item1 == polygon.Last().Item1 && index < loop.Count - 1)
                    {
                        index++;
                    }
                    polygon.Add(loop[index - 1]);
                }
            }

            
            //GetBounds
            (int, int) minBounds = (polygon.MinBy(x => x.Item1).Item1, polygon.MinBy(x => x.Item2).Item2);
            (int, int) maxBounds = (polygon.MaxBy(x => x.Item1).Item1, polygon.MaxBy(x => x.Item2).Item2);

            //raycast all area
            int count = 0;
            for (int l = minBounds.Item1; l <= maxBounds.Item1; l++)
            {
                for (int c = minBounds.Item2; c <= maxBounds.Item2; c++)
                {
                    if (!loop.Contains((l,c)))
                    {
                        if(IsPointInsidePolygon((l,c), polygon))
                        {
                            count++;
                        }
                    }
                }
            }

            return count.ToString();
        }

        internal (int, int)[] GetConnected((int, int) p, string[] grid)
        {
            List<(int, int)> connected = new List<(int, int)>();
            char current = grid[p.Item1][p.Item2];
            if(p.Item1 > 0 && connections[current].North && connections[grid[p.Item1 - 1][p.Item2]].South)
            {
                connected.Add((p.Item1 - 1, p.Item2));
            }
            if(p.Item1 < grid.Length -1 && connections[current].South && connections[grid[p.Item1 + 1][p.Item2]].North) 
            {
               connected.Add((p.Item1 + 1, p.Item2));
            }
            if (p.Item2 > 0 && connections[current].West && connections[grid[p.Item1][p.Item2 -1]].East)
            {
                connected.Add((p.Item1, p.Item2-1));
            }
            if (p.Item2 < grid[p.Item1].Length-1 && connections[current].East && connections[grid[p.Item1][p.Item2 +1]].West)
            {
                connected.Add((p.Item1, p.Item2 + 1));
            }
            return connected.ToArray();
        }

        internal bool IsPointInsidePolygon((int, int) point, List<(int, int)> polygon)
        {
            int count = 0;
            int n = polygon.Count;

            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                if (((polygon[i].Item2 <= point.Item2 && point.Item2 < polygon[j].Item2) || (polygon[j].Item2 <= point.Item2 && point.Item2 < polygon[i].Item2)))
                {
                    if ((point.Item1 < (polygon[j].Item1 - polygon[i].Item1) * (point.Item2 - polygon[i].Item2) / (polygon[j].Item2 - polygon[i].Item2) + polygon[i].Item1))
                    {
                        count++;
                    }
                }
            }

            return count % 2 == 1;
        }

        internal struct ConnectionMap
        {
            public bool North;
            public bool East;
            public bool South;
            public bool West;

        }

    }
}
