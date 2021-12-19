using System;
using System.Collections.Generic;
using ConnectionMap = System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<string>>;

namespace AoC2021
{
    public class Day12 : DayBase, IDay
    {
        private readonly ConnectionMap _map;

        public Day12(string filename)
            => _map = ParseConnections(TextFileLines(filename));

        public Day12() : this("Day12.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"{nameof(SimplePathCount)}: {SimplePathCount()}");
            Console.WriteLine($"{nameof(PathCountWithOneRevisit)}: {PathCountWithOneRevisit()}");
        }

        public int SimplePathCount()
            => FindUniquePathCount(false);

        public int PathCountWithOneRevisit()
            => FindUniquePathCount(true);

        private int FindUniquePathCount(bool allowOneRevisit)
        {
            int result = 0;
            var paths = new Stack<Path>();
            var start = new Path(allowOneRevisit);
            start.AddMove("start");
            paths.Push(start);

            while (paths.Count > 0)
            {
                var currPath = paths.Pop();
                var currPos = currPath.CurrPosition();
                var candidateMoves = _map[currPos];
                foreach (var move in candidateMoves)
                {
                    if (move.Equals("end"))
                        result++;
                    else if (move.Equals("start"))
                        continue;
                    else if (currPath.IsMoveValid(move))
                    {
                        var newPath = new Path(currPath);
                        newPath.AddMove(move);
                        paths.Push(newPath);
                    }
                }
            }
            return result;
        }

        private ConnectionMap ParseConnections(IEnumerable<string> rawConnections)
        {
            var result = new Dictionary<string, IList<string>>();
            foreach (var rawStr in rawConnections)
            {
                var temp = rawStr.Split('-');
                AddConnection(result, temp[0], temp[1]);
                AddConnection(result, temp[1], temp[0]);
            }
            return result;
        }

        private void AddConnection(ConnectionMap map, string src, string dest)
        {
            if (!map.ContainsKey(src))
                map.Add(src, new List<string>());
            map[src].Add(dest);
        }

        private class Path
        {
            private IList<string> _caves;
            private bool _allowOneRevisit;
            private bool _revisitOccured;

            public Path(bool allowOneRevisit)
            {
                _caves = new List<string>();
                _allowOneRevisit = allowOneRevisit;
                _revisitOccured = false;
            }

            public Path(Path src)
            {
                _caves = new List<string>(src._caves);
                _allowOneRevisit = src._allowOneRevisit;
                _revisitOccured = src._revisitOccured;
            }

            public bool IsMoveValid(string move)
            {
                // Caves with CAPS are valid
                if (move[0] < 'a')
                    return true;

                if (!_caves.Contains(move))
                    return true;

                if (_allowOneRevisit && !_revisitOccured)
                    return true;

                return false;
            }

            public void AddMove(string move)
            {
                if (move[0] >= 'a' && _caves.Contains(move))
                    _revisitOccured = true;
                _caves.Add(move);
            }

            public string CurrPosition()
                => _caves[_caves.Count - 1];

            public override string ToString()
            {
                return String.Join(',', _caves);
            }
        }
    }
}