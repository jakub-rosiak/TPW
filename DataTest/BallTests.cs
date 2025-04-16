using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTest
{
    [TestClass]
    public class BallTests
    {
        private Ball _ball = new Ball(0,0,0,0);
        
        [TestMethod]
        public void Constructor_WhenBallIsCreated_ShouldInitializeValuesCorrectly()
        {
            var ball = new Ball(5, 10, 2, 3);

            Assert.IsNotNull(ball);
            Assert.AreEqual(5, ball.XPos);
            Assert.AreEqual(10, ball.YPos);
        }

        [TestMethod]
        public void Constructor_WhenBallsAreCreated_ShouldNotBeSame()
        {
            var ball1 = new Ball(5, 10, 2, 3);
            var ball2 = new Ball(5, 10, 2, 3);

            Assert.AreEqual(ball1.XPos, ball2.XPos);
            Assert.AreEqual(ball1.YPos, ball2.YPos);
            Assert.AreEqual(ball1.Radius, ball2.Radius);

            Assert.AreNotSame(ball1, ball2);
        }
    }
}
