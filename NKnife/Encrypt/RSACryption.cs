using System;
using System.Security.Cryptography;
using System.Text;

namespace NKnife.Encrypt
{
    /// <summary> 
    /// RSA���ܽ��ܼ�RSAǩ������֤
    /// </summary> 
    public class RsaCryption
    {

        #region RSA ���ܽ���

        #region RSA ����Կ����

        /// <summary>
        /// RSA ����Կ���� ����˽Կ �͹�Կ 
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

        #region RSA �ļ��ܺ���
        //############################################################################## 
        //RSA ��ʽ���� 
        //˵��KEY������XML����ʽ,���ص����ַ��� 
        //����һ����Ҫ˵�������ü��ܷ�ʽ�� ���� ���Ƶģ��� 
        //############################################################################## 

        //RSA�ļ��ܺ���  string
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
        //RSA�ļ��ܺ��� byte[]
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

        #region RSA �Ľ��ܺ���
        //RSA�Ľ��ܺ���  string
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

        //RSA�Ľ��ܺ���  byte
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

        #region RSA ����ǩ��

        #region ��ȡHash������
        //��ȡHash������ 
        public bool GetHash(string mStrSource, ref byte[] hashData)
        {
            //���ַ�����ȡ��Hash���� 
            byte[] buffer;
            System.Security.Cryptography.HashAlgorithm md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(mStrSource);
            hashData = md5.ComputeHash(buffer);

            return true;
        }

        //��ȡHash������ 
        public bool GetHash(string mStrSource, ref string strHashData)
        {

            //���ַ�����ȡ��Hash���� 
            byte[] buffer;
            byte[] hashData;
            System.Security.Cryptography.HashAlgorithm md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(mStrSource);
            hashData = md5.ComputeHash(buffer);

            strHashData = Convert.ToBase64String(hashData);
            return true;

        }

        //��ȡHash������ 
        public bool GetHash(System.IO.FileStream objFile, ref byte[] hashData)
        {

            //���ļ���ȡ��Hash���� 
            System.Security.Cryptography.HashAlgorithm md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            hashData = md5.ComputeHash(objFile);
            objFile.Close();

            return true;

        }

        //��ȡHash������ 
        public bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {

            //���ļ���ȡ��Hash���� 
            byte[] hashData;
            System.Security.Cryptography.HashAlgorithm md5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            hashData = md5.ComputeHash(objFile);
            objFile.Close();

            strHashData = Convert.ToBase64String(hashData);

            return true;

        }
        #endregion

        #region RSA ǩ��
        //RSAǩ�� 
        public bool SignatureFormatter(string pStrKeyPrivate, byte[] hashbyteSignature, ref byte[] encryptedSignatureData)
        {

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter rsaFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(rsa);
            //����ǩ�����㷨ΪMD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            return true;

        }

        //RSAǩ�� 
        public bool SignatureFormatter(string pStrKeyPrivate, byte[] hashbyteSignature, ref string mStrEncryptedSignatureData)
        {

            byte[] encryptedSignatureData;

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter rsaFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(rsa);
            //����ǩ�����㷨ΪMD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            mStrEncryptedSignatureData = Convert.ToBase64String(encryptedSignatureData);

            return true;

        }

        //RSAǩ�� 
        public bool SignatureFormatter(string pStrKeyPrivate, string mStrHashbyteSignature, ref byte[] encryptedSignatureData)
        {

            byte[] hashbyteSignature;

            hashbyteSignature = Convert.FromBase64String(mStrHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter rsaFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(rsa);
            //����ǩ�����㷨ΪMD5 
            rsaFormatter.SetHashAlgorithm("MD5");
            //ִ��ǩ�� 
            encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);

            return true;

        }

        //RSAǩ�� 
        public bool SignatureFormatter(string pStrKeyPrivate, string mStrHashbyteSignature, ref string mStrEncryptedSignatureData)
        {

            byte[] hashbyteSignature;
            byte[] encryptedSignatureData;

            hashbyteSignature = Convert.FromBase64String(mStrHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter rsaFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(rsa);
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

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();

            rsa.FromXmlString(pStrKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter rsaDeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(rsa);
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
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
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
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
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
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
            //ָ�����ܵ�ʱ��HASH�㷨ΪMD5 
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
