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

using System.Data;
using Visifire.Charts;
using DataFactory.MODEL;
using FamilyLifeAccount.Comm;
using FamilyLifeAccount.View.Share;
using System.Threading;


namespace FamilyLifeAccount.View.Statistics
{
    /// <summary>
    /// CostClassChart.xaml 的交互逻辑
    /// </summary>
    public partial class CostClassChart : UserControl
    {
        public CostClassChart()
        {
            InitializeComponent();
            InitLoad();

        }
        UIBase uibase = new UIBase();

        #region 数据初始化

        string chartitle = "分类汇总统计";
        Chart chart;


        private void InitLoad()
        {
            string str = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, "01");

            DpSdate.SelectedDate = DateTime.Parse(str);
            DpEdate.SelectedDate = DateTime.Now.Date;

            using (familylifeaccountEntities db = new familylifeaccountEntities())
            {
                var sql = db.costclass.Where(m => m.ParentID.Equals(0)).ToList();
                sql.Insert(0, new costclass { CostClassID = 0, ClassName = "全部" });
                cbbParent.DisplayMemberPath = "ClassName";
                cbbParent.SelectedValuePath = "CostClassID";
                cbbParent.SelectedValue = sql[0].CostClassID;
                cbbParent.ItemsSource = sql;
            }
            cbbChild.Items.Insert(0, new costclass { CostClassID = 0, ClassName = "全部" });
            cbbChild.DisplayMemberPath = "ClassName";
            cbbChild.SelectedValuePath = "CostClassID";
            cbbChild.SelectedValue = 0;

            foreach (var item in Enum.GetValues(typeof(RenderAs)))
            {
                cbbChartType.Items.Add(item.ToString());
            }
            cbbChartType.SelectedValue = "Pie";

        }

