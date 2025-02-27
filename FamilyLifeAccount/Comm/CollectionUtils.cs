using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyLifeAccount.Comm
{
    public class CollectionUtils<T>
    {
        /// <summary>
        /// 移除指定集合中对象并返还新集合
        /// </summary>
        /// <param name="l">集合</param>
        /// <param name="t">对象</param>
        /// <returns>集合</returns>
        public static List<T> GetNewRemoveList(List<T> l, T t)
        {
            if (t == null || l == null)
            {
                return l;
            }
            else
            {
                if (l.Contains(t))
                {
                    l.Remove(t);
                }
                List<T> tmp = new List<T>();
                foreach (T o in l)
                {
                    tmp.Add(o);
                }
                return tmp;
            }
        }

        /// <summary>
        /// 添加对象到指定集合，并返还新集合
        /// </summary>
        /// <param name="l">集合</param>
        /// <param name="t">对象</param>
        /// <param name="IsRepeat">是否可以重复</param>
        /// <returns>集合</returns>
        public static List<T> GetNewAddList(List<T> l, T t, bool IsRepeat = false)
        {
            if (t == null || l == null)
            {
                return l;
            }
            else
            {
                if (!l.Contains(t) || IsRepeat)
                {
                    l.Add(t);
                }
                List<T> tmp = new List<T>();
                foreach (T o in l)
                {
                    tmp.Insert(0,o);
                }
                return tmp;
            }
        }

        public static List<T> GetNewRemoveList(List<T> l, IEqualityComparer<T> compare, T t)
        {
            if (t == null || l == null)
            {
                return l;
            }
            else
            {
                if (l.Contains(t, compare))
                {
                    l.Remove(t);
                }
                List<T> tmp = new List<T>();
                foreach (T o in l)
                {
                    tmp.Add(o);
                }
                return tmp;
            }
        }

        public static List<T> GetNewAddList(List<T> l, T t, IEqualityComparer<T> compare, bool IsRepeat = false)
        {
            if (t == null || l == null)
            {
                return l;
            }
            else
            {
                if (!l.Contains(t, compare) || IsRepeat)
                {
                    l.Add(t);
                }
                List<T> tmp = new List<T>();
                foreach (T o in l)
                {
                    tmp.Add(o);
                }
                return tmp;
            }
        }

        public static bool ListContains(List<T> l, T t, IEqualityComparer<T> compare)
        {
            return l.Contains(t, compare);
        }
    }
}
