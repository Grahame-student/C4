using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using C4.LibC4;

namespace C4.Gui
{
    internal class Renderer
    {
        private Image GetImage(String imageName)
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            Stream resource = assembly.GetManifestResourceStream($"C4.Gui.{imageName}");
            if (resource == null) throw new InvalidOperationException();
            using Image tmpImage = new Bitmap(resource);
            Image image = new Bitmap(tmpImage.Width, tmpImage.Height, PixelFormat.Format32bppArgb);
            using Graphics g = Graphics.FromImage(image);
            g.DrawImage(tmpImage, new Rectangle(0, 0, image.Width, image.Height));

            return image;
        }

        internal void PrepareMove(Graphics g, Int32 column, Token player)
        {
            Image token = GetToken("token.png", new Colours(player));
            g.Clear(Color.White);
            g.DrawImage(token, new Rectangle(column * 100, 0, token.Width, token.Height));
        }

        private Image GetToken(String imageName, Colours colours)
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            Stream resource = assembly.GetManifestResourceStream($"C4.Gui.{imageName}");
            using Image tmpImage = new Bitmap(resource);

            var colourMap = new ColorMap[2];
            colourMap[0] = new ColorMap
            {
                OldColor = Color.FromArgb(255, 150, 150, 150),
                NewColor = colours.Dark
            };
            colourMap[1] = new ColorMap
            {
                OldColor = Color.FromArgb(255, 200, 200, 200),
                NewColor = colours.Light
            };
            var attr = new ImageAttributes();
            attr.SetRemapTable(colourMap);

            Image image = new Bitmap(tmpImage.Width, tmpImage.Height, PixelFormat.Format32bppArgb);
            using Graphics g = Graphics.FromImage(image);
            g.DrawImage(tmpImage, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attr);

            return image;
        }

        public Image DrawTokens(IGame game)
        {
            Image background = new Bitmap(700, 600);
            using Graphics g = Graphics.FromImage(background);
            g.Clear(Color.White);

            for (var col = 0; col < game.Board.ColumnCount; col++)
            {
                for (var row = 0; row < game.Board.Columns[0].RowCount; row++)
                {
                    if (game.Board.Columns[col].Rows[row] == Token.None) continue;
                    PlaceToken(g, col, row, game.Board.Columns[col].Rows[row]);
                }
            }

            return background;
        }

        private void PlaceToken(Graphics g, Int32 col, Int32 row, Token player)
        {
            Int32 boardRow = 5 - row;
            Image token = GetToken("token.png", new Colours(player));
            g.DrawImage(token, new Rectangle(col * 100, boardRow * 100, token.Width, token.Height));
        }

        public void UpdateScreen(Graphics g, IGame game)
        {
            Image background = DrawTokens(game);
            Image board = GetImage("board.png");

            g.CompositingMode = CompositingMode.SourceOver;
            g.DrawImage(background, 0, 0, background.Width, background.Height);
            g.DrawImage(board, 0, 0, board.Width, board.Height);
        }
    }
}
