namespace Day21
{
    public class QuantumDie
    {
        readonly Dictionary<int, int> rollChances = new() { { 3, 1 }, { 4, 3 }, { 5, 6 }, { 6, 7 }, { 7, 6 }, { 8, 3 }, { 9, 1 } };

        // adapted from python https://www.youtube.com/watch?v=rEyAbeV48tI&list=PLWBKAf81pmOa5C0IGzmK-Pu48pH8YhXAJ&index=23
        public (long, long) ComputeWinCount(int p1Position, int p1Score, int p2Position, int p2Score)
        {
            long p1Wins = 0;
            long p2Wins = 0;
            foreach (var kvp in rollChances)
            {
                int newP1Pos = SpecialMod10(p1Position + kvp.Key);
                int newP1Score = p1Score + newP1Pos;
                if (newP1Score >= 21)
                {
                    p1Wins += kvp.Value;
                }
                else
                {
                    (long tmpP2Wins, long tmpP1Wins) = ComputeWinCount(p2Position, p2Score, newP1Pos, newP1Score);
                    p1Wins += tmpP1Wins * kvp.Value;
                    p2Wins += tmpP2Wins * kvp.Value;
                }
            }
            return (p1Wins, p2Wins);
        }

        private int SpecialMod10(int value)    // can't use mod, returns 1..10
        {
            while (value > 10)
                value -= 10;
            return value;
        }
    }
}
