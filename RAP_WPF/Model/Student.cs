using RAP_WPF.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Model
{
    class Student : Researcher
    {
        public string Degree { get; set; }
        public string SupervisorID { get; set; }
        public string SupervisorName
        {
            get
            {
                /*var supervisor = from Researcher r in ResearcherController.allResearcherList
                                  where r.ID == SupervisorID
                                  select r;*/
                var supervisor = ResearcherController.allResearcherList.Where(r => r.ID == SupervisorID).First().Fullname;
                return supervisor.ToString(); 
            }
        }
    }
}
