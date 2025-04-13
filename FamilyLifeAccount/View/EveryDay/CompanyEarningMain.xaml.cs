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
    /// CompanyEarningMain.xaml 的交互逻辑
    /// </summary>
    public partial class CompanyEarningMain : UserControl
    {
        public CompanyEarningMain()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //FrameworkElement d = new FrameworkElement(); 
            //System.Windows.Style style = (System.Windows.Style)this.Resources["messageBoxStyle"];
            //Xceed.Wpf.Toolkit.MessageBox.Show(null, _styledMessage, _styledTitle, style);
        }
        private void GetIndex(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
