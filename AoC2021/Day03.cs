using System;
using System.Collections.Generic;

namespace AoC2021
{
    public class Day03 : DayBase, IDay
    {
        private enum PopularBit
        {
            Ones,
            Zeroes,
            Tied
        }

        private readonly IList<string> _numbers;
        private readonly int _numberLen;

        public Day03(string filename)
        {
            _numbers = TextFileStringList(filename);
            _numberLen = _numbers[0].Length;
        }

        public Day03() : this("Day03.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"Part1: {PowerConsumption()}");
            Console.WriteLine($"Part2: {LifeSupportRating()}");
        }

        public int PowerConsumption()
        {
            var gamma = 0;
            var epsilon = 0;
            var multiplier = 1;

            for (var i = _numberLen - 1; i >= 0; i--)
            {
                var popularBit = MorePopularBit(_numbers, i);

                switch (popularBit)
                {
                    case PopularBit.Ones:
                        gamma += multiplier;
                        break;

                    case PopularBit.Zeroes:
                        epsilon += multiplier;
                        break;

                    default:
                        throw new Exception("Unexpected popularBit");
                }

                multiplier *= 2;
            }
            return gamma * epsilon;
        }

        public int LifeSupportRating()
        {
            var oxyGenRating = LocateValue(false);
            var co2Rating = LocateValue(true);
            return oxyGenRating * co2Rating;
        }

        private int LocateValue(bool flip)
        {
            var numbers = new List<string>(_numbers);

            for (var i = 0; i < _numberLen; i++)
            {
                if (numbers.Count == 1)
                    break;

                var popularBit = MorePopularBit(numbers, i);

                var bitToKeep = popularBit switch
                {
                    PopularBit.Ones => '1',
                    PopularBit.Zeroes => '0',
                    PopularBit.Tied => '1',
                    _ => throw new Exception("Unexpected popularBit")
                };

                if (flip)
                    bitToKeep = bitToKeep == '1' ? '0' : '1';

                var tempList = new List<string>();
                foreach (var number in numbers)
                    if (number[i] == bitToKeep)
                        tempList.Add(number);
                numbers = tempList;
            }
            if (numbers.Count != 1)
                throw new Exception("Expected 1 element");

            return Convert.ToInt32(numbers[0], 2);
        }

        private PopularBit MorePopularBit(
            IList<string> numbers,
            int index)
        {
            var excessOnes = 0;
            foreach (var number in numbers)
                excessOnes += number[index] == '1' ? 1 : -1;
            if (excessOnes > 0)
                return PopularBit.Ones;
            if (excessOnes < 0)
                return PopularBit.Zeroes;
            return PopularBit.Tied;
        }
    }
}