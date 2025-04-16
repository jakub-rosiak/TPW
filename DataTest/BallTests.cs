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
            Assert.AreEqual(2, ball.XVel);
            Assert.AreEqual(3, ball.YVel);
        }

        [TestMethod]
        public void test12()
        {
            var ball1 = new Ball(5, 10, 2, 3);
            var ball2 = new Ball(5, 10, 2, 3);

            Assert.AreEqual(ball1.XPos, ball2.XPos);
            Assert.AreEqual(ball1.YPos, ball2.YPos);
            Assert.AreEqual(ball1.XVel, ball2.XVel);
            Assert.AreEqual(ball1.YVel, ball2.YVel);

            Assert.AreNotSame(ball1, ball2);
        }

        [TestMethod]
        public void AddForce_ShouldUpdateVelocityCorrectly()
        {
            _ball.AddForce(10, 20);

            Assert.AreEqual(10, _ball.XVel);
            Assert.AreEqual(20, _ball.YVel);
        }

        [TestMethod]
        public void Move_ShouldUpdatePositionCorrectly()
        {
            _ball.AddForce(10, 20);

            int time = 2;
            _ball.Move(time);

            Assert.AreEqual(20, _ball.XPos);
            Assert.AreEqual(40, _ball.YPos);
        }
    }
}
