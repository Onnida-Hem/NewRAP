using RAP_WPF.Model;
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
    /// Interaction logic for SupervisionsView.xaml
    /// </summary>
    public partial class SupervisionsView : Window
    {
        public SupervisionsView(Researcher SelectedResearcher)
        {
            InitializeComponent();
            var staff = (Staff)SelectedResearcher;
            SupervisionListView.ItemsSource = staff.SupervisionList;
            DataContext = this;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
