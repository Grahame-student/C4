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
        private const Int32 SMALLER_COLUMN    = 4;
        private const Int32 BIGGER_COLUMN     = 6;
        private const Int32 SOME_ROW          = 7;
        private const Int32 SOME_OTHER_ROW    = 5;
        private const Int32 SMALLER_ROW       = 6;
        private const Int32 BIGGER_ROW        = 8;

        private Cell _cell;

        [Test]
        public void Equality_ReturnsFalse_WhenComparedAgainstDifferentType()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);
            var differentType = new GameObjectFactory();

            // This is deliberate to test the error handling logic
            // ReSharper disable once SuspiciousTypeConversion.Global
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

        [Test]
        public void ToString_Returns_ColumnOfCell()
        {
            _cell = new Cell(SOME_COLUMN, 0);

            Assert.That(_cell.ToString(), Is.EqualTo($"{SOME_COLUMN}, {0}"));
        }

        [Test]
        public void ToString_Returns_RowOfCell()
        {
            _cell = new Cell(0, SOME_ROW);

            Assert.That(_cell.ToString(), Is.EqualTo($"{0}, {SOME_ROW}"));
        }

        [Test]
        public void CompareTo_Returns1_WhenOtherIsNull()
        {
            _cell = new Cell(0, SOME_ROW);

            Assert.That(_cell.CompareTo(null), Is.EqualTo(1));
        }

        [Test]
        public void CompareTo_Returns1_WhenOthersColumnIsSmaller()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);
            var other = new Cell(SMALLER_COLUMN, SOME_ROW);

            Assert.That(_cell.CompareTo(other), Is.EqualTo(1));
        }

        [Test]
        public void CompareTo_ReturnsMinus1_WhenOthersColumnIsBigger()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);
            var other = new Cell(BIGGER_COLUMN, SOME_ROW);

            Assert.That(_cell.CompareTo(other), Is.EqualTo(-1));
        }

        [Test]
        public void CompareTo_Returns1_WhenOthersColumnIsSameAndRowIsSmaller()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);
            var other = new Cell(SOME_COLUMN, SMALLER_ROW);

            Assert.That(_cell.CompareTo(other), Is.EqualTo(1));
        }

        [Test]
        public void CompareTo_ReturnsMinus1_WhenColumnIsSameAndRowIsBigger()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);
            var other = new Cell(SOME_COLUMN, BIGGER_ROW);

            Assert.That(_cell.CompareTo(other), Is.EqualTo(-1));
        }

        [Test]
        public void CompareTo_Returns0_WhenColumnIsSameAndRowIsSame()
        {
            _cell = new Cell(SOME_COLUMN, SOME_ROW);
            var other = new Cell(SOME_COLUMN, SOME_ROW);

            Assert.That(_cell.CompareTo(other), Is.EqualTo(0));
        }
    }
}
