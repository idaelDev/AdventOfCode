using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2024/day/6
    internal class Solver_6_2024 : ISolver
    {

        private static Coord[] s_directions = new Coord[4]
        {
            new Coord() { X = 0, Y = -1},
            new Coord(){X = 1, Y = 0},
            new Coord(){X = 0, Y = 1},
            new Coord(){X = -1, Y = -0}
        };

        private static bool[][] _map;
        private static Coord _startPosition;


        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            LoadMap(data);
            DrawMap(_map, new Coord[1] { _startPosition });

            Coord[] path = GetPath(_map);
            return (path.Select(c => (c.X, c.Y)).Distinct().Count() - 1).ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            LoadMap(data);
            Coord[] path = GetPath(_map);
            object sync = new object();
            List<Coord> obstaclePosition = new List<Coord>() ;
            int execution = 0;
            //Parallel.For(1, path.Length - 1, i =>
            for(int i = 1; i < path.Length-1; i++)
            {
                bool[][] map = GetMap();
                map[path[i].Y][path[i].X] = true;
                Coord[] tempPath = GetPath(map);
                //DrawMap(map, tempPath);
                if(tempPath.Count(c => c.Equals(tempPath[tempPath.Length - 1])) > 1)
                {
                    //lock(sync)
                    {
                        obstaclePosition.Add(path[i]);
                    }
                }
                //lock(sync)
                {
                    execution++;
                    Console.WriteLine(execution + "/"+path.Length);
                }
            }; 
            //);


            return obstaclePosition.Select(c => (c.X, c.Y)).Distinct().Count().ToString();
        }

        private Coord[] GetPath(bool[][] map)
        {
            Coord current = _startPosition;
            List<Coord> path = new List<Coord>();
            path.Add(current);

            bool endPath = false;
            while(!endPath)
            {
                current = GetNext(map, current);
                path.Add(current);
                if(!IsInMap(map, current))
                {
                    endPath = true;
                }
                if(path.Count(c => c.Equals(current)) > 1)
                {
                    endPath = true;
                }
                //DrawMap(map, path.ToArray());
            }
            return path.ToArray();
        }

        private Coord GetNext(bool[][] map, Coord position)
        {
            Coord p = position;

            for(int i = 0; i < 4; i++)
            {
                Coord next = p.Next();
                if(!IsInMap(map, next))
                {
                    return next;
                }
                if(!map[next.Y][next.X])
                {
                    return next;
                }
                p.direction++;
                p.direction = p.direction % 4;
            }
            return p.Next();
        }

        bool IsInMap(bool[][] map, Coord c)
        {
            return c.X >= 0 && c.X < map[0].Length && c.Y >= 0 && c.Y < map.Length;
        }

        private void LoadMap(string data)
        {
            string[] lines = Tools.LineSplit(data);
            _startPosition = new Coord() { X = -1, Y = -1 };
            _map = new bool[lines.Length][];
            for(int y = 0; y < lines.Length; y++)
            {
                _map[y] = new bool[lines[y].Length];
                for(int x = 0; x < lines[y].Length; x++)
                {
                    if(lines[y][x] == '^')
                    {
                        _startPosition.X = x;
                        _startPosition.Y = y;
                        _startPosition.direction = 0;
                    }
                    else
                    {
                        _map[y][x] = lines[y][x] == '#';
                    }

                }
            }
        }

        private bool[][] GetMap()
        {
            bool[][] map = new bool[_map.Length][];
            for(int i = 0; i < map.Length; i++)
            {
                map[i] = new bool[_map[i].Length];
                for(int j = 0; j < _map[i].Length; j++)
                {
                    map[i][j] = _map[i][j];
                }
            }
            return map;
        }

        private void DrawMap(bool[][] map, Coord[] path)
        {
            StringBuilder sb = new StringBuilder();
            for(int y = 0; y < map.Length; y++)
            {
                for(int x = 0; x < map[y].Length; x++)
                {
                    if(map[y][x])
                    {
                        sb.Append("#");
                        continue;
                    }
                    if(path.Any(c => c.X == x && c.Y == y))
                    {
                        sb.Append("+");
                        continue;
                    }
                    sb.Append(".");
                }
                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());
        }

        internal Coord[] GetCoordInPath(int x, int y, Coord[] coord)
        {
            return coord.Where(c => c.X == x && c.Y == y).ToArray();
        }

        internal struct Coord
        {
            internal int X;
            internal int Y;
            internal int direction;

            internal Coord Next()
            {
                Coord d = s_directions[direction];
                int x = X + d.X;
                int y = Y + d.Y;
                int dir = direction;
                return new Coord()
                {
                    X = x,
                    Y = y,
                    direction = dir
                };
            }

            public override bool Equals(object? obj)
            {
                return obj is Coord coord &&
                       X == coord.X &&
                       Y == coord.Y &&
                       direction == coord.direction;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(X, Y, direction);
            }
        }
    }
}
