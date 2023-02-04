using Day17;

Console.WriteLine("Day17: Trick Shot");

string input = File.ReadAllText("input.txt");

ProbeLauncher pl2 = new(input);
pl2.FindTargetZone();

Console.WriteLine($"Part1: {pl2.Apex}");
Console.WriteLine($"Part1: {pl2.Hits}");
