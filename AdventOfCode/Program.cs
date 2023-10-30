// See https://aka.ms/new-console-template for more information
using AdventOfCode.API;

Console.WriteLine("Advent of code !!");

if(args.Length > 0)
{
    Start(args); 
}
else
{
    Console.Write(">>> ");
    string[] m = Console.ReadLine().Replace(">>> ", string.Empty).Split(' ');
    Start(m);
}

void Start(string[] args)
{
    DateTime date = DateTime.Now;
    int index = args.Length > 2 ? 1:0;
    if (int.TryParse(args[index], out int day) && int.TryParse(args[index+1], out int year))
    {
        date = new DateTime(year, 12, day);
    }

    if(args.Length > 2)
    {
        if (args[0] == "c" || args[0] == "create")
        {
            CreateTemplate(date);
            return;
        }
        if (args[0] == "t" || args[0] == "test")
        {
            Solve(date, true);
            return;
        }
    }

    Solve(date, false);

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

void Solve(DateTime date, bool test)
{
    Console.WriteLine(@$"Solving day {date.Day} of year {date.Year}");
    ISolver s = Tools.FindSolver(date);

    string input = test ? Tools.GetTestData(date) : Tools.GetInputData(date);
    Console.WriteLine("Launch Part 1");
    
    string r1 = s.SolvePart1(input, i => Console.WriteLine(i), s => Console.WriteLine(s));
    Console.WriteLine("End Part 1:\n\t"+r1);

    Console.WriteLine("Launch Part 2");
    string r2 = s.SolvePart2(input, i => Console.WriteLine(i), s => Console.WriteLine(s));
    Console.WriteLine("End Part 2:\n\t"+r2);

    Tools.WriteOutput(date, r1, r2);
}