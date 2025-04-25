using Data.Models;

namespace DataTest
{
    [TestClass]
    public class BallTests
    {
        [TestMethod]
        public void Constructor_WhenBallIsCreated_ShouldInitializeValuesCorrectly()
        {
            var ball = new Ball(5, 10, 2, 3, 10);

            Assert.IsNotNull(ball);
            Assert.AreEqual(5, ball.Id);
            Assert.AreEqual(10, ball.XPos);
            Assert.AreEqual(2, ball.YPos);
            Assert.AreEqual(3, ball.Radius);
            Assert.AreEqual(10, ball.Mass);
        }

        [TestMethod]
        public void Constructor_WhenBallsAreCreated_ShouldNotBeSame()
        {
            var ball1 = new Ball(5, 10, 2, 3, 10);
            var ball2 = new Ball(6, 10, 2, 3, 10);
            
            Assert.AreNotEqual(ball1.Id, ball2.Id);
            Assert.AreEqual(ball1.XPos, ball2.XPos);
            Assert.AreEqual(ball1.YPos, ball2.YPos);
            Assert.AreEqual(ball1.Radius, ball2.Radius);
            Assert.AreEqual(ball1.Mass, ball2.Mass);

            Assert.AreNotSame(ball1, ball2);
        }
    }
}
