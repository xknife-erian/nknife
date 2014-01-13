using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gean.Utility.Encrypt
{
    public class FileMD5
    {
        #region MD5签名验证

        /// <summary>
        /// 对给定文件路径的文件加上标签
        /// </summary>
        /// <param name="path">要加密的文件的路径</param>
        /// <returns>标签的值</returns>
        public static bool AddMD5(string path)
        {
            bool IsNeed = true;

            if (CheckMD5(path))//已进行MD5处理
                IsNeed = false;
            try
            {
                FileStream fsRead = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] md5File = new byte[fsRead.Length];
                fsRead.Read(md5File, 0, (int)fsRead.Length);// 将文件流读取到Buffer中
                fsRead.Close();

                if (IsNeed)
                {
                    string result = MD5Buffer(md5File, 0, md5File.Length);// 对Buffer中的字节内容算MD5
                    byte[] md5 = System.Text.Encoding.ASCII.GetBytes(result);// 将字符串转换成字节数组以便写人到文件中
                    FileStream fsWrite = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                    fsWrite.Write(md5File, 0, md5File.Length);// 将文件，MD5值 重新写入到文件中。
                    fsWrite.Write(md5, 0, md5.Length);
                    fsWrite.Close();
                }
                else
                {
                    FileStream fsWrite = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                    fsWrite.Write(md5File, 0, md5File.Length);
                    fsWrite.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 对给定路径的文件进行验证
        /// </summary>
        /// <param name="path"></param>
        /// <returns>是否加了标签或是否标签值与内容值一致</returns>
        public static bool CheckMD5(string path)
        {
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] md5File = new byte[fileStream.Length]; // 读入文件
                fileStream.Read(md5File, 0, (int)fileStream.Length);
                fileStream.Close();

                string result = MD5Buffer(md5File, 0, md5File.Length - 32); // 对文件除最后32位以外的字节计算MD5，这个32是因为标签位为32位。
                string md5 = Encoding.ASCII.GetString(md5File, md5File.Length - 32, 32);   //读取文件最后32位，其中保存的就是MD5值
                return result == md5;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="md5File">MD5签名文件字符数组</param>
        /// <param name="index">计算起始位置</param>
        /// <param name="count">计算终止位置</param>
        /// <returns>计算结果</returns>
        private static string MD5Buffer(byte[] md5File, int index, int count)
        {
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashByte = md5.ComputeHash(md5File, index, count);
            string result = System.BitConverter.ToString(hashByte);

            result = result.Replace("-", "");
            return result;
        }
        
        #endregion
    }
}
