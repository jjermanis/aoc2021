using System;
using System.Linq;

namespace AoC2021
{
    public class Day06 : DayBase, IDay
    {
        private readonly string _file;

        public Day06(string filename)
            => _file = TextFile(filename);

        public Day06() : this("Day06.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(PopulationAfter80Days)}: {PopulationAfter80Days()}");
            Console.WriteLine($"{nameof(PopulationAfter256Days)}: {PopulationAfter256Days()}");
        }

        public long PopulationAfter80Days()
            => CalcuatePopulation(80);

        public long PopulationAfter256Days()
            => CalcuatePopulation(256);

        private long CalcuatePopulation(int generations)
        {
            var rawAges = _file.Split(',').Select(x => int.Parse(x));

            var curr = new long[9];
            foreach (var age in rawAges)
                curr[age]++;

            for (int gen = 0; gen < generations; gen++)
            {
                var next = new long[9];

                for (int i = 1; i < 9; i++)
                    next[i - 1] = curr[i];
                next[6] += curr[0];
                next[8] = curr[0];

                curr = next;
            }

            return curr.Sum();
        }
    }
}