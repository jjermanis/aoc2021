using System;
using System.Collections.Generic;

namespace AoC2021
{
    public class Day10 : DayBase, IDay
    {
        private readonly Dictionary<char, char> _openingsMap = new Dictionary<char, char>
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' }
        };

        private readonly Dictionary<char, int> _closingSyntaxScores = new Dictionary<char, int>
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
        };

        private readonly Dictionary<char, int> _closingCompletionScores = new Dictionary<char, int>
        {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 }
        };

        private readonly IList<string> _lines;

        public Day10(string filename)
            => _lines = TextFileStringList(filename);

        public Day10() : this("Day10.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(SyntaxErrorScore)}: {SyntaxErrorScore()}");
            Console.WriteLine($"{nameof(CompletionScoreMedian)}: {CompletionScoreMedian()}");
        }

        public int SyntaxErrorScore()
        {
            var result = 0;

            foreach (var line in _lines)
                result += SyntaxScoreLine(line);

            return result;
        }

        public long CompletionScoreMedian()
        {
            var scores = new List<long>();

            foreach (var line in _lines)
            {
                var score = AutocompleteScoreLine(line);
                if (score > 0)
                    scores.Add(score);
            }
            scores.Sort();

            return scores[scores.Count / 2];
        }

        private int SyntaxScoreLine(string line)
        {
            var stack = new Stack<char>();
            foreach (char c in line)
            {
                if (_openingsMap.ContainsKey(c))
                    stack.Push(_openingsMap[c]);
                else
                {
                    var expected = stack.Pop();
                    if (c != expected)
                        return _closingSyntaxScores[c];
                }
            }
            return 0;
        }

        private long AutocompleteScoreLine(string line)
        {
            var stack = new Stack<char>();
            foreach (char c in line)
            {
                if (_openingsMap.ContainsKey(c))
                    stack.Push(_openingsMap[c]);
                else
                {
                    var expected = stack.Pop();
                    if (c != expected)
                        return 0;
                }
            }
            var result = 0L;
            while (stack.Count != 0)
            {
                result *= 5;
                result += _closingCompletionScores[stack.Pop()];
            }
            return result;
        }
    }
}