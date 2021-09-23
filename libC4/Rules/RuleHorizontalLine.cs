using System;
using System.Collections.Generic;

namespace C4.LibC4.Rules
{
    internal class RuleHorizontalLine : IGameRule
    {
        public IList<WinningLine> WinningLines { get; }

        public RuleHorizontalLine()
        {
            WinningLines = new List<WinningLine>();
        }

        public void FindLine(IBoard board)
        {
            WinningLines.Clear();
            for (var row = 0; row < board.Columns[0].RowCount; row++)
            {
                CheckRow(board, row);
            }
        }

        private void CheckRow(IBoard board, Int32 row)
        {
            var prev = Token.None;
            var cells = new List<Cell>();
            for (var col = 0; col < board.ColumnCount; col++)
            {
                Token token = board.Columns[col].Rows[row];
                if (token != prev)
                {
                    TryStoreLine(prev, cells);
                    cells = new List<Cell>();
                    prev = token;
                }
                cells.Add(new Cell(col, row));
            }
            TryStoreLine(prev, cells);
        }

        private void TryStoreLine(Token prev, IList<Cell> cells)
        {
            if (cells.Count < 4) return;
            if (prev != Token.None)
            {
                WinningLines.Add(new WinningLine(prev, cells));
            }
        }
    }
}
