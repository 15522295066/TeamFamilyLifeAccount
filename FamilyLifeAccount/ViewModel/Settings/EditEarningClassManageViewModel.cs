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
using FamilyLifeAccount.View.Settings;
using FamilyLifeAccount.Common;
using FamilyLifeAccount.Model;
using FamilyLifeAccount.ViewModel.EveryDay;

namespace FamilyLifeAccount.ViewModel.Settings
{

    public class EditEarningClassManageViewModel : ViewModelBase
    {
        #region 数据初始化

        DALBase dal = new DALBase();
        UIBase uibase = new UIBase();

        public EditEarningClassManageViewModel()
        {
            if (!IsInDesignMode)
            {
                InitLoad();
                Messenger.Default.Register<NotificationMessage<string>>(this, (msg) => ReceiveMsg(msg));
                SubCommand = new RelayCommand(() => Submit());
                CloseCommand = new RelayCommand<object>((msg) => ClosePage(msg));
            }
        }


        private void InitLoad()
        {

            EarningClassList = dal.GetList<earningclass>(m => m.ParentID.Equals(0));
            //StatusList = EnumExtension.GetKeyValueList(typeof(EnableStatus));
        }

        #endregion

        #region 命令初始化
        public RelayCommand SubCommand { get; set; }
        public RelayCommand<object> CloseCommand { get; set; }
        #endregion

        #region 方法函数

        private void ReceiveMsg(NotificationMessage<string> msg)
        {
            using (familylifeaccountEntities db = new familylifeaccountEntities())
            {
                if (msg.Notification.Equals(Notifications.UpdateShow))
                {
                    int ID = int.Parse(msg.Content);
                    MyEarningClass = db.earningclass.Where(m => m.EarningClassID.Equals(ID)).FirstOrDefault();
                }
                if (msg.Notification.Equals(Notifications.AddShow))
                {
                    MyEarningClass.EarningClassID = 0;
                }

            }
        }


        /// <summary>
        /// 提交更新
        /// </summary>
        private void Submit()
        {
            if (uibase.MessageShowError(MyEarningClass.ClassName, "分类名"))
            {
                try
                {
                    //MyEarningClass.ParentID = 0;

                    MyEarningClass.AddTime = DateTime.Now;
                    if (MyEarningClass.EarningClassID == 0)
                    {
                        dal.Add<earningclass>(MyEarningClass);
                        uibase.MessageBox("添加信息成功!");
                    }
                    else
                    {
                        //MyEarningClass.AddTime = DateTime.Parse(MyEarningClass.AddTime.Date);
                        dal.Update<earningclass>(MyEarningClass);
                        uibase.MessageBox("修改信息成功!");
                    }
                    ClosePage(MyEarningClass.EarningClassID.ToString());
                    var msg = new NotificationMessage(null, Notifications.Refresh);
                    Messenger.Default.Send<NotificationMessage, EarningClassManageViewModel>(msg);
                }
                catch (Exception ex)
                {
                    uibase.MessageBox(ex.Message);
                }
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void ClosePage(object id)
        {
            var msg = new NotificationMessage<object>(id, Notifications.Close);
            Messenger.Default.Send<NotificationMessage<object>, EditEarningClassManage>(msg);
        }

        #endregion

        #region 属性初始化

        private List<earningclass> _EarningClassList;
        public List<earningclass> EarningClassList
        {
            get { return _EarningClassList; }
            set
            {
                _EarningClassList = value;
                this.RaisePropertyChanged("EarningClassList");
            }
        }

        private List<CustomStatus> _StatusList;
        public List<CustomStatus> StatusList
        {
            get { return _StatusList; }
            set
            {
                _StatusList = value;
                this.RaisePropertyChanged("MyProperty");
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


        private List<earningclass> _PersionsList;
        public List<earningclass> PersionList
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

        private earningclass _EarningClass = new earningclass();
        public earningclass MyEarningClass
        {
            get { return _EarningClass; }
            set
            {
                _EarningClass = value;
                this.RaisePropertyChanged("MyEarningClass");
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