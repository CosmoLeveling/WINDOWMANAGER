using MyWM;

public class ManagedWindow : IManagedWindow
{
    public IntPtr Hwnd { get; }
    public Rect Bounds { get; private set; }

    public ManagedWindow(IntPtr hwnd)
    {
        Hwnd = hwnd;
        Bounds = new Rect(0, 0, 100, 100);
    }

    public void MoveAndResize(Rect bounds)
    {
        Bounds = bounds;
        WindowManager.MoveWindow(Hwnd, (int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height,false);
    }

    public void Focus()
    {
        Console.WriteLine($"Focusing window: {Hwnd}");
    }
}
