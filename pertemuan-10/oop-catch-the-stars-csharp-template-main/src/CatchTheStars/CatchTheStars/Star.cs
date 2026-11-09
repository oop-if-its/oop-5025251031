using System;
using System.Drawing;
using System.Windows.Forms;

namespace CatchTheStars;

public class Star
{
    private PictureBox _starPictureBox;
    private Random _random;
    private int _fallSpeed;

    public Star(int fallSpeed, Size skySize)
    {
        _fallSpeed = fallSpeed;
        _random = new Random();
        _starPictureBox = new PictureBox
        {
            Size = new Size(40, 40),
            Location = new Point(_random.Next(0, Math.Max(1, skySize.Width - 40)), 0),
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        try
        {
            _starPictureBox.Image = Resource.star;
        }
        catch (Exception e)
        {
            _starPictureBox.BackColor = Color.Gold;
            Console.WriteLine("Gagal load image star: " + e.Message);
        }
    }

    public PictureBox GetPictureBox()
    {
        return _starPictureBox;
    }

    public void Fall()
    {
        _starPictureBox.Top += _fallSpeed;
    }

    public bool IsOutOfSky(Size skySize)
    {
        return _starPictureBox.Top > skySize.Height;
    }
}