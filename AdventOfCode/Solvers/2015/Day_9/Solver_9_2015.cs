using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/9
    internal class Solver_9_2015 : ISolver
    {
        List<string> m_cities;
        int[,] m_distances;
        Dictionary<int[], int> m_paths;

        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] input = Tools.LineSplit(data);
            m_paths = new Dictionary<int[], int>();
            m_cities  = new List<string>();

            //Add cities
            foreach (var item in input)
            {
                string[] line = item.Split();
                if (!m_cities.Contains(line[0]))
                {
                    m_cities.Add(line[0]);
                }
                if (!m_cities.Contains(line[2]))
                {
                    m_cities.Add(line[2]);
                }
            }

            m_distances = CreateDistanceMatrix(input, m_cities);

            int[] remaining = new int[m_cities.Count];
            for (int i = 0; i < m_cities.Count; i++)
            {
                remaining[i] = i;
            }

            List<int> results = new List<int>();

            for (int i = 0; i < m_cities.Count; i++)
            {
                List<int> r = new List<int>(remaining);
                r.Remove(i);
                results.Add(GetMinCost(new int[] { i }, r));
            }

            return results.Min().ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] input = Tools.LineSplit(data);
            m_paths = new Dictionary<int[], int>();
            m_cities = new List<string>();

            //Add cities
            foreach (var item in input)
            {
                string[] line = item.Split();
                if (!m_cities.Contains(line[0]))
                {
                    m_cities.Add(line[0]);
                }
                if (!m_cities.Contains(line[2]))
                {
                    m_cities.Add(line[2]);
                }
            }

            m_distances = CreateDistanceMatrix(input, m_cities);

            int[] remaining = new int[m_cities.Count];
            for (int i = 0; i < m_cities.Count; i++)
            {
                remaining[i] = i;
            }

            List<int> results = new List<int>();

            for (int i = 0; i < m_cities.Count; i++)
            {
                List<int> r = new List<int>(remaining);
                r.Remove(i);
                results.Add(GetMaxCost(new int[] { i }, r));
            }

            return results.Max().ToString();
        }
        
        private int GetMinCost(int[] path, List<int> remaining)
        {
            if(m_paths.ContainsKey(path))
            {
                return m_paths[path];
            }
            int cost = 0;
            if(remaining.Count == 1)
            {
                cost = m_distances[path[path.Length - 1], remaining[0]];
                m_paths[path] = cost;
                return cost;
            }

            int next = 0;
            int min = int.MaxValue;
            for (int i = 0; i < remaining.Count; i++)
            {
                if(m_distances[path[path.Length - 1], remaining[i]] < min)
                {
                    min = m_distances[path[path.Length - 1], remaining[i]];
                    next = remaining[i];
                }
            }
            var newPath = path.ToList();
            newPath.Add(next);
            remaining.Remove(next);
            cost = min + GetMinCost(newPath.ToArray(), remaining);
            m_paths[path] = cost;
            return cost;

        }

        private int GetMaxCost(int[] path, List<int> remaining)
        {
            if (m_paths.ContainsKey(path))
            {
                return m_paths[path];
            }
            int cost = 0;
            if (remaining.Count == 1)
            {
                cost = m_distances[path[path.Length - 1], remaining[0]];
                m_paths[path] = cost;
                return cost;
            }

            int next = 0;
            int max = int.MinValue;
            for (int i = 0; i < remaining.Count; i++)
            {
                if (m_distances[path[path.Length - 1], remaining[i]] > max)
                {
                    max = m_distances[path[path.Length - 1], remaining[i]];
                    next = remaining[i];
                }
            }
            var newPath = path.ToList();
            newPath.Add(next);
            remaining.Remove(next);
            cost = max + GetMaxCost(newPath.ToArray(), remaining);
            m_paths[path] = cost;
            return cost;

        }

        private int[,] CreateDistanceMatrix(string[] input, List<string> cities)
        {
            int[,] distances = new int[cities.Count, cities.Count];

            //Get distance matrix
            foreach (var item in input)
            {
                string[] line = item.Split();
                distances[cities.IndexOf(line[0]), cities.IndexOf(line[2])] = int.Parse(line[4]);
                distances[cities.IndexOf(line[2]), cities.IndexOf(line[0])] = int.Parse(line[4]);
            }
            return distances;
        }

    }
}
