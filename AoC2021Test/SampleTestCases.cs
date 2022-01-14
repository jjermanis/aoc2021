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

        [TestMethod]
        public void Day14()
        {
            var d = new Day14("Day14Test.txt");
            Assert.AreEqual(1588, d.CommonRangeAfter10Steps());
            Assert.AreEqual(2188189693529, d.CommonRangeAfter40Steps());
        }

        [TestMethod]
        public void Day15()
        {
            var d = new Day15("Day15Test.txt");
            Assert.AreEqual(40, d.MinRiskSmallGrid());
            Assert.AreEqual(315, d.MinRiskLargeGrid());
        }

        [TestMethod]
        public void Day16()
        {
            var d1 = new Day16("Day16Test.txt");
            Assert.AreEqual(16, d1.Part1());
            var d2 = new Day16("Day16Test2.txt");
            Assert.AreEqual(12, d2.Part1());
            var d3 = new Day16("Day16Test3.txt");
            Assert.AreEqual(23, d3.Part1());
        }

        [TestMethod]
        public void Day17()
        {
            var d = new Day17("Day17Test.txt");
            Assert.AreEqual(45, d.HighestPosition());
            Assert.AreEqual(112, d.ValidVelocityCount());
        }
    }
}