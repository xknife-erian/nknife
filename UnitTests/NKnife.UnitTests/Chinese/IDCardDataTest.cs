using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NKnife.Chinese;
using Xunit;

namespace NKnife.UnitTests.Chinese
{
    // ReSharper disable once InconsistentNaming
    public class IDCardDataTest
    {
        [Fact]
        public void CheckTest01()
        {
            var id = "12345";
            IDCardData.Check(id).Should().BeFalse();
        }

        //TODO: IDCardChecker还需要更详尽的测试
    }
}
