using System;
using System.Collections.Generic;

namespace AoC2021
{
    public class Day18 : DayBase, IDay
    {
        private readonly IList<string> _lines;

        public Day18(string filename)
            => _lines = TextFileStringList(filename);

        public Day18() : this("Day18.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(TotalSumMagnitude)}: {TotalSumMagnitude()}");
            Console.WriteLine($"{nameof(MaxSumMagnitude)}: {MaxSumMagnitude()}");
        }

        public int TotalSumMagnitude()
        {
            var sum = SnailfishNumberSum();
            return sum.Magnitude();
        }

        public int MaxSumMagnitude()
        {
            var result = 0;
            for (var a = 0; a < _lines.Count; a++)
                for (var b = a + 1; b < _lines.Count; b++)
                {
                    result = Math.Max(result, SnailfishNumberSumMagnitude(_lines[a], _lines[b]));
                    result = Math.Max(result, SnailfishNumberSumMagnitude(_lines[b], _lines[a]));
                }
            return result;
        }

        public SnailfishNumber SnailfishNumberSum()
        {
            SnailfishNumber result = null;

            foreach (var line in _lines)
            {
                var curr = new SnailfishNumber(line);

                if (result == null)
                    result = curr;
                else
                {
                    var temp = result.Add(curr);
                    result = new SnailfishNumber(temp.ToString());
                }
            }

            return result;
        }

        private int SnailfishNumberSumMagnitude(string s1, string s2)
        {
            var sn1 = new SnailfishNumber(s1);
            var sn2 = new SnailfishNumber(s2);
            return sn1.Add(sn2).Magnitude();
        }

        public class SnailfishNumber
        {
            // Left and right elements if SnailfishNumber is a pair
            private SnailfishNumber Left;

            private SnailfishNumber Right;

            // Value if SnailfishNumber is a regular number
            private int? RegularNumber;

            // References to the regular numbers that would appear to the left and right
            // of a regular number is printed
            private SnailfishNumber LeftRegularNeighbor;

            private SnailfishNumber RightRegularNeighbor;

            // Helper for populating left and right neighbors
            private static SnailfishNumber PrevRegularNumber;

            public SnailfishNumber()
            {
            }

            public SnailfishNumber(string line) : this(line, null)
            {
            }

            public SnailfishNumber(
                string numberText,
                SnailfishNumber parent)
            {
                if (!numberText.Contains(','))
                {
                    // No pair - this is a regular number
                    RegularNumber = int.Parse(numberText);

                    if (PrevRegularNumber != null)
                    {
                        PrevRegularNumber.RightRegularNeighbor = this;
                        LeftRegularNeighbor = PrevRegularNumber;
                    }
                    PrevRegularNumber = this;
                }
                else
                {
                    // Pair - find the comma that splits this pair, and parse each side
                    var dividerPos = FindDivider(numberText);
                    Left = new SnailfishNumber(numberText.Substring(1, dividerPos - 1), this);
                    var temp = numberText.Substring(dividerPos + 1);
                    Right = new SnailfishNumber(temp.Remove(temp.Length - 1, 1), this);
                }
            }

            public SnailfishNumber Add(SnailfishNumber right)
            {
                var result = new SnailfishNumber()
                {
                    Left = this,
                    Right = right
                };

                while (true)
                {
                    // TODO there is an error in the logic maintaining the neighbors. Workaround: reparse
                    // the SnailfishNumber after every step. This is slow, but avoids the error.
                    if (result.TryExplode())
                    {
                        var temp = result.ToString();
                        result = new SnailfishNumber(temp);
                        continue;
                    }

                    if (result.TrySplit())
                    {
                        var temp = result.ToString();
                        result = new SnailfishNumber(temp);
                        continue;
                    }

                    break;
                }
                return result;
            }

            public int Magnitude()
            {
                if (RegularNumber.HasValue)
                    return RegularNumber.Value;
                else
                    return 3 * Left.Magnitude() + 2 * Right.Magnitude();
            }

            public override string ToString()
            {
                if (RegularNumber.HasValue)
                    return RegularNumber.Value.ToString();
                else
                    return $"[{Left},{Right}]";
            }

            private int FindDivider(string line)
            {
                int braceDepth = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    switch (line[i])
                    {
                        case '[':
                            braceDepth++;
                            break;

                        case ']':
                            braceDepth--;
                            break;

                        case ',':
                            if (braceDepth == 1)
                                return i;
                            break;
                    }
                }

                throw new Exception("Parse error in FindDivider");
            }

            private bool TryExplode()
            {
                var target = FindExplosionTarget(this, 0);

                if (target == null)
                    return false;

                if (target.Left.LeftRegularNeighbor != null)
                {
                    var leftRegularNeighbor = target.Left.LeftRegularNeighbor;
                    leftRegularNeighbor.RegularNumber =
                        leftRegularNeighbor.RegularNumber.Value + target.Left.RegularNumber.Value;
                    leftRegularNeighbor.RightRegularNeighbor = target;
                }
                if (target.Right.RightRegularNeighbor != null)
                {
                    var rightRegularNeighbor = target.Right.RightRegularNeighbor;
                    rightRegularNeighbor.RegularNumber =
                        rightRegularNeighbor.RegularNumber.Value + target.Right.RegularNumber.Value;
                    rightRegularNeighbor.LeftRegularNeighbor = target;
                }
                target.LeftRegularNeighbor = Left.LeftRegularNeighbor;
                target.Left = null;
                target.RightRegularNeighbor = Right.RightRegularNeighbor;
                target.Right = null;
                target.RegularNumber = 0;

                return true;
            }

            private bool TrySplit()
            {
                var target = FindSplitTarget(this);

                if (target == null)
                    return false;

                var val = target.RegularNumber.Value;
                target.Left = new SnailfishNumber();
                target.Left.RegularNumber = val / 2;
                target.Right = new SnailfishNumber();
                target.Right.RegularNumber = (val + 1) / 2;
                target.RegularNumber = null;

                target.Left.LeftRegularNeighbor = target.LeftRegularNeighbor;
                target.LeftRegularNeighbor = null;
                target.Right.RightRegularNeighbor = target.RightRegularNeighbor;
                target.RightRegularNeighbor = null;

                return true;
            }

            private SnailfishNumber FindSplitTarget(SnailfishNumber curr)
            {
                if (curr.RegularNumber.GetValueOrDefault() > 9)
                    return curr;
                if (curr.Left != null)
                {
                    var checkLeft = FindSplitTarget(curr.Left);
                    if (checkLeft != null)
                        return checkLeft;
                }
                if (curr.Right != null)
                {
                    var checkRight = FindSplitTarget(curr.Right);
                    if (checkRight != null)
                        return checkRight;
                }
                return null;
            }

            private SnailfishNumber FindExplosionTarget(SnailfishNumber curr, int depth)
            {
                if (depth == 4 && !curr.RegularNumber.HasValue)
                    return curr;
                if (curr.Left != null)
                {
                    var checkLeft = FindExplosionTarget(curr.Left, depth + 1);
                    if (checkLeft != null)
                        return checkLeft;
                }
                if (curr.Right != null)
                {
                    var checkRight = FindExplosionTarget(curr.Right, depth + 1);
                    if (checkRight != null)
                        return checkRight;
                }
                return null;
            }
        }
    }
}