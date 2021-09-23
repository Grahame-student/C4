using System;
using System.Collections.Generic;
using C4.LibC4;
using C4.LibC4.Rules;
using NUnit.Framework;

namespace TestLibC4.Rules
{
    public class TestRuleHorizontalLine
    {
        private const Int32 NO_LINES  = 0;
        private const Int32 ONE_LINE  = 1;
        private const Int32 TWO_LINES = 2;

        private const Int32 SOME_COLUMNS  = 8;
        private const Int32 SEVEN_COLUMNS = 7;
        private const Int32 SINGLE_ROW    = 1;
        private const Int32 TWO_ROWS      = 2;

        private const Int32 FIRST_COLUMN = 0;
        private const Int32 FIRST_ROW    = 0;

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
            _rule = new RuleHorizontalLine();

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(NO_LINES));
        }

        [Test]
        public void FindLines_SetsWinningLineTo1_WhenRowHas4ConsecutiveTokensOfSameValueStartingFromFirstColumn()
        {
            _rule = new RuleHorizontalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SINGLE_ROW);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(ONE_LINE));
        }

        [Test]
        public void FindLines_SetsPlayerToPlayer1_WhenRowHas4ConsecutivePlayer1TokensStartingFromFirstColumn()
        {
            _rule = new RuleHorizontalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SINGLE_ROW);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);

            _rule.FindLine(board);
            WinningLine line = _rule.WinningLines[0];

            Assert.That(line.Player, Is.EqualTo(Token.Player1));
        }

        [Test]
        public void FindLines_SetsTokenPositionsToListOfTokensInLine_WhenRowHas4ConsecutivePlayer1TokensStartingFromFirstColumn()
        {
            _rule = new RuleHorizontalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SINGLE_ROW);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);

            _rule.FindLine(board);
            WinningLine line = _rule.WinningLines[0];
            var expectedTokens = new List<Cell>
            {
                new(FIRST_COLUMN + 0, FIRST_ROW),
                new(FIRST_COLUMN + 1, FIRST_ROW),
                new(FIRST_COLUMN + 2, FIRST_ROW),
                new(FIRST_COLUMN + 3, FIRST_ROW)
            };

            Assert.That(line.TokenPositions, Is.EqualTo(expectedTokens));
        }

        [Test]
        public void FindLines_SetsWinningLineTo0_WhenRowHas3ConsecutiveTokensOfSameValueStartingFromFirstColumn()
        {
            _rule = new RuleHorizontalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SINGLE_ROW);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(NO_LINES));
        }

        [Test]
        public void FindLines_SetsWinningLineTo1_WhenRowHasMoreThan4ConsecutiveTokensOfSameValueStartingFromFirstColumn()
        {
            _rule = new RuleHorizontalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SINGLE_ROW);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(ONE_LINE));
        }

        [Test]
        public void FindLines_SetsTokenPositionsToListOfTokensInLine_WhenRowHasMoreThan4ConsecutivePlayer1TokensStartingFromFirstColumn()
        {
            _rule = new RuleHorizontalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, SINGLE_ROW);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1);

            _rule.FindLine(board);

            WinningLine line = _rule.WinningLines[0];
            var expectedTokens = new List<Cell>
            {
                new(FIRST_COLUMN + 0, FIRST_ROW),
                new(FIRST_COLUMN + 1, FIRST_ROW),
                new(FIRST_COLUMN + 2, FIRST_ROW),
                new(FIRST_COLUMN + 3, FIRST_ROW),
                new(FIRST_COLUMN + 4, FIRST_ROW)
            };

            Assert.That(line.TokenPositions, Is.EqualTo(expectedTokens));
        }

        [Test]
        public void FindLines_SetsWinningLineTo2_When2RowsHave4ConsecutiveTokensOfSameValueStartingFromFirstCol()
        {
            _rule = new RuleHorizontalLine();
            IBoard board = _factory.GetBoard(SOME_COLUMNS, TWO_ROWS);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1);
            board.AddToken(FIRST_COLUMN + 0, Token.Player1);
            board.AddToken(FIRST_COLUMN + 1, Token.Player1);
            board.AddToken(FIRST_COLUMN + 2, Token.Player1);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(TWO_LINES));
        }

        [Test]
        public void FindLines_SetsWinningLineTo1_WhenRowHas4ConsecutiveTokensOfSameValueAtEndOfRow()
        {
            _rule = new RuleHorizontalLine();
            IBoard board = _factory.GetBoard(SEVEN_COLUMNS, SINGLE_ROW);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1);
            board.AddToken(FIRST_COLUMN + 5, Token.Player1);
            board.AddToken(FIRST_COLUMN + 6, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(ONE_LINE));
        }

        [Test]
        public void FindLines_ReturnsSameAnswer_WhenRunMultipleTimes()
        {
            _rule = new RuleHorizontalLine();
            IBoard board = _factory.GetBoard(SEVEN_COLUMNS, SINGLE_ROW);
            board.AddToken(FIRST_COLUMN + 3, Token.Player1);
            board.AddToken(FIRST_COLUMN + 4, Token.Player1);
            board.AddToken(FIRST_COLUMN + 5, Token.Player1);
            board.AddToken(FIRST_COLUMN + 6, Token.Player1);

            _rule.FindLine(board);
            Int32 firstRun = _rule.WinningLines.Count;
            _rule.FindLine(board);
            Int32 secondRun = _rule.WinningLines.Count;

            Assert.That(secondRun, Is.EqualTo(firstRun));
        }
    }
}
