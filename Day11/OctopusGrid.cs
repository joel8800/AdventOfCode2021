namespace Day11
{
    internal class OctopusGrid
    {
        private int _xSize;
        private int _ySize;
        private List<List<int>> _grid;
        private List<List<int>> _flashed;

        public int Flashes { get; set; }

        public OctopusGrid(List<List<int>> grid)
        {
            _grid = grid;
            _xSize = _grid[0].Count;
            _ySize = _grid.Count;
            
            _flashed = new();
            for (int row = 0; row < _ySize; row++)
            {
                _flashed.Add(new List<int>());
                _flashed[row] = new();

                for (int col = 0; col < _xSize; col++)
                    _flashed[row].Add(0);
            }

            Flashes = 0;
        }

        public void PrintGrid(int i)
        {
            if (i > 0)
                Console.WriteLine($"After step {i}:");

            for (int row = 0; row < _ySize; row++)
            {
                for (int col = 0; col < _xSize; col++)
                {
                    if (_grid[row][col] < 10)
                        Console.Write($"{_grid[row][col]}");
                    else
                        Console.Write("#");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public int Step()
        {
            ClearFlashedGrid();

            // increase energy of all octopi
            for (int row = 0; row < _ySize; row++)
                for (int col = 0; col < _xSize; col++)
                    _grid[row][col]++;

            // check for octopi that need to flash
            int flashes;
            do
            {
                flashes = 0;
                for (int row = 0; row < _ySize; row++)
                    for (int col = 0; col < _xSize; col++)
                        if (_grid[row][col] > 9)
                        {
                            Flash(row, col);
                            flashes++;
                        }

                //Flashes += flashes;
            } while (flashes != 0);

            int flashesInThisStep = CountFlashes();

            // octopi that flashed have their energy go to 0
            ResetFlashedOctopi();

            //Console.WriteLine($"Flashes in this step         : {flashesInThisStep}");
            //Console.WriteLine($"Total flashes after this step: {totalFlashes}");

            return flashesInThisStep;
        }

        // marks the location of current octopus as flashed and increases energy of surrounding octopi
        private int Flash(int row, int col)
        {
            // flash this octopus, energy goes to 0
            _grid[row][col] = 0;
            _flashed[row][col] = 1;

            // set up energize surrounding octopi, including diagonals
            //                  NW   N  NE   W  E  SW  S  SE
            int[] rowOffset = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] colOffset = { -1, 0, 1, -1, 1, -1, 0, 1 };

            // increase energy of surrounding octopi
            for (int i = 0; i < 8; i++)
            {
                int r = row + rowOffset[i];
                int c = col + colOffset[i];

                // stay in bounds
                if (r < 0 || c < 0 || r >= _ySize || c >= _xSize)
                    continue;

                //Console.WriteLine($"[{r}, {c}] was {_grid[r, c]} increasing to {_grid[r, c] + 1}");
                _grid[r][c]++;
            }

            return 1;
        }

        private void ClearFlashedGrid()
        {
            for (int row = 0; row < _ySize; row++)
                for (int col = 0; col < _xSize; col++)
                    _flashed[row][col] = 0;
        }

        private int ResetFlashedOctopi()
        {
            int numFlashed = 0;

            for (int row = 0; row < _ySize; row++)
                for (int col = 0; col < _xSize; col++)
                    if (_flashed[row][col] == 1)
                    {
                        _grid[row][col] = 0;
                        numFlashed++;
                    }

            return numFlashed;
        }

        private int CountFlashes()
        {
            int numFlashed = 0;

            for (int row = 0; row < _ySize; row++)
                for (int col = 0; col < _xSize; col++)
                    if (_flashed[row][col] == 1)
                        numFlashed++;

            //if (numFlashed == xSize * ySize)
            //    Console.WriteLine("Simultaneous Flash");

            return numFlashed;
        }
    }
}
