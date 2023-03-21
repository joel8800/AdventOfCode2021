using AoCUtils;
using System.Diagnostics;

Console.WriteLine("Day12: Passage Pathing");

string[] input = FileUtil.ReadFileByLine("input.txt");  // part1: 4573  part2: 117509

Dictionary<string, List<string>> graph = new();
foreach (string line in input)
{
    string[] nodes = line.Split('-');

    if (graph.ContainsKey(nodes[0]) == false)
        graph.Add(nodes[0], new List<string>());

    if (graph.ContainsKey(nodes[1]) == false)
        graph.Add(nodes[1], new List<string>());

    graph[nodes[0]].Add(nodes[1]);
    graph[nodes[1]].Add(nodes[0]);
}


var stopwatch = Stopwatch.StartNew();

int answerPt1 = CountPaths(isPart1:true);
Console.WriteLine($"[{stopwatch.Elapsed}] Part1: {answerPt1}");

int answerPt2 = CountPaths(isPart1:false);
Console.WriteLine($"[{stopwatch.Elapsed}] Part2: {answerPt2}");


//=============================================================================

int CountPaths(bool isPart1)
{
    int pathsFound = 0;

    // setup queue (node, visited, visited2x) and add start node
    Queue <(string, HashSet<string>, bool)> Q = new();
    Q.Enqueue(("start", new() { "start" }, false));

    while (Q.Count > 0)
    {
        var current = Q.Dequeue();

        // assign fields to local variables to make code readable
        string currPos = current.Item1;
        HashSet<string> smallCavesVisited = current.Item2;
        bool visitedCaveTwice = current.Item3;
        
        //  if we've reached the end, count it and move on
        if (currPos == "end")
        {
            pathsFound += 1;
            continue;
        }

        foreach (string currNode in graph[currPos])
        {
            // if we haven't visited this neighbor, add it to the queue
            if (smallCavesVisited.Contains(currNode) == false)
            {
                HashSet<string> newSmallCavesVisited = CopySet(smallCavesVisited);

                if (currNode.ToLower() == currNode)
                    newSmallCavesVisited.Add(currNode);

                Q.Enqueue((currNode, newSmallCavesVisited, visitedCaveTwice));
            }
            else if (visitedCaveTwice == false && currNode != "start" && currNode != "end" && isPart1 == false)
            {
                // if part2, visit one small cave twice
                Q.Enqueue((currNode, smallCavesVisited, true));
            }
        }
    }

    return pathsFound;
}

// copy elements in a set so that the same reference doesn't get reused
HashSet<string> CopySet(HashSet<string> set)
{
    HashSet<string> newSet = new();
    foreach (string s in set)
        newSet.Add(s);

    return newSet;
}
