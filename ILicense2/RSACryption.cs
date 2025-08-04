using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
 

namespace ILicense2
{
    /// <summary> 
    /// RSA加密解密及RSA签名和验证
    /// </summary> 
    public static class RSACryption
    {
        //RSA 的密钥产生 产生私钥 和公钥 
        public static void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }

        //RSA的加密函数
        public static string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {
            byte[] PlainTextBArray = Encoding.UTF8.GetBytes(m_strEncryptString);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);

            int bufferSize = (rsa.KeySize / 8) - 11;//单块最大长度
            var buffer = new byte[bufferSize];
            using (MemoryStream inputStream = new MemoryStream(PlainTextBArray), outputStream = new MemoryStream())
            {
                while (true)
                {
                    //分段加密
                    int readSize = inputStream.Read(buffer, 0, bufferSize);
                    if (readSize <= 0)
                    {
                        break;
                    }
                    var temp = new byte[readSize];
                    Array.Copy(buffer, 0, temp, 0, readSize);
                    var encryptedBytes = rsa.Encrypt(temp, false);
                    outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                }
                rsa.Dispose();
                return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
            }
        }

        //RSA的加密函数
        public static string RSAEncrypt(string xmlPublicKey, byte[] EncryptString)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);

            int bufferSize = (rsa.KeySize / 8) - 11;//单块最大长度
            var buffer = new byte[bufferSize];
            using (MemoryStream inputStream = new MemoryStream(EncryptString), outputStream = new MemoryStream())
            {
                while (true)
                {
                    //分段加密
                    int readSize = inputStream.Read(buffer, 0, bufferSize);
                    if (readSize <= 0)
                    {
                        break;
                    }
                    var temp = new byte[readSize];
                    Array.Copy(buffer, 0, temp, 0, readSize);
                    var encryptedBytes = rsa.Encrypt(temp, false);
                    outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                }
                rsa.Dispose();
                return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
            }
        }

        //RSA的解密函数
        public static string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            byte[] PlainTextBArray = Convert.FromBase64String(m_strDecryptString);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);

            int bufferSize = rsa.KeySize / 8;
            var buffer = new byte[bufferSize];
            using (MemoryStream inputStream = new MemoryStream(PlainTextBArray), outputStream = new MemoryStream())
            {
                while (true)
                {
                    int readSize = inputStream.Read(buffer, 0, bufferSize);
                    if (readSize <= 0)
                    {
                        break;
                    }
                    var temp = new byte[readSize];
                    Array.Copy(buffer, 0, temp, 0, readSize);
                    var rawBytes = rsa.Decrypt(temp, false);
                    outputStream.Write(rawBytes, 0, rawBytes.Length);
                }
                rsa.Dispose();
                return Encoding.UTF8.GetString(outputStream.ToArray());
            }
        }

        //RSA的解密函数
        public static string RSADecrypt(string xmlPrivateKey, byte[] DecryptString)
        {
            //byte[] PlainTextBArray = Convert.FromBase64String(m_strDecryptString);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);

            int bufferSize = rsa.KeySize / 8;
            var buffer = new byte[bufferSize];
            using (MemoryStream inputStream = new MemoryStream(DecryptString), outputStream = new MemoryStream())
            {
                while (true)
                {
                    int readSize = inputStream.Read(buffer, 0, bufferSize);
                    if (readSize <= 0)
                    {
                        break;
                    }
                    var temp = new byte[readSize];
                    Array.Copy(buffer, 0, temp, 0, readSize);
                    var rawBytes = rsa.Decrypt(temp, false);
                    outputStream.Write(rawBytes, 0, rawBytes.Length);
                }
                rsa.Dispose();
                return Encoding.UTF8.GetString(outputStream.ToArray());
            }
        }

        //获取Hash描述表
        public static byte[] GetMD5Hash(string m_strSource)
        {
            //从字符串中取得Hash描述 
            byte[] Buffer;
            HashAlgorithm MD5 = HashAlgorithm.Create("MD5");
            Buffer = Encoding.UTF8.GetBytes(m_strSource);
            return MD5.ComputeHash(Buffer);
        }

        //SHA1签名和java中的SHA1withRSA加密是一样的
        public static byte[] GetSHA1Hash(string PlainText)
        {
            SHA1 sha1Hasher = SHA1.Create();
            byte[] data = sha1Hasher.ComputeHash(Encoding.UTF8.GetBytes(PlainText));
            return data;
        }

        //RSA签名
        public static byte[] RSASignData(string p_strKeyPrivate, byte[] HashbyteSignature)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            //RSAFormatter.SetHashAlgorithm("MD5");
            RSAFormatter.SetHashAlgorithm("SHA1");
            //执行签名 
            byte[] EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
            return EncryptedSignatureData;
        }

        //签名验证
        public static bool RSAVerifySign(string p_strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.FromXmlString(p_strKeyPublic);
            RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            //RSADeformatter.SetHashAlgorithm("MD5");
            RSADeformatter.SetHashAlgorithm("SHA1");
            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //先记下了，将来可能有用
        //https://social.msdn.microsoft.com/Forums/zh-CN/c39f2298-ffe3-48d9-ad1e-ababa122d229/sha1-with-rsa-in-c?forum=netfxbcl
        private static string GetRSAHash(string PlainText)
        {
            string ResultString = "";
            string DigitalCertificateName = "C=IE, O=ad, OU=1234567, CN=f";
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            X509CertificateCollection collection = store.Certificates;
            foreach (System.Security.Cryptography.X509Certificates.X509Certificate cert in collection)
            {
                if (cert.Subject == DigitalCertificateName)
                {
                    CspParameters CspParam;
                    string publicXmlString = string.Empty;
                    string privateXmlString = string.Empty;
                    RSACryptoServiceProvider RsaCsp;
                    RSACryptoServiceProvider RsaCsp2;
                    UnicodeEncoding ByteConverter = new UnicodeEncoding();
                    CspParam = new CspParameters();
                    CspParam.KeyContainerName = cert.Subject; ;
                    CspParam.Flags = CspProviderFlags.UseMachineKeyStore;
                    byte[] encryptedString = ByteConverter.GetBytes(PlainText);
                    RsaCsp = new RSACryptoServiceProvider(CspParam);
                    //Get private key
                    privateXmlString = RsaCsp.ToXmlString(true);
                    RsaCsp2 = new RSACryptoServiceProvider();
                    RsaCsp2.FromXmlString(privateXmlString);
                    encryptedString = RsaCsp2.Encrypt(System.Text.Encoding.Unicode.GetBytes(PlainText), false);
                    ResultString = Convert.ToBase64String(encryptedString);
                }
            }
            store.Close();
            return ResultString;
        }

    }

    
}