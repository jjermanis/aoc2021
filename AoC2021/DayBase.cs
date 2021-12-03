using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2021
{
    public abstract class DayBase
    {
        private const string FILE_PATH = @"..\..\..\..\AoC2021\Inputs\";

        protected IEnumerable<string> TextFileLines(string fileName)
            => File.ReadLines(FILE_PATH + fileName);

        protected IList<string> TextFileStringList(string fileName)
            => TextFileLines(fileName).ToList();

        protected IEnumerable<int> TextFileInts(string fileName)
            => TextFileLines(fileName).Select(m => int.Parse(m));

        protected IList<int> TextFileIntList(string fileName)
            => TextFileInts(fileName).ToList();

        protected IEnumerable<string[]> TextFileTokens(string fileName, char delimiter)
            => TextFileLines(fileName).Select(x => x.Split(delimiter));

        protected string TextFile(string fileName)
            => File.ReadAllText(FILE_PATH + fileName);
    }
}