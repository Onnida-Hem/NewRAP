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
using Enum = RAP_WPF.Model.Enum;

namespace RAP_WPF.View
{
    /// <summary>
    /// Interaction logic for PerformanceView.xaml
    /// </summary>
    public partial class PerformanceView : Window
    {
        public PerformanceView(Researcher SelectedResearcher)
        {
            InitializeComponent();

            if (SelectedResearcher.Type == Enum.ResearcherType.Staff)
            {
                var staff = (Staff)SelectedResearcher;
                DataContext = staff;
                PercentageQ1.Content = Math.Round(staff.PercentageQ1, 1) + "%";
                Average3Year.Content = Math.Round(staff.Average3Year,1) + "%";
                FundingReceived.Content = staff.FundingReceived.ToString("N0") + " AUD";
                PublicationPerformance.Content = staff.PublicationPerformance + " publication/yr.";
                FundingReceivedPerformance.Content = staff.FundingReceivedPerformance.ToString("N0") + " AUD/yr.";
            }
            else
            {
                var student = (Student)SelectedResearcher;
                DataContext = student;
                PercentageQ1.Content = Math.Round(student.PercentageQ1,1) + "%";
                Average3Year.Content = "-";
                FundingReceived.Content = "-";
                PublicationPerformance.Content = "-";
                FundingReceivedPerformance.Content = "-";
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
