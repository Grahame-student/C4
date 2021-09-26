using C4.LibC4;

using System;
using System.Windows.Forms;

namespace C4.Gui
{
    public partial class FrmMain : Form
    {
        private const Int32 CELL_WIDTH = 100;

        private readonly Renderer _renderer = new Renderer();
        private Int32 _previousCell = -1;

        public FrmMain()
        {
            InitializeComponent();

        }

        private void picBoard_Paint(object sender, PaintEventArgs e)
        {
            _renderer.DrawBoard(e.Graphics);
        }

        private void picSelectMove_MouseMove(object sender, MouseEventArgs e)
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

        private void picSelectMove_Paint(object sender, PaintEventArgs e)
        {
            _renderer.PrepareMove(e, _previousCell, Token.Player1);
        }
    }
}
