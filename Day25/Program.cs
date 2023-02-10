using AoCUtils;
using Day25;

Console.WriteLine("Day25: Sea Cucumber");

List<List<char>> input = FileUtil.ReadFileToCharGrid("input.txt");

Map map = new(input);   // for a cool visual
map.ShowMap = false;    // ShowMap = true and resize console window to size of map

int steps = map.StepUntilDone();

Console.WriteLine($"Part1: {steps}");

//=============================================================================