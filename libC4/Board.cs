using System;
using System.Collections.Generic;

namespace C4.LibC4
{
    internal class Board : IBoard
    {
        internal Board(IGameObjectFactory gameObjectFactory, UInt32 columnCount, UInt32 rowCount)
        {
            Columns = new List<IColumn>();
            for (var i = 0; i < columnCount; ++i)
            {
                Columns.Add(gameObjectFactory.GetColumn(rowCount));
            }
        }

        public Int32 ColumnCount => Columns.Count;
        public IList<IColumn> Columns { get; internal set; }

        public void AddToken(Int32 columnNumber, Token token)
        {
            Columns[columnNumber].AddToken(token);
        }

        public Boolean IsValidMove(Int32 columnNumber)
        {
            return !Columns[columnNumber].IsFull;
        }
    }
}
