using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

public class WindowEventHook
{
    public delegate void WindowCreatedHandler(IntPtr hwnd);
    public event WindowCreatedHandler? WindowCreated;

    private WinEventDelegate? _procDelegate;
    private IntPtr _hook = IntPtr.Zero;

    private const uint EVENT_OBJECT_CREATE = 0x8000;
    private const uint WINEVENT_OUTOFCONTEXT = 0;

    private delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType,
        IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

    [DllImport("user32.dll")]
    private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc,
        WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

    [DllImport("user32.dll")]
    private static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern IntPtr GetParent(IntPtr hWnd);

    public void Start()
    {
        _procDelegate = new WinEventDelegate(WinEventCallback);
        _hook = SetWinEventHook(EVENT_OBJECT_CREATE, EVENT_OBJECT_CREATE, IntPtr.Zero,
            _procDelegate, 0, 0, WINEVENT_OUTOFCONTEXT);
    }

    public void Stop()
    {
        if (_hook != IntPtr.Zero)
        {
            // Optional: UnhookWinEvent(_hook); // Add P/Invoke if needed
            _hook = IntPtr.Zero;
        }
    }

    private void WinEventCallback(IntPtr hWinEventHook, uint eventType,
        IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
    {
        if (hwnd == IntPtr.Zero || GetParent(hwnd) != IntPtr.Zero)
            return; // Skip child windows

        if (!IsWindowVisible(hwnd))
            return; // Skip invisible windows

        int length = GetWindowTextLength(hwnd);
        if (length == 0)
            return; // Skip windows with no title (likely not useful)

        var builder = new StringBuilder(length + 1);
        GetWindowText(hwnd, builder, builder.Capacity);
        string title = builder.ToString();

        // Optional: log
        Console.WriteLine($"[Window Created] HWND={hwnd} Title='{title}'");

        WindowCreated?.Invoke(hwnd);
    }
}
