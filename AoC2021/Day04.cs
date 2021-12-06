using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day04 : DayBase, IDay
    {
        private readonly IList<string> _fileLines;

        public Day04(string filename)
            => _fileLines = TextFileStringList(filename);

        public Day04() : this("Day04.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(FirstWinnerScore)}: {FirstWinnerScore()}");
            Console.WriteLine($"{nameof(LastWinnerScore)}: {LastWinnerScore()}");
        }

        public int FirstWinnerScore()
        {
            (var numbers, var boards) = InitElements();
            return PlayGame(numbers, boards, 1);
        }

        public int LastWinnerScore()
        {
            (var numbers, var boards) = InitElements();
            return PlayGame(numbers, boards, boards.Count);
        }

        private (IList<int>, IList<Board>) InitElements()
        {
            // First line is the numbers that are called, in order
            var numbers = _fileLines[0].Split(',').Select(x => int.Parse(x)).ToList();

            // The remainder are the boards. Each board is 5x5
            var boards = new List<Board>();
            for (var i = 2; i < _fileLines.Count; i += 6)
                boards.Add(new Board(_fileLines, i));
            return (numbers, boards);
        }

        private int PlayGame(
            IList<int> numbers,
            IList<Board> boards,
            int winCountNeeded)
        {
            var winCount = 0;
            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    if (board.HasWon())
                        continue;

                    if (board.NumberMakesWinner(number))
                    {
                        winCount++;

                        if (winCount == winCountNeeded)
                            return board.ValueUnmarkedSquares() * number;
                    }
                }
            }
            throw new Exception("Not enough wins found");
        }

        private class Board
        {
            private bool _hasWon;
            private int[,] _numbers = new int[5, 5];
            private bool[,] _isMarked = new bool[5, 5];

            public Board(IList<string> rawFile, int offset)
            {
                for (int a = 0; a < 5; a++)
                {
                    var curr = rawFile[a + offset].Split().Where(s => s.Length > 0).Select(x => int.Parse(x)).ToList();
                    for (int b = 0; b < 5; b++)
                        _numbers[a, b] = curr[b];
                }
            }

            public bool NumberMakesWinner(int number)
            {
                if (_hasWon)
                    return false; // Skip - board has already won

                for (int a = 0; a < 5; a++)
                    for (int b = 0; b < 5; b++)
                        if (_numbers[a, b] == number)
                        {
                            _isMarked[a, b] = true;
                            if (CheckWinner(a, b))
                            {
                                _hasWon = true;
                                return true;
                            }
                        }
                return false;
            }

            public int ValueUnmarkedSquares()
            {
                var result = 0;
                for (int a = 0; a < 5; a++)
                    for (int b = 0; b < 5; b++)
                        if (!_isMarked[a, b])
                            result += _numbers[a, b];
                return result;
            }

            public bool HasWon()
                => _hasWon;

            private bool CheckWinner(int a, int b)
            {
                var holeFound = false;
                for (int bb = 0; bb < 5; bb++)
                    if (!_isMarked[a, bb])
                        holeFound = true;
                if (!holeFound)
                    return true;

                holeFound = false;
                for (int aa = 0; aa < 5; aa++)
                    if (!_isMarked[aa, b])
                        holeFound = true;
                return !holeFound;
            }
        }
    }
}