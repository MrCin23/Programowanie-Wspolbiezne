using Logic;
using NUnit.Framework;
using System;

namespace LogicTests
{
    [TestFixture]
    public class LogicTests
    {
        [Test]
        public void createBallsTest()
        {
            // Arrange

            int maxX = 100;
            int maxY = 100;
            int amount = 10;

            // Act
            var result = Logic.Logic.createBalls(
                maxX,
                maxY,
                amount);

            // Assert
            Assert.That(result.Length, Is.EqualTo(10));

            foreach (var b in result)
            {
                Assert.IsTrue(b.x + b.getSize() / 2 <= maxX && b.x >= b.getSize() / 2);
                Assert.IsTrue(b.y + b.getSize() / 2 <= maxY && b.y >= b.getSize() / 2);
            }
        }

        [Test]
        public void changeXdirectionTest()
        {
            // Arrange
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI(100, 100, 1);

            //Ball ball = new Ball(100, 100);

            // Act
            var board = api.getBoard();
            var balls = board.getBalls();
            var ball = balls[0];
            ball.setXVelocity(5);
            ball.setYVelocity(0);
            Logic.Logic.changeXdirection(ball);

            // Assert
            Assert.That(ball.getXVelocity(), Is.EqualTo(-5));
        }

        [Test]
        public void changeYdirectionTest()
        {
            // Arrange
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI(100, 100, 1);

            //Ball ball = new Ball(100, 100);

            // Act
            var board = api.getBoard();
            var balls = board.getBalls();
            var ball = balls[0];
            ball.setXVelocity(5);
            ball.setYVelocity(0);
            Logic.Logic.changeXdirection(ball);

            // Act
            ball.setXVelocity(0);
            ball.setYVelocity(5);
            Logic.Logic.changeYdirection(ball);

            // Assert
            Assert.That(ball.getYVelocity(), Is.EqualTo(-5));
        }

        [Test]
        public void updatePositionTest()
        {
            // Arrange
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI(100, 100, 1);

            //Ball ball = new Ball(100, 100);

            // Act
            var board = api.getBoard();
            var balls = board.getBalls();
            var ball = balls[0];
            ball.setXVelocity(5);
            ball.setYVelocity(0);
            Logic.Logic.changeXdirection(ball);

            // Act
            ball.setXVelocity(5);
            ball.setYVelocity(5);
            ball.x = 50;
            ball.y = 50;
            Logic.Logic.updatePosition(ball);

            // Assert
            Assert.That(ball.x, Is.EqualTo(50.05).Within(0.01f));
            Assert.That(ball.y, Is.EqualTo(50.05).Within(0.01f));
        }

        [Test]
        public void updateBoardTest()
        {
            // Arrange
            Board board = new Board(100, 100, 1);

            // Act
            board.getBalls()[0].x = 50;
            board.getBalls()[0].y = 50;
            board.getBalls()[0].setXVelocity(5);
            board.getBalls()[0].setYVelocity(5);

            Logic.Logic.updateBoard(
                board);

            // Assert
            Assert.That(board.getBalls()[0].x, Is.EqualTo(50.05).Within(0.01f));
            Assert.That(board.getBalls()[0].y, Is.EqualTo(50.05).Within(0.01f));
        }
    }
}