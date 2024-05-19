using Data;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;
using System.Numerics;

namespace DataTests
{
    class testball : IBall
    {
        public Vector2 pos { get; internal set; }

        public Vector2 vel { get; set; }

        public event EventHandler<DataEventArgs>? ChangedPosition;

        public void destroy()
        {
            throw new NotImplementedException();
        }

        public float getMass()
        {
            throw new NotImplementedException();
        }

        public float getSize()
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class BallTests
    {
        [Test]
        public void getSizeTest()
        {
            // Arrange
            var board = DataAbstractAPI.CreateDataAPI();
            board.setBoardParameters(100, 100, 1);

            // Act
            IBall ball = board.getBalls()[0];
            var result = ball.getSize();

            // Assert
            Assert.That(10.0f, Is.EqualTo(result));
        }


        [Test]
        public void setXVelocityTest()
        {
            // Arrange
            var board = DataAbstractAPI.CreateDataAPI();
            board.setBoardParameters(100, 100, 1);
            float xVelocity = 10.0f;

            // Act
            IBall ball = board.getBalls()[0];
            Vector2 vel = new Vector2(xVelocity,0);
            ball.vel = vel;

            // Assert
            Assert.That(xVelocity, Is.EqualTo(ball.vel.X));
        }

        [Test]
        public void setYVelocityTest()
        {
            // Arrange
            var board = DataAbstractAPI.CreateDataAPI();
            board.setBoardParameters(100, 100, 1);
            float yVelocity = 10.0f;

            // Act
            IBall ball = board.getBalls()[0];
            Vector2 vel = new Vector2(0, yVelocity);
            ball.vel = vel;

            // Assert
            Assert.That(yVelocity, Is.EqualTo(ball.vel.Y));
        }

    }
}
