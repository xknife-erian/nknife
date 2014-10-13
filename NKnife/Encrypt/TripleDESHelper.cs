using System;
using System.Security.Cryptography;
using System.Text;

namespace NKnife.Encrypt
{
    public class TripleDESHelper
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="toEncrypt">Ҫ���ܵ��ַ�����������</param>
        /// <param name="key">������Կ</param>
        /// <param name="useHashing">�Ƿ�ʹ��MD5���ɻ�����Կ</param>
        /// <returns>���ܺ���ַ�����������</returns>
        public static string Encrypt(string toEncrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = Encoding.UTF8.GetBytes(key);

            var tdes = new TripleDESCryptoServiceProvider
                           {
                               Key = keyArray, 
                               Mode = CipherMode.ECB, 
                               Padding = PaddingMode.PKCS7
                           };

            var cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="toDecrypt">Ҫ���ܵ��ַ�����������</param>
        /// <param name="key">������Կ</param>
        /// <param name="useHashing">�Ƿ�ʹ��MD5���ɻ�����Կ</param>
        /// <returns>���ܺ���ַ�����������</returns>
        public static string Decrypt(string toDecrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = Encoding.UTF8.GetBytes(key);

            var tdes = new TripleDESCryptoServiceProvider
                           {
                               Key = keyArray,
                               Mode = CipherMode.ECB,
                               Padding = PaddingMode.PKCS7
                           };

            var cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}