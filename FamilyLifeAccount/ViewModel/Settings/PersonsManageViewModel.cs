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
    public class PersonsManageViewModel : ViewModelBase
    {
        DALBase dal = new DALBase();

        /// <summary>
        /// Initializes a new instance of the PersonsManageViewModel class.
        /// </summary>
        public PersonsManageViewModel()
        {
            if (!IsInDesignMode)
            {
                InitLoad();
                Messenger.Default.Register<NotificationMessage>(this, (msg) => Refresh(msg));
                QueryCommand = new RelayCommand<string>(msg => QueryList());
                ShowDialogCommand = new RelayCommand<string>((id) => ShowDialog(id));
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
            EditPersonsManage uc = new EditPersonsManage();
            var pop = UIBase.AddPopWindow(uc);
            pop.Closed += (s, e) => ViewModelLocator.ClearEditPersonsManage();
            if (string.IsNullOrWhiteSpace(id))
            {
                var msg = new NotificationMessage<string>(id, Notifications.AddShow);
                Messenger.Default.Send<NotificationMessage<string>, EditPersonsManageViewModel>(msg);
            }
            else
            {
                var msg = new NotificationMessage<string>(id, Notifications.UpdateShow);
                Messenger.Default.Send<NotificationMessage<string>, EditPersonsManageViewModel>(msg);
            }
            pop.ShowDialog();
        }
        public override void Cleanup()
        {
            base.Cleanup();
        }

        private void QueryList()
        {
            var sql = dal.GetList<persons>();
            if (!string.IsNullOrWhiteSpace(Key))
            {
                sql = sql.Where(m => m.UserName.Contains(Key)).ToList();
            }
            PersonsList = sql;
        }

        #endregion

        #region 属性初始化

       

         
        private List<persons> _PersonsList;
        public List<persons> PersonsList
        {
            get { return _PersonsList; }
            set
            {
                _PersonsList = value;
                this.RaisePropertyChanged("PersonsList");
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