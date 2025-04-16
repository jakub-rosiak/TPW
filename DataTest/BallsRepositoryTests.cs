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
    }
}
