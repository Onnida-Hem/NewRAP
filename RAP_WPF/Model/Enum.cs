using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Model
{
    public class Enum
    {
        #region Researcher
        public enum ResearcherType
        {
            Staff = 1,
            Student = 2,
            All = 0
        }

        public enum Campus
        {
            Hobart,
            Launceston,
            CradleCoast
        }

        public enum JobLevel
        {
            None,
            A,
            B,
            C,
            D,
            E
        }

        public enum Filter
        {
            All,
            Student,
            A,
            B,
            C,
            D,
            E
        }

        public static string JobTitle(JobLevel jobLevel)
        {
            switch (jobLevel)
            {
                case JobLevel.A: return "Research Associate";
                case JobLevel.B: return "Lecturer";
                case JobLevel.C: return "Assistant Professor";
                case JobLevel.D: return "Associate Professor";
                case JobLevel.E: return "Professor";
                default: return "-";
            }
        }
        #endregion

        #region Publication
        public enum Ranking
        {
            Q1 = 1,
            Q2 = 2,
            Q3 = 3,
            Q4 = 4
        }

        public enum PublicationType
        {
            Conference,
            Journal,
            Other
        }

        public static double ExpectationPub(JobLevel jobLevel)
        {
            switch (jobLevel)
            {
                case JobLevel.A: return 0.5;
                case JobLevel.B: return 1;
                case JobLevel.C: return 2;
                case JobLevel.D: return 3.2;
                case JobLevel.E: return 4;
                default: return 0;
            }
        }

        #endregion

        #region Report
        public enum ReportName
        {
            None = 0,
            StarPerformer = 1,
            MeetingMinimum = 2,
            BelowExpectation = 3,
            Poor = 4
        }

        public static ReportName ReportPerformance(double performance)
        {
            switch (performance)
            {
                case var p when p <= 70:
                    return ReportName.Poor;

                case var p when p > 70 && p < 110:
                    return ReportName.BelowExpectation;

                case var p when p >= 110 && p < 200:
                    return ReportName.MeetingMinimum;

                case var p when p >= 200:
                    return ReportName.StarPerformer;

                default:
                    return ReportName.None;
            };
        }
        #endregion
    }
}
