using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NKnife.Util
{
    /// <summary>
    ///     针对加解密方法的扩展
    ///     2009-12-17 17:56:04
    /// </summary>
    public class EncryptUtil
    {
        /// <summary>
        ///     本框架设定的缺省密钥字符串，只读状态。一般不建议使用。
        /// </summary>
        public static readonly string DefaultKey = "98$Mp6?W";

        /// <summary>
        ///     默认密钥向量
        /// </summary>
        private static readonly byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        ///     自动生成一个密钥。
        /// </summary>
        /// <returns>返回生成的密钥</returns>
        public static string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.  
            var desCrypto = (DESCryptoServiceProvider)DES.Create();
            // Use the Automatically generated key for Encryption.  
            return Encoding.ASCII.GetString(desCrypto.Key);
        }

        /// <summary>
        ///     计算输入数据的 MD5 哈希值
        /// </summary>
        /// <param name="strIn">输入的数据.</param>
        /// <returns></returns>
        public static string Md5Encrypt(string strIn)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var tmpByte = md5.ComputeHash(StringToBytes(strIn));
            md5.Clear();

            return BytesToString(tmpByte);
        }

        /// <summary>
        ///     计算输入数据的 SHA1 哈希值
        /// </summary>
        /// <param name="strIn">输入的数据.</param>
        /// <returns></returns>
        public static string Sha1Encrypt(string strIn)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            var tmpByte = sha1.ComputeHash(StringToBytes(strIn));
            sha1.Clear();

            return BytesToString(tmpByte);
        }

        /// <summary>
        ///     计算输入数据的 SHA256 哈希值
        /// </summary>
        /// <param name="strIn">输入的数据.</param>
        /// <returns></returns>
        public static string Sha256Encrypt(string strIn)
        {
            //string strIN = getstrIN(strIN);
            SHA256 sha256 = new SHA256Managed();

            var tmpByte = sha256.ComputeHash(StringToBytes(strIn));
            sha256.Clear();

            return BytesToString(tmpByte);
        }

        /// <summary>
        ///     计算输入数据的 SHA512 哈希值
        /// </summary>
        /// <param name="strIn">输入的数据.</param>
        /// <returns></returns>
        public static string Sha512Encrypt(string strIn)
        {
            //string strIN = getstrIN(strIN);
            SHA512 sha512 = new SHA512Managed();

            var tmpByte = sha512.ComputeHash(StringToBytes(strIn));
            sha512.Clear();

            return BytesToString(tmpByte);
        }

        /// <summary>
        ///     将字节型数组转换成数字
        /// </summary>
        /// <param name="byteValue">The byte value.</param>
        /// <returns></returns>
        private static string BytesToString(byte[] byteValue)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < byteValue.Length; i++)
            {
                sb.Append(byteValue[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        ///     将Key字符串转换为字节型数组
        /// </summary>
        /// <param name="strKey">The STR key.</param>
        /// <returns></returns>
        private static byte[] StringToBytes(string strKey)
        {
            var asciiEncoding = new UTF8Encoding();
            return asciiEncoding.GetBytes(strKey);
        }

        /// <summary>
        ///     获取用DES方法加密 [一个指定的待加密的字符串] 后的字符串
        /// </summary>
        /// <param name="encryptString">一个指定的待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串,失败返回源串</returns>
        public static string DesEncrypt(string encryptString, string encryptKey)
        {
            encryptKey = StringUtil.GetSubString(encryptKey, 0, 8, "");
            encryptKey = encryptKey.PadRight(8, ' ');
            var rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            var rgbIv = Keys;
            var inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            var dCsp = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        ///     DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
        public static string DesDecrypt(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = StringUtil.GetSubString(decryptKey, 0, 8, "");
                decryptKey = decryptKey.PadRight(8, ' ');
                var rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                var rgbIv = Keys;
                var inputByteArray = Convert.FromBase64String(decryptString);
                var dcsp = new DESCryptoServiceProvider();

                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        /// <summary>
        ///     加密文件
        /// </summary>
        /// <param name="inputFilename">要加密的文件</param>
        /// <param name="outputFilename">加密后保存的文件</param>
        /// <param name="key">密钥</param>
        public static void EncryptFile(string inputFilename, string outputFilename, string key)
        {
            using var fsInput = new FileStream(inputFilename, FileMode.Open, FileAccess.Read);

            var buffer = new byte[fsInput.Length];
            var read   = fsInput.Read(buffer, 0, buffer.Length);
            fsInput.Close();

            var fsEncrypted = new FileStream(outputFilename,
                                             FileMode.OpenOrCreate,
                                             FileAccess.Write);
            var des = new DESCryptoServiceProvider();

            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV  = Encoding.ASCII.GetBytes(key);

            var desEncrypt   = des.CreateEncryptor();
            var cryptoStream = new CryptoStream(fsEncrypted, desEncrypt, CryptoStreamMode.Write);
            cryptoStream.Write(buffer, 0, buffer.Length);
            cryptoStream.Close();
            fsEncrypted.Close();
        }

        /// <summary>
        ///     解密文件
        /// </summary>
        /// <param name="inputFilename">要加密的文件</param>
        /// <param name="outputFilename">加密后保存的文件</param>
        /// <param name="key">密钥</param>
        public static void DecryptFile(string inputFilename, string outputFilename, string key)
        {
            var des = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.  
            //Set secret key For DES algorithm.  
            des.Key = Encoding.ASCII.GetBytes(key);
            //Set initialization vector.  
            des.IV = Encoding.ASCII.GetBytes(key);

            //Create a file stream to read the encrypted file back.  
            using (var fsread = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                //Create a DES decryptor from the DES instance.  
                var desdecrypt = des.CreateDecryptor();
                //Create crypto stream set to read and do a  
                //DES decryption transform on incoming bytes.  
                var cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);
                //Print the contents of the decrypted file.  
                var fsDecrypted = new StreamWriter(outputFilename);
                fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
                fsDecrypted.Flush();
                fsDecrypted.Close();
            }
        }

        /// <summary>
        ///     解密文件
        /// </summary>
        /// <param name="inputFilename">要加密的文件</param>
        /// <param name="key">密钥</param>
        /// <returns>返回内容</returns>
        public static string DecryptFile(string inputFilename, string key)
        {
            var des = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.  
            //Set secret key For DES algorithm.  
            des.Key = Encoding.ASCII.GetBytes(key);
            //Set initialization vector.  
            des.IV = Encoding.ASCII.GetBytes(key);

            //Create a file stream to read the encrypted file back.  
            using (var fsread = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                var byt = new byte[fsread.Length];
                fsread.Read(byt, 0, byt.Length);
                fsread.Flush();
                fsread.Close();
                //Create a DES decryptor from the DES instance.  
                var desdecrypt = des.CreateDecryptor();
                var ms = new MemoryStream();
                var cryptostreamDecr = new CryptoStream(ms, desdecrypt, CryptoStreamMode.Write);
                cryptostreamDecr.Write(byt, 0, byt.Length);
                cryptostreamDecr.FlushFinalBlock();
                cryptostreamDecr.Close();
                return Encoding.UTF8.GetString(ms.ToArray()).Trim();
            }
        }
    }
}