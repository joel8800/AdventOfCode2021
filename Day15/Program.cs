using AoCUtils;

Console.WriteLine("Day15: Chiton");

List<List<int>> map = FileUtil.ReadFileToIntGrid("input.txt");

Console.WriteLine($"Part1: {BreadthFirstSearch(map, 1)}");
Console.WriteLine($"Part2: {BreadthFirstSearch(map, 5)}");

//=============================================================================

int BreadthFirstSearch(List<List<int>> map, int tileX)
{
    int tile0RowSize = map.Count;
    int tile0ColSize = map[0].Count;

    int rowMax = (tile0RowSize * tileX) - 1;
    int colMax = (tile0ColSize * tileX) - 1;

    int totalRisk = 0;

    HashSet<(int r, int c)> visited = new();
    PriorityQueue<(int r, int c), int> Q = new();
    Q.Enqueue((0, 0), 0);

    while (Q.Count > 0)
    {
        Q.TryDequeue(out var position, out int riskToHere);

        if (position == (rowMax, colMax))
        {
            totalRisk = riskToHere;
            break;
        }

        if (visited.Contains(position))
            continue;

        visited.Add(position);

        List<(int r, int c)> adjacents = GetAdjacents(position, rowMax, colMax);
        foreach (var adjacent in adjacents)
        {
            if (visited.Contains(adjacent))
                continue;

            // translate coordinates to tile0
            int tile0Row = adjacent.r % tile0RowSize; 
            int tile0Col = adjacent.c % tile0ColSize;

            // adjust risk based on which tile we're in
            int tileAdjustedRisk = map[tile0Row][tile0Col] + GetTileAdjustment(adjacent, tile0RowSize, tile0ColSize);
            
            // wrap to 1 after 9
            if (tileAdjustedRisk > 9)
                tileAdjustedRisk -= 9;

            Q.Enqueue(adjacent, riskToHere + tileAdjustedRisk);
        }
    }

    return totalRisk;
}

int GetTileAdjustment((int r, int c) adjacent, int rSize, int cSize)
{
    int rowAdjust = adjacent.r / rSize;
    int colAdjust = adjacent.c / cSize;

    return rowAdjust + colAdjust;
}

List<(int r, int c)> GetAdjacents((int r, int c) position, int rMax, int cMax)
{
    List<(int r, int c)> adj = new();

    if (position.r + 1 <= rMax)
        adj.Add((position.r + 1, position.c));

    if (position.r - 1 >= 0)
        adj.Add((position.r - 1, position.c));

    if (position.c + 1 <= cMax)
        adj.Add((position.r, position.c + 1));

    if (position.c - 1 >= 0)
        adj.Add((position.r, position.c - 1));

    return adj;
}