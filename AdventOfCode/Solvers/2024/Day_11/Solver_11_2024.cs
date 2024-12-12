using AdventOfCode.API;
using System;
using System.Collections.Generic;
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
            List<ulong> stones = data.Split(' ').Select(x => ulong.Parse(x)).ToList();

            Console.WriteLine("Initial arrangement:");
            DrawLine(ref stones);

            //for(int i = 0; i < 25; i++)
            //{
            //    Blink(ref stones);
            //    //Console.WriteLine("After " + (i + 1) + " blink");
            //    //DrawLine(ref stones);
            //}

            return SerialBlink(25, 0, ref stones, 100000).ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            List<ulong> stones = data.Split(' ').Select(x => ulong.Parse(x)).ToList();

            Console.WriteLine("Initial arrangement:");
            DrawLine(ref stones);

            //for(int i = 0; i < 75; i++)
            //{
            //    Blink(ref stones);
            //    Console.WriteLine("After " + (i + 1) + " blink");
            //    //DrawLine(ref stones);
            //}

            //return stones.Count.ToString();
            return SerialBlink(75, 0, ref stones, 100000).ToString();
        }

        void DrawLine(ref List<ulong> stones)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < stones.Count; i++)
            {
                sb.Append(stones[i] + " ");
            }
            Console.WriteLine(sb.ToString());
        }

        int SerialBlink(int blinkmax, int blink, ref List<ulong> baseStones, int maxArray)
        {
            int currentBlink = blink;
            List<ulong> stones = new List<ulong>(baseStones);
            while(stones.Count < maxArray && currentBlink < blinkmax)
            {
                Blink(ref stones);
                currentBlink++;
                
            }

            if(currentBlink == blinkmax)
            {
                return stones.Count;
            }

            List<ulong>[] l = { stones.Take(stones.Count / 2).ToList(), stones.Skip(stones.Count / 2).ToList()};

            int[] results = new int[2];
            Parallel.For(0, l.Length, i =>
            {
                results[i] = SerialBlink(blinkmax, currentBlink, ref l[i], maxArray);
            });
            
            return results[0] + results[1];
        }

        void Blink(ref List<ulong> stones)
        {
            int c = stones.Count;
            for(int i = 0; i < c; i++)
            {
                if(stones[i] == 0)
                {
                    stones[i] = 1;
                    continue;
                }
                string sV = stones[i].ToString();
                if(sV.Length % 2 == 0)
                {
                    stones[i] = ulong.Parse(sV.Substring(0, sV.Length / 2));
                    stones.Add(ulong.Parse(sV.Substring(sV.Length / 2)));
                    continue;
                }
                stones[i] *= 2024;
            }
        }
    }
}
