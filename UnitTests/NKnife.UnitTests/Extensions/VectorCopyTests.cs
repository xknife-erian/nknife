// Copyright (c) 2015 Illyriad Games Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Linq;
using Xunit;

namespace NKnife.UnitTests.Extensions
{
    public class VectorCopyTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        [InlineData(16)]
        [InlineData(17)]
        [InlineData(18)]
        [InlineData(19)]
        [InlineData(20)]
        [InlineData(21)]
        [InlineData(22)]
        [InlineData(23)]
        [InlineData(24)]
        [InlineData(25)]
        [InlineData(26)]
        [InlineData(27)]
        [InlineData(28)]
        [InlineData(29)]
        [InlineData(30)]
        [InlineData(31)]
        [InlineData(32)]
        [InlineData(33)]
        [InlineData(34)]
        [InlineData(35)]
        [InlineData(36)]
        [InlineData(37)]
        [InlineData(38)]
        [InlineData(39)]
        [InlineData(40)]
        [InlineData(41)]
        [InlineData(42)]
        [InlineData(64)]
        [InlineData(65)]
        [InlineData(66)]
        [InlineData(67)]
        [InlineData(68)]
        [InlineData(69)]
        [InlineData(70)]
        [InlineData(71)]
        [InlineData(72)]
        [InlineData(73)]
        [InlineData(74)]
        [InlineData(75)]
        [InlineData(76)]
        [InlineData(77)]
        [InlineData(78)]
        [InlineData(512)]
        public void CopyWorks(int length)
        {
            var dataFrom = Enumerable.Range(0, length).Select(x => (byte)x).ToArray();
            var dataTo = new byte[length];

            dataFrom.VectorizedCopy(0, dataTo, 0, length);

            for (var i = 0; i < dataFrom.Length; i++)
            {
                Assert.Equal(dataFrom[i], dataTo[i]);
            }
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 2, 0)]
        [InlineData(0, 0, 2)]
        [InlineData(0, 2, 2)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 2, 0)]
        [InlineData(1, 0, 2)]
        [InlineData(1, 2, 2)]
        [InlineData(2, 0, 0)]
        [InlineData(2, 2, 0)]
        [InlineData(2, 0, 2)]
        [InlineData(2, 2, 2)]
        [InlineData(8, 0, 0)]
        [InlineData(8, 2, 0)]
        [InlineData(8, 0, 2)]
        [InlineData(8, 2, 2)]
        [InlineData(32 - 1, 0, 0)]
        [InlineData(32 - 1, 1, 0)]
        [InlineData(32 - 1, 0, 3)]
        [InlineData(32 - 1, 7, 11)]
        [InlineData(32, 11, 0)]
        [InlineData(32 + 15, 7, 5)]
        [InlineData(64 - 1, 3, 17)]
        [InlineData(64, 17, 0)]
        [InlineData(64 + 31, 17, 17)]
        public void CopyOffsetWorks(int copyLength, int offset, int trailing)
        {
            var dataFrom = Enumerable.Range(0, copyLength + offset + trailing).Select(x => (byte)x).ToArray();
            var dataTo = Enumerable.Repeat(255, copyLength + offset + trailing).Select(x => (byte)x).ToArray();

            dataFrom.VectorizedCopy(offset, dataTo, offset, copyLength);

            var maxCopy = copyLength + offset;

            for (var i = 0; i < dataFrom.Length; i++)
            {
                if (i < offset)
                {
                    Assert.Equal((byte)255, dataTo[i]);
                }
                else if (i >= maxCopy)
                {
                    Assert.Equal((byte)255, dataTo[i]);
                }
                else
                {
                    Assert.Equal(dataFrom[i], dataTo[i]);
                }
            }
        }
    }
}
