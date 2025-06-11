# Lousa Interativa

Lousa Interativa (Interactive Whiteboard) is a simple Windows Forms application designed to provide a basic digital whiteboard experience. It allows users to change the background color, including transparency, and toggle a full-screen mode for an immersive experience.

## Features

-   **Full-Screen Mode:**
    -   Toggle full-screen display by pressing the **F11** key.
    -   Alternatively, use the **View > Full Screen** menu option.
    -   Provides an immersive, borderless window experience.
-   **Customizable Background Color:**
    -   Change the application's background color via the **Tools > Change Background Color** menu option.
    -   A color dialog allows selection of any color, including alpha (transparency) values for a semi-transparent background.
-   **Toggle Window Transparency:**
    -   Activate/deactivate full window transparency, making the client area see-through and removing borders.
    -   Toggle with the **F10** key or via the **View > Toggle Transparency** menu option.
    -   When transparency is deactivated, the previously chosen background color and form style are restored.
-   **Settings Persistence:**
    -   The application automatically saves your last used settings:
        -   Background color
        -   Full-screen state
        -   Window transparency state
        -   Normal window size and location
    -   These settings are loaded when the application starts, restoring your previous session's look and feel. Settings are stored in an XML file in your local application data folder (`%LOCALAPPDATA%/LousaInterativaCompany/LousaInterativaApp/settings.xml`).

## Technologies Used

-   **C# (C-Sharp):** The primary programming language.
-   **.NET 6:** The framework version used for the project.
-   **Windows Forms (WinForms):** The graphical user interface framework.

## How to Use (Development)

1.  Clone the repository.
2.  Open the `LousaInterativa.sln` solution file in Visual Studio (or a compatible .NET IDE).
3.  Build and run the project.

*(Further instructions on drawing tools and other features will be added as they are implemented.)*
