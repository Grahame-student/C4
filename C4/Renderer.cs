using System;
using System.Drawing;
using C4.LibC4;

namespace C4.Gui
{
    internal class Renderer
    {
        private const Single CELL_SIZE = 100;
        private const Single TOKEN_SIZE = 80;
        private const Single TOKEN_OUTER = 75;
        private const Single TOKEN_INNER = 60;

        internal void PrepareMove(Graphics g, Int32 column, Token player)
        {
            DrawToken(g, column, 0, player);
        }

        public void UpdateScreen(Graphics g, IGame game)
        {
            // draw board
            g.Clear(Color.Blue);

            // draw tokens
            DrawTokens(g, game);
        }

        private static void DrawTokens(Graphics g, IGame game)
        {
            for (var col = 0; col < game.Board.ColumnCount; col++)
            {
                for (var row = 0; row < game.Board.Columns[0].RowCount; row++)
                {
                    if (game.Board.Columns[col].Rows[row] == Token.None)
                    {
                        DrawCircle(g, col, (5 - row), TOKEN_SIZE, Color.White);
                    }
                    else
                    {
                        DrawToken(g, col, (5 - row), game.Board.Columns[col].Rows[row]);
                    }
                }
            }
        }

        private static void DrawToken(Graphics g, Int32 column, Int32 row, Token player)
        {
            var colours = new Colours(player);
            DrawCircle(g, column, row, TOKEN_SIZE, colours.Outline);
            DrawCircle(g, column, row, TOKEN_OUTER, colours.Dark);
            DrawCircle(g, column, row, TOKEN_INNER, colours.Light);
        }

        private static void DrawCircle(Graphics g, Int32 column, Int32 row, Single diameter, Color colour)
        {
            Single top = (row * CELL_SIZE) + (CELL_SIZE - diameter) / 2;
            Single left = (column * CELL_SIZE) +  (CELL_SIZE - diameter) / 2;
            g.FillEllipse(new SolidBrush(colour), left, top, diameter, diameter);
        }
    }
}
