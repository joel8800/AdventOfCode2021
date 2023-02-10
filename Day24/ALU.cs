using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
    public class ALU
    {
        private readonly Dictionary<string, int> 
            _regIndex = new() { {"w", 0}, {"x", 1}, {"y", 2}, {"z", 3} };
        
        private long[] _registers;
        private List<string> _instructions;
        private Queue<int> _inputQueue;

        public long W { get { return _registers[0]; } set { _registers[0] = value; } }
        public long X { get { return _registers[1]; } set { _registers[1] = value; } }
        public long Y { get { return _registers[2]; } set { _registers[2] = value; } }
        public long Z { get { return _registers[3]; } set { _registers[3] = value; } }
        public bool Q { get { return _inputQueue.Count > 0; } }

        public ALU()
        {
            _registers = new long[4];
            _instructions = new();
            _inputQueue = new();
        }

        public void PrintRegisters()
        {
            Console.WriteLine($"W:{W,14}  X:{X,14}  Y:{Y,14}  Z:{Z,14}");
        }

        public void LoadInstructions(string[] instInput)
        {
            _instructions = instInput.ToList();
        }

        public long RunMONAD(long input)
        {
            QueueInputDigits(input);

            foreach (string instruction in _instructions)
            {
                //if (instruction == "inp w")
                //    PrintRegisters();
                ParseInstruction(instruction);
            }
            //PrintRegisters();

            return Z;
        }

        // do not send numbers with any 0's in them
        public void QueueInputDigits(long input)
        {
            string inputStr = input.ToString();
            if (inputStr.Any(c => c == '0'))
                throw new ArgumentException("illegal digit 0");

            List<int> inputw = inputStr.Select(c => int.Parse(c.ToString())).ToList();

            foreach (int i in inputw)
                _inputQueue.Enqueue(i);
        }

        public void ParseInstruction(string inst)
        {
            string[] tokens = inst.Split(' ');

            int regIdx = _regIndex[tokens[1]];

            int argValue = 0;
            bool argIsNumber = false;

            if (tokens.Length > 2)
            {
                if (int.TryParse(tokens[2], out argValue))
                    argIsNumber = true;
                else
                {
                    argIsNumber = false;
                    argValue = _regIndex[tokens[2]];
                }
            }

            switch (tokens[0])
            {
                case "inp":
                    _registers[regIdx] = _inputQueue.Dequeue();
                    break;

                case "add":
                    if (argIsNumber)
                        _registers[regIdx] += argValue;
                    else
                        _registers[regIdx] += _registers[argValue];
                    break;

                case "mod":
                    _registers[regIdx] %= argValue;
                    break;

                case "mul":
                    if (argIsNumber)
                        _registers[regIdx] *= argValue;
                    else
                        _registers[regIdx] *= _registers[argValue];
                    break;

                case "div":
                    _registers[regIdx] /= argValue;
                    break;

                case "eql":
                    if (argIsNumber)
                        _registers[regIdx] = _registers[regIdx] == argValue ? 1 : 0;
                    else
                        _registers[regIdx] = _registers[regIdx] == _registers[argValue] ? 1 : 0;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
