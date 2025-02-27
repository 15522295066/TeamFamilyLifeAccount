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
using FamilyLifeAccount.View;
using FamilyLifeAccount.View.EveryDay;

namespace FamilyLifeAccount.ViewModel.EveryDay
{
    public class EditMonthCostMainViewModel :  ViewModelBase
    {
        #region 数据初始化

        DALBase dal = new DALBase();
        UIBase uibase = new UIBase();

        public EditMonthCostMainViewModel()
        {
            if (!IsInDesignMode)
            {
                InitLoad();
                Messenger.Default.Register<NotificationMessage<string>>(this, (msg) => ReceiveMsg(msg));
                SubCommand = new RelayCommand(Submit);
                CloseCommand = new RelayCommand<string>((msg) => ClosePage(msg));
            }
        }

        private void InitLoad()
        {
            MyCost.AddTime = DateTime.Now;
            var sql = dal.GetList<persons>().OrderByDescending(m => m.UserID);
            PersionList = new ObservableCollection<persons>(sql);

            ClassList = dal.GetList<costclass>();
            AccountList = dal.GetList<account>();

        }

        #endregion

        #region 命令初始化
        public RelayCommand SubCommand { get; set; }
        public RelayCommand<string> CloseCommand { get; set; }
        #endregion

        #region 方法函数

        private void ReceiveMsg(NotificationMessage<string> msg)
        {

            if (msg.Notification.Equals(Notifications.UpdateShow))
            {
                int ID = int.Parse(msg.Content);
                MyCost = dal.GetOneModel<monthcost>(m => m.ID.Equals(ID));

            }
            if (msg.Notification.Equals(Notifications.AddShow))
            {
                MyCost.ID = 0;
                //MyCost.AddTime = DateTime.Now;
            }
            //接收改变分类ID消息
            if (msg.Notification.Equals(Notifications.Parameter))
            {
                MyCost.CostClassID = int.Parse(msg.Content);

            }
        }

        /// <summary>
        /// 提交更新
        /// </summary>
        private void Submit()
        {

            if (uibase.MessageShowError(MyCost.CostClassID, "支出分类"))
            {
                if (uibase.MessageShowError(MyCost.CostName, "项目"))
                {
                    try
                    {
                        if (MyCost.ID == 0)
                        {
                            dal.Add<monthcost>(MyCost);
                            uibase.MessageBox("添加信息成功!");
                        }
                        else
                        {
                            dal.Update<monthcost>(MyCost);
                            uibase.MessageBox("修改信息成功!");
                        }
                        ClosePage(MyCost.ID.ToString());
                        var msg = new NotificationMessage(null, Notifications.Refresh);
                        Messenger.Default.Send<NotificationMessage, MonthCostMainViewModel>(msg);
                    }
                    catch (Exception ex)
                    {
                        uibase.MessageBox(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void ClosePage(string id)
        {
            var msg = new NotificationMessage<string>(id, Notifications.Close);
            Messenger.Default.Send<NotificationMessage<string>, EditMonthCostMain>(msg);
        }

        #endregion

        #region 属性初始化

        private List<CustomStatus> _StatusList;
        public List<CustomStatus> StatusList
        {
            get { return _StatusList; }
            set
            {
                _StatusList = value;
                this.RaisePropertyChanged("StatusList");
            }
        }


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



        private List<view_costlist> _CostList;
        public List<view_costlist> CostList
        {
            get { return _CostList; }
            set
            {
                _CostList = value;
                this.RaisePropertyChanged("CostList");
            }
        }


        private ObservableCollection<persons> _PersionsList;
        public ObservableCollection<persons> PersionList
        {
            get { return _PersionsList; }
            set
            {
                _PersionsList = value;
                this.RaisePropertyChanged("PersionList");
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


        #endregion
    }
}