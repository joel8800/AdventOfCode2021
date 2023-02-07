using AoCUtils;
using Day22;

Console.WriteLine("Day22: Reactor Reboot");

string[] input = FileUtil.ReadFileByLine("input.txt");

Reactor reactor1 = new(true);

foreach (string rebootStep in input)
    reactor1.ExecuteStep(rebootStep);

Console.WriteLine($"Part1: {reactor1.CubeCount()}");

//-----------------------------------------------------------------------------

Reactor reactor2 = new(false);

foreach (string rebootStep in input)
    reactor2.ExecuteStep(rebootStep);

Console.WriteLine($"Part2: {reactor2.CubeCount()}");

//=============================================================================