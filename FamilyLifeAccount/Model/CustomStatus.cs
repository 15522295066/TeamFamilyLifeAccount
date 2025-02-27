using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyLifeAccount.Model
{
    /// <summary>
    /// 自定义状态实体，方便页面展示用
    /// </summary>
    public class CustomStatus
    {
        /// <summary>
        /// 状态编码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 状态备注
        /// </summary>
        public string StatusRemark { get; set; }
    }
}
