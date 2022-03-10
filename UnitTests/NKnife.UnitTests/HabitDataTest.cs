using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace NKnife.UnitTests
{
    public class HabitDataTest
    {
        [Fact]
        public void Test01()
        {
            var hd = new HabitData();
            hd.SetValue("abcd", "abcd");
            hd.GetValue("abcd","1234").Should().Be("abcd");
        }
    }

}
