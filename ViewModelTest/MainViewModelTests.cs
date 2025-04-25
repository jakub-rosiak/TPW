using System.Collections.ObjectModel;
using Model.Models;
using Model.Interfaces;
using ViewModel.ViewModels;

namespace ViewModelTest
{
    [TestClass]
    public sealed class MainViewModelTests
    {
        private MainViewModel _vm;

        [TestInitialize]
        public void SetUp()
        {
            _vm = new MainViewModel();
        }

        [TestMethod]
        public void Constructor_DefaultBoardSize()
        {
            Assert.AreEqual(800, _vm.Board.Width);
            Assert.AreEqual(600, _vm.Board.Height);
        }

        [TestMethod]
        public void Constructor_DefaultBallCount()
        {
            Assert.AreEqual(5, _vm.BallCount);
        }

        [TestMethod]
        public void SettingBallCount_ShouldRaisePropertyChanged()
        {
            bool fired = false;
            _vm.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(_vm.BallCount)) fired = true; };
            _vm.BallCount = 10;
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void SettingModel_ShouldRaisePropertyChanged()
        {
            var fake = new FakeModel();
            bool fired = false;
            _vm.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(_vm.Model)) fired = true; };
            _vm.Model = fake;
            Assert.IsTrue(fired);
            Assert.AreSame(fake, _vm.Model);
        }

        [TestMethod]
        public void CreateBallsCommand_CanExecute_DependsOnBallCount()
        {
            _vm.BallCount = 0;
            Assert.IsFalse(_vm.CreateBallsCommand.CanExecute(null));
            _vm.BallCount = 2;
            Assert.IsTrue(_vm.CreateBallsCommand.CanExecute(null));
        }

        [TestMethod]
        public void CreateBallsCommand_Execute_InvokesModelAndRaisesCanExecuteChanged()
        {
            var fake = new FakeModel();
            _vm.Model = fake;
            _vm.BallCount = 3;
            int startEvents = 0, stopEvents = 0;
            _vm.StartSimulationCommand.CanExecuteChanged += (s, e) => startEvents++;
            _vm.StopSimulationCommand.CanExecuteChanged += (s, e) => stopEvents++;
            _vm.CreateBallsCommand.Execute(null);
            Assert.AreEqual(3, fake.CreatedCount);
            Assert.AreEqual(1, startEvents);
            Assert.AreEqual(1, stopEvents);
        }

        [TestMethod]
        public void StartSimulationCommand_CanExecute_And_Execute()
        {
            var fake = new FakeModel();
            _vm.Model = fake;
            Assert.IsFalse(_vm.StartSimulationCommand.CanExecute(null));
            fake.Balls.Add(new BallDisplay(1,0,0,1));
            Assert.IsTrue(_vm.StartSimulationCommand.CanExecute(null));
            _vm.StartSimulationCommand.Execute(null);
            Assert.AreEqual(1, fake.StartCalls);
            Assert.AreEqual(1, fake.LastInterval);
        }

        [TestMethod]
        public void StopSimulationCommand_CanExecute_And_Execute()
        {
            var fake = new FakeModel();
            _vm.Model = fake;
            Assert.IsFalse(_vm.StopSimulationCommand.CanExecute(null));
            fake.Balls.Add(new BallDisplay(1,0,0,1));
            Assert.IsTrue(_vm.StopSimulationCommand.CanExecute(null));
            _vm.StopSimulationCommand.Execute(null);
            Assert.IsTrue(fake.StopCalled);
        }

        private class FakeModel : IBallSimulationModel
        {
            public ObservableCollection<BallDisplay> Balls { get; } = new ObservableCollection<BallDisplay>();
            public event EventHandler? BallsChanged;
            public int CreatedCount { get; private set; }
            public int StartCalls { get; private set; }
            public double LastInterval { get; private set; }
            public bool StopCalled { get; private set; }

            public void CreateBalls(int count)
            {
                CreatedCount = count;
                Balls.Clear();
                for (int i = 0; i < count; i++)
                    Balls.Add(new BallDisplay(i, 0, 0, 1));
                BallsChanged?.Invoke(this, EventArgs.Empty);
            }

            public void StartSimulation()
            {
                StartCalls++;
                LastInterval = 1;
            }

            public void StopSimulation()
            {
                StopCalled = true;
            }
        }
    }
}
