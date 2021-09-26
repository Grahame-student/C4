using System;
using System.Drawing;
using C4.LibC4;

namespace C4.Gui
{
    internal class Colours
    {
        public Color Light { get; }
        public Color Dark { get; }
        public Color Outline { get; }

        public Colours(Token player)
        {
            Outline = Color.Black;
            Light = player == Token.Player1 ? Color.Red : Color.Yellow;
            Dark = player == Token.Player1 ? Color.DarkRed : Color.Gold;
        }

    }
}
