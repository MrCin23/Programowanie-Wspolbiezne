using Data;

namespace DataTests
{
    [TestFixture]
    public class BallTests
    {
        [Test]
        public void getSizeTest()
        {
            // Arrange
            var ball = new Ball(100, 100);

            // Act
            var result = ball.getSize();

            // Assert
            Assert.That(5.0f, Is.EqualTo(result));
        }


        [Test]
        public void setXVelocityTest()
        {
            // Arrange
            var ball = new Ball(100, 100);
            float xVelocity = 10.0f;

            // Act
            ball.setXVelocity(
                xVelocity);

            // Assert
            Assert.That(xVelocity, Is.EqualTo(ball.getXVelocity()));
        }

        [Test]
        public void setYVelocityTest()
        {
            // Arrange
            var ball = new Ball(100, 100);
            float yVelocity = 10.0f;

            // Act
            ball.setYVelocity(
                yVelocity);

            // Assert
            Assert.That(yVelocity, Is.EqualTo(ball.getYVelocity()));
        }
    }
}
