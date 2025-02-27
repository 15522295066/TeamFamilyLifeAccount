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
    public class EarningViewModel : PaginViewModel
    {
     
        DALBase dal = new DALBase();

        public EarningViewModel()
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
            EditEarningMain uc = new EditEarningMain();
            PopUpWindow pop = new PopUpWindow(uc);
            pop.Closed += (s, e) => ViewModelLocator.ClearEditEarningViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                var msg = new NotificationMessage<string>(id, Notifications.UpdateShow);
                Messenger.Default.Send<NotificationMessage<string>, EditEarningViewModel>(msg);
                var msg2 = new NotificationMessage<string>(id, Notifications.Parameter);
                Messenger.Default.Send<NotificationMessage<string>, EditEarningMain>(msg2);
            }
            else
            {
                var msg = new NotificationMessage<string>(id, Notifications.AddShow);
                Messenger.Default.Send<NotificationMessage<string>, EditEarningViewModel>(msg);
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
            StartDate = DateTime.Parse("2014-01-01");
            EndDate = DateTime.Now.Date;
            PersionList = dal.GetList<persons>().OrderByDescending(m=>m.UserID).ToList();
            GetList();
        }
         
        /// <summary>
        /// 收入列表
        /// </summary>
        public override void GetList()
        {
     
            DateTime ED = EndDate.Date.AddHours(23);
            var sql = dal.GetList<view_earninglist>(m => m.AddTime >= StartDate && m.AddTime <= ED && m.IsDel.Equals(0));
            if (UserID.Equals(1)||UserID.Equals(2))
            {
                sql = sql.Where(m => m.UserID.Equals(UserID)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(Key))
            {
                sql = sql.FindAll(m => m.EarningName.Contains(Key));
            }
            EarningList = sql.OrderByDescending(m => m.AddTime).ToList();
            if (sql.Count() > 0)
            {
                base.Pagin.SumPrice = sql.Sum(m => m.EarningMoney);
            }
            else
            {
                base.Pagin.SumPrice = 0;
            }
            //base.Pagin.RecordCount = EarningList.Count;
            //EarningList = Paging<view_earninglist>.GetListByPage(EarningList, Pagin.PageNo, Pagin.PageSize);

        }

        #endregion

        #region 命令初始化
        public RelayCommand SubCommand { get; set; }
        public RelayCommand QueryCommand { get; set; }
        public RelayCommand<string> DeleteCommand { get; set; }
        public RelayCommand<string> ShowDialogCommand { get; set; }
        #endregion

        #region 数据源属性初始化



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


        private List<earningclass> _MenuClassList = new List<earningclass>();
        public List<earningclass> MenuClassList
        {
            get { return _MenuClassList; }
            set
            {
                _MenuClassList = value;
                this.RaisePropertyChanged("MenuClassList");
            }
        }



        private List<view_earninglist> _EarningList;
        public List<view_earninglist> EarningList
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

        private int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set
            {
                _UserID = value;
                this.RaisePropertyChanged("UserID");
            }
        }


        #endregion
    }
}