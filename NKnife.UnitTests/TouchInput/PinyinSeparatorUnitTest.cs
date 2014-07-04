using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NKnife.Chinese.Ime.Pinyin;
using NKnife.Ioc;

namespace NKnife.UnitTests.TouchInput
{
    [TestClass]
    public class PinyinSeparatorUnitTest
    {
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            DI.Initialize();
        }

        [TestMethod]
        public void SeparateBaseTestMethod()
        {
            var separator = new PinyinSeparator(DI.Get<ISyllableCollection>());
            var result = separator.Separate("zhongguo");
            Assert.AreEqual(2, result.Count);
            result = separator.Separate("zhongguoqinghai");
            Assert.AreEqual(4, result.Count);
            result = separator.Separate("zhongguobeijingchangping");
            Assert.AreEqual(6, result.Count);
        }

        [TestMethod]
        public void SeparateProTestMethod()
        {
            var separator = new PinyinSeparator(DI.Get<ISyllableCollection>());
            var result = separator.Separate("xian");
            Assert.AreEqual(1, result.Count);
            result = separator.Separate("xiang");
            Assert.AreEqual(1, result.Count);
            result = separator.Separate("xianguo");
            Assert.AreEqual(2, result.Count);
            result = separator.Separate("xiana");
            Assert.AreEqual(2, result.Count);
        }
    }
}
