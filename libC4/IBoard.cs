using System;
using System.Collections.Generic;

namespace C4.LibC4
{
    public interface IBoard
    {
        Int32 ColumnCount { get; }

        IList<IColumn> Columns { get; }

        void AddToken(Int32 columnNumber, Token token);

        public Boolean IsValidMove(Int32 columnNumber);
    }
}
