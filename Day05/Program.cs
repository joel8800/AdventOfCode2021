using AoCUtils;
using Day05;

Console.WriteLine("Day05: Hydrothermal Venture");

string[] input = FileUtil.ReadFileByLine("input.txt");

List<(int x1, int y1, int x2, int y2)> ventLines = new();

foreach (string line in input)
{
    // get rid of "->" and spaces to make it easier to split
    string allCommas = line.Replace("->", ",").Replace(" ", string.Empty);

    // split on commas
    string[] nums = allCommas.Split(',', StringSplitOptions.RemoveEmptyEntries);

    int x1 = int.Parse(nums[0]);
    int y1 = int.Parse(nums[1]);
    int x2 = int.Parse(nums[2]);
    int y2 = int.Parse(nums[3]);

    ventLines.Add((x1, y1, x2, y2));
}

int maxX = Math.Max(ventLines.Max(v => v.x1), ventLines.Max(v => v.x2));
int maxY = Math.Max(ventLines.Max(v => v.y1), ventLines.Max(v => v.y2));

FloorMap map = new FloorMap(maxX + 1, maxY + 1);

for (int i = 0; i < ventLines.Count; i++)
{
    int x1 = ventLines[i].x1;
    int y1 = ventLines[i].y1;
    int x2 = ventLines[i].x2;
    int y2 = ventLines[i].y2;

    map.AddVentLines(x1, y1, x2, y2);
}
int answerPt1 = map.GetMultiPoints();

map.PrintMap();     // won't print map if bigger than 20x20


for (int i = 0; i < ventLines.Count; i++)
{
    int x1 = ventLines[i].x1;
    int y1 = ventLines[i].y1;
    int x2 = ventLines[i].x2;
    int y2 = ventLines[i].y2;

    map.AddDiagonalLine(x1, y1, x2, y2);
}
int answerPt2 = map.GetMultiPoints();

map.PrintMap();     // won't print map if bigger than 20x20

Console.WriteLine($"Part1: {answerPt1}");
Console.WriteLine($"Part2: {answerPt2}");