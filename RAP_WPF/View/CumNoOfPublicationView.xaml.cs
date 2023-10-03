using RAP_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CumNoOfPublicationView.xaml
    /// </summary>
    public partial class CumNoOfPublicationView : Window
    {
        public CumNoOfPublicationView(List<CumulativeCount> CumulativeCountList)
        {
            InitializeComponent();
            CumNoOfPublicationListView.ItemsSource = CumulativeCountList;
            DataContext = this;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
