using RAP_WPF.Database;
using RAP_WPF.Model;
using RAP_WPF.Datasource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Controller
{
    class PublicationController
    {
        public static List<Publication> publicationList;
        public static List<Publication> selectedPublicationList;

        //NIDA
        public static List<Publication> ListAllPublication()
        {
            publicationList = DBAdapter.FetchPublicationDetails();
            return publicationList;
        }

        public static List<Publication> ListPublication(string selectedResearcherID)
        {
            var filterPublicationList = (from Publication p in publicationList
                                         where p.ResearchID == selectedResearcherID
                                         select p).OrderBy(c => c.PublicationTitle).OrderByDescending(c => c.PublicationYear);

            selectedPublicationList = new List<Publication>(filterPublicationList);
            return selectedPublicationList;
        }
        public static List<Publication> FilterByPublication(int fromYearFilter, int toYearFilter, bool recentYearFilter = true)
        {
            List<Publication> filterPublicationList = new List<Publication>();
            List<Publication> orderedPublicationList = new List<Publication>();

            /*
            foreach (Publication p in publicationList)
            {
                if (p.PublicationYear <= toYearFilter && p.PublicationYear >= fromYearFilter)
                {
                    filterPublicationList.Add(p);
                }
            }
            */

            if (recentYearFilter)
            {
                if (fromYearFilter == 0 && toYearFilter == 0)
                {
                    //LINQ
                    var selected = (from Publication p in selectedPublicationList
                                    select p).OrderBy(c => c.PublicationTitle).OrderByDescending(c => c.PublicationYear);
                    return new List<Publication>(selected);
                }
                else
                {
                    var selected = (from Publication p in selectedPublicationList
                                    where p.PublicationYear <= toYearFilter && p.PublicationYear >= fromYearFilter
                                    select p).OrderBy(c => c.PublicationTitle).OrderByDescending(c => c.PublicationYear);
                    return new List<Publication>(selected);
                }
            }
            else
            {
                if (fromYearFilter == 0 && toYearFilter == 0)
                {
                    //LINQ
                    var selected = (from Publication p in selectedPublicationList
                                    select p).OrderBy(c => c.PublicationTitle).OrderBy(c => c.PublicationYear);
                    return new List<Publication>(selected);
                }
                else
                {
                    //LINQ
                    var selected = (from Publication p in selectedPublicationList
                                    where p.PublicationYear <= toYearFilter && p.PublicationYear >= fromYearFilter
                                    select p).OrderBy(c => c.PublicationTitle).OrderBy(c => c.PublicationYear);
                    return new List<Publication>(selected);
                }
            }
            //return orderedPublicationList;
        }

        public static Publication ShowPublicationDetail(string selectedPublicationID)
        {
            return DBAdapter.CompletePublicationDetail(selectedPublicationID);
        }
    }
}
