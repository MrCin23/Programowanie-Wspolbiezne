using Logic;

namespace LogicTests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void ballGenerationTest()
        {
            // Arrange
            var board = new Board(100, 100, 10);

            // Assert
            Assert.AreEqual(10, board.getBalls().Length);

            foreach (var b in board.getBalls())
            {
                Assert.IsTrue(b.x + b.getSize() <= board.sizeX && b.x >= b.getSize());
                Assert.IsTrue(b.y + b.getSize() <= board.sizeY && b.y >= b.getSize());
            }
        }
    }
}
