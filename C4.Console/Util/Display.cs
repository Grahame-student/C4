using System;

namespace C4.Console.Util
{
    internal static class Display
    {
        internal static void At(Int32 left, Int32 top, String text, ConsoleColor background, ConsoleColor foreground)
        {
            System.Console.BackgroundColor = background;
            System.Console.ForegroundColor = foreground;
            At(left, top, text);
            System.Console.ResetColor();
        }

        internal static void At(Int32 left, Int32 top, String text, ConsoleColor foreground)
        {
            System.Console.ForegroundColor = foreground;
            At(left, top, text);
            System.Console.ResetColor();
        }

        internal static void At(Int32 left, Int32 top, String text)
        {
            System.Console.SetCursorPosition(left, top);
            System.Console.WriteLine(text);
        }
    }
}
