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
using FamilyLifeAccount.Comm;
using System.IO;

namespace FamilyLifeAccount.View.Head
{
    /// <summary>
    /// SwitchWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SwitchWindow : UserControl
    {
        // private String[] IMAGES = { "images/image1.png", "images/image2.png", "images/image3.png", "images/image4.png", "images/image5.png", "images/image6.png", "images/image7.png", "images/image8.png", "images/image9.png", "images/image1.png", "images/image2.png", "images/image3.png", "images/image4.png", "images/image5.png", "images/image6.png", "images/image7.png", "images/image8.png", "images/image9.png" };    // images
        
        public SwitchWindow()
        {
            InitializeComponent();

            string background = "Background";
            string path = UIBase.GetOutPath(background);
  
            Random r = new Random();
            DirectoryInfo d = new DirectoryInfo(path);
            string [] str = Directory.GetFiles(path);
        

            


            imageScrollView1.ChildViewWidth = 400;
            imageScrollView1.ChildViewHeight = 400;
            imageScrollView1.SpaceWidth = 300;
            imageScrollView1.AddImages(str);
            imageScrollView1.Enableslide = false;

        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            imageScrollView1.SlideAffect = ImageScrollView.SlideAffectEnum.JumpSlide;

            if (checkBox1 != null)
                checkBox1.IsEnabled = false;
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            imageScrollView1.SlideAffect = ImageScrollView.SlideAffectEnum.OrderSlide;
            checkBox1.IsEnabled = true;

        }

        private void checkBox1_Click(object sender, RoutedEventArgs e)
        {
            imageScrollView1.Enableslide = checkBox1.IsChecked.Value;
        }
    }
}
