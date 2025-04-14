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
    /// EditCostMain.xaml 的交互逻辑
    /// </summary>
    public partial class EditCostMain : UserControl
    {
        UIBase uibase = new UIBase();
        public EditCostMain()
        {
            InitializeComponent();
            CreateMenu();
            Messenger.Default.Register<NotificationMessage<string>>(this, (msg) => ReceiveMsg(msg));
        }

        private void ReceiveMsg(NotificationMessage<string> msg)
        {
            //接收从CostMainViewModel发来的分类信息名称的消息
            if (msg.Notification.Equals(Notifications.Parameter))
            {
                using (familylifeaccountEntities db = new familylifeaccountEntities())
                {
                    int id = int.Parse(msg.Content);
                    //Menu1.Header = db.view_costlist.Where(m => m.CostID.Equals(id)).FirstOrDefault().ClassName;
                }
            }
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
            UIBase.TreeMenuEveryCostclass(MenuItem1, 1, item_Click);
        }


        void item_Click(object sender, RoutedEventArgs e)
        {
            MenuItem ob = e.OriginalSource as MenuItem;
            MenuItem1.Header = ob.Header.ToString();
            string classid = ob.Tag.ToString();
            if (classid.Equals("0"))
            {
                uibase.MessageBox("请选择支出分类!");
            }
            else
            {
                //向EditCostMainViewModel发送改变ClassID消息
                var msg = new NotificationMessage<string>(classid, Notifications.Parameter);
                Messenger.Default.Send<NotificationMessage<string>, EditCostMainViewModel>(msg);
                if (combobox1.Items.Count>0)
                {
                    //根据分类ID查询商户默认第一个
                    combobox1.SelectedIndex = 0;
                }
            }
        }
        #endregion


    }
}