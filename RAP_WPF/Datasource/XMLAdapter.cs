using RAP_WPF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RAP_WPF.Datasource
{
    public static class XMLAdapter
    {
        //https://stackoverflow.com/questions/40994534/get-relative-path-of-a-file-c-sharp
        //get relative path
        private static string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        private static string filePath = path + "\\Datasource\\Fundings_Rankings.xml";

        public static List<Funding> LoadAll()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(filePath);

            List<Funding> fundinglist = new List<Funding>();

            XmlNodeList fundingNode = xml.SelectNodes("/Projects/Project");

            foreach (XmlNode node in fundingNode)
            {
                Funding funding = new Funding();
                funding.ProjectID = node.Attributes["id"].Value;
                funding.FundingPrice = Convert.ToInt32(node["Funding"].InnerText);
                funding.FundingYear = Convert.ToInt32(node["Year"].InnerText);
                XmlNodeList researcherNodes = node.SelectNodes("Researchers/staff_id");
                if (researcherNodes != null)
                {
                    foreach (XmlNode researcherNode in researcherNodes)
                    {
                        funding.AddResearcherID(researcherNode.InnerText);
                    }
                }
                fundinglist.Add(funding);
            }

            return fundinglist;
        }
    }
}
