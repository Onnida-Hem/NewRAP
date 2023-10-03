using RAP_WPF.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RAP_WPF.Model.Enum;

namespace RAP_WPF.Model
{
    public class Publication
    {
        public string ResearchID { get; set; }
        public string DOI { get; set; }
        public string PublicationTitle { get; set; }
        public string Authors { get; set; }
        public int PublicationYear { get; set; }
        public Ranking Ranking { get; set; }
        public PublicationType Type { get; set; }
        public string Cite { get; set; }
        public DateTime AvailableDate  { get; set; }
        public int Age {
            get { return (DateTime.Today - AvailableDate).Days; }
        }

        /*
        public override string ToString()
        {
            return "DOI: " + DOI + '\n' + "Title: " + PublicationTitle + '\n' + "Ranking: " + Ranking + '\n' +
                "PublicationYear: " + PublicationYear + '\n' + "Type: " + Type + '\n';
        }
        */
    }

   
}
