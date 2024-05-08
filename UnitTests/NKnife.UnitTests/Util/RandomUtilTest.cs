using System.Text.RegularExpressions;
using FluentAssertions;
using NKnife.Chinese;
using NKnife.Util;
using Xunit;

namespace NKnife.UnitTests.Util
{
    public class RandomUtilTest
    {
        [Fact]
        public void RandomStringTest()
        {
            int length = 999;
            var str = RandomUtil.GetRandomStringByLength(length, RandomUtil.RandomCharType.All);
            str.Length.Should().Be(length);
        }
    }
}
