using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.API
{
    internal interface ISolver
    {

        string SolvePart1(string data, Action<float> progress, Action<string> debug);

        string SolvePart2(string data, Action<float> progress, Action<string> debug);

    }
}
