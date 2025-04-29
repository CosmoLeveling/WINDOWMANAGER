namespace MyWM.Layouts
{
    public class HorizontalStackLayout : IWindowLayout
    {
        public string Name => "Horizontal Layout";
        public void ArrangeWindows(IEnumerable<IManagedWindow> windows, Rect monitorBounds)
        {
            double xOffset = 0;
            double yOffset = 0;

            foreach (var window in windows)
            {
                // Set each window to a new horizontal position
                window.MoveAndResize(new Rect(xOffset, yOffset, monitorBounds.Width / windows.Count(), monitorBounds.Height));
                
                // Move to the next horizontal position
                xOffset += monitorBounds.Width / windows.Count();
            }
        }
    }
}
