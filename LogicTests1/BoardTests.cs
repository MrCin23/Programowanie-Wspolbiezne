using Logic;

namespace LogicTests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void ballGenerationAmountTest()
        {
            // Arrange
            var board = new Board(100, 100, 10);

            // Assert
            Assert.That(board.getBalls().Length, Is.EqualTo(10));
        }

        [Test]
        public void ballGenerationPositionsTest()
        {
            // Arrange
            var board = new Board(100, 100, 10000);

            // ASsert
            foreach (var b in board.getBalls())
            {
                Assert.IsTrue(b.x + b.getSize()/2 <= board.sizeX && b.x >= b.getSize()/2);
                Assert.IsTrue(b.y + b.getSize()/2 <= board.sizeY && b.y >= b.getSize()/2);
            }
        }
    }
}
