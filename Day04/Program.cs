using AoCUtils;
using Day04;

Console.WriteLine("Day04: Giant Squid");

string[] input = FileUtil.ReadFileByBlock("input.txt");

// get the draw numbers from first line of input
List<int> drawNums = new();
string[] draws = input[0].Split(',');
foreach (string draw in draws)
{
    drawNums.Add(Convert.ToInt32(draw));
}

// rest of the lines are bingo cards
List<BingoCard> cards = new();
for (int i = 1; i < input.Length; i++)
{
    cards.Add(new BingoCard(input[i]));
}

//foreach (BingoCard card in cards)
//{
//    card.PrintCard();
//}

int answerPt1 = 0;
int answerPt2 = 0;

foreach (int draw in drawNums)
{
    //Console.WriteLine($"Drawing new number: {draw}");

    foreach (BingoCard card in cards)
    {
        if (card.IsDone)
            continue;

        card.MarkNumber(draw);

        if (card.CheckForWin())
        {
            int activeCards = cards.Where(x => x.IsDone == false).Count();
            int finishedCards = cards.Where(x => x.IsDone ==true).Count();

            if (finishedCards == 1)
            {
                answerPt1 = card.CalculateMagicNumber(draw);
            }

            if (activeCards == 0)
            {
                answerPt2 = card.CalculateMagicNumber(draw);
            }

            //Console.WriteLine($"Winning board number: {card.UniqueID} ({activeCards} remaining)");
        }
    }
}

Console.WriteLine($"Part1: {answerPt1}");
Console.WriteLine($"Part2: {answerPt2}");