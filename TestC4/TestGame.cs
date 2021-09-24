using System;
using C4.LibC4;
using Moq;
using NUnit.Framework;

namespace TestLibC4
{
    public class TestGame
    {
        private const Int32 EXPECTED_COLUMNS = 7;
        private const Int32 EXPECTED_ROWS = 6;

        private const Int32 SOME_COLUMN = 3;

        private IGameObjectFactory _factory;
        private IGame _game;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new GameObjectFactory();
        }

        [Test]
        public void Constructor_SetsIsRunning_ToTrue()
        {
            _game = _factory.GetGame();

            Assert.That(_game.IsRunning, Is.EqualTo(true));
        }

        [Test]
        public void New_SetsBoardColumns_To7()
        {
            var mockFactory = new Mock<IGameObjectFactory>();
            _game = new Game(mockFactory.Object);

            _game.New();

            mockFactory.Verify(x => x.GetBoard(EXPECTED_COLUMNS, It.IsAny<UInt32>()));
        }

        [Test]
        public void New_SetsBoardRows_To6()
        {
            var mockFactory = new Mock<IGameObjectFactory>();
            _game = new Game(mockFactory.Object);

            _game.New();

            mockFactory.Verify(x => x.GetBoard(It.IsAny<UInt32>(), EXPECTED_ROWS));
        }

        [Test]
        public void New_SetsTurn_ToPlayer1()
        {
            _game = _factory.GetGame();

            _game.New();

            Assert.That(_game.Turn, Is.EqualTo(Token.Player1));
        }

        [Test]
        public void New_SetsWinner_ToNone()
        {
            _game = _factory.GetGame();

            _game.New();

            Assert.That(_game.Winner, Is.EqualTo(Token.None));
        }

        [Test]
        public void Quit_SetsIsRunning_ToFalse()
        {
            _game = _factory.GetGame();

            _game.Quit();

            Assert.That(_game.IsRunning, Is.EqualTo(false));
        }

        [Test]
        public void PlaceToken_PassesColumnNumber_ToBoard()
        {
            var mockFactory = new Mock<IGameObjectFactory>();
            var mockBoard = new Mock<IBoard>();
            mockFactory.Setup(x => x.GetBoard(It.IsAny<UInt32>(), It.IsAny<UInt32>()))
                .Returns(mockBoard.Object);
            _game = new Game(mockFactory.Object);
            _game.New();

            _game.PlaceToken(SOME_COLUMN);

            mockBoard.Verify(x => x.AddToken(SOME_COLUMN, It.IsAny<Token>()));
        }

        [Test]
        public void PlaceToken_PassesCurrentPlayer_ToBoard()
        {
            var mockFactory = new Mock<IGameObjectFactory>();
            var mockBoard = new Mock<IBoard>();
            mockFactory.Setup(x => x.GetBoard(It.IsAny<UInt32>(), It.IsAny<UInt32>()))
                .Returns(mockBoard.Object);
            _game = new Game(mockFactory.Object);
            _game.New();

            Token currentPlayer = _game.Turn;
            _game.PlaceToken(SOME_COLUMN);

            mockBoard.Verify(x => x.AddToken(It.IsAny<Int32>(), currentPlayer));
        }

        [Test]
        public void PlaceToken_SetsTurnToPlayer2_WhenTurnIsPlayer1()
        {
            _game = _factory.GetGame();

            _game.New();
            _game.PlaceToken(SOME_COLUMN);

            Assert.That(_game.Turn, Is.EqualTo(Token.Player2));
        }

        [Test]
        public void PlaceToken_SetsTurnToPlayer1_WhenTurnIsPlayer2()
        {
            _game = _factory.GetGame();

            _game.New();
            _game.PlaceToken(SOME_COLUMN);
            _game.PlaceToken(SOME_COLUMN);

            Assert.That(_game.Turn, Is.EqualTo(Token.Player1));
        }

        [Test]
        public void IsTurnValid_ReturnsTrue_WhenColumnNotFull()
        {
            var mockFactory = new Mock<IGameObjectFactory>();
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.IsValidMove(It.IsAny<Int32>())).Returns(true);
            mockFactory.Setup(x => x.GetBoard(It.IsAny<UInt32>(), It.IsAny<UInt32>()))
                .Returns(mockBoard.Object);

            _game = new Game(mockFactory.Object);
            _game.New();

            Assert.That(_game.IsMoveValid(1), Is.True);
        }

        [Test]
        public void IsTurnValid_ReturnsFalse_WhenColumnFull()
        {
            var mockFactory = new Mock<IGameObjectFactory>();
            var mockBoard = new Mock<IBoard>();
            mockBoard.Setup(x => x.IsValidMove(It.IsAny<Int32>())).Returns(false);
            mockFactory.Setup(x => x.GetBoard(It.IsAny<UInt32>(), It.IsAny<UInt32>()))
                .Returns(mockBoard.Object);

            _game = new Game(mockFactory.Object);
            _game.New();

            Assert.That(_game.IsMoveValid(1), Is.False);
        }
    }
}
