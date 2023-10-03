using RAP_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Model
{
    public class Funding
    {
        public string ProjectID { get; set; }
        public int FundingPrice { get; set; }
        public int FundingYear { get; set; }
        public List<string> FundingResearcherList { get; set; }

        public Funding()
        {
            FundingResearcherList = new List<string>();
        }
        public void AddResearcherID(string researcherID)
        {
            FundingResearcherList.Add(researcherID);
        }
    }
}
