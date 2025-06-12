using System;
using System.Windows.Forms;
using System.Drawing;

namespace LousaInterativa
{
    public partial class EraserSizeForm : Form
    {
        public int SelectedEraserSize { get; private set; }

        public EraserSizeForm(int initialSize)
        {
            InitializeComponent();
            this.SelectedEraserSize = initialSize;
            // Garante que initialSize esteja dentro dos limites do TrackBar
            if (sizeTrackBar.Minimum <= initialSize && initialSize <= sizeTrackBar.Maximum)
            {
                this.sizeTrackBar.Value = initialSize;
            }
            else
            {
                this.sizeTrackBar.Value = Math.Clamp(initialSize, this.sizeTrackBar.Minimum, this.sizeTrackBar.Maximum);
            }
            UpdateValueLabel();
        }

        private void UpdateValueLabel()
        {
            this.valueLabel.Text = string.Format("{0}x{0} px", this.sizeTrackBar.Value);
        }

        private void sizeTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateValueLabel();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.SelectedEraserSize = this.sizeTrackBar.Value;
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
