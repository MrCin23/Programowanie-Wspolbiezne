using Data;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;

namespace DataTests
{
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
            ball.setXVelocity(xVelocity);

            // Assert
            Assert.That(xVelocity, Is.EqualTo(ball.getXVelocity()));
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
            ball.setYVelocity(yVelocity);

            // Assert
            Assert.That(yVelocity, Is.EqualTo(ball.getYVelocity()));
        }

        [Test]
        public void positionTest()
        {
            var board = DataAbstractAPI.CreateDataAPI();
            board.setBoardParameters(100, 100, 1);
            float posx = 50;
            float posy = 50;

            IBall ball = board.getBalls()[0];
            ball.x = posx;
            ball.y = posy;

            Assert.That(posx, Is.EqualTo(ball.x));
            Assert.That(posy, Is.EqualTo(ball.y));
        }
    }
}
