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
            this.mainToolStrip = new System.Windows.Forms.ToolStrip(); // Instantiation
            this.selectToolStripButton = new System.Windows.Forms.ToolStripButton(); // Instantiation
            this.penToolStripButton = new System.Windows.Forms.ToolStripButton(); // Instantiation
            this.linesToolStripButton = new System.Windows.Forms.ToolStripButton(); // Instantiation - New Lines Tool
            this.lineColorToolStripButton = new System.Windows.Forms.ToolStripButton(); // Renamed from penColorToolStripButton
            this.lineThicknessToolStripButton = new System.Windows.Forms.ToolStripButton(); // Renamed from penSizeToolStripButton
            this.eraserToolStripButton = new System.Windows.Forms.ToolStripButton(); // Instantiation - Eraser Tool
            this.eraserSizeToolStripButton = new System.Windows.Forms.ToolStripButton(); // Instantiation - Eraser Size Tool
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacityTrackBar)).BeginInit();
            this.mainToolStrip.SuspendLayout(); // For adding items
            this.SuspendLayout();
            //
            // viewMenu
            //
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullScreenMenuItem,
            this.toggleTransparencyMenuItem,
            this.toggleMenuVisibilityMenuItem}); // Added here
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(48, 20); // Adjusted size for "Exibir"
            this.viewMenu.Text = "Exibir"; // Translated
            //
            // fullScreenMenuItem
            //
            this.fullScreenMenuItem.Name = "fullScreenMenuItem";
            this.fullScreenMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.fullScreenMenuItem.Size = new System.Drawing.Size(252, 22); // Adjusted size for new text
            this.fullScreenMenuItem.Text = "Tela Cheia (F11)"; // Translated
            this.fullScreenMenuItem.Click += new System.EventHandler(this.fullScreenMenuItem_Click);
            //
            // toggleTransparencyMenuItem
            //
            this.toggleTransparencyMenuItem.Name = "toggleTransparencyMenuItem";
            this.toggleTransparencyMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.toggleTransparencyMenuItem.Size = new System.Drawing.Size(252, 22); // Adjusted size for new text
            this.toggleTransparencyMenuItem.Text = "Alternar Transparência (F10)"; // Translated
            this.toggleTransparencyMenuItem.Click += new System.EventHandler(this.toggleTransparencyMenuItem_Click);
            //
            // toggleMenuVisibilityMenuItem
            //
            this.toggleMenuVisibilityMenuItem.Name = "toggleMenuVisibilityMenuItem";
            this.toggleMenuVisibilityMenuItem.Size = new System.Drawing.Size(252, 22); // Adjusted size for new text
            this.toggleMenuVisibilityMenuItem.Text = "Alternar Visibilidade do Menu (F8)"; // Translated
            this.toggleMenuVisibilityMenuItem.Click += new System.EventHandler(this.toggleMenuVisibilityMenuItem_Click);
            //
            // toolsMenu
            //
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeBackgroundColorMenuItem,
            this.adjustOpacityMenuItem}); // Added here
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(84, 20);  // Adjusted size for "Ferramentas"
            this.toolsMenu.Text = "Ferramentas"; // Translated
            //
            // changeBackgroundColorMenuItem
            //
            this.changeBackgroundColorMenuItem.Name = "changeBackgroundColorMenuItem";
            this.changeBackgroundColorMenuItem.Size = new System.Drawing.Size(200, 22);
            this.changeBackgroundColorMenuItem.Text = "Alterar Cor de Fundo"; // Translated
            this.changeBackgroundColorMenuItem.Click += new System.EventHandler(this.changeBackgroundColorMenuItem_Click);
            //
            // adjustOpacityMenuItem
            //
            this.adjustOpacityMenuItem.Name = "adjustOpacityMenuItem";
            this.adjustOpacityMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.adjustOpacityMenuItem.Size = new System.Drawing.Size(200, 22);
            this.adjustOpacityMenuItem.Text = "Ajustar Opacidade (F9)"; // Translated
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
            // mainToolStrip
            //
            this.mainToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0); // Actual Y will be below opacityTrackBar
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(800, 25); // Height 25, width will be auto
            this.mainToolStrip.TabIndex = 2; // After menuStrip1 (0) and opacityTrackBar (1)
            this.mainToolStrip.Text = "mainToolStrip";
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectToolStripButton,
            this.penToolStripButton,
            this.linesToolStripButton, // Added new Lines Tool button
            this.lineColorToolStripButton, // Renamed from penColorToolStripButton
            this.lineThicknessToolStripButton, // Renamed from penSizeToolStripButton
            this.eraserToolStripButton, // Added Eraser Tool button
            this.eraserSizeToolStripButton}); // Added Eraser Size Tool button
            //
            // selectToolStripButton
            //
            this.selectToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.selectToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectToolStripButton.Name = "selectToolStripButton";
            this.selectToolStripButton.Size = new System.Drawing.Size(64, 22); // Adjusted for "Selecionar"
            this.selectToolStripButton.Text = "Selecionar"; // Translated
            this.selectToolStripButton.ToolTipText = "Selecionar Formas"; // Translated
            this.selectToolStripButton.CheckOnClick = true;
            this.selectToolStripButton.Click += new System.EventHandler(this.selectToolStripButton_Click);
            //
            // penToolStripButton
            //
            this.penToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.penToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.penToolStripButton.Name = "penToolStripButton";
            this.penToolStripButton.Size = new System.Drawing.Size(34, 22); // Example text-based size
            this.penToolStripButton.Text = "Pen";
            this.penToolStripButton.CheckOnClick = true;
            this.penToolStripButton.Visible = false; // Disable pen tool
            this.penToolStripButton.Click += new System.EventHandler(this.penToolStripButton_Click);
            //
            // linesToolStripButton
            //
            this.linesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.linesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.linesToolStripButton.Name = "linesToolStripButton";
            this.linesToolStripButton.Size = new System.Drawing.Size(46, 22); // Adjusted size for "Linhas"
            this.linesToolStripButton.Text = "Linhas";
            this.linesToolStripButton.ToolTipText = "Desenhar Linhas";
            this.linesToolStripButton.CheckOnClick = true;
            this.linesToolStripButton.Click += new System.EventHandler(this.linesToolStripButton_Click);
            //
            // lineColorToolStripButton
            //
            this.lineColorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lineColorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lineColorToolStripButton.Name = "lineColorToolStripButton";
            this.lineColorToolStripButton.Size = new System.Drawing.Size(78, 22); // Adjusted for "Cor da Linha"
            this.lineColorToolStripButton.Text = "Cor da Linha";
            this.lineColorToolStripButton.ToolTipText = "Selecionar Cor da Linha";
            this.lineColorToolStripButton.Click += new System.EventHandler(this.lineColorToolStripButton_Click);
            //
            // lineThicknessToolStripButton
            //
            this.lineThicknessToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lineThicknessToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lineThicknessToolStripButton.Name = "lineThicknessToolStripButton";
            this.lineThicknessToolStripButton.Size = new System.Drawing.Size(120, 22); // Adjusted for "Espessura da Linha"
            this.lineThicknessToolStripButton.Text = "Espessura da Linha";
            this.lineThicknessToolStripButton.ToolTipText = "Selecionar Espessura da Linha";
            this.lineThicknessToolStripButton.Click += new System.EventHandler(this.lineThicknessToolStripButton_Click);
            //
            // eraserToolStripButton
            //
            this.eraserToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.eraserToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eraserToolStripButton.Name = "eraserToolStripButton";
            this.eraserToolStripButton.Size = new System.Drawing.Size(58, 22); // Adjusted for "Borracha"
            this.eraserToolStripButton.Text = "Borracha";
            this.eraserToolStripButton.ToolTipText = "Apagar Desenho";
            this.eraserToolStripButton.CheckOnClick = true;
            this.eraserToolStripButton.Click += new System.EventHandler(this.eraserToolStripButton_Click);
            //
            // eraserSizeToolStripButton
            //
            this.eraserSizeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.eraserSizeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eraserSizeToolStripButton.Name = "eraserSizeToolStripButton";
            this.eraserSizeToolStripButton.Size = new System.Drawing.Size(90, 22); // Adjusted for "Tam. Borracha"
            this.eraserSizeToolStripButton.Text = "Tam. Borracha";
            this.eraserSizeToolStripButton.ToolTipText = "Selecionar Tamanho da Borracha";
            this.eraserSizeToolStripButton.Click += new System.EventHandler(this.eraserSizeToolStripButton_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            // Corrected order for DockStyle.Top: Last added is lowest.
            // To have opacityTrackBar at the top, then mainToolStrip, then menuStrip1 (if visible):
            // Add menuStrip1 first, then mainToolStrip, then opacityTrackBar.
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.opacityTrackBar);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Lousa Interativa"; // Translated Form Title
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacityTrackBar)).EndInit();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
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
        private System.Windows.Forms.ToolStrip mainToolStrip; // Declaration
        private System.Windows.Forms.ToolStripButton penToolStripButton; // Declaration
        private System.Windows.Forms.ToolStripButton lineColorToolStripButton; // Renamed from penColorToolStripButton
        private System.Windows.Forms.ToolStripButton lineThicknessToolStripButton; // Renamed from penSizeToolStripButton
        private System.Windows.Forms.ToolStripButton selectToolStripButton; // Declaration
        private System.Windows.Forms.ToolStripButton linesToolStripButton; // Declaration - New Lines Tool
        private System.Windows.Forms.ToolStripButton eraserToolStripButton; // Declaration - Eraser Tool
        private System.Windows.Forms.ToolStripButton eraserSizeToolStripButton; // Declaration - Eraser Size Tool
    }
}
