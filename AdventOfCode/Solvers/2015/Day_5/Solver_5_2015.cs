using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/5
    internal class Solver_5_2015 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            //string[] d = Tools.LineSplit(data);
            //return d.Select(s => P1_Rule1(s) && P1_Rule2(s) && P1_Rule3(s)).Count(b => b==true).ToString();
            string regexPattern = "^(?=(?:.*[aeiou]){3,})(?=(?:.*)(.)\\1)(?!.*(ab|cd|pq|xy)).*$";
            string[] d = Tools.LineSplit(data);
            return d.Select(s => Regex.IsMatch(s, regexPattern)).Count(b => b == true).ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string regexPattern = "^(?=.*(.)(.).*\\1\\2)(?=.*(.).\\3).*";
            string[] d = Tools.LineSplit(data);
            //var eee = d.Select(s => Regex.IsMatch(s, regexPattern));
            return d.Select(s => Regex.IsMatch(s, regexPattern)).Count(b => b == true).ToString();

        }
    }
}
