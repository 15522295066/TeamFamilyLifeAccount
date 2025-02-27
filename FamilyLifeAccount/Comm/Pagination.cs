using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyLifeAccount.Comm
{
    public class Pagination
    {
        //当前页
        int _pageno = 1;

        public int PageNo
        {
            get { return _pageno; }
            set { _pageno = value; }
        }
        //每页显示数量
        int _pagesize = 10;

        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
        //总页数

        int _pagecount;

        public int PageCount
        {
            set { _pagecount = value; }
            get { return _pagecount = (int)(_recordcount + _pagesize - 1) / _pagesize; }
        }

        //总记录数
        long _recordcount;

        public long RecordCount
        {
            get { return _recordcount; }
            set 
            { 
                _recordcount = value;
                PageCount = (int)(_recordcount + _pagesize - 1) / _pagesize;
                PageTitle = "共" + _pagecount + "页 当前第" + _pageno + "页, 共" + _recordcount + "条记录";
            }
        }

        //分页提示
        string _pagetitle;

        public string PageTitle
        {
            set 
            {
                _pagetitle = value;
            }
            get { return _pagetitle = "共" + _pagecount + "页 当前第" + _pageno + "页, 共" + _recordcount + "条记录"; }
        }

        //当前页
        string _currentPage;

        public string CurrentPage
        {
            get { return _currentPage = _pageno + "/" + _pagecount; }
        }


    }
}
