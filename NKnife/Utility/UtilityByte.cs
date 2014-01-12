namespace NKnife.Utility
{
    public class UtilityByte
    {
        public static int MakeInt(byte[] bs)
        {
            byte b3 = bs[0];
            byte b2 = bs[1];
            byte b1 = bs[2];
            byte b0 = bs[3];
            return (int)((((b3 & 0xff) << 24) | ((b2 & 0xff) << 16) | ((b1 & 0xff) << 8) | ((b0 & 0xff) << 0)));
        } 
    }
}
