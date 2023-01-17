using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    internal class FloorMap
    {
        private int[,] _theMap;
        private readonly int _xSize;
        private readonly int _ySize;

        // constructor
        public FloorMap(int x, int y)
        {
            _theMap = new int[x, y];
            _xSize = x;
            _ySize = y;
        }

        // handle horizontal, vertical, and perfect diagonals
        public void AddVentLines(int x1, int y1, int x2, int y2)
        {
            if ((x1 > _xSize) || (x2 > _xSize))
            {
                Console.WriteLine($"Error: x dimension {x1} or {x2} is beyond {_xSize}");
                return;
            }

            if ((y1 > _ySize) || (y2 > _ySize))
            {
                Console.WriteLine($"Error: x dimension {y1} or {y2} is beyond {_ySize}");
                return;
            }

            AddHorizontalLine(x1, y1, x2, y2);
            AddVerticalLine(x1, y1, x2, y2);
        }

        public void PrintMap()
        {
            // don't print if the grid is too big
            if ((_xSize > 21) || (_ySize > 21))
                return;

            Console.WriteLine();

            for (int y = 0; y < _ySize; y++)
            {
                for (int x = 0; x < _xSize; x++)
                {
                    if (_theMap[x, y] == 0)
                        Console.Write(".");
                    else
                        Console.Write($"{_theMap[x, y]}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public int GetMultiPoints()
        {
            int multiPoints = 0;

            for (int y = 0; y < _ySize; y++)
            {
                for (int x = 0; x < _xSize; x++)
                {
                    if (_theMap[x, y] > 1)
                        multiPoints++;
                }
            }

            return multiPoints;
        }


        private void AddHorizontalLine(int x1, int y1, int x2, int y2)
        {
            // y values must be the same to be horizontal
            if (y1 != y2)
                return;

            // either x value can be larger, handle both
            int xStart = Math.Min(x1, x2);
            int xFinish = Math.Max(x1, x2);
            for (int x = xStart; x <= xFinish; x++)
                _theMap[x, y1]++;
        }

        private void AddVerticalLine(int x1, int y1, int x2, int y2)
        {
            // x values must be the same to be vertical
            if (x1 != x2)
                return;

            // either y value can be larger, handle both
            int yStart = Math.Min(y1, y2);
            int yFinish = Math.Max(y1, y2);
            for (int y = yStart; y <= yFinish; y++)
                _theMap[x1, y]++;
        }

        public void AddDiagonalLine(int x1, int y1, int x2, int y2)
        {
            int xDirection, yDirection, numPoints;

            // difference of x values and difference of y values must match to be diagonal
            if (Math.Abs(x1 - x2) != Math.Abs(y1 - y2))
            {
                //Console.WriteLine($"{x1},{y1} to {x2},{y2} line is NOT diagonal");
                return;
            }

            // set up start coordinates and directions to simplify loop
            numPoints = Math.Abs(x1 - x2);
            xDirection = (x1 < x2) ? 1 : -1;
            yDirection = (y1 < y2) ? 1 : -1;


            for (int i = 0, x = x1, y = y1; i <= numPoints; i++)
            {
                _theMap[x, y]++;

                // move x, y in the proper directions to next point
                x += xDirection;
                y += yDirection;
            }
        }
    }
}
