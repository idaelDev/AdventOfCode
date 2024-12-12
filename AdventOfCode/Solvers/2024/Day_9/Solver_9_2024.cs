using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2024/day/9
    internal class Solver_9_2024 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            //Import
            List<int> disk = new List<int>();

            int fileIndex = 0;
            for(int i = 0; i < data.Length; i++)
            {
                int v = int.Parse(data[i].ToString());
                for(int j = 0; j < v; j++)
                {
                    if(i % 2 == 0)
                    {
                        disk.Add(fileIndex);
                    }
                    else
                    {
                        disk.Add(-1);
                    }
                }
                if(i % 2 == 0)
                {
                    fileIndex++;
                }
            }

            //..
            //DrawLine(disk);

            for(int i = 0; i < disk.Count; i++)
            {
                if(disk[i] < 0)
                {
                    int index = disk.FindLastIndex(x => x >= 0);
                    if(index < i)
                    {
                        break;
                    }
                    disk[i] = disk[index];
                    disk[index] = -1;
                    //DrawLine(disk);
                }
            }
            var result = disk.Where(x => x >= 0);
            result = result.Select((x, i) => x * i);
            long r = result.Sum(x => (long)x);
            return r.ToString();
        }


        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            List<Block> blocks = new List<Block>();
            int index = 0;
            for(int i = 0; i < data.Length; i++)
            {
                blocks.Add(new Block()
                {
                    id = i%2 == 0 ? index : -1,
                    size = int.Parse(data[i].ToString())
                });
                if(i%2 == 0)
                {
                    index++;
                }
            }

            //DrawBlocks(blocks);

            List<Block> files = blocks.Where(x => x.id >= 0).ToList();
            files.Reverse();

            for(int i = 0; i < files.Count; i++)
            {
                int pos = blocks.IndexOf(files[i]);
                int newPos = blocks.FindIndex(x => x.id == -1 && x.size >= files[i].size);
                if(newPos != -1 && newPos < pos)
                {
                    blocks[pos - 1] = new Block()
                    {
                        id = -1,
                        size = blocks[pos-1].size + files[i].size
                    };
                    blocks[newPos] = new Block()
                    {
                        id = -1,
                        size = blocks[newPos].size - files[i].size
                    };
                    blocks.Insert(newPos, files[i]);
                    blocks.RemoveAt(pos +1);
                }
                Console.WriteLine(i + "/" + files.Count);
                //DrawBlocks(blocks);
            }

            long sum = 0;
            int id = 0;
            foreach(Block item in blocks)
            {
                if(item.id >= 0)
                {
                    for(int i = id; i < id+item.size; i++)
                    {
                        sum += i * item.id;
                    }
                }
                id += item.size;
            }

            return sum.ToString();
        }

        internal struct Block
        {
            internal int id;
            internal int size;
        }

        private void DrawBlocks(List<Block> blocks)
        {
            StringBuilder sb = new StringBuilder();
            foreach(Block item in blocks)
            {
                for(int i = 0; i < item.size; i++)
                {
                    sb.Append(item.id >= 0 ? item.id : ".");
                }
            }
            Console.WriteLine(sb.ToString());

        }

        private void DrawLine(List<int> disk)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < disk.Count; i++)
            {
                sb.Append(disk[i] >= 0 ? disk[i].ToString() : ".");
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
