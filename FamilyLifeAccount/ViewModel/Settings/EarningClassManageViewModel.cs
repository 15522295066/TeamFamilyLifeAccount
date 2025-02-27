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
    public class EarningClassManageViewModel : ViewModelBase
    {
          
            DALBase dal = new DALBase();

        /// <summary>
        /// Initializes a new instance of the PersonsManageViewModel class.
        /// </summary>
        public EarningClassManageViewModel()
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

        private void Refresh(NotificationMessage msg)
        {
            if (msg.Notification.Equals(Notifications.Refresh))
            {
                QueryList();
            }
        }

        private void InitLoad()
        {
            QueryList();
        }
        private void ShowDialog(string id)
        {
            EditEarningClassManage uc = new EditEarningClassManage();
            var pop = UIBase.AddPopWindow(uc);
            pop.Closed += (s, e) => ViewModelLocator.ClearEditEarningClassViewModel();
            if (string.IsNullOrWhiteSpace(id))
            {
                var msg = new NotificationMessage<string>(id, Notifications.AddShow);
                Messenger.Default.Send<NotificationMessage<string>, EditEarningClassManageViewModel>(msg);
            }
            else
            {
                var msg = new NotificationMessage<string>(id, Notifications.UpdateShow);
                Messenger.Default.Send<NotificationMessage<string>, EditEarningClassManageViewModel>(msg);
            }
            pop.ShowDialog();
        }
        public override void Cleanup()
        {
            base.Cleanup();
        }

        private void QueryList()
        {
            var sql = dal.GetList<earningclass>();
            if (!string.IsNullOrWhiteSpace(Key))
            {
                sql = sql.Where(m => m.ClassName.Contains(Key)).ToList();
            }
            EarningClassList = sql;
        }

        private void Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                // Xceed.Wpf.Toolkit.MessageBox.Show(MessageEnum.确定要删除此条记录吗.ToString(), ModelEnum.支出.ToString(), MessageBoxButton.OKCancel);
                if (MessageBoxResult.OK == Xceed.Wpf.Toolkit.MessageBox.Show(MessageEnum.确定要删除此条记录吗.ToString(), ModelEnum.收入.ToString(), MessageBoxButton.OKCancel))
                {
                    //int ID = int.Parse(id);
                    //ShopModel = dal.GetOneModel<shops>(m => m.CostID.Equals(ID));
                    //ShopModel.IsDel = 1;
                    //dal.Update<shops>(ShopModel);
                    //GetList();
                }
            }
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


        #endregion

        #region 命令初始化
        public RelayCommand<string> QueryCommand { get; set; }
        public RelayCommand<string> ShowDialogCommand { get; set; }
        public RelayCommand<string> DeleteCommand { get; set; }
        public RelayCommand<string> ChangedCommand { get; set; }

        #endregion


    }
}