using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day09 : DayBase, IDay
    {
        private readonly IDictionary<(int, int), int> _grid;

        public Day09(string filename)
            => _grid = CreateGrid(TextFileStringList(filename));

        public Day09() : this("Day09.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(RiskLevelSum)}: {RiskLevelSum()}");
            Console.WriteLine($"{nameof(BiggestBasinsProduct)}: {BiggestBasinsProduct()}");
        }

        public int RiskLevelSum()
        {
            var result = 0;

            foreach ((var a, var b) in _grid.Keys)
                if (IsLowestPoint(a, b))
                    result += _grid[(a, b)] + 1;

            return result;
        }

        public int BiggestBasinsProduct()
        {
            var lowPoints = new List<(int, int)>();

            foreach ((var a, var b) in _grid.Keys)
                if (IsLowestPoint(a, b))
                    lowPoints.Add((a, b));

            var basinSizes = new List<int>();
            foreach ((var a, var b) in lowPoints)
            {
                var currPoints = new HashSet<(int, int)>();
                FloodFill(a, b, currPoints);
                basinSizes.Add(currPoints.Count);
            }
            basinSizes = basinSizes.OrderByDescending(i => i).ToList();

            return basinSizes[0] * basinSizes[1] * basinSizes[2];
        }

        private void FloodFill(int a, int b, HashSet<(int, int)> filled)
        {
            if (!filled.Contains((a, b)) && _grid[(a, b)] != 9)
            {
                filled.Add((a, b));
                if (_grid.ContainsKey((a - 1, b)))
                    FloodFill(a - 1, b, filled);
                if (_grid.ContainsKey((a + 1, b)))
                    FloodFill(a + 1, b, filled);
                if (_grid.ContainsKey((a, b - 1)))
                    FloodFill(a, b - 1, filled);
                if (_grid.ContainsKey((a, b + 1)))
                    FloodFill(a, b + 1, filled);
            }
        }

        private bool IsLowestPoint(int a, int b)
        {
            int value = _grid[(a, b)];
            if (_grid.ContainsKey((a - 1, b)) && _grid[(a - 1, b)] <= value)
                return false;
            if (_grid.ContainsKey((a + 1, b)) && _grid[(a + 1, b)] <= value)
                return false;
            if (_grid.ContainsKey((a, b - 1)) && _grid[(a, b - 1)] <= value)
                return false;
            if (_grid.ContainsKey((a, b + 1)) && _grid[(a, b + 1)] <= value)
                return false;

            return true;
        }

        private IDictionary<(int, int), int> CreateGrid(IList<string> lines)
        {
            var result = new Dictionary<(int, int), int>();
            var lineCount = lines.Count;
            for (int a = 0; a < lineCount; a++)
            {
                var currLine = lines[a];
                var currLen = currLine.Length;
                for (int b = 0; b < currLen; b++)
                {
                    result[(a, b)] = currLine[b] - '0';
                }
            }
            return result;
        }
    }
}