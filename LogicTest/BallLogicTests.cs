using System.Reflection;
using Data.Repositories;
using Data.Models;
using Logic.Services;
using Logic.Events;

namespace LogicTest
{
    [TestClass]
    public class BallLogicTests
    { 
        private static BallLogic CreateLogic(int w = 100, int h = 100)
        {
            return new BallLogic(new BallsRepository(), new Board(w, h), new Logger());
        }

        [TestMethod]
        public void CreateBalls_ShouldPopulateRepository()
        {
            var logic = CreateLogic();
            logic.CreateBalls(5);
            Assert.AreEqual(5, logic.GetBalls().Count());
        }

        [TestMethod]
        public async Task UpdatePositions_ShouldKeepBallsInsideBoard()
        {
            var logic = CreateLogic(200, 200);
            logic.CreateBalls(10);
            await logic.UpdatePositionsAsync(0.1);
            foreach (var b in logic.GetBalls())
            {
                Assert.IsTrue(b.XPos >= b.Radius && b.XPos <= 200 - b.Radius);
                Assert.IsTrue(b.YPos >= b.Radius && b.YPos <= 200 - b.Radius);
            }
        }

        [TestMethod]
        public void Start_ShouldRaiseBallMovedEvents()
        {
            var logic = CreateLogic();
            logic.CreateBalls(1);
            var count = 0;
            logic.BallMoved += (_, __) => Interlocked.Increment(ref count);
            logic.Start();
            Thread.Sleep(60);
            logic.Stop();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void Stop_ShouldStopRaisingEvents()
        {
            var logic = CreateLogic();
            logic.CreateBalls(1);
            var count = 0;
            logic.BallMoved += (_, __) => Interlocked.Increment(ref count);
            logic.Start();
            Thread.Sleep(50);
            logic.Stop();
            var afterStop = count;
            Thread.Sleep(50);
            Assert.AreEqual(afterStop, count);
        }

        [TestMethod]
        public void GetBalls_ShouldReturnRepositoryContent()
        {
            var logic = CreateLogic();
            logic.CreateBalls(3);
            Assert.AreEqual(3, logic.GetBalls().Count());
        }

        [TestMethod]
        public async Task BallMoved_AddAndRemoveHandlers()
        {
            var logic = CreateLogic();
            logic.CreateBalls(1);
            var count = 0;
            EventHandler<BallMovedEventArgs> h = (_, __) => Interlocked.Increment(ref count);
            logic.BallMoved += h;
            await logic.UpdatePositionsAsync(0.1);
            Assert.IsTrue(count > 0);
            count = 0;
            logic.BallMoved -= h;
            await logic.UpdatePositionsAsync(0.1);
            Assert.AreEqual(0, count);
        }
    }
}
