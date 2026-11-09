using System;
using System.Drawing;
using System.Windows.Forms;

public class MainForm : Form
{
	private Label infoLabel;
	private PictureBox box;

	public MainForm()
	{
		// Mengatur properti dasar form
		this.Text = "Mouse Event Handling";
		this.Size = new Size(400, 400);
		this.StartPosition = FormStartPosition.CenterScreen;

		// Membuat label untuk menampilkan informasi event
		infoLabel = new Label
		{
			Text = "Arahkan mouse ke kotak atau klik kotak.",
			Location = new Point(10, 10),
			AutoSize = true
		};
		this.Controls.Add(infoLabel);

		// Membuat PictureBox sebagai objek yang akan merespons event mouse
		box = new PictureBox
		{
			Size = new Size(100, 100),
			Location = new Point(150, 150),
			BackColor = Color.Blue
		};
		this.Controls.Add(box);

		// Menangani beberapa event mouse
		box.MouseClick += new MouseEventHandler(OnBoxMouseClick);
		box.MouseEnter += new EventHandler(OnBoxMouseEnter);
		box.MouseLeave += new EventHandler(OnBoxMouseLeave);
		box.MouseMove += new MouseEventHandler(OnBoxMouseMove);
	}

	private void OnBoxMouseClick(object sender, MouseEventArgs e)
	{
		box.BackColor = Color.FromArgb(new Random().Next(256), new Random().Next(256), new Random().Next(256));
		infoLabel.Text = $"Mouse clicked at ({e.X}, {e.Y})";
	}

	private void OnBoxMouseEnter(object sender, EventArgs e)
	{
		infoLabel.Text = "Mouse entered the box area.";
	}

	private void OnBoxMouseLeave(object sender, EventArgs e)
	{
		infoLabel.Text = "Mouse left the box area.";
	}

	private void OnBoxMouseMove(object sender, MouseEventArgs e)
	{
		infoLabel.Text = $"Mouse moving at ({e.X}, {e.Y})";
	}
}