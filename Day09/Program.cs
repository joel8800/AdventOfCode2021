using AoCUtils;
using Day09;

Console.WriteLine("Day09: Smoke Basin");

List<List<int>> input = FileUtil.ReadFileToIntGrid("input.txt");

BasinMap map = new(input);
//map.PrintReliefMap();

int riskSum = 0;

for (int y = 0; y < map.ySize; y++)
{
    for (int x = 0; x < map.xSize; x++)
    {
        if (map.IsLowPoint(x, y))
        {
            int riskLevel = map.GetHeight(x, y) + 1;
            riskSum += riskLevel;
        }
    }
}

Console.WriteLine($"Part1: {riskSum}");

//-----------------------------------------------------------------------------

int big3Regions = map.GetBiggestRegions();

Console.WriteLine($"Part2: {big3Regions}");

//=============================================================================