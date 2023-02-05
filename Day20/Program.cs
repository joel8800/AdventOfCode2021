using AoCUtils;
using Day20;

Console.WriteLine("Day20: Trench Map");

string[] input = FileUtil.ReadFileByBlock("input.txt");

PixelEnhancer pePt1 = new(input[0], input[1]);
pePt1.EnhanceImage();
pePt1.EnhanceImage();

Console.WriteLine($"Part1: {pePt1.Ones}");

//-----------------------------------------------------------------------------

PixelEnhancer pePt2 = new(input[0], input[1]);
for (int i = 0; i < 50; i++)
    pePt2.EnhanceImage();

Console.WriteLine($"Part2: {pePt2.Ones}");

//=============================================================================