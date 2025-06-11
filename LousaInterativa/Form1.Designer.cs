namespace LousaInterativa
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fullScreenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.changeBackgroundColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleTransparencyMenuItem = new System.Windows.Forms.ToolStripMenuItem(); // Instantiation
            this.opacityTrackBar = new System.Windows.Forms.TrackBar();
            this.adjustOpacityMenuItem = new System.Windows.Forms.ToolStripMenuItem(); // Instantiation
            this.toggleMenuVisibilityMenuItem = new System.Windows.Forms.ToolStripMenuItem(); // Instantiation
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacityTrackBar)).BeginInit();
            this.SuspendLayout();
            //
            // viewMenu
            //
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullScreenMenuItem,
            this.toggleTransparencyMenuItem,
            this.toggleMenuVisibilityMenuItem}); // Added here
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(44, 20);
            this.viewMenu.Text = "View";
            //
            // fullScreenMenuItem
            //
            this.fullScreenMenuItem.Name = "fullScreenMenuItem";
            this.fullScreenMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.fullScreenMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fullScreenMenuItem.Text = "Full Screen";
            this.fullScreenMenuItem.Click += new System.EventHandler(this.fullScreenMenuItem_Click);
            //
            // toggleTransparencyMenuItem
            //
            this.toggleTransparencyMenuItem.Name = "toggleTransparencyMenuItem";
            this.toggleTransparencyMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.toggleTransparencyMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toggleTransparencyMenuItem.Text = "&Toggle Transparency";
            this.toggleTransparencyMenuItem.Click += new System.EventHandler(this.toggleTransparencyMenuItem_Click);
            //
            // toggleMenuVisibilityMenuItem
            //
            this.toggleMenuVisibilityMenuItem.Name = "toggleMenuVisibilityMenuItem";
            this.toggleMenuVisibilityMenuItem.Size = new System.Drawing.Size(180, 22); // Consistent with others in View
            this.toggleMenuVisibilityMenuItem.Text = "Show/Hide &Menu";
            this.toggleMenuVisibilityMenuItem.Click += new System.EventHandler(this.toggleMenuVisibilityMenuItem_Click);
            //
            // toolsMenu
            //
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeBackgroundColorMenuItem,
            this.adjustOpacityMenuItem}); // Added here
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(46, 20);
            this.toolsMenu.Text = "Tools";
            //
            // changeBackgroundColorMenuItem
            //
            this.changeBackgroundColorMenuItem.Name = "changeBackgroundColorMenuItem";
            this.changeBackgroundColorMenuItem.Size = new System.Drawing.Size(200, 22); // Keep consistent if possible
            this.changeBackgroundColorMenuItem.Text = "Change Background Color";
            this.changeBackgroundColorMenuItem.Click += new System.EventHandler(this.changeBackgroundColorMenuItem_Click);
            //
            // adjustOpacityMenuItem
            //
            this.adjustOpacityMenuItem.Name = "adjustOpacityMenuItem";
            this.adjustOpacityMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.adjustOpacityMenuItem.Size = new System.Drawing.Size(200, 22); // Consistent size
            this.adjustOpacityMenuItem.Text = "&Adjust Opacity";
            this.adjustOpacityMenuItem.Click += new System.EventHandler(this.adjustOpacityMenuItem_Click);
            //
            // menuStrip1
            //
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewMenu,
            this.toolsMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false; // Make menuStrip1 invisible by default
            //
            // opacityTrackBar
            //
            this.opacityTrackBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.opacityTrackBar.Location = new System.Drawing.Point(0, 24); // Location is illustrative, Dock takes precedence
            this.opacityTrackBar.Maximum = 100;
            this.opacityTrackBar.Name = "opacityTrackBar";
            this.opacityTrackBar.Size = new System.Drawing.Size(800, 45);    // Size is illustrative, Dock takes precedence for width
            this.opacityTrackBar.TabIndex = 1;
            this.opacityTrackBar.TickFrequency = 10;
            this.opacityTrackBar.Value = 100; // Default to 100% opaque
            this.opacityTrackBar.Visible = false; // Initially hidden
            this.opacityTrackBar.Scroll += new System.EventHandler(this.opacityTrackBar_Scroll);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.opacityTrackBar); // Added TrackBar
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacityTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem fullScreenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem changeBackgroundColorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleTransparencyMenuItem; // Declaration
        private System.Windows.Forms.TrackBar opacityTrackBar; // Declaration
        private System.Windows.Forms.ToolStripMenuItem adjustOpacityMenuItem; // Declaration
        private System.Windows.Forms.ToolStripMenuItem toggleMenuVisibilityMenuItem; // Declaration
    }
}
