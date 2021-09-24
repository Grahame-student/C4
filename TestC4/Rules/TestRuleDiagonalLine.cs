using System;
using System.Collections.Generic;
using C4.LibC4;
using C4.LibC4.Rules;
using NUnit.Framework;

namespace TestLibC4.Rules
{
    public class TestRuleDiagonalLine
    {
        private const Int32 SOME_COLUMNS = 7;
        private const Int32 SOME_ROWS = 6;

        private const Int32 NO_LINES  = 0;
        private const Int32 ONE_LINE  = 1;
        private const Int32 TWO_LINES = 2;

        private const Int32 FIRST_COLUMN  = 0;
        private const Int32 FIRST_ROW = 0;

        private IGameObjectFactory _factory;
        private IGameRule _rule;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new GameObjectFactory();
        }

        [Test]
        public void Constructor_SetsWinningLineToEmptyList()
        {
            _rule = new RuleDiagonalLine();

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(NO_LINES));
        }

        /// <summary>
        /// ...X
        /// ..X.
        /// .X..
        /// X...
        /// </summary>
        [Test]
        public void FindLines_SetsWinningLineTo1_WhenDiagonalHas4ConsecutiveTokensOfSameValueStartingFromFirstRowAndFirstColumn()
        {
            _rule = new RuleDiagonalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player2);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(ONE_LINE));
        }

        [Test]
        public void FindLines_SetsPlayerToPlayer1_WhenDiagonalHas4ConsecutiveTokensOfSameValueStartingFromFirstRowAndFirstColumn()
        {
            _rule = new RuleDiagonalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player2);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);

            _rule.FindLine(board);

            WinningLine line = _rule.WinningLines[0];

            Assert.That(line.Player, Is.EqualTo(Token.Player1));
        }

        [Test]
        public void FindLines_SetsTokenPositionsToListOfTokensInLine_WhenDiagonalHas4ConsecutiveTokensOfSameValueStartingFromFirstRowAndFirstColumn()
        {
            _rule = new RuleDiagonalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player2);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);

            _rule.FindLine(board);

            WinningLine line = _rule.WinningLines[0];
            var expectedTokens = new List<Cell>
            {
                new(FIRST_COLUMN + 0, FIRST_ROW + 0),
                new(FIRST_COLUMN + 1, FIRST_ROW + 1),
                new(FIRST_COLUMN + 2, FIRST_ROW + 2),
                new(FIRST_COLUMN + 3, FIRST_ROW + 3)
            };

            Assert.That(line.TokenPositions, Is.EqualTo(expectedTokens));
        }

        /// <summary>
        /// ....
        /// ..X.
        /// .X..
        /// X...
        /// </summary>
        [Test]
        public void FindLines_SetsWinningLineTo0_WhenDiagonalHas3ConsecutiveTokensOfSameValueStartingFromFirstRowAndFirstColumn()
        {
            _rule = new RuleDiagonalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player2);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);

            _rule.FindLine(board);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(NO_LINES));
        }


        /// <summary>
        /// ....X
        /// ...X.
        /// ..X..
        /// .X...
        /// Xt.t
        ///
        /// X are the intended winning line
        /// t are to block p2 from registering a winning line
        /// </summary>
        [Test]
        public void FindLines_SetsWinningLineTo1_WhenDiagonalHasMoreThan4ConsecutiveTokensOfSameValueStartingFromFirstRowAndFirstColumn()
        {
            _rule = new RuleDiagonalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1); // We don't want P2 to have a winning line
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1); // We don't want P2 to have a winning line
            board.AddToken(FIRST_COLUMN + 4, Token.Player2);
            board.AddToken(FIRST_COLUMN + 4, Token.Player2);
            board.AddToken(FIRST_COLUMN + 4, Token.Player2);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(ONE_LINE));
        }

        [Test]
        public void FindLines_SetsTokenPositionsToListOfTokensInLine_WhenDiagonalHasMoreThan4ConsecutiveTokensOfSameValueStartingFromFirstRowAndFirstColumn()
        {
            _rule = new RuleDiagonalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1); // We don't want P2 to have a winning line
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1); // We don't want P2 to have a winning line
            board.AddToken(FIRST_COLUMN + 4, Token.Player2);
            board.AddToken(FIRST_COLUMN + 4, Token.Player2);
            board.AddToken(FIRST_COLUMN + 4, Token.Player2);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1);

            _rule.FindLine(board);

            WinningLine line = _rule.WinningLines[0];
            var expectedTokens = new List<Cell>
            {
                new(FIRST_COLUMN + 0, FIRST_ROW + 0),
                new(FIRST_COLUMN + 1, FIRST_ROW + 1),
                new(FIRST_COLUMN + 2, FIRST_ROW + 2),
                new(FIRST_COLUMN + 3, FIRST_ROW + 3),
                new(FIRST_COLUMN + 4, FIRST_ROW + 4)
            };

            Assert.That(line.TokenPositions, Is.EqualTo(expectedTokens));
        }

        /// ...XX
        /// ..XX.
        /// .XX..
        /// XX...
        [Test]
        public void FindLines_SetsWinningLineTo2_When2DiagonalsHave4ConsecutiveTokensOfSameValueStartingFromFirstRowAndFirstColumn()
        {
            _rule = new RuleDiagonalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player2);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);
            board.AddToken(FIRST_COLUMN + 4, Token.Player2);
            board.AddToken(FIRST_COLUMN + 4, Token.Player2);
            board.AddToken(FIRST_COLUMN + 4, Token.Player2);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(TWO_LINES));
        }


        /// ......X
        /// .....X.
        /// ....X..
        /// ...X...
        [Test]
        public void FindLines_SetsWinningLineTo1_WhenColumnHas4ConsecutiveTokensOfSameValueStartingAtEndOfColumn()
        {
            _rule = new RuleDiagonalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);
            board.AddToken(FIRST_COLUMN + 4, Token.Player2);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1);
            board.AddToken(FIRST_COLUMN + 5, Token.Player2);
            board.AddToken(FIRST_COLUMN + 5, Token.Player2);
            board.AddToken(FIRST_COLUMN + 5, Token.Player1);
            board.AddToken(FIRST_COLUMN + 6, Token.Player2);
            board.AddToken(FIRST_COLUMN + 6, Token.Player2);
            board.AddToken(FIRST_COLUMN + 6, Token.Player2);
            board.AddToken(FIRST_COLUMN + 6, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(ONE_LINE));
        }

        [Test]
        public void FindLines_ReturnsSameAnswer_WhenRunMultipleTimes()
        {
            _rule = new RuleDiagonalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);
            board.AddToken(FIRST_COLUMN + 4, Token.Player2);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1);
            board.AddToken(FIRST_COLUMN + 5, Token.Player2);
            board.AddToken(FIRST_COLUMN + 5, Token.Player2);
            board.AddToken(FIRST_COLUMN + 5, Token.Player1);
            board.AddToken(FIRST_COLUMN + 6, Token.Player2);
            board.AddToken(FIRST_COLUMN + 6, Token.Player2);
            board.AddToken(FIRST_COLUMN + 6, Token.Player2);
            board.AddToken(FIRST_COLUMN + 6, Token.Player1);

            _rule.FindLine(board);
            Int32 firstRun = _rule.WinningLines.Count;
            _rule.FindLine(board);
            Int32 secondRun = _rule.WinningLines.Count;

            Assert.That(secondRun, Is.EqualTo(firstRun));
        }




        /// <summary>
        /// X...
        /// .X..
        /// ..X.
        /// ...X
        /// </summary>
        [Test]
        public void FindLines_SetsWinningLineTo1_WhenDiagonalHas4ConsecutiveTokensOfSameValueStartingFromLastRowAndFirstColumn()
        {
            _rule = new RuleDiagonalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);
            board.AddToken(FIRST_COLUMN + 0, Token.Player2);
            board.AddToken(FIRST_COLUMN + 0, Token.Player2);
            board.AddToken(FIRST_COLUMN + 0, Token.Player2);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player2);
            board.AddToken(FIRST_COLUMN + 1, Token.Player2);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player2);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(ONE_LINE));
        }
    }
}
