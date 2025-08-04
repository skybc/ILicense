using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseCheckWin
{
    /// <summary>
    /// 
    /// </summary>
    public class LicenseCheckMain
    {
        /// <summary>
        /// 检验License
        /// </summary>
        /// <param name="softcode"></param>
        /// <returns></returns>
        public static ILicense2.Entity.SecretKey CheckLicense(string privateKey,string softcode,string fileName="")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    fileName = "license.lic";
                }
                if (File.Exists(fileName))
                {
                    using (var stream = new StreamReader(File.OpenRead(fileName)))
                    {
                        var sc= stream.ReadToEnd();
                        var re= ILicense2.LicenseManage.Verify(privateKey, softcode, ILicense2.LicenseManage.GetRegistration(), sc);
                        if(re.result=="ok")
                        {
                            return re.secretkey;
                        }
                        throw new Exception(re.result);
                    }
                }
                else
                {
                    throw new Exception("没有授权");
                }
            }
            catch(Exception ex)
            {
                WinRegisiter winRegisiter = new WinRegisiter() { SoftCode=softcode,Key=privateKey,FileName=fileName};
                if (winRegisiter.ShowDialog() == true)
                {
                    return winRegisiter.Secretkey;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
