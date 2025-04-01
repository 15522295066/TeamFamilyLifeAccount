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
            using (familylifeaccountEntities db = new familylifeaccountEntities())
            {
                List<costclass> costlist = db.costclass.Where(m => m.ParentID.Equals(0) && m.IsCompany == 1).OrderBy(m => m.Sort).ToList();
                // Menu2.Style = Resources["MenuItemStyle"] as Style;
                foreach (var cost in costlist)
                {
                    //RibbonApplicationMenuItem item = new RibbonApplicationMenuItem();
                    MenuItem item = new MenuItem();
                    item.Name = "item1";
                    item.Style = Resources["MenuItemStyle"] as Style;
                    item.Header = cost.ClassName;
                    item.Tag = cost.CostClassID;
                    List<costclass> child = db.costclass.Where(m => m.ParentID.Equals(cost.CostClassID)).OrderBy(m => m.Sort).ToList();
                    item.Click += new RoutedEventHandler(item_Click);
                    item.Width = 200;
                    #region 二级菜单
                    foreach (var childcost in child)
                    {
                        MenuItem item2 = new MenuItem();
                        item2.Style = Resources["MenuItemStyle"] as Style;
                        item.FontSize = 13;
                        item2.Click += new RoutedEventHandler(item_Click);
                        item2.Header = childcost.ClassName;
                        item2.Tag = childcost.CostClassID;
                        item.Items.Add(item2);
                        List<costclass> thirdchild = db.costclass.Where(m => m.ParentID.Equals(childcost.CostClassID)).OrderBy(m => m.Sort).ToList();
                        #region 三级菜单
                        foreach (var third in thirdchild)
                        {
                            MenuItem item3 = new MenuItem();
                            item3.Style = Resources["MenuItemStyle"] as Style;
                            item.FontSize = 13;
                            item3.Click += new RoutedEventHandler(item_Click);
                            item3.Header = third.ClassName;
                            item3.Tag = third.CostClassID;
                            item2.Items.Add(item3);
                        }
                        #endregion
                    }
                    #endregion
                    Menu1.Items.Add(item);

                }
            }
        }

        void item_Click(object sender, RoutedEventArgs e)
        {
            MenuItem ob = e.OriginalSource as MenuItem;
            //Menu1.Header = ob.Header.ToString();
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
                Messenger.Default.Send<NotificationMessage<string>, CompanyEditCostMain>(msg);
                Messenger.Default.Send<NotificationMessage<string>, CompanyEditCostMainViewModel>(msg);
            }
        }
        #endregion


    }
}