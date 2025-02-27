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
using System.Windows.Threading;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace FamilyLifeAccount.View.Share
{
    internal delegate object AsyncHandler();

    /// <summary>
    /// Loading.xaml 的交互逻辑
    /// </summary>
    public partial class Loading : UserControl
    {
        public Loading()
        {
            InitializeComponent();

        }


       
        /// <summary>
        ///  封装异步后台执行函数，后台运算时加载Loading
        /// </summary>
        /// <param name="handler">传递执行函数，通过委托来指派</param>
        /// <param name="param">执行函数的参数</param>
        /// <param name="dispatcher">DispatcherPriority</param>
        internal void BackgroundLoading(ParameterizedThreadStart handler, object param, DispatcherPriority dispatcher)
        {
            //3.执行异步方法
            //AsyncHandler thread = new AsyncHandler(ob);
            //4.完成异步操作后执行，当方法结束时继续
            Action<IAsyncResult> resultHandhler = delegate(IAsyncResult AsyncResult)
            {
                //轮询异步方法是否完成
                handler.EndInvoke(AsyncResult);
                this.Visibility = System.Windows.Visibility.Collapsed;
            };
            //2.分发调用异步方法  //异步任务执行完毕后的callback, 此callback运行在后台线程上. 
            //此callback会异步调用resultHandler来处理异步任务的返回值.
            AsyncCallback callback = delegate(IAsyncResult Asyncrsult)
            {
                this.Dispatcher.BeginInvoke(dispatcher, resultHandhler, Asyncrsult);
            };
            //1.开始执行异步方法 显示Loading 调用AsyncHandler后台执行
            //asyncAction(后台线程), asyncActionCallback(后台线程)和resultHandler(UI线程) 将被依次执行
           
            this.Visibility = System.Windows.Visibility.Visible;
            handler.BeginInvoke(param,callback, param);
          
        }

        /// <summary>
        /// 封装异步后台执行函数，后台运算时加载Loading
        /// </summary>
        /// <param name="handler">传递执行函数，通过委托来指派</param>
        /// <param name="sender">函数的参数</param>
        internal void  BackgroundLoading(ParameterizedThreadStart handler, object sender)
        {
            BackgroundLoading(handler, sender, DispatcherPriority.Normal);
        }


        internal void BackgroundLoading(AsyncHandler handler)
        {
            //1.开始执行异步方法 显示Loading 调用AsyncHandler后台执行
            //asyncAction(后台线程), asyncActionCallback(后台线程)和resultHandler(UI线程) 将被依次执行
            handler.BeginInvoke(new AsyncCallback(AsyncCallback), handler);
            this.Visibility = System.Windows.Visibility.Visible;
        }


        internal void AsyncCallback(IAsyncResult iresult)
        {
            this.Visibility = System.Windows.Visibility.Hidden;
            AsyncHandler handler = (AsyncHandler)((AsyncResult)result).AsyncDelegate;
            result = handler.EndInvoke(iresult);
        }

        private object _result;

        public object result
        {
            get { return _result; }
            set { _result = value; }
        }


    }
}
