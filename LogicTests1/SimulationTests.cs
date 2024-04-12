using Logic;
using NUnit.Framework;

namespace LogicTests
{
    [TestFixture]
    public class SimulationTests
    {
        [Test]
        public void isRunning_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            Board board = new Board(300, 100, 5);
            var simulation = new Simulation(board);

            // Act
            var result = simulation.isRunning();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task startSimulation_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            Board board = new Board(300, 100, 5);
            var simulation = new Simulation(board);

            Thread thread = new Thread(simulation.startSimulation);

            // Assert
            thread.Start();
            await Task.Delay(10);
            Assert.IsTrue(simulation.isRunning());
            simulation.stopSimulation();
            await Task.Delay(10);
            Assert.IsFalse(simulation.isRunning());
        }
    }
}