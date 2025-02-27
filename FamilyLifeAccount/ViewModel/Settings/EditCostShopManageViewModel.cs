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
using FamilyLifeAccount.View.Settings;

namespace FamilyLifeAccount.ViewModel.Settings
{
    
    public class EditCostShopManageViewModel : ViewModelBase
    {

        #region 数据初始化

        DALBase dal = new DALBase();
        UIBase uibase = new UIBase();

        public EditCostShopManageViewModel()
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
            using (familylifeaccountEntities db = new familylifeaccountEntities())
            {
                ShopList = db.shops.ToList();
                ClassList = db.costclass.ToList();
            }
        }

        #endregion

        #region 命令初始化
        public RelayCommand SubCommand { get; set; }
        public RelayCommand<string> CloseCommand { get; set; }
        #endregion

        #region 方法函数

        /// <summary>
        /// 接收信息 
        /// </summary>
        /// <param name="msg"></param>
        private void ReceiveMsg(NotificationMessage<string> msg)
        {
            using (familylifeaccountEntities db = new familylifeaccountEntities())
            {
                if (msg.Notification.Equals(Notifications.UpdateShow))
                {
                    int ID = int.Parse(msg.Content);
                    ShopModel = db.shops.Where(m => m.ShopID.Equals(ID)).FirstOrDefault();
                }
                if (msg.Notification.Equals(Notifications.AddShow))
                {
                    ShopModel.ShopID = 0;
                }
                //接收改变分类ID消息
                if (msg.Notification.Equals(Notifications.Parameter))
                {
                    ShopModel.CostClassID = int.Parse(msg.Content);
                    //获取商家名称
                    //ShopModel = db.shops.Where(m => m.CostClassID.Equals(ID)).FirstOrDefault();
                }
            }
        }


        /// <summary>
        /// 提交更新
        /// </summary>
        private void Submit()
        {

            if (uibase.MessageShowError(ShopModel.CostClassID, "支出分类"))
            {
                if (uibase.MessageShowError(ShopModel.ShopName, "项目"))
                {
                    try
                    {
                        if (ShopModel.ShopID == 0)
                        {
                            dal.Add<shops>(ShopModel);
                            uibase.MessageBox("添加信息成功!");
                        }
                        else
                        {
                            //ShopModel.AddTime = DateTime.Parse(ShopModel.AddTime.Date);
                            dal.Update<shops>(ShopModel);
                            uibase.MessageBox("修改信息成功!");
                        }
                        ClosePage(ShopModel.ShopID.ToString());
                        var msg = new NotificationMessage(null, Notifications.Refresh);
                        Messenger.Default.Send<NotificationMessage, CostShopManageViewModel>(msg);
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
            Messenger.Default.Send<NotificationMessage<string>, EditCostShopManage>(msg);
        }

        #endregion

        #region 属性初始化

        private shops _ShopModel = new shops();
        public shops ShopModel
        {
            get { return _ShopModel; }
            set
            {
                _ShopModel = value;
                this.RaisePropertyChanged("ShopModel");
            }
        }



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