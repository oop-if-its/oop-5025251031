using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using CatchTheStars;

namespace CatchTheStarsTests
{
    [TestClass]
    public class SkyTests
    {
        private Sky _sky;

        [TestInitialize]
        public void Setup()
        {
            _sky = new Sky();
        }

        [TestMethod]
        public void GenerateStar_NewStarIsCreated()
        {
            _sky.GenerateStar();
            Assert.IsNotNull(_sky.Controls.Find("_starPictureBox", true));
        }

        [TestMethod]
        public void UpdateScore_ScoreIncreases()
        {
            int initialScore = _sky.Score;
            _sky.UpdateScore();
            Assert.IsTrue(_sky.Score > initialScore);
        }
    }
}
