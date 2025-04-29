using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MyWM;
using MyWM.Layouts;

class Program
{
    
    static void Main()
    {
        WindowEventHook windowEventHook = new WindowEventHook();
        // Create the VerticalStackLayout layout
        var layout = new ProportionalHorizontalLayout();

        // Create the WindowManager and pass the layout
        var wm = new WindowManager(layout);

        // Create a list of windows (implement IManagedWindow)
        var windows = new List<IManagedWindow>
        {
        };
        // Define the monitor bounds (Rect) where windows will be arranged
        var monitorBounds = Screen.PrimaryScreen.Bounds; // Example: 800x600 screen
        Rect rect = new Rect(0,0, monitorBounds.Width,monitorBounds.Height);
        // Use the WindowManager to arrange the windows
        wm.ArrangeWindows(windows, rect);

        // Start the WindowManager
        wm.Start();

        Console.WriteLine("Window Manager running. Press any key to exit.");
        Console.ReadKey();

        // Stop the WindowManager
        wm.Stop();
    }
}
