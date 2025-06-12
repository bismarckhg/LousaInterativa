using System;
using System.Windows.Forms;
using System.Drawing; // Required for Point, Size etc. if they were used, but not directly here.

namespace LousaInterativa
{
    public partial class PenSizeForm : Form
    {
        public int SelectedPenSize { get; private set; }

        public PenSizeForm(int initialSize)
        {
            InitializeComponent();
            this.SelectedPenSize = initialSize;
            // Ensure initialSize is within the TrackBar's bounds before setting its Value
            this.sizeTrackBar.Value = Math.Clamp(initialSize, this.sizeTrackBar.Minimum, this.sizeTrackBar.Maximum);
            UpdateValueLabel();
        }

        private void UpdateValueLabel()
        {
            this.valueLabel.Text = string.Format("{0} px", this.sizeTrackBar.Value);
        }

        private void sizeTrackBar_Scroll(object sender, EventArgs e)
        {
            // Update the label as the trackbar scrolls
            UpdateValueLabel();
            // Optionally, update SelectedPenSize here too if live feedback to another part of app was needed,
            // but for a dialog, usually it's set only on OK.
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.SelectedPenSize = this.sizeTrackBar.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
