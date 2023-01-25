using AoCUtils;
using Day11;

Console.WriteLine("Day11: Dumbo Octopus");

List<List<int>> input = FileUtil.ReadFileToIntGrid("input.txt");

OctopusGrid dumbos = new(input);

HashSet<int> markers = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

int flashes = 0;
for (int i = 1; i <= 100; i++)
{
    flashes += dumbos.Step();
    //if (markers.Contains(i))
    //    dumbos.PrintGrid(i);
}

Console.WriteLine($"Part1: {flashes}");

//-----------------------------------------------------------------------------

// assuming we haven't seen all octopi flash, continue from end of part 1
int step = 100;
while (flashes != 100)
{
    step++;
    flashes = dumbos.Step();
}

Console.WriteLine($"Part2: {step}");

//=============================================================================