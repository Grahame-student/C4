using System;
using System.Collections.Generic;
using System.Linq;

namespace C4.LibC4
{
    internal class Column : IColumn
    {
        private const Int32 ROW_BOTTOM = 0;

        public Column(UInt32 rowCount)
        {
            Rows = new List<Token>();
            for (var i = 0; i < rowCount; ++i)
            {
                Rows.Add(Token.None);
            }
        }

        public Int32 RowCount => Rows.Count;
        public IList<Token> Rows { get; }

        public Boolean IsFull
        {
            get
            {
                Int32 count = Rows.Count(token => token != Token.None);
                return count >= RowCount;
            }
        }

        public void AddToken(Token token)
        {
            Int32 row = ROW_BOTTOM;
            while (row < RowCount)
            {
                if (Rows[row] == Token.None) break;
                row++;
            }
            Rows[row] = token;
        }
    }
}
