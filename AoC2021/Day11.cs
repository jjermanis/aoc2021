using System;
using System.Collections.Generic;
using Grid = System.Collections.Generic.IDictionary<(int, int), int>;

namespace AoC2021
{
    public class Day11 : DayBase, IDay
    {
        private readonly Grid _grid;

        private readonly IList<(int, int)> _neighborOffsets = new List<(int, int)>
        {
            (-1, -1),
            (0, -1),
            (1, -1),
            (-1, 0),
            (1, 0),
            (-1, 1),
            (0, 1),
            (1, 1)
        };

        public Day11(string filename)
            => _grid = TextFileIntGrid(filename);

        public Day11() : this("Day11.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(FlashCountAfter100Steps)}: {FlashCountAfter100Steps()}");
            Console.WriteLine($"{nameof(StepCountForSynchronization)}: {StepCountForSynchronization()}");
        }

        public int FlashCountAfter100Steps()
        {
            var totalCount = 0;
            var curr = _grid;

            for (int i = 0; i < 100; i++)
            {
                int stepCount;
                (curr, stepCount) = ExecuteStep(curr);
                totalCount += stepCount;
            }

            return totalCount;
        }

        public int StepCountForSynchronization()
        {
            var curr = _grid;
            var cellCount = _grid.Count;
            var stepCount = 0;
            while (true)
            {
                stepCount++;
                int flashCount;
                (curr, flashCount) = ExecuteStep(curr);
                if (flashCount == cellCount)
                    return stepCount;
            }
        }

        private (Grid, int) ExecuteStep(Grid grid)
        {
            var curr = grid;
            Grid next = new Dictionary<(int, int), int>();
            var flashes = new HashSet<(int, int)>();
            var locs = new List<(int, int)>();

            // Increase each energy level by one
            foreach (var key in curr.Keys)
            {
                next[key] = curr[key] + 1;
                locs.Add(key);
            }

            // Energy levels greater than 9 flash and increase neighbor enery by one
            // Repeat until no octopuses flash
            bool hasHadFlash;
            do
            {
                hasHadFlash = false;
                foreach (var loc in locs)
                    if (next[loc] > 9 && !flashes.Contains(loc))
                    {
                        hasHadFlash = true;
                        flashes.Add(loc);
                        IncreaseNeighborEnergy(next, loc);
                    }
            } while (hasHadFlash);

            // Everyone that flashed goes down to 0
            foreach (var key in flashes)
                next[key] = 0;

            return (next, flashes.Count);
        }

        private void IncreaseNeighborEnergy(Grid grid, (int, int) loc)
        {
            foreach (var offset in _neighborOffsets)
            {
                var neighborLoc = (loc.Item1 + offset.Item1, loc.Item2 + offset.Item2);
                if (grid.ContainsKey(neighborLoc))
                    grid[neighborLoc]++;
            }
        }
    }
}