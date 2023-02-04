using System.Text;

namespace Day16
{
    internal class PacketDecoder
    {
        private int _version;
        private int _typeID;
        private int _currIdx;

        public int SumVersion { get; set; } = 0;
        public long CalcResult { get; set; }

        public string PacketHex { get; set; }
        public string PacketBin { get; set; }

        /// <summary>
        /// Converts a hex character in to a 4-bit binary representation string
        /// </summary>
        /// <param name="h"></param>
        /// <returns>four bit binary string representation of hex character</returns>
        private static string Hex2Bin(char h)
        {
            string bin = Convert.ToString(Convert.ToInt32(h.ToString(), 16), 2);
            return bin.PadLeft(4, '0');
        }

        /// <summary>
        /// constructor
        /// </summary>
        public PacketDecoder()
        {
            _typeID = -1;
            _version = -1;
            _currIdx = 0;

            PacketHex = string.Empty;
            PacketBin = string.Empty;
        }

        /// <summary>
        /// Sets up PacketDecoder after setting PacketHex string
        /// </summary>
        public void Init()
        {
            _currIdx = 0;
            PacketBin = ConvertToBinary(PacketHex);
            _version = GetPacketVersion();
            _typeID = GetPacketTypeID();
        }

        /// <summary>
        /// Converts a string of hex characters into a binary representation string
        /// </summary>
        /// <param name="hex"></param>
        /// <returns>binary representation of hex string</returns>
        public static string ConvertToBinary(string hex)
        {
            StringBuilder sb = new();

            foreach (char c in hex)
                sb.Append(Hex2Bin(c));

            return sb.ToString();
        }

        /// <summary>
        /// Gets the packet _version from the first three bits of the packet
        /// </summary>
        /// <returns>packet _version as an integer</returns>
        private int GetPacketVersion()
        {
            return Convert.ToInt32(PacketBin.Substring(_currIdx, 3), 2);
        }

        /// <summary>
        /// Gets the packet type from the second three bits of the packet
        /// </summary>
        /// <returns>packet type as an integer</returns>
        private int GetPacketTypeID()   //string packet, int startIndex)
        {
            return Convert.ToInt32(PacketBin.Substring(_currIdx + 3, 3), 2);
        }


        public long Decode()
        {
            return DecodePacket();
        }

        private long DecodePacket()
        {
            long result;

            _version = GetPacketVersion();
            _typeID = GetPacketTypeID();
            SumVersion += _version;

            if (_typeID == 4)
            {
                result = DecodeLiteralPacket();
            }
            else
            {
                result = DecodeOperatorPacket();
            }

            CalcResult = result;
            return result;
        }

        private long DecodeLiteralPacket()
        {
            StringBuilder sb = new();

            _currIdx += 6;

            char lastGroupBit = '1';
            while (lastGroupBit == '1')
            {
                string group = PacketBin.Substring(_currIdx, 5);
                sb.Append(group.Substring(1, 4));
                lastGroupBit = group[0];
                _currIdx += 5;
            }

            long result = Convert.ToInt64(sb.ToString(), 2);
            
            return result;
        }

        private long DecodeOperatorPacket()
        {
            int localTypeID = _typeID;

            _currIdx += 6;

            long result = 0;
            List<long> operands = new();

            char lengthType = PacketBin[_currIdx++];
            if (lengthType == '0')
            {
                int subPacketLength = Convert.ToInt32(PacketBin.Substring(_currIdx, 15), 2);

                _currIdx += 15;
                int payloadStart = _currIdx;

                while (_currIdx < (payloadStart + subPacketLength))
                {
                    result = DecodePacket();
                    operands.Add(result);
                }
            }
            else
            {
                int numSubPackets = Convert.ToInt32(PacketBin.Substring(_currIdx, 11), 2);

                _currIdx += 11;

                for (int i = 0; i < numSubPackets; i++)
                {
                    result = DecodePacket();
                    operands.Add(result);
                }
            }

            switch (localTypeID)
            {
                case 0:         // sum
                    result = 0;
                    foreach (long operand in operands)
                        result += operand;
                    break;
                case 1:         // product
                    result = 1;
                    foreach (long operand in operands)
                        result *= operand;
                    break;
                case 2:         // min
                    result = operands.Min();
                    break;
                case 3:         // max
                    result = operands.Max();
                    break;
                case 4:         // literal, should never be this
                    break;
                case 5:         // > compare (assume exactly 2 operands)
                    result = (operands[0] > operands[1]) ? 1 : 0;
                    break;
                case 6:         // < compare (assume exactly 2 operands)
                    result = (operands[0] < operands[1]) ? 1 : 0;
                    break;
                case 7:         // = compare (assume exactly 2 operands)
                    result = (operands[0] == operands[1]) ? 1 : 0;
                    break;
            }

            return result;
        }
    }
}
