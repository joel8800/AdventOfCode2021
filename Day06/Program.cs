using AoCUtils;

Console.WriteLine("Day06: Lanterfish");

string input = File.ReadAllText("input.txt");

const int DaysToRun = 256;

long[] daysToSpawn = new long[9];

// count the initial occurrences of each "days to spawn"
for (int i = 0; i < daysToSpawn.Length; i++)
{
    // length of the array from Split -1 = occurances
    daysToSpawn[i] = input.Split(Convert.ToString(i)).Length - 1;
    
    //Console.WriteLine($"number of {i}'s = {daysToSpawn[i]}");
}

long after80 = 0;
long after256 = 0;

// age all the fish by moving the fish into the next lower array element
// array index represents days before spawning new fish
for (int day = 1; day <= DaysToRun; day++)
{
    long totalFish = 0;
    long newFish = daysToSpawn[0];

    // shift fish a day closer to spawning
    for (int j = 0; j < daysToSpawn.Length - 1; j++)
    {
        daysToSpawn[j] = daysToSpawn[j + 1];
    }

    // add the fish that spawned new fish to day 6
    daysToSpawn[6] += newFish;

    // new fish start at day 8
    daysToSpawn[8] = newFish;

    // add up all the fish to get a total for each day and report it
    for (int i = 0; i < 9; i++)
        totalFish += daysToSpawn[i];

    //Console.WriteLine($"After {day, 3} days, number of LanternFish: {totalFish}");

    if (day == 80)              // part1
        after80 = totalFish;
    if (day == 256)             // part2
        after256 = totalFish;
}

Console.WriteLine($"Part1: {after80}");
Console.WriteLine($"Part2: {after256}");
