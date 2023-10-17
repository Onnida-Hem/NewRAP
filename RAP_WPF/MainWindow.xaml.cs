using RAP_WPF.Controller;
using RAP_WPF.Model;
using RAP_WPF.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Enum = RAP_WPF.Model.Enum;

namespace RAP_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //declare view for check object null or not
        private PublicationDetailView publicationDetailView;
        private PerformanceView performanceView;
        private ErrorWindow errorWindow = null;
        private CumNoOfPublicationView cumNoOfPublicationView = null;
        private SupervisionsView supervisionsView = null;
        private ReportView reportView = null;

        public static Researcher SelectedResearcher;
        public static List<Publication> PublicationList;
        public static List<int> Year
        {
            get
            {
                return Enumerable.Range(DateTime.Now.Year - 20, 21).OrderByDescending(x => x).ToList();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            filterByNameTextBox.mainWindow = this;
            ResearcherController.LoadFundingList();
            PublicationController.ListAllPublication();
        }

        public ObservableCollection<Researcher> Researchers
        {
            get
            {
                return new ObservableCollection<Researcher>(ResearcherController.ListResearcher());
            }
        }

        public ObservableCollection<string> FilterByLevel
        {
            get
            {
                List<string> type = new List<string>(System.Enum.GetNames(typeof(Enum.Filter)).ToList());
                ObservableCollection<string> filter = new ObservableCollection<string>(type);

                return filter;
            }
        }

        ////from Week11_SampleCode
        //private void filterByNameTextBox_KeyUp(object sender, RoutedEventArgs e)
        //{
        //    if (filterByNameTextBox.Text != null)
        //    {
        //        ResearchersListView.ItemsSource = ResearcherController.FilterByName(filterByNameTextBox.Text);
        //    }
        //    else
        //    {
        //        ResearchersListView.ItemsSource = Researchers;
        //    }
        //}

        private void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] != null)
            {
                filterByNameTextBox.NameTextBox.Text = string.Empty;

                if (e.AddedItems[0].ToString().StartsWith("All"))
                {
                    ResearchersListView.ItemsSource = Researchers;
                }
                else if (e.AddedItems[0].ToString().StartsWith("Student"))
                {
                    ResearchersListView.ItemsSource = ResearcherController.FilterByLevel(Enum.JobLevel.None, Enum.ResearcherType.Student);
                }
                else if (e.AddedItems[0].ToString().StartsWith("A"))
                {
                    ResearchersListView.ItemsSource = ResearcherController.FilterByLevel(Enum.JobLevel.A, Enum.ResearcherType.Staff);
                }
                else if (e.AddedItems[0].ToString().StartsWith("B"))
                {
                    ResearchersListView.ItemsSource = ResearcherController.FilterByLevel(Enum.JobLevel.B, Enum.ResearcherType.Staff);
                }
                else if (e.AddedItems[0].ToString().StartsWith("C"))
                {
                    ResearchersListView.ItemsSource = ResearcherController.FilterByLevel(Enum.JobLevel.C, Enum.ResearcherType.Staff);
                }
                else if (e.AddedItems[0].ToString().StartsWith("D"))
                {
                    ResearchersListView.ItemsSource = ResearcherController.FilterByLevel(Enum.JobLevel.D, Enum.ResearcherType.Staff);
                }
                else if (e.AddedItems[0].ToString().StartsWith("E"))
                {
                    ResearchersListView.ItemsSource = ResearcherController.FilterByLevel(Enum.JobLevel.E, Enum.ResearcherType.Staff);
                }
            }
        }

        private void ResearchersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Researcher researcher = e.AddedItems[0] as Researcher;
                PublicationList = PublicationController.ListPublication(researcher.ID);
                SelectedResearcher = ResearcherController.ShowResearcherDetail(researcher.ID);

                #region PHOTO
                //from RAPLitev2
                var photo = new Image();

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(SelectedResearcher.Photo);
                bitmap.EndInit();
                ImageSource imageSource = bitmap;

                PhotoImage.Source = imageSource;
                #endregion

                CurrentJobTitle.Content = Enum.JobTitle(SelectedResearcher.CurrentJobTitle);

                if (researcher.Type == Enum.ResearcherType.Staff)
                {
                    Staff staff = (Staff)SelectedResearcher;
                    ResearcherDetailView.DataContext = staff;
                    //FirstPositionStartDate.Content = staff.FirstPositionStartDate.ToString("d");
                    //CurrentPositionStartDate.Content = staff.CurrentPositionStartDate.ToString("d");
                    Supervisions.Text = staff.SupervisionList.Count().ToString();
                    Supervisions.TextDecorations = TextDecorations.Underline;
                    Degree.Content = "-";
                    Supervisor.Content = "-";
                    PositionListView.ItemsSource = staff.Positions;
                }
                else
                {
                    var student = (Student)SelectedResearcher;
                    ResearcherDetailView.DataContext = student;
                    //FirstPositionStartDate.Content = student.FirstPositionStartDate.ToString("d");
                    //CurrentPositionStartDate.Content = student.CurrentPositionStartDate.ToString("d");
                    Supervisions.Text = "-";
                    Supervisions.TextDecorations = null;
                    Degree.Content = student.Degree;
                    Supervisor.Content = student.SupervisorName;
                    PositionListView.ItemsSource = new List<Position>();
                }

                PublicationListView.ItemsSource = PublicationList;
                SortYearComboBox.SelectedIndex = 0;
                FromYearComboBox.SelectedIndex = -1;
                ToYearComboBox.SelectedIndex = -1;
            }
        }

        private void PerformanceDetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedResearcher == null)
            {
                Error("Please select Researcher!!!");
            }
            else
            {
                //Close when previous window if it still open
                if (performanceView != null)
                {
                    performanceView.Close();
                }

                //Create and open new window of Performance
                performanceView = new PerformanceView(SelectedResearcher);

                performanceView.Show();
            }
        }

        private void Error(string errormsg)
        {
            //Close when previous window if it still open
            if (errorWindow != null)
            {
                errorWindow.Close();
            }

            //Create and open new window of Error
            errorWindow = new ErrorWindow(errormsg);

            errorWindow.Show();
        }

        private void SortYearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] != null && SelectedResearcher != null)
            {
                int fromYear = FromYearComboBox.SelectedValue != null ? Convert.ToInt32(FromYearComboBox.SelectedValue.ToString()) : 0;
                int toYear = ToYearComboBox.SelectedValue != null ? Convert.ToInt32(ToYearComboBox.SelectedValue.ToString()) : 0;

                //if (e.AddedItems[0].ToString().Contains("Oldest-"))
                //{
                //    PublicationListView.ItemsSource = PublicationController.FilterByPublication(0, 0, false);
                //}
                //else if (e.AddedItems[0].ToString().Contains("Newest-"))
                //{
                //    PublicationListView.ItemsSource = PublicationList;
                //}

                if (e.AddedItems[0].ToString().Contains("All"))
                {
                    PublicationListView.ItemsSource = PublicationList;
                    FromYearComboBox.SelectedIndex = -1;
                    ToYearComboBox.SelectedIndex = -1;
                }
                else
                {
                    bool recentYearFilter = SortYearComboBox.SelectedValue.ToString().Contains("Oldest-") ? false : true;
                    PublicationListView.ItemsSource = PublicationController.FilterByPublication(fromYear, toYear, recentYearFilter);
                }
            }
        }

        private void FromToYearButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedResearcher == null)//(FromYearComboBox.SelectedValue == null)
            {
                Error("Please select Researcher!!!");
            }
            else
            {
                if (FromYearComboBox.SelectedValue == null || ToYearComboBox.SelectedValue == null)
                {
                    Error("Please select both of From: Year and To: Year");
                }
                else
                {
                    int fromYear = Convert.ToInt32(FromYearComboBox.SelectedValue.ToString());
                    int toYear = Convert.ToInt32(ToYearComboBox.SelectedValue.ToString());

                    if (fromYear > toYear)
                    {
                        Error("From: Year should be more than To: Year!!!");
                    }
                    else
                    {
                        bool recentYearFilter = SortYearComboBox.SelectedValue.ToString().Contains("Oldest-") ? false : true;
                        PublicationListView.ItemsSource = PublicationController.FilterByPublication(fromYear, toYear, recentYearFilter);
                    }
                }
            }
        }

        private void PublicationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Publication publication = e.AddedItems[0] as Publication;
                Publication selectedPublication = PublicationController.ShowPublicationDetail(publication.DOI);

                if (publicationDetailView != null)
                {
                    publicationDetailView.Close();
                }

                publicationDetailView = new PublicationDetailView(selectedPublication);

                publicationDetailView.Show();

            }
        }

        private void CumNoOfPublication_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SelectedResearcher != null)
            {
                //Close when previous window if it still open
                if (cumNoOfPublicationView != null)
                {
                    cumNoOfPublicationView.Close();
                }

                //Create and open new window of CumNoOfPublication
                cumNoOfPublicationView = new CumNoOfPublicationView(SelectedResearcher.CumulativeCountList);

                cumNoOfPublicationView.Show();
            }
        }

        private void Supervisions_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SelectedResearcher != null)
            {
                if (Supervisions.Text.Contains("0") || Supervisions.Text.Contains("-"))
                { }
                else
                {
                    //Close when previous window if it still open
                    if (publicationDetailView != null)
                    {
                        supervisionsView.Close();
                    }

                    //Create and open new window of Supervision
                    supervisionsView = new SupervisionsView(SelectedResearcher);

                    supervisionsView.Show();
                }
            }
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            //Close when previous window if it still open
            if (reportView != null)
            {
                reportView.Close();
            }

            //Create and open new window of Report
            reportView = new ReportView();

            reportView.Show();
        }
    }
}
