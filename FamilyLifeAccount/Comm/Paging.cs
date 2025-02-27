using System.Collections.Generic;
using System;

using System.Linq;
using System.Text;

namespace FamilyLifeAccount.Comm
{
    public class Paging<T>
    {

        private static int _pagesize=10;
        public static int pagesize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="pageindex">页数</param>
        /// <param name="pagecount">每页数量</param>
        /// <returns></returns>
        public static List<T> GetListByPage(List<T> list, int pageindex, int pagecount)
        {
            int start = (pageindex - 1) * pagecount;
            return list.Skip(start).Take(pagecount).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">数据源</param>
        /// <param name="pageindex">页数</param>
        /// <param name="pagecount">每页数量</param>
        /// <param name="fun">lamba表达式</param>
        /// <returns></returns>
        public static List<T> GetListByPage(List<T> list, int pageindex, int pagecount, Func<T, bool> fun)
        {
            int start = (pageindex - 1) * pagecount;
            return list.Where(fun).Skip(start).Take(pagecount).ToList();
        }
        /// <summary>
        /// 分页提示显示
        /// </summary>
        /// <param name="count">总条数</param>
        /// <param name="page">总页数</param>
        /// <returns></returns>
        public static string GetPageDisplay(int count, int page)
        {
            string str = string.Format("共{0}条记录 每页{1}条 共{2}页", count, pagesize, page);
            return str;
        }
        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <param name="list">总条数</param>
        /// <returns></returns>
        public static int GetPageCount(int list)
        {
            int count = list / pagesize;
            if (list % pagesize != 0)
                count++;
            return count;
        }

 
    }
}
