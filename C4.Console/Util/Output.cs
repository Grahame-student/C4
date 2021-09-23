using System;
using C4.LibC4;
using C4.LibC4.Rules;

namespace C4.Console.Util
{
    /// <summary>
    /// Class responsible for rendering game state
    /// </summary>
    internal class Output
    {
        private const Int32 TOKEN_HEIGHT = 3;
        private const Int32 TOKEN_WIDTH = 5;

        public void DrawBoard()
        {
            for (var row = 0; row < 6; row++)
            {
                for (var col = 0; col < 7; col++)
                {
                    DrawBoardCell(col, row);
                }
            }
        }

        public void DrawBoardCell(Int32 boardCol, Int32 boardRow)
        {
            Int32 consoleCol = boardCol * (TOKEN_WIDTH + 1) + 1;
            Int32 consoleRow = (5 - boardRow) * (TOKEN_HEIGHT + 1);

            Display.At(consoleCol, consoleRow + 0, "#######", ConsoleColor.Blue);
            Display.At(consoleCol, consoleRow + 1, "#     #", ConsoleColor.Blue);
            Display.At(consoleCol, consoleRow + 2, "#     #", ConsoleColor.Blue);
            Display.At(consoleCol, consoleRow + 3, "#     #", ConsoleColor.Blue);
            Display.At(consoleCol, consoleRow + 4, "#######", ConsoleColor.Blue);
        }

        public void ClearTokens()
        {
            for (var row = 0; row < 6; row++)
            {
                for (var col = 0; col < 7; col++)
                {
                    DrawToken(col, row, ConsoleColor.Black, false);
                }
            }
        }

        public void DrawTokens(IBoard board)
        {
            for (var row = 0; row < 6; row++)
            {
                for (var col = 0; col < 7; col++)
                {
                    if (board.Columns[col].Rows[row] == Token.None) continue;
                    ConsoleColor colour = GetPlayerColour(board.Columns[col].Rows[row]);
                    DrawToken(col, row, colour, false);
                }
            }
        }

        public void DrawWinningLines(IGame game)
        {
            foreach (WinningLine line in game.WinningLines)
            {
                foreach (Cell cell in line.TokenPositions)
                {
                    DrawToken(cell.Column, cell.Row, GetPlayerColour(line.Player), true);
                }
            }
        }

        public ConsoleColor GetPlayerColour(Token player)
        {
            return player == Token.Player1 ? ConsoleColor.Red : ConsoleColor.Yellow;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boardCol">Column number of the game board, 0 on the left, 6 on the right</param>
        /// <param name="boardRow">Row number of the game board, 0 on the bottom, 5 at the top</param>
        /// <param name="colour">Colour to use when drawing the token</param>
        /// <param name="winning">Indicates if token is part of a winning line</param>
        public void DrawToken(Int32 boardCol, Int32 boardRow, ConsoleColor colour, Boolean winning)
        {
            Int32 consoleCol = boardCol * (TOKEN_WIDTH + 1) + 2;
            Int32 consoleRow = (5 - boardRow) * (TOKEN_HEIGHT + 1) + 1;

            Display.At(consoleCol, consoleRow + 0, "/---\\", colour);
            Display.At(consoleCol, consoleRow + 1, winning ? "|XXX|" : "|   |", colour);
            Display.At(consoleCol, consoleRow + 2, "\\---/", colour);
        }

        public void ShowPlayerPrompt(IGame game)
        {
            // Clear any previous text there might be
            Display.At(1, 26, new String(' ', 70));

            // Display new message
            Display.At(1, 26, game.Turn == Token.Player1 ? "Player 1" : "Player 2", GetPlayerColour(game.Turn));
            Display.At(10, 26, "- Select select a column from 0 to 6");
        }

        public void ShowInvalidTurnPrompt()
        {
            Display.At(10, 25, "Invalid Move");
        }

        public void ShowWinnerPrompt(Token gameWinner)
        {
            // Clear any previous text there might be
            Display.At(1, 26, new String(' ', 70));

            // Display new message
            Display.At(1, 26, gameWinner == Token.Player1 ? "Player 1" : "Player 2", GetPlayerColour(gameWinner));
            Display.At(10, 26, "- Has won, congratulations!");
        }
    }
}
