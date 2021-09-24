using System;
using System.Collections.Generic;

namespace C4.LibC4
{
    public interface IGame
    {
        IBoard Board { get; }
        Token Turn { get; }
        Boolean IsRunning { get; }
        Token Winner { get; }
        public IList<WinningLine> WinningLines { get; }

        void New();
        void Quit();
        void PlaceToken(Int32 columnNumber);
        Boolean IsMoveValid(Int32 v);
    }
}
