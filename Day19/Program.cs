using AoCUtils;
using Day19;

Console.WriteLine("Day19: Beacon Scanner");

List<Scanner> knownScanners = new();
List<Scanner> unknownScanners = new();

string[] input = FileUtil.ReadFileByBlock("input.txt");

int id = 0;
foreach (string scannerLines in input)
{
    List<string> beaconLines = scannerLines.Split(Environment.NewLine).ToList();
    beaconLines.RemoveAt(0);    // scanner ID line, don't need it

    List<Beacon> beacons = new();
    foreach (string beaconLine in beaconLines)
    {
        string[] xyz = beaconLine.Split(',', StringSplitOptions.TrimEntries);
        Beacon b = new(Convert.ToInt32(xyz[0]), Convert.ToInt32(xyz[1]), Convert.ToInt32(xyz[2]));
        beacons.Add(b);
    }

    Scanner s = new(id++, beacons);
    unknownScanners.Add(s);
}

// use scanner 0 as the origin, add it to knownScanners and set coordinates to [0,0,0]
Scanner origin = unknownScanners[0];
origin.X = 0; origin.Y = 0; origin.Z = 0;

knownScanners.Add(origin);
unknownScanners.Remove(origin);

// check each known scanner until we find an unknown one with enough common beacons
// the relationships between beacons in each scanners list is constant and if two scanners share 66
// of them, then there are 12 beacons shared. 12 take 2 = (11 + 10 + 9 ... + 1) = 66
while (unknownScanners.Count > 0)
{
    bool foundOne = false;
    Scanner resolvedScanner = new();

    foreach (Scanner s0 in knownScanners)
    {
        foreach (Scanner s1 in unknownScanners)
        {
            if (s0 == s1 || s1.IsPlaced())
                continue;

            // if 66 relationships match, then 12 beacons are common
            var sharedBeacons = s0.Vectors.Intersect(s1.Vectors);
            if (sharedBeacons.Count() >= 66)
            {
                //Console.Write($"scanner {s0.Id,2} and scanner {s1.Id,2} share at least 12 beacons, ");
            
                // check each of the 24 possible orientations
                // find the one that yields the 12 common beacons
                for (int i = 0; i < 24; i++)
                {
                    int matches = s0.RotateRemoteScanner(s1, i);
                    if (matches >= 12)
                    {
                        //Console.WriteLine($"rotation: {i,2}");
                        s1.Reposition();
                        resolvedScanner = s1;
                        foundOne = true;
                        break;
                    }
                }
            }

            if (foundOne)
                break;
        }

        if (foundOne)
            break;
    }

    // must add and remove outside of the foreach loops
    if (foundOne)
    {
        knownScanners.Add(resolvedScanner);
        unknownScanners.Remove(resolvedScanner);
    }
}


// add all the beacons from all known scanners to hash set to remove duplicates
HashSet<Beacon> allBeacons = new();
foreach (Scanner s in knownScanners)
{
    //s.PrintScannerInfo();
    //s.PrintBeacons();
    foreach (Beacon b in s.Beacons)
        allBeacons.Add(b);
}

int answerPt1 = allBeacons.Count;

// ----------------------------------------------------------------------------

// calculate manhattan distances between all scanners, save the largest
int manhattan = 0;
for (int i = 0; i < knownScanners.Count; i++)
{
    for (int j = i + 1; j < knownScanners.Count; j++)
    {
        int xDiff = Math.Abs(knownScanners[i].X - knownScanners[j].X);
        int yDiff = Math.Abs(knownScanners[i].Y - knownScanners[j].Y);
        int zDiff = Math.Abs(knownScanners[i].Z - knownScanners[j].Z);

        //Console.Write($"from:[{knownScanners[i].X,5},{knownScanners[i].Y,5},{knownScanners[i].Z,5}] ->");
        //Console.Write($"  to:[{knownScanners[j].X,5},{knownScanners[j].Y,5},{knownScanners[j].Z,5}] = ");
        //Console.WriteLine($"diff:[{xDiff,5},{yDiff,5},{zDiff,5}] ==> manhattan:{xDiff + yDiff + zDiff}");

        if (manhattan < (xDiff + yDiff + zDiff))
            manhattan = xDiff + yDiff + zDiff;
    }
}

int answerPt2 = manhattan;

Console.WriteLine($"Part1: {answerPt1}");
Console.WriteLine($"Part2: {answerPt2}");

// ============================================================================