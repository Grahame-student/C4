using System;

namespace C4.Console.Util
{
    internal class Input
    {
        internal ConsoleKeyInfo GetInput()
        {
            return System.Console.ReadKey(true);
        }
    }
}
