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
    public class MonthCostMainViewModel : PaginViewModel
    { 
        DALBase dal = new DALBase();

        public MonthCostMainViewModel()
        {
            if (!IsInDesignMode)
            {
                InitLoad();
                Messenger.Default.Register<NotificationMessage>(this, (msg) => Refresh(msg));
                QueryCommand = new RelayCommand( GetList);
                DeleteCommand = new RelayCommand<string>((id) => Delete(id));
                ShowDialogCommand = new RelayCommand<string>((id) => ShowDialog(id));
            }
        }

        #region 方法函数

        private void ShowDialog(string id)
        {
            EditMonthCostMain uc = new EditMonthCostMain();
            PopUpWindow pop = new PopUpWindow(uc);
            pop.Closed += (s, e) => ViewModelLocator.ClearEditMonthCostMain();
            if (!string.IsNullOrEmpty(id))
            {
                var msg = new NotificationMessage<string>(id, Notifications.UpdateShow);
                Messenger.Default.Send<NotificationMessage<string>, EditMonthCostMainViewModel>(msg);
                var msg2 = new NotificationMessage<string>(id, Notifications.Parameter);
                Messenger.Default.Send<NotificationMessage<string>, EditMonthCostMainViewModel>(msg2);

                Messenger.Default.Send<NotificationMessage<string>, EditMonthCostMain>(msg2);
            }
            else
            {
                var msg = new NotificationMessage<string>(id, Notifications.AddShow);
                Messenger.Default.Send<NotificationMessage<string>, EditMonthCostMainViewModel>(msg);
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
                    MyCost = dal.GetOneModel<monthcost>(m => m.ID.Equals(ID));
                    MyCost.IsDel = 1;
                    dal.Update<monthcost>(MyCost);
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
            string str = string.Format("2014-1-1");
            MyCost.AddTime = DateTime.Now;
            StartDate = DateTime.Parse(str);
            EndDate = DateTime.Now.Date;
            GetList();
            using (familylifeaccountEntities db = new familylifeaccountEntities())
            {
                Head = db.navigation.Where(m => m.NavigationID.Equals(36)).FirstOrDefault().Title;
            }
        }

        public override void GetList()
        {
            DateTime ED = EndDate.Date.AddHours(23);
            var sql = dal.GetList<monthcost>(m => m.AddTime >= StartDate && m.AddTime <= ED).Where(m=>m.IsDel.Equals(0));

            if (!string.IsNullOrWhiteSpace(Key))
            {
                sql = sql.Where(m => m.CostName.Contains(Key)).ToList();
            }
            MonthCostList = sql.OrderByDescending(m => m.AddTime).ThenBy(m => m.ClassName).ToList();

            if (sql.Count() > 0)
            {
                base.Pagin.SumPrice = sql.Sum(m => m.CostMoney);
            }
            else
            {
                base.Pagin.SumPrice = 0;
            }
            //base.Pagin.RecordCount = MonthCostList.Count;
            //MonthCostList = Paging<view_costlist>.GetListByPage(MonthCostList, Pagin.PageNo, Pagin.PageSize);

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



        private List<monthcost> _MonthCostList;
        public List<monthcost> MonthCostList
        {
            get { return _MonthCostList; }
            set
            {
                _MonthCostList = value;
                this.RaisePropertyChanged("MonthCostList");
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

        private monthcost _MyCost = new monthcost();
        public monthcost MyCost
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

        private string _Head;
        public string Head
        {
            get { return _Head; }
            set
            {
                _Head = value;
                this.RaisePropertyChanged("Head");
            }
        }


        #endregion
    }
}