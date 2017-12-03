using System;
using System.Drawing;
using System.Windows.Forms;

namespace EasySnapshot
{
    public partial class Result : Form
    {
        // Configurations:
        // Base size is the basis for thumbnail image
        // Timer is the time to display thumbnail

        private int _baseSize = 130;
        public int timer = 5;

        public Result()
        {
            InitializeComponent();

            // Make label parent to image to show it transparent
            var pos = PointToScreen(label1.Location);
            pos = pictureBox1.PointToClient(pos);
            label1.Parent = pictureBox1;
            label1.Location = pos;
            label1.BackColor = Color.Transparent;
            label1.Text = "Close in: " + timer + "s";

            // Hook events
            HookEvents();
        }

        // Hook mouse click to make event
        private void HookEvents()
        {
            foreach (Control ctl in this.Controls)
            {
                ctl.MouseClick += new MouseEventHandler(Result_MouseClick);
            }
        }

        // Close the thumbnail on mouse click
        private void Result_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        // Show the thumbnail
        public void ShowImage(string filepath)
        {
            Image screenshot = Image.FromFile(filepath);
            int width = (int)((float) screenshot.Width / screenshot.Height * _baseSize), height = _baseSize;

            Width = width;
            Height = height;

            pictureBox1.Width = width-6;
            pictureBox1.Height = height-6;
            pictureBox1.Image = screenshot.GetThumbnailImage(pictureBox1.Width, pictureBox1.Height, null, new IntPtr(0));
            Show();

            timer1.Interval = 1000;
            timer1.Start();
        }

        // Set pos for the result
        private void Result_Load(object sender, EventArgs e)
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;

            Location = new Point(bounds.Width - Size.Width - 5, bounds.Height - Size.Height - 50);
            Console.WriteLine("Result was loaded.");
        }

        // Change label text with 'time to close' info and close if the time ends
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer--;

            label1.Text = "Close in: " + timer + "s";

            if (timer <= 0)
            {
                timer1.Stop();
                Close();
            }
        }
    }
}
