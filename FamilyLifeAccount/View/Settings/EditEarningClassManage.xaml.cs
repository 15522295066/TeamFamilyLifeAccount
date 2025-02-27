using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace FamilyLifeAccount.View.Settings
{
    /// <summary>
    /// EditEarningClassManage.xaml 的交互逻辑
    /// </summary>
    public partial class EditEarningClassManage : UserControl
    {

        public EditEarningClassManage()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage<object>>(this, (msg) => ReceiveMsg(msg));
        }


        private void ReceiveMsg(NotificationMessage<object> msg)
        {
            //接收从EditEarningClassManageViewModel发来关闭消息
            if (msg.Notification.Equals(Notifications.Close))
            {
                //ViewModelLocator.ClearEditCostMain();
                ((Window)this.Parent).Close();
            }
        }

    }
}