using RAP_WPF.Controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RAP_WPF.View
{
    /// <summary>
    /// Interaction logic for FilterNameControl.xaml
    /// </summary>
    public partial class FilterNameControl : UserControl
    {
        public MainWindow mainWindow;
        public FilterNameControl()
        {
            InitializeComponent();
        }

        //from Week11_SampleCode
        private void filterByNameTextBox_KeyUp(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text != null)
            {
                mainWindow.ResearchersListView.ItemsSource = ResearcherController.FilterByName(NameTextBox.Text);
            }
            else
            {
                mainWindow.ResearchersListView.ItemsSource = mainWindow.Researchers;
            }
        }
    }

   
}
