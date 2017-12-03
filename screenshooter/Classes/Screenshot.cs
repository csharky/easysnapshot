using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace EasySnapshot
{
    public class Screenshot
    {
        private string _filepath;
        DateTime date = DateTime.Now;

        // Construct to make screenshot of whole screen
        public Screenshot(string filepath)
        {
            // Set the filepath and filename of current date.

            _filepath = filepath + date.Day + "-" + date.Month + "-" + date.Year + " " + date.Hour + "." + date.Minute + "." + date.Second + ".png";
            Make();
        }

        // Overload construct to make screenshot of screenpart
        public Screenshot(string filepath, int x, int y, int width, int height)
        {
            // Set the filepath and filename of current date.

            _filepath = filepath + date.Day + "-" + date.Month + "-" + date.Year + " " + date.Hour + "." + date.Minute + "." + date.Second + ".png";

            Make(x, y, width, height);
        }

        // Method to make screenshot with image parametres.
        private void Make(int x = 0, int y = 0, int width = 0, int height = 0)
        {
            Rectangle bounds = new Rectangle(x, y, width, height);

            // Check if parameters are 0 then make screenshot of whole screen
            if (x + y + width + height == 0)
            {
                bounds = Screen.PrimaryScreen.Bounds;
            }

            Bitmap bmpScreenShot = new Bitmap(bounds.Width, bounds.Height);

            // Copying image from screen
            Graphics image = Graphics.FromImage(bmpScreenShot);
            image.CopyFromScreen(
                bounds.X, 
                bounds.Y, 
                0, 
                0, 
                bmpScreenShot.Size,
                CopyPixelOperation.SourceCopy
            );

            // Save our file
            bmpScreenShot.Save(_filepath, ImageFormat.Png);
            bmpScreenShot.Dispose();

            Console.WriteLine("Screenshot created: " + _filepath);
        }

        public string GetFile()
        {
            return _filepath;
        }
    }
}