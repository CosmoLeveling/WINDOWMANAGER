using MyWM;

public class ProportionalHorizontalLayout : IWindowLayout
{
    public string Name => "ProHorizontal Layout";
    public void ArrangeWindows(IEnumerable<IManagedWindow> windows, Rect monitorBounds)
    {
        var windowList = windows.ToList();
        int count = windowList.Count;
        if (count == 0) return;

        double totalWeight = count; // Equal weight, adjust if you want different proportions
        double unitWidth = monitorBounds.Width / totalWeight;
        double xOffset = monitorBounds.X;

        foreach (var window in windowList)
        {
            // Scale each window proportionally
            var rect = new Rect(xOffset, monitorBounds.Y, unitWidth, monitorBounds.Height);
            window.MoveAndResize(rect);
            xOffset += unitWidth;
        }
    }
}
