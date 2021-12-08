using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day07 : DayBase, IDay
    {
        private readonly int[] _positions;

        public Day07(string filename)
            => _positions = TextFile(filename).Split(',').Select(x => int.Parse(x)).ToArray();

        public Day07() : this("Day07.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(Part1)}: {Part1()}");
            Console.WriteLine($"{nameof(Part2)}: {Part2()}");
        }

        public int Part1()
        {
            Array.Sort(_positions);
            var medIndex = _positions.Length / 2;
            var option1 = CalculateLinearFuel(_positions, _positions[medIndex-1]);
            var option2 = CalculateLinearFuel(_positions, _positions[medIndex-2]);

            return Math.Min(option1, option2);
        }

        public int Part2()
        {
            var result = int.MaxValue;
            int index = 0;
            while (true)
            {
                var curr = CalculateGeometricFuel(_positions, index);
                if (curr > result)
                    return result;
                result = curr;
                index++;
            }
        }

        private int CalculateLinearFuel(int[] positions, int destination)
        {
            var result = 0;
            foreach (var position in positions)
                result += Math.Abs(position - destination);
            return result;
        }

        private int CalculateGeometricFuel(int[] positions, int destination)
        {
            var result = 0;
            foreach (var position in positions)
            {
                var distance = Math.Abs(position - destination);
                result += distance * (distance + 1) / 2;
            }
            return result;
        }
    }
}
