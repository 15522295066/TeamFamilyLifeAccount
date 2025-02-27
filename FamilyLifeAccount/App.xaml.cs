using System.Windows;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Threading;
using System.Reflection;
using System.Globalization;
using System.Windows.Interop;
using System.Windows.Threading;

namespace FamilyLifeAccount
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
        void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var comException = e.Exception as System.Runtime.InteropServices.COMException;
            if (comException != null && comException.ErrorCode == -2147221040)///OpenClipboard HRESULT:0x800401D0 (CLIPBRD_E_CANT_OPEN))
                e.Handled = true;
        }
        //#region Members
        //private Int32 m_Message;
        //private Mutex m_Mutex;
        //#endregion

        //#region Methods: Functions
        //private IntPtr HandleMessages(IntPtr handle, Int32 message, IntPtr wParameter, IntPtr lParameter, ref Boolean handled)
        //{
        //    if (message == m_Message)
        //    {
        //        if (MainWindow.WindowState == WindowState.Minimized)
        //            MainWindow.WindowState = WindowState.Normal;

        //        Boolean topmost = MainWindow.Topmost;

        //        MainWindow.Topmost = true;
        //        MainWindow.Topmost = topmost;
        //    }

        //    return IntPtr.Zero;
        //}

        //private void Dispose(Boolean disposing)
        //{
        //    if (disposing && (m_Mutex != null))
        //    {
        //        m_Mutex.ReleaseMutex();
        //        m_Mutex.Close();
        //        m_Mutex = null;
        //    }
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //#endregion

        //#region Methods: Overrides
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    Assembly assembly = Assembly.GetExecutingAssembly();
        //    Boolean mutexCreated;
        //    String mutexName = String.Format(CultureInfo.InvariantCulture, "Local\\{{{0}}}{{{1}}}", assembly.GetType().GUID, assembly.GetName().Name);

        //    m_Mutex = new Mutex(true, mutexName, out mutexCreated);
        //    //m_Message = NativeMethods.RegisterWindowMessage(mutexName);

        //    if (!mutexCreated)
        //    {
        //        m_Mutex = null;

        //     //   NativeMethods.PostMessage(NativeMethods.HWND_BROADCAST, m_Message, IntPtr.Zero, IntPtr.Zero);

        //        Current.Shutdown();

        //        return;
        //    }

        //    base.OnStartup(e);

        //    MainWindow window = new MainWindow();
        //    MainWindow = window;
        //    window.Show();

        //    HwndSource.FromHwnd((new WindowInteropHelper(window)).Handle).AddHook(new HwndSourceHook(HandleMessages));
        //}
        //#endregion
        protected override void OnExit(ExitEventArgs e)
        {
            //Dispose();
            base.OnExit(e);
        }
      

    }


}
