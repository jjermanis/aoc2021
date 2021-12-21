using System;
using System.Collections.Generic;

using Grid = System.Collections.Generic.IDictionary<(int, int), int>;

namespace AoC2021
{
    // This is definitely nowhere near the optimal approach. Knowing what I know now (working
    // on the actual problems not just the sample) I would have used Dijkstra's algorithm.
    public class Day15 : DayBase, IDay
    {
        private readonly Grid _costs;
        private readonly int _width;
        private readonly int _height;

        public Day15(string filename)
        {
            var lines = TextFileStringList(filename);
            _costs = TextFileIntGrid(lines);
            _width = lines[0].Length;
            _height = lines.Count;
        }

        public Day15() : this("Day15.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(MinRiskSmallGrid)}: {MinRiskSmallGrid()}");
            Console.WriteLine($"{nameof(MinRiskLargeGrid)}: {MinRiskLargeGrid()}");
        }

        public int MinRiskSmallGrid()
        {
            var distances = SimpleDistances(1);
            return distances[(_width - 1, _height - 1)];
        }

        public int MinRiskLargeGrid()
        {
            var distances = SimpleDistances(5);
            for (int i = 0; i < 5; i++)
                AdjustDistances(distances, 5);
            return distances[(_width * 5 - 1, _height * 5 - 1)];
        }

        private Grid SimpleDistances(int repeatedCount)
        {
            var distances = new Dictionary<(int, int), int>() { { (0, 0), 0 } };
            for (int x = 1; x < _width * repeatedCount; x++)
                distances[(x, 0)] = distances[(x - 1, 0)] + LookupCost(x, 0);
            for (int y = 1; y < _height * repeatedCount; y++)
                distances[(0, y)] = distances[(0, y - 1)] + LookupCost(0, y);

            for (int y = 1; y < _height * repeatedCount; y++)
                for (int x = 1; x < _height * repeatedCount; x++)
                {
                    distances[(x, y)] = Math.Min(distances[(x - 1, y)], distances[(x, y - 1)])
                        + LookupCost(x, y);
                }
            return distances;
        }

        private void AdjustDistances(Grid distances, int repeatedCount)
        {
            for (int y = 1; y < _height * repeatedCount - 1; y++)
                for (int x = 1; x < _height * repeatedCount - 1; x++)
                {
                    var curr = LookupCost(x, y);

                    distances[(x, y)] = Math.Min(
                        distances[(x, y)],
                        distances[(x + 1, y)] + curr);
                    distances[(x, y)] = Math.Min(
                        distances[(x, y)],
                        distances[(x, y + 1)] + curr);
                }
            for (int y = 1; y < _height * repeatedCount; y++)
                for (int x = 1; x < _height * repeatedCount; x++)
                {
                    var curr = LookupCost(x, y);

                    distances[(x, y)] = Math.Min(
                        distances[(x, y)],
                        distances[(x - 1, y)] + curr);
                    distances[(x, y)] = Math.Min(
                        distances[(x, y)],
                        distances[(x, y - 1)] + curr);
                }
        }

        private int LookupCost(int x, int y)
        {
            var dupes = (x / _width) + (y / _height);
            var result = _costs[(x % _width, y % _height)] + dupes;
            if (result > 9)
                result -= 9;
            return result;
        }
    }
}