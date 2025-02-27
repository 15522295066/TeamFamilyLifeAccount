using System;
using System.Windows;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using WPF.Themes;
using Microsoft.WindowsAPICodePack.Shell;
using DataFactory.MODEL;
using DataFactory.DAL;
using FamilyLifeAccount.Comm;
using GalaSoft.MvvmLight.Messaging;
using FamilyLifeAccount.View.Share;
using FamilyLifeAccount.ViewModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace FamilyLifeAccount
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        readonly static bool IsShowBackGround = false;//背景图开关

        public MainWindow()
        {
            InitializeComponent();
            BeginInvokeCreateBackground(); 
            InitLoad();
            Messenger.Default.Register<NotificationMessage<object>>(this, (msg) => CloseTabItem(msg));
        }

        #region 数据初始化

        private void InitLoad()
        {
            try
            {
                load.BackgroundLoading(new ParameterizedThreadStart(m =>
                {
                    //Thread.Sleep(1000);
                    Dispatcher.BeginInvoke(new ThreadStart(CreateBackground), DispatcherPriority.Normal);
                    Dispatcher.BeginInvoke(new ThreadStart(InitializeMenu), DispatcherPriority.Normal);
                    Dispatcher.BeginInvoke(new ThreadStart(LaunchTimer),DispatcherPriority.Normal);
            
                   
                }), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 初始化树状导航
        /// </summary>
        private void InitializeMenu()
        {
            accordion1.Items.Clear();
            tabControl1.Items.Clear();
            List<navigation> list = new List<navigation>();
            using (familylifeaccountEntities db = new familylifeaccountEntities())
            {
                //父级数据源
                list = db.navigation.Where(m => m.ParentID.Equals(0)).OrderBy(m => m.Sort).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    AccordionItem ai = new AccordionItem();

                    ai.Header = "  " + list[i].Title;
                    string style = string.Format("AccordionItemStyle");
                    ai.Style = Resources[style] as Style;
                    ScrollViewer scroll = new ScrollViewer();
                    scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    StackPanel sp = new StackPanel();
                    //子级数据源
                    int chaildid = list[i].NavigationID;
                    List<navigation> child = db.navigation.Where(m => m.IsShow.Equals(1) && m.ParentID.Equals(chaildid)).OrderBy(m => m.Sort).ToList();
                    foreach (var it in child)
                    {
                        Label lb = new Label();
                        lb.Content = it.Title;
                        lb.Tag = it.AssemblyPath;
                        lb.Style = Resources["NavigationStyle"] as Style;
                        lb.MouseLeftButtonDown += new MouseButtonEventHandler(lb_MouseLeftButtonDown);
                        sp.Children.Add(lb);
                    }
                    scroll.Content = sp;
                    ai.Content = scroll;
                    accordion1.Items.Add(ai);
                }
            }
        }

        /// <summary>
        /// 反射实例UserControl
        /// </summary>
        /// <param name="usercontrol"></param>
        /// <returns></returns>
        private UserControl CreateUserControl(string usercontrol)
        {
            string assembly = Assembly.GetExecutingAssembly().GetName().Name;
            object obj = Assembly.Load(assembly).CreateInstance(usercontrol, false);
            UserControl child = (UserControl)obj;
            return child;
        }


        /// <summary>
        /// 关闭选项卡
        /// </summary>
        /// <param name="msg"></param>
        private void CloseTabItem(NotificationMessage<object> msg)
        {
            CloseTab(msg.Content);
        }

        private void CloseTab(object sender)
        {
            TabItem tabitem = sender as TabItem;
            if (tabControl1.Items.Contains(tabitem))
            {
                tabControl1.Items.Remove(tabitem);
            }
        }
        #endregion

        #region 系统事件

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //必须在InitializeComponent之后创建
            NativeMethods.ExtendAeroGlass(this);
        }

        private void main_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void main_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //如果是全屏,则最小化
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                ResizeMode = ResizeMode.CanResizeWithGrip;//设置为可调整窗体大小
                Width = 1366;
                Height = 768;

            }
            //如果是窗口,则全屏
            else if (WindowState == WindowState.Normal)
            {
                //变成无边窗体
                WindowState = WindowState.Maximized;//假如已经是Maximized，就不能进入全屏，所以这里先调整状态
                ////解决切换应用程序的问题
                //Activated += new EventHandler(window_Activated);
                //Deactivated += new EventHandler(window_Deactivated);
            }

        }

        private void ImgExpand_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            //img.Source = new BitmapImage(new Uri("/images/" + item.LINKIMAGE + ".png", UriKind.Relative));
            double width = g.ColumnDefinitions[0].Width.Value;
            if (width > 32)
            {
                g.ColumnDefinitions[0].Width = new GridLength(32, GridUnitType.Pixel);
                img.Style = Resources["MainImgCollapseStyle"] as Style;
            }
            else
            {
                g.ColumnDefinitions[0].Width = new GridLength(150, GridUnitType.Pixel);
                img.Style = Resources["MainImgExpandStyle"] as Style;
            }
        }

        /// <summary>
        ///  可拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #region Windows 7 扩展玻璃效果（Aero Glass）
        public class NativeMethods
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct MARGINS
            {
                public int cxLeftWidth;
                public int cxRightWidth;
                public int cyTopHeight;
                public int cyBottomHeight;
            };

            [DllImport("DwmApi.dll")]
            private static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);
            public static void ExtendAeroGlass(Window window)
            {
                try
                {
                    // 为WPF程序获取窗口句柄
                    IntPtr mainWindowPtr = new WindowInteropHelper(window).Handle;
                    HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
                    mainWindowSrc.CompositionTarget.BackgroundColor = Colors.Transparent;
                    // 设置Margins
                    MARGINS margins = new MARGINS();
                    // 扩展Aero Glass
                    margins.cxLeftWidth = -1;
                    margins.cxRightWidth = -1;
                    margins.cyTopHeight = -1;
                    margins.cyBottomHeight = -1;
                    int hr = DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
                    if (hr < 0)
                    {
                        MessageBox.Show("DwmExtendFrameIntoClientArea Failed");
                    }
                }
                catch (DllNotFoundException)
                {
                    Application.Current.MainWindow.Background = Brushes.White;
                }
            }
        }
        #endregion

        /// <summary>
        /// 异步执行背景图方法
        /// </summary>
        /// <param name="dispatcher">默认后台执行</param>
        private void BeginInvokeCreateBackground()
        {
            load.BackgroundLoading(new ParameterizedThreadStart((d) =>
            {
               // Thread.Sleep(3000);
                //涉及UI控件调用，必须新建Dispatcher来指
                this.Dispatcher.BeginInvoke(new ThreadStart(CreateBackground));
                //this.load.BackgroundLoading(new AsyncHandler(CreateBackground));

            }), null);
        }
 
        private void CreateBackground()
        {
            System.Windows.Media.ImageBrush ib = new System.Windows.Media.ImageBrush();

            if (IsShowBackGround)
            {
                ib.ImageSource = new BitmapImage(new Uri(UIBase.RandomBackground(), UriKind.Relative));
                Background = ib;
            }
            else
            {
                LinearGradientBrush lgb = new LinearGradientBrush();
                lgb.Opacity = 0.5;
                lgb.StartPoint = new Point(1, 1);
                lgb.EndPoint = new Point(1, 0);
                //七彩设置
                GradientStop gs1 = new GradientStop(Colors.Crimson, 0.14);
                GradientStop gs2 = new GradientStop(Colors.Orange, 0.28);
                GradientStop gs3 = new GradientStop(Colors.Yellow, 0.42);
                GradientStop gs4 = new GradientStop(Colors.Green, 0.56);
                GradientStop gs5 = new GradientStop(Colors.Cyan, 0.70);
                GradientStop gs6 = new GradientStop(Colors.Blue, 0.84);
                GradientStop gs7 = new GradientStop(Colors.Purple, 0.98);
                lgb.GradientStops.Add(gs1);
                lgb.GradientStops.Add(gs2);
                lgb.GradientStops.Add(gs3);
                lgb.GradientStops.Add(gs4);
                lgb.GradientStops.Add(gs5);
                lgb.GradientStops.Add(gs6);
                lgb.GradientStops.Add(gs7);
                BorderALL.Background = lgb;
            }


        }

        private void CreateHead()
        {
            string[] str = { "/Images/Head/1.png", "/Images/Head/2.png", "/Images/Head/3.png", "images/4.png", "/Images/Head/5.png" };    // images
            //imageScrollView1.AddImages(str);
            //imageScrollView1.Enableslide = false;
        }


        /// <summary>
        /// 同步调用创建选项卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //调用loading控件，参数为指定执行的函数（通过委托来指派）
            load.BackgroundLoading(new ParameterizedThreadStart((d) =>
            {
                //Thread.Sleep(2000);
                //Dispatcher.BeginInvoke(new ThreadStart(CreateBackground), DispatcherPriority.Normal);
                ////涉及UI控件调用，必须新建Dispatcher来指派
                Dispatcher.Invoke(new ParameterizedThreadStart(CreateTab), DispatcherPriority.Normal, sender);

            }), sender);
        }

        #endregion

        #region Loading

        /// <summary>
        /// 创建标签页方法
        /// </summary>
        /// <param name="ob"></param>
        private void CreateTab(object ob)
        {
            Label lb = ((Label)ob);
            object xaml = lb.Tag;
            bool hastab = false;
            ItemCollection items = tabControl1.Items;
            if (items.Count > 0)
            {
                foreach (var item in tabControl1.Items)
                {
                    TabItem tab = (TabItem)item;
                    if (tab.Header.Equals(lb.Content))
                    {
                        tabControl1.SelectedItem = tab;
                        hastab = true;
                        break;
                    }
                }
            }
            if (hastab.Equals(false))
            {
                if (xaml != null)
                {
                    TabItem tab = new TabItem();
                    tab.Style = Resources["CloseTabItemStyle"] as Style;
                    UserControl uc = CreateUserControl(xaml.ToString());
                    tab.Content = uc;
                    tab.Header = lb.Content;
                    tabControl1.Items.Add(tab);
                    tabControl1.SelectedItem = tab;
                }
            }
        }


        private void LaunchTimer()
        {
            DispatcherTimer innerTimer = new DispatcherTimer(DispatcherPriority.Loaded,this.Dispatcher);
            innerTimer.Interval = TimeSpan.FromSeconds(30.0);
            innerTimer.Tick += new EventHandler(innerTimer_Tick);
            innerTimer.Start();
        }

        void innerTimer_Tick(object sender, EventArgs e)
        {
            CreateBackground();
        }

        #endregion

        
    }
}