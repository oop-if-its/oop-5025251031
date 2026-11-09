using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using CatchTheStars;

namespace CatchTheStarsTests
{
    [TestClass]
    public class StarTests
    {
        private Star _star;
        private Size _skySize;

        [TestInitialize]
        public void Setup()
        {
            _skySize = new Size(800, 600);
            _star = new Star(5, _skySize);
        }

        [TestMethod]
        public void Fall_StarFallsDown()
        {
            int initialPosition = _star.GetPictureBox().Top;
            _star.Fall();
            Assert.IsTrue(_star.GetPictureBox().Top > initialPosition);
        }

        [TestMethod]
        public void IsOutOfSky_WhenStarIsOutOfSky_ReturnsTrue()
        {
            _star.GetPictureBox().Top = _skySize.Height + 1;
            Assert.IsTrue(_star.IsOutOfSky(_skySize));
        }
    }
}
