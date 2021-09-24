using System;
using System.Collections.Generic;
using System.Linq;

namespace C4.LibC4.Rules
{
    internal class RuleDiagonalLine : IGameRule
    {
        public IList<WinningLine> WinningLines { get; }

        public RuleDiagonalLine()
        {
            WinningLines = new List<WinningLine>();
        }

        public void FindLine(IBoard board)
        {
            WinningLines.Clear();
            IEnumerable<IEnumerable<Cell>> diagonals = GetAllDiagonals(board);

            foreach (IEnumerable<Cell> diagonal in diagonals)
            {
                var prev = Token.None;
                var cells = new List<Cell>();
                foreach (Cell cell in diagonal)
                {
                    Token token = board.Columns[cell.Column].Rows[cell.Row];
                    if (token != prev)
                    {
                        TryStoreLine(prev, cells);
                        cells = new List<Cell>();
                        prev = token;
                    }
                    cells.Add(cell);
                }
                TryStoreLine(prev, cells);
            }
        }

        private void TryStoreLine(Token prev, IList<Cell> cells)
        {
            if (cells.Count < 4) return;
            if (prev != Token.None)
            {
                WinningLines.Add(new WinningLine(prev, cells));
            }
        }

        /// <summary>
        /// Return a list of all the unique diagonals with a length of at least 4
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private static IEnumerable<IList<Cell>> GetAllDiagonals(IBoard board)
        {
            // This could be lazy-loaded on first use and then stored for future rule checks
            var result =  new List<IList<Cell>>();

            for (var col = 0; col < board.ColumnCount; col++)
            {
                // Get bottom row diagonals
                result.AddRange(GetDiagonals(board, col, 0).Where(diagonal => diagonal.Count >= 4));

                // Get top row diagonals
                result.AddRange(GetDiagonals(board, col, board.Columns[0].RowCount - 1).Where(diagonal => diagonal.Count >= 4));
            }

            for (var row = 0; row < board.ColumnCount; row++)
            {
                // Get left edge diagonals
                result.AddRange(GetDiagonals(board, 0, row).Where(diagonal => diagonal.Count >= 4));

                // Get right edge diagonals
                result.AddRange(GetDiagonals(board, board.ColumnCount - 1, row).Where(diagonal => diagonal.Count >= 4));
            }

            // From the list of all diagonals return a list of the unique ones
            return result.Where((x, i) => !result.Skip(i + 1).Any(s => s.SequenceEqual(x))).OrderBy(list => list[0]);
        }

        /// <summary>
        /// Return the diagonals passing through the passed in col / row
        /// </summary>
        /// <param name="board"></param>
        /// <param name="startCol"></param>
        /// <param name="startRow"></param>
        /// <returns></returns>
        private static IEnumerable<IList<Cell>> GetDiagonals(IBoard board, Int32 startCol, Int32 startRow)
        {
            Int32 numCols = board.ColumnCount;
            Int32 numRows = board.Columns[0].RowCount;

            var diagonal = new List<Cell>();
            for (var row = 0; row < numRows; row++)
            {
                Int32 col = row - startRow + startCol;
                if (col >= 0 && col < numCols)
                {
                    diagonal.Add(new Cell(col, row));
                }
            }
            diagonal.Sort();
            yield return diagonal;

            diagonal = new List<Cell>();
            for (var row = 0; row < numRows; row++)
            {
                Int32 col = startRow + startCol - row;
                if (col >= 0 && col < numCols)
                {
                    diagonal.Add(new Cell(col, row));
                }
            }
            diagonal.Sort();
            yield return diagonal;
        }
    }
}
