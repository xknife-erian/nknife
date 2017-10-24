using System;
using FluentAssertions;
using NUnit.Framework;

namespace NKnife.UnitTest.Extensions
{
    public partial class StringTest
    {
        [Test]
        public void ToBytesTest0()
        {
            var abcd = "ab";
            var bs = abcd.ToBytes();
            bs.Should().Equal(97, 98);
        }

        [Test]
        public void ToBytesTest1()
        {
            var abcd = "abcd";
            var bs = abcd.ToBytes();
            bs.Should().Equal(97, 98, 99, 100);
        }

        [Test]
        public void ToBytesTest2()
        {
            var abcd = "a,b,c,d";
            var bs = abcd.ToBytes(',');
            bs.Should().Equal(0x61, 0x62, 0x63, 0x64);
        }

        [Test]
        public void ToBytesTest3()
        {
            var abcd = "a__b__c__d";
            var bs = abcd.ToBytes('_', '_');
            bs.Should().Equal(97, 98, 99, 100);
        }

        [Test]
        public void ToBytesTest4()
        {
            var abcd = "a,b_c,d";
            var bs = abcd.ToBytes(',', '_');
            bs.Should().Equal(97, 98, 99, 100);
        }
    }
}