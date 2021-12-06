using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AoC2021
{
    public class Day05 : DayBase, IDay
    {
        private readonly IEnumerable<string> _lines;

        public Day05(string filename)
            => _lines = TextFileLines(filename);

        public Day05() : this("Day05.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(OrthagonalIntersectingPoints)}: {OrthagonalIntersectingPoints()}");
            Console.WriteLine($"{nameof(AllIntersectingPoints)}: {AllIntersectingPoints()}");
        }

        public int OrthagonalIntersectingPoints()
            => IntersectingPointCount(false);

        public int AllIntersectingPoints()
            => IntersectingPointCount(true);

        private int IntersectingPointCount(bool includeDiagonals)
        {
            var grid = new Dictionary<(int, int), int>();

            foreach (var line in _lines)
            {
                var points = ParsePoints(line);

                var a1 = points[0];
                var b1 = points[1];
                var a2 = points[2];
                var b2 = points[3];

                // Only process orthogonal lines
                if (!includeDiagonals && a1 != a2 && b1 != b2)
                    continue;

                var adelta = Math.Sign(a2 - a1);
                var bdelta = Math.Sign(b2 - b1);

                while (a1 != a2 || b1 != b2)
                {
                    IncrementGrid(grid, a1, b1);
                    a1 += adelta;
                    b1 += bdelta;
                }
                IncrementGrid(grid, a1, b1);
            }

            var result = 0;
            foreach (var count in grid.Values)
                if (count >= 2)
                    result++;
            return result;
        }

        private void IncrementGrid(IDictionary<(int, int), int> grid, int a, int b)
        {
            if (!grid.ContainsKey((a, b)))
                grid[(a, b)] = 0;
            grid[(a, b)]++;
        }

        private List<int> ParsePoints(string line)
        {
            var result = new List<int>();
            var m = Regex.Match(line, @"(\d*),(\d*) -> (\d*),(\d*)");
            for (var i = 1; i <= 4; i++)
                result.Add(int.Parse(m.Groups[i].Value));
            return result;
        }
    }
}