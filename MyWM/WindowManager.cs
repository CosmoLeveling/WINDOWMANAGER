using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MyWM;

namespace MyWM
{
    public class WindowManager
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

private static readonly IntPtr HWND_TOP = IntPtr.Zero;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOACTIVATE = 0x0010;
        public static void MoveWindowByTitle(string windowTitle, int x, int y, int width, int height)
        {
            IntPtr hWnd = FindWindow(null, windowTitle);  // Find the window by its title

            if (hWnd != IntPtr.Zero)
            {
                // Move the window to the new position (x, y)
                MoveWindow(hWnd, x, y, width, height, true);  // 800x600 is just an example size
            }
            else
            {
                Console.WriteLine("Window not found!");
            }
        }
        public static void SetPosition(string windowTitle, int x, int y)
        {
            IntPtr hWnd = FindWindow(null, windowTitle);

            if (hWnd != IntPtr.Zero)
            {
                SetWindowPos(hWnd, HWND_TOP, x, y, 0, 0, SWP_NOZORDER | SWP_NOACTIVATE);
            }
            else
            {
                Console.WriteLine("Window not found!");
            }
        }
        private readonly WindowEventHook _eventHook = new();
        private readonly List<IManagedWindow> _managedWindows = new();
        private readonly IWindowLayout _layout;
        
            public void OnWindowCreated(ManagedWindow hwnd)
    {
        // You can wrap this HWND using your ManagedWindow class
        _managedWindows.Add(hwnd);
        ApplyLayout();
    }

    private void ApplyLayout()
    {
        var monitorBounds = Screen.PrimaryScreen.Bounds;
        Rect rect = new Rect(0,0,monitorBounds.Width,monitorBounds.Height);
        _layout.ArrangeWindows(_managedWindows, rect);
    }
        // Constructor: Takes the layout to apply to windows
        public WindowManager(IWindowLayout layout)
        {
            _layout = layout ?? throw new ArgumentNullException(nameof(layout));
        }

        // Start the window manager (you can add any logic to initialize windows)
        public void Start()
        {
            Console.WriteLine("Window Manager started.");
        }

        // Stop the window manager (you can add any cleanup logic here)
        public void Stop()
        {
            Console.WriteLine("Window Manager stopped.");
        }

        // Apply layout to the list of windows
        public void ArrangeWindows(IEnumerable<IManagedWindow> windows, Rect monitorBounds)
        {
            // Use the layout to arrange the windows
            _layout.ArrangeWindows(windows, monitorBounds);
        }

        
    }
}
