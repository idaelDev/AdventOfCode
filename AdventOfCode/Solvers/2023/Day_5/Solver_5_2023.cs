using AdventOfCode.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    //URL: https://adventofcode.com/2023/day/5
    internal class Solver_5_2023 : ISolver
    {
        public string SolvePart1(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.EmpltyLineSplit(data);
            uint[] seeds = lines[0].Split(':')[1].Trim().Split().Select(s => uint.Parse(s)).ToArray();
            //Vector3(destination, source, lenght)
            List<Converter[]> converters = new List<Converter[]>();
            for (int i = 1; i < lines.Length; i++)
            {
                converters.Add(ParseConverter(lines[i]));
            }

            List<uint> soils = new List<uint>();
            foreach (uint seed in seeds)
            {
                uint seedValue = seed;
                foreach (Converter[] converter in converters)
                {
                    foreach (Converter range in converter)
                    {
                        if(seedValue >= range.Source && seedValue < range.Source + range.Range)
                        {
                            seedValue = range.Destination + (seedValue - range.Source);
                            break;
                        }
                    }
                }
                soils.Add(seedValue);
            }
            return soils.Min().ToString();
        }

        public string SolvePart2(string data, Action<float> progress, Action<string> debug)
        {
            string[] lines = Tools.EmpltyLineSplit(data);
            uint[] seeds = lines[0].Split(':')[1].Trim().Split().Select(s => uint.Parse(s)).ToArray();
            //Vector3(destination, source, lenght)
            List<Converter[]> converters = new List<Converter[]>();
            for (int i = 1; i < lines.Length; i++)
            {
                converters.Add(ParseConverter(lines[i]));
            }

            List<uint> soils = new List<uint>();
            for(int i=0; i< seeds.Length; i=i+2)
            {
                for (uint s = seeds[i]; s < seeds[i]+seeds[i+1]; s++)
                {
                    uint seedValue = s;
                    foreach (Converter[] converter in converters)
                    {
                        foreach (Converter range in converter)
                        {
                            if (seedValue >= range.Source && seedValue < range.Source + range.Range)
                            {
                                seedValue = range.Destination + (seedValue - range.Source);
                                break;
                            }
                        }
                    }
                    soils.Add(seedValue);
                }
                Console.WriteLine($@"Part {i}/{seeds.Length}");
            }
            return soils.Min().ToString();
        }

        private Converter[] ParseConverter(string entry)
        {
            string[] lines = Tools.LineSplit(entry);
            List<Converter> results = new List<Converter>();
            for(int i = 1; i<lines.Length; i++) 
            {
                string[] v = lines[i].Split();
                results.Add(new Converter() { Destination = uint.Parse(v[0]), Source = uint.Parse(v[1]), Range = uint.Parse(v[2]) });
                
            }
            return results.ToArray();
        }

        public struct Converter
        {
            public uint Source;
            public uint Destination;
            public uint Range;
        }

    }
}
