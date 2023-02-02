using AoCUtils;
using Day14;

Console.WriteLine("Day14: Extended Polymerization");

string[] input = FileUtil.ReadFileByBlock("input.txt");
string initialPolymer = input[0];
string insertionRules = input[1];

Polymer ployPt1 = new();
ployPt1.Initialize(initialPolymer, insertionRules);

for (int i = 0; i < 10; i++)
{
    ployPt1.PerformInsertionStep();
    //Console.WriteLine($"After step {i}");
    //ployPt1.PrintHisto();
}
long mcePt1 = ployPt1.GetMostCommonElementCount();
long lcePt1 = ployPt1.GetLeastCommonElementCount();

Console.WriteLine($"{mcePt1 - lcePt1}");

//-----------------------------------------------------------------------------

Polymer polyPt2 = new();
polyPt2.Initialize(initialPolymer, insertionRules);

for (int i = 0; i < 40; i++)
{
    polyPt2.PerformInsertionStep();
    //Console.WriteLine($"After step {i}");
    //ployPt2.PrintHisto();
}

long mcePt2 = polyPt2.GetMostCommonElementCount();
long lcePt2 = polyPt2.GetLeastCommonElementCount();

Console.WriteLine($"{mcePt2 - lcePt2}");

//=============================================================================