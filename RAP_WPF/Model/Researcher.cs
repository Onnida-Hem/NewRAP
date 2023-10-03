using RAP_WPF.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RAP_WPF.Model.Enum;

namespace RAP_WPF.Model
{

    public class Researcher
    {
        public string ID { get; set; }
        public ResearcherType Type { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string ResearcherTitle { get; set; }
        public string School { get; set; }
        public Campus Campus { get; set; }
        public string Email { get; set; }
        public JobLevel CurrentJobTitle { get; set; }
        public DateTime FirstPositionStartDate { get; set; }
        public DateTime CurrentPositionStartDate { get; set; }
        public int TenureYear
        {
            get
            {
                return DateTime.Today.Year - FirstPositionStartDate.Year;
            }
        }
        public int PublicationCount
        {
            get
            {
                int countPub = (from Publication p in PublicationController.publicationList
                                where p.ResearchID == ID
                                select p).Count();
                return countPub;
            }
        }

        public double PercentageQ1
        {
            get
            {
                double countPub = PublicationCount;
                double countPubQ1 = (from Publication p in PublicationController.publicationList
                                     where p.ResearchID == ID && p.Ranking == Ranking.Q1
                                     select p).Count();
                double result = Math.Round(countPubQ1 / countPub * 100, 1);
                return result;
            }
        }

        public List<CumulativeCount> CumulativeCountList
        {
            get
            {
                return ResearcherController.CalCumNoOfPublication();
            }
        }
        public string Photo { get; set; }

        public string Fullname
        {
            get { return FamilyName + ", " + GivenName + " (" + ResearcherTitle + ")"; }
        }

        /*
        public override string ToString()
        {
            get { return GivenName + ", " + FamilyName + " (" + Title + ")"; }
        }
        */
    }


}
