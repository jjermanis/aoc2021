using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AoC2021
{
    public class Day17 : DayBase, IDay
    {
        private int _minTarX;
        private int _maxTarX;
        private int _minTarY;
        private int _maxTarY;

        public Day17(string filename)
        {
            ParseInput(TextFile(filename));
        }

        public Day17() : this("Day17.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(HighestPosition)}: {HighestPosition()}");
            Console.WriteLine($"{nameof(ValidVelocityCount)}: {ValidVelocityCount()}");
        }

        public int HighestPosition()
        {
            (_, var result) = HighestCase();
            return result;
        }

        public int ValidVelocityCount()
        {
            // Every target square can be used for a one-turn solution
            var result = (_maxTarX - _minTarX + 1) * (_maxTarY - _minTarY + 1);

            // Calculate min dx that can make it to the target
            var tempTotal = 0;
            var minDx = 0;
            while (tempTotal <= _minTarX)
                tempTotal += ++minDx;

            // Max dy can be calcualted from the Part 1 solution
            (var maxDy, _) = HighestCase();

            for (int x = minDx; x <= _maxTarX / 2; x++)
                for (int y = _minTarY / 2; y <= maxDy; y++)
                    if (DoesLandInTarget(x, y))
                        result++;
            return result;
        }

        private (int velocity, int maxHeight) HighestCase()
        {
            var maxAltitude = 0;
            var maxVelo = 0;
            var currVelo = 0;
            while (true)
            {
                int currMaxAlt;
                var doesLand = DoesLandInTargetSimple(currVelo, out currMaxAlt);
                if (doesLand)
                {
                    maxVelo = currVelo;
                    maxAltitude = Math.Max(maxAltitude, currMaxAlt);
                }
                else if (currVelo - maxVelo > _maxTarY - _minTarY)
                    return (maxVelo, maxAltitude);
                currVelo++;
            }
        }

        private bool DoesLandInTargetSimple(int velocity, out int maxAltitude)
        {
            maxAltitude = 0;
            var currVelo = velocity;
            int currY = 0;

            while (currY >= _minTarY)
            {
                currY += currVelo;
                currVelo--;
                maxAltitude = Math.Max(maxAltitude, currY);
                if (currY < _minTarY)
                    return false;
                if (currY < _maxTarY)
                    return true;
            }
            return false;
        }

        private bool DoesLandInTarget(int startVelocityX, int startVelocityY)
        {
            var dx = startVelocityX;
            var dy = startVelocityY;
            var x = 0;
            var y = 0;

            while (true)
            {
                x += dx;
                y += dy;

                if (y < _minTarY || x > _maxTarX)
                    return false;
                if (y <= _maxTarY && x >= _minTarX)
                    return true;

                dy--;
                dx -= Math.Sign(dx);
            }
        }

        private void ParseInput(string text)
        {
            var values = new List<int>();
            var m = Regex.Match(text, @"target area: x=(-*\d+)..(-*\d+), y=(-*\d+)..(-*\d+)");
            for (var i = 1; i <= 4; i++)
                values.Add(int.Parse(m.Groups[i].Value));
            _minTarX = values[0];
            _maxTarX = values[1];
            _minTarY = values[2];
            _maxTarY = values[3];
        }
    }
}