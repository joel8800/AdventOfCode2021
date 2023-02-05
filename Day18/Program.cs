using AoCUtils;
using Day18;

Console.WriteLine("Day18: Snailfish");

string[] input = FileUtil.ReadFileByLine("input.txt");

Snailfish sf = new(input[0]);
for (int i = 1; i < input.Length; i++)
{
    sf.AddNext(input[i]);
    sf.Reduce();
}
sf.CalcMagnitude();

Console.WriteLine($"Part1: {sf.Magnitude}");

//-----------------------------------------------------------------------------

int maximum = 0;
for (int i = 0; i < input.Length - 1; i++)
{
    for (int j = i + 1; j < input.Length; j++)
    {
        // calc sum of input[i] + input[j]
        sf.Add(input[i], input[j]);
        sf.Reduce();
        int mag1 = Convert.ToInt32(sf.CalcMagnitude());

        // calc sum of input[j] + input[i]
        sf.Add(input[j], input[i]);
        sf.Reduce();
        int mag2 = Convert.ToInt32(sf.CalcMagnitude());

        // track maximum sum
        maximum = Math.Max(maximum, mag1);
        maximum = Math.Max(maximum, mag2);
    }
}

Console.WriteLine($"Part2: {maximum}");

//=============================================================================