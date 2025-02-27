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
using DataFactory.DAL;
using FamilyLifeAccount.View.Share;
using System.Threading;
using FamilyLifeAccount.Model;

namespace FamilyLifeAccount.View.Statistics
{
    /// <summary>
    /// EarningAndCostChart.xaml 的交互逻辑
    /// </summary>
    public partial class EarningAndCostChart : UserControl
    {
        public EarningAndCostChart()
        {
            InitializeComponent();
            InitLoad();

        }

        DALBase dal = new DALBase();
        UIBase uibase = new UIBase();

        private void InitLoad()
        {
            string str = string.Format("2014-5-1");
          
            DpSdate.SelectedDate = DateTime.Parse(str);
            DpEdate.SelectedDate = DateTime.Now;
            foreach (var item in Enum.GetValues(typeof(RenderAs)))
            {
                cbbChartType.Items.Add(item.ToString());
            }
            cbbChartType.SelectedValue = "Area";
            chartitle = dal.GetOneModel<navigation>(m => m.NavigationID.Equals(14)).Title;
            title.Content = chartitle;
        }

        Chart chart;
        string chartitle = string.Empty;

        /// <summary>
        /// 创建两个DataSeries，分别添加12个DataPoint
        /// </summary>
        private void CreateChar2()
        {

            Simon.Children.Clear();
            chart = new Chart();//创建一个图标
            chart.Margin = new Thickness(5);
            //chart.ToolBarEnabled = true;//显示打印和保持图片
            //chart.ShadowEnabled = true;//显示阴影
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = true;//3D效果显示
            Title title = new Title(); //创建一个标题的对象
            title.Text = chartitle; //设置标题的名称
            title.Padding = new Thickness(10);
            title.FontSize = 14;
            title.FontWeight = FontWeights.Bold;
            chart.Titles.Add(title);//向图标添加标题
            #region 查询条件
            DateTime s = DpSdate.SelectedDate.Value.Date;
            DateTime e = DpEdate.SelectedDate.Value.Date.AddDays(1);
            #endregion
            DataSeries dataSeriesPineapple = new DataSeries(); // 创建一个新的数据线。
            DataSeries dataSeriesPineapple2 = new DataSeries(); // 创建一个新的数据线。
            dataSeriesPineapple.AxisYType = AxisTypes.Primary;
            dataSeriesPineapple2.AxisYType = AxisTypes.Primary;

            string temp = cbbChartType.SelectedValue.ToString();//动态设置统计图类型
            dataSeriesPineapple.RenderAs = (RenderAs)Enum.Parse(typeof(RenderAs), temp);
            dataSeriesPineapple2.RenderAs = (RenderAs)Enum.Parse(typeof(RenderAs), temp);

            #region 数据点

            AddEarningDataPoint(dataSeriesPineapple);
            AddCostDataPoint(dataSeriesPineapple2);
           
            #endregion

            dataSeriesPineapple.Color = new SolidColorBrush(Colors.Red);//收入为红色
            dataSeriesPineapple.LegendText = "收入";  
            dataSeriesPineapple.LabelEnabled = true;
            //dataSeriesPineapple.LabelStyle = LabelStyles.Inside;
 
            dataSeriesPineapple2.Color = new SolidColorBrush(Colors.GreenYellow);//支出为绿色
            dataSeriesPineapple2.LegendText = "支出";
  
            dataSeriesPineapple2.LabelEnabled = true;
            //dataSeriesPineapple2.LabelStyle = LabelStyles.Inside;
            chart.Series.Add(dataSeriesPineapple);  // 添加数据线到数据序列。
            chart.Series.Add(dataSeriesPineapple2);

            Simon.Children.Add(chart);
            try
            {
                var cost = dal.GetList<cost>(m => m.AddTime >= s && m.AddTime <= e && m.IsDel.Equals(0)).Sum(m => m.CostMoney);
                var earning = dal.GetList<earning>(m => m.AddTime >= s && m.AddTime <= e && m.IsDel.Equals(0)).Sum(m => m.EarningMoney);
                var residual = earning - cost;
                int months = (DateTime.Parse(DpEdate.Text).Year - DateTime.Parse(DpSdate.Text).Year) * 12 + (DateTime.Parse(DpEdate.Text).Month - DateTime.Parse(DpSdate.Text).Month) +1;
                title.Text = string.Format("『{0}』总收入{1}元，总支出{2}元，结余{3}元，（共{4}个月，{5}/月）", 
                    new object[] { chartitle, earning, cost, residual, months,  (earning/months).ToString("0.00") });
            }
            catch
            {
                title.Text = chartitle;
            }
        }

