using System; // For Math.Clamp
using System.Windows.Forms;
using System.Drawing; // For Point, Color, etc.
using System.Drawing.Drawing2D; // For SmoothingMode and LineCap
using System.Collections.Generic; // For List<DrawableLine>

namespace LousaInterativa
{
    public partial class FrmLousaInterativa : Form
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
        private bool _isLinesToolActive = false; // Field for the new Lines tool state
        private System.Drawing.Color _currentPenColor = System.Drawing.Color.Black; // Field for current pen color
        private int _currentPenSize = 1; // Field for current pen size
        // private System.Collections.Generic.List<DrawableLine> _drawnLines = new System.Collections.Generic.List<DrawableLine>(); // REMOVED - Now a local var in Load, data lives in DrawingSurfaceForm
        // private System.Drawing.Point? _currentLineStartPoint = null; // REMOVED - Old pen tool's start point
        private System.Drawing.Point? _currentLineStartPointMiddleMouse = null; // For the new Lines tool
        private DrawableLine _selectedLine = null; // Field to store the currently selected line
        private DrawingSurfaceForm _drawingSurface; // Added for DrawingSurfaceForm

        // Eraser tool fields
        private bool _isEraserToolActive = false;
        private int _currentEraserSize = 10; // Default eraser size (adjust as needed)
        private bool _isErasing = false; // Track if mouse is down for erasing

        public FrmLousaInterativa()
        {
            InitializeComponent();

            // Enable double buffering for smoother drawing
            this.DoubleBuffered = true;

            // Wire up event handlers
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);

            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            this.LocationChanged += new System.EventHandler(this.FrmLousaInterativa_LocationChanged); // Added
            this.SizeChanged += new System.EventHandler(this.FrmLousaInterativa_SizeChanged); // Added

            // _lastOpaqueBackColor will be initialized in Form1_Load after settings are loaded
            // _previousFormBorderStyle & _previousWindowState also initialized in Form1_Load

            _drawingSurface = new DrawingSurfaceForm(this);
            _drawingSurface.Owner = this;

