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
    }
}