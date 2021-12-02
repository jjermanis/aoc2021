using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day01 : DayBase, IDay
    {
        private readonly IList<int> _readings;

        public Day01(string filename)
            => _readings = TextFileIntList(filename);

        public Day01() : this("Day01.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"Part1: {SimpleMeasurement()}");
            Console.WriteLine($"Part2: {WindowMeasurement()}");
        }

        public int SimpleMeasurement()
        {
            return AscendingCount(_readings);
        }

        public int WindowMeasurement()
        {
            var windows = new int[_readings.Count].ToList();
            for (var i = 0; i < _readings.Count; i++)
            {
                var curr = _readings[i];
                AddToWindow(curr, windows, i);
                AddToWindow(curr, windows, i - 1);
                AddToWindow(curr, windows, i - 2);
            }

            return AscendingCount(windows);
        }

        private int AscendingCount(IList<int> readings)
        {
            var result = 0;
            var last = int.MaxValue;
            foreach (var reading in readings)
            {
                if (reading > last)
                    result++;
                last = reading;
            }
            return result;
        }

        private void AddToWindow(int value, IList<int> windows, int index)
        {
            if (index >= 0)
                windows[index] += value;
        }
    }
}