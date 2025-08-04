using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILicense2.Entity
{
    /// <summary>
    /// 密钥
    /// </summary>
    public class SecretKey
    {
        /// <summary>
        /// 软件代号
        /// </summary>
        public string SoftCode { get; set; }

        /// <summary>
        /// 授权方式
        /// </summary>
         public LicenseType LT { get; set; }

        /// <summary>
        /// 授权日期
        /// </summary>
         public string SDT { get;set; }
        /// <summary>
        /// 截至日期
        /// </summary>
        public string Deadline { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 注册码
        /// </summary>
        public string RC { get; set; }

        /// <summary>
        /// 随机码
        /// </summary>
        public string RandCode { get; set; }

        /// <summary>
        /// 自定义数据，不要过长
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// License
        /// </summary>
        [Newtonsoft.Json.JsonIgnore()]
        public string License { get; set; }

    }
}
