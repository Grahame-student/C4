using System;
using System.Collections.Generic;

namespace C4.LibC4
{
    public interface IColumn
    {
        Int32 RowCount { get; }
        IList<Token> Rows { get; }
        Boolean IsFull { get; }

        void AddToken(Token token);
    }
}
