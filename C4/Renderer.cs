using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using C4.LibC4;

namespace C4.Gui
{
    internal class Renderer
    {
        internal void DrawBoard(Graphics g)
        {
            Image image = GetImage("board.png");

            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
        }

        private Image GetImage(String imageName)
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            Stream resource = assembly.GetManifestResourceStream($"C4.Gui.{imageName}");
            using Image tmpImage = new Bitmap(resource);
            Image image = new Bitmap(tmpImage.Width, tmpImage.Height, PixelFormat.Format32bppArgb);
            using Graphics g = Graphics.FromImage(image);
            g.DrawImage(tmpImage, new Rectangle(0, 0, image.Width, image.Height));

            return image;
        }

        internal void PrepareMove(PaintEventArgs e, Int32 column, Token player)
        {
            Image token = GetToken("token.png", new Colours(player));
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawImage(token, new Rectangle(column * 100, 0, token.Width, token.Height));
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
    }
}
