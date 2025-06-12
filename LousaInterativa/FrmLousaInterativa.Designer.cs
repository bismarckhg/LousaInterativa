namespace LousaInterativa
{
    partial class FrmLousaInterativa
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
            menuStrip1 = new MenuStrip();
            viewMenu = new ToolStripMenuItem();
            fullScreenMenuItem = new ToolStripMenuItem();
            toggleTransparencyMenuItem = new ToolStripMenuItem();
            toggleMenuVisibilityMenuItem = new ToolStripMenuItem();
            toolsMenu = new ToolStripMenuItem();
            changeBackgroundColorMenuItem = new ToolStripMenuItem();
            adjustOpacityMenuItem = new ToolStripMenuItem();
            opacityTrackBar = new TrackBar();
            mainToolStrip = new ToolStrip();
            selectToolStripButton = new ToolStripButton();
            penToolStripButton = new ToolStripButton();
            penColorToolStripButton = new ToolStripButton();
            penSizeToolStripButton = new ToolStripButton();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)opacityTrackBar).BeginInit();
            mainToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { viewMenu, toolsMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.Visible = false;
            // 
            // viewMenu
            // 
            viewMenu.DropDownItems.AddRange(new ToolStripItem[] { fullScreenMenuItem, toggleTransparencyMenuItem, toggleMenuVisibilityMenuItem });
            viewMenu.Name = "viewMenu";
            viewMenu.Size = new Size(44, 20);
            viewMenu.Text = "View";
            // 
            // fullScreenMenuItem
            // 
            fullScreenMenuItem.Name = "fullScreenMenuItem";
            fullScreenMenuItem.ShortcutKeys = Keys.F11;
            fullScreenMenuItem.Size = new Size(206, 22);
            fullScreenMenuItem.Text = "Full Screen";
            fullScreenMenuItem.Click += fullScreenMenuItem_Click;
            // 
            // toggleTransparencyMenuItem
            // 
            toggleTransparencyMenuItem.Name = "toggleTransparencyMenuItem";
            toggleTransparencyMenuItem.ShortcutKeys = Keys.F10;
            toggleTransparencyMenuItem.Size = new Size(206, 22);
            toggleTransparencyMenuItem.Text = "&Toggle Transparency";
            toggleTransparencyMenuItem.Click += toggleTransparencyMenuItem_Click;
            // 
            // toggleMenuVisibilityMenuItem
            // 
            toggleMenuVisibilityMenuItem.Name = "toggleMenuVisibilityMenuItem";
            toggleMenuVisibilityMenuItem.Size = new Size(206, 22);
            toggleMenuVisibilityMenuItem.Text = "Show/Hide &Menu";
            toggleMenuVisibilityMenuItem.Click += toggleMenuVisibilityMenuItem_Click;
            // 
            // toolsMenu
            // 
            toolsMenu.DropDownItems.AddRange(new ToolStripItem[] { changeBackgroundColorMenuItem, adjustOpacityMenuItem });
            toolsMenu.Name = "toolsMenu";
            toolsMenu.Size = new Size(46, 20);
            toolsMenu.Text = "Tools";
            // 
            // changeBackgroundColorMenuItem
            // 
            changeBackgroundColorMenuItem.Name = "changeBackgroundColorMenuItem";
            changeBackgroundColorMenuItem.Size = new Size(214, 22);
            changeBackgroundColorMenuItem.Text = "Change Background Color";
            changeBackgroundColorMenuItem.Click += changeBackgroundColorMenuItem_Click;
            // 
            // adjustOpacityMenuItem
            // 
            adjustOpacityMenuItem.Name = "adjustOpacityMenuItem";
            adjustOpacityMenuItem.ShortcutKeys = Keys.F9;
            adjustOpacityMenuItem.Size = new Size(214, 22);
            adjustOpacityMenuItem.Text = "&Adjust Opacity";
            adjustOpacityMenuItem.Click += adjustOpacityMenuItem_Click;
            // 
            // opacityTrackBar
            // 
            opacityTrackBar.Dock = DockStyle.Top;
            opacityTrackBar.Location = new Point(0, 0);
            opacityTrackBar.Maximum = 100;
            opacityTrackBar.Name = "opacityTrackBar";
            opacityTrackBar.Size = new Size(800, 45);
            opacityTrackBar.TabIndex = 1;
            opacityTrackBar.TickFrequency = 10;
            opacityTrackBar.Value = 100;
            opacityTrackBar.Visible = false;
            opacityTrackBar.Scroll += opacityTrackBar_Scroll;
            // 
            // mainToolStrip
            // 
            mainToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            mainToolStrip.Items.AddRange(new ToolStripItem[] { selectToolStripButton, penToolStripButton, penColorToolStripButton, penSizeToolStripButton });
            mainToolStrip.Location = new Point(0, 45);
            mainToolStrip.Name = "mainToolStrip";
            mainToolStrip.Size = new Size(800, 25);
            mainToolStrip.TabIndex = 2;
            mainToolStrip.Text = "mainToolStrip";
            // 
            // selectToolStripButton
            // 
            selectToolStripButton.CheckOnClick = true;
            selectToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            selectToolStripButton.ImageTransparentColor = Color.Magenta;
            selectToolStripButton.Name = "selectToolStripButton";
            selectToolStripButton.Size = new Size(42, 22);
            selectToolStripButton.Text = "Select";
            selectToolStripButton.Click += selectToolStripButton_Click;
            // 
            // penToolStripButton
            // 
            penToolStripButton.CheckOnClick = true;
            penToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            penToolStripButton.ImageTransparentColor = Color.Magenta;
            penToolStripButton.Name = "penToolStripButton";
            penToolStripButton.Size = new Size(31, 22);
            penToolStripButton.Text = "Pen";
            penToolStripButton.Click += penToolStripButton_Click;
            // 
            // penColorToolStripButton
            // 
            penColorToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            penColorToolStripButton.ImageTransparentColor = Color.Magenta;
            penColorToolStripButton.Name = "penColorToolStripButton";
            penColorToolStripButton.Size = new Size(40, 22);
            penColorToolStripButton.Text = "Color";
            penColorToolStripButton.Click += penColorToolStripButton_Click;
            // 
            // penSizeToolStripButton
            // 
            penSizeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            penSizeToolStripButton.ImageTransparentColor = Color.Magenta;
            penSizeToolStripButton.Name = "penSizeToolStripButton";
            penSizeToolStripButton.Size = new Size(31, 22);
            penSizeToolStripButton.Text = "Size";
            penSizeToolStripButton.Click += penSizeToolStripButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            Controls.Add(mainToolStrip);
            Controls.Add(opacityTrackBar);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            KeyDown += Form1_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)opacityTrackBar).EndInit();
            mainToolStrip.ResumeLayout(false);
            mainToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

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
        private System.Windows.Forms.ToolStrip mainToolStrip; // Declaration
        private System.Windows.Forms.ToolStripButton penToolStripButton; // Declaration
        private System.Windows.Forms.ToolStripButton penColorToolStripButton; // Declaration
        private System.Windows.Forms.ToolStripButton penSizeToolStripButton; // Declaration
        private System.Windows.Forms.ToolStripButton selectToolStripButton; // Declaration
    }
}
