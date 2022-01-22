using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2021
{
    public class Day22 : DayBase, IDay
    {
        private readonly IList<RebootStep> _steps;

        public Day22(string filename)
        {
            var lines = TextFileLines(filename);
            _steps = lines.Select(x => ParseLine(x)).ToList();
        }

        public Day22() : this("Day22.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(InitializationProcedureCubeCount)}: {InitializationProcedureCubeCount()}");
            Console.WriteLine($"{nameof(Part2)}: {Part2()}");
        }

        public int InitializationProcedureCubeCount()
        {
            var cuboid = new HashSet<(int, int, int)>();

            foreach (var step in _steps)
            {
                for (var x=Math.Max(step.xLow, -50); x <= Math.Min(step.xHi, 50); x++)
                    for (var y = Math.Max(step.yLow, -50); y <= Math.Min(step.yHi, 50); y++)
                        for (var z = Math.Max(step.zLow, -50); z <= Math.Min(step.zHi, 50); z++)
                        {
                            if (step.IsOn)
                                cuboid.Add((x, y, z));
                            else if (cuboid.Contains((x, y, z)))
                                cuboid.Remove((x, y, z));
                        }
            }
            return cuboid.Count;
        }

        public int Part2()
        {
            return 0;
        }

        private RebootStep ParseLine(string text)
        {
            var values = new List<int>();
            var m = Regex.Match(text, @"(o[fn]+) x=(-*\d+)..(-*\d+),y=(-*\d+)..(-*\d+),z=(-*\d+)..(-*\d+)");

            for (var i = 2; i <= 7; i++)
                values.Add(int.Parse(m.Groups[i].Value));
            return new RebootStep
            {
                IsOn = m.Groups[1].Value == "on",
                xLow = values[0],
                xHi = values[1],
                yLow = values[2],
                yHi = values[3],
                zLow = values[4],
                zHi = values[5],
            };
        }

        class RebootStep
        {
            public bool IsOn;
            public int xLow;
            public int xHi;
            public int yLow;
            public int yHi;
            public int zLow;
            public int zHi;

        }
    }
}