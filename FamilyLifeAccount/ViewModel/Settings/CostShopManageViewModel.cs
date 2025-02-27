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
using FamilyLifeAccount.Model;
using FamilyLifeAccount.Common;
using System.Collections.ObjectModel;

namespace FamilyLifeAccount.ViewModel.Settings
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
    public class CostShopManageViewModel : ViewModelBase
    {
       DALBase dal = new DALBase();

        /// <summary>
        /// Initializes a new instance of the PersonsManageViewModel class.
        /// </summary>
       public CostShopManageViewModel()
        {
            if (!IsInDesignMode)
            {
                InitLoad();
                Messenger.Default.Register<NotificationMessage>(this, (msg) => Refresh(msg));
                QueryCommand = new RelayCommand<string>(msg => QueryList());
                ShowDialogCommand = new RelayCommand<string>((id) => ShowDialog(id));
                DeleteCommand = new RelayCommand<string>((id) => Delete(id));
            }
            else
            {

            }
        }

        #region 方法函数初始化
        private void InitLoad()
        {
            QueryList();
        }
        private void ShowDialog(string id)
        {
            EditCostShopManage uc = new EditCostShopManage();
            PopUpWindow pop = new PopUpWindow(uc);
            pop.Closed += (s, e) => ViewModelLocator.ClearEditCostShopManage();
            if (string.IsNullOrWhiteSpace(id))
            {
                var msg = new NotificationMessage<string>(id, Notifications.AddShow);
                Messenger.Default.Send<NotificationMessage<string>, EditCostShopManageViewModel>(msg);
            }
            else
            {
                //发送EditCostShopManageViewModel获取实体对象
                var msg = new NotificationMessage<string>(id, Notifications.UpdateShow);
                Messenger.Default.Send<NotificationMessage<string>, EditCostShopManageViewModel>(msg);
                //发送前台通知分类名称
                var msg2 = new NotificationMessage<string>(id, Notifications.Parameter);
                Messenger.Default.Send<NotificationMessage<string>, EditCostShopManage>(msg2);
            }
            pop.ShowDialog();
        }
        public override void Cleanup()
        {
            base.Cleanup();
        }

        private void QueryList()
        {
            var sql = dal.GetList<view_shopslist>();
            if (!string.IsNullOrWhiteSpace(Key))
            {
                sql = sql.Where(m => m.ShopName.Contains(Key)).ToList();
            }
            ShopsList = new ObservableCollection<view_shopslist>(sql);
        }

        private void Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                // Xceed.Wpf.Toolkit.MessageBox.Show(MessageEnum.确定要删除此条记录吗.ToString(), ModelEnum.支出.ToString(), MessageBoxButton.OKCancel);
                if (MessageBoxResult.OK == Xceed.Wpf.Toolkit.MessageBox.Show(MessageEnum.确定要删除此条记录吗.ToString(), ModelEnum.支出.ToString(), MessageBoxButton.OKCancel))
                {
                    //int ID = int.Parse(id);
                    //ShopModel = dal.GetOneModel<shops>(m => m.CostID.Equals(ID));
                    //ShopModel.IsDel = 1;
                    //dal.Update<shops>(ShopModel);
                    //GetList();
                }
            }
        }
        private void Refresh(NotificationMessage msg)
        {
            //接收从CostMainViewModel发来的分类信息名称的消息
            if (msg.Notification.Equals(Notifications.Refresh))
            {
                QueryList();
            }
        }

        #endregion

        #region 属性初始化

        private ObservableCollection<view_shopslist> _ShopsList;
        public ObservableCollection<view_shopslist> ShopsList
        {
            get { return _ShopsList; }
            set
            {
                _ShopsList = value;
                this.RaisePropertyChanged("ShopsList");
            }
        }
  
        private string _key;
        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                this.RaisePropertyChanged("Key");
            }
        }

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



        #endregion

        #region 命令初始化
        public RelayCommand<string> QueryCommand { get; set; }
        public RelayCommand<string> ShowDialogCommand { get; set; }
        public RelayCommand<string> DeleteCommand { get; set; }
        public RelayCommand<string> ChangedCommand { get; set; }

        #endregion


    }
}