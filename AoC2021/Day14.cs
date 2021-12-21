using System;
using System.Collections.Generic;
using PairCounts = System.Collections.Generic.Dictionary<string, long>;

namespace AoC2021
{
    public class Day14 : DayBase, IDay
    {
        private readonly PairCounts _startPairCounts;
        private readonly IDictionary<string, string> _insertionMap;
        private char _first;
        private char _last;

        public Day14(string filename)
        {
            _startPairCounts = new PairCounts();
            _insertionMap = new Dictionary<string, string>();
            ParseInput(TextFileLines(filename));
        }

        public Day14() : this("Day14.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(CommonRangeAfter10Steps)}: {CommonRangeAfter10Steps()}");
            Console.WriteLine($"{nameof(CommonRangeAfter40Steps)}: {CommonRangeAfter40Steps()}");
        }

        public long CommonRangeAfter10Steps()
            => DifferenceMostLeastCommon(10);

        public long CommonRangeAfter40Steps()
            => DifferenceMostLeastCommon(40);

        private long DifferenceMostLeastCommon(int stepCount)
        {
            var pairCounts = _startPairCounts;
            for (int i = 0; i < stepCount; i++)
                pairCounts = ExecuteStep(pairCounts);

            var compundCounts = GetCompoundCounts(pairCounts);
            var min = long.MaxValue;
            var max = 0L;
            foreach (var count in compundCounts)
            {
                if (count != 0)
                {
                    min = Math.Min(min, count);
                    max = Math.Max(max, count);
                }
            }
            return max - min;
        }

        private PairCounts ExecuteStep(PairCounts pairCounts)
        {
            var result = new Dictionary<string, long>();

            foreach (var pairCount in pairCounts)
            {
                var pair = pairCount.Key;
                var insertion = _insertionMap[pairCount.Key];
                AddPairCount(result, pair[0] + insertion, pairCount.Value);
                AddPairCount(result, insertion + pair[1], pairCount.Value);
            }
            return result;
        }

        private long[] GetCompoundCounts(PairCounts pairCounts)
        {
            var result = new long[26];
            foreach (var pairCount in pairCounts)
            {
                result[pairCount.Key[0] - 'A'] += pairCount.Value;
                result[pairCount.Key[1] - 'A'] += pairCount.Value;
            }

            // Everything has been double-counted... except the very first and last chars
            result[_first - 'A']++;
            result[_last - 'A']++;
            for (var i = 0; i < 26; i++)
                result[i] = (result[i]) / 2;

            return result;
        }

        private void ParseInput(IEnumerable<string> lines)
        {
            var readStart = false;
            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    continue;
                }
                if (!readStart)
                {
                    for (int i = 0; i < line.Length - 1; i++)
                    {
                        AddPairCount(_startPairCounts, line.Substring(i, 2), 1);
                    }
                    _first = line[0];
                    _last = line[^1];
                    readStart = true;
                }
                else
                {
                    var tokens = line.Split(" -> ");
                    _insertionMap[tokens[0]] = tokens[1];
                }
            }
        }

        private void AddPairCount(PairCounts pairCount, string pair, long count)
        {
            if (!pairCount.ContainsKey(pair))
                pairCount[pair] = 0;
            pairCount[pair] += count;
        }
    }
}