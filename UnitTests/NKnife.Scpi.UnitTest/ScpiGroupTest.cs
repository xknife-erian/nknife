using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScpiKnife;

namespace NKnife.Scpi.UnitTest
{
    [TestClass]
    public class ScpiGroupTest
    {
        private ScpiGroup _ScpiGroup;

        [TestInitialize()]
        public void Preload()
        {
            _ScpiGroup = new ScpiGroup
            {
                new ScpiCommand {Interval = 0},
                new ScpiCommand {Interval = 1},
                new ScpiCommand {Interval = 2},
                new ScpiCommand {Interval = 3},
                new ScpiCommand {Interval = 4},
            };
        }

        #region UpItem

        [TestMethod]
        public void UpItemTest1()
        {
            Assert.IsTrue(_ScpiGroup.UpItem(1));
            Assert.AreEqual(1, _ScpiGroup[0].Interval);
        }

        [TestMethod]
        public void UpItemTest2()
        {
            Assert.IsFalse(_ScpiGroup.UpItem(0));
        }

        [TestMethod]
        public void UpItemTest3()
        {
            Assert.IsFalse(_ScpiGroup.UpItem(5));
        }

        [TestMethod]
        public void UpItemTest4()
        {
            Assert.IsTrue(_ScpiGroup.UpItem(4));
            Assert.AreEqual(4, _ScpiGroup[3].Interval);
        }

        #endregion

        [TestMethod]
        public void DownItemTest1()
        {
            Assert.IsTrue(_ScpiGroup.DownItem(3));
            Assert.AreEqual(3, _ScpiGroup[4].Interval);
        }

        [TestMethod]
        public void DownItemTest2()
        {
            Assert.IsFalse(_ScpiGroup.DownItem(4));
        }

        [TestMethod]
        public void DownItemTest3()
        {
            Assert.IsFalse(_ScpiGroup.DownItem(-1));
        }

        [TestMethod]
        public void DownItemTest4()
        {
            Assert.IsTrue(_ScpiGroup.DownItem(0));
            Assert.AreEqual(1, _ScpiGroup[0].Interval);
            Assert.AreEqual(0, _ScpiGroup[1].Interval);
        }
    }
}