namespace Day07
{
    internal class CrabSub
    {
        public int Position { get; private set; }
 
        // constructor
        public CrabSub(int position)
        {
            Position = position;
        }

        // returns the fuel units required to go to targetPos from _position
        public int FuelRequiredPart1(int targetPos)
        {
            // Part 1: return distance to target position
            return Math.Abs(Position - targetPos);
        }

        // returns the fuel units required to go to targetPos from _position
        public int FuelRequiredPart2(int targetPos)
        {
            // Part 2: distance increases fuel cost at each step
            int distance = Math.Abs(Position - targetPos);

            // sum of integers formula
            // s = n(a + 1) / 2
            int fuelCost = distance * (1 + distance) / 2;

            //for (int i = 0; i <= distance; i++)
            //    fuelCost += i;

            return fuelCost;
        }
    }
}
