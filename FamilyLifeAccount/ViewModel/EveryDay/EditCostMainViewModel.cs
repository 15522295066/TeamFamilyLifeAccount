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
    public class EditCostMainViewModel : ViewModelBase
    {
        #region 数据初始化

        DALBase dal = new DALBase();
        UIBase uibase = new UIBase();

        public EditCostMainViewModel()
        {
            if (!IsInDesignMode)
            {
                InitLoad();
                Messenger.Default.Register<NotificationMessage<string>>(this, (msg) => ReceiveMsg(msg));
                SubCommand = new RelayCommand(new Action(Submit));
                CloseCommand = new RelayCommand<string>(ClosePage);
            }
        }

        private void InitLoad()
        {
            MyCost.AddTime = DateTime.Now;
            var sql = dal.GetList<persons>(m=>m.State==0).OrderByDescending(m => m.UserID);
            PersionList = new ObservableCollection<persons>(sql);
            ShopList = new List<shops>();
            ShopList.Add(new shops { ShopName = "请选择", ShopID = 0 });
            ClassList = dal.GetList<costclass>();
            AccountList = dal.GetList<account>(m=>m.State==0);

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
                ShopList = dal.GetList<shops>();
                int ID = int.Parse(msg.Content);
                MyCost = dal.GetOneModel<cost>(m => m.CostID.Equals(ID));
              
            }
            if (msg.Notification.Equals(Notifications.AddShow))
            {
                MyCost.CostID = 0;
               
            }
            //接收改变分类ID消息
            if (msg.Notification.Equals(Notifications.Parameter))
            {
                MyCost.CostClassID = int.Parse(msg.Content);
                ShopList = dal.GetList<shops>(m => m.CostClassID.Equals(MyCost.CostClassID));
                if (ShopList.Count > 0)
                {
                    MyCost.ShopID = ShopList[0].ShopID;
                }
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
                        //添加IsCompany默认值 修改当前分支的最新提交
                        MyCost.IsCompany = 1;
                        if (MyCost.CostID == 0)
                        {
                           
                            dal.Add<cost>(MyCost);
                            #region 添加月费用支付
                            costclass model = dal.GetOneModel<costclass>(m => m.CostClassID.Equals(MyCost.CostClassID));
                            if (model!=null)
                            {
                                if (model.ParentID.Equals(8))
                                {
                                    monthcost mod = new monthcost
                                    {
                                        AddTime = MyCost.AddTime,
                                        CostClassID = MyCost.CostClassID,
                                        CostID = MyCost.CostID,
                                        CostMoney = MyCost.CostMoney,
                                        CostName = MyCost.CostName,
                                        ClassName = model.ClassName,
                                        Num = 0,
                                        Note = MyCost.CostContent,
                                        IsDel  =0,
                                         UnitPrice =0
                                    };
                                    dal.Add<monthcost>(mod);
                                }
                            }
                           
                            #endregion
                            uibase.MessageBox("添加信息成功!");
                        }
                        else
                        {
                            dal.Update<cost>(MyCost);
                            uibase.MessageBox("修改信息成功!");
                        }
                        ClosePage(MyCost.CostID.ToString());
                        var msg = new NotificationMessage(null, Notifications.Refresh);
                        Messenger.Default.Send<NotificationMessage, CostMainViewModel>(msg);
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
            Messenger.Default.Send<NotificationMessage<string>, EditCostMain>(msg);
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

        private DateTime _AddTime = DateTime.Now;
        public DateTime AddTime 
        {
            get { return _AddTime; }
            set
            {
                _AddTime = value;
                this.RaisePropertyChanged("AddTime");
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