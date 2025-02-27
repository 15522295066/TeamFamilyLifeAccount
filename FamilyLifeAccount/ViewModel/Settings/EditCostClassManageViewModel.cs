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

namespace FamilyLifeAccount.ViewModel.Settings
{
    public class EditCostClassManageViewModel : ViewModelBase
    {
        #region 构造函数
        DALBase dal = new DALBase();
        UIBase uibase = new UIBase();

        public EditCostClassManageViewModel()
        {
            if (!IsInDesignMode)
            {
                Messenger.Default.Register<NotificationMessage<string>>(this, (msg) => LoadInit(msg));
                SubCommand = new RelayCommand(() => Submit());
                CancelCommand = new RelayCommand<object>((uc) => Cancel(uc));
            }
            else
            {
                // Code runs "for real": Connect to service, etc...
            }
        }
        #endregion

        #region 命令初始化
        public RelayCommand SubCommand { get; set; }
        public RelayCommand<object> CancelCommand { get; set; }
        public RelayCommand<int> QueryCommand { get; set; }
        #endregion

        #region 属性初始化
        private List<costclass> _CostClassList;
        public List<costclass> CostClassList
        {
            get { return _CostClassList; }
            set
            {
                _CostClassList = value;
                this.RaisePropertyChanged("CostClassList");
            }
        }

        private costclass _MyCostClass = new costclass();
        public costclass MyCostClass
        {
            get { return _MyCostClass; }
            set
            {
                _MyCostClass = value;
                this.RaisePropertyChanged("MyCostClass");
            }
        }


        private string _ParentName ="请选择";
        public string ParentName
        {
            get { return _ParentName; }
            set
            {
                _ParentName = value;
                this.RaisePropertyChanged("ParentName");
            }
        }


        

        public bool IsAdd { get; set; }

        #endregion

        #region 方法函数初始化

        private void LoadInit(NotificationMessage<string> msg)
        {
            CostClassList = dal.GetList<costclass>(m => m.ParentID.Equals(0));
            if (msg.Notification.Equals(Notifications.AddShow))
            {
                IsAdd = true;
                try
                {
                    if (msg.Content != null)
                    {
                        MyCostClass.ParentID = int.Parse(msg.Content);
                        int ID = int.Parse(msg.Content);
                        //MyCostClass = dal.GetOneModel<costclass>(m => m.CostClassID.Equals(ID));
                    }
                }
                catch (Exception ex) { uibase.MessageBox(ex.Message); }
            }
            if (msg.Notification.Equals(Notifications.UpdateShow))
            {
                IsAdd = false;
                try
                {
                    int ID = int.Parse(msg.Content);
                    MyCostClass = dal.GetOneModel<costclass>(m => m.CostClassID.Equals(ID));
                    ParentName = dal.GetOneModel<costclass>(m => m.CostClassID.Equals(MyCostClass.ParentID)).ClassName;
                }
                catch (Exception ex) { uibase.MessageBox(ex.Message); }
            }
             
        }

        private void Cancel(object uc)
        {
            UIBase.Close(uc);
        }

        private void Submit()
        {
            if (IsAdd)
            {
                try
                {
                    MyCostClass.AddTime = DateTime.Now;
                    dal.Add<costclass>(MyCostClass);
                    uibase.MessageBox("新增分类信息成功");
                }
                catch (Exception ex) { uibase.MessageBox(ex.Message); }
            }
            else
            {
                try
                {
                    dal.Update<costclass>(MyCostClass);
                    uibase.MessageBox("更新分类信息成功");
                }
                catch (Exception ex) { uibase.MessageBox(ex.Message); }
            }
            ClosePage();
            var query = new NotificationMessage(Notifications.Refresh);
            Messenger.Default.Send<NotificationMessage, CostClassManageViewModel>(query);
           

        }

        /// <summary>
        /// 取消
        /// </summary>
        private void ClosePage()
        {
            var msg = new NotificationMessage(Notifications.Close);
            Messenger.Default.Send<NotificationMessage, EditCostClassManage>(msg);
        }
        public override void Cleanup()
        {
            base.Cleanup();
        }

        #endregion
    }
}
