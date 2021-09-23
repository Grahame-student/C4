using System;

namespace C4.LibC4
{
    public interface IGameObjectFactory
    {
        IColumn GetColumn(UInt32 rowCount);
        IBoard GetBoard(UInt32 columnCount, UInt32 rowCount);
        IGame GetGame();
    }
}
