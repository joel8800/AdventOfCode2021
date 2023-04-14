using AoCUtils;
using Day21;

Console.WriteLine("Day21: Dirac Dice");

string[] input = FileUtil.ReadFileByLine("input.txt");

List<Player> players = new()
{
    new(Convert.ToInt32(input[0].Split(' ').Last())),
    new(Convert.ToInt32(input[1].Split(' ').Last()))
};

Die100 d = new();

int turn = 0;
while (true)
{

    players[turn].Score += players[turn].Move(d.Roll3());

    if (players[turn].Score >= 1000)
        break;

    turn++;
    turn %= 2;
}

int result = players.Min(p => p.Score) * d.Rolls;

Console.WriteLine($"Part1: {result}");

//-----------------------------------------------------------------------------

int player1 = Convert.ToInt32(input[0].Split(' ').Last());
int player2 = Convert.ToInt32(input[1].Split(' ').Last());

long p1Wins, p2Wins;
QuantumDie qd = new();

(p1Wins, p2Wins) = qd.ComputeWinCount(player1, 0, player2, 0);

Console.WriteLine($"Part2: {Math.Max(p1Wins, p2Wins)}");

//=============================================================================
