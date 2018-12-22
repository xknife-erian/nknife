using System;
using System.Security.Cryptography;
using System.Text;

namespace NKnife.Encrypt
{
    /// <summary> 
    /// RSA加密解密及RSA签名和验证
    /// </summary> 
    public class RsaCryption
    {

        #region RSA 加密解密

        #region RSA 的密钥产生

        /// <summary>
        /// RSA 的密钥产生 产生私钥 和公钥 
        /// </summary>
        /// <param name="xmlKeys"></param>
        /// <param name="xmlPublicKey"></param>
        public void RsaKey(out string xmlKeys, out string xmlPublicKey)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
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

        //RSA的加密函数  string
        public string RsaEncrypt(string xmlPublicKey, string mStrEncryptString)
        {

            byte[] plainTextBArray;
            byte[] cypherTextBArray;
            string result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            plainTextBArray = (new UnicodeEncoding()).GetBytes(mStrEncryptString);
            cypherTextBArray = rsa.Encrypt(plainTextBArray, false);
            result = Convert.ToBase64String(cypherTextBArray);
            return result;

        }
        //RSA的加密函数 byte[]
        public string RsaEncrypt(string xmlPublicKey, byte[] encryptString)
        {

            byte[] cypherTextBArray;
            string result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            cypherTextBArray = rsa.Encrypt(encryptString, false);
            result = Convert.ToBase64String(cypherTextBArray);
            return result;

        }
        #endregion

        #region RSA 的解密函数
        //RSA的解密函数  string
        public string RsaDecrypt(string xmlPrivateKey, string mStrDecryptString)
        {
            byte[] plainTextBArray;
            byte[] dypherTextBArray;
            string result;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            plainTextBArray = Convert.FromBase64String(mStrDecryptString);
            dypherTextBArray = rsa.Decrypt(plainTextBArray, false);
            result = (new UnicodeEncoding()).GetString(dypherTextBArray);
            return result;

        }

        //RSA的解密函数  byte
        public string RsaDecrypt(string xmlPrivateKey, byte[] decryptString)
        {
            byte[] dypherTextBArray;
            string result;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            dypherTextBArray = rsa.Decrypt(decryptString, false);
            result = (new UnicodeEncoding()).GetString(dypherTextBArray);
            return result;

        }
        #endregion

        #endregion

        #region RSA 数字签名

        #region 获取Hash描述表
        //获取Hash描述表 
        public bool GetHash(string mStrSource, ref byte[] hashData)
        {
            //从字符串中取得Hash描述 
            byte[] buffer;
            System.Security.Cryptography.HashAlgorithm md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(mStrSource);
            hashData = md5.ComputeHash(buffer);

            return true;
        }

        //获取Hash描述表 
        public bool GetHash(string mStrSource, ref string strHashData)
        {

            //从字符串中取得Hash描述 
            byte[] buffer;
            byte[] hashData;
            System.Security.Cryptography.HashAlgorithm md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(mStrSource);
            hashData = md5.ComputeHash(buffer);

            strHashData = Convert.ToBase64String(hashData);
            return true;

        }

        //获取Hash描述表 
        public bool GetHash(System.IO.FileStream objFile, ref byte[] hashData)
        {

            //从文件中取得Hash描述 
            System.Security.Cryptography.HashAlgorithm md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            hashData = md5.ComputeHash(objFile);
            objFile.Close();

            return true;

        }

        //获取Hash描述表 
        public bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {

            //从文件中取得Hash描述 
            byte[] hashData;
            System.Security.Cryptography.HashAlgorithm md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            hashData = md5.ComputeHash(objFile);
            objFile.Close();

            strHashData = Convert.ToBase64String(hashData);

            return true;

        }
        #endregion

        #region RSA 签名
        //RSA签名 
        public bool SignatureFormatter(string pStrKeyPrivate, byte[] hashbyteSignature, ref byte[] encryptedSignatureData)
        {

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter rsaFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(rsa);
            //设置签名的算法为MD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            return true;

        }

        //RSA签名 
        public bool SignatureFormatter(string pStrKeyPrivate, byte[] hashbyteSignature, ref string mStrEncryptedSignatureData)
        {

            byte[] encryptedSignatureData;

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter rsaFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(rsa);
            //设置签名的算法为MD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            mStrEncryptedSignatureData = Convert.ToBase64String(encryptedSignatureData);

            return true;

        }

        //RSA签名 
        public bool SignatureFormatter(string pStrKeyPrivate, string mStrHashbyteSignature, ref byte[] encryptedSignatureData)
        {

            byte[] hashbyteSignature;

            hashbyteSignature = Convert.FromBase64String(mStrHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter rsaFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(rsa);
            //设置签名的算法为MD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            return true;

        }

        //RSA签名 
        public bool SignatureFormatter(string pStrKeyPrivate, string mStrHashbyteSignature, ref string mStrEncryptedSignatureData)
        {

            byte[] hashbyteSignature;
            byte[] encryptedSignatureData;

            hashbyteSignature = Convert.FromBase64String(mStrHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter rsaFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(rsa);
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

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter rsaDeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(rsa);
            //指定解密的时候HASH算法为MD5 
            rsaDeformatter.SetHashAlgorithm("MD5");

            if (rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string pStrKeyPublic, string pStrHashbyteDeformatter, byte[] deformatterData)
        {

            byte[] hashbyteDeformatter;

            hashbyteDeformatter = Convert.FromBase64String(pStrHashbyteDeformatter);

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter rsaDeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(rsa);
            //指定解密的时候HASH算法为MD5 
            rsaDeformatter.SetHashAlgorithm("MD5");

            if (rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string pStrKeyPublic, byte[] hashbyteDeformatter, string pStrDeformatterData)
        {

            byte[] deformatterData;

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter rsaDeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(rsa);
            //指定解密的时候HASH算法为MD5 
            rsaDeformatter.SetHashAlgorithm("MD5");

            deformatterData = Convert.FromBase64String(pStrDeformatterData);

            if (rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string pStrKeyPublic, string pStrHashbyteDeformatter, string pStrDeformatterData)
        {

            byte[] deformatterData;
            byte[] hashbyteDeformatter;

            hashbyteDeformatter = Convert.FromBase64String(pStrHashbyteDeformatter);
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter rsaDeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(rsa);
            //指定解密的时候HASH算法为MD5 
            rsaDeformatter.SetHashAlgorithm("MD5");

            deformatterData = Convert.FromBase64String(pStrDeformatterData);

            if (rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        #endregion


        #endregion

    }
} 
