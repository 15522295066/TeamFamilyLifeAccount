
using FamilyLifeAccount.Comm;
using System;
using System.IO;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FamilyLifeAccount.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
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
    public class MainViewModel : ViewModelBase
    {
        #region 构造函数
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (!IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                //GetBackground();
                SubmitCommand = new RelayCommand(() => Submit());
                CloseTabItemCommand = new RelayCommand<object>((tabitem) => CloseTabItem(tabitem));
            }
            else
            {
                // Code runs "for real"
            }
        }

        private void CloseTabItem(object tabitem)
        {
            NotificationMessage<object> msg = new NotificationMessage<object>(tabitem, Notifications.Close);
            Messenger.Default.Send<NotificationMessage<object>, MainWindow>(msg);
        }
        #endregion

        #region 命令初始化
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand<object> CloseTabItemCommand { get; set; }
        #endregion

        #region 依赖属性初始化

        private string _Background;
        public string Background
        {
            get { return _Background; }
            set
            {
                _Background = value;
                this.RaisePropertyChanged("Background");
            }
        }
        #endregion

        #region 方法函数初始化

        private void GetBackground()
        {
            Background = UIBase.RandomBackground();
        }


        private void Submit()
        {
            GetBackground();
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }

        #endregion
    }
}