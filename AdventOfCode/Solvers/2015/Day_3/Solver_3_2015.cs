using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2015/day/3
    internal class Solver_3_2015 : ISolver
    {
        Dictionary<char, Vector2> directions = new Dictionary<char, Vector2>
            {
                {'>', new Vector2(1,0) },
                {'<', new Vector2(-1,0) },
                {'v', new Vector2(0,-1) },
                {'^', new Vector2(0,1) },
            };

        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            HashSet<Vector2> pos =  new HashSet<Vector2>();
            Vector2 p = new Vector2(0, 0);
            pos.Add(p);
            data.ToList().Select(c => directions[c]).ToList().ForEach(v =>
            {
                p += v;
                pos.Add(p);
            });

            return pos.Count().ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            HashSet<Vector2> pos = new HashSet<Vector2>();
            Vector2 p1 = new Vector2(0, 0);
            Vector2 p2 = new Vector2(0, 0);
            pos.Add(p1);
            var v = data.ToList().Select(c => directions[c]).ToList();
            for (int i = 0; i < v.Count; i++)
            {
                if(i%2==0)
                {
                    p1 += v[i];
                    pos.Add(p1);
                }
                else
                {
                    p2+= v[i];
                    pos.Add(p2);
                }
            }

            return pos.Count().ToString();
        }

    }
}
