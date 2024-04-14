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
            var ball = DataAbstractAPI.CreateDataAPI(100, 100);

            // Act
            var result = ball.getSize();

            // Assert
            Assert.That(10.0f, Is.EqualTo(result));
        }


        [Test]
        public void setXVelocityTest()
        {
            // Arrange
            var api = DataAbstractAPI.CreateDataAPI(100, 100);
            float xVelocity = 10.0f;

            // Act
            api.setXVelocity(
                xVelocity);

            // Assert
            Assert.That(xVelocity, Is.EqualTo(api.getXVelocity()));
        }

        [Test]
        public void setYVelocityTest()
        {
            // Arrange
            var ball = DataAbstractAPI.CreateDataAPI(100, 100);
            float yVelocity = 10.0f;

            // Act
            ball.setYVelocity(
                yVelocity);

            // Assert
            Assert.That(yVelocity, Is.EqualTo(ball.getYVelocity()));
        }

        [Test]
        public void positionTest()
        {
            var ball = DataAbstractAPI.CreateDataAPI(100, 100);
            float posx = 50;
            float posy = 50;

            ball.x = posx;
            ball.y = posy;

            Assert.That(posx, Is.EqualTo(ball.x));
            Assert.That(posy, Is.EqualTo(ball.y));
        }
    }
}
