using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NKnife.Chinese;
using Xunit;

namespace NKnife.UnitTests.Chinese
{
    public class SurnamesTest
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var surnames = new Surnames();

            // Act
            var result = surnames.GenerateRandomSurnames(10);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Length.Should().Be(10);
        }
    }
}
