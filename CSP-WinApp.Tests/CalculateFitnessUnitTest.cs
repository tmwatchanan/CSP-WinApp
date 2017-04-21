using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSP_WinApp.Tests
{
    [TestClass]
    public class CalculateFitnessUnitTest
    {
        [TestMethod]
        public void BasicOverlap()
        {
            Rectangle rect1 = new Rectangle(0, 0, 2, 2, 0);
            Rectangle rect2 = new Rectangle(0, 0, 1, 1, 0);
            int expectedArea = 1;
            Assert.AreEqual(expectedArea, rect1.CalculateFitness(rect2));
        }

        [TestMethod]
        public void HorzVertOverlap()
        {
            Rectangle rect1 = new Rectangle(0, 0, 1, 3, 1);
            Rectangle rect2 = new Rectangle(0, 0, 2, 1, 0);
            int expectedArea = 1;
            Assert.AreEqual(expectedArea, rect1.CalculateFitness(rect2));
        }
    }
}
