using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILicense2.Entity
{
    /// <summary>
    /// 授权管理配置类
    /// </summary>
    public sealed class LicenseConfig
    {

        /// <summary>
        /// 软件代号
        /// </summary>
        public string SoftCode { get; set; }

        /// <summary>
        /// 客户端类型
        /// </summary>
        public ClientType ClientType { get; set; } = ClientType.CS;
        /// <summary>
        /// 公钥
        /// </summary>
        public string PublicKey { get; set; }
        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// 授权类型
        /// </summary>
        public LicenseType LicenseType { get; set; } = LicenseType.Limited;

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime Due { get; set; } = DateTime.Now.AddMonths(1);
        /// <summary>
        /// 客户名
        /// </summary>
        public string CustomerName { get; set; } = "客户";


    }
}
