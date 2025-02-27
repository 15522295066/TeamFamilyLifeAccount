﻿using System;
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

namespace FamilyLifeAccount.View.Settings
{
    /// <summary>
    /// CostClassManage.xaml 的交互逻辑
    /// </summary>
    public partial class CostClassManage : UserControl
    {
        public CostClassManage()
        {
            InitializeComponent();
        }

        private void GetIndex(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        
    }
}