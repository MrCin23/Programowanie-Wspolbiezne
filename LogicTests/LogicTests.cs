using Logic;
using Data;
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
            Assert.AreEqual(10, result.Length);

            foreach (var b in result)
            {
                Assert.IsTrue(b.x + b.getSize() <= maxX && b.x >= b.getSize());
                Assert.IsTrue(b.y + b.getSize() <= maxY && b.y >= b.getSize());
            }
        }

        [Test]
        public void changeXdirectionTest()
        {
            // Arrange
            Ball ball = new Ball(100, 100);

            // Act
            ball.setXVelocity(5);
            ball.setYVelocity(0);
            Logic.Logic.changeXdirection(ball);

            // Assert
            Assert.AreEqual(-5, ball.x);
        }

        [Test]
        public void changeYdirectionTest()
        {
            // Arrange
            Ball ball = new Ball(100, 100);

            // Act
            ball.setXVelocity(0);
            ball.setYVelocity(5);
            Logic.Logic.changeXdirection(ball);

            // Assert
            Assert.AreEqual(-5, ball.y);
        }

        [Test]
        public void updatePositionTest()
        {
            // Arrange
            Ball ball = new Ball(100, 100);

            // Act
            ball.setXVelocity(5);
            ball.setYVelocity(5);
            ball.x = 50;
            ball.y = 50;
            Logic.Logic.updatePosition(ball);

            // Assert
            Assert.AreEqual(50.05, ball.x);
            Assert.AreEqual(50.05, ball.y);
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
            Assert.AreEqual(50.05, board.getBalls()[0].x);
            Assert.AreEqual(50.05, board.getBalls()[0].y);
        }
    }
}