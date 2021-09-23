using System;
using System.Linq;
using C4.LibC4;
using NUnit.Framework;

namespace TestLibC4
{
    internal class TestColumn
    {
        private const UInt32 SOME_HEIGHT = 5;

        private const Int32 ROW_BOTTOM = 0;
        private const Int32 ROW_NEXT   = 1;

        private IColumn _column;

        [Test]
        public void Constructor_SetsRowCount_ToPassedInValue()
        {
            _column = new Column(SOME_HEIGHT);

            Assert.That(_column.RowCount, Is.EqualTo(SOME_HEIGHT));
        }

        [Test]
        public void Constructor_SetsRows_ToNoToken()
        {
            _column = new Column(SOME_HEIGHT);

            // check that each row is set to Token.None
            Boolean valid = _column.Rows.Aggregate(true, (current, row) => current & row == Token.None);

            Assert.That(valid, Is.True);
        }

        [Test]
        public void AddToken_SetsBottomRow_ToPassedInTokenValue()
        {
            _column = new Column(SOME_HEIGHT);

            _column.AddToken(Token.Player1);

            Assert.That(_column.Rows[ROW_BOTTOM], Is.EqualTo(Token.Player1));
        }

        [Test]
        public void AddToken_SetsNextRow_ToPassedInTokenValue()
        {
            _column = new Column(SOME_HEIGHT);

            _column.AddToken(Token.Player1);
            _column.AddToken(Token.Player1);

            Assert.That(_column.Rows[ROW_NEXT], Is.EqualTo(Token.Player1));
        }

        [Test]
        public void IsFull_ReturnsTrue_WhenColumnFull()
        {
            _column = new Column(SOME_HEIGHT);

            for (var i = 0; i < SOME_HEIGHT; ++i)
            {
                _column.AddToken(Token.Player1);
            }

            Assert.That(_column.IsFull, Is.True);
        }

        [Test]
        public void IsFull_ReturnsFalse_WhenColumnNotFull()
        {
            _column = new Column(SOME_HEIGHT);

            _column.AddToken(Token.Player1);

            Assert.That(_column.IsFull, Is.False);
        }
    }
}
