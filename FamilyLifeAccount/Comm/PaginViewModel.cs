using GalaSoft.MvvmLight;

using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;


namespace FamilyLifeAccount.Comm
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public abstract class PaginViewModel : ViewModelBase
    {

        /// <summary>
        /// Initializes a new instance of the PaginViewModel class.
        /// </summary>
        public PaginViewModel()
        {
            PageTurning = new RelayCommand<string>((page) => GetPage(page));
            PageGoto = new RelayCommand<string>((page) => GetPageGoTo(page));
            PageSize = new RelayCommand(() => GetPageSize());
        }

        #region 分页属性 Pagination
        private Pagination _pagin = new Pagination();
        public const string PaginProp = "Pagin";
        public Pagination Pagin
        {
            get
            {
                return _pagin;
            }
            set
            {
                if (_pagin.Equals(value))
                {
                    return;
                }
                var oldValue = _pagin;
                _pagin = value;
                RaisePropertyChanged(PaginProp);
            }
        }
        #endregion

        #region 翻页 PageTurning

        public RelayCommand<string> PageTurning
        {
            get;
            private set;
        }

        public void GetPage(string page)
        {

            switch (page)
            {
                case "0": _pagin.PageNo = 1; break;
                case "1": if (_pagin.PageNo < _pagin.PageCount) { _pagin.PageNo += 1; } break;
                case "-1": if (_pagin.PageNo > 1) { _pagin.PageNo -= 1; } break;
                case "-99": _pagin.PageNo = _pagin.PageCount; break;
                default: break;
            }
            GetList();
        }


        #endregion

        #region 分页方法 GetList
        public abstract void GetList();
        #endregion

        #region 跳转页 PageGoto
        public RelayCommand<string> PageGoto
        {
            get;
            private set;
        }

        public void GetPageGoTo(string page)
        {
            int pageNumber;
            if (string.IsNullOrWhiteSpace(page))
            {
                return;
            }
            try
            {
                pageNumber = int.Parse(page);
            }
            catch
            {
                return;
            }
            if (pageNumber > _pagin.PageCount || pageNumber < 1)
            {
                //CustomMessageBox.CustomMessageBox.Show("aaaaa");
                return;
            }
            else
            {
                _pagin.PageNo = int.Parse(page);
                GetList();
            }
        }
        #endregion

        #region 一页显示记录数列表
        private List<PageSizeList> _paginsize;
        public const string PageSizeListProp = "PageSizeList";
        public List<PageSizeList> PageSizeList
        {
            get
            {
                if (_paginsize == null)
                {
                    PageSizeList p = new PageSizeList();
                    _paginsize = p.GetPageSizeList();
                }
                return _paginsize;
            }
            set
            {
                if (_paginsize.Equals(value))
                {
                    return;
                }
                var oldValue = _paginsize;
                _paginsize = value;
                RaisePropertyChanged(PageSizeListProp);
            }
        }
        #endregion

        #region 一页显示记录数量
        public RelayCommand PageSize
        {
            get;
            private set;
        }

        public void GetPageSize()
        {
            GetList();
        }
        #endregion




        public override void Cleanup()
        {
            _pagin = null;
            base.Cleanup();
        }

      

        public class Pagination:ViewModelBase
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
                get { return _currentPage = "/" + _pagecount; }
            }


            private object _SumPrice;
            public object SumPrice
            {
                get
                {
                    return string.Format("{0:C2}", _SumPrice);
                }
                set
                {
                    _SumPrice = value;
                    this.RaisePropertyChanged("SumPrice");
                }
            }
          

        }
    }
}