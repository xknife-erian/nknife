using System.Text.RegularExpressions;
using FluentAssertions;
using NKnife.Chinese;
using NKnife.Util;
using Xunit;

namespace NKnife.UnitTests.Util
{
    public class UtilRandomTest
    {
        [Fact]
        public void RandomStringTest()
        {
            int length = 999;
            var str = UtilRandom.GetRandomString(length);
            str.Length.Should().Be(length);
        }

        [Fact]
        public void GetUnrepeatIntsTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                var input = UtilRandom.GetString(100, UtilRandom.RandomCharType.Number);
                Regex.IsMatch(input, @"^\d{100}$").Should().BeTrue(input);
            }
        }
    }
}
