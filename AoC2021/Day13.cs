using System;
using System.Collections.Generic;
using Dots = System.Collections.Generic.HashSet<(int, int)>;

namespace AoC2021
{
    public class Day13 : DayBase, IDay
    {
        private readonly Dots _startDots;
        private readonly IList<Fold> _folds;

        public Day13(string filename)
        {
            _startDots = new Dots();
            _folds = new List<Fold>();
            ParseInput(TextFileLines(filename));
        }

        public Day13() : this("Day13.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(FirstFoldDotCount)}: {FirstFoldDotCount()}");
            Console.WriteLine($"Note: {nameof(CompleteInstructions)} is different for this problem. Interpret the following print out");
            Console.WriteLine("as letters - this is the answer.");
            Console.WriteLine($"{nameof(CompleteInstructions)}: {CompleteInstructions()}");
        }

        public int FirstFoldDotCount()
        {
            var next = FoldPaper(_startDots, _folds[0]);
            return next.Count;
        }

        public int CompleteInstructions()
        {
            Dots curr = _startDots;
            foreach (var fold in _folds)
                curr = FoldPaper(curr, fold);
            PrintPaper(curr);
            return curr.Count;
        }

        private Dots FoldPaper(Dots dots, Fold fold)
        {
            var result = new Dots();

            if (fold.Axis == 'x')
            {
                foreach (var dot in dots)
                {
                    if (dot.Item1 < fold.Location)
                        result.Add(dot);
                    else
                        result.Add((2 * fold.Location - dot.Item1, dot.Item2));
                }
            }
            else
            {
                foreach (var cell in dots)
                {
                    if (cell.Item2 < fold.Location)
                        result.Add(cell);
                    else
                        result.Add((cell.Item1, (2 * fold.Location - cell.Item2)));
                }
            }
            return result;
        }

        private void PrintPaper(Dots dots)
        {
            var maxX = 0;
            var maxY = 0;
            foreach (var cell in dots)
            {
                maxX = Math.Max(maxX, cell.Item1);
                maxY = Math.Max(maxY, cell.Item2);
            }

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    bool isSet = dots.Contains((x, y));
                    Console.Write(isSet ? 'X' : ' ');
                }
                Console.WriteLine();
            }
        }

        private void ParseInput(IEnumerable<string> lines)
        {
            var finishedDots = false;
            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    finishedDots = true;
                    continue;
                }
                if (!finishedDots)
                {
                    var tokens = line.Split(',');
                    _startDots.Add((int.Parse(tokens[0]), int.Parse(tokens[1])));
                }
                else
                {
                    var parse1 = line.Split(' ');
                    var tokens = parse1[2].Split('=');
                    _folds.Add(new Fold()
                    {
                        Axis = tokens[0][0],
                        Location = int.Parse(tokens[1])
                    });
                }
            }
        }

        private class Fold
        {
            public char Axis { get; set; }
            public int Location { get; set; }
        }
    }
}