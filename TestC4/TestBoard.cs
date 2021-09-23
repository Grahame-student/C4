using System;
using System.Linq;
using C4.LibC4;

using Moq;

using NUnit.Framework;

namespace TestLibC4
{
    public class TestBoard
    {
        private const UInt32 SOME_COLUMNS = 7;
        private const UInt32 SOME_ROWS = 6;

        private const Int32 SOME_COLUMN = 2;

        private IGameObjectFactory _factory;
        private IBoard _board;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new GameObjectFactory();
        }

        [Test]
        public void Constructor_SetsColumnCount_ToPassedInValue()
        {
            _board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);

            Assert.That(_board.ColumnCount, Is.EqualTo(SOME_COLUMNS));
        }

        [Test]
        public void Constructor_Initialises_Columns()
        {
            _board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);

            // Check that all columns are not null
            Boolean valid = _board.Columns.Aggregate(true, (current, column) => current & column != null);

            Assert.That(valid, Is.True);
        }

        [Test]
        public void Constructor_PassesRowCount_ToColumns()
        {
            _board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);

            Boolean valid = _board.Columns.Aggregate(true, (current, column) => current & column.RowCount == SOME_ROWS);

            Assert.That(valid, Is.True);
        }

        [Test]
        public void AddToken_AddsPassedInToken_ToPassedInColumn()
        {
            _board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);
            var mockColumn = new Mock<IColumn>();
            _board.Columns[SOME_COLUMN] = mockColumn.Object;

            _board.AddToken(SOME_COLUMN, Token.Player2);

            mockColumn.Verify(x => x.AddToken(Token.Player2));
        }

        [Test]
        public void IsValidMove_ReturnsTrue_When_PassedInColumnNotFull()
        {
            _board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);

            var mockColumn = new Mock<IColumn>();
            mockColumn.SetupGet(x => x.IsFull).Returns(false);
            _board.Columns[SOME_COLUMN] = mockColumn.Object;

            Assert.That(_board.IsValidMove(SOME_COLUMN), Is.True);
        }

        [Test]
        public void IsValidMove_ReturnsFalse_When_PassedInColumnFull()
        {
            _board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);

            var mockColumn = new Mock<IColumn>();
            mockColumn.SetupGet(x => x.IsFull).Returns(true);
            _board.Columns[SOME_COLUMN] = mockColumn.Object;

            Assert.That(_board.IsValidMove(SOME_COLUMN), Is.False);
        }
    }
}
