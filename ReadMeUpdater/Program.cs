using ReadMeUpdater;
using System.Text;
using System.Text.RegularExpressions;

const int year = 2021;
const string progressFile = "progress.txt";

// read last saved status
string[] progressEntries = File.ReadAllLines(progressFile);
List<PuzzleInfo> puzzles = new();
foreach (string entry in progressEntries)
{
    PuzzleInfo pi = new(entry);
    puzzles.Add(pi);
}


// check status of each challenge, prompt for update if not complete
foreach (PuzzleInfo pi in puzzles)
{
    if (pi.Part1Solved && pi.Part2Solved)
        continue;

    CheckIfPartsSolved(pi);
}


// get AoC projects from .sln file. Projects should be named "DayXX"
string[] solution = File.ReadAllLines($"../../../../AdventOfCode{year}.sln");

foreach (string line in solution)
{
    if (line.StartsWith("Project("))
    {
        Match proj = Regex.Match(line, @"Day\d\d");   // grab project names that start with "Day" followed by 2-digit number
        if (proj.Success)
        {
            string dayProj = proj.Value;
            int day = Convert.ToInt32(dayProj.Substring(3, 2));

            if (puzzles.Exists(n => n.PuzzleNum == day))
            { 
                // puzzle already in progress file
                PuzzleInfo? pi = puzzles.Find(n => n.PuzzleNum == day);
                
                if (pi.Part1Solved && pi.Part2Solved)
                    continue;

                // in case both parts haven't already been solved
                CheckIfPartsSolved(pi);

                continue;
            }

            // print some kind of status since we pause below
            Console.WriteLine($"Getting title for day {day}");

            // get title of puzzle for this day from the AoC site
            string url = $"https://adventofcode.com/{year}/day/{day}";
            string title = string.Empty;

            HttpClient client = new();
            try
            {
                // load the puzzle page for the day
                string responseBody = await client.GetStringAsync(url);

                // be a good net citizen and dont DDOS the site
                Thread.Sleep(5000);

                // find the title
                Match m = Regex.Match(responseBody, @"--- Day (.*) ---");
                if (m.Success)
                {
                    string fullTitle = m.Groups[0].Value;
                    string[] titleParts = fullTitle.Split(':', StringSplitOptions.TrimEntries);
                    title = titleParts[1][..^4];
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception caught");
                Console.WriteLine($"Message :{e.Message}");
            }

            if (title != string.Empty)
            {
                PuzzleInfo pi = new();
                pi.PuzzleNum = day;
                pi.PuzzleNumStr = $"{day:D02}";
                pi.PuzzleTitle = title;

                CheckIfPartsSolved(pi);
                
                puzzles.Add(pi);
            }
        }
    }
    else if (line.StartsWith("Global"))    // skip the rest of the .sln file
        break;
}


int DayProgress = puzzles.Count;

// Formatting the output string for file "Readme.md"
List<string> ReadMe = new()
            {
                $"# Advent of Code {year}",
                "- My attempt to catch up on all the Advents of Code. I'm starting this in 2023 ",
                "- ",
                "",
                $"## Progression:  ![Progress](https://progress-bar.dev/{DayProgress}/?scale=25&title=projects&width=240&suffix=/25)",
                "",
                "",
                "| Day                                                          | C#                            | Stars |  Solution Description |",
                "| ------------------------------------------------------------ | ----------------------------- | ----- | -------------------- |"
            };

foreach (PuzzleInfo pi in puzzles)
{
    StringBuilder sb = new();
    sb.Append($"| [Day {pi.PuzzleNumStr}:  ");
    sb.Append($"{pi.PuzzleTitle}](https://adventofcode.com/{year}/day/{pi.PuzzleNum}) ");
    sb.Append($"| [Solution](./Day{pi.PuzzleNumStr}/Program.cs) ");

    string star1 = pi.Part1Solved ? ":star:" : " ";
    string star2 = pi.Part2Solved ? ":star:" : " ";
    sb.Append($"| {star1}{star2} |");

    ReadMe.Add(sb.ToString());
}

// send to README.md
File.WriteAllLines("../../../../README.md", ReadMe);
//foreach (string s in ReadMe)
//    Console.WriteLine(s);


// save our progress
List<string> progressOut = new();
foreach (PuzzleInfo pi in puzzles)
    progressOut.Add(pi.MakeDBString());
File.WriteAllLines(progressFile, progressOut);


//=============================================================================

bool AskIfPartSolved(int part)
{   
    Console.Write($"    Have you solved part {part} (y/n)? ");
    string answer = Console.ReadLine();
    
    if (answer.ToLower().Trim().StartsWith("y"))
    {
        Console.WriteLine($"    -- Marking part {part} as solved.");
        return true;
    }
    else
        Console.WriteLine($"    -- Good luck in solving part {part}.");
    
    return false;
}

void CheckIfPartsSolved(PuzzleInfo pi)
{
    Console.WriteLine($"Day {pi.PuzzleNum} --");
    if (pi.Part1Solved == false)
        pi.Part1Solved = AskIfPartSolved(1);

    if (pi.Part2Solved == false)
        pi.Part2Solved = AskIfPartSolved(2);
}