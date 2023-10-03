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
                    MeetMinimumListView.ItemsSource = ReportController.GenerateReport(ReportName.MeetingMinimum);
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
                    BelowExpectationsListView.ItemsSource = ReportController.GenerateReport(ReportName.BelowExpectation);
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
                    PoorListView.ItemsSource = ReportController.GenerateReport(ReportName.Poor);
                }
                else
                {
                    PoorListView.ItemsSource = PoorReport;
                }
            }
        }
    }
}
