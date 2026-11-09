using System;
using System.Drawing;
using System.Windows.Forms;

namespace CatchTheStars;

public class Basket
{
    private PictureBox _basketPictureBox;
    private int _speed;

    public Basket(Size skySize, int speed)
    {
        _speed = speed;
        _basketPictureBox = new PictureBox
        {
            Size = new Size(120, 80),
            Location = new Point(skySize.Width / 2 - 60, skySize.Height - 80),
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        try
        {
            _basketPictureBox.Image = Resource.basket;
        }
        catch (Exception ex)
        {
            _basketPictureBox.BackColor = Color.Black;
            Console.WriteLine("Gagal load image basket: " + ex.Message);
        }
    }

    public PictureBox GetPictureBox()
    {
        return _basketPictureBox;
    }

    public void MoveLeft()
    {
        if (_basketPictureBox.Left > 0)
        {
            _basketPictureBox.Left -= _speed;
        }
    }

    public void MoveRight(int boundary)
    {
        if (_basketPictureBox.Right < boundary)
        {
            _basketPictureBox.Left += _speed;
        }
    }

    public bool IsCollidedWith(Star star)
    {
        return star.GetPictureBox().Bounds.IntersectsWith(this.GetPictureBox().Bounds);
    }
}