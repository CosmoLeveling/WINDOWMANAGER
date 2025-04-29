using System;
using MyWM.Layouts; // Import the layouts

namespace MyWM
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an event hook and window manager with a layout
            var eventHook = new WindowEventHook();
            var manager = new WindowManager(new HorizontalStackLayout()); // Use HorizontalStackLayout initially

            // Event handler when a new window is created
            eventHook.WindowCreated += hwnd =>
            {
                Console.WriteLine($"New window detected: {hwnd}");

                // Wrap the HWND into ManagedWindow and add it to the manager
                var managedWindow = new ManagedWindow(hwnd);
                manager.OnWindowCreated(managedWindow);
            };

            // Start listening for window creation events
            eventHook.Start();

            // Optionally, change layout after some time or based on user input
            // manager.SetLayout(new VerticalStackLayout()); // Uncomment this to switch to vertical layout

            Console.WriteLine("Window manager running. Press Enter to exit.");
            Console.ReadLine(); // Keeps app alive
        }
    }
}
