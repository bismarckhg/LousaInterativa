using System.Windows.Forms;

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

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            // _lastOpaqueBackColor will be initialized in Form1_Load after settings are loaded
            // _previousFormBorderStyle & _previousWindowState also initialized in Form1_Load
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
