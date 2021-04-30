using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace NKnife.UnitTests.Extensions
{
    public class SequentialGuidTest
    {
        [Fact]
        public void CreateTest1()
        {
            var id1 = SequentialGuid.Create().ToString();
            var id2 = SequentialGuid.Create().ToString();
            var id3 = SequentialGuid.Create().ToString();
            var id4 = SequentialGuid.Create().ToString();
            var a = id1.Substring(0, 8);
            var b = id2.Substring(0, 8);
            var c = id3.Substring(0, 8);
            var d = id4.Substring(0, 8);
            a.Should().Be(b).And.Be(c).And.Be(d);
        }

        [Fact]
        public void CreateTest2()
        {
            var id1 = SequentialGuid.Create(SequentialGuidType.SequentialAsBinary).ToString();
            var id2 = SequentialGuid.Create(SequentialGuidType.SequentialAsBinary).ToString();
            var id3 = SequentialGuid.Create(SequentialGuidType.SequentialAsBinary).ToString();
            var id4 = SequentialGuid.Create(SequentialGuidType.SequentialAsBinary).ToString();
            var a = id1.Substring(0, 8);
            var b = id2.Substring(0, 8);
            var c = id3.Substring(0, 8);
            var d = id4.Substring(0, 8);
            a.Should().Be(b).And.Be(c).And.Be(d);
        }

        [Fact]
        public void CreateTest3()
        {
            var id1 = SequentialGuid.Create(SequentialGuidType.SequentialAtEnd).ToString();
            var id2 = SequentialGuid.Create(SequentialGuidType.SequentialAtEnd).ToString();
            var id3 = SequentialGuid.Create(SequentialGuidType.SequentialAtEnd).ToString();
            var id4 = SequentialGuid.Create(SequentialGuidType.SequentialAtEnd).ToString();
            var a = id1.Substring(id1.Length - 9, 8);
            var b = id2.Substring(id2.Length - 9, 8);
            var c = id3.Substring(id3.Length - 9, 8);
            var d = id4.Substring(id4.Length - 9, 8);
            a.Should().Be(b).And.Be(c).And.Be(d);
        }

        [Fact]
        public void ExtractDateTimeTicksTest1()
        {
            var now = DateTime.UtcNow;
            long timestampSrc = DateTime.UtcNow.Ticks / 10000L;
            var ticksBytes = BitConverter.GetBytes(timestampSrc);
            ticksBytes[0] = 0;
            ticksBytes[1] = 0;
            long timestampNew = BitConverter.ToInt64(ticksBytes);

            var id = SequentialGuid.Create();
            var ticks = SequentialGuid.ExtractDateTimeTicks(id);

            ticks.Should().Be(timestampNew);
            var dt = new DateTime(ticks);
        }
    }
}
