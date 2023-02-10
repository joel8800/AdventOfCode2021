using AoCUtils;
using Day23;

Console.WriteLine("Day23: Amphipod");

List<string> input = FileUtil.ReadFileByLine("input.txt").ToList();

string allLines = string.Join("", input);

Organizer gamePt1 = new(allLines);
int answerPt1 = gamePt1.Organize();

Console.WriteLine($"Part1: {answerPt1}");

//-----------------------------------------------------------------------------

input.Insert(3, "  #D#C#B#A#");         // part 2
input.Insert(4, "  #D#B#A#C#");         // part 2

allLines = string.Join("", input);

Organizer gamePt2 = new(allLines);
int answerPt2 = gamePt2.Organize();

Console.WriteLine($"Part2: {answerPt2}");

//=============================================================================
