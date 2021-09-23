using System;
using System.Collections.Generic;

namespace C4.LibC4.Rules
{
    internal class RuleVerticalLine : IGameRule
    {
        public IList<WinningLine> WinningLines { get; }

        public RuleVerticalLine()
        {
            WinningLines = new List<WinningLine>();
        }

        public void FindLine(IBoard board)
        {
            WinningLines.Clear();
            for (var col = 0; col < board.ColumnCount; col++)
            {
                CheckColumn(board, col);
            }
        }

        private void CheckColumn(IBoard board, Int32 column)
        {
            var prev = Token.None;
            var cells = new List<Cell>();
            for (var row = 0; row < board.Columns[0].RowCount; row++)
            {
                Token token = board.Columns[column].Rows[row];
                if (token != prev)
                {
                    TryStoreLine(prev, cells);
                    cells = new List<Cell>();
                    prev = token;
                }
                cells.Add(new Cell(column, row));
            }
            TryStoreLine(prev, cells);
        }

        private void TryStoreLine(Token prev, List<Cell> cells)
        {
            if (cells.Count < 4) return;
            if (prev != Token.None)
            {
                WinningLines.Add(new WinningLine(prev, cells));
            }
        }
    }
}
