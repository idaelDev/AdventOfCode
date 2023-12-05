using AdventOfCode.API;

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
                        if (seedValue >= range.Source && seedValue < range.Source + range.Range)
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
            List<SeedRange> seedRanges = new List<SeedRange>();
            for (int i = 0; i < seeds.Length; i = i + 2)
            {
                seedRanges.Add(new SeedRange() { Value = seeds[i], Range = seeds[i + 1] });
            }

            List<SeedRange> results = new List<SeedRange>();
            foreach (Converter[] c in converters)
            {
                results.Clear();
                foreach (SeedRange sr in seedRanges)
                {
                    results.AddRange(RangeSolve(sr, c, 0));
                }
                seedRanges.Clear();
                seedRanges.AddRange(results);
            }
            var v = results.OrderBy(x => x.Value).ToList();
            return results.Where(sr => sr.Value != 0).MinBy(sr => sr.Value).Value.ToString();
        }

        private SeedRange[] RangeSolve(SeedRange sr, Converter[] converters, int converterIndex)
        {
            uint end = sr.Value + sr.Range - 1;
            List<SeedRange> results = new List<SeedRange>();
            for (int i = converterIndex; i < converters.Length; i++)
            {
                uint converterEnd = converters[i].Source + converters[i].Range;
                //Rentre complet
                if (sr.Value >= converters[i].Source && sr.Value < converterEnd)
                {
                    //Rentre Complet dans le range
                    if (end >= converters[i].Source && end < converterEnd)
                    {
                        results.Add(new SeedRange() { Value = converters[i].Destination + (sr.Value - converters[i].Source), Range = sr.Range });
                        return results.ToArray();
                    }
                    //Le début rentre mais pas la fin
                    else if (end >= converterEnd)
                    {
                        results.AddRange(RangeSolve(new SeedRange() { Value = sr.Value, Range = converterEnd - sr.Value }, converters, i));
                    }
                }
                //Le debut rentre pas mais la fin si
                else if (end >= converters[i].Source && end < converterEnd)
                {
                    results.AddRange(RangeSolve(new SeedRange() { Value = converters[i].Source, Range = end - converters[i].Source + 1 }, converters, i));
                }
                //Une partie du milieu est in range
                else if (sr.Value < converters[i].Source && end >= converterEnd)
                {
                    results.AddRange(RangeSolve(new SeedRange() { Value = converters[i].Source, Range = converters[i].Range }, converters, i));
                }
            }
            results.Add(sr);
            return results.ToArray();
        }

        private Converter[] ParseConverter(string entry)
        {
            string[] lines = Tools.LineSplit(entry);
            List<Converter> results = new List<Converter>();
            for (int i = 1; i < lines.Length; i++)
            {
                string[] v = lines[i].Split();
                results.Add(new Converter() { Destination = uint.Parse(v[0]), Source = uint.Parse(v[1]), Range = uint.Parse(v[2]) });
            }
            return results.ToArray();
        }

        public struct SeedRange
        {
            public uint Value;
            public uint Range;
        }

        public struct Converter
        {
            public uint Source;
            public uint Destination;
            public uint Range;
        }
    }
}