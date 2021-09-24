using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace C4.Console.Platform
{
    /// <summary>
    /// This class encapsulates the Windows specific code
    /// </summary>
    [SupportedOSPlatform("windows")]
    internal static class WinMain
    {
        private const Int32 MF_BYCOMMAND = 0x00000000;
        private const Int32 SC_MINIMISE = 0xF020;
        private const Int32 SC_MAXIMISE = 0xF030;
        private const Int32 SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        private static extern Int32 DeleteMenu(IntPtr hMenu, Int32 nPosition, Int32 wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, Boolean bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        /// <summary>
        /// Create a fixed size console for the game state to be displayed upon
        /// </summary>
        /// <param name="width">Width of the console in columns</param>
        /// <param name="height">Height of the console in rows</param>
        internal static void CreateConsole(Int32 width, Int32 height)
        {
            System.Console.SetWindowSize(width, height);
            System.Console.SetBufferSize(width, height);
            System.Console.CursorVisible = false;

            _ = DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMISE, MF_BYCOMMAND);
            _ = DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMISE, MF_BYCOMMAND);
            _ = DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);
        }
    }
}
