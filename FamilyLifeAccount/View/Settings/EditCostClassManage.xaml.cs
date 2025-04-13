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
using FamilyLifeAccount.ViewModel.Settings;

namespace FamilyLifeAccount.View.Settings
{
    /// <summary>
    /// EditCostClassManage.xaml 的交互逻辑
    /// </summary>
    public partial class EditCostClassManage : UserControl
    {
        UIBase uibase = new UIBase();
        public EditCostClassManage()
        {
            InitializeComponent();
            CreateMenu();
            Messenger.Default.Register<NotificationMessage<string>>(this, (msg) => ReceiveMsg(msg));
        }

        private bool _IsAdd = true;
        public bool IsAdd
        {
            get { return _IsAdd = true; }
            set { _IsAdd = value; }
        }


        private void ReceiveMsg(NotificationMessage<string> msg)
        {
            //接收从CostClassManageViewModel发的信息
            if (msg.Notification.Equals(Notifications.UpdateShow))
            {
                IsAdd = false;
            }
            //接收从CostShopManageViewModel发来的分类信息名称的消息
            if (msg.Notification.Equals(Notifications.Parameter))
            {
                using (familylifeaccountEntities db = new familylifeaccountEntities())
                {
                    int id = int.Parse(msg.Content);
                    Menu2.Header = db.view_shopslist.Where(m => m.ShopID.Equals(id)).FirstOrDefault().ClassName;
                }
            }
            //接收从EditCostMainViewModel发来关闭消息
            if (msg.Notification.Equals(Notifications.Close))
            {
                //ViewModelLocator.ClearEditCostMain();
                ((Window)this.Parent).Close();
            }
        }
        private void CreateMenu()
        {
            Menu2.Style = Resources["MenuItemStyle"] as Style;
            UIBase.TreeMenuCostclass(Menu2, 1, item_Click);
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

                if (IsAdd)
                {
                    var msg = new NotificationMessage<string>(classid, Notifications.AddShow);
                    Messenger.Default.Send<NotificationMessage<string>, EditCostClassManageViewModel>(msg);
                }
                else {
                    var msg = new NotificationMessage<string>(classid, Notifications.UpdateShow);
                    Messenger.Default.Send<NotificationMessage<string>, EditCostClassManageViewModel>(msg);
                }

                //向EditCostShopManageViewModel发送改变ClassID消息
              
            }
        }

    }
}
