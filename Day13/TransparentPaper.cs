using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    internal class TransparentPaper
    {
        private int _xSize;
        private int _ySize;
        private int[,] _grid;

        private HashSet<(int x, int y)> _marks;
        private List<(string xy, int n)> _folds = new();


        public TransparentPaper(string[] marks, string[] folds)
        {
            _marks = new();

            foreach (string line in marks)
                _marks.Add(GetXY(line));
                
            _xSize = _marks.Max(m => m.x) + 1;
            _ySize = _marks.Max(m => m.y) + 1;

            MakeGrid();
            
            foreach (string line in folds)
            {
                string[] words = line.Split(' ');
                string[] parts = words[2].Split('=');

                string xy = parts[0];
                int n = Convert.ToInt32(parts[1]);

                _folds.Add((xy, n));
            }
        }


        private (int x, int y) GetXY(string line)
        {
            var coords = line.Split(',');
            int x = Convert.ToInt32(coords[0]);
            int y = Convert.ToInt32(coords[1]);

            return (x, y);
        }

        /// <summary>
        /// Create the _grid used to represent paper
        /// </summary>
        private void MakeGrid()
        {
            _grid = new int[_xSize, _ySize];

            foreach ((int x, int y) mark in _marks)
                _grid[mark.x, mark.y] = 1;
        }

        /// <summary>
        /// Print out the current paper _grid regardless of size
        /// </summary>
        public void PrintGrid()
        {
            Console.WriteLine();
            for (int row = 0; row < _ySize; row++)
            {
                for (int col = 0; col < _xSize; col++)
                {
                    if (_grid[col, row] >= 1)
                        Console.Write('#');
                    else
                        Console.Write('.');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Return the _grid in a single string with embedded '\n' to separate rows
        /// </summary>
        /// <returns></returns>
        public string ReturnGrid()
        {
            StringBuilder sb = new();

            sb.Append('\n');
            for (int row = 0; row < _ySize; row++)
            {
                for (int col = 0; col < _xSize; col++)
                {
                    if (_grid[col, row] >= 1)
                        sb.Append('#');
                    else
                        sb.Append('.');
                }
                sb.Append('\n');
            }

            return sb.ToString();
        }

        /// <summary>
        /// Perform fold along an axis: remove overlapping marks, 
        /// set new x and y sizes, create new grid
        /// </summary>
        /// <param name="xy"></param>
        /// <param name="foldAxis"></param>
        private void Fold(string xy, int foldAxis)
        {
            HashSet<(int x, int y)> newSet = new();

            foreach ((int x, int y) in _marks)
            {
                if (xy == "x")
                {
                    if (x > foldAxis)
                    {
                        int xNew = (foldAxis * 2) - x; //foldAxis - (x - foldAxis);
                        newSet.Add((xNew, y));
                    }
                    else
                        newSet.Add((x, y));
                }
                else
                {
                    if (y > foldAxis)
                    {
                        int yNew = (foldAxis * 2) - y; //foldAxis - (y - foldAxis);
                        newSet.Add((x, yNew));
                    }
                    else
                        newSet.Add((x, y));
                }
            }

            if (xy == "x")
                _xSize = foldAxis;
            else
                _ySize = foldAxis;

            _marks = newSet;
            MakeGrid();
            //PrintGrid();
        }

        /// <summary>
        /// Get a count of the visible _marks in the current _grid
        /// </summary>
        /// <returns></returns>
        public int CountVisibleMarks()
        {
            return _marks.Count;
        }

        /// <summary>
        /// Perform all fold instructions in _folds list
        /// </summary>
        public void PerformFolds(bool isPart1)
        {
            // print fold instructions
            foreach ((string xy, int n) fold in _folds)
            {
                //if (fold.xy == "x")
                //    FoldAlongX(fold.n);
                //else
                //    FoldAlongY(fold.n);
                Fold(fold.xy, fold.n);

                if (isPart1)
                    break;
            }
        }
    }
}
