using System;
using System.Collections.Generic;
using C4.LibC4;
using C4.LibC4.Rules;
using NUnit.Framework;

namespace TestLibC4.Rules
{
    public class TestRuleVerticalLine
    {
        private const Int32 NO_LINES  = 0;
        private const Int32 ONE_LINE  = 1;
        private const Int32 TWO_LINES = 2;

        private const Int32 FIRST_COLUMN  = 0;
        private const Int32 SECOND_COLUMN = 1;
        private const Int32 SINGLE_COLUMN = 1;
        private const Int32 TWO_COLUMNS   = 2;
        private const Int32 FIRST_ROW     = 0;
        private const Int32 SOME_ROWS     = 7;
        private const Int32 SIX_ROWS      = 6;

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
            _rule = new RuleVerticalLine();

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(NO_LINES));
        }

        [Test]
        public void FindLines_SetsWinningLineTo1_WhenColumnHas4ConsecutiveTokensOfSameValueStartingFromFirstRow()
        {
            _rule = new RuleVerticalLine();
            IBoard board = _factory.GetBoard(SINGLE_COLUMN, SOME_ROWS);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(ONE_LINE));
        }

        [Test]
        public void FindLines_SetsPlayerToPlayer1_WhenColumnHas4ConsecutivePlayer1TokensStartingFromFirstRow()
        {
            _rule = new RuleVerticalLine();
            IBoard board = _factory.GetBoard(SINGLE_COLUMN, SOME_ROWS);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);

            _rule.FindLine(board);

            WinningLine line = _rule.WinningLines[0];

            Assert.That(line.Player, Is.EqualTo(Token.Player1));
        }

        [Test]
        public void FindLines_SetsTokenPositionsToListOfTokensInLine_WhenColumnHas4ConsecutivePlayer1TokensStartingFromFirstRow()
        {
            _rule = new RuleVerticalLine();
            IBoard board = _factory.GetBoard(SINGLE_COLUMN, SOME_ROWS);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);

            _rule.FindLine(board);

            WinningLine line = _rule.WinningLines[0];
            var expectedTokens = new List<Cell>
            {
                new(FIRST_COLUMN, FIRST_ROW),
                new(FIRST_COLUMN, FIRST_ROW + 1),
                new(FIRST_COLUMN, FIRST_ROW + 2),
                new(FIRST_COLUMN, FIRST_ROW + 3)
            };

            Assert.That(line.TokenPositions, Is.EqualTo(expectedTokens));
        }

        [Test]
        public void FindLines_SetsWinningLineTo0_WhenColumnHas3ConsecutiveTokensOfSameValueStartingFromFirstRow()
        {
            _rule = new RuleVerticalLine();
            IBoard board = _factory.GetBoard(SINGLE_COLUMN, SOME_ROWS);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(NO_LINES));
        }


        [Test]
        public void FindLines_SetsWinningLineTo1_WhenColumnMoreThan4ConsecutiveTokensOfSameValueStartingFromFirstRow()
        {
            _rule = new RuleVerticalLine();
            IBoard board = _factory.GetBoard(SINGLE_COLUMN, SOME_ROWS);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(ONE_LINE));
        }

        [Test]
        public void FindLines_SetsTokenPositionsToListOfTokensInLine_WhenColumnHasMoreThan4ConsecutivePlayer1TokensStartingFromFirstRow()
        {
            _rule = new RuleVerticalLine();
            IBoard board = _factory.GetBoard(SINGLE_COLUMN, SOME_ROWS);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);

            _rule.FindLine(board);

            WinningLine line = _rule.WinningLines[0];
            var expectedTokens = new List<Cell>
            {
                new(FIRST_COLUMN, FIRST_ROW),
                new(FIRST_COLUMN, FIRST_ROW + 1),
                new(FIRST_COLUMN, FIRST_ROW + 2),
                new(FIRST_COLUMN, FIRST_ROW + 3),
                new(FIRST_COLUMN, FIRST_ROW + 4)
            };

            Assert.That(line.TokenPositions, Is.EqualTo(expectedTokens));
        }

        [Test]
        public void FindLines_SetsWinningLineTo2_When2ColumnsHave4ConsecutiveTokensOfSameValueStartingFromFirstRow()
        {
            _rule = new RuleVerticalLine();
            IBoard board = _factory.GetBoard(TWO_COLUMNS, SOME_ROWS);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(SECOND_COLUMN, Token.Player1);
            board.AddToken(SECOND_COLUMN, Token.Player1);
            board.AddToken(SECOND_COLUMN, Token.Player1);
            board.AddToken(SECOND_COLUMN, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(TWO_LINES));
        }

        [Test]
        public void FindLines_SetsWinningLineTo1_WhenColumnHas4ConsecutiveTokensOfSameValueStartingAtEndOfColumn()
        {
            _rule = new RuleVerticalLine();
            IBoard board = _factory.GetBoard(SINGLE_COLUMN, SIX_ROWS);
            board.AddToken(FIRST_COLUMN, Token.Player2);
            board.AddToken(FIRST_COLUMN, Token.Player2);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);

            _rule.FindLine(board);

            Assert.That(_rule.WinningLines.Count, Is.EqualTo(ONE_LINE));
        }

        [Test]
        public void FindLines_ReturnsSameAnswer_WhenRunMultipleTimes()
        {
            _rule = new RuleVerticalLine();
            IBoard board = _factory.GetBoard(SINGLE_COLUMN, SIX_ROWS);
            board.AddToken(FIRST_COLUMN, Token.Player2);
            board.AddToken(FIRST_COLUMN, Token.Player2);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);
            board.AddToken(FIRST_COLUMN, Token.Player1);

            _rule.FindLine(board);
            Int32 firstRun = _rule.WinningLines.Count;
            _rule.FindLine(board);
            Int32 secondRun = _rule.WinningLines.Count;

            Assert.That(secondRun, Is.EqualTo(firstRun));
        }
    }
}
