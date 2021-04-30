using System;
using FluentAssertions;
using Xunit;

namespace NKnife.UnitTests.Extensions
{
    public partial class StringTest
    {
        [Fact]
        public void ToBytesTest0()
        {
            var abcd = "ab";
            var bs = abcd.ToBytes();
            bs.Should().Equal(97, 98);
        }

        [Fact]
        public void ToBytesTest1()
        {
            var abcd = "abcd";
            var bs = abcd.ToBytes();
            bs.Should().Equal(97, 98, 99, 100);
        }

        [Fact]
        public void ToBytesTest2()
        {
            var abcd = "a,b,c,d";
            var bs = abcd.ToBytes(',');
            bs.Should().Equal(0x61, 0x62, 0x63, 0x64);
        }

        [Fact]
        public void ToBytesTest3()
        {
            var abcd = "a__b__c__d";
            var bs = abcd.ToBytes('_', '_');
            bs.Should().Equal(97, 98, 99, 100);
        }

        [Fact]
        public void ToBytesTest4()
        {
            var abcd = "a,b_c,d";
            var bs = abcd.ToBytes(',', '_');
            bs.Should().Equal(97, 98, 99, 100);
        }
    }
}