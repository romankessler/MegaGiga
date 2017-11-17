namespace _MG.CLI.Helper
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    internal static class ConsoleWindowHelper
    {
        private const int SW_MAXIMIZE = 3;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        public static void Maximize()
        {
            var currentProcess = Process.GetCurrentProcess();
            ShowWindow(currentProcess.MainWindowHandle, SW_MAXIMIZE);
        }
    }
}