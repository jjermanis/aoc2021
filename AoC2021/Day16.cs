using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2021
{
    public class Day16 : DayBase, IDay
    {
        private readonly string _packets;
        private int _packetVersionSum;

        public Day16(string filename)
        {
            _packets = ConvertToBinaryString(TextFile(filename));
        }

        public Day16() : this("Day16.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(Part1)}: {Part1()}");
            Console.WriteLine($"{nameof(Part2)}: {Part2()}");
        }

        public long Part1()
        {
            _packetVersionSum = 0;

            EvaluatePacket();

            return _packetVersionSum;
        }

        public long Part2()
            => EvaluatePacket();

        private long EvaluatePacket()
        {
            var curr = 0;
            return EvaluatePacket(ref curr);
        }

        private long EvaluatePacket(ref int currPos)
        {
            var result = 0L;

            var packetVersion = ParseBinary(ref currPos, 3);
            _packetVersionSum += packetVersion;

            var packetType = ParseBinary(ref currPos, 3);

            if (packetType == 4)
            {
                var isLast = false;

                while (!isLast)
                {
                    if (_packets[currPos] == '0')
                        isLast = true;
                    currPos++;

                    result *= 16;
                    result += ParseBinary(ref currPos, 4);
                }
            }
            else
            {
                var subPackets = new List<long>();

                var lengthType = _packets[currPos];
                currPos++;
                if (lengthType == '0')
                {
                    var packetSize = ParseBinary(ref currPos, 15);
                    var endPoint = currPos + packetSize;
                    while (currPos < endPoint)
                        subPackets.Add(EvaluatePacket(ref currPos));
                }
                else
                {
                    var packetCount = ParseBinary(ref currPos, 11);
                    for (int i = 0; i < packetCount; i++)
                    {
                        subPackets.Add(EvaluatePacket(ref currPos));
                    }
                }

                switch (packetType)
                {
                    case 0:
                        result = subPackets.Sum();
                        break;

                    case 1:
                        result = 1;
                        foreach (var value in subPackets)
                            result *= value;
                        break;

                    case 2:
                        result = subPackets.Min();
                        break;

                    case 3:
                        result = subPackets.Max();
                        break;

                    case 5:
                        result = subPackets[0] > subPackets[1] ? 1 : 0;
                        break;

                    case 6:
                        result = subPackets[0] < subPackets[1] ? 1 : 0;
                        break;

                    case 7:
                        result = subPackets[0] == subPackets[1] ? 1 : 0;
                        break;
                }
            }
            return result;
        }

        private int ParseBinary(ref int loc, int bitCount)
        {
            var rawVal = _packets.Substring(loc, bitCount);
            loc += bitCount;
            return ParseBinary(rawVal);
        }

        private int ParseBinary(string rawVal)
            => Convert.ToInt32(rawVal, 2);

        private string ConvertToBinaryString(string input)
        {
            var sb = new StringBuilder(input.Length * 4);

            foreach (char c in input)
                sb.Append(Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'));

            return sb.ToString();
        }
    }
}