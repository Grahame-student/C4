using System.Collections.Generic;

namespace C4.LibC4.Rules
{
    internal interface IGameRule
    {
        IList<WinningLine> WinningLines { get; }
        void FindLine(IBoard board);
    }
}
