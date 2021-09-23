using System;

namespace C4.LibC4
{
    public class GameObjectFactory : IGameObjectFactory
    {
        public IColumn GetColumn(UInt32 rowCount)
        {
            return new Column(rowCount);
        }

        public IBoard GetBoard(UInt32 columnCount, UInt32 rowCount)
        {
            return new Board(this, columnCount, rowCount);
        }

        public IGame GetGame()
        {
            return new Game(this);
        }
    }
}
