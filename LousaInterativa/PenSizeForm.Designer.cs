namespace LousaInterativa
{
    partial class PenSizeForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TrackBar sizeTrackBar;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.sizeTrackBar = new System.Windows.Forms.TrackBar();
            this.valueLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sizeTrackBar)).BeginInit();
            this.SuspendLayout();
            //
            // sizeTrackBar
            //
            this.sizeTrackBar.Location = new System.Drawing.Point(12, 25);
            this.sizeTrackBar.Maximum = 15;
            this.sizeTrackBar.Minimum = 1;
            this.sizeTrackBar.Name = "sizeTrackBar";
            this.sizeTrackBar.Size = new System.Drawing.Size(260, 45);
            this.sizeTrackBar.TabIndex = 0;
            this.sizeTrackBar.Value = 1;
            this.sizeTrackBar.Scroll += new System.EventHandler(this.sizeTrackBar_Scroll);
            //
            // valueLabel
            //
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(120, 9); // Centered above trackbar
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(29, 13);
            this.valueLabel.TabIndex = 1;
            this.valueLabel.Text = "1 px";
            //
            // okButton
            //
            this.okButton.Location = new System.Drawing.Point(60, 76);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            //
            // cancelButton
            //
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(150, 76);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            //
            // PenSizeForm
            //
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 111); // Adjusted client size
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.sizeTrackBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PenSizeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pen Size";
            ((System.ComponentModel.ISupportInitialize)(this.sizeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
