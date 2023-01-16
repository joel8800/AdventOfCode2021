using AoCUtils;

Console.WriteLine("Day01: Sonar Sweep");

string[] input = FileUtil.ReadFileByLine("input.txt");

int prevMeas = 0;
int increases = -1;         // first increase doesn't count

foreach (string line in input)
{
    int currMeas = Convert.ToInt32(line);

    if (currMeas > prevMeas)
        increases++;

    prevMeas = currMeas;
}

Console.WriteLine($"Part1: {increases}");

int meas1 = 0;
int meas2 = 0;
int last3ptSum = 0;

increases = -1;             // first increase doesn't count

foreach (string line in input)
{
    int currMeas = Convert.ToInt32(line);

    if (meas1 == 0)         // this is the first measurement
    {
        meas1 = currMeas;
        continue;
    }

    if (meas2 == 0)         // this is the second measurement
    {
        meas2 = currMeas;
        continue;
    }

    // subsequent measurements use the previous two to create a 3 pt sum
    int curr3ptSum = meas1 + meas2 + currMeas;

    // compare the current 3pt sum to the previous
    if (last3ptSum < curr3ptSum)
        increases++;

    last3ptSum = curr3ptSum;

    // slide the measurements over, getting ready for next iteration
    meas1 = meas2;
    meas2 = currMeas;
}

Console.WriteLine($"Part2: {increases}");