using System;

using Pixels = System.Collections.Generic.HashSet<(int, int)>;

namespace AoC2021
{
    public class Day20 : DayBase, IDay
    {
        private readonly string _algorithm;
        private readonly Pixels _pixels;
        private readonly int _startWidth;
        private readonly int _startHeight;

        public Day20(string filename)
        {
            (_algorithm, _pixels, _startWidth, _startHeight) = ParseInput(filename);
        }

        public Day20() : this("Day20.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(PixelCountAfter2Rounds)}: {PixelCountAfter2Rounds()}");
            Console.WriteLine($"{nameof(PixelCountAfter50Rounds)}: {PixelCountAfter50Rounds()}");
        }

        public int PixelCountAfter2Rounds()
            => PixelCount(2);

        public int PixelCountAfter50Rounds()
            => PixelCount(50);

        private int PixelCount(int roundCount)
        {
            var curr = _pixels;
            for (int i = 1; i <= roundCount; i++)
                curr = Enhance(curr, i);
            return curr.Count;
        }

        private Pixels Enhance(Pixels curr, int iterRound)
        {
            var result = new Pixels();
            for (var x = LeftBound(iterRound); x <= RightBound(iterRound); x++)
                for (var y = TopBound(iterRound); y <= BottomBound(iterRound); y++)
                {
                    if (IsPixelSetNextRound(curr, x, y, iterRound))
                        result.Add((x, y));
                }
            return result;
        }

        private bool IsPixelSetNextRound(Pixels curr, int x, int y, int iterRound)
        {
            var algoIndex =
                256 * IsPixelCurrentlyLit(curr, x - 1, y - 1, iterRound) +
                128 * IsPixelCurrentlyLit(curr, x, y - 1, iterRound) +
                64 * IsPixelCurrentlyLit(curr, x + 1, y - 1, iterRound) +
                32 * IsPixelCurrentlyLit(curr, x - 1, y, iterRound) +
                16 * IsPixelCurrentlyLit(curr, x, y, iterRound) +
                8 * IsPixelCurrentlyLit(curr, x + 1, y, iterRound) +
                4 * IsPixelCurrentlyLit(curr, x - 1, y + 1, iterRound) +
                2 * IsPixelCurrentlyLit(curr, x, y + 1, iterRound) +
                1 * IsPixelCurrentlyLit(curr, x + 1, y + 1, iterRound);
            return _algorithm[algoIndex] == '#';
        }

        private int IsPixelCurrentlyLit(Pixels pixels, int x, int y, int iterRound)
        {
            if (_algorithm[0] == '#')
                if (x <= LeftBound(iterRound) || x >= RightBound(iterRound) ||
                    y <= TopBound(iterRound) || y >= BottomBound(iterRound))
                    return (iterRound + 1) % 2;

            return pixels.Contains((x, y)) ? 1 : 0;
        }

        private void PrintPixels(Pixels pixels, int iterRound)
        {
            for (var y = -iterRound; y <= BottomBound(iterRound); y++)
            {
                for (var x = -iterRound; x <= RightBound(iterRound); x++)
                    Console.Write(pixels.Contains((x, y)) ? '#' : '.');
                Console.WriteLine();
            }
        }

        private int LeftBound(int iterRound)
            => -iterRound;

        private int RightBound(int iterRound)
            => _startWidth + iterRound + 1;

        private int TopBound(int iterRound)
            => -iterRound;

        private int BottomBound(int iterRound)
            => _startHeight + iterRound + 1;

        private (string algorithm, Pixels pixels, int width, int height) ParseInput(string filename)
        {
            var lines = TextFileStringList(filename);

            string algo = lines[0];

            var pixels = new Pixels();
            var width = 0;
            for (int y = 2; y < lines.Count; y++)
            {
                var currLine = lines[y];
                var currLen = currLine.Length;
                width = Math.Max(width, currLen);
                for (int x = 0; x < currLen; x++)
                {
                    if (currLine[x] == '#')
                        pixels.Add((x, y));
                }
            }

            return (algo, pixels, width, lines.Count - 2);
        }
    }
}