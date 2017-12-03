using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EasySnapshot
{
    public partial class Main : Form
    {
        private Keys _hotkey = Keys.PrintScreen;
        private string _saveFolder = "C:/Screenshots/";

        public Main()
        {
            InitializeComponent();

            label2.Text = "Hotkey: " + _hotkey.ToString();

            // Register _hotkey key as hotkey
            RegisterHotKey(this.Handle, this.GetType().GetHashCode(), 0x0000, (int) _hotkey);
        }

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        // Select the folder for our screenshots
        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = _saveFolder;
            folderBrowserDialog1.ShowDialog();
            _saveFolder = folderBrowserDialog1.SelectedPath;
        }

        // Take screenshot by clicking button.
        private void button2_Click(object sender, EventArgs e)
        {
            Screenshot();
        }

        // Open save folder
        private void button3_Click(object sender, EventArgs e)
        {
            CheckAndCreateFolder();

            System.Diagnostics.Process.Start(_saveFolder);
        }

        // Check if hotkey is down, make screenshot
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                Console.WriteLine("Hotkey registered, making screenshot.");
                Screenshot();
            }
            base.WndProc(ref m);
        }

        // Method to take screenshot
        private void Screenshot()
        {
            CheckAndCreateFolder();

            Screenshot screenshot = new Screenshot(_saveFolder);

            Result result = new Result();
            result.ShowImage(screenshot.GetFile());
        }

        // Check if folder not exist then create it
        private void CheckAndCreateFolder()
        {
            if (!System.IO.Directory.Exists(_saveFolder))
            {
                Console.WriteLine("Directory not exists");
                Console.WriteLine("Creating directory.");

                System.IO.Directory.CreateDirectory(_saveFolder);
            }
        }
    }
}
