using RAP_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RAP_WPF.Model.Enum;

namespace RAP_WPF.Controller
{
    class ReportController
    {
        public static List<ReportPerformance> ReportList = new List<ReportPerformance>();

        public static void GenerateAllReport()
        {
            foreach (Researcher researcher in ResearcherController.allResearcherList.Where(p => p.Type == ResearcherType.Staff))
            {
                ReportPerformance report = new ReportPerformance();
                report.ResearcherID = researcher.ID;
                report.Fullname = researcher.Fullname;
                //report.Performance = researcher.Average3Year;
                //report.FundingReceived = researcher.FundingReceived.ToString("N0") + " AUD";
                report.Email = researcher.Email;
                ReportList.Add(report);
            }
            //return ReportList;
        }

        public static List<ReportPerformance> GenerateReport(ReportName report)
        {
            if (report == ReportName.BelowExpectation || report == ReportName.Poor)
            {
                var reportPerformance = ReportList.Where(r => r.ReportName == report).OrderBy(r => r.Performance).ToList();
                return new List<ReportPerformance>(reportPerformance);
            }
            else
            {
                var reportPerformance = ReportList.Where(r => r.ReportName == report).OrderByDescending(r => r.Performance).ToList();
                return new List<ReportPerformance>(reportPerformance);
            }
        }

        public List<string> CopyAllEmails()
        {
            return new List<string>();
        }
    }
}
