using System;
using Xunit;

namespace NKnife.UnitTests.Extensions
{
    public partial class StringTest
    {
        [Fact]
        public void TrimZeroTest1()
        {
            var testString = "1.234000";
            var act = testString.TrimZero();
            Assert.Equal("1.234", act);
        }

        [Fact]
        public void TrimZeroTest2()
        {
            var testString = "1234.00000";
            var act = testString.TrimZero();
            Assert.Equal("1234", act);
        }

        [Fact]
        public void TrimZeroTest3()
        {
            var testString = "100.00000";
            var act = testString.TrimZero();
            Assert.Equal("100", act);
        }

        [Fact]
        public void TrimZeroTest4()
        {
            var testString = "ABC0.00000";
            var act = testString.TrimZero();
            Assert.Equal("ABC0", act);
        }

        [Fact]
        public void TrimZeroTest5()
        {
            var testString = "1.00000";
            var act = testString.TrimZero();
            Assert.Equal("1", act);
        }

        [Fact]
        public void TrimZeroTest6()
        {
            var testString = "123000";
            var act = testString.TrimZero();
            Assert.Equal("123000", act);
        }

        [Fact]
        public void TrimZeroTest7()
        {
            var testString = "0000";
            var act = testString.TrimZero();
            Assert.Equal("0", act);
        }

        [Fact]
        public void TrimZeroTest8()
        {
            var testString = "0.005000";
            var act = testString.TrimZero();
            Assert.Equal("0.005", act);
        }

        [Fact]
        public void TrimZeroTest9()
        {
            var testString = "0.00";
            var act = testString.TrimZero();
            Assert.Equal("0", act);
        }
    }


}
