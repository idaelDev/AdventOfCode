using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2023/day/11
    internal class Solver_11_2023 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            List<string> lines = Tools.LineSplit(data).ToList();
            List<int> empltyLines = lines.Select((s,i) => i).Where(i => !lines[i].Contains('#')).ToList();
            List<int> emptyColumns = lines.Select((s,x) => s.Select((c, i) => i).Where(y => lines[x][y] == '.')).Aggregate((a,b) => a.Intersect(b)).ToList();

            for (int i = 0; i < emptyColumns.Count; i++)
            {
                lines = lines.Select(s => s.Insert(emptyColumns[i]+i, ".")).ToList();
            }
            string empty = lines[empltyLines[0]];
            for (int i = 0; i < empltyLines.Count; i++)
            {
                lines.Insert(empltyLines[i]+i, empty);
            }

            List<(int, int)> galaxies = lines.SelectMany((s, x) => s.Select((c, i) => i).Where(y => lines[x][y] == '#').Select((a) => (x, a))).ToList();

            List<int> distances = new List<int>();

            for (int i = 0; i < galaxies.Count-1; i++)
            {
                for (int j = i+1; j < galaxies.Count; j++)
                {
                    distances.Add(GetManhattan(galaxies[i], galaxies[j]));
                }
            }

           
            return distances.Sum().ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            List<string> lines = Tools.LineSplit(data).ToList();
            List<int> empltyLines = lines.Select((s, i) => i).Where(i => !lines[i].Contains('#')).ToList();
            List<int> emptyColumns = lines.Select((s, x) => s.Select((c, i) => i).Where(y => lines[x][y] == '.')).Aggregate((a, b) => a.Intersect(b)).ToList();
            List<(int, int)> galaxies = lines.SelectMany((s, x) => s.Select((c, i) => i).Where(y => lines[x][y] == '#').Select((a) => (x, a))).ToList();
            List<long> distances = new List<long>();
            for (int i = 0; i < galaxies.Count - 1; i++)
            {
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    distances.Add(GetManhattanExpansion(galaxies[i], galaxies[j], empltyLines, emptyColumns));
                }
            }


            return distances.Sum().ToString();
        }

        internal long GetManhattanExpansion((int, int) g1, (int, int) g2, List<int> emptyLine, List<int> emptyColumns)
        {
            int factor = 1000000;
            factor--;
            (int, int) minMaxLine = (Math.Min(g1.Item1, g2.Item1), Math.Max(g1.Item1, g2.Item1));
            (int, int) minMaxColumn = (Math.Min(g1.Item2, g2.Item2), Math.Max(g1.Item2, g2.Item2));
            int emptyLinesCount = emptyLine.Where(l => l > minMaxLine.Item1 && l < minMaxLine.Item2).Count();
            int emptyColumnsCount = emptyColumns.Where(l => l > minMaxColumn.Item1 && l < minMaxColumn.Item2).Count();

            long m = Math.Abs((minMaxLine.Item2 + (factor*emptyLinesCount)) - minMaxLine.Item1) + Math.Abs((minMaxColumn.Item2 + (factor * emptyColumnsCount)) - minMaxColumn.Item1);

            return m;
        }

        internal int GetManhattan((int,int) g1, (int, int) g2)
        {
            return Math.Abs(g1.Item1 - g2.Item1) + Math.Abs(g1.Item2 - g2.Item2);
        }

        internal void DrawLines(List<string> lines)
        {
            Console.WriteLine("Lines");
            foreach (var item in lines)
            {
                Console.WriteLine(item);
            }
        }

    }
}
