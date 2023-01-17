using System.ComponentModel;

namespace Day04
{
    internal class BingoCard
    {
        public bool IsDone { get; private set; }
        public int UniqueID { get; private set; }

        private int[,] _cardGrid;
        private int[,] _markedGrid;

        private static int _instanceCtr;
        private readonly int _instanceID;
        private readonly bool _verbose = false;

        // constructor
        public BingoCard(string lineOfNums)
        {
            IsDone = false;
            _instanceID = ++_instanceCtr;

            _cardGrid = new int[5, 5];
            _markedGrid = new int[5, 5];

            string[] cardLines = lineOfNums.Split(Environment.NewLine);

            for (int i = 0; i < 5; i++)
            {
                var numStr = cardLines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < 5; j++)
                {
                    _cardGrid[i, j] = Convert.ToInt32(numStr[j]);
                    _markedGrid[i, j] = 0;
                }
            }
        }

        // prints out the bingo card
        public void PrintCard()
        {
            if (IsDone)
                return;

            if (_verbose)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Card #{_instanceID}");
                Console.WriteLine("---------------");
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (_markedGrid[i, j] == 1)
                        if (_verbose) Console.ForegroundColor = ConsoleColor.Blue;

                    if (_verbose)
                    {
                        Console.Write($"{_cardGrid[i, j],2} ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                if (_verbose) Console.WriteLine();
            }
            if (_verbose) Console.WriteLine();
        }

        public bool MarkNumber(int draw)
        {
            // check each number in _cardGrid and mark if it matches draw
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    if (_cardGrid[i, j] == draw)
                    {
                        _markedGrid[i, j] = 1;
                    }
                }
            }

            return false;
        }


        public bool CheckForWin()
        {
            bool winner = false;

            // check rows
            for (int row = 0; row < 5; row++)
            {
                int sum = 0;
                for (int col = 0; col < 5; col++)
                    sum += _markedGrid[row, col];

                if (sum == 5)
                {
                    if (_verbose)
                        Console.WriteLine($"row {row + 1} is a complete!");
                    PrintCard();
                    IsDone = true;
                    winner = true;
                    break;
                }
            }

            // check columns
            for (int col = 0; col < 5; col++)
            {
                int sum = 0;
                for (int row = 0; row < 5; row++)
                    sum += _markedGrid[row, col];

                if (sum == 5)
                {
                    if (_verbose)
                        Console.WriteLine($"column {col + 1} is a complete!");
                    PrintCard();
                    IsDone = true;
                    winner = true;
                    break;
                }
            }

            return winner;
        }

        public int CalculateMagicNumber(int draw)
        {
            int sum = 0;

            // get sum of all unmarked numbers
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (_markedGrid[i, j] == 0)
                        sum += _cardGrid[i, j];
                }
            }

            // calculate magic number
            int magicNumber = sum * draw;

            if (_verbose)
            {
                Console.WriteLine($"Card {_instanceID} Score = {sum} * {draw} = {magicNumber}");
                Console.WriteLine("----------");
            }

            return magicNumber;
        }
    }
}