        private void cbbParent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = Convert.ToInt32(cbbParent.SelectedValue);
            if (!id.Equals(0))
            {
                using (familylifeaccountEntities db = new familylifeaccountEntities())
                {
                    cbbChild.Items.Clear();
                    cbbChild.Items.Insert(0, new costclass { CostClassID = 0, ClassName = "全部" });
                    var sql2 = db.costclass.Where(m => m.ParentID.Equals(id)).ToList();
                    foreach (var item in sql2)
                    {
                        cbbChild.Items.Add(item);
                        cbbChild.SelectedValuePath = "CostClassID";
                        cbbChild.DisplayMemberPath = "ClassName";
                    }
                }
                cbbChild.SelectedValue = 0;
            }
        }

        #endregion

        #region 折线图
        public void CreateChartSpline()
        {
            Simon.Children.Clear();
            chart = new Chart();//创建一个图标
            chart.Margin = new Thickness(5);
            //chart.ToolBarEnabled = true;//显示打印和保持图片
            chart.ShadowEnabled = true;//显示阴影
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = true;//3D效果显示
            Title title = new Title(); //创建一个标题的对象
            title.Text = chartitle; //设置标题的名称
            title.Padding = new Thickness(10);
            title.FontSize = 14;
            title.FontWeight = FontWeights.Bold;
            chart.Titles.Add(title);//向图标添加标题
            Axis yAxis = new Axis();
            yAxis.AxisMinimum = 0; //设置图标中Y轴的最小值永远为0       
            yAxis.Suffix = "元"; //设置图表中Y轴的后缀     
            chart.AxesY.Add(yAxis);
            #region 查询条件
            DateTime s = DpSdate.SelectedDate.Value.Date;
            DateTime e = DpEdate.SelectedDate.Value.Date.AddDays(1);
            int parentid = Convert.ToInt32(cbbParent.SelectedValue);
            int childid = Convert.ToInt32(cbbChild.SelectedValue);
            #endregion
            DataSeries dataSeriesPineapple = new DataSeries(); // 创建一个新的数据线。      
            string temp = cbbChartType.SelectedValue.ToString();//动态设置统计图类型
            dataSeriesPineapple.RenderAs = (RenderAs)Enum.Parse(typeof(RenderAs), temp);
            dataSeriesPineapple.XValueType = ChartValueTypes.Numeric;// 设置数据线的格式。    
            dataSeriesPineapple.LightingEnabled = true;
            //dataSeriesPineapple.ShowInLegend = true;//图例
            // 设置数据点              
            DataPoint dataPoint2;
            using (familylifeaccountEntities db = new familylifeaccountEntities())
            {
                //var sql = db.view_costlist.Where(m => m.AddTime >= s && m.AddTime <= e && m.IsDel.Equals(0));
                var sql = from c in db.view_costlist
                          where c.AddTime >= s && c.AddTime <= e
                          group c by c.CostClassID into gg
                          select new
                          {
                              CostMoney = gg.Sum(m => m.CostMoney),
                              AddTime = gg.Select(m => m.AddTime),
                              ClassName = gg.Select(m => m.ClassName),
                              CostName = gg.Select(m => m.CostName),
                              CostClassID = gg.Select(m => m.CostClassID),
                              ParentID =gg.Select(m=>m.ParentID),
                          };
                if (!parentid.Equals(0))
                {
                    //sql = sql.Where(m => m.ParentID.Equals(parentid));
                    sql = from c in db.view_costlist
                          where c.AddTime >= s && c.AddTime <= e && c.ParentID.Equals(parentid)
                          group c by c.CostClassID into gg
                          select new
                          {
                              CostMoney = gg.Sum(m => m.CostMoney),
                              AddTime = gg.Select(m => m.AddTime),
                              ClassName = gg.Select(m => m.ClassName),
                              CostName = gg.Select(m => m.CostName),
                              CostClassID = gg.Select(m => m.CostClassID),
                              ParentID = gg.Select(m => m.ParentID),
                          };
                }
                if (!childid.Equals(0))
                {
                    sql = sql.Where(m => m.CostClassID.Equals(childid));
                }
                
                foreach (var item in sql)
                {
                    dataPoint2 = new DataPoint();
                    dataPoint2.AxisXLabel = item.ClassName.FirstOrDefault();
                    dataPoint2.YValue = (Double)item.CostMoney;
                    dataPoint2.ToolTipText = string.Format("合计{0:c}元", item.CostMoney);

                    if (!parentid.Equals(0))
                    {
                        dataPoint2.ShowInLegend = true;
                        dataPoint2.LegendText = item.ClassName.ToString();
                    }
                    dataSeriesPineapple.DataPoints.Add(dataPoint2);
                    dataSeriesPineapple.LabelEnabled = true;
                    //dataSeriesPineapple.LegendText = "#AxisXLabel,#YValue";
                }
                chart.Series.Add(dataSeriesPineapple);  // 添加数据线到数据序列。      
                //chart.Series[0].Color = new SolidColorBrush(Colors.LightPink);
                Simon.Children.Add(chart);

                try
                {
                    int days = (e - s).Days;
                    decimal sum = sql.Sum(m => m.CostMoney);
                    decimal average = sum / days;
                    title.Text = string.Format("『{0}』　{1}天总支出{2}元,平均{3:N2}元/天", chartitle, days, sum, average);
                }
                catch 
                {
                    title.Text = chartitle;
                }
                
            }
        }
        #endregion

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window current = App.Current.MainWindow;
                Loading load = current.FindName("load") as Loading;
                load.BackgroundLoading(new ParameterizedThreadStart((d) =>
                    this.Dispatcher.Invoke(new Action(CreateChartSpline))
                    ), null);

            }
            catch { };
        }

       /// <summary>
       /// 导出统计图
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void BtExpor_Click(object sender, RoutedEventArgs e)
        {
            if (chart!=null)
            {
                try
                {
                    chart.Export();
                }
                catch (Exception ex)
                {
                   uibase.MessageBox(ex.Message);
                }
            }
        }

        //private void SaveToImage(FrameworkElement p_FrameworkElement, string p_FileName)
        //{
        //    System.IO.FileStream fs = new
        //    System.IO.FileStream(p_FileName,
        //    System.IO.FileMode.Create);
        //    RenderTargetBitmap bmp = new
        //    RenderTargetBitmap((int)p_FrameworkElement.ActualWidth,
        //    (int)p_FrameworkElement.ActualHeight, 1 / 96, 1 / 96, PixelFormats.Default);
        //    bmp.Render(p_FrameworkElement);
        //    BitmapEncoder encoder = new TiffBitmapEncoder();
        //    encoder.Frames.Add
        //    (BitmapFrame.Create(bmp));
        //    encoder.Save(fs);
        //    fs.Close();
        //}

    }
}