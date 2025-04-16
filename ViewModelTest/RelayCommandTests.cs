using ViewModel;

namespace ViewModelTest
{
    [TestClass]
    public sealed class RelayCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullAction_ShouldThrow()
        {
            new RelayCommand(null);
        }

        [TestMethod]
        public void CanExecute_NoCanExecuteProvided_ShouldReturnTrue()
        {
            var cmd = new RelayCommand(_ => { });
            Assert.IsTrue(cmd.CanExecute(null));
        }

        [TestMethod]
        public void CanExecute_CanExecuteFuncProvided_ShouldReturnItsResult()
        {
            var cmdTrue = new RelayCommand(_ => { }, _ => true);
            var cmdFalse = new RelayCommand(_ => { }, _ => false);
            Assert.IsTrue(cmdTrue.CanExecute("x"));
            Assert.IsFalse(cmdFalse.CanExecute("x"));
        }

        [TestMethod]
        public void Execute_ShouldInvokeAction()
        {
            object received = null;
            var cmd = new RelayCommand(p => received = p);
            cmd.Execute(123);
            Assert.AreEqual(123, received);
        }

        [TestMethod]
        public void RaiseCanExecuteChanged_ShouldRaiseEvent()
        {
            bool fired = false;
            var cmd = new RelayCommand(_ => { });
            cmd.CanExecuteChanged += (s, e) => fired = true;
            cmd.RaiseCanExecuteChanged();
            Assert.IsTrue(fired);
        }
    }
}