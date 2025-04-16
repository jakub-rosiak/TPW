using Model;

namespace ModelTest
{
    [TestClass]
    public sealed class BallDisplayTests
    {
        private BallDisplay _ball;

        [TestInitialize]
        public void SetUp()
        {
            _ball = new BallDisplay(1, 2.0, 3.0, 4.0);
        }

        [TestMethod]
        public void Constructor_WhenCreated_ShouldInitializeValuesCorrectly()
        {
            Assert.IsNotNull(_ball);
            Assert.AreEqual(1, _ball.Id);
            Assert.AreEqual(2.0, _ball.XPos);
            Assert.AreEqual(3.0, _ball.YPos);
            Assert.AreEqual(4.0, _ball.Radius);
        }

        [TestMethod]
        public void SettingXPos_WhenChanged_ShouldFirePropertyChanged()
        {
            var fired = new List<string>();
            _ball.PropertyChanged += (s, e) => fired.Add(e.PropertyName);

            _ball.XPos = 5.5;

            Assert.AreEqual(5.5, _ball.XPos);
            CollectionAssert.Contains(fired, nameof(BallDisplay.XPos));
        }

        [TestMethod]
        public void SettingYPos_WhenChanged_ShouldFirePropertyChanged()
        {
            var fired = new List<string>();
            _ball.PropertyChanged += (s, e) => fired.Add(e.PropertyName);

            _ball.YPos = 6.6;

            Assert.AreEqual(6.6, _ball.YPos);
            CollectionAssert.Contains(fired, nameof(BallDisplay.YPos));
        }

        [TestMethod]
        public void IdAndRadius_ShouldBeReadOnlyProperties()
        {
            var idProp     = typeof(BallDisplay).GetProperty(nameof(BallDisplay.Id));
            var radiusProp = typeof(BallDisplay).GetProperty(nameof(BallDisplay.Radius));

            Assert.IsTrue (idProp.CanRead);
            Assert.IsFalse(idProp.CanWrite);

            Assert.IsTrue (radiusProp.CanRead);
            Assert.IsFalse(radiusProp.CanWrite);
        }
    }
}