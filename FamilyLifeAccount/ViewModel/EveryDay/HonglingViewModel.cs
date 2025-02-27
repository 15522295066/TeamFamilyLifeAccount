﻿using System;
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
    public class HonglingViewModel: PaginViewModel
    {
     
        DALBase dal = new DALBase();

        public HonglingViewModel()
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
            EditHongling uc = new EditHongling();
            PopUpWindow pop = new PopUpWindow(uc);
            pop.Closed += (s, e) => ViewModelLocator.ClearEditHonglingViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                var msg = new NotificationMessage<string>(id, Notifications.UpdateShow);
                Messenger.Default.Send<NotificationMessage<string>, EditHonglingViewModel>(msg);
                var msg2 = new NotificationMessage<string>(id, Notifications.Parameter);
                Messenger.Default.Send<NotificationMessage<string>, EditHongling>(msg2);
            }
            else
            {
                var msg = new NotificationMessage<string>(id, Notifications.AddShow);
                Messenger.Default.Send<NotificationMessage<string>, EditHonglingViewModel>(msg);
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
                    MyHongling = dal.GetOneModel<hongling>(m => m.HonglingID.Equals(ID));
                    MyHongling.IsDel = 1;
                    dal.Update<hongling>(MyHongling);
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
            var sql = dal.GetList<view_honglinglist>(m => m.AddTime >= StartDate && m.AddTime <= ED && m.IsDel.Equals(0));
            if (UserID.Equals(1)||UserID.Equals(2))
            {
                sql = sql.Where(m => m.UserID.Equals(UserID)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(Key))
            {
                sql = sql.FindAll(m => m.HonglingName.Contains(Key));
            }
            HonglingList = sql.OrderByDescending(m => m.AddTime).ToList();
            if (sql.Count() > 0)
            {
                base.Pagin.SumPrice = sql.Sum(m => m.HonglingMoney);
            }
            else
            {
                base.Pagin.SumPrice = 0;
            }
            //base.Pagin.RecordCount = HonglingList.Count;
            //HonglingList = Paging<view_honglinglist>.GetListByPage(HonglingList, Pagin.PageNo, Pagin.PageSize);

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



        private List<view_honglinglist> _HonglingList;
        public List<view_honglinglist> HonglingList
        {
            get { return _HonglingList; }
            set
            {
                _HonglingList = value;
                this.RaisePropertyChanged("HonglingList");
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

        private hongling _MyHongling = new hongling();
        public hongling MyHongling
        {
            get { return _MyHongling; }
            set
            {
                _MyHongling = value;
                this.RaisePropertyChanged("MyHongling");
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