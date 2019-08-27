// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : DashboardDAL.cs
// Created Date: 12/04/2012
// Description : Data Access class for Dashboard.
// Last Modified Date:
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PMOscar.Core;
using PMOscar.Model;

namespace PMOscar.DAL
{
    /// <summary>
    /// DAL class for Dashboard.
    /// </summary>
    public class DashboardDAL : BaseDAL
    {




        /// <summary>
        /// Gets all Dashboards.
        /// </summary>
        /// <returns></returns>
        public static List<Dashboard> GetAll()
        {
            var dashboardList = new List<Dashboard>();
            try
            {
                var dtDashboard = new DataTable();
                var parameter = new List<SqlParameter> { new SqlParameter("@DashboardID", 0) }; // Set '0' to get all Dashboard

                dtDashboard = ExecuteSPDataTable("GetDashboard", parameter);

                dashboardList = Enumerable.Select(dtDashboard.AsEnumerable(), d => new Dashboard()
                {
                    DashboardId = (int)d[Constants.FieldName.Dashboard.DASHBOARDID],
                    Name = d[Constants.FieldName.Dashboard.NAME].ToString(),
                    FromDate = d[Constants.FieldName.Dashboard.FROM_DATE].ToString(),
                    ToDate = d[Constants.FieldName.Dashboard.TO_DATE].ToString(),
                    PeriodType = d[Constants.FieldName.Dashboard.PERIODTYPE].ToString(),
                    Status = d[Constants.FieldName.Dashboard.STATUS].ToString()
                }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dashboardList;
        }

        /// <summary>
        /// Gets the dashborad by id.
        /// </summary>
        /// <param name="dashboardId">The dashboard id.</param>
        /// <returns></returns>
        public static Dashboard GetDashboradById(int dashboardId)
        {
            var dashboard = new Dashboard();
            try
            {
                var dtDashboard = new DataTable();
                var parameter = new List<SqlParameter> { new SqlParameter("@DashboardID", dashboardId) };

                dtDashboard = ExecuteSPDataTable("GetDashboard", parameter);

                dashboard =  new Dashboard()
                {
                    DashboardId = (int)dtDashboard.Rows[0][Constants.FieldName.Dashboard.DASHBOARDID],
                    Name = dtDashboard.Rows[0][Constants.FieldName.Dashboard.NAME].ToString(),
                    FromDate = dtDashboard.Rows[0][Constants.FieldName.Dashboard.FROM_DATE].ToString(),
                    ToDate = dtDashboard.Rows[0][Constants.FieldName.Dashboard.TO_DATE].ToString(),
                    PeriodType = dtDashboard.Rows[0][Constants.FieldName.Dashboard.PERIODTYPE].ToString(),
                    Status = dtDashboard.Rows[0][Constants.FieldName.Dashboard.STATUS].ToString()
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dashboard;
        }

        /// <summary>
        /// Gets the dashboard by program ID and project ID.
        /// </summary>
        /// <param name="programId">The program id.</param>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        public static List<Dashboard> GetDashboardByProgramIDAndProjectID(int programId, int projectId)
        {
            var dashboardList = new List<Dashboard>();
            var trans = new List<Dashboard>();
            try
            {
                var dtDashboard = new DataTable();
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ProgramID", programId));
                parameter.Add(new SqlParameter("@ProjectID", projectId));

                dtDashboard = ExecuteSPDataTable("GetDashboardByProgramIDAndProjectID", parameter);

                dashboardList = Enumerable.Select(dtDashboard.AsEnumerable(), d => new Dashboard()
                {
                    DashboardId = (int)d[Constants.FieldName.Dashboard.DASHBOARDID],
                    Name = d[Constants.FieldName.Dashboard.NAME].ToString(),
                    FromDate = d[Constants.FieldName.Dashboard.FROM_DATE].ToString(),
                    ToDate = d[Constants.FieldName.Dashboard.TO_DATE].ToString(),
                    PeriodType = d[Constants.FieldName.Dashboard.PERIODTYPE].ToString(),
                    Status = d[Constants.FieldName.Dashboard.STATUS].ToString(),
                    toSort= Convert.ToDateTime(d[Constants.FieldName.Dashboard.TOSORT]),
                }).ToList();
                trans = dashboardList.OrderByDescending(t => t.toSort).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return trans;
        }
    }
}
