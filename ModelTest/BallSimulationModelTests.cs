using System;
using System.Linq;
using System.Reflection;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Models;

namespace ModelTest
{
    [TestClass]
    public sealed class BallSimulationModelTests
    {
        private BallSimulationModel _model;
        private bool _ballsChangedFired;

        [TestInitialize]
        public void SetUp()
        {
            _model = new BallSimulationModel();
            _ballsChangedFired = false;
            _model.BallsChanged += (s, e) => _ballsChangedFired = true;
        }

        [TestMethod]
        public void CreateBalls_WhenCalled_ShouldPopulateBallsCollection()
        {
            _model.CreateBalls(3);
            Assert.AreEqual(3, _model.Balls.Count);
            foreach (var ball in _model.Balls)
            {
                Assert.IsNotNull(ball);
                Assert.IsInstanceOfType(ball, typeof(BallDisplay));
            }
        }

        [TestMethod]
        public void CreateBalls_WhenCalled_ShouldRaiseBallsChangedEvent()
        {
            _model.CreateBalls(1);
            Assert.IsTrue(_ballsChangedFired);
        }

        [TestMethod]
        public void CreateBalls_WhenCalledMultipleTimes_ShouldClearPreviousAndRepopulate()
        {
            _model.CreateBalls(2);
            Assert.AreEqual(2, _model.Balls.Count);
            _ballsChangedFired = false;
            _model.CreateBalls(5);
            Assert.AreEqual(5, _model.Balls.Count);
            Assert.IsTrue(_ballsChangedFired);
        }

        [TestMethod]
        public void StartSimulation_And_StopSimulation_ShouldNotThrow()
        {
            _model.StartSimulation(100);
            _model.StopSimulation();
        }

        [TestMethod]
        public void OnBallMoved_WhenBallExists_ShouldUpdateBallPosition()
        {
            _model.CreateBalls(1);
            var ball = _model.Balls.First();
            double newX = ball.XPos + 10;
            double newY = ball.YPos + 20;
            var mi = typeof(BallSimulationModel)
                     .GetMethod("OnBallMoved", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(_model, new object?[] { null, new BallMovedEventArgs(ball.Id, newX, newY) });
            Assert.AreEqual(newX, ball.XPos);
            Assert.AreEqual(newY, ball.YPos);
        }

        [TestMethod]
        public void OnBallMoved_WhenBallDoesNotExist_ShouldNotChangeAnyBall()
        {
            _model.CreateBalls(1);
            var ball = _model.Balls.First();
            double oldX = ball.XPos;
            double oldY = ball.YPos;
            var mi = typeof(BallSimulationModel)
                     .GetMethod("OnBallMoved", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(_model, new object?[] { null, new BallMovedEventArgs(ball.Id + 1, oldX + 5, oldY + 5) });
            Assert.AreEqual(oldX, ball.XPos);
            Assert.AreEqual(oldY, ball.YPos);
        }
    }
}
