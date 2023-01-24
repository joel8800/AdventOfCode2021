using AoCUtils;
using System.Text;

Console.WriteLine("Day08: Seven Segment Search");

string[] input = FileUtil.ReadFileByLine("input.txt");

List<string> pattern = new();
List<string> output = new();

foreach (string line in input)
{
    string[] parts = line.Split(" | ");
    pattern.Add(parts[0]);
    output.Add(parts[1]);
}

int count1478 = 0;
foreach (string line in output)
{
    string[] outDigits = line.Split(' ');
    foreach (string digit in outDigits)
    {
        // length of "1" = 2, length of "7" = 3, length of "4" = 4, length of "8" = 7
        if (digit.Length == 2 || digit.Length == 3 || digit.Length == 4 || digit.Length == 7)
            count1478++;
    }
}

Console.WriteLine($"Part1: {count1478}");

//-----------------------------------------------------------------------------

int outputTotal = 0;
for (int i = 0; i < pattern.Count; i++)
{
    string patternLine = pattern[i];
    string outDigits = output[i];

    Dictionary<string, string> decoder;

    decoder = DecodePatterns(patternLine);
    outputTotal += DecodeOutput(decoder, outDigits);
}

Console.WriteLine($"Part2: {outputTotal}");

//=============================================================================

int DecodeOutput(Dictionary<string, string> decoder, string output)
{
    List<string> outDigits = output.Split(' ').ToList();

    StringBuilder sb = new();
    foreach (string digit in outDigits)
    {
        string lookup = decoder[SortString(digit)];
        sb.Append(lookup);
    }

    return Convert.ToInt32(sb.ToString());
}


Dictionary<string, string> DecodePatterns(string patternLine)
{
    List<string> patternDigits = patternLine.Split(' ').ToList();
    for (int i = 0; i < patternDigits.Count; i++)
        patternDigits[i] = SortString(patternDigits[i]);

    Dictionary<int, string> mapping = new();
    mapping[1] = patternDigits.Find(s => s.Length == 2);
    mapping[7] = patternDigits.Find(s => s.Length == 3);
    mapping[4] = patternDigits.Find(s => s.Length == 4);
    mapping[8] = patternDigits.Find(s => s.Length == 7);

    Handle6Segments(patternDigits, mapping);
    Handle5Segments(patternDigits, mapping);

    Dictionary<string, string> decoder = new();
    foreach (var segMap in mapping)
        decoder[segMap.Value] = segMap.Key.ToString();

    return decoder;
}

void Handle6Segments(List<string> patternSegs, Dictionary<int, string> mapping)
{
    List<string> length6 = patternSegs.FindAll(s => s.Length == 6);

    // 6 = intersection of one of the 6 seg strings and mapping[1] == 1
    mapping[6] = length6.Find(s => s.ToHashSet().Intersect(mapping[1].ToHashSet()).Count() == 1);
    length6.Remove(mapping[6]);

    // 9 = intersection of one of the 6 seg strings and mapping[4] == 4
    mapping[9] = length6.Find(s => s.ToHashSet().Intersect(mapping[4].ToHashSet()).Count() == 4);
    length6.Remove(mapping[9]);

    // 0 = remaining 6 seg string
    mapping[0] = length6[0];

    return;
}

void Handle5Segments(List<string> patternSegs, Dictionary<int, string> mapping)
{
    List<string> length5 = patternSegs.FindAll(s => s.Length == 5);

    // 5 = intersection of one of the 5 seg strings and mapping[6] == 5
    mapping[5] = length5.Find(s => s.ToHashSet().Intersect(mapping[6].ToHashSet()).Count() == 5);
    length5.Remove(mapping[5]);

    // 3 = intersection of one of the 5 seg strings and mapping[1] == 2
    mapping[3] = length5.Find(s => s.ToHashSet().Intersect(mapping[1].ToHashSet()).Count() == 2);
    length5.Remove(mapping[3]);

    // 2 = remaining
    mapping[2] = length5[0];

    return;
}

string SortString(string input)
{
    char[] chars = input.ToArray();
    Array.Sort(chars);
    return new string(chars);
}