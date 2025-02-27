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
    public class EditHonglingViewModel  : ViewModelBase
    {
        
        #region 数据初始化

        DALBase dal = new DALBase();
        UIBase uibase = new UIBase();

        public EditHonglingViewModel()
        {
            if (!IsInDesignMode)
            {
                InitLoad();
                Messenger.Default.Register<NotificationMessage<string>>(this, (msg) => ReceiveMsg(msg));
                SubCommand = new RelayCommand(() => Submit());
                CloseCommand = new RelayCommand<string>((msg) => ClosePage(msg));
            }
        }


        private void InitLoad()
        {
            MyHongling.AddTime = DateTime.Now;
            using (familylifeaccountEntities db = new familylifeaccountEntities())
            {
                var sql = db.persons.OrderByDescending(m => m.UserID);
                PersionList = new ObservableCollection<persons>(sql);
                ClassList = db.earningclass.ToList();
                AccountList = db.account.ToList();
            }
        }

        #endregion

        #region 命令初始化
        public RelayCommand SubCommand { get; set; }
        public RelayCommand<string> CloseCommand { get; set; }
        #endregion

        #region 方法函数

        private void ReceiveMsg(NotificationMessage<string> msg)
        {
            using (familylifeaccountEntities db = new familylifeaccountEntities())
            {
                if (msg.Notification.Equals(Notifications.UpdateShow))
                {
                    ShopList = db.shops.ToList();
                    int ID = int.Parse(msg.Content);
                    MyHongling = db.hongling.Where(m => m.HonglingID.Equals(ID)).FirstOrDefault();
                 
                }
                if (msg.Notification.Equals(Notifications.AddShow))
                {
                    MyHongling.HonglingID = 0;
                    //MyHongling.AddTime = DateTime.Now;
                }
                //接收改变分类ID消息
                if (msg.Notification.Equals(Notifications.Parameter))
                {
                    MyHongling.EarningClassID = int.Parse(msg.Content);
                     
                }
            }
        }
 

        /// <summary>
        /// 提交更新
        /// </summary>
        private void Submit()
        {

            if (uibase.MessageShowError(MyHongling.EarningClassID, "支出分类"))
            {
                if (uibase.MessageShowError(MyHongling.HonglingName, "项目"))
                {
                    try
                    {
                        
                        if (MyHongling.HonglingID == 0)
                        {

                            dal.Add<hongling>(MyHongling);
                            uibase.MessageBox("添加信息成功!");
                        }
                        else
                        {
                            dal.Update<hongling>(MyHongling);
                            uibase.MessageBox("修改信息成功!");
                        }
                        ClosePage(MyHongling.HonglingID.ToString());
                        var msg = new NotificationMessage(null, Notifications.Refresh);
                        Messenger.Default.Send<NotificationMessage, HonglingViewModel>(msg);
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
            Messenger.Default.Send<NotificationMessage<string>, EditHongling>(msg);
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