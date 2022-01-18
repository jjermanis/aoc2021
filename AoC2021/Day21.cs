using System;
using System.Collections.Generic;

namespace AoC2021
{
    public class Day21 : DayBase, IDay
    {
        private readonly int _p1Start;
        private readonly int _p2Start;

        // These tuples represent:
        // Item1: a total when rolling a three-sided die 3 times
        // Item2: the number of different ways to roll that total
        private readonly List<(int, int)> _DIRAC_FREQUENCY = new List<(int, int)>
        {
            (3, 1), (4, 3), (5, 6), (6, 7), (7, 6), (8,3), (9,1)
        };

        private readonly IDictionary<DiracGameState, DiracWinCounts> _memo;

        public Day21(string filename)
        {
            var lines = TextFileStringList(filename);
            _p1Start = ParseStartingSpace(lines[0]);
            _p2Start = ParseStartingSpace(lines[1]);
            _memo = new Dictionary<DiracGameState, DiracWinCounts>();
        }

        public Day21() : this("Day21.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(DeterministicLoserProduct)}: {DeterministicLoserProduct()}");
            Console.WriteLine($"{nameof(DiracWinningUniverseCount)}: {DiracWinningUniverseCount()}");
        }

        public int DeterministicLoserProduct()
        {
            var die = 0;
            var rolls = 0;
            var p1Space = _p1Start;
            var p2Space = _p2Start;
            var p1Points = 0;
            var p2Points = 0;

            while (true)
            {
                TakeDerterministicTurn(ref die, ref p1Space, ref p1Points);
                rolls += 3;
                if (p1Points >= 1000)
                    return p2Points * rolls;
                TakeDerterministicTurn(ref die, ref p2Space, ref p2Points);
                rolls += 3;
                if (p2Points >= 1000)
                    return p1Points * rolls;
            }
        }

        public long DiracWinningUniverseCount()
        {
            var start = new DiracGameState
            {
                CurrSpace = _p1Start,
                NextSpace = _p2Start
            };
            var dist = WinsDistribution(start);
            return Math.Max(dist.CurrWins, dist.NextWins);
        }

        private int ParseStartingSpace(string line)
            => int.Parse(line[^2..]);

        private void TakeDerterministicTurn(ref int die, ref int space, ref int points)
        {
            for (var i = 0; i < 3; i++)
            {
                die = (die + 1) % 100;
                space = (space + die) % 10;
            }
            if (space == 0)
                space = 10;
            points += space;
        }

        private DiracWinCounts WinsDistribution(DiracGameState state)
        {
            if (_memo.ContainsKey(state))
                return _memo[state];

            var result = new DiracWinCounts();
            foreach (var diracFreq in _DIRAC_FREQUENCY)
            {
                int rollTotal = diracFreq.Item1;
                int rollFreq = diracFreq.Item2;

                int newSpace = (state.CurrSpace + rollTotal) % 10;
                if (newSpace == 0)
                    newSpace = 10;
                int newTotal = state.CurrScore + newSpace;
                if (newTotal >= 21)
                    result.CurrWins += (long)rollFreq;
                else
                {
                    var updatedMemo = new DiracGameState
                    {
                        CurrScore = state.NextScore,
                        CurrSpace = state.NextSpace,
                        NextScore = newTotal,
                        NextSpace = (int)newSpace
                    };
                    var subDistribution = WinsDistribution(updatedMemo);
                    result.CurrWins += rollFreq * subDistribution.NextWins;
                    result.NextWins += rollFreq * subDistribution.CurrWins;
                }
            }

            _memo[state] = result;
            return result;
        }

        private class DiracGameState
        {
            public int CurrSpace;
            public int CurrScore;
            public int NextSpace;
            public int NextScore;

            public override bool Equals(Object obj)
            {
                var o = (DiracGameState)obj;
                return (CurrScore == o.CurrScore && CurrSpace == o.CurrSpace && NextScore == o.NextScore && NextSpace == o.NextSpace);
            }

            public override int GetHashCode()
                => CurrSpace + 100 * CurrScore + 10000 * NextSpace + 1000000 * NextScore;
        }

        private class DiracWinCounts
        {
            public long CurrWins;
            public long NextWins;
        }
    }
}