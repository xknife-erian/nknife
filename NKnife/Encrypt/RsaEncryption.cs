using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NKnife.Encrypt
{
    /// <summary>
    ///     RSA加密解密及RSA签名和验证
    /// </summary>
    public class RsaEncryption
    {
        #region RSA 加密解密

        #region RSA 的密钥产生

        /// <summary>
        ///     RSA 的密钥产生 产生私钥 和公钥
        /// </summary>
        /// <param name="xmlKeys"></param>
        /// <param name="xmlPublicKey"></param>
        public void RsaKey(out string xmlKeys, out string xmlPublicKey)
        {
            var rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }

        #endregion

        #region RSA 的加密函数

        //############################################################################## 
        //RSA 方式加密 
        //说明KEY必须是XML的行式,返回的是字符串 
        //在有一点需要说明！！该加密方式有 长度 限制的！！ 
        //############################################################################## 

        /// <summary>
        ///     RSA的加密函数  string
        /// </summary>
        public string RsaEncrypt(string xmlPublicKey, string mStrEncryptString)
        {
            byte[] plainTextBArray;
            byte[] cypherTextBArray;
            string result;
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            plainTextBArray = new UnicodeEncoding().GetBytes(mStrEncryptString);
            cypherTextBArray = rsa.Encrypt(plainTextBArray, false);
            result = Convert.ToBase64String(cypherTextBArray);
            return result;
        }

        /// <summary>
        ///     RSA的加密函数 byte[]
        /// </summary>
        public string RsaEncrypt(string xmlPublicKey, byte[] encryptString)
        {
            byte[] cypherTextBArray;
            string result;
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            cypherTextBArray = rsa.Encrypt(encryptString, false);
            result = Convert.ToBase64String(cypherTextBArray);
            return result;
        }

        #endregion

        #region RSA 的解密函数

        /// <summary>
        ///     RSA的解密函数  string
        /// </summary>
        public string RsaDecrypt(string xmlPrivateKey, string mStrDecryptString)
        {
            byte[] plainTextBArray;
            byte[] dypherTextBArray;
            string result;
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            plainTextBArray = Convert.FromBase64String(mStrDecryptString);
            dypherTextBArray = rsa.Decrypt(plainTextBArray, false);
            result = new UnicodeEncoding().GetString(dypherTextBArray);
            return result;
        }

        /// <summary>
        ///     RSA的解密函数  byte
        /// </summary>
        public string RsaDecrypt(string xmlPrivateKey, byte[] decryptString)
        {
            byte[] dypherTextBArray;
            string result;
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            dypherTextBArray = rsa.Decrypt(decryptString, false);
            result = new UnicodeEncoding().GetString(dypherTextBArray);
            return result;
        }

        #endregion

        #endregion

        #region RSA 数字签名

        #region 获取Hash描述表

        /// <summary>
        ///     获取Hash描述表
        /// </summary>
        public bool GetHash(string mStrSource, ref byte[] hashData)
        {
            //从字符串中取得Hash描述 
            byte[] buffer;
            var md5 = HashAlgorithm.Create("MD5");
            buffer = Encoding.GetEncoding("GB2312").GetBytes(mStrSource);
            hashData = md5.ComputeHash(buffer);

            return true;
        }

        /// <summary>
        ///     获取Hash描述表
        /// </summary>
        public bool GetHash(string mStrSource, ref string strHashData)
        {
            //从字符串中取得Hash描述 
            byte[] buffer;
            byte[] hashData;
            var md5 = HashAlgorithm.Create("MD5");
            buffer = Encoding.GetEncoding("GB2312").GetBytes(mStrSource);
            hashData = md5.ComputeHash(buffer);

            strHashData = Convert.ToBase64String(hashData);
            return true;
        }

        /// <summary>
        ///     获取Hash描述表
        /// </summary>
        public bool GetHash(FileStream objFile, ref byte[] hashData)
        {
            //从文件中取得Hash描述 
            var md5 = HashAlgorithm.Create("MD5");
            hashData = md5.ComputeHash(objFile);
            objFile.Close();

            return true;
        }

        /// <summary>
        ///     获取Hash描述表
        /// </summary>
        public bool GetHash(FileStream objFile, ref string strHashData)
        {
            //从文件中取得Hash描述 
            byte[] hashData;
            var md5 = HashAlgorithm.Create("MD5");
            hashData = md5.ComputeHash(objFile);
            objFile.Close();

            strHashData = Convert.ToBase64String(hashData);

            return true;
        }

        #endregion

        #region RSA 签名

        /// <summary>
        ///     RSA签名
        /// </summary>
        public bool SignatureFormatter(string pStrKeyPrivate, byte[] hashbyteSignature, ref byte[] encryptedSignatureData)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(pStrKeyPrivate);
            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            //设置签名的算法为MD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            return true;
        }

        /// <summary>
        ///     RSA签名
        /// </summary>
        public bool SignatureFormatter(string pStrKeyPrivate, byte[] hashbyteSignature, ref string mStrEncryptedSignatureData)
        {
            byte[] encryptedSignatureData;

            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(pStrKeyPrivate);
            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            //设置签名的算法为MD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);
            mStrEncryptedSignatureData = Convert.ToBase64String(encryptedSignatureData);

            return true;
        }

        /// <summary>
        ///     RSA签名
        /// </summary>
        public bool SignatureFormatter(string pStrKeyPrivate, string mStrHashbyteSignature, ref byte[] encryptedSignatureData)
        {
            byte[] hashbyteSignature;

            hashbyteSignature = Convert.FromBase64String(mStrHashbyteSignature);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(pStrKeyPrivate);
            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            //设置签名的算法为MD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            return true;
        }

        /// <summary>
        ///     RSA签名
        /// </summary>
        public bool SignatureFormatter(string pStrKeyPrivate, string mStrHashbyteSignature, ref string mStrEncryptedSignatureData)
        {
            byte[] hashbyteSignature;
            byte[] encryptedSignatureData;

            hashbyteSignature = Convert.FromBase64String(mStrHashbyteSignature);

            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(pStrKeyPrivate);
            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            //设置签名的算法为MD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);
            mStrEncryptedSignatureData = Convert.ToBase64String(encryptedSignatureData);

            return true;
        }

        #endregion

        #region RSA 签名验证

        public bool SignatureDeformatter(string pStrKeyPublic, byte[] hashbyteDeformatter, byte[] deformatterData)
        {
            var rsa = new RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            //指定解密的时候HASH算法为MD5 
            rsaDeformatter.SetHashAlgorithm("MD5");

            if (rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData))
                return true;
            return false;
        }

        public bool SignatureDeformatter(string pStrKeyPublic, string pStrHashbyteDeformatter, byte[] deformatterData)
        {
            byte[] hashbyteDeformatter;

            hashbyteDeformatter = Convert.FromBase64String(pStrHashbyteDeformatter);

            var rsa = new RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            //指定解密的时候HASH算法为MD5 
            rsaDeformatter.SetHashAlgorithm("MD5");

            if (rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData))
                return true;
            return false;
        }

        public bool SignatureDeformatter(string pStrKeyPublic, byte[] hashbyteDeformatter, string pStrDeformatterData)
        {
            byte[] deformatterData;

            var rsa = new RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            //指定解密的时候HASH算法为MD5 
            rsaDeformatter.SetHashAlgorithm("MD5");

            deformatterData = Convert.FromBase64String(pStrDeformatterData);

            if (rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData))
                return true;
            return false;
        }

        public bool SignatureDeformatter(string pStrKeyPublic, string pStrHashbyteDeformatter, string pStrDeformatterData)
        {
            byte[] deformatterData;
            byte[] hashbyteDeformatter;

            hashbyteDeformatter = Convert.FromBase64String(pStrHashbyteDeformatter);
            var rsa = new RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            //指定解密的时候HASH算法为MD5 
            rsaDeformatter.SetHashAlgorithm("MD5");

            deformatterData = Convert.FromBase64String(pStrDeformatterData);

            if (rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData))
                return true;
            return false;
        }

        #endregion

        #endregion
    }
}