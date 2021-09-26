using C4.LibC4;

using System;
using System.Windows.Forms;

namespace C4.Gui
{
    public partial class FrmMain : Form
    {
        private const Int32 CELL_WIDTH = 100;

        private readonly Renderer _renderer = new();
        private readonly IGame _game;
        private Int32 _previousCell = -1;

        public FrmMain()
        {
            InitializeComponent();

            _game = new GameObjectFactory().GetGame();
            _game.New();
        }

        private void picSelectMove_MouseMove(Object sender, MouseEventArgs e)
        {
            Int32 column = GetColumn(e.X);
            if (column == _previousCell) return;

            _previousCell = column;
            picSelectMove.Invalidate();
        }

        private static Int32 GetColumn(Int32 posX)
        {
            return posX / CELL_WIDTH;
        }

        private void picSelectMove_Paint(Object sender, PaintEventArgs e)
        {
            _renderer.PrepareMove(e.Graphics, _previousCell, _game.Turn);
        }

        private void picBoard_Paint(Object sender, PaintEventArgs e)
        {
            _renderer.UpdateScreen(e.Graphics, _game);
        }

        private void picSelectMove_MouseUp(Object sender, MouseEventArgs e)
        {
            if (!_game.IsMoveValid(_previousCell)) return;
            _game.PlaceToken(_previousCell);
            picBoard.Invalidate();
            picSelectMove.Invalidate();
        }
    }
}
