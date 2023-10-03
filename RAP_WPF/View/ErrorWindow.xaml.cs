using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RAP_WPF.View
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(string errormsg)
        {
            InitializeComponent();

            ////from RAPLitev2
            //var erroricon = new Image();

            //BitmapImage bitmap = new BitmapImage();
            //bitmap.BeginInit();
            ////bitmap.UriSource = new Uri("C:/Users/oh_pp/OneDrive/JaoDekPun/MITS/KIT506/Assignment/RAP_WPF/RAP_WPF/Image/error_icon.png");
            //bitmap.UriSource = new Uri("/RAP_WPF;component/Resources/error_icon.png");
            //bitmap.UriSource = new Uri("pack://
    
            //    bitmap.EndInit();
            //ImageSource imageSource = bitmap;

            //ErrorIcon.Source = imageSource;

            ErrorLabel.Content = errormsg;
        }
    }
}
