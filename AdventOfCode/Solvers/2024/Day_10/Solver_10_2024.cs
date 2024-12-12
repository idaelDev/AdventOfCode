using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2024/day/10
    internal class Solver_10_2024 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data);
            int[][] map = new int[lines.Length][];
            List<(int x, int y)> startPoints = new List<(int x, int y)>();
            for(int y = 0; y < lines.Length; y++)
            {
                map[y] = lines[y].Select(c => int.Parse(c.ToString())).ToArray();
                startPoints.AddRange(map[y].Select((v, x) => (v, x)).Where(t => t.v == 0).Select(t => (t.x, y)));
            }

            List<int> scores = new List<int>();
            foreach(var item in startPoints)
            {
                scores.Add(GetPathScore(item.x, item.y, -1, ref map).Count);
            }

            return scores.Sum().ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.LineSplit(data);
            int[][] map = new int[lines.Length][];
            List<(int x, int y)> startPoints = new List<(int x, int y)>();
            for(int y = 0; y < lines.Length; y++)
            {
                map[y] = lines[y].Select(c => int.Parse(c.ToString())).ToArray();
                startPoints.AddRange(map[y].Select((v, x) => (v, x)).Where(t => t.v == 0).Select(t => (t.x, y)));
            }

            List<int> scores = new List<int>();
            foreach(var item in startPoints)
            {
                scores.Add(GetRating(item.x, item.y, -1, ref map));
            }

            return scores.Sum().ToString();
        }

        List<(int, int)> GetPathScore(int x, int y, int index, ref int[][] map)
        {
            List<(int, int)> result = new List<(int, int)>();
            if(x < 0 || x >= map[0].Length || y < 0 || y >= map.Length)
            {
                return result;
            }
            int currentIndex = map[y][x];
            if(currentIndex != index+1)
            {
                return result;
            }

            if(map[y][x] == 9)
            {
                result.Add((x, y));
                return result;
            }

            result.AddRange(GetPathScore(x + 1, y, currentIndex, ref map));
            result.AddRange(GetPathScore(x - 1, y, currentIndex, ref map));
            result.AddRange(GetPathScore(x, y + 1, currentIndex, ref map));
            result.AddRange(GetPathScore(x, y - 1, currentIndex, ref map));

            return result.Distinct().ToList();
        }

        int GetRating(int x, int y, int index, ref int[][] map)
        {
            if(x < 0 || x >= map[0].Length || y < 0 || y >= map.Length)
            {
                return 0;
            }
            int currentIndex = map[y][x];
            if(currentIndex != index + 1)
            {
                return 0;
            }

            if(map[y][x] == 9)
            {
                return 1;
            }

            int score = 0;
            score += GetRating(x + 1, y, currentIndex, ref map);
            score += GetRating(x - 1, y, currentIndex, ref map);
            score += GetRating(x, y + 1, currentIndex, ref map);
            score += GetRating(x, y - 1, currentIndex, ref map);

            return score;
        }
    }
}
