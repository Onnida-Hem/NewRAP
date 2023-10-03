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
    /// Interaction logic for PublicationDetailView.xaml
    /// </summary>
    public partial class PublicationDetailView : Window
    {
        //private Publication selectedPublication { get; set; }
        public Publication SelectedPublication { get; set; }

        //public PublicationDetailView()
        //{
        //    InitializeComponent();
        //    //selectedPublication = SelectedPublication;
        //    PublicationDetailGrid.DataContext = SelectedPublication;
        //    DataContext = this;
        //}

        public PublicationDetailView(Publication selectedPublication)
        {
            InitializeComponent();
            DataContext = selectedPublication;
        }

        //public void Reload()
        //{
        //    DataContext = selectedPublication;
        //}

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
