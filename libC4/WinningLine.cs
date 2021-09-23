using System.Collections.Generic;
using C4.LibC4.Rules;

namespace C4.LibC4
{
    public class WinningLine
    {
        public Token Player { get; }
        public IList<Cell> TokenPositions { get; }

        public WinningLine(Token player, IList<Cell> cells)
        {
            Player = player;
            TokenPositions = cells;
        }
    }
}
