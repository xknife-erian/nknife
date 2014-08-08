namespace NKnife.Zip
{
    /// <summary>
    ///     简单加密类
    /// </summary>
    class SimpleCipher
    {
        private static readonly byte[] _XorVector = {8, 3, 6, 1, 0, 9};

        public static void EncryptBytes(byte[] byteArray)
        {
            int k = 0;
            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] ^= _XorVector[k];
                k++;
                k = k%_XorVector.Length;
            }
        }
    }
}