using System.Collections.Specialized;
using System.Drawing;

namespace Day09
{
    internal class BasinMap
    {
        public int xSize { get; }
        public int ySize { get; }

        private List<List<int>> _basinMap;

        public BasinMap(List<List<int>> map)
        {
            xSize = map[0].Count;
            ySize = map.Count;

            _basinMap = map;
        }

        public bool IsLowPoint(int x, int y)
        {
            int N = -1, S = -1, E = -1, W = -1;
            int thisPoint;

            if ((x >= 0) && (x < xSize) && (y >= 0) && (y < ySize))
            {
                thisPoint = GetHeight(x, y);
            }
            else
            {
                return false;
            }

            if (x == 0) W = 9;
            if (y == 0) N = 9;
            if (x == xSize - 1) E = 9;
            if (y == ySize - 1) S = 9;

            if (N == -1)
                N = GetHeight(x, y - 1);
            if (S == -1)
                S = GetHeight(x, y + 1);
            if (E == -1)
                E = GetHeight(x + 1, y);
            if (W == -1)
                W = GetHeight(x - 1, y);

            if ((thisPoint < N) && (thisPoint < S) && (thisPoint < E) && (thisPoint < W))
                return true;
            else
                return false;
        }

        public int GetHeight(int x, int y)
        {
            return _basinMap[y][x];
        }

        public int GetRegionSize(int x, int y)
        {

            // make sure we're in bounds
            if (x < 0 || y < 0 || x >= xSize || y >= ySize)
            {
                return 0;
            }

            //Console.WriteLine($"basinMap[{y},{x}]:{_basinMap[y][x]}");

            // continue only if we're in a basin
            if (_basinMap[x][y] == 9 || _basinMap[x][y] == -1)
            {
                return 0;
            }

            // if we get here, this point is in a basin, mark it
            _basinMap[x][y] = -1;
            int size = 1;

            // set up to check west, east, north and south points
            int[] xOffset = { -1, 1, 0, 0 };
            int[] yOffset = { 0, 0, -1, 1 };

            // recursively check surrounding points (N,S,E,W), adding to total as we go
            for (int i = 0; i < 4; i++)
            {
                size += GetRegionSize(x + xOffset[i], y + yOffset[i]);
            }

            return size;
        }

        public int GetBiggestRegions()
        {
            int maxRegion = 0;
            List<int> basinSizes = new();

            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    if (_basinMap[x][y] < 9)
                    {
                        int size = GetRegionSize(y, x);
                        //if (size > 0)
                        //    Console.WriteLine($"region found, size = {size}");
                        basinSizes.Add(size);
                        maxRegion = Math.Max(size, maxRegion);
                    }
                }
            }

            if (basinSizes.Count < 3)
            {
                Console.WriteLine("Error: didn't find enough basins");
                return 0;
            }

            // sort list of regions found
            basinSizes.Sort();
            basinSizes.Reverse();

            return basinSizes[0] * basinSizes[1] * basinSizes[2];
        }

        public void PrintBasinMap()
        {
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    if (_basinMap[y][x] == 9)
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        public void PrintReliefMap()
        {
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    int i = _basinMap[y][x];
                    switch (i)
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(i);
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(i);
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write(i);
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(i);
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(i);
                            break;
                        case 5:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(i);
                            break;
                        case 6:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(i);
                            break;
                        case 7:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(i);
                            break;
                        case 8:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(i);
                            break;
                        case 9:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(i);
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Key: ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("0");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("1");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("2");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("3");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("4");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("5");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("6");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("7");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("8");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("9");
            Console.WriteLine();
        }

    }
}
