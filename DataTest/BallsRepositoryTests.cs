using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Data;

namespace DataTests
{
    [TestClass]
    public class BallsRepositoryTests
    {
        [TestMethod]
        public void AddBall_ShouldIncreaseCount()
        {
            // Arrange
            IBallsRepository repository = new BallsRepository();
            var ball = new Ball(10, 10, 1, 1);

            // Act
            repository.AddBall(ball);

            // Assert
            Assert.AreEqual(1, repository.GetAllBalls().Count());
        }

        [TestMethod]
        public void RemoveBall_ShouldDecreaseCount()
        {
            // Arrange
            IBallsRepository repository = new BallsRepository();
            var ball1 = new Ball(10, 10, 1, 1);
            var ball2 = new Ball(20, 20, 2, 2);

            repository.AddBall(ball1);
            repository.AddBall(ball2);

            // Act
            repository.RemoveBall(ball1);

            // Assert
            Assert.AreEqual(1, repository.GetAllBalls().Count());
            Assert.AreSame(ball2, repository.GetAllBalls().First());
        }

        [TestMethod]
        public void GetAllBalls_ShouldReturnAllBalls()
        {
            // Arrange
            IBallsRepository repository = new BallsRepository();
            var ball1 = new Ball(10, 10, 1, 1);
            var ball2 = new Ball(20, 20, 2, 2);

            repository.AddBall(ball1);
            repository.AddBall(ball2);

            // Act
            var allBalls = repository.GetAllBalls().ToList();

            // Assert
            Assert.AreEqual(2, allBalls.Count);
            Assert.IsTrue(allBalls.Contains(ball1));
            Assert.IsTrue(allBalls.Contains(ball2));
        }

        [TestMethod]
        public void Ball_Move_ShouldUpdatePosition()
        {
            // Arrange
            double initialX = 10;
            double initialY = 10;
            double vx = 2;
            double vy = -3;
            double deltaTime = 1.0;
            var ball = new Ball(initialX, initialY, vx, vy);

            // Act
            ball.Move(deltaTime);

            // Assert
            Assert.AreEqual(initialX + vx * deltaTime, ball.XPos, 0.001);
            Assert.AreEqual(initialY + vy * deltaTime, ball.YPos, 0.001);
        }

        [TestMethod]
        public void Ball_AddForce_ShouldChangeVelocity()
        {
            // Arrange
            double initialXVel = 2;
            double initialYVel = 3;
            double forceX = 1;
            double forceY = -2;
            var ball = new Ball(0, 0, initialXVel, initialYVel);

            // Act
            ball.AddForce(forceX, forceY);

            // Assert
            Assert.AreEqual(initialXVel + forceX, ball.XVel, 0.001);
            Assert.AreEqual(initialYVel + forceY, ball.YVel, 0.001);
        }
    }
}
