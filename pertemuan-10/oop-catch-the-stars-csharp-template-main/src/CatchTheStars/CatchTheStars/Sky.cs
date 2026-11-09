using System;
using System.Drawing;
using System.Windows.Forms;

namespace CatchTheStars;

public class Sky : Form
{
	private Label _scoreLabel = null!;

	public Sky()
	{
		this.Text = "Catch the Falling Stars";
		this.Size = new Size(800, 600);
		this.StartPosition = FormStartPosition.CenterScreen;

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
	}
}