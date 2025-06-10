using System.Windows.Forms;

namespace LousaInterativa
{
    public partial class Form1 : Form
    {
        private FormWindowState previousWindowState;
        private FormBorderStyle previousFormBorderStyle;
        private bool isFullScreen = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void ToggleFullScreen()
        {
            if (isFullScreen)
            {
                // Exit full screen
                this.WindowState = previousWindowState;
                this.FormBorderStyle = previousFormBorderStyle;
                isFullScreen = false;
            }
            else
            {
                // Enter full screen
                previousWindowState = this.WindowState;
                previousFormBorderStyle = this.FormBorderStyle;

                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
                // It's important to set Bounds after changing WindowState and FormBorderStyle
                this.Bounds = Screen.PrimaryScreen.Bounds;
                isFullScreen = true;
            }
        }

        private void fullScreenMenuItem_Click(object sender, System.EventArgs e)
        {
            ToggleFullScreen();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                ToggleFullScreen();
            }
        }

        private void changeBackgroundColorMenuItem_Click(object sender, System.EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                // Allow the user to select colors with alpha values
                colorDialog.FullOpen = true;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    this.BackColor = colorDialog.Color;

                    // Handle transparency
                    if (colorDialog.Color.A < 255)
                    {
                        this.TransparencyKey = colorDialog.Color;
                    }
                    else
                    {
                        // If fully opaque, remove any existing transparency key
                        this.TransparencyKey = Color.Empty;
                    }
                }
            }
        }
    }
}
