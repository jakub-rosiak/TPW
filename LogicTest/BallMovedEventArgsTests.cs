using Data;
using Logic;

namespace LogicTest
{
    [TestClass]
    public sealed class BallMovedEventArgsTests
    {
        [TestMethod]
        public void Constructor_WhenBallMovedEventArgsIsCreated_ShouldInitializeValuesCorrectly()
        {
            var eventargs = new BallMovedEventArgs(1, 2, 3);
            Assert.IsNotNull(eventargs);
            Assert.AreEqual(1, eventargs.BallId);
            Assert.AreEqual(2, eventargs.NewX);
            Assert.AreEqual(3, eventargs.NewY);
        }
    }
}
