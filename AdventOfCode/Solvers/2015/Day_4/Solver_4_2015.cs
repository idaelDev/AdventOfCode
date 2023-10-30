using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/4
    internal class Solver_4_2015 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            int v = 0;
            while(true)
            {
                string hex = Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(data+v)));
                if(hex.Substring(0,5) == "00000")
                {
                    return v.ToString();
                }
                v++;
            }
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            int v = 0;
            while (true)
            {
                string hex = Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(data + v)));
                if (hex.Substring(0, 6) == "000000")
                {
                    return v.ToString();
                }
                v++;
            }
        }
    }
}