            // Register DrawingSurface mouse events for eraser
            _drawingSurface.MouseDown += DrawingSurface_MouseDown;
            _drawingSurface.MouseMove += DrawingSurface_MouseMove;
            _drawingSurface.MouseUp += DrawingSurface_MouseUp;
        }

        private void FrmLousaInterativa_LocationChanged(object sender, EventArgs e)
        {
            UpdateDrawingSurfaceBounds();
        }

        private void FrmLousaInterativa_SizeChanged(object sender, EventArgs e)
        {
            UpdateDrawingSurfaceBounds();
        }

        private void UpdateDrawingSurfaceBounds()
        {
            if (_drawingSurface == null) return;

            Rectangle clientRectInScreenCoords = this.RectangleToScreen(this.ClientRectangle);

            _drawingSurface.Bounds = clientRectInScreenCoords;
            // _drawingSurface.BringToFront(); // Owner relationship should handle z-order with TopMost
        }

        public void HandleSurfaceMouseClick(MouseEventArgs e)
        {
            if (!this.Focused)
            {
                this.Focus();
            }
            if (_isEraserToolActive) return; // Simple clicks don't do anything with eraser active
            this.Form1_MouseClick(this, e);
        }

        // Mouse event handlers for DrawingSurface events (for Eraser)
        private void DrawingSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.Focused)
            {
                this.Focus();
            }
            if (_isEraserToolActive && e.Button == MouseButtons.Left)
            {
                _isErasing = true;
                // e.Location is already in DrawingSurface client coordinates
                _drawingSurface.AddEraserMark(e.Location);
            }
            // Forward to main MouseClick for other tools if necessary, or handle other tools' MouseDown here
            // else if (!this.MouseButtons.HasFlag(MouseButtons.None) && !_isEraserToolActive)
            // {
            //     // This would simulate a click on FrmLousaInterativa for other tools
            //     // Potentially complex if other tools also need MouseDown/Move/Up
            //     HandleSurfaceMouseClick(e);
            // }
        }

        private void DrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isEraserToolActive && _isErasing) // No need to check e.Button == MouseButtons.Left here, _isErasing covers it
            {
                // e.Location is already in DrawingSurface client coordinates
                _drawingSurface.AddEraserMark(e.Location);
            }
        }

        private void DrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isEraserToolActive && e.Button == MouseButtons.Left)
            {
                _isErasing = false;
            }
        }

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Set high quality graphics options
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Line drawing is now handled by DrawingSurfaceForm.
            // this._drawnLines might be cleared or used as a source for DrawingSurfaceForm.
            // FrmLousaInterativa's Paint method should primarily handle its own UI elements
            // and background, but not the dynamic drawing of lines or temporary markers.

            // Optional: Draw a visual marker for the current line's start point if it's set (old pen tool)
            // Only show if pen tool was somehow active and a point was set. Given pen tool is disabled, this is defensive.
            // if (this._isPenToolActive && this._currentLineStartPoint != null) // REMOVED - _currentLineStartPoint is removed
            // {
            //     // Example: Draw a small circle at the start point
            //     // Adjust color and size as needed
            //     using (System.Drawing.SolidBrush startPointBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(128, this._currentPenColor))) // Semi-transparent version of pen color
            //     {
            //         int markerSize = Math.Max(2, this._currentPenSize / 2 + 2); // Make marker size relative to pen size but not too small
            //         e.Graphics.FillEllipse(startPointBrush,
            //                                this._currentLineStartPoint.Value.X - markerSize / 2,
            //                                this._currentLineStartPoint.Value.Y - markerSize / 2,
            //                                markerSize, markerSize);
            //     }
            // }

            // Visual marker for the new "Lines" tool's first point (middle mouse)
            // This is now handled by DrawingSurfaceForm_Paint
            // if (this._isLinesToolActive && this._currentLineStartPointMiddleMouse != null)
            // {
            //     using (System.Drawing.SolidBrush startPointBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(128, this._currentPenColor))) // Semi-transparent
            //     {
            //         int markerSize = Math.Max(2, this._currentPenSize / 2 + 2);
            //         e.Graphics.FillEllipse(startPointBrush,
            //                                this._currentLineStartPointMiddleMouse.Value.X - markerSize / 2,
            //                                this._currentLineStartPointMiddleMouse.Value.Y - markerSize / 2,
            //                                markerSize, markerSize);
            //     }
            // }

            // Draw visual feedback for the selected line
            // TODO: Selection logic might need to interact with _drawingSurface lines.
            // For now, if _selectedLine is from _drawnLines (which is now on _drawingSurface), this won't work directly
            // without fetching lines from _drawingSurface or making _selectedLine refer to a line from _drawingSurface.
            // This part needs careful review in a subsequent step if selection is to work with DrawingSurfaceForm.
            // Selection feedback is now exclusively drawn by DrawingSurfaceForm.
            // FrmLousaInterativa's Paint method is now only responsible for its own background and UI elements,
            // not for any of the dynamic drawing content (lines, selections, markers).
        }

        private void Form1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this._isPenToolActive && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // This block is effectively dead code as _isPenToolActive is always false.
                // Content related to _currentLineStartPoint was here.
                // For safety and clarity, ensuring no operations if it were somehow true:
                this.Invalidate(); // Invalidate if any visual change was expected
            }
            else if (this._isSelectToolActive && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // New logic for select tool
                bool lineHit = false;
                // Iterate in reverse order to select the topmost line if lines overlap
                if (_drawingSurface != null)
                {
                    var linesToSearch = _drawingSurface.GetLines(); // Get lines from DrawingSurface
                    for (int i = linesToSearch.Count - 1; i >= 0; i--)
                    {
                        DrawableLine currentLine = linesToSearch[i];
                        if (IsPointNearLine(e.Location, currentLine))
                        {
                            this._selectedLine = currentLine;
                            _drawingSurface.SelectedLine = this._selectedLine; // Inform DrawingSurface
                            lineHit = true;
                            break;
                        }
                    }
                }

                if (!lineHit)
                {
                    this._selectedLine = null;
                    if (_drawingSurface != null) _drawingSurface.SelectedLine = null;
                }
                _drawingSurface?.Invalidate(); // Ensure DrawingSurface redraws for selection feedback
            }
            else if (this._isLinesToolActive && e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                 // The owner form's e.Location is already in its own client coordinates.
                 // These are the coordinates that should be used for drawing on the surface.
                if (this._currentLineStartPointMiddleMouse == null)
                {
                    this._currentLineStartPointMiddleMouse = e.Location; // Stored in FrmLousaInterativa client coordinates
                    if (_drawingSurface != null)
                    {
                        // Pass the same client coordinates to DrawingSurfaceForm
                        _drawingSurface.CurrentLineStartPoint = e.Location;
                        _drawingSurface.CurrentLineColor = _currentPenColor;
                        _drawingSurface.CurrentLineWidth = _currentPenSize;
                        _drawingSurface.Invalidate();
                    }
                }
                else
                {
                    DrawableLine newLine = new DrawableLine(
                        this._currentLineStartPointMiddleMouse.Value, // Start point in FrmLousaInterativa client coordinates
                        e.Location, // End point in FrmLousaInterativa client coordinates
                        this._currentPenColor,
                        this._currentPenSize
                    );
                    _drawingSurface?.AddLine(newLine); // Add to surface
                    this._currentLineStartPointMiddleMouse = null;
                    if(_drawingSurface != null)
                    {
                        _drawingSurface.CurrentLineStartPoint = null;
                        // AddLine already invalidates _drawingSurface
                    }
                }
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
                // Load and clamp eraser size
                _currentEraserSize = Math.Clamp(_currentSettings.EraserSize, 1, 10);
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

            // Ensure pen tool is not active on load, as it's being disabled.
            this._isPenToolActive = false;
            if (this.penToolStripButton != null) // Should exist, but good practice to check
            {
                this.penToolStripButton.Checked = false;
            }

            // Sincronizar inicialmente e mostrar a superfície de desenho
            UpdateDrawingSurfaceBounds();
            if (_drawingSurface != null)
            {
                // Pass initial properties from FrmLousaInterativa to DrawingSurfaceForm
                _drawingSurface.CurrentLineColor = this._currentPenColor;
                _drawingSurface.CurrentLineWidth = this._currentPenSize;
                _drawingSurface.EraserSize = _currentEraserSize;
                _drawingSurface.CurrentLineStartPoint = this._currentLineStartPointMiddleMouse;
                _drawingSurface.IsLinesToolActive = this._isLinesToolActive;
                _drawingSurface.IsEraserActive = this._isEraserToolActive;
                _drawingSurface.SelectedLine = this._selectedLine;


                _drawingSurface.Show();

                // Load lines from settings and pass them to the drawing surface
                List<DrawableLine> loadedLines = _currentSettings.DrawnLines ?? new List<DrawableLine>();
                _drawingSurface.SetLines(loadedLines);

                // FrmLousaInterativa._drawnLines is no longer the primary store.
                this.Invalidate(); // FrmLousaInterativa no longer draws lines directly.
            }
        }

        // Helper method to deactivate all tools and clear shared states.
        // The button that was just clicked (and whose Checked state is now true due to CheckOnClick)
        // is passed as 'activatedButton' to avoid unchecking it.
        private void DeactivateAllTools(ToolStripButton activeButton = null)
        {
            // Store current states from buttons if they are the active one
            bool isLinesNowActive = (activeButton == linesToolStripButton && linesToolStripButton.Checked);
            bool isSelectNowActive = (activeButton == selectToolStripButton && selectToolStripButton.Checked);
            bool isEraserNowActive = (activeButton == eraserToolStripButton && eraserToolStripButton.Checked);

            // Uncheck all other buttons
            if (linesToolStripButton != activeButton && linesToolStripButton != null) linesToolStripButton.Checked = false;
            if (selectToolStripButton != activeButton && selectToolStripButton != null) selectToolStripButton.Checked = false;
            if (eraserToolStripButton != activeButton && eraserToolStripButton != null) eraserToolStripButton.Checked = false;
            if (penToolStripButton != null) penToolStripButton.Checked = false; // Old pen tool, always ensure it's off

            // Update internal state flags
            _isLinesToolActive = isLinesNowActive;
            _isSelectToolActive = isSelectNowActive;
            _isEraserToolActive = isEraserNowActive;
            _isPenToolActive = false; // Always false

            // Update DrawingSurface states
            if (_drawingSurface != null)
            {
                _drawingSurface.IsLinesToolActive = _isLinesToolActive;
                _drawingSurface.IsEraserActive = _isEraserToolActive;
                // If select tool is not active, clear selection on drawing surface
                if (!_isSelectToolActive)
                {
                    _drawingSurface.SelectedLine = null;
                }
                // If lines tool is not active, clear its pending start point on drawing surface
                if (!_isLinesToolActive)
                {
                    _drawingSurface.CurrentLineStartPoint = null;
                }
            }

            // Clear FrmLousaInterativa's tracking of selected line if select tool is not the active one
            if (!_isSelectToolActive)
            {
                _selectedLine = null;
            }
            // Clear FrmLousaInterativa's tracking of line start point if lines tool is not the active one
            if (!_isLinesToolActive)
            {
                _currentLineStartPointMiddleMouse = null;
            }

            // Reset cursor - the active tool's handler will set it appropriately.
            this.Cursor = Cursors.Default;

            // Request redraw of drawing surface to reflect any changes (e.g., selection highlight, start point marker)
            _drawingSurface?.Invalidate();
        }


        private void lineThicknessToolStripButton_Click(object sender, EventArgs e) // Renamed from penSizeToolStripButton_Click
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
                    if (_drawingSurface != null)
                    {
                        _drawingSurface.CurrentLineWidth = this._currentPenSize;
                        _drawingSurface.Invalidate(); // Ensure marker preview updates if visible
                    }
                    // Optional: Update some UI element to show the new pen size, if desired.
                }
            }
        }

        private void lineColorToolStripButton_Click(object sender, EventArgs e) // Renamed from penColorToolStripButton_Click
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
                     if (_drawingSurface != null)
                     {
                        _drawingSurface.CurrentLineColor = this._currentPenColor;
                        _drawingSurface.Invalidate(); // Ensure marker preview updates if visible
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
                if (_drawingSurface != null)
                {
                    _currentSettings.DrawnLines = new List<DrawableLine>(_drawingSurface.GetLines());
                }
                else
                {
                    // Fallback if drawingSurface is somehow null - save an empty list or previously loaded state if it existed.
                    // Since _drawnLines is cleared, this path should ideally not be hit if _drawingSurface is always available.
                    _currentSettings.DrawnLines = new List<DrawableLine>();
                }
                this._currentSettings.PenColorArgb = this._currentPenColor.ToArgb();
                this._currentSettings.PenSize = this._currentPenSize;
                _currentSettings.EraserSize = _currentEraserSize; // Save eraser size
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

        private void eraserSizeToolStripButton_Click(object sender, EventArgs e)
        {
            using (EraserSizeForm eraserSizeForm = new EraserSizeForm(_currentEraserSize))
            {
                if (eraserSizeForm.ShowDialog(this) == DialogResult.OK)
                {
                    _currentEraserSize = eraserSizeForm.SelectedEraserSize;
                    if (_drawingSurface != null)
                    {
                        _drawingSurface.EraserSize = _currentEraserSize;
                    }
                    // Save settings immediately for consistency
                    if (_currentSettings != null)
                    {
                        _currentSettings.EraserSize = _currentEraserSize;
                        SettingsManager.SaveSettings(_currentSettings);
                    }
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
            // Pen tool is disabled
            this._isPenToolActive = false;
            this.penToolStripButton.Checked = false; // Ensure it's always unchecked
            this.Cursor = System.Windows.Forms.Cursors.Default;
            // if (this._currentLineStartPoint != null) // REMOVED - _currentLineStartPoint is removed
            // {
            //     this._currentLineStartPoint = null;
            //     this.Invalidate();
            // }
            this.Invalidate(); // Ensure repaint if any visual assumptions were tied to old pen state
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
                    // _isPenToolActive = false; // This is handled by penToolStripButton_Click
                }
                this._isPenToolActive = false; // Explicitly ensure pen tool is deactivated

                if (this.linesToolStripButton != null)
                {
                    this.linesToolStripButton.Checked = false;
                }
                this._isLinesToolActive = false; // Deactivate Lines tool

                // Cancel any pending line drawing from the pen tool (old) - REMOVED _currentLineStartPoint
                // if (this._currentLineStartPoint != null)
                // {
                //     this._currentLineStartPoint = null;
                //     this.Invalidate();
                // }
                // Cancel any pending line drawing from the Lines tool (new)
                if (this._currentLineStartPointMiddleMouse != null)
                {
                    this._currentLineStartPointMiddleMouse = null;
                    // this.Invalidate(); // FrmLousaInterativa no longer draws this marker
                }
                if (_drawingSurface != null)
                {
                    _drawingSurface.CurrentLineStartPoint = null;
                    _drawingSurface.Invalidate(); // Clear marker on surface
                }
            }
            // If select tool is unchecked (meaning it was just deactivated by the user clicking it again)
            // or if another tool was activated (which DeactivateAllTools would have handled by unchecking this one),
            // ensure selection state is cleared.
            if (!selectToolStripButton.Checked)
            {
                 _selectedLine = null;
                 if (_drawingSurface != null) _drawingSurface.SelectedLine = null;
            }
            // DeactivateAllTools has already handled unchecking other buttons and updating most state.
            // The primary role here is to set the correct cursor if this tool IS active.
            if (_isSelectToolActive)
            {
                 this.Cursor = Cursors.Default;
            }
             _drawingSurface?.Invalidate(); // Ensure selection visual is updated
        }

        private void linesToolStripButton_Click(object sender, EventArgs e)
        {
            // The CheckOnClick property automatically toggles linesToolStripButton.Checked.
            // DeactivateAllTools will use this new Checked state to determine _isLinesToolActive.
            DeactivateAllTools(linesToolStripButton);

            if (_isLinesToolActive)
            {
                this.Cursor = System.Windows.Forms.Cursors.Cross;
                // _currentLineStartPointMiddleMouse and _drawingSurface.CurrentLineStartPoint are reset by DeactivateAllTools
                // if this tool was not the one activated. If it was activated, they remain as they were (e.g. null).
            }
        }

        private void eraserToolStripButton_Click(object sender, EventArgs e)
        {
            DeactivateAllTools(eraserToolStripButton);

            if (_isEraserToolActive)
            {
                this.Cursor = Cursors.Default; // Or a custom eraser cursor
                if (_drawingSurface != null) _drawingSurface.EraserSize = _currentEraserSize;
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
            else if (e.KeyCode == Keys.Delete && _isSelectToolActive && _selectedLine != null && _drawingSurface != null)
            {
                bool removed = _drawingSurface.RemoveLine(_selectedLine);
                if (removed)
                {
                    _selectedLine = null;
                    // _drawingSurface.SelectedLine is already set to null by RemoveLine if it was the selected one
                    // _drawingSurface.Invalidate() is called by RemoveLine.
                }
                e.Handled = true;
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
