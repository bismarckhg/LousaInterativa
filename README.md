# Lousa Interativa

Lousa Interativa (Interactive Whiteboard) is a simple Windows Forms application designed to provide a basic digital whiteboard experience. It allows users to change the background color, including transparency, and toggle a full-screen mode for an immersive experience.

## User Interface Notes
-   **Menu Visibility & Access:**
    -   The main menu bar's visibility at startup (shown or hidden) is loaded from settings (defaults to hidden).
    -   Press **F8** to toggle the menu bar's visibility at any time.
    -   Alternatively, use the **View > Show/Hide Menu** option. This menu item displays a checkmark if the menu is currently visible.
    -   Even if the menu bar is hidden, menu commands can typically be accessed by pressing the `Alt` key (e.g., `Alt+V` for the View menu, then navigate with arrow keys or mnemonics like 'M' for "Show/Hide Menu").

## Toolbar
The application features a toolbar docked at the top of the window (below the opacity adjustment trackbar, if visible) for quick access to drawing tools and other commands.

### Drawing Tools
*   **Pen Tool:**
    *   **Button:** "Pen" (text button on the toolbar).
    *   **Functionality:**
        *   Click to activate or deactivate Pen mode. When active, the mouse cursor changes to a crosshair.
        *   **To Draw a Line:**
            1.  Activate the Pen tool.
            2.  Click on the whiteboard (the main form area) to set the starting point of your line. A temporary marker may indicate this point.
            3.  Click again on the whiteboard to set the end point of your line.
            4.  The line will be drawn using the currently selected Pen Color and Pen Size.
    *   *(Note: More advanced drawing features like freehand drawing, shapes, selection, and editing are planned for future updates.)*
*   **Pen Color Tool:**
    *   **Button:** "Color" (text button on the toolbar, next to "Pen").
    *   **Functionality:** Click to open a color selection dialog. The chosen color becomes the active pen color for subsequent drawing operations.
    *   **Persistence:** The selected pen color is saved and reloaded across application sessions.
*   **Pen Size Tool:**
    *   **Button:** "Size" (text button on the toolbar, next to "Color").
    *   **Functionality:** Click to open a dialog with a trackbar allowing selection of pen size from 1px to 15px. This size affects subsequent drawing operations.
    *   **Persistence:** The selected pen size is saved and reloaded across application sessions.

## Features

-   **Full-Screen Mode (F11 / View Menu):**
    -   Toggle full-screen display.
    -   Provides an immersive, borderless window experience.
-   **Customizable Background Color (Tools Menu):**
    -   Change the application's background color.
    -   Allows selection of colors with alpha values for a semi-transparent background.
-   **Toggle Window Transparency (F10 / View Menu):**
    -   Activate/deactivate full window transparency (client area see-through, borders removed using `TransparencyKey`).
    -   Activating this mode sets form opacity to 100%.
-   **Adjustable Form Opacity (F9 / Tools Menu):**
    -   Control overall window transparency (0%-100%) via a TrackBar.
    -   Setting opacity < 100% deactivates 'Toggle Window Transparency' mode.
-   **Settings Persistence:**
    -   The application automatically saves and loads your last used settings, including:
        -   Background color
        -   Full-screen state
        -   Window transparency state (on/off for F10 mode)
        -   Form Opacity level (for F9 control)
        -   Menu Bar Visibility (shown/hidden)
        -   **Selected Pen Color**
        -   **Selected Pen Size**
        -   Normal window size and location
    -   Settings are stored in an XML file: `%LOCALAPPDATA%/LousaInterativaCompany/LousaInterativaApp/settings.xml`.

## Technologies Used

-   **C# (C-Sharp):** The primary programming language.
-   **.NET 6:** The framework version used for the project.
-   **Windows Forms (WinForms):** The graphical user interface framework.

## How to Use (Development)

1.  Clone the repository.
2.  Open the `LousaInterativa.sln` solution file in Visual Studio (or a compatible .NET IDE).
3.  Build and run the project.

*(Further instructions on drawing tools and other features will be added as they are implemented.)*
