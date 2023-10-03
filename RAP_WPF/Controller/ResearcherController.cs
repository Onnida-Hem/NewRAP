using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAP_WPF.Database;
using RAP_WPF.Datasource;
using RAP_WPF.Model;
using static RAP_WPF.Model.Enum;

namespace RAP_WPF.Controller
{
    class ResearcherController
    {
        public static List<Researcher> allResearcherList;
        public static Researcher selectedResearcher = new Researcher();

        //Barry
        public static List<Researcher> ListResearcher()
        {
            allResearcherList = DBAdapter.FetchResearcherDetails();
            return allResearcherList;
        }

        //Nida
        public static List<Researcher> FilterByName(string nameFilter)
        {
            List<Researcher> filteredResearchers = new List<Researcher>();

            filteredResearchers = allResearcherList.Where(r => r.Fullname.ToLower().Contains(nameFilter.ToLower())).ToList();

            return filteredResearchers;
        }
   
        //Yogeesha
        public static List<Researcher> FilterByLevel(JobLevel levelFilter, ResearcherType typeFilter)
        {
            //List<Researcher> researchers = ListResearcher();

            if (typeFilter == ResearcherType.Student)
            {
                // Filter for students
                return allResearcherList.Where(r => r.Type == ResearcherType.Student).ToList();
            }
            else if (typeFilter == ResearcherType.Staff)
            {
                // Filter for staff members with JobLevel
                return allResearcherList
                    .Where(r => r.Type == ResearcherType.Staff && r.CurrentJobTitle == levelFilter)
                    .ToList();
            }
            else
            {
                // Show all researchers
                return allResearcherList;
            }
            //return new List<Researcher>();
        }


        //Yogeesha
        public static Researcher ShowResearcherDetail(string selectedResearcherID)
        {
            selectedResearcher = DBAdapter.CompleteResearcherDetail(selectedResearcherID);
            return selectedResearcher;
        }

        public static Researcher ShowResearcherPerformance()
        {
            return selectedResearcher;
        }

        //Nida
        public static List<Researcher> ListSupervision(string selectedResearcherID) //(Staff)
        {
            var students = DBAdapter.FetchListSupervision(selectedResearcherID);
            return students;
        }

        //Nida
        public static List<CumulativeCount> CalCumNoOfPublication()
        {
            List<CumulativeCount> cum = PublicationController.selectedPublicationList
                        .GroupBy(p => p.PublicationYear)
                        .Select(g => new CumulativeCount
                        {
                            Year = g.Key,
                            NoOfPublication = g.Count()
                        })
                        .ToList();

            return cum;
        }

        public static List<Funding> LoadFundingList()
        {
            List<Funding> fundingListAll = XMLAdapter.LoadAll();
            return fundingListAll;
        }
    }
}
