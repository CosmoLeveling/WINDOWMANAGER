namespace MyWM
{
    public interface IManagedWindow
    {
        void MoveAndResize(Rect bounds); // Move and resize the window
        void Focus(); // (Optional) Focus the window
    }
}
