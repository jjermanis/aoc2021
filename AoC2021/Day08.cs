using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day08 : DayBase, IDay
    {
        private readonly IEnumerable<string> _lines;

        public Day08(string filename)
            => _lines = TextFileLines(filename);

        public Day08() : this("Day08.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(EasyDigitCount)}: {EasyDigitCount()}");
            Console.WriteLine($"{nameof(OutputSum)}: {OutputSum()}");
        }

        public int EasyDigitCount()
        {
            var result = 0;
            var validLengths = new List<int> { 2, 3, 4, 7 };
            var outputs = _lines.Select(x => x.Split('|')[1]);
            foreach (var output in outputs)
            {
                var tokens = output.Split(' ');
                result += tokens.Where(t => validLengths.Contains(t.Length)).Count();
            }
            return result;
        }

        public int OutputSum()
        {
            var result = 0;

            foreach (var line in _lines)
            {
                var split = line.Split('|');
                var digits = ParseDigits(split[0]);
                var output = ParseDigits(split[1]);

                // Find 1,4,7,8
                var lookup = GetEasyDigits(digits);

                // Find 9 - it's the remaining digit that has the same segments as 4 and 7
                foreach (var digit in digits)
                {
                    if (digit.Value.HasValue)
                        continue;
                    if (((digit.Bits | lookup[4].Bits) == digit.Bits) &&
                        ((digit.Bits | lookup[7].Bits) == digit.Bits))
                    {
                        lookup[9] = digit;
                        digit.Value = 9;
                        break;
                    }
                }

                // Find 0 - it's the remaining digit with 6 segments and same as 7
                foreach (var digit in digits)
                {
                    if (digit.Value.HasValue)
                        continue;
                    if ((digit.SegmentCount == 6) &&
                        ((digit.Bits | lookup[7].Bits) == digit.Bits))
                    {
                        lookup[0] = digit;
                        digit.Value = 0;
                        break;
                    }
                }

                // Find 6 - it's the remaining digit with 6 segments
                foreach (var digit in digits)
                {
                    if (digit.Value.HasValue)
                        continue;
                    if (digit.SegmentCount == 6)
                    {
                        lookup[6] = digit;
                        digit.Value = 6;
                        break;
                    }
                }

                // Find 3 - it has 5 segments, and the same as 7
                foreach (var digit in digits)
                {
                    if (digit.Value.HasValue)
                        continue;
                    if ((digit.SegmentCount == 5) &&
                        ((digit.Bits | lookup[7].Bits) == digit.Bits))
                    {
                        lookup[3] = digit;
                        digit.Value = 3;
                        break;
                    }
                }

                // Find 5 - 9 has the same segments as it
                foreach (var digit in digits)
                {
                    if (digit.Value.HasValue)
                        continue;
                    if ((digit.Bits | lookup[9].Bits) == lookup[9].Bits)
                    {
                        lookup[5] = digit;
                        digit.Value = 5;
                        break;
                    }
                }

                // 2 is the last remaining one
                foreach (var digit in digits)
                {
                    if (digit.Value.HasValue)
                        continue;
                    lookup[2] = digit;
                    digit.Value = 2;
                    break;
                }

                // Translate output
                var curr = 0;
                foreach (var digit in output)
                {
                    foreach (var inputDigit in digits)
                    {
                        if (inputDigit.Bits == digit.Bits)
                        {
                            curr *= 10;
                            curr += inputDigit.Value.Value;
                            break;
                        }
                    }
                }
                result += curr;
            }
            return result;
        }

        private IList<Digit> ParseDigits(string raw)
            => raw.Split(' ').Where(x => x.Length > 0).Select(d => new Digit(d)).ToList();

        private IDictionary<int, Digit> GetEasyDigits(IList<Digit> digits)
        {
            var result = new Dictionary<int, Digit>();
            foreach (var digit in digits)
            {
                switch (digit.SegmentCount)
                {
                    case 2:
                        result[1] = digit;
                        digit.Value = 1;
                        break;

                    case 3:
                        result[7] = digit;
                        digit.Value = 7;
                        break;

                    case 4:
                        result[4] = digit;
                        digit.Value = 4;
                        break;

                    case 7:
                        result[8] = digit;
                        digit.Value = 8;
                        break;
                }
            }
            return result;
        }

        private class Digit
        {
            private static int[] _POWERS = new int[] { 1, 2, 4, 8, 16, 32, 64 };
            public int Bits;
            public int? Value;
            public int SegmentCount;
            public string Raw;

            public Digit(string value)
            {
                Raw = value;
                SegmentCount = value.Length;
                foreach (char c in value)
                    Bits += _POWERS[c - 'a'];
            }

            public override string ToString()
            {
                return Raw;
            }
        }
    }
}