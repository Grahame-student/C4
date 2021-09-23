using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using C4.LibC4.Rules;

[assembly: InternalsVisibleTo("TestLibC4")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace C4.LibC4
{
    public class Game : IGame
    {
        private const Int32 DEFAULT_COLUMNS = 7;
        private const Int32 DEFAULT_ROWS = 6;

        private readonly IGameObjectFactory _factory;

        private readonly IList<IGameRule> _rules = new List<IGameRule>
        {
            new RuleVerticalLine(),
            new RuleHorizontalLine()
        };

        public IBoard Board { get; private set; }
        public Token Turn { get; private set; }
        public Boolean IsRunning { get; private set; }

        public Token Winner
        {
            get;
            private set;
        }

        public IList<WinningLine> WinningLines { get; private set; }

        public Game(IGameObjectFactory gameObjectFactory)
        {
            _factory = gameObjectFactory;
            IsRunning = true;
        }

        public void New()
        {
            Board = _factory.GetBoard(DEFAULT_COLUMNS, DEFAULT_ROWS);
            Turn = Token.Player1;

            // TODO: add test
            Winner = Token.None;
            WinningLines = new List<WinningLine>();
        }

        public void Quit()
        {
            IsRunning = false;
        }

        public void PlaceToken(Int32 columnNumber)
        {
            Board.AddToken(columnNumber, Turn);
            Turn = Turn == Token.Player1 ? Token.Player2 : Token.Player1;

            CheckRules();
        }

        private void CheckRules()
        {
            WinningLines.Clear();
            foreach (IGameRule gameRule in _rules)
            {
                // FIXME: this guard is to make a quick PoC console application playable
                if (Board.Columns == null) return;

                gameRule.FindLine(Board);
                foreach (WinningLine line in gameRule.WinningLines)
                {
                    WinningLines.Add(line);
                }
            }

            if (WinningLines.Count > 0)
            {
                Winner = WinningLines[0].Player;
            }
        }

        public Boolean IsMoveValid(Int32 column)
        {
            return Board.IsValidMove(column);
        }
    }
}
