using System;
using System.Drawing;
using System.Windows.Forms;

namespace KeyboardEventHandling
{
    public class Box
    {
        private PictureBox _pictureBox;
        private int _moveSpeed;

        public Box(int x, int y, Color color, Size size, int moveSpeed)
        {
            _pictureBox = new PictureBox
            {
                Size = size,
                BackColor = color,
                Location = new Point(x, y)
            };
            _moveSpeed = moveSpeed;
        }

        public void MoveUp()
        {
            if (_pictureBox.Location.Y > 0)
            {
                _pictureBox.Location = new Point(_pictureBox.Location.X, _pictureBox.Location.Y - _moveSpeed);
            }
        }

        public void MoveDown(int maxHeight)
        {
            if (_pictureBox.Location.Y + _pictureBox.Height < maxHeight)
            {
                _pictureBox.Location = new Point(_pictureBox.Location.X, _pictureBox.Location.Y + _moveSpeed);
            }
        }

        public void MoveLeft()
        {
            if (_pictureBox.Location.X > 0)
            {
                _pictureBox.Location = new Point(_pictureBox.Location.X - _moveSpeed, _pictureBox.Location.Y);
            }
        }

        public void MoveRight(int maxWidth)
        {
            if (_pictureBox.Location.X + _pictureBox.Width < maxWidth)
            {
                _pictureBox.Location = new Point(_pictureBox.Location.X + _moveSpeed, _pictureBox.Location.Y);
            }
        }

        public void ChangeColor()
        {
            Random random = new Random();
            _pictureBox.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        public PictureBox GetPictureBox()
        {
            return _pictureBox;
        }
    }
}