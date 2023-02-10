using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    public class Map
    {
        private int _numRows;
        private int _numCols;
        private int _steps;
        private int _eastMoves;
        private int _southMoves;
        private List<List<char>> _map;

        public bool ShowMap { get; set; }

        public Map(List<List<char>> map)
        {
            _map = map;

            _steps = 0;
            _eastMoves = 0;
            _southMoves = 0;

            _numRows = _map.Count;
            _numCols = _map[0].Count;

            ShowMap = false;
        }

        public void PrintMap()
        {
            for (int row = 0; row < _numRows; row++)
            {
                for (int col = 0; col < _numCols; col++)
                {
                    Console.Write(_map[row][col]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public int StepUntilDone()
        {
            _steps = 0;

            do
            {
                _steps++;

                MoveEast();
                MoveSouth();

                if (ShowMap)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine($"Step:{_steps,-4}  Moves East:{_eastMoves,-5} Moves South:{_southMoves,-5}");
                    Console.SetCursorPosition(0, 2);

                    PrintMap();
                }
            } while (_eastMoves + _southMoves > 0);

            return _steps;
        }

        public void MoveEast()
        {
            _eastMoves = 0;
            
            for (int row = 0; row < _numRows; row++)
            {
                //_map[row] = MoveEastList(_map[row]);
                _map[row] = MoveWithinRow(_map[row], '>');
            }
        }

        public void MoveSouth()
        {
            _southMoves = 0;

            for (int col = 0; col < _numCols; col++)
            {
                // create list from each column
                List<char> column = new();

                for (int i = 0; i < _numRows; i++)
                    column.Add(_map[i][col]);

                // process list
                //column = MoveSouthList(column);
                column = MoveWithinRow(column, 'v');

                // copy column back to _map
                for (int i = 0; i < _numRows; i++)
                    _map[i][col] = column[i];
            }
        }

        public List<char> MoveWithinRow(List<char> row, char dir)
        {
            List<char> newRow = new();

            for (int idx = 0; idx < row.Count; idx++)
            {
                char c = row[idx];

                if (c == dir)
                {
                    if (IsBlocked(row, idx))
                        newRow.Add(c);
                    else
                    {
                        // move cucumber
                        if (dir == '>')
                            _eastMoves++;
                        else
                            _southMoves++;
                        
                        newRow.Add('.');

                        idx++;
                        if (idx < row.Count)
                            newRow.Add(dir);
                        else
                        {
                            newRow.RemoveAt(0);
                            newRow.Insert(0, dir);      // wrap
                        }
                    }
                }
                else
                    newRow.Add(c);
            }

            return newRow;
        }

        private bool IsBlocked(List<char> list, int index)
        {
            char c = list[index];

            if (c == '.')
                return true;

            // check location to the right
            int nextCol = index + 1;

            if ((nextCol) >= list.Count)
                nextCol = 0;

            if (list[nextCol] == '.')
                return false;
            else
                return true;
        }
    }
}
