using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using DataFactory.MODEL;
using DataFactory.DAL;
using FamilyLifeAccount.Comm;
using System.Collections.ObjectModel;
using FamilyLifeAccount.Model;
using FamilyLifeAccount.View.EveryDay;
using System.Data;

namespace FamilyLifeAccount.ViewModel.EveryDay
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
    public class CompanyEarningViewModel : PaginViewModel
    {

        DALBase dal = new DALBase();

        public CompanyEarningViewModel()
        {
            if (!IsInDesignMode)
            {
                InitLoad();
                Messenger.Default.Register<NotificationMessage>(this, (msg) => Refresh(msg));
                QueryCommand = new RelayCommand(() => GetList());
                DeleteCommand = new RelayCommand<string>((id) => Delete(id));
                ShowDialogCommand = new RelayCommand<string>((id) => ShowDialog(id));
            }
        }

        #region 方法函数

        private void ShowDialog(string id)
        {
            CompanyEditEarningMain uc = new CompanyEditEarningMain();
            PopUpWindow pop = new PopUpWindow(uc);
            pop.Closed += (s, e) => ViewModelLocator.ClearCompanyEditEarningMain();
            if (!string.IsNullOrEmpty(id))
            {
                var msg = new NotificationMessage<string>(id, Notifications.UpdateShow);
                Messenger.Default.Send<NotificationMessage<string>, CompanyEditEarningViewModel>(msg);
                var msg2 = new NotificationMessage<string>(id, Notifications.Parameter);
                Messenger.Default.Send<NotificationMessage<string>, CompanyEditEarningMain>(msg2);
            }
            else
            {
                var msg = new NotificationMessage<string>(id, Notifications.AddShow);
                Messenger.Default.Send<NotificationMessage<string>, CompanyEditEarningViewModel>(msg);
            }

            pop.ShowDialog();
        }

        private void Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                // Xceed.Wpf.Toolkit.MessageBox.Show(MessageEnum.确定要删除此条记录吗.ToString(), ModelEnum.支出.ToString(), MessageBoxButton.OKCancel);
                if (MessageBoxResult.OK == Xceed.Wpf.Toolkit.MessageBox.Show(MessageEnum.确定要删除此条记录吗.ToString(), ModelEnum.支出.ToString(), MessageBoxButton.OKCancel))
                {
                    int ID = int.Parse(id);
                    MyEarning = dal.GetOneModel<earning>(m => m.EarningID.Equals(ID));
                    MyEarning.IsDel = 1;
                    dal.Update<earning>(MyEarning);
                    GetList();
                }
            }
        }

        private void Refresh(NotificationMessage msg)
        {
            if (msg.Notification.Equals(Notifications.Refresh))
            {
                GetList();
            }
        }

        private void InitLoad()
        {
            string str = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, "01");
            MyEarning.AddTime = DateTime.Now;
            StartDate = DateTime.Parse(str);
            EndDate = DateTime.Now.Date;
            GetList();
            //using (familylifeaccountEntities db = new familylifeaccountEntities())
            //{
            //    PersionList = db.persons.ToList();
            //    ShopList = db.shops.ToList();
            //    ClassList = db.Earningclass.Where(m => m.ParentID.Equals(0)).ToList();
            //    AccountList = db.account.ToList();
            //}
        }



        public override void GetList()
        {
            DateTime ED = EndDate.Date.AddHours(23);
            var sql = dal.FindAll<view_companyearninglist>(m => m.IsDel == 0);
            sql = sql.Where(m => (m.AddTime >= StartDate && m.AddTime <= ED) && m.IsDel.Equals(0));

            if (!string.IsNullOrWhiteSpace(Key))
            {
                sql = sql.Where(m => m.EarningName.Contains(Key));
            }
            var list = sql.ToList();
            MyCompanyEarningList = list.Select(m => new MyCompanyEarningList
            {
                AddTime = m.AddTime,
                ClassName = m.ClassName,
                CompanyName = m.CompanyName,
                EarningContent = m.EarningContent,
                EarningMoney = m.EarningMoney,
                EarningName = m.EarningName,
                EarningClassID = m.EarningClassID,
                CompanyId = m.CompanyId,
                ParentID = m.ParentID,
                EarningID = m.EarningID,
                ParentClassName = dal.GetOneModel<earningclass>(c => c.EarningClassID == m.ParentID).ClassName,

            }).ToList();

            base.Pagin.RecordCount = MyCompanyEarningList.Count;
            base.Pagin.SumPrice = MyCompanyEarningList.Sum(m => m.EarningMoney);
            //CompanyEarningList = Paging<MyCompanyList>.GetListByPage(CompanyEarningList, Pagin.PageNo, Pagin.PageSize);
            //EarningList = list.ToList();
        }

        #endregion

        #region 命令初始化
        public RelayCommand SubCommand { get; set; }
        public RelayCommand QueryCommand { get; set; }
        public RelayCommand<string> DeleteCommand { get; set; }
        public RelayCommand<string> ShowDialogCommand { get; set; }
        #endregion

        #region 数据源属性初始化

        private List<MenuEarningClass> _MenuClassList = new List<MenuEarningClass>();
        public List<MenuEarningClass> MenuClassList
        {
            get { return _MenuClassList; }
            set
            {
                _MenuClassList = value;
                this.RaisePropertyChanged("MenuClassList");
            }
        }


        private List<MyCompanyEarningList> _MyCompanyEarningList;
        public List<MyCompanyEarningList> MyCompanyEarningList
        {
            get { return _MyCompanyEarningList; }
            set
            {
                _MyCompanyEarningList = value;
                this.RaisePropertyChanged("MyCompanyEarningList");
            }
        }


        private List<view_companyearninglist> _EarningList;
        public List<view_companyearninglist> EarningList
        {
            get { return _EarningList; }
            set
            {
                _EarningList = value;
                this.RaisePropertyChanged("EarningList");
            }
        }


        private List<persons> _PersionsList;
        public List<persons> PersionList
        {
            get { return _PersionsList; }
            set
            {
                _PersionsList = value;
                this.RaisePropertyChanged("PersionList");
            }
        }

        private List<shops> _ShopList;
        public List<shops> ShopList
        {
            get { return _ShopList; }
            set
            {
                _ShopList = value;
                this.RaisePropertyChanged("ShopList");
            }
        }

        private List<earningclass> _ClassList;
        public List<earningclass> ClassList
        {
            get { return _ClassList; }
            set
            {
                _ClassList = value;
                this.RaisePropertyChanged("ClassList");
            }
        }

        private List<account> _AccountList;
        public List<account> AccountList
        {
            get { return _AccountList; }
            set
            {
                _AccountList = value;
                this.RaisePropertyChanged("AccountList");
            }
        }

        private earning _MyEarning = new earning();
        public earning MyEarning
        {
            get { return _MyEarning; }
            set
            {
                _MyEarning = value;
                this.RaisePropertyChanged("MyEarning");
            }
        }

        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                _StartDate = value;
                this.RaisePropertyChanged("StartDate");
            }
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                _EndDate = value;
                this.RaisePropertyChanged("EndDate");
            }
        }

        private string _Key;
        public string Key
        {
            get { return _Key; }
            set
            {
                _Key = value;
                this.RaisePropertyChanged("Key");
            }
        }



        private string _ParentClassName = "sss";
        public string ParentClassName
        {
            get
            {

                return _ParentClassName;
            }
            set
            {
                _ParentClassName = value;
                this.RaisePropertyChanged("ParentClassName");
            }
        }


        #endregion
    }
}