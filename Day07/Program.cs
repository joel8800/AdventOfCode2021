using Day07;

Console.WriteLine("Day07: The Treachery of Whales");

string input = File.ReadAllText("input.txt");
string[] positions = input.Split(',');

// add all the crab subs to a list
List<CrabSub> crabSubs = new();
foreach (string position in positions)
{
    crabSubs.Add(new CrabSub(Convert.ToInt32(position)));
}

int minPos = crabSubs.Min(cs => cs.Position);
int maxPos = crabSubs.Max(cs => cs.Position);

int minFuelCostPt1 = Int32.MaxValue;
int MinFuelCostPt2 = Int32.MaxValue;

// calculate fuel cost and track position with minimal cost
for (int i = minPos; i <= maxPos; i++)
{
    int fuelCostPt1 = 0;
    int fuelCostPt2 = 0;

    foreach (var crabSub in crabSubs)
    {
        fuelCostPt1 += crabSub.FuelRequiredPart1(i);
        fuelCostPt2 += crabSub.FuelRequiredPart2(i);
    }

    // keep track of the position with the lowest overall fuel cost
    minFuelCostPt1 = Math.Min(fuelCostPt1, minFuelCostPt1);
    MinFuelCostPt2 = Math.Min(fuelCostPt2, MinFuelCostPt2);
}

Console.WriteLine($"Part1: {minFuelCostPt1}");
Console.WriteLine($"Part2: {MinFuelCostPt2}");