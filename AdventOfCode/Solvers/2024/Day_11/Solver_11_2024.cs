using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2024/day/11
    internal class Solver_11_2024 : ISolver
    {

        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            Dictionary<long, long> map = new Dictionary<long, long>();
            string[] s = data.Split(' ');
            for(int i = 0; i < s.Length; i++)
            {
                long l = long.Parse(s[i].ToString());
                map[l] = 1;
            }

            for(int i = 0; i < 25; i++)
            {
                map = Blink(map);
            }
            long result = map.Values.Sum(x => (long)x);

            return result.ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            Dictionary<long, long> map = new Dictionary<long, long>();
            string[] s = data.Split(' ');
            for(int i = 0; i < s.Length; i++)
            {
                long l = long.Parse(s[i].ToString());
                map[l] = 1;
            }

            for(int i = 0; i < 75; i++)
            {
                map = Blink(map);
                Console.WriteLine(i);
            }
            long result = map.Values.Sum(x => (long)x);

            return result.ToString();
        }

        void DrawLine(ref List<long> stones)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < stones.Count; i++)
            {
                sb.Append(stones[i] + " ");
            }
            Console.WriteLine(sb.ToString());
        }

        //int SerialBlink(int blinkmax, int blink, ref List<long> baseStones, int maxArray)
        //{
        //    int currentBlink = blink;
        //    List<long> stones = new List<long>(baseStones);
        //    while(stones.Count < maxArray && currentBlink < blinkmax)
        //    {
        //        Blink(ref stones);
        //        currentBlink++;
                
        //    }

        //    if(currentBlink == blinkmax)
        //    {
        //        return stones.Count;
        //    }

        //    List<long>[] l = { stones.Take(stones.Count / 2).ToList(), stones.Skip(stones.Count / 2).ToList()};

        //    int[] results = new int[2];
        //    Parallel.For(0, l.Length, i =>
        //    {
        //        results[i] = SerialBlink(blinkmax, currentBlink, ref l[i], maxArray);
        //    });
            
        //    return results[0] + results[1];
        //}

        Dictionary<long, long> Blink(Dictionary<long, long> map)
        {
            Dictionary<long, long> newMap = new Dictionary<long, long>();
            foreach(long key in map.Keys)
            {
                if(key == 0)
                {
                    if(!newMap.ContainsKey(1))
                    {
                        newMap[1] = 0;
                    }
                    newMap[1] += map[key];
                    continue;
                }
                string sV = key.ToString();
                if(sV.Length % 2 == 0)
                {
                    long left = long.Parse(sV.Substring(0, sV.Length / 2));
                    if(!newMap.ContainsKey(left))
                    {
                        newMap[left] = 0;
                    }
                    long right = long.Parse(sV.Substring(sV.Length / 2));
                    if(!newMap.ContainsKey(right))
                    {
                        newMap[right] = 0;
                    }
                    newMap[left] += map[key];
                    newMap[right] += map[key];
                    continue;
                }
                if(!newMap.ContainsKey(key * 2024))
                {
                    newMap[key * 2024] = 0;
                }
                newMap[key * 2024] = map[key];
            }
            return newMap;
        }
    }
}
