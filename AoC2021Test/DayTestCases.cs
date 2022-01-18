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
            Assert.AreEqual(355592, d.SumLinearFuel());
            Assert.AreEqual(101618069, d.SumGeometricFuel());
        }

        [TestMethod]
        public void Day08()
        {
            var d = new Day08();
            Assert.AreEqual(445, d.EasyDigitCount());
            Assert.AreEqual(1043101, d.OutputSum());
        }

        [TestMethod]
        public void Day09()
        {
            var d = new Day09();
            Assert.AreEqual(528, d.RiskLevelSum());
            Assert.AreEqual(920448, d.BiggestBasinsProduct());
        }

        [TestMethod]
        public void Day10()
        {
            var d = new Day10();
            Assert.AreEqual(362271, d.SyntaxErrorScore());
            Assert.AreEqual(1698395182L, d.CompletionScoreMedian());
        }

        [TestMethod]
        public void Day11()
        {
            var d = new Day11();
            Assert.AreEqual(1642, d.FlashCountAfter100Steps());
            Assert.AreEqual(320, d.StepCountForSynchronization());
        }

        [TestMethod]
        public void Day12()
        {
            var d = new Day12();
            Assert.AreEqual(5333, d.SimplePathCount());
            Assert.AreEqual(146553, d.PathCountWithOneRevisit());
        }

        [TestMethod]
        public void Day13()
        {
            var d = new Day13();
            Assert.AreEqual(684, d.FirstFoldDotCount());
            // This is not the real answer for 13-2. See Day13.Do() for details.
            Assert.AreEqual(98, d.CompleteInstructions());
        }

        [TestMethod]
        public void Day14()
        {
            var d = new Day14();
            Assert.AreEqual(4244, d.CommonRangeAfter10Steps());
            Assert.AreEqual(4807056953866, d.CommonRangeAfter40Steps());
        }

        [TestMethod]
        public void Day15()
        {
            var d = new Day15();
            Assert.AreEqual(790, d.MinRiskSmallGrid());
            Assert.AreEqual(2998, d.MinRiskLargeGrid());
        }

        [TestMethod]
        public void Day16()
        {
            var d = new Day16();
            Assert.AreEqual(1007, d.Part1());
            Assert.AreEqual(834151779165, d.Part2());
        }

        [TestMethod]
        public void Day17()
        {
            var d = new Day17();
            Assert.AreEqual(7750, d.HighestPosition());
            Assert.AreEqual(4120, d.ValidVelocityCount());
        }

        [TestMethod]
        public void Day18()
        {
            var d = new Day18();
            Assert.AreEqual(3305, d.TotalSumMagnitude());
            Assert.AreEqual(4563, d.MaxSumMagnitude());
        }

        [TestMethod]
        public void Day20()
        {
            var d = new Day20();
            Assert.AreEqual(4917, d.PixelCountAfter2Rounds());
            Assert.AreEqual(16389, d.PixelCountAfter50Rounds());
        }

        [TestMethod]
        public void Day21()
        {
            var d = new Day21();
            Assert.AreEqual(734820, d.DeterministicLoserProduct());
            Assert.AreEqual(193170338541590, d.DiracWinningUniverseCount());
        }

        public void DayStarter()
        {
            var d = new DayStarter();
            Assert.AreEqual(-1, d.Part1());
            Assert.AreEqual(-1, d.Part2());
        }
    }
}