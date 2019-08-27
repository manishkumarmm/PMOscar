// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : ProjectDAL.cs
// Created Date: 12/04/2012
// Description : Data Access class for Project.
// Last Modified Date:
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using PMOscar.Core;
using PMOscar.Model;

namespace PMOscar.DAL
{
    /// <summary>
    /// DAL class for Project.
    /// </summary>
    public class ProjectDAL : BaseDAL
    {
        /// <summary>
        /// Method to get all projects
        /// </summary>
        /// <returns>Projects as DataTable</returns>
        public DataTable GetAllProjects()
        {
            var dtProjects = new DataTable();

            try
            {
                dtProjects = ExecuteSPDataTable("GetProjects", new List<SqlParameter>());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dtProjects;
        }

        /// <summary>
        /// Method to get all Projects by ProgramId
        /// </summary>
        /// <param name="programId">ProgramId</param>
        /// <returns>Projects as DataTable</returns>
        public static Project GetProjectDetailsById(int projectId)
        {
            var projectDetails = new Project();

            try
            {
                var parameter = new List<SqlParameter> { new SqlParameter("@ProjectID", projectId) };
                var dtProject = new DataTable();

                dtProject = ExecuteSPDataTable("GetProjectDetailsById", parameter);

                if(dtProject.Rows.Count > 0)
                {
                    projectDetails = new Project
                                         {
                                             ProjectId = (int) dtProject.Rows[0][Constants.FieldName.Project.PROJECTID],
                                             ProjectName = dtProject.Rows[0][Constants.FieldName.Project.PROJECTNAME].ToString(),
                                             ShortName = dtProject.Rows[0][Constants.FieldName.Project.SHORTNAME].ToString(),
                                             ProjectCode = dtProject.Rows[0][Constants.FieldName.Project.PROJECTCODE].ToString(),
                                             CurrentPhase = dtProject.Rows[0][Constants.FieldName.Project.CURRENTPHASE].ToString(),
                                             ProgramId = (int)dtProject.Rows[0][Constants.FieldName.Project.PROGRAMID],
                                             ProgramName = dtProject.Rows[0][Constants.FieldName.Project.PROGRAMNAME].ToString(),
                                             ProjectOwner = dtProject.Rows[0][Constants.FieldName.Project.PROJECTOWNER].ToString(),
                                             ProjectManager = dtProject.Rows[0][Constants.FieldName.Project.PROJECTMANAGER].ToString()
                                         };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return projectDetails;
        }

        /// <summary>
        /// Method to get all Projects by ProgramId
        /// </summary>
        /// <param name="programId">ProgramId</param>
        /// <returns>Projects as DataTable</returns>
        public static DataTable GetProjectsByProgramId(int programId)
        {
            var dtProjects = new DataTable();

            try
            {
                var parameters = new List<SqlParameter> {new SqlParameter("@ProgramID", programId)};

                dtProjects = ExecuteSPDataTable("GetProjectsByProgramID", parameters);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dtProjects;
        }
    }
}
