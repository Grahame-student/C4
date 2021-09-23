using System;
using C4.LibC4;
using NUnit.Framework;

namespace TestLibC4
{
    public class TestGameObjectFactory
    {
        private const Int32 SOME_ROWS = 6;
        private const Int32 SOME_COLUMNS = 5;

        private IGameObjectFactory _factory;

        [Test]
        public void GetColumn_Returns_ColumnInstance()
        {
            _factory = new GameObjectFactory();

            Assert.That(_factory.GetColumn(SOME_ROWS), Is.InstanceOf<IColumn>());
        }

        [Test]
        public void GetColumn_PassesRowCount_ToColumnConstructor()
        {
            _factory = new GameObjectFactory();

            IColumn column = _factory.GetColumn(SOME_ROWS);

            Assert.That(column.RowCount, Is.EqualTo(SOME_ROWS));
        }

        [Test]
        public void GetBoard_Returns_BoardInstance()
        {
            _factory = new GameObjectFactory();

            Assert.That(_factory.GetBoard(0, 0), Is.InstanceOf<IBoard>());
        }

        [Test]
        public void GetBoard_PassesColumnCount_ToBoardConstructor()
        {
            _factory = new GameObjectFactory();

            IBoard board = _factory.GetBoard(SOME_COLUMNS, 0);

            Assert.That(board.ColumnCount, Is.EqualTo(SOME_COLUMNS));
        }

        [Test]
        public void GetBoard_PassesRowCount_ToBoardConstructor()
        {
            _factory = new GameObjectFactory();

            IBoard board = _factory.GetBoard(SOME_COLUMNS, SOME_ROWS);

            Assert.That(board.Columns[0].RowCount, Is.EqualTo(SOME_ROWS));
        }

        [Test]
        public void GetGame_Returns_GameInstance()
        {
            _factory = new GameObjectFactory();

            Assert.That(_factory.GetGame(), Is.InstanceOf<IGame>());
        }
    }
}
