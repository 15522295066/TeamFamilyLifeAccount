using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Windows;

namespace FamilyLifeAccount.Comm
{
    public class ControlBase : UserControl
    {
        public bool MessageShowError(object value, string title)
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
                if (t.Equals(typeof(System.Int32)))
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

        public static void MessageBox(string msg)
        {
            string caption = "家庭记账";
            Xceed.Wpf.Toolkit.MessageBox.Show(msg, caption, MessageBoxButton.OK);
        }


    }
}
