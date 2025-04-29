using System;
using System.Runtime.InteropServices;

namespace MyWM
{
    public class ManagedWindow : IManagedWindow
    {
        public IntPtr Hwnd { get; }
        public Rect Bounds { get; private set; }

        public ManagedWindow(IntPtr hwnd)
        {
            Hwnd = hwnd;
            Bounds = new Rect(0, 0, 100, 100); // Default size
        }

        public void MoveAndResize(Rect bounds)
        {
            Bounds = bounds;
            Console.WriteLine($"Moving HWND {Hwnd} to ({bounds.X}, {bounds.Y}, {bounds.Width}, {bounds.Height})");

            MoveWindow(Hwnd, (int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height, true);
        }

        public void Focus()
        {
            Console.WriteLine($"Focusing window: {Hwnd}");
            SetForegroundWindow(Hwnd);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
