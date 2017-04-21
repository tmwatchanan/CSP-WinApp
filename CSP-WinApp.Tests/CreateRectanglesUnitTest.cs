using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CSP_WinApp.Tests
{
    [TestClass]
    public class CreateRectanglesUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            coordinates.Add(new Coordinate { Width = 2, Length = 1, Count = 2 });
            coordinates.Add(new Coordinate { Width = 1, Length = 3, Count = 1 });
            List<Rectangle> rectangles = Form1.CreateRectangles(coordinates);

            List<Rectangle> expectedRectangles = new List<Rectangle>();
            expectedRectangles.Add(new Rectangle { X = 0, Y = 0, Width = 2, Length = 1, Orientation = 0 });
            expectedRectangles.Add(new Rectangle { X = 0, Y = 0, Width = 2, Length = 1, Orientation = 0 });
            expectedRectangles.Add(new Rectangle { X = 0, Y = 0, Width = 1, Length = 3, Orientation = 0 });

            Assert.AreEqual(expectedRectangles, rectangles);
        }
    }
}
