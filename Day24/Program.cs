using AoCUtils;
using Day24;
using System.Diagnostics;

Console.WriteLine("Day24: Arithmetic Logic Unit");

string[] monad = FileUtil.ReadFileByLine("input.txt");      // for my input part1=96918996924991  part2=91811241911641

ALU alu = new();
alu.LoadInstructions(monad);

long answerPt1;
long inputPt1 = 9999999L;  // 7 digits affect outcome, start at max and count down
while (true)
{
    answerPt1 = TryInputNumbers(inputPt1);
    if (answerPt1 != -1)
        break;

    inputPt1--;
}

// verify our answer works
if (alu.RunMONAD(answerPt1) == 0)
    Console.WriteLine("Part1 solution confirmed");

Console.WriteLine($"Part1: {answerPt1}");

//-----------------------------------------------------------------------------

long answerPt2;
long inputPt2 = 1111111L;   // 7 inDigits affect outcome, start at min and count up
while (true)
{
    answerPt2 = TryInputNumbers(inputPt2);
    if (answerPt2 != -1)
        break;

    inputPt2++;
}

// verify our answer works
if (alu.RunMONAD(answerPt2) == 0)
    Console.WriteLine("Part2 solution confirmed");

Console.WriteLine($"Part2: {answerPt2}");

//=============================================================================

static long TryInputNumbers(long inputLong)
{
    // key values in _instructions that affect the result in z
    int[] addend = new int[] { 5, 5, 1, 15, 2, 1, 5, 8, 7, 8, 7, 2, 2, 13 };      // number to add or subtract
    int[] addSub = new int[] { 1, 1, 1,  1, 1, 0, 1, 0, 0, 0, 1, 0, 0,  0 };      // add (1) or subtract (0) value

    long[] resArray = new long[14];
    string inputStr = inputLong.ToString();

    // don't allow input with any 0's
    if (inputStr.Any(c => c == '0'))
        return -1;

    // convert input into array of its digits
    List<int> inDigits = inputStr.Select(c => int.Parse(c.ToString())).ToList();
    long z = 0;

    // i = 0 is MS digit
    for (int i = 0, idx = 0; i < 14; i++)
    {
        if (addSub[i] == 1)
        {
            z = z * 26 + inDigits[idx] + addend[i];
            resArray[i] = inDigits[idx];
            idx++;
        }
        else
        {
            resArray[i] = ((z % 26) - addend[i]);
            z /= 26;

            // bail if number is outside 1-9
            if ((resArray[i] < 1) || (resArray[i] > 9))   
            return -1;
        }
    }

    // convert array to 14 digit string
    string result = string.Empty;
    foreach (long i in resArray)
        result += i.ToString();
   
    return Convert.ToInt64(result);
}