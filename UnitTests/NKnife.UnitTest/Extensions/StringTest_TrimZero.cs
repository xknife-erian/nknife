using System;
using FluentAssertions;
using NKnife.Extensions;
using NUnit.Framework;

namespace NKnife.UnitTest.Extensions
{
    [TestFixture]
    public partial class StringTest
    {
        [Test]
        public void TrimZeroTest1()
        {
            var testString = "1.234000";
            var act = testString.TrimZero();
            Assert.AreEqual("1.234", act);
        }

        [Test]
        public void TrimZeroTest2()
        {
            var testString = "1234.00000";
            var act = testString.TrimZero();
            Assert.AreEqual("1234", act);
        }

        [Test]
        public void TrimZeroTest3()
        {
            var testString = "100.00000";
            var act = testString.TrimZero();
            Assert.AreEqual("100", act);
        }

        [Test]
        public void TrimZeroTest4()
        {
            var testString = "ABC0.00000";
            var act = testString.TrimZero();
            Assert.AreEqual("ABC0", act);
        }

        [Test]
        public void TrimZeroTest5()
        {
            var testString = "1.00000";
            var act = testString.TrimZero();
            Assert.AreEqual("1", act);
        }

        [Test]
        public void TrimZeroTest6()
        {
            var testString = "123000";
            var act = testString.TrimZero();
            Assert.AreEqual("123000", act);
        }

        [Test]
        public void TrimZeroTest7()
        {
            var testString = "0000";
            var act = testString.TrimZero();
            Assert.AreEqual("0", act);
        }

        [Test]
        public void TrimZeroTest8()
        {
            var testString = "0.005000";
            var act = testString.TrimZero();
            Assert.AreEqual("0.005", act);
        }

        [Test]
        public void TrimZeroTest9()
        {
            var testString = "0.00";
            var act = testString.TrimZero();
            Assert.AreEqual("0", act);
        }
    }


}