        private string GetDate(string year, string date)
        {
            return string.Format("{0}-{1}", year, date);



        }

        private List<Months> CalculateMonth()
        {
            DateTime startDate = DateTime.Parse(DpSdate.Text);
            DateTime endDate = DateTime.Parse(DpEdate.Text);
            int Month = (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month);
            List<Months> newlist = new List<Model.Months>();
            var list = FamilyLifeAccount.Model.Months.GetOneYear(startDate.Year);
            int index = list.FindIndex(m => m.startDate.Contains(startDate.Month.ToString()));
            var year = startDate.Year;
            for (int i = index; i < list.Count; i++)
            {
                var start = list[i].startDate;
                var end = list[i].endDate;

                newlist.Add(new Months { Index = list[i].Index, year = year, startDate = list[i].startDate, endDate = list[i].endDate });
                if (list[i].Index == 12)
                {
                    year = year + 1;
                    i = -1;
                }
                if (newlist.Count == Month + 1)
                {
                    break;
                }
            }
            return newlist;
        }

        private void AddEarningDataPoint(DataSeries ds)
        {
            List<Months> list = CalculateMonth();
            foreach (var item in list)
            {
                AddEarningDataPoint(GetDate(item.year.ToString(), item.startDate), GetDate(item.year.ToString(), item.endDate),item.Index.ToString(), ds);
            }
        }

        private void AddCostDataPoint(DataSeries ds)
        {
            List<Months> list = CalculateMonth();
            foreach (var item in list)
            {
                AddCostDataPoint(GetDate(item.year.ToString(), item.startDate), GetDate(item.year.ToString(), item.endDate), item.Index.ToString(), ds);
            }
        }

        /// <summary>
        /// 收入
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="month"></param>
        /// <param name="ds"></param>
        private void AddEarningDataPoint(string date1, string date2, string month, DataSeries ds)
        {
            DateTime s = DateTime.Parse(date1);
            DateTime e = DateTime.Parse(date2);
            var source = dal.GetList<earning>(m => m.AddTime >= s && m.AddTime <= e && m.IsDel.Equals(0)).Sum(m => m.EarningMoney);
            // 设置数据点             
            DataPoint d5 = new DataPoint();
            d5.AxisXLabel = month + "月";
            d5.YValue = (double)source;
            d5.ZValue = (double)source; d5.LightingEnabled = true;
            d5.ToolTipText = string.Format("{0}收入{1}元", d5.AxisXLabel, source);
            d5.LegendText = d5.ToolTipText;
            d5.TextParser(d5.ToolTipText);
            d5.LabelEnabled = true;
            d5.ShowInLegend = true;
            ds.DataPoints.Add(d5);
        }

        /// <summary>
        /// 支出
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="month"></param>
        /// <param name="ds"></param>
        private void AddCostDataPoint(string date1, string date2, string month, DataSeries ds)
        {
            DateTime s = DateTime.Parse(date1);
            DateTime e = DateTime.Parse(date2);
            var source = dal.GetList<cost>(m => m.AddTime >= s && m.AddTime <= e&&m.IsDel.Equals(0)).Sum(m => m.CostMoney);
            // 设置数据点             
            DataPoint d5 = new DataPoint();
            d5.LabelEnabled = true;
            d5.ShowInLegend = true; d5.LightingEnabled = true;
            d5.AxisXLabel = month + "月";
            d5.YValue = (double)source;
            d5.ToolTipText = string.Format("{0}支出{1}元", d5.AxisXLabel, source);
            ds.DataPoints.Add(d5);
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                Window current = App.Current.MainWindow;
                Loading load = current.FindName("load") as Loading;
                load.BackgroundLoading(new ParameterizedThreadStart((d) =>
                    this.Dispatcher.Invoke(new Action(CreateChar2))
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
            if (chart != null)
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
    }
}
