using RAP_WPF.Controller;
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
using static RAP_WPF.Model.Enum;

namespace RAP_WPF.View
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : Window
    {
        private List<ReportPerformance> StarPerformerReport;
        private List<ReportPerformance> MeetMinimumReport;
        private List<ReportPerformance> BelowExpectationsReport;
        private List<ReportPerformance> PoorReport;

        public ReportView()
        {
            ReportController.GenerateAllReport();
            InitializeComponent();
            StarPerformerReport = ReportController.GenerateReport(ReportName.StarPerformer);
            StarPerformerListView.ItemsSource = StarPerformerReport;
            DataContext = this;
        }

        //https://stackoverflow.com/questions/772841/is-there-selected-tab-changed-event-in-the-standard-wpf-tab-control
        //use for tabselection
        private void ReportTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StarPerformerTab.IsSelected)
            {
                if (MeetMinimumReport == null)
                {
                    StarPerformerListView.ItemsSource = ReportController.GenerateReport(ReportName.StarPerformer);
                }
                else
                {
                    StarPerformerListView.ItemsSource = StarPerformerReport;
                }
            }
            else if (MeetMinimumTab.IsSelected)
            {
                if (MeetMinimumReport == null)
                {
                    MeetMinimumReport = ReportController.GenerateReport(ReportName.MeetingMinimum);
                    MeetMinimumListView.ItemsSource = MeetMinimumReport;
                }
                else
                {
                    MeetMinimumListView.ItemsSource = MeetMinimumReport;
                }
            }
            else if (BelowExpectationsTab.IsSelected)
            {
                if (BelowExpectationsReport == null)
                {
                    BelowExpectationsReport = ReportController.GenerateReport(ReportName.BelowExpectation);
                    BelowExpectationsListView.ItemsSource = BelowExpectationsReport;
                }
                else
                {
                    BelowExpectationsListView.ItemsSource = BelowExpectationsReport;
                }
            }
            else if (PoorTab.IsSelected)
            {
                if (PoorReport == null)
                {
                    PoorReport = ReportController.GenerateReport(ReportName.Poor);
                    PoorListView.ItemsSource = PoorReport;
                }
                else
                {
                    PoorListView.ItemsSource = PoorReport;
                }
            }
        }

        private void StarPerformerButton_Click(object sender, RoutedEventArgs e)
        {
            var emails = ReportController.CopyAllEmails(StarPerformerReport);
            MessageBox.Show("Emails are copied: " + String.Join("; ", emails));
        }

        private void MeetMinimumButton_Click(object sender, RoutedEventArgs e)
        {
            var emails = ReportController.CopyAllEmails(MeetMinimumReport);
            MessageBox.Show("Emails are copied: " + String.Join("; ", emails));
        }

        private void BelowExpectationsButton_Click(object sender, RoutedEventArgs e)
        {
            var emails = ReportController.CopyAllEmails(BelowExpectationsReport);
            MessageBox.Show("Emails are copied: " + String.Join("; ", emails));
        }

        private void PoorButton_Click(object sender, RoutedEventArgs e)
        {
            var emails = ReportController.CopyAllEmails(PoorReport);
            MessageBox.Show("Emails are copied: " + String.Join("; ", emails));
        }

        
    }
}
