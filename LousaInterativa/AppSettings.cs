using System.Drawing;
using System.Windows.Forms;

namespace LousaInterativa
{
    public class AppSettings
    {
        public int LastBackColorAsArgb { get; set; }
        public bool IsFullScreen { get; set; }
        public bool WasWindowTransparent { get; set; }
        public FormWindowState NormalWindowState { get; set; }
        public FormBorderStyle NormalFormBorderStyle { get; set; }
        public Size NormalFormSize { get; set; }
        public Point NormalFormLocation { get; set; }
        public double FormOpacity { get; set; } // New property
        public bool IsMenuVisible { get; set; } // Property for menu visibility

        public AppSettings()
        {
            LastBackColorAsArgb = SystemColors.Control.ToArgb();
            IsFullScreen = false;
            WasWindowTransparent = false;
            NormalWindowState = FormWindowState.Normal;
            NormalFormBorderStyle = FormBorderStyle.Sizable;
            NormalFormSize = new Size(816, 523); // Default size, can be adjusted. e.g. 800,600
            NormalFormLocation = new Point(100, 100); // Default location
            FormOpacity = 1.0; // Default to 100% opaque
            IsMenuVisible = false; // Default to menu being hidden
        }
    }
}
