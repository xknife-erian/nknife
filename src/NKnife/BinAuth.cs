using System;
using System.Linq;

namespace NKnife
{
    /// <summary>
    /// 二进制授权帮助类,最多支持 62 种不同权限，鉴权值最大为2的62次方（即：4611686018427387904）
    /// </summary>
    public class BinAuth
    {
        /// <summary>
        /// 验证非负正整数是否为2的幂级
        /// </summary>
        /// <remarks></remarks>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsBinPower(long n)
        {
            /*             
            判断是2的幂，1个数乘以2就是将该数左移1位，而2的0次幂为1， 
            所以2的n次幂（就是2的0次幂n次乘以2）就是将1左移n位，
            这样我们知道如果一个数n是2的幂，则其只有首位为1，
            其后若干个0，必然有n & (n - 1)为0。（在求1个数的二进制表示中1的个数的时候说过
            ，n&(n-1)去掉n的最后一个1）。因此，判断一个数n是否为2的幂，只需要判断n&(n-1)是否为0即可。
             */
            return (n & (n - 1)) == 0;
        }

        /// <summary>
        /// 获取2 的 x 次方值
        /// </summary>
        /// <param name="x">x 次方值</param>
        /// <returns></returns>
        public static long GetBinPower(int x)
        {
            return (long) System.Math.Pow(2, x);
        }

        /// <summary>
        /// 生成鉴权码
        /// </summary>
        /// <param name="arr">权限值（2的幂级）</param>
        /// <remarks>每个鉴权值执行或操作（code = code | n）</remarks>
        /// <returns></returns>
        public static long GenAuthCode(params long[] arr)
        {
            if (arr == null)
                throw new Exception("权限值数组不允许为空,GenAuthCode()");
            long code = 0;
            arr.ToList().ForEach(x =>
            {
                if (!IsBinPower(x))
                    throw new Exception($"值 {x} 为无效的鉴权码不是2的幂级");
                if (x < 0 || x > 4611686018427387904)
                    throw new Exception($"鉴权值 {x} 应大于 0 小于 4611686018427387904");
                code = code | x;
            });
            return code;
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="authCode">鉴权码</param>
        /// <param name="auth">权限值（2的幂级）</param>
        /// <remarks>code = authCode | auth</remarks>
        /// <returns></returns>
        public static long AddAuth(long authCode, long auth)
        {
            if (!IsBinPower(auth))
                throw new Exception($"值 {auth} 为无效的鉴权码不是2的幂级");

            if (auth < 0 || auth > 4611686018427387904)
                throw new Exception($"鉴权值 {auth} 应大于 0 小于 4611686018427387904");

            long code = authCode | auth;
            return code;
        }

        /// <summary>
        /// 移除权限
        /// </summary>
        /// <param name="authCode">鉴权码</param>
        /// <param name="auth">权限值（2的幂级）</param>
        /// <remarks>code = authCode & (~auth)</remarks>
        /// <returns></returns>
        public static long RemoveAuth(long authCode, long auth)
        {
            if (!IsBinPower(auth))
                throw new Exception($"值 {auth} 为无效的鉴权码不是2的幂级");

            if (auth < 0 || auth > 4611686018427387904)
                throw new Exception($"鉴权值 {auth} 应大于 0 小于 4611686018427387904");

            long code = authCode & (~auth);
            return code;
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="authCode">鉴权码</param>
        /// <param name="auth">权限值（2的幂级）</param>
        /// <remarks>auth == (authCode & auth)</remarks>
        /// <returns></returns>
        public static bool IsHasAuth(long authCode, long auth)
        {
            if (!IsBinPower(auth))
                throw new Exception($"值 {auth} 为无效的鉴权码不是2的幂级");

            if (authCode <= 0 || auth <= 0)
                return false;

            return auth == (authCode & auth);
        }
    }
}