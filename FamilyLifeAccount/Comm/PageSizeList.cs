using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyLifeAccount.Comm
{
    public class PageSizeList
    {
        int pagesize = 0;

        public int Pagesize
        {
            get { return pagesize; }
            set { pagesize = value; }
        }
        public List<PageSizeList> GetPageSizeList()
        {
            List<PageSizeList> list = new List<PageSizeList>();
            for (int i = 0; i < 5; i++)
            {
                PageSizeList p = new PageSizeList();
                if (i == 0)
                    p.pagesize = 5;
                else if (i == 1)
                    p.pagesize = 10;
                else if (i == 2)
                    p.pagesize = 20;
                else if (i == 3)
                    p.pagesize = 50;
                else if (i == 4)
                    p.pagesize = 100;
                list.Add(p);
            }
            return list;
        }
    }
}
