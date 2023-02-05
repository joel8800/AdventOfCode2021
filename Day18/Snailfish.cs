using System.Text;
using System.Text.RegularExpressions;

namespace Day18
{
    internal class Snailfish
    {
        public string Number { get; set; }
        public string Magnitude { get; set; }
        public bool Verbose { get; set; }

        private bool _didSomething;

        public Snailfish()
        {
            Number = string.Empty;
            Magnitude = string.Empty;
            Verbose = false;
            _didSomething = true;
        }

        public Snailfish(string input)
        {
            Number = input;
            Magnitude = string.Empty;
            Verbose = false;
            _didSomething = true;
       }

        public void InputSnailfishNumber(string input)
        {
            Number = input;
            Magnitude = string.Empty;
            _didSomething = true;
        }

        public void AddNext(string addend)
        {
            string sumString = "[" + Number + "," + addend + "]";
            Number = sumString;

            if (Verbose)
                Console.WriteLine($"add new number  : {Number}");
        }

        public void Add(string addend1, string addend2)
        {
            string sumString = "[" + addend1 + "," + addend2 + "]";
            Number = sumString;

            if (Verbose)
                Console.WriteLine($"add new number  : {addend1} + {addend2} = {Number}");
        }

        public int GetDepth()
        {
            int maxDepth = 0;
            int currDepth = 0;
            int maxDepthPtr = 0;
            string line = Number;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '[')
                {
                    currDepth++;
                }
                if (line[i] == ']')
                {
                    currDepth--;
                }
                if (currDepth > maxDepth)
                {
                    maxDepth = currDepth;
                    maxDepthPtr = i;
                }
                //if (Verbose)
                //    Console.WriteLine($"currDepth: {currDepth}, maxDepth: {maxDepth}");
            }

            if (Verbose)
                Console.WriteLine($"GetDepth        : -- maxDepth {maxDepth}, index {maxDepthPtr}");

            if (maxDepth > 4)
                return maxDepthPtr;
            else
                return 0;
        }

        public int Explode(int after)
        {
            string line = Number;

            // create regular expressions
            Regex rePair = new("\\[(\\d+),(\\d+)\\]");
            Regex reLeft = new("\\d+", RegexOptions.RightToLeft);
            Regex reRight = new("\\d+");

            // find position of pair and get left and right values
            Match match = rePair.Match(line, after);
            int pos = match.Index;
            int pairLength = match.Groups[0].Value.Length;
            int pairLVal = Convert.ToInt32(match.Groups[1].Value);
            int pairRVal = Convert.ToInt32(match.Groups[2].Value);

            // find next left and next right numbers
            Match leftMatch = reLeft.Match(line, pos);
            Match rightMatch = reRight.Match(line, pos + pairLength);
            int leftNumIdx = leftMatch.Index;
            int rightNumIdx = rightMatch.Index;

            string marks = new('-', line.Length);
            marks = marks.Remove(leftNumIdx, 1).Insert(leftNumIdx, "L");
            marks = marks.Remove(after, 1).Insert(after, "^");
            marks = marks.Remove(rightNumIdx, 1).Insert(rightNumIdx, "R");

            if (Verbose)
            {
                Console.WriteLine($"                : -- pair at {after}, left {leftNumIdx}, right {rightNumIdx}");
                Console.WriteLine($"explode   input : {line}");
                Console.WriteLine($"                : {marks}");
            }

            // handle right number first to not affect indexing of left number
            if ((rightNumIdx != 0) && (rightNumIdx < line.Length))
            {
                int rightNum = Convert.ToInt32(rightMatch.Groups[0].Value) + pairRVal;     //line.Substring(rightNumIdx, 1)) + pairRVal;

                StringBuilder sb = new(line.Substring(0, rightNumIdx));
                sb.Append(rightNum);
                sb.Append(line.Substring(rightNumIdx + rightMatch.Groups[0].Value.Length));
                line = sb.ToString();
            }
            if (Verbose)
                Console.WriteLine($"          right : {line}");

            // handle left number
            if (leftNumIdx >= 1)
            {
                int leftNum = Convert.ToInt32(leftMatch.Groups[0].Value) + pairLVal;    // line.Substring(leftNumIdx, 1)) + pairLVal;

                StringBuilder sb = new(line.Substring(0, leftNumIdx));
                sb.Append(leftNum);
                sb.Append(line.Substring(leftNumIdx + leftMatch.Groups[0].Value.Length));
                line = sb.ToString();
            }
            if (Verbose)
                Console.WriteLine($"           left : {line}");

            // replace pair with "0"
            Number = rePair.Replace(line, "0", 1, leftNumIdx);

            if (Verbose)
                Console.WriteLine($"explode replace : {Number}");

            return 0;
        }

        public bool Split()
        {
            string line = Number;

            // regular expression to find first double digit number
            Regex reTwoDigit = new("\\d\\d");
            Match match = reTwoDigit.Match(line);

            // split number into a pair and replace the number with pair
            if (Verbose)
                Console.WriteLine($"split     input : {line}");

            if (match.Success)
            {
                int gt10 = Convert.ToInt32(match.Groups[0].Value);

                StringBuilder sb = new(line.Substring(0, match.Index));
                sb.Append("[" + gt10 / 2 + "," + (gt10 + 1) / 2 + "]");
                sb.Append(line.Substring(match.Index + 2));
                line = sb.ToString();
                _didSomething = true;
            }
            else
                _didSomething = false;

            Number = line;

            //if (Verbose)
            //    Console.WriteLine($"split    output : {Number}");
            return _didSomething;
        }

        public int Reduce()
        {
            int startAt;
            _didSomething = true;

            if (Verbose)
                Console.WriteLine($"reduce    input : {Number}");

            while (_didSomething)
            {
                while ((startAt = GetDepth()) > 0)
                {
                    Explode(startAt);
                    //Console.WriteLine($"explode done      : {Number}");
                }
                Split();

                if (Verbose)
                    Console.WriteLine($"split      done : {Number}");
            }

            if (Verbose)
                Console.WriteLine($"reduce     done : {Number}");

            return 0;
        }

        private string CalcPairMag(Match pair)
        {
            int left = Convert.ToInt32(pair.Groups[1].Value);
            int right = Convert.ToInt32(pair.Groups[2].Value);
            int mag = (left * 3) + (right * 2);

            return mag.ToString();
        }

        public string CalcMagnitude()
        {
            Regex rePair = new("\\[(\\d+),(\\d+)\\]");
            Match match;

            while (rePair.IsMatch(Number) == true)
            {
                match = rePair.Match(Number);
                string pairMag = CalcPairMag(match);

                Number = Number.Replace(match.Value, pairMag);

                if (Verbose)
                    Console.WriteLine(Number);
            }

            Magnitude = Number;
            return Magnitude;
        }
    }
}
