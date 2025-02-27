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
    /// EditMonthCostMain.xaml 的交互逻辑
    /// </summary>
    public partial class EditMonthCostMain : UserControl
    {
        UIBase uibase = new UIBase();
        public EditMonthCostMain()
        {
            InitializeComponent();
            CreateMenu();
            Messenger.Default.Register<NotificationMessage<string>>(this, (msg) => ReceiveMsg(msg));
        }


        private void CreateMenu()
        {
            using (familylifeaccountEntities db = new familylifeaccountEntities())
            {
                List<costclass> costlist = db.costclass.Where(m => m.ParentID.Equals(8)).ToList();
                // Menu2.Style = Resources["MenuItemStyle"] as Style;
                foreach (var cost in costlist)
                {
                    //RibbonApplicationMenuItem item = new RibbonApplicationMenuItem();
                    MenuItem item = new MenuItem();
                    item.Style = Resources["MenuItemStyle"] as Style;

                    item.Header = cost.ClassName;
                    item.Tag = cost.CostClassID;
                    List<costclass> child = db.costclass.Where(m => m.ParentID.Equals(cost.CostClassID)).ToList();
                    item.Click += new RoutedEventHandler(item_Click);
                    foreach (var childcost in child)
                    {
                        MenuItem childitem = new MenuItem();
                        childitem.Style = Resources["MenuItemStyle"] as Style;
                        item.FontSize = 13;
                      
                        childitem.Header = childcost.ClassName;
                        childitem.Tag = childcost.CostClassID;
                        item.Items.Add(childitem);
                    }
                    Menu2.Items.Add(item);
                }
            }
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
                //向EditCostMainViewModel发送改变ClassID消息
                var msg = new NotificationMessage<string>(classid, Notifications.Parameter);
                Messenger.Default.Send<NotificationMessage<string>, EditMonthCostMainViewModel>(msg);
            }
        }

        private void ReceiveMsg(NotificationMessage<string> msg)
        {
            //接收从CostMainViewModel发来的分类信息名称的消息
            if (msg.Notification.Equals(Notifications.Parameter))
            {
                using (familylifeaccountEntities db = new familylifeaccountEntities())
                {
                    int id = int.Parse(msg.Content);
                    Menu2.Header = db.monthcost.Where(m => m.ID.Equals(id)).FirstOrDefault().ClassName;
                }
            }
            //接收从EditCostMainViewModel发来关闭消息
            if (msg.Notification.Equals(Notifications.Close))
            {
                //ViewModelLocator.ClearEditCostMain();
                ((Window)this.Parent).Close();
            }
        }
    }
}
