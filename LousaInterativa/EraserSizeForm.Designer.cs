namespace LousaInterativa
{
    partial class EraserSizeForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TrackBar sizeTrackBar;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1; // Static label

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
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sizeTrackBar)).BeginInit();
            this.SuspendLayout();
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13); // Adjusted size for text
            this.label1.TabIndex = 4; // Ensure unique tab index
            this.label1.Text = "Tamanho da Borracha:"; // Translated
            //
            // sizeTrackBar
            //
            this.sizeTrackBar.Location = new System.Drawing.Point(12, 45); // Positioned below labels
            this.sizeTrackBar.Maximum = 10; // Eraser max size
            this.sizeTrackBar.Minimum = 1;  // Eraser min size
            this.sizeTrackBar.Name = "sizeTrackBar";
            this.sizeTrackBar.Size = new System.Drawing.Size(260, 45);
            this.sizeTrackBar.TabIndex = 0;
            this.sizeTrackBar.Value = 5; // Default value, will be overridden by constructor
            this.sizeTrackBar.Scroll += new System.EventHandler(this.sizeTrackBar_Scroll);
            //
            // valueLabel
            //
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(120, 29); // Centered above trackbar or next to label1
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(48, 13); // Adjusted for "10x10 px"
            this.valueLabel.TabIndex = 1;
            this.valueLabel.Text = "5x5 px"; // Initial example text
            //
            // okButton
            //
            this.okButton.Location = new System.Drawing.Point(60, 96); // Adjusted Y position
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
            this.cancelButton.Location = new System.Drawing.Point(150, 96); // Adjusted Y position
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancelar";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            //
            // EraserSizeForm
            //
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 131); // Adjusted client size
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.sizeTrackBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EraserSizeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selecionar Tamanho da Borracha"; // Translated
            ((System.ComponentModel.ISupportInitialize)(this.sizeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
