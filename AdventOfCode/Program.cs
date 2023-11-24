// See https://aka.ms/new-console-template for more information
using AdventOfCode.API;
using System.Diagnostics;

Console.WriteLine("Advent of code !!");

if (args.Length > 0)
{
    Start(args);
}
else
{
    while (true)
    {
        Console.Write(">>> ");
        string[] m = Console.ReadLine().Replace(">>> ", string.Empty).Split(' ');
        Start(m);
    }
}

void Start(string[] args)
{
    if (args.Length == 0)
    {
        return;
    }

    if (args[0] == "c" || args[0] == "create")
    {
        CreateTemplate(GetDate(args));
    }

    bool test = false;
    bool part1 = true;
    bool part2 = true;
    if (args[0] == "t" || args[0] == "test")
    {
        test = true;
    }

    if (args.Length > 3)
    {
        if (args[3] == "1")
        {
            part2 = false;
        }
        if (args[3] == "2")
        {
            part1 = false;
        }
    }

    Solve(GetDate(args), test, part1, part2);
}

DateTime GetDate(string[] args)
{
    DateTime d = DateTime.Now;
    if (args.Length > 2)
    {
        if (int.TryParse(args[1], out int day) && int.TryParse(args[2], out int year))
        {
            d = new DateTime(year, 12, day);
        }
    }
    return d;
}

void CreateTemplate(DateTime date)
{
    Console.WriteLine(@$"Creating template for day {date.Day} of year {date.Year}");
    string t = File.ReadAllText("./../../../Templates/SolverTemplate.txt");
    t = t.Replace("<day>", date.Day.ToString());
    t = t.Replace("<year>", date.Year.ToString());

    if (!Directory.Exists(Tools.WorkFolder(date)))
        Directory.CreateDirectory(Tools.WorkFolder(date));
    File.WriteAllText(Tools.ClassFile(date), t);

    File.Create(Tools.Input(date));
    File.Create(Tools.TestInput(date));
    Console.WriteLine("Done");
}

void Solve(DateTime date, bool test, bool part1, bool part2)
{
    Stopwatch timer = new Stopwatch();

    Console.WriteLine(@$"Solving day {date.Day} of year {date.Year}");
    ISolver s = Tools.FindSolver(date);

    string input = test ? Tools.GetTestData(date) : Tools.GetInputData(date);
    string r1 = null;
    string r2 = null;
    if (part1)
    {
        Console.WriteLine("Launch Part 1");
        timer.Start();
        r1 = s.SolvePart1(input, i => Console.WriteLine(i), s => Console.WriteLine(s));
        timer.Stop();
        Console.WriteLine("End Part 1 in " + timer.ElapsedMilliseconds + "ms:\n\t" + r1);
        timer.Reset();
    }

    if (part2)
    {
        Console.WriteLine("Launch Part 2");
        timer.Start();
        r2 = s.SolvePart2(input, i => Console.WriteLine(i), s => Console.WriteLine(s));
        timer.Stop();
        Console.WriteLine("End Part 2 in \"+ timer.ElapsedMilliseconds +\"ms:\n\t" + r2);
    }

    Tools.WriteOutput(date, r1, r2);
}