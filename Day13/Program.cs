using AoCUtils;
using Day13;

Console.WriteLine("Day13: Transparent Origami");

string[] inputs = FileUtil.ReadFileByBlock("input.txt");
string[] markCoordinates = inputs[0].Split(Environment.NewLine);
string[] foldInstructions = inputs[1].Split(Environment.NewLine);

TransparentPaper paperPt1 = new(markCoordinates, foldInstructions);
paperPt1.PerformFolds(isPart1: true);

//-----------------------------------------------------------------------------

Console.WriteLine($"Part1: {paperPt1.CountVisibleMarks()}");
TransparentPaper paperPt2 = new(markCoordinates, foldInstructions);
paperPt2.PerformFolds(isPart1: false);
paperPt2.PrintGrid();

CodeInterpreter interpreter = new();
string code = interpreter.TranslateCodes(paperPt2.ReturnGrid());

Console.WriteLine($"Part2: {code}");

//=============================================================================