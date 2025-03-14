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

            Assert.AreEqual(5, ball.XPos);
            Assert.AreEqual(10, ball.YPos);
            Assert.AreEqual(2, ball.XVel);
            Assert.AreEqual(3, ball.YVel);
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
