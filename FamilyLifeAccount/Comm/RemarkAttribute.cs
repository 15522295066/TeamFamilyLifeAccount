using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace FamilyLifeAccount.Common
{
    /// <summary>
    /// 备注中文含义
    /// </summary>
    public class RemarkAttribute : Attribute
    {
        /// <summary>
        /// 备注
        /// </summary>
        private string remark;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="remark">备注</param>
        public RemarkAttribute(string remark)
        {
            this.remark = remark;
        }

        /// <summary>
        /// 备注属性
        /// </summary>
        public string Remark
        {
            get
            {
                return this.remark;
            }

            set
            {
                this.remark = value;
            }
        }

        /// <summary> 
        /// 获取枚举的备注信息 
        /// </summary> 
        /// <param name="val">枚举值</param> 
        /// <returns>返回备注信息</returns> 
        public static string GetEnumRemark(Enum val)
        {
            if (val == null)
            {
                return string.Empty;
            }

            Type type = val.GetType();
            FieldInfo field = type.GetField(val.ToString());
            if (field == null)
            {
                return string.Empty;
            }

            object[] attrs = field.GetCustomAttributes(typeof(RemarkAttribute), false);
            string name = string.Empty;
            foreach (RemarkAttribute attr in attrs)
            {
                name = attr.Remark;
            }

            return name;
        } 
    }
}
