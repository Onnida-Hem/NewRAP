using RAP_WPF.Controller;
using RAP_WPF.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RAP_WPF.Model.Enum;

namespace RAP_WPF.Model
{
    
    //create this class for report without loading all detail of researcher and publication
    //report button be able to click at the first time when open main window
    class ReportPerformance
    {
        public ReportName ReportName
        {
            get
            {
                return ReportPerformance(Performance);
            }
        }
        public string ResearcherID { get; set; }
        public string Fullname { get; set; }
        //public double Performance { get; set; }
        //public string FundingReceived { get; set; }
        public double Performance
        {
            get
            {
                double countPub3Year = (from Publication p in PublicationController.publicationList
                                     where p.ResearchID == ResearcherID &&
                                     p.PublicationYear < DateTime.Today.Year && p.PublicationYear >= (DateTime.Today.Year - 3)
                                     select p).Count();
                JobLevel jobLevel = DBAdapter.ParseEnum<JobLevel>(ResearcherController.allResearcherList
                                    .Where(p => p.ID == ResearcherID)
                                    .Select(p => p.CurrentJobTitle).FirstOrDefault().ToString());
                double expectedPub = Enum.ExpectationPub(jobLevel);
                return Math.Round(countPub3Year / 3 / expectedPub * 100, 1);
            }
        }
        public string FundingReceived
        {
            get
            {
                var funding = from Funding f in ResearcherController.LoadFundingList()
                              where f.FundingResearcherList.Contains(ResearcherID)
                              select f;
                var fundingReceived = funding.Sum(x => x.FundingPrice);
                return fundingReceived.ToString("N0") + " AUD";
            }
        }
        public string Email { get; set; }
    }
}
