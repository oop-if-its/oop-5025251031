using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using CatchTheStars;

namespace CatchTheStarsTests
{
    [TestClass]
    public class BasketTests
    {
        private Basket _basket;
        private Size _skySize;

        [TestInitialize]
        public void Setup()
        {
            _skySize = new Size(800, 600);
            _basket = new Basket(_skySize, 15);
        }

        [TestMethod]
        public void MoveLeft_BasketMovesLeft()
        {
            int initialPosition = _basket.GetPictureBox().Left;
            _basket.MoveLeft();
            Assert.IsTrue(_basket.GetPictureBox().Left < initialPosition);
        }

        [TestMethod]
        public void MoveRight_BasketMovesRight()
        {
            int initialPosition = _basket.GetPictureBox().Left;
            _basket.MoveRight(_skySize.Width);
            Assert.IsTrue(_basket.GetPictureBox().Left > initialPosition);
        }

        [TestMethod]
        public void IsCollidedWith_WhenCollidingWithStar_ReturnsTrue()
        {
            Star star = new Star(5, _skySize);
            star.GetPictureBox().Location = _basket.GetPictureBox().Location;
            Assert.IsTrue(_basket.IsCollidedWith(star));
        }
    }
}
