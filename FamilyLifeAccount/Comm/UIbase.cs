using System.Windows;
using System.Collections.Generic;

using System.Reflection;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Xml;
using System.Windows.Markup;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Data;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using System.Configuration;

namespace FamilyLifeAccount.Comm
{
    /// <summary>
    /// 继承ViewModelBase
    /// </summary>
    public class UIBase : System.Windows.Window
    {

        public static Window AddPopWindow(System.Windows.Controls.UserControl uc)
        {
            PopUpWindow pop = new PopUpWindow();
            pop.Background = Brushes.Transparent;
            pop.AllowsTransparency = true;
            pop.Content = uc;
            pop.Height = uc.Height ;
            pop.Width = uc.Width ;
            pop.WindowStyle = WindowStyle.None;
            //pop.griduc.Children.Add(uc);
            pop.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            return pop;
        }

        public static void Close(object usercontrol)
        {
            System.Windows.Controls.UserControl uc = usercontrol as System.Windows.Controls.UserControl;
            ((Window)(uc.Parent)).Close();
        }

        public  bool MessageShowError(object value, string title)
        {
            string msg = string.Format("请检查{0}信息，是否正确!", title);
            if (value != null)
            {
                Type t = value.GetType();
                if (t.Equals(typeof(System.String)))
                {
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        MessageBox(msg);
                        return false;
                    }
                }
                else if (t.Equals(typeof(System.Int32)))
                {
                    if (Convert.ToInt32(value).Equals(0))
                    {
                        MessageBox(msg);
                        return false;
                    }
                }
            }
            else
            {
                MessageBox(msg);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 获项目文件夹路径
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetProjectPath(int i)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string rootpath = string.Empty;
            for (int j = 0; j < i; j++)
            {
                path = path.Substring(0, path.LastIndexOf("\\"));
            }
            return path;
        }


        public static string GetOutPath(string DirectoryName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(path, DirectoryName);
        }

        public  void MessageBox(string msg)
        {
            //动态加载资源字典
            ResourceDictionary resource = (ResourceDictionary)Application.LoadComponent(new Uri("Resources/XceedToolkit.xaml", UriKind.Relative));
            this.Resources.MergedDictionaries.Add(resource);
            string caption = "家庭记账";
            System.Windows.Style style = Resources["messageBoxStyle"] as Style;
            Xceed.Wpf.Toolkit.MessageBox.Show(msg, caption, MessageBoxButton.OK,style);
        }


        /// <summary>
        /// 随机背景
        /// </summary>
        /// <returns></returns>
        public static string RandomBackground()
        {
            string path = string.Empty;
            try
            {
                string background = ConfigurationManager.AppSettings["Background"];
                path = UIBase.GetOutPath(background);
                path = @"E:\相册\Photo\巴厘岛\";
                Random r = new Random();
                string[] str = Directory.GetFiles(path);
                int i = r.Next(str.Length);
                path = str[i];
            }
            catch
            {
                return path;
            }
            return path;
        }



        /// <summary>
        /// 移除指定集合中对象并返还新集合
        /// </summary>
        /// <param name="l">集合</param>
        /// <param name="t">对象</param>
        /// <returns>集合</returns>
        public static List<T> GetNewRemoveList<T>(List<T> l, T t)
        {
            if (t == null || l == null)
            {
                return l;
            }
            else
            {
                if (l.Contains(t))
                {
                    l.Remove(t);
                }
                List<T> tmp = new List<T>();
                foreach (T o in l)
                {
                    tmp.Add(o);
                }
                return tmp;
            }
        }

        /// <summary>
        /// 添加对象到指定集合，并返还新集合
        /// </summary>
        /// <param name="l">集合</param>
        /// <param name="t">对象</param>
        /// <param name="IsRepeat">是否可以重复</param>
        /// <returns>集合</returns>
        public static List<T> GetNewAddList<T>(List<T> l, T t, bool IsRepeat = false)
        {
            if (t == null || l == null)
            {
                return l;
            }
            else
            {
                if (!l.Contains(t) || IsRepeat)
                {
                    l.Add(t);
                }
                List<T> tmp = new List<T>();
                foreach (T o in l)
                {
                    tmp.Add(o);
                }
                return tmp;
            }
        }

        public static List<T> GetNewRemoveList<T>(List<T> l, IEqualityComparer<T> compare, T t)
        {
            if (t == null || l == null)
            {
                return l;
            }
            else
            {
                if (l.Contains(t))
                {
                    l.Remove(t);
                }
                List<T> tmp = new List<T>();
                foreach (T o in l)
                {
                    tmp.Add(o);
                }
                return tmp;
            }
        }

        public static List<T> GetNewAddList<T>(List<T> l, T t, IEqualityComparer<T> compare, bool IsRepeat = false)
        {
            if (t == null || l == null)
            {
                return l;
            }
            else
            {
                if (!l.Contains(t) || IsRepeat)
                {
                    l.Add(t);
                }
                List<T> tmp = new List<T>();
                foreach (T o in l)
                {
                    tmp.Add(o);
                }
                return tmp;
            }
        }


        public static System.Drawing.Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            //  对于C#的随机数，没什么好说的
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            //  为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            return System.Drawing.Color.FromArgb(int_Red, int_Green, int_Blue);
        }

    }
}
