using DataFactory.DAL;
using DataFactory.MODEL;
using FamilyLifeAccount.Comm;
using FamilyLifeAccount.ViewModel.EveryDay;
using GalaSoft.MvvmLight.Messaging;
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

namespace FamilyLifeAccount.View.EveryDay
{
    /// <summary>
    /// CompanyEditEarningMain.xaml 的交互逻辑
    /// </summary>
    public partial class CompanyEditEarningMain : UserControl
    {
        UIBase uibase = new UIBase();
        DALBase dal = new DALBase();
        public CompanyEditEarningMain()
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
            else if (msg.Notification.Equals(Notifications.Parameter))
            {
                int id = int.Parse(msg.Content);
                Menu2.Header = dal.GetOneModel<View_EarningList>(m => m.EarningID==id).ClassName;
            }
        }

        #region 初始化分类菜单
        /// <summary>
        /// 初始化分类菜单
        /// </summary>
        private void CreateMenu()
        {
            using (FamilyLifeAccountEntities db = new FamilyLifeAccountEntities())
            {
                List<EarningClass> earning = db.EarningClass.Where(m => m.ParentID.Equals(25)).OrderBy(m => m.Sort).ToList();
                // Menu2.Style = Resources["MenuItemStyle"] as Style;
                foreach (var earn in earning)
                {
                    //RibbonApplicationMenuItem item = new RibbonApplicationMenuItem();
                    MenuItem item = new MenuItem();
                    item.Style = Resources["MenuItemStyle"] as Style;

                    item.Header = earn.ClassName;
                    item.Tag = earn.EarningClassID;
                    List<EarningClass> child = db.EarningClass.Where(m => m.ParentID.Equals(earn.EarningClassID)).OrderBy(m => m.Sort).ToList();
                    foreach (var childcost in child)
                    {
                        MenuItem childitem = new MenuItem();
                        childitem.Style = Resources["MenuItemStyle"] as Style;
                        item.FontSize = 13;
                        childitem.Click += new RoutedEventHandler(item_Click);
                        childitem.Header = childcost.ClassName;
                        childitem.Tag = childcost.EarningClassID;
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
                //向EditEarningMainViewModel发送改变ClassID消息
                var msg = new NotificationMessage<string>(classid, Notifications.Parameter);
                Messenger.Default.Send<NotificationMessage<string>, CompanyEditEarningViewModel>(msg);
            }
        }
        #endregion
    }
}
