using System;

namespace C4.LibC4.Rules
{
    public class Cell : IEquatable<Cell>
    {
        public Int32 Column { get; }
        public Int32 Row { get; }

        public Cell(Int32 column, Int32 row)
        {
            Column = column;
            Row = row;
        }

        public override Boolean Equals(Object other)
        {
            return Equals(other as Cell);
        }

        public Boolean Equals(Cell other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;

            return (Column == other.Column) && (Row == other.Row);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Column, Row);
        }
    }
}
