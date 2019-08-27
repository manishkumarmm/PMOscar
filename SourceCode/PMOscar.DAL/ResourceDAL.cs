// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceDAL.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : ResourceDAL.cs
// Created Date: 12/04/2012
// Description : Data Access class for Resource.
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

namespace PMOscar.DAL
{
    /// <summary>
    /// DAL class for Resource
    /// </summary>
    public class ResourceDAL: BaseDAL
    {
        /// <summary>
        /// Method to get all Resource by ResourceId
        /// </summary>
        /// <param name="resourceId">ProgramId</param>
        /// <returns>ProjectsResource as DataSet</returns>
        public static DataSet GetResourceDetailsById(int resourceId)
        {
            DataSet dsResourceDetails = new DataSet();

            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ResourceID", resourceId));
                dsResourceDetails = ExecuteSPDataSet("GetResourceDetailsById", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsResourceDetails;
        }
    }
}
