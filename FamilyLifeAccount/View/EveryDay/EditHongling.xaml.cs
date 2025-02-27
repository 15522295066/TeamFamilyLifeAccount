using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using FamilyLifeAccount.Comm;
using FamilyLifeAccount.ViewModel;
using DataFactory.MODEL;
using FamilyLifeAccount.ViewModel.EveryDay;

namespace FamilyLifeAccount.View.EveryDay
{
    /// <summary>
    /// EditHongling.xaml 的交互逻辑
    /// </summary>
    public partial class EditHongling : UserControl
    {
        public EditHongling()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage<string>>(this, (msg) => ReceiveMsg(msg));
        }
        private void ReceiveMsg(NotificationMessage<string> msg)
        {
            //接收从EditCostMainViewModel发来关闭消息
            if (msg.Notification.Equals(Notifications.Close))
            {
                //ViewModelLocator.ClearEditCostMain();
                ((Window)this.Parent).Close();
            }
        }
    }
}
