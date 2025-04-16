using Data;
using Logic;

namespace LogicTest
{
    [TestClass]
    public sealed class BallLogicTests
    {
        [TestMethod]
        public void Constructor_WhenBallLogicIsCreated_ShouldInitializeValuesCorrectly()
        {
            var repository = new BallsRepository();
            var board = new Board(800, 600);
            var logic = new BallLogic(repository, board);
            Assert.IsNotNull(logic);
            
        }
    }
}
