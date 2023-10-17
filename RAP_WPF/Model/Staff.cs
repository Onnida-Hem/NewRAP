using RAP_WPF.Controller;
using RAP_WPF.Database;
using RAP_WPF.Datasource;
using RAP_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Model
{
    class Staff : Researcher
    {
        public List<Researcher> SupervisionList
        {
            get
            {
                return ResearcherController.ListSupervision(ID);
            }
        }

        public double Average3Year
        {
            get
            {
                int countPub3Year = (from Publication p in PublicationController.publicationList
                                     where p.ResearchID == ID &&
                                     p.PublicationYear < DateTime.Today.Year && p.PublicationYear >= (DateTime.Today.Year - 3) && p.PublicationYear <= (DateTime.Today.Year - 3)
                                     select p).Count();
                double expectedPub = Enum.ExpectationPub(CurrentJobTitle);
                return Math.Round(countPub3Year / 3 / expectedPub * 100, 1);
            }
        }

        public double FundingReceived
        {
            get
            {
                var funding = from Funding f in ResearcherController.LoadFundingList()
                              where f.FundingResearcherList.Contains(ID)
                              select f;
                var fundingReceived = funding.Sum(x => x.FundingPrice);
                return fundingReceived;
            }
        }

        public int PublicationPerformance
        {
            get
            {
                if (TenureYear == 0)
                    return 0;
                else
                    return PublicationCount / TenureYear;
            }
        }

        public double FundingReceivedPerformance
        {
            get
            {
                if (TenureYear == 0)
                    return 0;
                else
                    return Math.Round(FundingReceived / TenureYear, 1);
            }
        }

        public List<Position> Positions
        {
            get
            {
                return DBAdapter.ListPosition(ID);
            }
        }
    }
}
