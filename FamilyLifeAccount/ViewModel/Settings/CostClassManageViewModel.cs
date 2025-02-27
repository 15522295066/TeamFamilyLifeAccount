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
    public class CostClassManageViewModel : ViewModelBase
    {
        DALBase dal = new DALBase();

        /// <summary>
        /// Initializes a new instance of the CostClassManageViewModel class.
        /// </summary>
        public CostClassManageViewModel()
        {
            if (!IsInDesignMode)
            {
                InitLoad();
                Messenger.Default.Register<NotificationMessage>(this, (msg) => Refresh(msg));
                QueryCommand = new RelayCommand<string>(msg => QueryList());
                ShowShowCommand = new RelayCommand<string>((id) => ShowShowDialog(id));
            }
            else
            {

            }
        }

        private void Refresh(NotificationMessage msg)
        {
            if (msg.Notification.Equals(Notifications.Refresh))
            {
                QueryList();
            }
        }

       


        #region 方法函数初始化
        private void InitLoad()
        {
            QueryList();
        }
        private void ShowShowDialog(string id)
        {
            EditCostClassManage uc = new EditCostClassManage();
            var pop = UIBase.AddPopWindow(uc);
            pop.Closed += (s, e) => ViewModelLocator.ClearEditCostClassManage();

            NotificationMessage<string> msg ;
            if (string.IsNullOrEmpty(id))
            {
                 msg = new NotificationMessage<string>(id, Notifications.AddShow);
            }
            else
            {
                 msg = new NotificationMessage<string>(id, Notifications.UpdateShow);
            }
            Messenger.Default.Send<NotificationMessage<string>, EditCostClassManageViewModel>(msg);
            Messenger.Default.Send<NotificationMessage<string>, EditCostClassManage>(msg);
            pop.ShowDialog();
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }

        private void QueryList()
        {
            var sql = from cc in dal.GetList<costclass>(m => !m.ParentID.Equals(0))
                      orderby cc.CostClassID descending
                      select new MyCostClass
                      {
                          CostClassID=cc.CostClassID,
                          ParentName = dal.GetOneModel<costclass>(m => m.CostClassID.Equals(cc.ParentID)).ClassName,
                          ClassName = cc.ClassName
                      };
            if (!string.IsNullOrWhiteSpace(Key))
            {
                sql = sql.Where(m => m.ClassName.Contains(Key.Trim()) || m.ParentName.Contains(Key.Trim())).ToList();
            }
            CostClassList = sql.ToList();
        }

        #endregion

        #region 属性初始化

        private List<MyCostClass> _CostClassList;
        public List<MyCostClass> CostClassList
        {
            get { return _CostClassList; }
            set
            {
                _CostClassList = value;
                this.RaisePropertyChanged("CostClassList");
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
        public RelayCommand<string> ShowShowCommand { get; set; }
        public RelayCommand<string> DeleteCommand { get; set; }
        public RelayCommand<string> ChangedCommand { get; set; }

        #endregion


    }
}