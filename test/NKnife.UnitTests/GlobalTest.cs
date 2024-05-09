
using FluentAssertions;
using Xunit;

namespace NKnife.UnitTests
{
    public class GlobalUnitTest
    {
        [Fact]
        public void TestMethod()
        {
            Global.Culture.Should().NotBeNullOrEmpty();
        }
    }
}
