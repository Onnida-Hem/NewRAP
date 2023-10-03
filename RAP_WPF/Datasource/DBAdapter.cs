using MySql.Data.MySqlClient;
using RAP_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RAP_WPF.Model.Enum;

namespace RAP_WPF.Database
{
    abstract class DBAdapter
    {
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        public static T ParseEnum<T>(string value)
        {
            if(value.Contains(" "))
            {
                value = value.Replace(" ", string.Empty);
            }
            return (T)System.Enum.Parse(typeof(T), value);
        }

        #region [Researcher] 
        //Yogeesha
        public static List<Researcher> FetchResearcherDetails()
        {
            List<Researcher> researcherList = new List<Researcher>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name, title, type, level, email from researcher", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    researcherList.Add(new Researcher
                    {
                        ID = rdr.GetString(0),
                        GivenName = rdr.GetString(1),
                        FamilyName = rdr.GetString(2),
                        ResearcherTitle = rdr.GetString(3),
                        Type = ParseEnum<ResearcherType>(rdr.GetString(4)),
                        CurrentJobTitle = rdr.IsDBNull(5) ? JobLevel.None : ParseEnum<JobLevel>(rdr.GetString(5)),
                        Email = rdr.GetString(6)
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return researcherList;
        }

        //Yogeesha
        public static Researcher CompleteResearcherDetail(string selectedResearcherID)
        {
            Researcher researcher = new Researcher();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                string query = "SELECT * FROM researcher " +
                   "WHERE id = @selectedResearcherID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@selectedResearcherID", selectedResearcherID);
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    if (ParseEnum<ResearcherType>(rdr.GetString(1)) == ResearcherType.Staff)
                    {
                        researcher = new Staff
                        {
                            ID = rdr.GetString(0),
                            Type = ParseEnum<ResearcherType>(rdr.GetString(1)),
                            GivenName = rdr.GetString(2),
                            FamilyName = rdr.GetString(3),
                            ResearcherTitle = rdr.GetString(4),
                            School = rdr.GetString(5),
                            Campus = ParseEnum<Campus>(rdr.GetString(6)),
                            Email = rdr.GetString(7),
                            Photo = rdr.GetString(8),
                            CurrentJobTitle = ParseEnum<JobLevel>(rdr.GetString(11)),
                            FirstPositionStartDate = rdr.GetDateTime(12),
                            CurrentPositionStartDate = rdr.GetDateTime(13)
                        };
                        return researcher;
                    }
                    else
                    {
                        researcher = new Student
                        {
                            ID = rdr.GetString(0),
                            Type = ParseEnum<ResearcherType>(rdr.GetString(1)),
                            GivenName = rdr.GetString(2),
                            FamilyName = rdr.GetString(3),
                            ResearcherTitle = rdr.GetString(4),
                            School = rdr.GetString(5),
                            Campus = ParseEnum<Campus>(rdr.GetString(6)),
                            Email = rdr.GetString(7),
                            Photo = rdr.GetString(8),
                            Degree = rdr.GetString(9),
                            SupervisorID = rdr.GetString(10),
                            FirstPositionStartDate = rdr.GetDateTime(12),
                            CurrentPositionStartDate = rdr.GetDateTime(13)
                        };
                        return researcher;
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return researcher;
        }

        //Nida
        public static List<Position> ListPosition(string selectedResearcherID)
        {
            List<Position> positionList = new List<Position>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM position " +
                                                    "WHERE id = @selectedResearcherID", conn);
                cmd.Parameters.AddWithValue("@selectedResearcherID", selectedResearcherID);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    positionList.Add(new Position
                    {
                        JobTitle = JobTitle(ParseEnum<JobLevel>(rdr.GetString(1))),
                        StartDate = rdr.GetDateTime(2).ToString("d"),
                        EndDate = rdr.IsDBNull(3) ? "-" : rdr.GetDateTime(3).ToString("d")
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return positionList;
        }

        //Nida
        public static List<Researcher> FetchListSupervision(string selectedResearcherID)
        {
            List<Researcher> supervisionList = new List<Researcher>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT id, given_name, family_name, title, type from researcher " +
                                                    "WHERE supervisor_id = @selectedResearcherID", conn);
                cmd.Parameters.AddWithValue("@selectedResearcherID", selectedResearcherID);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    supervisionList.Add(new Researcher
                    {
                        ID = rdr.GetString(0),
                        GivenName = rdr.GetString(1),
                        FamilyName = rdr.GetString(2),
                        ResearcherTitle = rdr.GetString(3),
                        Type = ParseEnum<ResearcherType>(rdr.GetString(4))
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return supervisionList;
        }

        #endregion

        #region [Publication]
        //Nida
        public static List<Publication> FetchPublicationDetails()
        {
            List<Publication> publicationList = new List<Publication>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                //MySqlCommand cmd = new MySqlCommand("SELECT researcher_publication.researcher_id, publication.doi, publication.title, publication.year, publication.ranking FROM researcher_publication " +
                //                                    "JOIN publication on researcher_publication.doi = publication.doi " +
                //                                    "WHERE researcher_publication.researcher_id = @selectedResearcherID", conn);
                //cmd.Parameters.AddWithValue("@selectedResearcherID", selectedResearcherID);
                //rdr = cmd.ExecuteReader();

                //while (rdr.Read())
                //{
                //    publicationList.Add(new Publication
                //    {
                //        DOI = rdr.GetString(0),
                //        PublicationTitle = rdr.GetString(1),
                //        PublicationYear = rdr.GetInt32(2),
                //        Ranking = ParseEnum<Ranking>(rdr.GetString(3))
                //    });
                //}

                MySqlCommand cmd = new MySqlCommand("SELECT publication.doi, publication.title, publication.year, publication.ranking, researcher_publication.researcher_id FROM researcher_publication " +
                                                    "JOIN publication on researcher_publication.doi = publication.doi" , conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    publicationList.Add(new Publication
                    {
                        DOI = rdr.GetString(0),
                        PublicationTitle = rdr.GetString(1),
                        PublicationYear = rdr.GetInt32(2),
                        Ranking = ParseEnum<Ranking>(rdr.GetString(3)),
                        ResearchID = rdr.GetString(4)
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return publicationList;
        }

        public static Publication CompletePublicationDetail(string selectedPublicationID)
        {
            Publication publicationDetail = new Publication();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM publication WHERE doi = @selectedPublicationID", conn);
                cmd.Parameters.AddWithValue("@selectedPublicationID", selectedPublicationID);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    publicationDetail.DOI = rdr.GetString(0);
                    publicationDetail.PublicationTitle = rdr.GetString(1);
                    publicationDetail.Ranking = (Ranking)System.Enum.Parse(typeof(Ranking), rdr.GetString(2));
                    publicationDetail.Authors = rdr.GetString(3);
                    publicationDetail.PublicationYear = rdr.GetInt32(4);
                    publicationDetail.Type = (PublicationType)System.Enum.Parse(typeof(PublicationType), rdr.GetString(5));
                    publicationDetail.Cite = rdr.GetString(6);
                    publicationDetail.AvailableDate = DateTime.Parse(rdr.GetString(7));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return publicationDetail;
        }
        #endregion

        #region [Report]
        /*public static string FetchDataForReport()
        {
                List<Researcher> supervisionList = new List<Researcher>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT researcher.id, researcher_publication.doi, publication.year FROM researcher "+
                                                    "JOIN researcher_publication ON researcher.id = researcher_publication.researcher_id " +
                                                    "JOIN publication ON publication.doi = researcher_publication.doi "+
                                                    "WHERE researcher.type = 'Staff'");
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    supervisionList.Add(new Researcher
                    {
                        ID = rdr.GetString(0),
                        GivenName = rdr.GetString(1),
                        FamilyName = rdr.GetString(2),
                        ResearcherTitle = rdr.GetString(3),
                        Type = ParseEnum<ResearcherType>(rdr.GetString(4))
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to database: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return supervisionList;
        }*/
        #endregion
    }
}
