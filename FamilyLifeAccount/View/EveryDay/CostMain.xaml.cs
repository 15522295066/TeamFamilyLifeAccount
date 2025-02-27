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
using DataFactory.MODEL;
using Microsoft.Windows.Controls.Ribbon;
using GalaSoft.MvvmLight.Messaging;
using FamilyLifeAccount.Comm;

namespace FamilyLifeAccount.View.EveryDay
{
    /// <summary>
    /// CostMain.xaml 的交互逻辑
    /// </summary>
    public partial class CostMain : UserControl
    {
        public CostMain()
        {
            InitializeComponent();
           
        }
        private string _styledTitle = "Extended WPF Toolkit MessageBox";
        private string _styledMessage = "The Toolkit MessageBox allows you to style it in order to integrate it into your application colors and styles.";
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
