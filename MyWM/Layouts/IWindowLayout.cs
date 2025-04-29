namespace MyWM
{
    public interface IWindowLayout
    {
        string Name { get; }
        void ArrangeWindows(IEnumerable<IManagedWindow> windows, Rect monitorBounds); // Arrange windows based on layout
    }
}
