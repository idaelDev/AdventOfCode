using AdventOfCode.API;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/13
    internal class Solver_13_2015 : ISolver
    {
        private static Dictionary<Permutation, int> happinessCache = new Dictionary<Permutation, int>();
        private static Dictionary<string, Dictionary<string, int>> matrix;

        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            happinessCache.Clear();
            matrix = ParseData(data);

            string[] s = matrix.Keys.ToArray();
            var l = GetAllPermutation(ref s, 0, matrix.Count - 1, new List<Permutation>());


            int max = l.Select(x => GetPermutationTotalHappiness(x)).Max();

            return max.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            happinessCache.Clear();
            matrix = ParseData(data);

            string[] s = matrix.Keys.ToArray();
            matrix["moi"] = new Dictionary<string, int>();

            foreach (var item in s)
            {
                matrix[item]["moi"] = 0;
                matrix["moi"][item] = 0;
            }

            s = matrix.Keys.ToArray();
            var l = GetAllPermutation(ref s, 0, matrix.Count - 1, new List<Permutation>());


            int max = l.Select(x => GetPermutationTotalHappiness(x)).Max();

            return max.ToString();
        }


        public Dictionary<string, Dictionary<string, int>> ParseData(string data)
        {
            Dictionary<string, Dictionary<string, int>> matrix = new Dictionary<string, Dictionary<string, int>>();
            string[] lines = Tools.LineSplit(data);

            foreach (var item in lines)
            {
                string[] s = item.TrimEnd('.').Split();
                if (!matrix.ContainsKey(s[0]))
                {
                    matrix[s[0]] = new Dictionary<string, int>();
                }
                matrix[s[0]][s[10]] = int.Parse(s[3]) * (s[2] == "gain" ? 1 : -1);
            }

            return matrix;
        }

        public List<Permutation> GetAllPermutation(ref string[] init, int start, int end, List<Permutation> list)
        {
            if (start == end)
            {
                // We have one of our possible n! solutions,
                // add it to the list.
                list.Add(new Permutation()
                {
                    table = init.ToArray()
                });
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    Swap(ref init[start], ref init[i]);
                    GetAllPermutation(ref init, start + 1, end, list);
                    Swap(ref init[start], ref init[i]);
                }
            }

            return list;
        }

        static void Swap(ref string a, ref string b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        public int GetPermutationTotalHappiness(Permutation p)
        {
            if (happinessCache.TryGetValue(p, out int h))
            {
                return h;
            }

            int hap = 0;
            for (int i = 0; i < p.table.Length; i++)
            {
                hap += matrix[p.table[i]][i > 0 ? p.table[i - 1] : p.table[^1]];
                hap += matrix[p.table[i]][i < p.table.Length - 1 ? p.table[i + 1] : p.table[0]];
            }

            happinessCache[p] = hap;
            return hap;

        }

        public struct Permutation
        {
            public string[] table;

            public override string ToString()
            {
                string s = "{";
                for (int i = 0; i < table.Length; i++)
                {
                    if(i>0)
                    {
                        s += ", ";
                    }
                    s += table[i];
                }
                s += "}";
                return s;
            }
        }
    }
}