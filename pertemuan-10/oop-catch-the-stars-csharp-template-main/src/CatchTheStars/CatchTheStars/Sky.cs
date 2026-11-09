using System;
using System.Drawing;
using System.Windows.Forms;

namespace CatchTheStars;

public class Sky : Form
{
	private Label _scoreLabel = null!;
    private Basket _basket = null!;
    private Star _star = null!;
    private System.Windows.Forms.Timer _gameTimer = null!;
    private bool _goLeft, _goRight;

    public Sky()
    {
        this.Text = "Catch the Falling Stars";
        this.Size = new Size(800, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.KeyPreview = true;

        this.KeyDown += new KeyEventHandler(OnKeyDown);
        this.KeyUp += new KeyEventHandler(OnKeyUp);

        InitializeGame();
    }

    private void InitializeGame()
    {
        _scoreLabel = new Label
        {
            Text = "Score: 0",
            Location = new Point(10, 10),
            Font = new Font("Arial", 16),
            ForeColor = Color.Black,
            AutoSize = true
        };
        this.Controls.Add(_scoreLabel);

        _basket = new Basket(this.ClientSize, 15);
        this.Controls.Add(_basket.GetPictureBox());

        _star = new Star(5, this.ClientSize);
        this.Controls.Add(_star.GetPictureBox());

        _gameTimer = new System.Windows.Forms.Timer
        {
            Interval = 20
        };
        _gameTimer.Tick += new EventHandler(GameLoop);
        _gameTimer.Start();
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Left) _goLeft = true;
        if (e.KeyCode == Keys.Right) _goRight = true;
    }

    private void OnKeyUp(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Left) _goLeft = false;
        if (e.KeyCode == Keys.Right) _goRight = false;
    }

    private void GameLoop(object? sender, EventArgs e)
    {
        _star.Fall();

        if (_goLeft) _basket.MoveLeft();
        if (_goRight) _basket.MoveRight(this.ClientSize.Width);

        if (_basket.IsCollidedWith(_star))
        {
        }
    }
}