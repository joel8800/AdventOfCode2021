using AoCUtils;

Console.WriteLine("Day03: Binary Diagnostic");

string[] input = FileUtil.ReadFileByLine("input.txt");
int totalEntries = input.Length;

int numBits = 0;
int gammaRate = 0;
int epsilonRate = 0;
int mask = 0;

// walt through entries from right to left
// get count of 1's in each bit location
numBits = input[0].Length;
for (int i = 0; i < numBits; i++)
{
    int onesCount = input.Where(s => s[numBits - i - 1] == '1').Count();

    if (onesCount > (input.Length / 2))
        gammaRate |= (1 << i);

    // create mask for epsilonRate
    mask |= (1 << i);
}

// epsilonRate is gammaRate inverted
epsilonRate = ~gammaRate;
epsilonRate &= mask;      // mask unused bits

// calculate power out
int powerOut = gammaRate * epsilonRate;
Console.WriteLine($"Part1: {powerOut}");


// part 2
List<string> o2Generator = input.ToList();
List<string> co2Scrubber = input.ToList();

for (int i = numBits - 1; i >= 0; i--)
{
    FindRating(o2Generator, i, 1);
    FindRating(co2Scrubber, i, 0);
}

//Console.WriteLine($"o2 gen   : {o2Generator[0]}");
//Console.WriteLine($"co2 scrub: {co2Scrubber[0]}");

int lifeSupport = Convert.ToInt32(o2Generator[0], 2) * Convert.ToInt32(co2Scrubber[0], 2);
Console.WriteLine($"Part2: {lifeSupport}");


//=============================================================================

static void FindRating(List<string> list, int bitPos, int lookFor)
{
    // get number of ones in all list entries at bit position
    int onesCount = list.Where(s => s[s.Length - bitPos - 1] == '1').Count();
    
    // determine if 1 or 0 is most common
    char mostCommon = onesCount >= list.Count - onesCount ? '1' : '0';

    if (list.Count > 1)
    {
        if (lookFor == 1)
            list.RemoveAll(s => s[s.Length - bitPos - 1] != mostCommon);
        else
            list.RemoveAll(s => s[s.Length - bitPos - 1] == mostCommon);
    }

    //Console.WriteLine($"=== bit {bitPos} ({lookFor}) ===");
    //foreach (var s in list)
    //    Console.WriteLine(s);
}




