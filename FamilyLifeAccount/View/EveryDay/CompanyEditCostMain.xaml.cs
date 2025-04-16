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
using DataFactory.DAL;
using FamilyLifeAccount.Model;
using System.ComponentModel.Design;

namespace FamilyLifeAccount.View.EveryDay
{
    /// <summary>
    /// CompanyEditCostMain.xaml 的交互逻辑
    /// </summary>
    public partial class CompanyEditCostMain : UserControl
    {

        UIBase uibase = new UIBase();
        public CompanyEditCostMain()
        {
            InitializeComponent();
            CreateMenu();
            Messenger.Default.Register<NotificationMessage<string>>(this, (msg) => ReceiveMsg(msg));
        }

        private void ReceiveMsg(NotificationMessage<string> msg)
        {
            //接收从CompanyCostMainViewModel发来的分类信息名称的消息
            if (msg.Notification.Equals(Notifications.Parameter))
            {
                DALBase dal = new DALBase();
                int id = int.Parse(msg.Content);
                MenuItem menuItem = msg.Sender as MenuItem;
                if (menuItem != null)
                {
                    int costId = Convert.ToInt32(menuItem.Tag);
                    menuItem.Header = menuItem.Header;

                    //Menu1.Items.Add(menuItem);
                }

            }
            //接收从CompanyEditCostMainViewModel发来关闭消息
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
            UIBase.TreeMenuCostclass(Menu2, 2, item_Click);
        }

        void item_Click(object sender, RoutedEventArgs e)
        {
            MenuItem ob = e.OriginalSource as MenuItem;
            Menu2.Header = ob.Header.ToString();
            string classid = ob.Tag.ToString();
            company company = (company)com_pany.SelectedItem as company;

            if (classid.Equals("0"))
            {
                uibase.MessageBox("请选择支出分类!");
            }
            else
            {
                string companyId = company.CompanyId;
                //向CompanyEditCostMainViewModel发送改变ClassID消息
                var msg = new NotificationMessage<string>(companyId, classid, Notifications.Parameter);
               // Messenger.Default.Send<NotificationMessage<string>, CompanyEditCostMain>(msg);
                Messenger.Default.Send<NotificationMessage<string>, CompanyEditCostMainViewModel>(msg);
            }
        }
        #endregion


    }
}