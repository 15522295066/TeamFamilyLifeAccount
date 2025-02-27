using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using FamilyLifeAccount.Model;
 


namespace FamilyLifeAccount.Common
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary> 
        /// 获取枚举的备注信息 
        /// </summary> 
        /// <param name="em">枚举</param> 
        /// <returns>返回备注信息</returns> 
        public static string GetRemark(this Enum em)
        {
            Type type = em.GetType();
            FieldInfo fd = type.GetField(em.ToString());
            if (fd == null)
            {
                return string.Empty;
            }
            object[] attrs = fd.GetCustomAttributes(typeof(RemarkAttribute), false);
            string name = string.Empty;
            foreach (RemarkAttribute attr in attrs)
            {
                name = attr.Remark;
            }
            return name;
        }

        /// <summary> 
        /// 获取枚举的值 
        /// </summary> 
        /// <param name="em">枚举</param> 
        /// <returns>返回枚举的值</returns> 
        public static int GetValue(this Enum em)
        {
            Type type = em.GetType();
            FieldInfo fd = type.GetField(em.ToString());
            if (fd == null)
            {
                //throw new UnKnowException("系统错误！");
            }
            return (int)fd.GetValue(em);
        }

        /// <summary>
        /// 通过值获取枚举备注
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="em">枚举的类型</param>
        /// <returns>返回枚举备注</returns>
        public static string GetRemark(this int val, Type em)
        {
            var e = Enum.Parse(em, val.ToString());
            FieldInfo fd = em.GetField(e.ToString());
            if (fd == null)
            {
                return string.Empty;
            }
            object[] attrs = fd.GetCustomAttributes(typeof(RemarkAttribute), false);
            string name = string.Empty;
            foreach (RemarkAttribute attr in attrs)
            {
                name = attr.Remark;
            }
            return name;
        }

        /// <summary>
        /// 通过值获取枚举备注
        /// </summary>
        /// <param name="em">枚举的类型</param>
        /// <param name="val">值</param>
        /// <returns>返回枚举备注</returns>
        public static string GetRemarkByVal(Type em, int val)
        {
            var e = Enum.Parse(em, val.ToString());
            FieldInfo fd = em.GetField(e.ToString());
            if (fd == null)
            {
                return string.Empty;
            }
            object[] attrs = fd.GetCustomAttributes(typeof(RemarkAttribute), false);
            string name = string.Empty;
            foreach (RemarkAttribute attr in attrs)
            {
                name = attr.Remark;
            }
            return name;
        }

        /// <summary>
        /// 获取枚举的键值列表
        /// </summary>
        /// <param name="em">枚举的类型</param>
        /// <returns>返回枚举的键值列表</returns>
        public static List<CustomStatus> GetKeyValueList(Type em)
        {
            var list = new List<CustomStatus>();
            Array ems = Enum.GetValues(em);
            FieldInfo fd;
            CustomStatus c;
            string name = string.Empty;
            foreach (var e in ems)
            {
                c = new CustomStatus();
                fd = em.GetField(e.ToString());
                if (fd == null)
                {
                    continue;
                }
                object[] attrs = fd.GetCustomAttributes(typeof(RemarkAttribute), false);
                foreach (RemarkAttribute attr in attrs)
                {
                    name = attr.Remark;
                }
                c.StatusCode = (int)fd.GetValue(e);
                c.StatusRemark = name;
                list.Add(c);
            }
            return list;
        }
    }
}
