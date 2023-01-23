using AoCUtils;

Console.WriteLine("Day10: Syntax Scoring");

string[] input = FileUtil.ReadFileByLine("input.txt");

int score = 0;
List<string> incompleteLines = new();
foreach (string line in input)
{
    int lineScore;
    bool chunkFound = true;
    string tmpLine = line;

    //Console.WriteLine($"NavLine: {tmpLine}");

    while (chunkFound == true)
    {
        if (tmpLine.Contains("()") || tmpLine.Contains("[]") || tmpLine.Contains("{}") || tmpLine.Contains("<>"))
        {
            tmpLine = RemoveChunk(tmpLine, "()");
            tmpLine = RemoveChunk(tmpLine, "[]");
            tmpLine = RemoveChunk(tmpLine, "{}");
            tmpLine = RemoveChunk(tmpLine, "<>");

            chunkFound = true;
        }
        else
            chunkFound = false;
        //Console.WriteLine($"       : {tmpLine}");
    }
    //Console.WriteLine();

    lineScore = ScoreLine(tmpLine);
    score += lineScore;

    // save incomplete lines for part 2
    if (lineScore == 0)
        incompleteLines.Add(tmpLine);
}

Console.WriteLine($"Part1: {score}");

//-----------------------------------------------------------------------------

List<long> incompleteScores = new();
foreach (string line in incompleteLines)
{
    incompleteScores.Add(ScoreIncompleteLine(line));
}

incompleteScores.Sort();
int medianIndex = (incompleteScores.Count - 1) / 2;
Console.WriteLine($"Part2: {incompleteScores[medianIndex]}");

//=============================================================================

// removes pairs of characters that match the pattern in chunk
string RemoveChunk(string line, string chunk)
{
    int pos = line.IndexOf(chunk, 0);

    if (pos != -1)
        line = line.Remove(pos, 2);

    return line;
}

// calculates the score of a line by finding the first illegal character
// which is a closing ), ], }, or >
int ScoreLine(string line)
{
    var tp = (score: 0, pos: 999);
    int position;

    position = line.IndexOf(')');
    if (position >= 0 && position < tp.pos)
        tp = (3, position);

    position = line.IndexOf(']');
    if (position >= 0 && position < tp.pos)
        tp = (57, position);

    position = line.IndexOf('}');
    if (position >= 0 && position < tp.pos)
        tp = (1197, position);

    position = line.IndexOf('>');
    if (position >= 0 && position < tp.pos)
        tp = (25137, position);

    //Console.WriteLine($"Score for this line: {tp.score}");
    return tp.score;
}

// For part 2: incomplete line score
// calculates the score of an incomplete line
long ScoreIncompleteLine(string line)
{
    long score = 0;

    for (int i = line.Length - 1; i >= 0; i--)
    {
        score *= 5;

        switch (line[i])
        {
            case '(':
                score += 1;
                break;
            case '[':
                score += 2;
                break;
            case '{':
                score += 3;
                break;
            case '<':
                score += 4;
                break;
        }
    }

    return score;
}