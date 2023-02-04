using AoCUtils;
using Day16;

Console.WriteLine("Day16: Packet Decoder");

//// inputSample.txt contains multiple examples. input.txt is one line
//string[] inputSamples = FileUtil.ReadFileByLine("inputSample.txt");
//foreach (string line in inputSamples)
//{
//    PacketDecoder pdSample = new();
//    pdSample.PacketHex = line;
//    pdSample.Init();
//    pdSample.Decode();
//    Console.WriteLine($"{pdSample.SumVersion}");
//}

string input = File.ReadAllText("input.txt");

PacketDecoder pd = new();
pd.PacketHex = input;
pd.Init();
long answerPt2 = pd.Decode();

Console.WriteLine($"Part1: {pd.SumVersion}");
Console.WriteLine($"Part2: {answerPt2}");