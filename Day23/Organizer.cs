namespace Day23
{
    internal class Organizer
    {
        // solution adapted from Reddit post

        record struct GameState(char[] World, int Energy) { }

        private GameState _gameState;
        private char[] _initialState;
        private int _hallwaySize;

        private int[] _cost = new[] { 1, 10, 100, 1000 };
        private int[] _directions = new[] { -1, 1 };

        public Organizer(string input)
        {
            _initialState = input.Where(c => c == '.' || (c >= 'A' && c <= 'D')).ToArray();

            _gameState = new();
            _gameState.World = _initialState;
            _gameState.Energy = 0;

            _hallwaySize = _initialState.Where(c => c == '.').Count();
        }


        private bool IsRoomOrganized(char[] world, int depth, int r)
        {
            for (int i = depth - 1; i >= 0; i--)
            {
                var c = world[_hallwaySize + i * 4 + r];

                if (c == '.')
                    return true;

                if (c != 'A' + r)
                    return false;
            }
            return true;
        }

        private int RoomCount(char[] world, int depth, int r)
        {
            int count = 0;
            for (int i = depth - 1; i >= 0; i--)
            {
                var c = world[_hallwaySize + i * 4 + r];
                if (c == '.')
                    return count;

                count++;
            }
            return count;
        }

        private void PushRoom(char[] world, int depth, int r, char c)
        {
            for (int i = depth - 1; i >= 0; i--)
            {
                var index = _hallwaySize + i * 4 + r;
                if (world[index] == '.')
                {
                    world[index] = c;
                    return;
                }
            }
            throw new Exception("Cannot push into full room");
        }

        private void PopRoom(char[] world, int depth, int r)
        {
            for (int i = 0; i < depth; i++)
            {
                var index = _hallwaySize + i * 4 + r;
                if (world[index] != '.')
                {
                    world[index] = '.';
                    return;
                }
            }
            throw new Exception("Cannot pop from empty room");
        }

        private char PeekRoom(char[] world, int depth, int r)
        {
            for (int i = 0; i < depth; i++)
            {
                var index = _hallwaySize + i * 4 + r;
                if (world[index] != '.')
                    return world[index];
            }
            throw new Exception("Cannot peek at empty room");
        }


        private List<GameState> GetNeighbors(int depth, GameState state)
        {
            var neighbors = new List<GameState>();

            // Option 1: Move an amphipod from a hallway to their target room
            for (int i = 0; i < _hallwaySize; i++)
            {
                if (state.World[i] == '.')
                    continue;

                var picked = state.World[i];
                var pickedIndex = picked - 'A';

                // Is the room empty or ready?
                bool canMoveToRoom = IsRoomOrganized(state.World, depth, pickedIndex);
                if (!canMoveToRoom)
                    continue;

                var targetPosition = 2 + 2 * pickedIndex;

                // Is the path empty?
                var direction = targetPosition > i ? 1 : -1;
                for (int j = direction; Math.Abs(j) <= Math.Abs(targetPosition - i); j += direction)
                {
                    if (state.World[i + j] != '.')
                    {
                        canMoveToRoom = false;
                        break;
                    }
                }

                if (!canMoveToRoom)
                    continue;

                var newWorld = new char[state.World.Length];
                state.World.CopyTo(newWorld, 0);

                newWorld[i] = '.';
                PushRoom(newWorld, depth, pickedIndex, picked);

                var newEnergy = state.Energy + (Math.Abs(targetPosition - i) + (depth - RoomCount(state.World, depth, pickedIndex))) * _cost[pickedIndex];
                neighbors.Add(new GameState(newWorld, newEnergy));
            }

            // Greedy, don't try to remove amphipods from rooms if the rooms are ready to be filled
            if (neighbors.Count > 0)
            {
                return neighbors;
            }

            // Option 2: Remove one amphipod from a room
            for (int r = 0; r < 4; r++)
            {
                if (IsRoomOrganized(state.World, depth, r))
                    continue;   // can't take from this one

                var picked = PeekRoom(state.World, depth, r);
                var pickedIndex = picked - 'A';

                // Possible targets are empty spaces on hallway, not in front of any room, with no blockers
                var energy = state.Energy + (depth - RoomCount(state.World, depth, r) + 1) * (_cost[pickedIndex]);
                var roomPosition = 2 + 2 * r;
                foreach (var direction in _directions)
                {
                    var distance = direction;
                    while (roomPosition + distance >= 0 && roomPosition + distance < _hallwaySize && state.World[roomPosition + distance] == '.')
                    {
                        if (roomPosition + distance == 2 || roomPosition + distance == 4 || roomPosition + distance == 6 || roomPosition + distance == 8)
                        {
                            // In front of a room, skip
                            distance += direction;
                            continue;
                        }

                        var newWorld = new char[state.World.Length];
                        state.World.CopyTo(newWorld, 0);

                        newWorld[roomPosition + distance] = picked;
                        PopRoom(newWorld, depth, r);

                        neighbors.Add(new GameState(newWorld, energy + Math.Abs(distance) * _cost[pickedIndex]));

                        distance += direction;
                    }
                }
            }

            return neighbors;
        }

        private bool IsTarget(GameState state, int depth)
        {
            for (int i = 0; i < _hallwaySize; i++)
            {
                if (state.World[i] != '.')
                    return false;
            }

            for (int r = 0; r < 4; r++)
            {
                if (!IsRoomOrganized(state.World, depth, r))
                    return false;
            }

            return true;
        }

        public int Organize()
        {
            // Dijkstra on the graph
            var depth = (_gameState.World.Length - _hallwaySize) / 4;
            var frontier = new PriorityQueue<GameState, int>();

            frontier.Enqueue(_gameState, 0);
            var visited = new HashSet<string>();

            while (frontier.Count > 0)
            {
                var node = frontier.Dequeue();
                var worldString = new string(node.World);

                if (visited.Contains(worldString))
                    continue;
                
                if (IsTarget(node, depth))
                    return node.Energy;
                
                visited.Add(worldString);
                frontier.EnqueueRange(GetNeighbors(depth, node).Select(n => (n, n.Energy)));
            }

            throw new Exception("Unable to organize");
        }
    }
}
