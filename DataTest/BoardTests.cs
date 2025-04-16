using Data;

namespace DataTest;

[TestClass]
public class BoardTests
{
    [TestMethod]
    public void Constructor_WhenBoardIsCreated_InitializeValuesCorrectly()
    {
        var board = new Board(800, 600);
        
        Assert.IsNotNull(board);
        Assert.AreEqual(800, board.Width);
        Assert.AreEqual(600, board.Height);
    }
}