using System;
using C4.LibC4;
using C4.LibC4.Rules;
using NUnit.Framework;

namespace TestLibC4
{
    public class TestCell
    {
        private const Int32 SOME_COLUMN       = 5;
        private const Int32 SOME_OTHER_COLUMN = 4;
        private const Int32 SOME_ROW          = 7;
        private const Int32 SOME_OTHER_ROW    = 5;

        private Cell _cell;

        [Test]
        public void Equality_ReturnsFalse_WhenComparedAgainstDifferentType()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);
            var differentType = new GameObjectFactory();

            Assert.That(_cell.Equals(differentType), Is.False);
        }

        [Test]
        public void Equality_ReturnsFalse_WhenComparedAgainstNull()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);

            Assert.That(_cell.Equals(null), Is.False);
        }

        [Test]
        public void Equality_ReturnsTrue_WhenComparedAgainstSameInstance()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);
            Cell sameCell = _cell;

            Assert.That(_cell.Equals(sameCell), Is.True);
        }

        [Test]
        public void Equality_ReturnsFalse_WhenColumnValueDoesNotMatch()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);
            var otherCell = new Cell(SOME_OTHER_COLUMN, SOME_ROW);

            Assert.That(_cell.Equals(otherCell), Is.False);
        }

        [Test]
        public void Equality_ReturnsFalse_WhenRowValueDoesNotMatch()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);
            var otherCell = new Cell(SOME_COLUMN, SOME_OTHER_ROW);

            Assert.That(_cell.Equals(otherCell), Is.False);
        }

        [Test]
        public void Equality_ReturnsTrue_WhenRowAndColumnValuesMatch()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);
            var otherCell = new Cell(SOME_COLUMN, SOME_ROW);

            Assert.That(_cell.Equals(otherCell), Is.True);
        }

        [Test]
        public void GetHash_ReturnsSameValue_WhenObjectsHaveSameValues()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);
            var otherCell = new Cell(SOME_COLUMN, SOME_ROW);

            Assert.That(_cell.GetHashCode(), Is.EqualTo(otherCell.GetHashCode()));
        }
    }
}
