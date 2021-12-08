using AoC2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoC2021Test
{
    [TestClass]
    public class DayTestCases
    {
        [TestMethod]
        public void Day01()
        {
            var d = new Day01();
            Assert.AreEqual(1167, d.SimpleMeasurement());
            Assert.AreEqual(1130, d.WindowMeasurement());
        }

        [TestMethod]
        public void Day02()
        {
            var d = new Day02();
            Assert.AreEqual(1947824, d.ProductSimpleCommands());
            Assert.AreEqual(1813062561, d.ProductCommandsWithAim());
        }

        [TestMethod]
        public void Day03()
        {
            var d = new Day03();
            Assert.AreEqual(749376, d.PowerConsumption());
            Assert.AreEqual(2372923, d.LifeSupportRating());
        }

        [TestMethod]
        public void Day04()
        {
            var d = new Day04();
            Assert.AreEqual(60368, d.FirstWinnerScore());
            Assert.AreEqual(17435, d.LastWinnerScore());
        }

        [TestMethod]
        public void Day05()
        {
            var d = new Day05();
            Assert.AreEqual(4873, d.OrthagonalIntersectingPoints());
            Assert.AreEqual(19472, d.AllIntersectingPoints());
        }

        [TestMethod]
        public void Day06()
        {
            var d = new Day06();
            Assert.AreEqual(371379, d.PopulationAfter80Days());
            Assert.AreEqual(1674303997472L, d.PopulationAfter256Days());
        }

        [TestMethod]
        public void Day07()
        {
            var d = new Day07();
            Assert.AreEqual(355592, d.Part1());
            Assert.AreEqual(101618069, d.Part2());
        }
    }
}