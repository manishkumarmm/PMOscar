
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="UserCreation.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
//  Description  : 
//  Author       : 
//  Created Date : 
//  Modified By  : Vibin MB
//  Modified Date: 11/04/2018
// </summary>
// -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



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
    /// DAL class for BillingDetails.
    /// </summary>
    public class BillingDetailsDAL :BaseDAL
    {

        #region "Get Billing Details by month and year"

        /// <summary>
        /// Gets the billing details by month year.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public DataSet GetBillingDetailsByMonthYear(string month, string year)
        {
            var dsBillingDetails = new DataSet();
       
           
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@month", month));
                parameter.Add(new SqlParameter("@year", year));


                dsBillingDetails = ExecuteSPDataSet("GetBillingDetailsByMonthYear", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsBillingDetails;
        }

       #endregion 

        #region "Export billing details"
        /// <summary>
        /// Exports the billing details.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public DataTable  ExportBillingDetails(string month, string year)
        {
            var dsExportBillingDetails = new DataTable();
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@Month", month));
                parameter.Add(new SqlParameter("@year", year));


                dsExportBillingDetails = ExecuteSPDataSet("[ExportBillingDetails]", parameter).Tables[0];
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsExportBillingDetails;

        }
        #endregion

        #region "Get Billing Details based on project"
        /// <summary>
        /// Gets the project wise billing details.
        /// </summary>
        /// <param name="monthPart">The month part.</param>
        /// <param name="year">The year.</param>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        public DataSet GetProjectWiseBillingDetails(string monthPart, int year,string projectId)
        {
            var dsBillingDetails = new DataSet();
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@month", monthPart));
                parameter.Add(new SqlParameter("@year", year));
                parameter.Add(new SqlParameter("@projectId", projectId));
                dsBillingDetails = ExecuteSPDataSet("[GetProjectWiseBillingDetails]", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsBillingDetails;

        }

        /// <summary>
        /// Gets the yearly project billing details.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        public DataSet GetYearlyProjectBillingDetails(int year, string projectId)
        {
            var dsBillingDetails = new DataSet();
            try
            {
                var parameter = new List<SqlParameter>();               
                parameter.Add(new SqlParameter("@Year", year));
                parameter.Add(new SqlParameter("@ProjectID", projectId));
                dsBillingDetails = ExecuteSPDataSet("GetYearlyProjectBillingDetails", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsBillingDetails;
        }

        #endregion

        #region "Get resource name by Id"
        /// <summary>
        /// Gets the resourcename id.
        /// </summary>
        /// <param name="editid">The editid.</param>
        /// <returns></returns>
        public DataSet GetResourcenameId(int editid)
        {
            var dsBillingDetailsByResource = new DataSet();
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@editid", editid));

                dsBillingDetailsByResource = ExecuteSPDataSet("[GetResourcenameId]", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsBillingDetailsByResource;

        }
        #endregion

        #region "Get billing details based on billingId"
        /// <summary>
        /// Getbillings the details.
        /// </summary>
        /// <param name="editid">The editid.</param>
        /// <param name="billingId">The billing id.</param>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        public DataSet GetbillingDetails(int editid,string  billingId,int projectId, int month ,int year)
        {
            var dsBillingDetailsByResource = new DataSet();
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@editid", editid));  	
                parameter.Add(new SqlParameter("@billingId",billingId));
                parameter.Add(new SqlParameter("@projectId",projectId));
                parameter.Add(new SqlParameter("@month", month));
                parameter.Add(new SqlParameter("@year", year));

                dsBillingDetailsByResource = ExecuteSPDataSet("GetBillingDetails", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsBillingDetailsByResource;

        }
        #endregion

        #region "Get ProjectName by ProjectId"
        /// <summary>
        /// Gets the project name by id.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        public DataSet GetProjectNameById(int projectId)
        {
            var dsProjectNameById = new DataSet();
            try
            {
                var parameter = new List<SqlParameter>();
              
                parameter.Add(new SqlParameter("@projectId", projectId));

                dsProjectNameById = ExecuteSPDataSet("GetProjectNameById", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsProjectNameById;

        }
        #endregion

        #region "Insert to billing details audit table"
        /// <summary>
        /// Inserts the into billing details audit.
        /// </summary>
        /// <param name="BillingID">The billing ID.</param>
        /// <returns></returns>
        public DataSet InsertIntoBillingDetailsAudit(string BillingID)
        {
            var dsBillingDetails = new DataSet();
            try
            {
                var parameter = new List<SqlParameter>();

                parameter.Add(new SqlParameter("@BillingID", BillingID));

                dsBillingDetails = ExecuteSPDataSet("InsertIntoBillingDetailsAudit", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsBillingDetails;

        }
        #endregion

        #region "delete from billing details table"
        /// <summary>
        /// Deletes the billing details.
        /// </summary>
        /// <param name="BillingID">The billing ID.</param>
        /// <returns></returns>
        public DataSet DeleteBillingDetails(string BillingID)
        {
            var dsBillingDetailsDeleteId = new DataSet();
            try
            {
                var parameter = new List<SqlParameter>();

                parameter.Add(new SqlParameter("@BillingID", BillingID));

                dsBillingDetailsDeleteId = ExecuteSPDataSet("DeleteBillingDetails", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsBillingDetailsDeleteId;

        }
        #endregion

        #region "check for duplicate billing entry"
        /// <summary>
        /// Billings the details check duplicate.
        /// </summary>
        /// <param name="resourceId">The resource id.</param>
        /// <param name="projectId">The project id.</param>
        /// <param name="roleId">The role id.</param>
        /// <param name="BillingID">The billing ID.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public DataSet BillingDetailsCheckDuplicate(int resourceId,int projectId,int roleId,string BillingID,int month,int year)
        {
            var dscount = new DataSet();
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ResourceID", resourceId));
                parameter.Add(new SqlParameter("@ProjectID", projectId));
                parameter.Add(new SqlParameter("@RoleID", roleId));
                parameter.Add(new SqlParameter("@BillingID", BillingID));
                parameter.Add(new SqlParameter("@month", month));
                parameter.Add(new SqlParameter("@year", year));
                dscount = ExecuteSPDataSet("[BillingDetailsCheckDuplicate]", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dscount;

        }
        #endregion

        #region "Billing Details given resourceid,projectid,roleid,month and year"
        /// <summary>
        /// Billings the detailsby resource role project.
        /// </summary>
        /// <param name="resourceId">The resource id.</param>
        /// <param name="projectId">The project id.</param>
        /// <param name="roleId">The role id.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public DataSet BillingDetailsbyResourceRoleProject(string resourceId, int projectId, string roleId,int month,int year)
        {
            var dsBillingDetailsbyResourceRoleProject = new DataSet();
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ResourceID", resourceId));
                parameter.Add(new SqlParameter("@ProjectID", projectId));
                parameter.Add(new SqlParameter("@RoleID", roleId));
                parameter.Add(new SqlParameter("@month", month));
                parameter.Add(new SqlParameter("@year", year));
                dsBillingDetailsbyResourceRoleProject = ExecuteSPDataSet("BillingDetailsbyResourceRoleProject", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsBillingDetailsbyResourceRoleProject;

        }
        #endregion

        #region "get count of billing details"
        public int CountofBillingDetailsbyResourceRoleProject(string resourceId, int projectId, int roleId,int month,int year)
        {
             int  count=0;
          
               var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ResourceID", resourceId));
                parameter.Add(new SqlParameter("@ProjectID", projectId));
                parameter.Add(new SqlParameter("@RoleID", roleId));
                parameter.Add(new SqlParameter("@month", month));
                parameter.Add(new SqlParameter("@year", year));

               count= Convert.ToInt32(ExecuteSPScalar("CountofBillingDetailsbyResourceRoleProject", parameter));
           
            return count;

        }
        #endregion

        #region "Get Details by resourceId"
        /// <summary>
        /// Gets the billing details by resource ID.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="roleId">The role id.</param>
        /// <param name="projectId">The project id.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public DataSet GetBillingDetailsByResourceID(string resource, int roleId, int projectId, string month, string year)
        {
            var dsBillingDetailsNameByResourceID = new DataSet();
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@resourceId", resource));
                parameter.Add(new SqlParameter("@roleId", roleId));
                parameter.Add(new SqlParameter("@project", projectId));
               
                parameter.Add(new SqlParameter("@month",month ));
                parameter.Add(new SqlParameter("@year", year));

                dsBillingDetailsNameByResourceID = ExecuteSPDataSet("[GetBillingDetailsByResourceID]", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsBillingDetailsNameByResourceID;

        }

        #endregion

        #region "Get sum of planned and actual billing"
        /// <summary>
        /// Gets the sum of planned actual billing.
        /// </summary>
        /// <param name="monthPart">The month part.</param>
        /// <param name="year">The year.</param>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        public DataSet GetSumOfPlannedActualBilling(int monthPart, int year, int projectId)
        {
            var dsBillingDetails = new DataSet();
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@month", monthPart));
                parameter.Add(new SqlParameter("@year", year));
                parameter.Add(new SqlParameter("@projectId", projectId));
                dsBillingDetails = ExecuteSPDataSet("[GetSumOfPlannedActualBilling]", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsBillingDetails;

        }
        #endregion

        #region "Bind role"
        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <returns></returns>
        public DataTable GetRole()
        {
            var dtRoles = new DataTable();
            try
            {
                var parameter = new List<SqlParameter>();
            
                 dtRoles = BaseDAL.ExecuteSPDataTable("GetRole", parameter);
               
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dtRoles;

        }
        #endregion

        #region "Bind resource"
        /// <summary>
        /// Gets all resource.
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllResource()
        {
            var dtResource= new DataTable();
            try
            {
                var parameter = new List<SqlParameter>();

                dtResource = BaseDAL.ExecuteSPDataTable("GetAllResource", parameter);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dtResource;

        }
        #endregion

        #region "Bind Project"
        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllProjects(string month,int year)
        {
            var dtprojects = new DataTable();
            try
            {
                string yearvalue = year.ToString();
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@month", month));
                parameter.Add(new SqlParameter("@year", yearvalue));
                dtprojects = BaseDAL.ExecuteSPDataTable("GetAllProjects", parameter);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dtprojects;

        }
        #endregion

        #region "Insert BillingDetails"
        /// <summary>
        /// Inserts the billing details.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="projectId">The project id.</param>
        /// <param name="roleId">The role id.</param>
        /// <param name="planned">The planned.</param>
        /// <param name="actual">The actual.</param>
        /// <param name="actual">The actual spent.</param>
        /// <param name="actual">The comments.</param>
        /// <param name="fromdate">The fromdate.</param>
        /// <param name="todate">The todate.</param>
        /// <param name="createdBy">The created by.</param>
        /// <param name="createdDate">The created date.</param>
        /// <param name="updatedBy">The updated by.</param>
        /// <param name="updatedDate">The updated date.</param>
        /// <param name="editResource">The edit resource.</param>
        /// <param name="status">The status.</param>
        /// <param name="editrole">The editrole.</param>
        /// <param name="opmode">The opmode.</param>
        /// <param name="billingId">The billing id.</param>
        /// <returns></returns>
        public DataSet InsertBillingDetails(string resource, int projectId, string  roleId, string  planned, string  actual,string actualSpent,string comments, DateTime fromdate, DateTime todate, object  createdBy, DateTime createdDate, object  updatedBy, DateTime updatedDate, int editResource, string  status, int
            editrole,string opmode,string billingId,int iFlag)
        {
            var dsBillingDetails1 = new DataSet();


            try
            {
                var parameter = new List<SqlParameter>();
                
                                parameter.Add(new SqlParameter("@ResourceId", resource));
                                parameter.Add(new SqlParameter("@ProjectId", projectId));
                                parameter.Add(new SqlParameter("@RoleId", roleId));
                                parameter.Add(new SqlParameter("@Planned", planned));
                                parameter.Add(new SqlParameter("@Actual", actual));
                                parameter.Add(new SqlParameter("@Actualspent", actualSpent));
                                parameter.Add(new SqlParameter("@FromDate", fromdate));
                                parameter.Add(new SqlParameter("@ToDate", todate));
                                parameter.Add(new SqlParameter("@CreatedBy", createdBy));
                                parameter.Add(new SqlParameter("@CreatedDate", createdDate));
                                parameter.Add(new SqlParameter("@UpdatedBy", updatedBy));
                                parameter.Add(new SqlParameter("@UpdatedDate", updatedDate));
                                parameter.Add(new SqlParameter("@editResource", editResource));
                                parameter.Add(new SqlParameter("@status", status));
                                parameter.Add(new SqlParameter("@editRole", editrole));
                                parameter.Add(new SqlParameter("@opmode", opmode));
                                parameter.Add(new SqlParameter("@BillingID", billingId));
                                parameter.Add(new SqlParameter("StatusFlag", iFlag));
                                parameter.Add(new SqlParameter("@Comments", comments));
               

                                dsBillingDetails1 = BaseDAL.ExecuteSPDataSet("InsertBillingDetails", parameter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return dsBillingDetails1;
        }


        #endregion

        #region "Resource Exists in Timetracker"

        /// <summary>
        /// check if an allocation exists in resource planning for the given project,month and year with roleId.
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// <param name="year">year</param>
        /// <param name="month">month</param>
        /// <param name="resourceId">resourceId</param>
        /// <param name="roleId">roleId</param>
        /// <returns>resoursceExists</returns>
        public bool CheckResourceExistsORNot(int projectId,int year,int month,int resourceId,int roleId)
        {
            bool resoursceExists = false;
             
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ProjectId", projectId));
                parameter.Add(new SqlParameter("@Year", year));
                parameter.Add(new SqlParameter("@Month", month));
                parameter.Add(new SqlParameter("@ResourceId", resourceId));
                parameter.Add(new SqlParameter("@RoleId", roleId));
                resoursceExists = Convert.ToBoolean(BaseDAL.ExecuteSPScalar("Resource_ExistORNotInTimeTracker", parameter));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
            return resoursceExists;
        }
        #endregion

        /// <summary>
        /// Freezes the project.
        /// </summary>        
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns></returns>       

        public int FreezeProject( string year, string month)
        {
            return ExecuteNonQuery("UPDATE [BillingDetails] SET [Freeze] = 1 WHERE   DATEPART(month,fromDate) = '" + month + "' AND DATEPART(year,fromDate) = " + year);
        }

        /// <summary>
        /// Determines whether the specified project identifier is freezed.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        public bool IsFreezed(int projectId, int year, string month)
        {
            return Convert.ToBoolean(ExecuteScalar("SELECT [Freeze] FROM [BillingDetails] WHERE [ProjectId] = " + projectId + " AND DATEPART(month,fromDate) = '" + month + "' AND DATEPART(year,fromDate) = " + year));
        }

       /// <summary>
        /// To Get Sum Of Actual Spent Hours 
       /// </summary>
        /// <param name="resource">resource</param>
        /// <param name="roleId">roleId</param>
        /// <param name="projectId">projectId</param>
        /// <param name="month">month</param>
        /// <param name="year">year</param>
       /// <returns></returns>
        public decimal GetSumOfActualSpentHours(int projectId,int resourceId, string fromDate, string toDate)
        {
            var dsTimes = new DataSet();
            decimal actualSpentHours = 0;
            try
            {            
                dsTimes = BaseDAL.ExecuteDataSet("select ISNULL(sum(tt.ActualHours),0) from [Timetracker] tt where tt.ResourceId="+resourceId+" and tt.ProjectId=" + projectId + 
 " AND ((tt.fromDate>=CONVERT(Datetime, '"+ fromDate+"', 105) AND tt.fromDate<=CONVERT(Datetime, '"+toDate+"', 105)) OR (tt.ToDate>=CONVERT(Datetime, '"+fromDate+
 "', 105) AND tt.ToDate<=CONVERT(Datetime, '"+toDate+"', 105)))");
                if (dsTimes.Tables[0].Rows.Count>0)
                actualSpentHours =Convert.ToDecimal(dsTimes.Tables[0].Rows[0].ItemArray[0].ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return actualSpentHours;

        }
    }
}
