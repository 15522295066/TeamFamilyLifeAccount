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
using System.Windows.Shapes;

namespace FamilyLifeAccount.Comm
{
    /// <summary>
    /// PopUpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PopUpWindow : Window
    {
        public PopUpWindow()
        {
            InitializeComponent();
        }

        public PopUpWindow(UserControl uc):base ()
        {
            //Rect adornedElementRect = new Rect(uc.RenderSize);
            this.WindowStyle = WindowStyle.None;
            this.Background = System.Windows.Media.Brushes.Transparent;
            //this.Opacity = 0.5;
            this.AllowsTransparency = true;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //System.Windows.Media.Effects.DropShadowEffect ds = new System.Windows.Media.Effects.DropShadowEffect();
            //ds.ShadowDepth = 5;
            //ds.Color = System.Windows.Media.Colors.Black;
            //ds.RenderingBias = System.Windows.Media.Effects.RenderingBias.Quality;
            //ds.BlurRadius = 10;
            //ds.Direction = 315;
            //ds.Opacity = 1;
            //this.Effect = ds;
            this.Height = uc.Height;
            this.Width = uc.Width;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Content = uc;
            this.Topmost = true;
            this.MouseLeftButtonDown += delegate { this.DragMove(); };
            //this.Closing += delegate { this.IsClosing(); };
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
