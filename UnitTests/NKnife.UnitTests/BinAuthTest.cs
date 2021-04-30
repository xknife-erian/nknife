using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace NKnife.UnitTests
{
    public class BinAuthTest
    {
        /// <summary>
        /// 验证非负正整数是否为 2 的幂级
        /// </summary>
        [Fact]
        public void TestIsBinPower()
        {
            for (int i = 0; i < 64; i++)
            {
                long n = BinAuth.GetBinPower(i);
                BinAuth.IsBinPower(n).Should().BeTrue();
            }
        }
        
        /// <summary>
        /// 获取2 的 x 次方值
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void TestGetBinPower()
        {
            long n = (long)Math.Pow(2, 50);
            long m = BinAuth.GetBinPower(50);
            m.Should().Be(n);
        }

        /// <summary>
        /// 生成鉴权码
        /// </summary>
        [Fact]
        public void TestGenAuthCode()
        {
            long authCode = 0;
            List<long> codeList = new List<long>();
            for (int i = 1; i <= 62; i++)
            {
                codeList.Add((long)Math.Pow(2, i));
            }
            authCode = BinAuth.GenAuthCode(codeList.ToArray());
            Assert.Equal(9223372036854775806, authCode);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        [Fact]
        public void TestAddAuth()
        {
            long authCode = 0;
            for (int i = 1; i <= 62; i++)
            {
                long x = BinAuth.GetBinPower(i);
                authCode = BinAuth.AddAuth(authCode, x);
            }
            Assert.Equal(9223372036854775806, authCode);
        }

        /// <summary>
        /// 移除权限
        /// </summary>
        [Fact]
        public void TestRemoveAuth()
        {
            long authCode = 9223372036854775806;//表示最大权限值
            for (int i = 1; i <= 62; i++)
            {
                long x = BinAuth.GetBinPower(i);
                authCode = BinAuth.RemoveAuth(authCode, x);
            }
            Assert.Equal(0L, authCode);
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        [Fact]
        public void TestIsHasAuth()
        {
            long authCode = 0;
            for (int i = 1; i <= 62; i++)
            {
                long x = BinAuth.GetBinPower(i);
                authCode = BinAuth.AddAuth(authCode, x);
                BinAuth.IsHasAuth(authCode, x).Should().BeTrue();
            }

            for (int i = 1; i <= 62; i++)
            {
                BinAuth.IsHasAuth(authCode, BinAuth.GetBinPower(i)).Should().BeTrue();
            }

            BinAuth.IsHasAuth(-1, 0).Should().BeFalse();
            BinAuth.IsHasAuth(0, 0).Should().BeFalse();
        }
    }
}

