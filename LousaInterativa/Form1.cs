using System; // For Math.Clamp
using System.Windows.Forms;
using System.Drawing; // For Point, Color, etc.
using System.Drawing.Drawing2D; // For SmoothingMode and LineCap
using System.Collections.Generic; // For List<DrawableLine>

namespace LousaInterativa
{
    public partial class Form1 : Form
    {
        // AppSettings instance
        private AppSettings _currentSettings;

        // Fields for Full Screen functionality
        private FormWindowState _previousWindowState;
        private FormBorderStyle _previousFormBorderStyle; // Used by FullScreen
        private bool _isFullScreen = false;

        // Fields for Window Transparency functionality
        private bool _isWindowTransparent = false;
        private FormBorderStyle _transparencyPreviousFormBorderStyle;
        private Color _lastOpaqueBackColor;
        private readonly System.Drawing.Color _magicOpaqueKeyForTransparency = System.Drawing.Color.FromArgb(255, 7, 7, 7); // An arbitrary, opaque, dark color
        private double _currentFormOpacity = 1.0; // Field for current form opacity level
        private bool _isPenToolActive = false; // Field for pen tool state
        private bool _isSelectToolActive = false; // Field for select tool state
        private System.Drawing.Color _currentPenColor = System.Drawing.Color.Black; // Field for current pen color
        private int _currentPenSize = 1; // Field for current pen size
        private System.Collections.Generic.List<DrawableLine> _drawnLines = new System.Collections.Generic.List<DrawableLine>();
        private System.Drawing.Point? _currentLineStartPoint = null;
        private DrawableLine _selectedLine = null; // Field to store the currently selected line

        public Form1()
        {
            InitializeComponent();

            // Enable double buffering for smoother drawing
            this.DoubleBuffered = true;

            // Wire up event handlers
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);

            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            // _lastOpaqueBackColor will be initialized in Form1_Load after settings are loaded
            // _previousFormBorderStyle & _previousWindowState also initialized in Form1_Load
        }

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Set high quality graphics options
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw all stored lines
            foreach (DrawableLine line in this._drawnLines)
            {
                if (line == null) continue; // Basic null check

                using (System.Drawing.Pen linePen = new System.Drawing.Pen(line.LineColor, line.LineWidth))
                {
                    linePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    linePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    e.Graphics.DrawLine(linePen, line.StartPoint, line.EndPoint);
                }
            }

