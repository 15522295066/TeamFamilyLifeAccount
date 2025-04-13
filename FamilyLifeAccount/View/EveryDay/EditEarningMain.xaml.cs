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
using FamilyLifeAccount.ViewModel;
using DataFactory.MODEL;
using FamilyLifeAccount.ViewModel.EveryDay;

namespace FamilyLifeAccount.View.EveryDay
{
    /// <summary>
    /// EditEarningMain.xaml 的交互逻辑
    /// </summary>
    public partial class EditEarningMain : UserControl
    {
        UIBase uibase = new UIBase();
        public EditEarningMain()
        {
            InitializeComponent();
            CreateMenu();
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

        #region 初始化分类菜单
        /// <summary>
        /// 初始化分类菜单
        /// </summary>
        private void CreateMenu()
        {
            Menu2.Style = Resources["MenuItemStyle"] as Style;
            UIBase.TreeMenuEarningclass(Menu2, 1, item_Click);
        }

        void item_Click(object sender, RoutedEventArgs e)
        {
            MenuItem ob = e.OriginalSource as MenuItem;
            Menu2.Header = ob.Header.ToString();
            string classid = ob.Tag.ToString();
            if (classid.Equals("0"))
            {

                uibase.MessageBox("请选择支出分类!");
            }
            else
            {
                //向EditEarningMainViewModel发送改变ClassID消息
                var msg = new NotificationMessage<string>(classid, Notifications.Parameter);
                Messenger.Default.Send<NotificationMessage<string>, EditEarningViewModel>(msg);
            }
        }
        #endregion
    }
}
