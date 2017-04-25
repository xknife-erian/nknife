using System;

namespace NKnife.App.CharMatrix.Outline
{
    public static class ConvertUtil
    {
        /// <summary>
        /// Fixed转单精度
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float FixedToFloat(FIXED val)
        {
            float f = val.value + (float)val.fract / 0xFFFF;
            return f;
        }

        /// <summary>
        /// 单精度转Fixed
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static FIXED FloatToFixed(float val)
        {
            FIXED fix = new FIXED();
            fix.value = (short)val;
            long l = (long)Math.Round(val * 0xFFFF, MidpointRounding.AwayFromZero);
            fix.fract = (ushort)(l % 0xFFFF);
            return fix;
        }
    }
}