            // Optional: Draw a visual marker for the current line's start point if it's set
            if (this._currentLineStartPoint != null)
            {
                // Example: Draw a small circle at the start point
                // Adjust color and size as needed
                using (System.Drawing.SolidBrush startPointBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(128, this._currentPenColor))) // Semi-transparent version of pen color
                {
                    int markerSize = Math.Max(2, this._currentPenSize / 2 + 2); // Make marker size relative to pen size but not too small
                    e.Graphics.FillEllipse(startPointBrush,
                                           this._currentLineStartPoint.Value.X - markerSize / 2,
                                           this._currentLineStartPoint.Value.Y - markerSize / 2,
                                           markerSize, markerSize);
                }
            }

            // Draw visual feedback for the selected line
            if (this._selectedLine != null)
            {
                // Calculate the bounding box of the selected line
                float minX = Math.Min(this._selectedLine.StartPoint.X, this._selectedLine.EndPoint.X);
                float minY = Math.Min(this._selectedLine.StartPoint.Y, this._selectedLine.EndPoint.Y);
                float maxX = Math.Max(this._selectedLine.StartPoint.X, this._selectedLine.EndPoint.X);
                float maxY = Math.Max(this._selectedLine.StartPoint.Y, this._selectedLine.EndPoint.Y);

                // Add padding to the bounding box
                float padding = Math.Max(4, this._selectedLine.LineWidth / 2.0f + 2);

                RectangleF bounds = new RectangleF(
                    minX - padding,
                    minY - padding,
                    (maxX - minX) + (2 * padding),
                    (maxY - minY) + (2 * padding)
                );

                // Use a semi-transparent brush for the selection rectangle
                using (System.Drawing.SolidBrush selectionBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(80, 0, 100, 255))) // Semi-transparent blue
                {
                    e.Graphics.FillRectangle(selectionBrush, bounds);
                }

                // Optional: Draw small handles (commented out as per plan)
                // int handleSize = 6;
                // using (System.Drawing.SolidBrush handleBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(150, 0, 0, 200)))
                // {
                //     e.Graphics.FillRectangle(handleBrush, _selectedLine.StartPoint.X - handleSize / 2, _selectedLine.StartPoint.Y - handleSize / 2, handleSize, handleSize);
                //     e.Graphics.FillRectangle(handleBrush, _selectedLine.EndPoint.X - handleSize / 2, _selectedLine.EndPoint.Y - handleSize / 2, handleSize, handleSize);
                // }
            }
        }

        private void Form1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this._isPenToolActive && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // Existing pen tool logic for drawing lines
                if (this._currentLineStartPoint == null)
                {
                    // First click: define start point
                    this._currentLineStartPoint = e.Location;
                    this.Invalidate(); // Trigger repaint to draw the start point marker
                }
                else
                {
                    // Second click: define end point and complete the line
                    DrawableLine newLine = new DrawableLine(
                        this._currentLineStartPoint.Value,
                        e.Location,
                        this._currentPenColor,
                        this._currentPenSize
                    );
                    this._drawnLines.Add(newLine);
                    this._currentLineStartPoint = null; // Reset for the next line

                    this.Invalidate(); // Trigger a repaint to draw the new line
                }
            }
            else if (this._isSelectToolActive && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // New logic for select tool
                bool lineHit = false;
                // Iterate in reverse order to select the topmost line if lines overlap
                for (int i = this._drawnLines.Count - 1; i >= 0; i--)
                {
                    DrawableLine currentLine = this._drawnLines[i];
                    if (IsPointNearLine(e.Location, currentLine)) // Using the helper method
                    {
                        this._selectedLine = currentLine;
                        lineHit = true;
                        break; // Select only one line (the topmost one hit)
                    }
                }

                if (!lineHit)
                {
                    this._selectedLine = null; // Clicked on empty space, deselect
                }

                this.Invalidate(); // Trigger a repaint to show/hide selection feedback
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _currentSettings = SettingsManager.LoadSettings();

            this.Size = _currentSettings.NormalFormSize;
            this.Location = _currentSettings.NormalFormLocation;
            this.WindowState = _currentSettings.NormalWindowState;
            this.FormBorderStyle = _currentSettings.NormalFormBorderStyle;
            this.BackColor = Color.FromArgb(_currentSettings.LastBackColorAsArgb);

            // Initialize _currentFormOpacity and set form Opacity directly from settings
            this._currentFormOpacity = _currentSettings.FormOpacity;
            if (this._currentFormOpacity < 0.0) this._currentFormOpacity = 0.0; // Clamp
            if (this._currentFormOpacity > 1.0) this._currentFormOpacity = 1.0; // Clamp
            this.Opacity = this._currentFormOpacity;

            // Initialize internal state fields based on loaded & applied settings
            _lastOpaqueBackColor = this.BackColor;
            _previousFormBorderStyle = this.FormBorderStyle; // For full-screen feature's runtime restoration
            _previousWindowState = this.WindowState;       // For full-screen feature's runtime restoration
            // _transparencyPreviousFormBorderStyle will be set when transparency is first toggled by user or load.

            // Apply menu visibility
            if (this.menuStrip1 != null)
            {
                this.menuStrip1.Visible = _currentSettings.IsMenuVisible;
            }
            if (this.toggleMenuVisibilityMenuItem != null) // Sync check state on load
            {
                this.toggleMenuVisibilityMenuItem.Checked = this.menuStrip1.Visible;
            }

            // Apply Pen Color
            if (this._currentSettings != null)
            {
                this._currentPenColor = System.Drawing.Color.FromArgb(this._currentSettings.PenColorArgb);
                // Clamp pen size to be within 1 and 15 (inclusive)
                this._currentPenSize = Math.Clamp(this._currentSettings.PenSize, 1, 15);
            }


            // Apply states if they were active
            // Note: ToggleFullScreen() and ToggleWindowTransparency() will call SaveSettings.
            if (_currentSettings.IsFullScreen)
            {
                ToggleFullScreen(); // This will set _isFullScreen and save settings
            }
            if (_currentSettings.WasWindowTransparent)
            {
                // Ensure this is called *after* full-screen, as it might also change FormBorderStyle
                ToggleWindowTransparency(); // This will set _isWindowTransparent and save settings
            }

            // Final check on TransparencyKey after all initial toggles
            if (!_isWindowTransparent && this.BackColor.A < 255)
            {
                this.TransparencyKey = this.BackColor;
            }
            else if (!_isWindowTransparent) // Is opaque or full-window transparent
            {
                this.TransparencyKey = Color.Empty;
            }
        }

        private void penSizeToolStripButton_Click(object sender, EventArgs e)
        {
            // Create an instance of the PenSizeForm, passing the current pen size
            using (PenSizeForm penSizeForm = new PenSizeForm(this._currentPenSize))
            {
                // Show the dialog modally
                if (penSizeForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    // User clicked OK, update the current pen size
                    this._currentPenSize = penSizeForm.SelectedPenSize;

                    // Update and save settings
                    if (this._currentSettings != null)
                    {
                        this._currentSettings.PenSize = this._currentPenSize;
                        SettingsManager.SaveSettings(this._currentSettings);
                    }
                    // Optional: Update some UI element to show the new pen size, if desired.
                }
            }
        }

        private void penColorToolStripButton_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog())
            {
                colorDialog.Color = this._currentPenColor; // Set initial color in dialog
                colorDialog.FullOpen = true; // Show custom colors section

                if (colorDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    this._currentPenColor = colorDialog.Color;

                    if (this._currentSettings != null)
                    {
                        this._currentSettings.PenColorArgb = this._currentPenColor.ToArgb();
                        SettingsManager.SaveSettings(this._currentSettings);
                    }
                    // Optional: Update some UI element to show the new pen color, if desired.
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If the window is currently in a "normal" state (not full screen), capture its geometry.
            if (!this._isFullScreen)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    // If minimized, save the state *before* minimization as the "normal" state.
                    _currentSettings.NormalWindowState = this._previousWindowState;
                }
                else
                {
                    _currentSettings.NormalWindowState = this.WindowState;
                }
                _currentSettings.NormalFormSize = this.Size;
                _currentSettings.NormalFormLocation = this.Location;
                // Determine the "true" normal border style, accounting for transparency.
                _currentSettings.NormalFormBorderStyle = GetTrueNormalFormBorderStyle();
            }
            // If currently full-screen, _currentSettings.Normal... properties should already
            // hold the state from before full-screen was entered.

            _currentSettings.LastBackColorAsArgb = _lastOpaqueBackColor.ToArgb();
            _currentSettings.IsFullScreen = _isFullScreen;
            _currentSettings.WasWindowTransparent = _isWindowTransparent;
            _currentSettings.FormOpacity = this._currentFormOpacity; // Ensure final opacity is saved
            if (this.menuStrip1 != null)
            {
                _currentSettings.IsMenuVisible = this.menuStrip1.Visible;
            }
            else
            {
                _currentSettings.IsMenuVisible = false; // Default if menuStrip1 is unexpectedly null
            }
            if (this._currentSettings != null)
            {
                this._currentSettings.PenColorArgb = this._currentPenColor.ToArgb();
                this._currentSettings.PenSize = this._currentPenSize;
            }

            SettingsManager.SaveSettings(_currentSettings);
        }

        private void ToggleFullScreen()
        {
            if (_isFullScreen) // About to exit full screen
            {
                this.WindowState = _currentSettings.NormalWindowState;
                this.FormBorderStyle = _currentSettings.NormalFormBorderStyle; // Restore true normal border style
                this.Size = _currentSettings.NormalFormSize;
                this.Location = _currentSettings.NormalFormLocation;
                _isFullScreen = false;

                // If exiting full-screen AND transparency should be active, re-apply transparency visuals
                if (this._isWindowTransparent)
                {
                    this.FormBorderStyle = FormBorderStyle.None; // Transparency needs borderless
                    Color magicTransparentColor = Color.FromArgb(1, 1, 1, 1);
                    this.BackColor = magicTransparentColor;
                    this.TransparencyKey = magicTransparentColor;
                }
            }
            else // About to enter full screen
            {
                // 1. Save current runtime state for immediate toggle back (used by this.ToggleFullScreen itself)
                this._previousWindowState = this.WindowState;
                this._previousFormBorderStyle = this.FormBorderStyle;
                // Note: For full-screen, actual previous Size/Location are less critical to store in these
                // runtime fields (_previousSize, _previousLocation) because exiting full-screen
                // primarily uses _currentSettings.NormalFormSize/Location.

                // 2. Update _currentSettings.Normal... (persisted "true normal" state)
                // These should reflect the state of the window *before* going full-screen.
                _currentSettings.NormalWindowState = this._previousWindowState; // The state just before maximizing
                _currentSettings.NormalFormSize = this.Size; // Current size before maximizing (if not maximized yet)
                _currentSettings.NormalFormLocation = this.Location; // Current location before moving
                _currentSettings.NormalFormBorderStyle = GetTrueNormalFormBorderStyle(); // Determine true normal border style

                // 3. Apply full screen visuals
                this.WindowState = FormWindowState.Maximized; // This should be after saving NormalWindowState
                this.FormBorderStyle = FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                _isFullScreen = true;
            }
            _currentSettings.IsFullScreen = _isFullScreen;
            SettingsManager.SaveSettings(_currentSettings);
        }

        private void ToggleWindowTransparency()
        {
            if (_isWindowTransparent) // Deactivate transparency
            {
                // Restore the border style that was active before transparency was turned on.
                // This could be FormBorderStyle.None if full-screen was active, which is fine.
                this.FormBorderStyle = _transparencyPreviousFormBorderStyle;
                this.BackColor = _lastOpaqueBackColor;

                // Restore user-defined semi-transparency if the last opaque color had it.
                if (_lastOpaqueBackColor.A < 255)
                {
                    this.TransparencyKey = _lastOpaqueBackColor;
                }
                else
                {
                    this.TransparencyKey = Color.Empty; // Fully opaque
                }
                _isWindowTransparent = false;
            }
            else // Activate transparency
            {
                // If form is semi-transparent via Opacity property, make it fully opaque first
                // for TransparencyKey to work reliably.
                if (this.Opacity < 1.0)
                {
                    this.Opacity = 1.0;
                    this._currentFormOpacity = 1.0;
                    if (this._currentSettings != null)
                    {
                        this._currentSettings.FormOpacity = 1.0;
                        // Settings will be saved by the general SaveSettings call below
                    }
                }

                // Store the current border style (which might be None if full-screen)
                // This is so we can revert to it if transparency is toggled off.
                _transparencyPreviousFormBorderStyle = this.FormBorderStyle;
                // _lastOpaqueBackColor should be up-to-date from color changes or load.

                this.FormBorderStyle = FormBorderStyle.None; // Transparency requires a borderless window.
                this.BackColor = _magicOpaqueKeyForTransparency;
                this.TransparencyKey = _magicOpaqueKeyForTransparency; // BackColor and TransparencyKey must match.
                _isWindowTransparent = true;
            }
            _currentSettings.WasWindowTransparent = _isWindowTransparent;
            // Save all relevant settings, including potential FormOpacity change
            SettingsManager.SaveSettings(_currentSettings);
        }

        private void ToggleMenuVisibility()
        {
            if (this.menuStrip1 != null)
            {
                this.menuStrip1.Visible = !this.menuStrip1.Visible;

                if (this._currentSettings != null)
                {
                    this._currentSettings.IsMenuVisible = this.menuStrip1.Visible;
                    SettingsManager.SaveSettings(this._currentSettings);
                }

                if (this.toggleMenuVisibilityMenuItem != null)
                {
                    this.toggleMenuVisibilityMenuItem.Checked = this.menuStrip1.Visible;
                }
            }
        }

        private void toggleMenuVisibilityMenuItem_Click(object sender, EventArgs e)
        {
            ToggleMenuVisibility();
        }

        private void ApplyFormOpacity(double opacityLevel, bool saveSetting = true)
        {
            // Clamp opacityLevel between 0.0 (fully transparent) and 1.0 (fully opaque)
            if (opacityLevel < 0.0) opacityLevel = 0.0;
            if (opacityLevel > 1.0) opacityLevel = 1.0;

            // If trying to make semi-transparent (Opacity < 1.0) AND
            // full-key transparency (_isWindowTransparent) is currently active,
            // then deactivate full-key transparency first.
            if (opacityLevel < 1.0 && this._isWindowTransparent)
            {
                // ToggleWindowTransparency will call SaveSettings, potentially saving an
                // Opacity of 1.0 if it was just set by ToggleWindowTransparency.
                // Then, the lines below will apply the desired semi-transparent opacityLevel.
                ToggleWindowTransparency();
            }

            this.Opacity = opacityLevel;
            this._currentFormOpacity = opacityLevel;

            if (saveSetting)
            {
                if (this._currentSettings != null) // Ensure settings object exists
                {
                    this._currentSettings.FormOpacity = this._currentFormOpacity;
                    // Avoid double save if ToggleWindowTransparency was called and saved.
                    // However, if ToggleWindowTransparency was *not* called, we need to save.
                    // For simplicity, always saving here is acceptable, or add more complex flag.
                    // Let's assume ToggleWindowTransparency already saved if it was called.
                    // A more robust way: check if opacity actually changed due to ToggleWindowTransparency.
                    // For now, if opacityLevel < 1.0 and _isWindowTransparent was true, a save already happened.
                    // This check is imperfect. A better way would be for ToggleWindowTransparency to return a bool.
                    // Simplification: if saveSetting is true, we save. The state will be consistent.
                    SettingsManager.SaveSettings(this._currentSettings);
                }
            }
        }

        // Helper method to determine the "true" normal FormBorderStyle
        private FormBorderStyle GetTrueNormalFormBorderStyle()
        {
            if (this._isWindowTransparent)
            {
                // If currently transparent, the "true" normal border style is
                // whatever was active before transparency was turned on.
                return this._transparencyPreviousFormBorderStyle;
            }
            else
            {
                // Otherwise, the current FormBorderStyle is the one to consider normal.
                return this.FormBorderStyle;
            }
        }

        private void opacityTrackBar_Scroll(object sender, EventArgs e)
        {
            double newOpacity = (double)this.opacityTrackBar.Value / 100.0;
            ApplyFormOpacity(newOpacity); // saveSetting defaults to true
        }

        private void penToolStripButton_Click(object sender, EventArgs e)
        {
            // penToolStripButton.Checked state is automatically toggled due to CheckOnClick = true
            this._isPenToolActive = this.penToolStripButton.Checked;

            if (this._isPenToolActive)
            {
                this.Cursor = System.Windows.Forms.Cursors.Cross; // Use Crosshair cursor for pen
                if (this.selectToolStripButton != null)
                {
                    this.selectToolStripButton.Checked = false;
                }
                this._isSelectToolActive = false; // Ensure select tool is deactivated
                // _currentLineStartPoint is managed by drawing logic when pen is active
            }
            else // Pen tool deactivated
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
                this._currentLineStartPoint = null; // Cancel pending line if pen tool is deselected
                this.Invalidate(); // Redraw to clear start point marker
            }
        }

        private void selectToolStripButton_Click(object sender, EventArgs e)
        {
            this._isSelectToolActive = this.selectToolStripButton.Checked;

            if (this._isSelectToolActive)
            {
                this.Cursor = System.Windows.Forms.Cursors.Default; // Default cursor for select tool
                if (this.penToolStripButton != null)
                {
                    this.penToolStripButton.Checked = false;
                }
                this._isPenToolActive = false; // Ensure pen tool is deactivated

                // Cancel any pending line drawing from the pen tool
                if (this._currentLineStartPoint != null)
                {
                    this._currentLineStartPoint = null;
                    this.Invalidate(); // Redraw to clear any start point marker
                }
            }
            // If select tool is unchecked, another tool's click handler will manage cursor and active state.
            // If no other tool becomes active, cursor remains Default.
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
                e.Handled = true; // Prevent further processing of F11
            }
            else if (e.KeyCode == Keys.F10) // New condition for F10
            {
                ToggleWindowTransparency();
                e.Handled = true; // Prevent further processing of F10
            }
            else if (e.KeyCode == Keys.F9) // New condition for F9
            {
                this.opacityTrackBar.Visible = !this.opacityTrackBar.Visible;
                if (this.opacityTrackBar.Visible)
                {
                    this.opacityTrackBar.Value = (int)(this._currentFormOpacity * 100);
                }
                e.Handled = true; // Prevent further processing of F9
            }
            else if (e.KeyCode == Keys.F8)
            {
                ToggleMenuVisibility();
                e.Handled = true; // Mark the event as handled
            }
        }

        private void adjustOpacityMenuItem_Click(object sender, EventArgs e)
        {
            this.opacityTrackBar.Visible = !this.opacityTrackBar.Visible;
            if (this.opacityTrackBar.Visible)
            {
                // Update TrackBar's value to reflect current form opacity when it becomes visible
                this.opacityTrackBar.Value = (int)(this._currentFormOpacity * 100);
            }
        }

        private bool IsPointNearLine(Point point, DrawableLine line, int tolerance = 5)
        {
            if (line == null) return false;

            Point p1 = line.StartPoint;
            Point p2 = line.EndPoint;
            Point p = point;

            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            if (dx == 0 && dy == 0) // Line is actually a point
            {
                // Distance from p to p1 (or p2)
                float distToPoint = (float)Math.Sqrt(Math.Pow(p.X - p1.X, 2) + Math.Pow(p.Y - p1.Y, 2));
                return distToPoint <= tolerance + (line.LineWidth / 2.0f);
            }

            // Calculate the t parameter for the projection of point p onto the line defined by p1 and p2
            // t = [(p - p1) . (p2 - p1)] / |p2 - p1|^2
            float t = ((p.X - p1.X) * dx + (p.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            Point closestPoint;
            if (t < 0)
            {
                closestPoint = p1; // Closest point is p1
            }
            else if (t > 1)
            {
                closestPoint = p2; // Closest point is p2
            }
            else
            {
                // Projection falls on the line segment
                closestPoint = new Point((int)(p1.X + t * dx), (int)(p1.Y + t * dy));
            }

            // Calculate distance from p to closestPoint
            float distance = (float)Math.Sqrt(Math.Pow(p.X - closestPoint.X, 2) + Math.Pow(p.Y - closestPoint.Y, 2));

            // Consider line width in tolerance
            return distance <= tolerance + (line.LineWidth / 2.0f);
        }

        private void toggleTransparencyMenuItem_Click(object sender, EventArgs e)
        {
            ToggleWindowTransparency();
        }

        private void changeBackgroundColorMenuItem_Click(object sender, System.EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.FullOpen = true; // Allow alpha selection
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    this._lastOpaqueBackColor = selectedColor;
                    _currentSettings.LastBackColorAsArgb = _lastOpaqueBackColor.ToArgb();

                    if (!_isWindowTransparent)
                    {
                        this.BackColor = selectedColor;
                        if (selectedColor.A < 255)
                        {
                            this.TransparencyKey = selectedColor;
                        }
                        else
                        {
                            this.TransparencyKey = Color.Empty;
                        }
                    }
                    SettingsManager.SaveSettings(_currentSettings);
                }
            }
        }
    }
}
