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
using System.ComponentModel;
using System.Threading;

namespace FamilyLifeAccount.View.Share
{
    /// <summary>
    /// Loading2.xaml 的交互逻辑
    /// </summary>
    public partial class Loading2 : UserControl
    {
        public Loading2()
        {
            InitializeComponent();
         
        }

        public object argument { get; set; }
        public delegate object AsyncHandler(params object[] args);
        private Delegate _handler;
        public Delegate Handler
        {
            get { return _handler; }
            set { _handler = value; }
        }


        BackgroundWorker backgroundWorker1;
     
        internal void BackgroundLoading(Delegate handler,object par)
        {

            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(bgMeet_DoWork);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(bgMeet_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgMeet_RunWorkerCompleted);
            this.Handler = handler;
            backgroundWorker1.RunWorkerAsync(par);
        }


       
        void bgMeet_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
            this.Dispatcher.Invoke(new Action(() =>
            {
                //this.lab_pro.Content = "完成";
            }));
        }
        void bgMeet_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                //this.lab_pro.Content = e.ProgressPercentage;
                //argument = e.ProgressPercentage;
          
            }));

         
        }

        void bgMeet_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.Visibility = System.Windows.Visibility.Visible;
            }));
            Handler.DynamicInvoke();
            //t = new Thread(new ParameterizedThreadStart((ms) =>
            //    {
            //        Handler.DynamicInvoke();
            //    }));
            //Dispatcher.Invoke(Handler);
            //Application.Current.Dispatcher.BeginInvoke(Handler);

           // Handler.DynamicInvoke();
            //Thread newWindowThread = new Thread(new ThreadStart(() =>
            //{
            //    Dispatcher.Invoke(new Action(() =>
            //    {
            //        object[] ob = { e.Argument };
            //        Application.Current.Dispatcher.BeginInvoke(Handler, ob);
            //    }));
            //}));
 
        }


        
        
 
         

        protected void InvokeOnUIDispatcher(Delegate callback, params object[] args)
        {
            Application.Current.Dispatcher.BeginInvoke(callback, args);
        }

       
    }
}
