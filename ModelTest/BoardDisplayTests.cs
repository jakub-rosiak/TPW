using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;

namespace ModelTest
{
    [TestClass]
    public sealed class BoardDisplayTests
    {
        private BoardDisplay _board;
        private List<string> _fired;

        [TestInitialize]
        public void SetUp()
        {
            _board = new BoardDisplay(10.0, 20.0);
            _fired = new List<string>();
            _board.PropertyChanged += (s, e) => _fired.Add(e.PropertyName);
        }

        [TestMethod]
        public void Constructor_ShouldInitializeValues()
        {
            Assert.AreEqual(10.0, _board.Width);
            Assert.AreEqual(20.0, _board.Height);
        }

        [TestMethod]
        public void SettingWidth_ShouldFirePropertyChanged()
        {
            _board.Width = 30.0;
            Assert.AreEqual(30.0, _board.Width);
            CollectionAssert.Contains(_fired, nameof(BoardDisplay.Width));
        }

        [TestMethod]
        public void SettingHeight_ShouldFirePropertyChanged()
        {
            _board.Height = 40.0;
            Assert.AreEqual(40.0, _board.Height);
            CollectionAssert.Contains(_fired, nameof(BoardDisplay.Height));
        }
    }
}