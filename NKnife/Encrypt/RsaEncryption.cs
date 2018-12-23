using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NKnife.Encrypt
{
    /// <summary>
    ///     RSA���ܽ��ܼ�RSAǩ������֤
    /// </summary>
    public class RsaEncryption
    {
        #region RSA ���ܽ���

        #region RSA ����Կ����

        /// <summary>
        ///     RSA ����Կ���� ����˽Կ �͹�Կ
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

        #region RSA �ļ��ܺ���

        //############################################################################## 
        //RSA ��ʽ���� 
        //˵��KEY������XML����ʽ,���ص����ַ��� 
        //����һ����Ҫ˵�������ü��ܷ�ʽ�� ���� ���Ƶģ��� 
        //############################################################################## 

        /// <summary>
        ///     RSA�ļ��ܺ���  string
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
        ///     RSA�ļ��ܺ��� byte[]
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

        #region RSA �Ľ��ܺ���

        /// <summary>
        ///     RSA�Ľ��ܺ���  string
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
        ///     RSA�Ľ��ܺ���  byte
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

        #region RSA ����ǩ��

        #region ��ȡHash������

        /// <summary>
        ///     ��ȡHash������
        /// </summary>
        public bool GetHash(string mStrSource, ref byte[] hashData)
        {
            //���ַ�����ȡ��Hash���� 
            byte[] buffer;
            var md5 = HashAlgorithm.Create("MD5");
            buffer = Encoding.GetEncoding("GB2312").GetBytes(mStrSource);
            hashData = md5.ComputeHash(buffer);

            return true;
        }

        /// <summary>
        ///     ��ȡHash������
        /// </summary>
        public bool GetHash(string mStrSource, ref string strHashData)
        {
            //���ַ�����ȡ��Hash���� 
            byte[] buffer;
            byte[] hashData;
            var md5 = HashAlgorithm.Create("MD5");
            buffer = Encoding.GetEncoding("GB2312").GetBytes(mStrSource);
            hashData = md5.ComputeHash(buffer);

            strHashData = Convert.ToBase64String(hashData);
            return true;
        }

        /// <summary>
        ///     ��ȡHash������
        /// </summary>
        public bool GetHash(FileStream objFile, ref byte[] hashData)
        {
            //���ļ���ȡ��Hash���� 
            var md5 = HashAlgorithm.Create("MD5");
            hashData = md5.ComputeHash(objFile);
            objFile.Close();

            return true;
        }

        /// <summary>
        ///     ��ȡHash������
        /// </summary>
        public bool GetHash(FileStream objFile, ref string strHashData)
        {
            //���ļ���ȡ��Hash���� 
            byte[] hashData;
            var md5 = HashAlgorithm.Create("MD5");
            hashData = md5.ComputeHash(objFile);
            objFile.Close();

            strHashData = Convert.ToBase64String(hashData);

            return true;
        }

        #endregion

        #region RSA ǩ��

        /// <summary>
        ///     RSAǩ��
        /// </summary>
        public bool SignatureFormatter(string pStrKeyPrivate, byte[] hashbyteSignature, ref byte[] encryptedSignatureData)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(pStrKeyPrivate);
            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            //����ǩ�����㷨ΪMD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            return true;
        }

        /// <summary>
        ///     RSAǩ��
        /// </summary>
        public bool SignatureFormatter(string pStrKeyPrivate, byte[] hashbyteSignature, ref string mStrEncryptedSignatureData)
        {
            byte[] encryptedSignatureData;

            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(pStrKeyPrivate);
            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            //����ǩ�����㷨ΪMD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);
            mStrEncryptedSignatureData = Convert.ToBase64String(encryptedSignatureData);

            return true;
        }

        /// <summary>
        ///     RSAǩ��
        /// </summary>
        public bool SignatureFormatter(string pStrKeyPrivate, string mStrHashbyteSignature, ref byte[] encryptedSignatureData)
        {
            byte[] hashbyteSignature;

            hashbyteSignature = Convert.FromBase64String(mStrHashbyteSignature);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(pStrKeyPrivate);
            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            //����ǩ�����㷨ΪMD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            return true;
        }

        /// <summary>
        ///     RSAǩ��
        /// </summary>
        public bool SignatureFormatter(string pStrKeyPrivate, string mStrHashbyteSignature, ref string mStrEncryptedSignatureData)
        {
            byte[] hashbyteSignature;
            byte[] encryptedSignatureData;

            hashbyteSignature = Convert.FromBase64String(mStrHashbyteSignature);

            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(pStrKeyPrivate);
            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            //����ǩ�����㷨ΪMD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);
            mStrEncryptedSignatureData = Convert.ToBase64String(encryptedSignatureData);

            return true;
        }

        #endregion

        #region RSA ǩ����֤

        public bool SignatureDeformatter(string pStrKeyPublic, byte[] hashbyteDeformatter, byte[] deformatterData)
        {
            var rsa = new RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
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
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
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
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
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
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
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