using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MyWM
{
    public class WindowEventHook
    {
        // Event that fires when a new window is created
        public event Action<IntPtr> WindowCreated;

        // Delegate for window enumeration
        private delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        // Windows API functions for enumerating and getting window info
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public WindowEventHook()
        {
            // Call the EnumWindows function to get current windows
            EnumWindows(EnumWindowsCallback, IntPtr.Zero);
        }

        // Starts the process to hook new windows
        public void Start()
        {
            Console.WriteLine("Starting window hook...");
            EnumWindows(EnumWindowsCallback, IntPtr.Zero);
        }

        // Callback when a window is found
        private bool EnumWindowsCallback(IntPtr hwnd, IntPtr lParam)
        {
            // Check if the window is new (created)
            Console.WriteLine($"Checking window: {hwnd}");

            // If the window is new (no parent, no history), we treat it as a created window
            // A simple check for parent window can work, or use other checks like GetClassName
            IntPtr parentHwnd = GetParent(hwnd);
            if (parentHwnd == IntPtr.Zero)  // No parent window means it's likely a new top-level window
            {
                Console.WriteLine($"New window detected: {hwnd}");

                // Fire the WindowCreated event
                WindowCreated?.Invoke(hwnd);
            }

            return true;
        }
    }
}
