using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/8
    internal class Solver_8_2015 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] input = Tools.LineSplit(data);
            int memoryCount = 0;
            int litteralCount = 0;
            foreach (var item in input)
            {
                memoryCount += item.Length;
                litteralCount += item.Length - 2;
                string s = item.Substring(1,item.Length - 2);
                s = s.Replace('\\', '/');
                MatchCollection mc1 = Regex.Matches(s, @"//");
                MatchCollection mc2 = Regex.Matches(s, "/\"");
                MatchCollection mc3 = Regex.Matches(s, "/x[0-9a-fA-F]{2}");

                int mc3Count = mc3.Where(m => !((m.Index == 1 && s[0] == '/') || (m.Index > 1 && s[m.Index - 1] == '/' && s[m.Index - 2] != '/'))).Count();
                

                litteralCount -= mc1.Count + mc2.Count + (mc3Count *3);
            }
            return (memoryCount - litteralCount).ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] input = Tools.LineSplit(data);
            int memoryCount = 0;
            int encodedCount = 0; 
            foreach (var item in input)
            {
                memoryCount += item.Length;
                encodedCount += item.Length + 4;
                string s = item.Substring(1, item.Length - 2);
                s = s.Replace('\\', '/');
                MatchCollection mc1 = Regex.Matches(s, @"//");
                encodedCount += mc1.Count * 2;
                MatchCollection mc2 = Regex.Matches(s, "/\"");
                encodedCount += mc2.Count * 2;
                MatchCollection mc3 = Regex.Matches(s, "/x[0-9a-fA-F]{2}");
                int mc3Count = mc3.Where(m => !((m.Index == 1 && s[0] == '/') || (m.Index > 1 && s[m.Index - 1] == '/' && s[m.Index - 2] != '/'))).Count();
                encodedCount += mc3Count;
            }
            return (encodedCount - memoryCount).ToString();
        }
    }
}
