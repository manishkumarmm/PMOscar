// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : ProgramDAL.cs
// Created Date: 12/04/2012
// Description : Data Access class for Program.
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
    /// DAL class for Program.
    /// </summary>
    public class ProgramDAL : BaseDAL
    {
        /// <summary>
        /// Method to get all Programs
        /// </summary>
        /// <returns> Program as DataTable</returns>
        public static List<Program> GetAll()
        {
            var dtPrograms = new DataTable();
            var programList = new List<Program>();

            try
            {
                dtPrograms = ExecuteSPDataTable("GetPrograms", new List<SqlParameter>());

                if(dtPrograms.Rows.Count > 0)
                {
                    programList.AddRange(from DataRow row in dtPrograms.Rows
                                         select new Program { ProgramId = (int) row[Constants.FieldName.Project.PROGRAMID], ProgramName = row[Constants.FieldName.Project.PROGRAMNAME].ToString()});
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return programList;
        }

        /// <summary>
        /// Method to get Program deatils by ProgramId
        /// </summary>
        /// <param name="programId">ProgramId</param>
        /// <returns>Program</returns>
        public static Program GetByProgramId(int programId)
        {
            var dtProgram = new DataTable();
            var programDetails = new Program();

            try
            {
                var parameters = new List<SqlParameter> { new SqlParameter("@ProgramID", programId) };

                dtProgram = ExecuteSPDataTable("GetPrograms", parameters);

                if (dtProgram.Rows.Count > 0)
                {
                  programDetails = new Program
                    {
                        ProgramId = (int)dtProgram.Rows[0][Constants.FieldName.Project.PROGRAMID],
                        ProgramName = dtProgram.Rows[0][Constants.FieldName.Project.PROGRAMNAME].ToString(),
                        
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return programDetails;
        }
    }
}
