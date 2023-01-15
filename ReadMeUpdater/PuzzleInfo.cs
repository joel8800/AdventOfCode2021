namespace ReadMeUpdater
{
    internal class PuzzleInfo
    {
        public int PuzzleNum { get; set; }
        public string PuzzleNumStr { get; set; }
        public string PuzzleTitle { get; set;}
        public bool Part1Solved { get; set; }
        public bool Part2Solved { get; set; }

        public PuzzleInfo()
        {
            PuzzleNum = -1;
            PuzzleNumStr = string.Empty;
            PuzzleTitle = string.Empty;
            Part1Solved = false;
            Part2Solved = false;
        }

        public PuzzleInfo(string infoString)
        {
            string[] info = infoString.Split('|', StringSplitOptions.TrimEntries);
            PuzzleNum = Convert.ToInt32(info[0]);
            PuzzleTitle = info[1];
            Part1Solved = info[2] == "True" ? true : false;
            Part2Solved = info[3] == "True" ? true : false;
            PuzzleNumStr = $"{PuzzleNum:D02}";
        }

        public string MakeDBString()
        {
            string output = $"{PuzzleNum}|{PuzzleTitle}|{Part1Solved}|{Part2Solved}";
            return output;
        }
    }
}
