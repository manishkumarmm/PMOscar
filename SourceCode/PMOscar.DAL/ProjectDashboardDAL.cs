// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : ProjectDashboardDAL.cs
// Created Date: 12/04/2012
// Description : Data Access class for Project Dashboard.
// Last Modified Date:
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PMOscar.Core;

namespace PMOscar.DAL
{
    /// <summary>
    /// DAL class for ProjectDashboard.
    /// </summary>
    public class ProjectDashboardDAL : BaseDAL
    {
        /// <summary>
        /// Method to get period Dashboard details.
        /// </summary>
        /// <param name="programId"></param>
        /// <param name="projectId"></param>
        /// <param name="startWeekId"></param>
        /// <param name="endWeekId"></param>
        /// <param name="isIncludeProposalAndVAS"></param>
        /// <returns>Data Set</returns>
        public static DataSet GetPeriodDashboardDetails(int programId, int projectId, int startWeekId, int endWeekId, bool isIncludeProposalAndVAS)
        {
            var dsperiodDashboardDetails = new DataSet();

            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ProgramID", programId));
                parameters.Add(new SqlParameter("@ProjectID", projectId));
                parameters.Add(new SqlParameter("@StartWeekID", startWeekId));
                parameters.Add(new SqlParameter("@EndWeekID", endWeekId));
                parameters.Add(new SqlParameter("@IsIncludeProposalAndVAS", isIncludeProposalAndVAS ? 1 : 0));

                dsperiodDashboardDetails = ExecuteSPDataSet("GetPeriodDashboardDetails", parameters);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsperiodDashboardDetails;
        }
    }
}
