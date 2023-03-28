using System;
using FluentAssertions;
using Xunit;

namespace NKnife.Bytes.UnitTests;

public class UtilByteSetBitValueTest
{
    [Fact]
    public void SetBitValueTest01()
    {
        int i = 0;

        int sut = UtilByte.SetBitValue(i, 0, true);
        sut.Should().Be(1);

        for (ushort j = 1; j < 32; j++)
        {
            sut = UtilByte.SetBitValue(i, j, true);
            sut.Should().Be((int)Math.Pow(2, j));
        }
    }

    [Fact]
    public void SetBitValueTest02()
    {
        int i = 240;
        int sut = UtilByte.SetBitValue(i, 7, true);
        sut.Should().Be(240);

        sut = UtilByte.SetBitValue(i, 3, true);
        sut.Should().Be(248);

        sut = UtilByte.SetBitValue(i, 0, true);
        sut.Should().Be(241);
    }
}