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
    public class CostMainViewModel : PaginViewModel
    {
        DALBase dal = new DALBase();

        public CostMainViewModel()
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
            EditCostMain uc = new EditCostMain();
            PopUpWindow pop = new PopUpWindow(uc);
            pop.Closed += (s, e) => ViewModelLocator.ClearEditCostMain();
            if (!string.IsNullOrEmpty(id))
            {
                var msg = new NotificationMessage<string>(id, Notifications.UpdateShow);
                Messenger.Default.Send<NotificationMessage<string>, EditCostMainViewModel>(msg);
                var msg2 = new NotificationMessage<string>(id, Notifications.Parameter);
                Messenger.Default.Send<NotificationMessage<string>, EditCostMain>(msg2);
            }
            else
            {
                var msg = new NotificationMessage<string>(id, Notifications.AddShow);
                Messenger.Default.Send<NotificationMessage<string>, EditCostMainViewModel>(msg);
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
                    MyCost = dal.GetOneModel<cost>(m => m.CostID.Equals(ID));
                    MyCost.IsDel = 1;
                    dal.Update<cost>(MyCost);
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
            MyCost.AddTime = DateTime.Now;
            StartDate = DateTime.Parse(str);
            EndDate = DateTime.Now.Date;
            GetList();
            //using (familylifeaccountEntities db = new familylifeaccountEntities())
            //{
            //    PersionList = db.persons.ToList();
            //    ShopList = db.shops.ToList();
            //    ClassList = db.costclass.Where(m => m.ParentID.Equals(0)).ToList();
            //    AccountList = db.account.ToList();
            //}
        }



        public override void GetList()
        {

            DateTime ED = EndDate.Date.AddHours(23);
            var sql = dal.GetList<view_costlist>(m => (m.AddTime >= StartDate && m.AddTime <= ED) && m.IsDel.Equals(0) && m.IsCompany == 0);

            if (!string.IsNullOrWhiteSpace(Key))
            {
                sql = sql.Where(m => m.CostName.Contains(Key)).ToList();
            }
            sql = sql.OrderByDescending(m => m.AddTime).ToList();
            if (sql.Count() > 0)
            {
                base.Pagin.SumPrice = sql.Sum(m => m.CostMoney);
            }
            else
            {
                base.Pagin.SumPrice = 0;
            }
            CostList = (from c in sql
                       select new  MyCostList
                       {
                           CostID = c.CostID,
                           ParentClassName = dal.GetOneModel<costclass>(m => m.CostClassID == c.ParentID).ClassName,
                           CostName = c.CostName,
                           ClassName = c.ClassName,
                           ShopName = c.ShopName,
                           CostMoney = c.CostMoney,
                           AccountName = c.AccountName,
                           AddTime = c.AddTime,
                           UserName = c.UserName,
                           CostContent = c.CostContent
                       }).ToList();

            //base.Pagin.RecordCount = CostList.Count;
            //CostList = Paging<MyCostList>.GetListByPage(list.ToList(), Pagin.PageNo, Pagin.PageSize);
            //CostList = list.ToList();
        }

        #endregion

        #region 命令初始化
        public RelayCommand SubCommand { get; set; }
        public RelayCommand QueryCommand { get; set; }
        public RelayCommand<string> DeleteCommand { get; set; }
        public RelayCommand<string> ShowDialogCommand { get; set; }
        #endregion

        #region 数据源属性初始化

        private List<MenuCostClass> _MenuClassList = new List<MenuCostClass>();
        public List<MenuCostClass> MenuClassList
        {
            get { return _MenuClassList; }
            set
            {
                _MenuClassList = value;
                this.RaisePropertyChanged("MenuClassList");
            }
        }



        private List<MyCostList> _CostList;
        public List<MyCostList> CostList
        {
            get { return _CostList; }
            set
            {
                _CostList = value;
                this.RaisePropertyChanged("CostList");
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

        private List<costclass> _ClassList;
        public List<costclass> ClassList
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

        private cost _MyCost = new cost();
        public cost MyCost
        {
            get { return _MyCost; }
            set
            {
                _MyCost = value;
                this.RaisePropertyChanged("MyCost");
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