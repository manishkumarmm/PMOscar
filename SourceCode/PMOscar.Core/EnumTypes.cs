// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : EnumTypes.cs
// Created Date: 06/01/2012
// Description : Class for declairing enumerators.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PMOscar.Utility
{
    /// <summary>
    /// Class for declairing enumerators
    /// </summary>
    public static class EnumTypes
    {
        /// <summary>
        /// Enum for reports
        /// </summary>
        public enum Reports
        {

            Select = 0,
            /// <summary>
            /// Project_Utilization
            /// </summary>
            Project_Wise_Utilization_Report = 1,
            /// <summary>
            /// Team_Utilization
            /// </summary>
            Team_Wise_Utilization_Report = 2,


            /// <summary>
            /// Resource utilization
            /// </summary>
            Resource_Wise_Utilization_Report = 3,

            /// <summary>
            /// Company Utilization
            /// </summary>
            Company_Utilization_Report = 4,

            /// <summary>
            /// The Budget revision report
            /// </summary>
            //Budget_Revision_Report = 5

            /// <summary>
            /// The Open Hours report
            /// </summary>
            Open_Hours_Report = 6,
            /// <summary>
            /// The Open Hours report with break up
            /// </summary>
            Open_Hours_Report_With_Break_Up = 7
        }

        /// <summary>
        /// UserRoles
        /// </summary>
        public enum UserRoles
        {
            /// <summary>
            /// PResource_Utilization
            /// </summary>
            ProjectOwner = 1,
            /// <summary>
            /// Team_Utilization
            /// </summary>
            ProjectManager = 2,
            Employees = 5,
        }

        /// <summary>
        /// Insert/Update mode
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// Insert
            /// </summary>
            Insert = 1,
            /// <summary>
            /// Update
            /// </summary>
            Update = 2
        }

        /// <summary>
        /// Month
        /// </summary>

        public enum Month
        {
            /// <summary>
            /// First Month
            /// </summary>
            First = 1,
            /// <summary>
            /// Last Month
            /// </summary>
            Last = 12
        }

        public enum Estimation
        {
            MinEstimation=8,
            MinDays=7,
            WeeklyHours=40,
            EstimationPercentage=100
            
        }
    }

}
