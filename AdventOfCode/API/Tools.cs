﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.API
{
    internal static class Tools
    {

        public static string WorkFolder(DateTime date) => "./AdventOfCode/Solvers/" + date.Year;
        public static string ClassFile(DateTime date) => @$"{WorkFolder(date)}/Solver_{date.Day}_{date.Year}.cs";
        public static string TestInput(DateTime date) => @$"{WorkFolder(date)}/input_test.txt";
        public static string Input(DateTime date) => @$"{WorkFolder(date)}/input.txt";
        public static string Output(DateTime date) => @$"{WorkFolder(date)}/output.txt";

        public static string GetTestData(DateTime date) => File.ReadAllText(TestInput(date));

        public static string GetInputData(DateTime date) => File.ReadAllText(Input(date));

        public static void WriteOutput(DateTime date, string out1, string out2)
        {
            if(File.Exists(Output(date)))
            {
                File.Delete(Output(date));
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Part 1 results");
            sb.AppendLine(out1);
            sb.AppendLine("Part 2 results");
            sb.AppendLine(out2);
            File.WriteAllText(Output(date), sb.ToString());
        }

        public static string[] LineSplit(string text)
        {
            return text.Split(new[] { Environment.NewLine },StringSplitOptions.RemoveEmptyEntries);
        }

        public static ISolver FindSolver(DateTime date)
        {
            List<Type> solvers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(t => t.GetInterfaces().Contains(typeof(ISolver)) && t.IsClass).ToList();
            Type t = solvers.Find(x => x.Name == $@"Solver_{date.Day}_{date.Year}");
            return Activator.CreateInstance(t) as ISolver;
        }
    }
}