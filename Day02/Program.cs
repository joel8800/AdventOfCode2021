using AoCUtils;
using System.Runtime.CompilerServices;

Console.WriteLine("Day02: Dive!");

string[] input = FileUtil.ReadFileByLine("input.txt");

int depth = 0;
int distance = 0;

foreach (string line in input)
{
    string[] words = line.Split(' ');
    if (words.Length != 2)
    {
        Console.WriteLine($"Error: wrong number of words on the line: {line}");
        break;
    }

    int arg = Convert.ToInt32(words[1]);

    if (words[0] == "forward")
        distance += arg;

    if (words[0] == "down")
        depth += arg;

    if (words[0] == "up")
        depth -= arg;
}

int product = distance * depth;
Console.WriteLine($"Part1: {product}");

int aim = 0;
depth = 0;
distance = 0;

foreach (string line in input)
{
    string[] words = line.Split(' ');
    if (words.Length != 2)
    {
        Console.WriteLine($"Error: wrong number of words on the line: {line}");
        break;
    }

    int arg = Convert.ToInt32(words[1]);

    if (words[0] == "forward")
    {
        distance += arg;
        depth += (aim * arg);
    }

    if (words[0] == "down")
        aim += arg;

    if (words[0] == "up")
        aim -= arg;
}

product = distance * depth;
Console.WriteLine($"Part2: {product}");