using ILicense2.Entity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ILicense2
{
    /// <summary>
    /// License管理类
    /// </summary>
    public sealed class LicenseManage
    {
        private const string LicensePath = "license.lic";
        static LicenseConfig config = new LicenseConfig();
        /// <summary>
        /// 设置配置
        /// </summary>
        /// <param name="config"></param>
        public static void SetLicenseConfig(LicenseConfig config)
        {
            LicenseManage.config = config;
        }
        /// <summary>
        /// 获取配置
        /// </summary>
        public static LicenseConfig GetLicenseConfig()
        {
            return config;
        }

        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        public static (string privatekey, string publickey) CreateRsaKey()
        {
            var rsaProvider = new RSACryptoServiceProvider(1024);
            var publicKey = rsaProvider.ToXmlString(false);
            //将RSA算法的私钥导出到字符串PrivateKey中，参数为true表示导出私钥
            var PrivateKey = rsaProvider.ToXmlString(true);
            return (privatekey: PrivateKey, publickey: publicKey);
        }


        ///// <summary>
        ///// RSA加密
        ///// </summary>
        ///// <param name="publickey"></param>
        ///// <param name="content"></param>
        ///// <returns></returns>
        //public static string RSAEncrypt(string publickey, string content)
        //{
        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
        //    byte[] cipherbytes;
        //    rsa.FromXmlString(publickey);
        //    cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);
        //    return Convert.ToBase64String(cipherbytes);
        //}

        ///// <summary>
        ///// RSA解密
        ///// </summary>
        ///// <param name="privatekey"></param>
        ///// <param name="content"></param>
        ///// <returns></returns>
        //public static string RSADecrypt(string privatekey, string content)
        //{
        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
        //    byte[] cipherbytes;
        //    rsa.FromXmlString(privatekey);
        //    cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);
        //    return Encoding.UTF8.GetString(cipherbytes);
        //}



        /// <summary>
        /// 分析License
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        static string AnalysisLicense(string key, string content)
        {

            string aeskey = "";
            try
            {
                return RSACryption.RSADecrypt(key,  content);
            }
            catch(Exception ex)
            {

            }
            return "";

          //  return (aeskey, content.Remove(0,index+1));
        }


        /// <summary>
        /// 获取注册密钥
        /// </summary>
        /// <returns></returns>
        public static string GetLicense(LicenseConfig lc, string registration, string data)
        {
            if (lc == null)
            {
                return "";
            }
            DateTime dt = DateTime.Now;
            if (lc.Due <= dt)
            {
                dt = lc.Due;
            }
            SecretKey secret = new SecretKey();
            secret.Data = data ?? "";
            //到期时间
            secret.Deadline = lc.Due.ToString("yyyyMMdd");
            //软件代号
            secret.SoftCode = lc.SoftCode;
            //注册码
            secret.RC = registration;
            //授权日期
            secret.SDT = dt.ToString("yyyyMMdd");
            secret.Customer = lc.CustomerName;
            string guid = Guid.NewGuid().ToString("N");
            secret.RandCode = guid.Substring(0, 10);
            string strsecret = Newtonsoft.Json.JsonConvert.SerializeObject(secret);

            string key = RSACryption.RSAEncrypt(lc.PublicKey,strsecret);
            //string content = AESEncrypt(strsecret, guid);

            return key;
        }


        ///// <summary>  
        ///// AES加密(无向量)  
        ///// </summary>  
        ///// <param name="plainBytes">被加密的明文</param>  
        ///// <param name="key">密钥</param>  
        ///// <returns>密文</returns>  
        //public static string AESEncrypt(String Data, String Key)
        //{
        //    MemoryStream mStream = new MemoryStream();
        //    RijndaelManaged aes = new RijndaelManaged();

        //    byte[] plainBytes = Encoding.UTF8.GetBytes(Data);
        //    Byte[] bKey = new Byte[32];
        //    Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);

        //    aes.Mode = CipherMode.ECB;
        //    aes.Padding = PaddingMode.PKCS7;
        //    aes.KeySize = 128;
        //    //aes.Key = _key;  
        //    aes.Key = bKey;
        //    //aes.IV = _iV;  
        //    CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        //    try
        //    {
        //        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
        //        cryptoStream.FlushFinalBlock();
        //        return Convert.ToBase64String(mStream.ToArray());
        //    }
        //    finally
        //    {
        //        cryptoStream.Close();
        //        mStream.Close();
        //        aes.Clear();
        //    }
        //}
        ///// <summary>  
        ///// AES解密(无向量)  
        ///// </summary>  
        ///// <param name="encryptedBytes">被加密的明文</param>  
        ///// <param name="key">密钥</param>  
        ///// <returns>明文</returns>  
        //public static string AESDecrypt(String Data, String Key)
        //{
        //    Byte[] encryptedBytes = Convert.FromBase64String(Data);
        //    Byte[] bKey = new Byte[32];
        //    Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);

        //    MemoryStream mStream = new MemoryStream(encryptedBytes);
        //    //mStream.Write( encryptedBytes, 0, encryptedBytes.Length );  
        //    //mStream.Seek( 0, SeekOrigin.Begin );  
        //    RijndaelManaged aes = new RijndaelManaged();
        //    aes.Mode = CipherMode.ECB;
        //    aes.Padding = PaddingMode.PKCS7;
        //    aes.KeySize = 128;
        //    aes.Key = bKey;
        //    //aes.IV = _iV;  
        //    CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        //    try
        //    {
        //        byte[] tmp = new byte[encryptedBytes.Length + 32];
        //        int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length + 32);
        //        byte[] ret = new byte[len];
        //        Array.Copy(tmp, 0, ret, 0, len);
        //        return Encoding.UTF8.GetString(ret);
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //    finally
        //    {
        //        cryptoStream.Close();
        //        mStream.Close();
        //        aes.Clear();
        //    }

        //}






        /// <summary>
        /// 获取序列号
        /// </summary>
        /// <returns></returns>
        public static string GetRegistration()
        {
            string key = "";
            key = MachineHelper.GetCpuID();
            if (string.IsNullOrWhiteSpace(key))
            {
                key = MachineHelper.GetDiskID();
            }
            key += MachineHelper.GetMotherBoardID();
            if (string.IsNullOrWhiteSpace(key))
            {
                key = MachineHelper.GetDiskID();
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                key = MachineHelper.GetMacAddress();
            }
            using (MD5 md5Hash = MD5.Create())
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    key = "1";
                }

                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(key));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }


        /// <summary>
        /// 授权检验
        /// </summary>
        /// <param name="license"></param>
        /// <returns></returns>
        public static (string result, SecretKey secretkey) Verify(string key,
                                                                 string softcode,
                                                                 string regcode,
                                                                 string license)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return ("软件严重故障03，请联系管理员处理。", null);
            }
            var   content = AnalysisLicense(key, license);

            if (string.IsNullOrWhiteSpace(content)  )
            {
                return ("密钥无效", null);
            }
            try
            {
               // content = AESDecrypt(content, aeskey);
                
                var secrekey = Newtonsoft.Json.JsonConvert.DeserializeObject<SecretKey>(content);
                if (secrekey == null)
                {
                    return ("密钥无效", null);
                }
                //权限校验
                if (softcode != secrekey.SoftCode)
                {
                    return ("此密钥不适用此版本软件", null);
                }
                var dt = GetDateTime();
                // 期限校验
                if (Convert.ToUInt64(dt) > Convert.ToUInt64(secrekey.Deadline))
                {
                    return ("密钥已过期。", secrekey);
                }
                if (string.IsNullOrWhiteSpace(secrekey.RC))
                {
                    // 不和设备绑定
                }
                else
                {
                    if (secrekey.RC != regcode)
                    {
                        return ("此密钥不适用本软件", null);
                    }
                }
                return ("ok", new SecretKey()
                {

                    SDT = secrekey.SDT,
                    SoftCode = secrekey.SoftCode,
                    LT = secrekey.LT,
                    Deadline = secrekey.Deadline,
                    Customer = secrekey.Customer,
                    License = license,
                    Data=secrekey.Data,
                });
            }
            catch (Exception ex)
            {
                return ("密钥验证失败。", null);
            }
        }

        static bool isRun = false;
        static void Run()
        {
            if (isRun)
            {
                return;
            }
            isRun = true;
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        RegistryKey key = Registry.CurrentUser;
                        RegistryKey software = key.CreateSubKey("SOFTWARE\\licensecore");
                        var datetime = software.GetValue("dt");
                        if (datetime == null)
                        {
                            datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        var strdt = datetime.ToString();
                        if (!DateTime.TryParse(strdt, out DateTime dt))
                        {
                            dt = DateTime.Now;
                        }
                        if (dt > DateTime.Now)
                        {
                            dt = dt.AddMinutes(1);
                        }

                        else
                        {
                            dt = DateTime.Now;
                        }
                        software.SetValue("dt", dt.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    catch (Exception e)
                    {

                    }
                    Thread.Sleep(60000);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDateTime()
        {
            try
            {
                Run();
                RegistryKey key = Registry.CurrentUser;
                RegistryKey software = key.CreateSubKey("SOFTWARE\\licensecore");
                var datetime = software.GetValue("dt");
                if (datetime == null)
                {
                    datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                var strdt = datetime.ToString();
                if (!DateTime.TryParse(strdt, out DateTime dt))
                {
                    dt = DateTime.Now;
                }

                return dt.ToString("yyyyMMdd");

            }
            catch (Exception ex)
            {
                return DateTime.Now.ToString("yyyyMMdd");
            }
        }



        /// <summary>
        /// 权限校验
        /// </summary>
        /// <param name="path"></param>
        /// <param name="softcode"></param>
        /// <param name="regcode"></param>
        /// <param name="license"></param>
        /// <returns></returns>
        public static (string result, SecretKey SecretKey) VerifyFormFile(string key, string softcode,
                                                                 string regcode,
                                                                 string path)
        {

            if (!File.Exists(path))
            {
                return ("密钥无效", null);
            }
            string license = "";
            try
            {
                using (System.IO.StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    license = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch
            {
                license = "";
            }

            var content = AnalysisLicense(key, license);
            if (string.IsNullOrWhiteSpace(key))
            {
                return ("软件严重故障01，请联系管理员处理。", null);

            }

            //if (string.IsNullOrWhiteSpace(aeskey))
            //{
            //    return ("软件严重故障02，请联系管理员处理。", null);
            //}
            if (string.IsNullOrWhiteSpace(content))
            {
                return ("软件严重故障04，请联系管理员处理。", null);

            }

            try
            {
                //content = AESDecrypt(content, aeskey);
                //if (string.IsNullOrWhiteSpace(content))
                //{
                //    return ("密钥无效", null);
                //}
                var secrekey = Newtonsoft.Json.JsonConvert.DeserializeObject<SecretKey>(content);
                if (secrekey == null)
                {
                    return ("密钥无效", null);
                }
                //权限校验
                if (softcode != secrekey.SoftCode)
                {
                    return ("此密钥不适用此版本软件", null);
                }



                var dt = GetDateTime();
                // 期限校验
                if (Convert.ToUInt64(dt) > Convert.ToUInt64(secrekey.Deadline))
                {
                    return ("密钥以过期。", secrekey);
                }
                if (string.IsNullOrWhiteSpace(secrekey.RC))
                {
                    // 不和设备绑定
                }
                else
                {
                    if (secrekey.RC != regcode)
                    {
                        return ("此密钥不适用本软件", null);
                    }
                }
                return ("ok", new SecretKey()
                {

                    SDT = secrekey.SDT,
                    SoftCode = secrekey.SoftCode,
                    LT = secrekey.LT,
                    Deadline = secrekey.Deadline,
                    Customer = secrekey.Customer,
                    License = license,
                    Data = secrekey.Data,

                });
            }
            catch (Exception ex)
            {
                return ("密钥验证失败。", null);
            }
        }


        /// <summary>
        /// 跟新验证
        /// </summary>
        /// <param name="key"></param>
        /// <param name="softcode"></param>
        /// <param name="regcode"></param>
        /// <param name="license"></param>
        /// <returns></returns>
        public static (string result, SecretKey SecretKey) UpdateVerfy(string key,
                                                                 string softcode,
                                                                 string regcode,
                                                                 string license)
        {
            var (result, secretkey) = Verify(key, softcode, regcode, license);
            if (result == "ok")
            {
                try
                {
                    if (File.Exists(LicenseManage.LicensePath))
                    {
                        System.IO.File.Delete(LicenseManage.LicensePath);
                    }
                    using (System.IO.StreamWriter sw = new StreamWriter(LicenseManage.LicensePath))
                    {
                        sw.Write(license);
                        sw.Flush();
                        sw.Close();
                    }
                }
                catch
                {
                }
            }
            return (result, secretkey);
        }
        /// <summary>
        /// 跟新验证
        /// </summary>
        /// <param name="key"></param>
        /// <param name="softcode"></param>
        /// <param name="regcode"></param>
        /// <param name="license"></param>
        /// <returns></returns>
        public static (string result, SecretKey SecretKey) UpdateVerfy(string key,
                                                                 string softcode,
                                                                 string license)
        {
            var (result, secretkey) = Verify(key, softcode, GetRegistration(), license);
            if (result == "ok")
            {
                try
                {
                    if (File.Exists(LicenseManage.LicensePath))
                    {
                        System.IO.File.Delete(LicenseManage.LicensePath);

                    }
                    using (System.IO.StreamWriter sw = new StreamWriter(LicenseManage.LicensePath))
                    {
                        sw.Write(license);
                        sw.Flush();
                        sw.Close();
                    }
                }
                catch
                {
                }
            }
            return (result, secretkey);
        }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="key"></param>
        /// <param name="softcode"></param>
        /// <returns></returns>
        public static (string result, SecretKey SecretKey) Verify(string key, string softcode)
        {
            return VerifyFormFile(key, softcode, GetRegistration(), LicensePath);
        }

    }

}
