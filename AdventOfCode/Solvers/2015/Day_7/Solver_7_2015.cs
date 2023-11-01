using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/7
    internal class Solver_7_2015 : ISolver
    {
        internal static Dictionary<string, ushort> wires = new Dictionary<string, ushort>();
        internal static Dictionary<string, Gate> gates = new Dictionary<string, Gate>();
        static string numberRegex = @"^\d+$";
        internal struct Gate
        {
            public string key;
            public string action;
            
            public ushort Solve()
            {
                if (!wires.ContainsKey(key))
                {
                    string[] s = action.Split(' ');
                    if(action.Contains("AND"))
                    {
                        wires[key] = (ushort)((Regex.IsMatch(s[0], numberRegex) ? ushort.Parse(s[0]) : gates[s[0]].Solve()) 
                            & (Regex.IsMatch(s[2], numberRegex) ? ushort.Parse(s[2]) : gates[s[2]].Solve()));
                    }
                    else if (action.Contains("OR"))
                    {
                        wires[key] = (ushort)((Regex.IsMatch(s[0], numberRegex) ? ushort.Parse(s[0]) : gates[s[0]].Solve())
                            | (Regex.IsMatch(s[2], numberRegex) ? ushort.Parse(s[2]) : gates[s[2]].Solve()));
                    }
                    else if (action.Contains("LSHIFT"))
                    {
                        wires[key] = (ushort)((Regex.IsMatch(s[0], numberRegex) ? ushort.Parse(s[0]) : gates[s[0]].Solve())
                            << (Regex.IsMatch(s[2], numberRegex) ? ushort.Parse(s[2]) : gates[s[2]].Solve()));
                    }
                    else if (action.Contains("RSHIFT"))
                    {
                        wires[key] = (ushort)((Regex.IsMatch(s[0], numberRegex) ? ushort.Parse(s[0]) : gates[s[0]].Solve())
                            >> (Regex.IsMatch(s[2], numberRegex) ? ushort.Parse(s[2]) : gates[s[2]].Solve()));
                    }
                    else if (action.Contains("NOT"))
                    {
                        wires[key] = (ushort)~(Regex.IsMatch(s[1], numberRegex) ? ushort.Parse(s[1]) : gates[s[1]].Solve());
                    }
                    else
                    {
                        wires[key] = Regex.IsMatch(s[0], numberRegex) ? ushort.Parse(s[0]) : gates[s[0]].Solve();
                    }
                }
                return wires[key];
            }
        }

        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] sData = Tools.LineSplit(data);
            foreach (string s in sData)
            {
                string[] row = s.Split("->");

                gates[row[1].Trim()] = new Gate
                {
                    key = row[1].Trim(),
                    action = row[0].Trim() 
                };
            }

            int i = 1;
            int count = gates.Count;
            foreach (var item in gates.Keys)
            {
                Console.WriteLine("Solving item "+i+"/"+count+": " + item);
                wires[item] = gates[item].Solve();
                i++;
            }
            return wires["a"].ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] sData = Tools.LineSplit(data);
            foreach (string s in sData)
            {
                string[] row = s.Split("->");

                gates[row[1].Trim()] = new Gate
                {
                    key = row[1].Trim(),
                    action = row[0].Trim()
                };
            }

            int i = 1;
            int count = gates.Count;
            foreach (var item in gates.Keys)
            {
                Console.WriteLine("Solving item " + i + "/" + count + ": " + item);
                wires[item] = gates[item].Solve();
                i++;
            }

            ushort a = wires["a"];
            wires.Clear();
            wires["b"] = a;
            foreach (var item in gates.Keys)
            {
                Console.WriteLine("Solving item " + i + "/" + count + ": " + item);
                wires[item] = gates[item].Solve();
                i++;
            }
            return wires["a"].ToString();
        }
    }
}
