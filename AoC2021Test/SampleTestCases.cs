using AoC2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoC2021Test
{
    [TestClass]
    public class SampleTestCases
    {
        [TestMethod]
        public void Day03()
        {
            var d = new Day03("Day03Test.txt");
            Assert.AreEqual(198, d.PowerConsumption());
            Assert.AreEqual(230, d.LifeSupportRating());
        }

        [TestMethod]
        public void Day05()
        {
            var d = new Day05("Day05Test.txt");
            Assert.AreEqual(5, d.OrthagonalIntersectingPoints());
            Assert.AreEqual(12, d.AllIntersectingPoints());
        }

        [TestMethod]
        public void Day08()
        {
            var d = new Day08("Day08Test.txt");
            Assert.AreEqual(26, d.EasyDigitCount());
            Assert.AreEqual(61229, d.OutputSum());
        }

        [TestMethod]
        public void Day09()
        {
            var d = new Day09("Day09Test.txt");
            Assert.AreEqual(15, d.RiskLevelSum());
            Assert.AreEqual(1134, d.BiggestBasinsProduct());
        }

        [TestMethod]
        public void Day10()
        {
            var d = new Day10("Day10Test.txt");
            Assert.AreEqual(26397, d.SyntaxErrorScore());
            Assert.AreEqual(288957L, d.CompletionScoreMedian());
        }

        [TestMethod]
        public void Day12()
        {
            var d = new Day12("Day12Test.txt");
            Assert.AreEqual(10, d.SimplePathCount());
            Assert.AreEqual(36, d.PathCountWithOneRevisit());
        }
    }
}