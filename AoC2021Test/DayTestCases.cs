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
    }
}