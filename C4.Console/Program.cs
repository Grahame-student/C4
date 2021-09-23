using C4.Console.Platform;
using C4.Console.Util;
using C4.LibC4;

using System;
using System.Linq;

namespace C4.Console
{
    internal class Program
    {
        private static readonly ConsoleKey[] ValidInput = {
            ConsoleKey.D0,
            ConsoleKey.D1,
            ConsoleKey.D2,
            ConsoleKey.D3,
            ConsoleKey.D4,
            ConsoleKey.D5,
            ConsoleKey.D6,
            ConsoleKey.NumPad0,
            ConsoleKey.NumPad1,
            ConsoleKey.NumPad2,
            ConsoleKey.NumPad3,
            ConsoleKey.NumPad4,
            ConsoleKey.NumPad5,
            ConsoleKey.NumPad6
        };

        private static void Main()
        {
            if (OperatingSystem.IsWindows())
            {
                WinMain.CreateConsole(80, 28);
            }
            else
            {
                System.Console.WriteLine("Operating System not supported at this time");
                Environment.Exit(-1);
            }


            var input = new Input();
            var output = new Output();
            IGame game = new GameObjectFactory().GetGame();
            game.New();
            output.DrawBoard();
            while (game.IsRunning)
            {
                // Display game state
                output.DrawTokens(game.Board);
                output.ShowPlayerPrompt(game);

                ConsoleKeyInfo pressed = input.GetInput();
                var column = (Int32)Char.GetNumericValue(pressed.KeyChar);
                if (pressed.Key == ConsoleKey.Q)
                {
                    game.Quit();
                    // wait for keyboard input before continuing
                    _ = input.GetInput();
                }
                else if (ValidInput.Contains(pressed.Key) && game.IsMoveValid(column))
                {
                    // Update game state
                    game.PlaceToken(column);
                }
                else
                {
                    output.ShowInvalidTurnPrompt();
                }

                if (game.Winner == Token.None) continue;
                output.ShowWinnerPrompt(game.Winner);
                output.DrawWinningLines(game);

                // wait for keyboard input before continuing
                _ = input.GetInput();
                game.New();
                output.ClearTokens();
            }

            Display.At(0, 24, "Exiting");
            Environment.Exit(0);
        }
    }
}
