// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : Constants.cs
// Created Date: 06/01/2012
// Description : Class for declaring application constants.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMOscar.Core
{
    /// <summary>
    /// Class for declaring application constants
    /// </summary>
    public class Constants
    {
        #region General

        public const string SELECT = "--Select--";
        public const string ALL = "--All--";
        public const string VALUE = "Value";
        public const string TEXT = "Text";
        public const string PASSWORD_HASH_FORMAT = "MD5";
        public const string DATE_FORMAT = "dd/MM/yyy";
        public const string host = "DBSERVER";
        public const int port = 25;
        public const string Active = "Active";
        public const string InActive = "Inactive";
        public const string ToSort = "toSort DESC";
        public const string SYNC = "Do you want to sync ?";
        public const string NOSYNC = "No data to sync.";

        #endregion

        #region Field Name

        /// <summary>
        /// Constant class for Field Names
        /// </summary>
        public static class FieldName
        {

            #region Project

            /// <summary>
            /// Constant class for Field Names> Project
            /// </summary>
            public static class Project
            {
                public const string PROJECTID = "ProjectId";
                public const string PROJECTNAME = "ProjectName";
                public const string PROGRAMID = "ProgramId";
                public const string SHORTNAME = "ShortName";
                public const string PROJECTCODE = "ProjectCode";
                public const string PROJECTOWNER = "ProjectOwner";
                public const string PROJECTMANAGER = "ProjectManager";
                public const string PROGRAMNAME = "ProgramName";
                public const string CURRENTPHASE = "CurrentPhase";
                

            }

            public static class ProjectDashboard
            {
                public const string PROJECTID = "ProjectId";
                public const string DASHBOARDID = "DashboardID";
                public const string PROJECTNAME = "ProjectName";
                public const string PROGRAMID = "ProgramId";
                public const string PERIOD = "Period";
                public const string DELIVERYDATE = "DeliveryDate";
                public const string REVISEDDELIVERYDATE = "RevisedDeliveryDate";
                public const string MAINTENANCECLOSINGDATE = "MaintClosingDate";
                public const string PROGRAMNAME = "ProgramName";
                public const string CURRENTPHASE = "CurrentPhase";
                public const string CATEGORY = "Category";
                public const string PMCOMMENTS = "PMComments";
                public const string POCOMMENTS = "POComments";
                public const string DELIVERYCOMMENTS = "DeliveryComments";
                public const string WEEKLYCOMMENTS = "WeeklyComments";

            }

            #endregion

            #region Program

            /// <summary>
            /// Constant class for Field Names> Program
            /// </summary>
            public static class Program
            {
                public const string PROGRAMID = "ProgId";
                public const string PROGRAMNAME = "ProgName";

            }

            /// <summary>
            /// Constant class for Field Names> Dashboard
            /// </summary>
            public static class Dashboard
            {
                public const string DASHBOARDID = "DashboardID";
                public const string NAME = "Name";
                public const string FROM_DATE = "FromDate";
                public const string TO_DATE = "ToDate";
                public const string PERIODTYPE = "PeriodType";
                public const string STATUS = "Status";
                public const string TOSORT = "toSort";

            }

            #endregion

            #region User

            /// <summary>
            /// Constant class for Field Names> Mobile Connection
            /// </summary>
            public static class User
            {
                public const string USERID = "UserId";
                public const string USERNAME = "UserName";
                public const string PASSWORD = "Password";
                public const string FIRSTNAME = "FirstName";
                public const string MIDDLENAME = "MiddleName";
                public const string LASTNAME = "LastName";
                public const string EMAILID = "EmailId";
                public const string ROLE = "Role";
                public const string USERROLEID = "UserRoleID";
                public const string STATUS = "Status";
                public const string ISACTIVE = "IsActive";
                public const string MODIFIEDDATE = "ModifiedDate";
                public const string MODIFIEDBY = "ModifiedBy";
                public const string CREATEDBY = "CreatedBy";
                public const string CREATEDDATE = "CreatedDate";
            }

            #endregion

            #region User Role

            /// <summary>
            /// Constant class for User Role
            /// </summary>
            public static class UserRole
            {
                public const string USERROLEID = "UserRoleID";
                public const string USERROLE = "UserRole";
            }

            #endregion

            #region Team


            /// <summary>
            /// Constant class for Field Names> Dashboard
            /// </summary>
            public static class Team
            {
                public const string TEAM = "Team";
                public const string RESOURCENAME = "ResourceName";
               
            }

            #endregion


        }

        #endregion

        #region Session Name

        /// <summary>
        /// Constant class for Session names
        /// </summary>
        public static class SessionName
        {
            public const string USERID = "UserID";
            public const string USERNAME = "UserName";
            public const string ENCRYPTEDPASSWORD = "EncryptedPassword";
            public const string USERFULLNAME = "UserFullName";
            public const string ROLEID = "RoleID";
            public const string USERROLEID = "UserRoleID";
            public const string WORKORDERID = "WorkOrderID";
            public const string SUCCESS = "Success";

        }

        #endregion

        #region HTML

        /// <summary>
        /// Constant class for HTML tags
        /// </summary>
        public static class HTML
        {
            public const string GRID_START_TABLE = "<table width='100%'><tbody><tr><td align='left'><div><table class='dashboardTable' cellspacing='0' cellpadding='4' rules='all' tabindex='-1'>";
            public const string GRID_END_TABLE = "</tbody></table></div></td></tr></tbody></table>";
            public const string TBODY_TR_HEADERROW = "<tbody><tr class='headerRow'>";
            public const string TR_FOOTERROW = "<tr class='footerRow'>";
            public const string TD_CLASS_NUMBER = "<td class=number>";
            public const string TD_CLASS_TOTAL = "<td class='total'>";
            public const string TD_CLASS_NUMBER4 = "<td class='number4'>";
            public const string TD_CLASS_TOTAL4 = "<td class='total4'>";
            public const string TD_CLASS_PROJECTCOL = "<td class='projectCol'>";
            public const string TD_CLASS_REVISEDCOMMENTS = "<td style='width:3%;'>";
            public const string TD_CLASS_PERIDCOL = "<td scope='col' class='periodCol'>";
            public const string TD_CLASS_PHASECOL = "<td class='phaseCol' scope='col'>";
            public const string TD_CLASS_COMMENTSCOL = "<td class='commentsCol'>";
            public const string TD_CLASS_DATECOL = "<td class='dateCol'>";
            public const string TD_CLASS_SICOL = "<td class='slCol' scope='col'>";
            public const string TR_CLASS_ALT = "<tr class='alt'>";
            public const string TD_START = "<td>";
            public const string TD_END = "</td>";
            public const string TR_START = "<tr>";
            public const string TR_END = "</tr>";
            public const string TH_START = "<th>";

            public const string TH_END = "</th>";
            public const string TH_CLASS_PERIDCOL = "<th scope='col' class='periodCol'>";
            public const string TH_CLASS_PERIDCOL_20PERC = "<th scope='col' class='periodCol_20perc'>";
            public const string TH_CLASS_PROJECTCOL = "<th class='projectCol' scope='col'>";
            public const string TH_CLASS_REVISEDCOMMENTS = "<th style='width:3%;' scope='col'>";
            public const string TH_CLASS_PHASECOL = "<th class='phaseCol' scope='col'>";
            public const string TH_CLASS_COMMENTSCOL = "<th class='commentsCol'>";
            public const string TH_CLASS_DATECOL = "<th class='dateCol'>";
            public const string TH_SICOL = "<th class='slCol' scope='col'>";
            public const string INNERTABLE_TR_4COL = "<tr><td class='billType'>Bill.</td><td class='billType'>Bud.</td><td class='billType'>Rev. Bud.</td><td class='billType'>Act.</td></tr>";
            public const string TH_INNERTABLE_TBODY_TR_TD_COLSPAN4 = "<th scope='col' colspan='4'><table class='innerTable' cellspacing='0'><tbody><tr><td class='upper' colspan='4'>";
            public const string INNERTABLE_TR_2COL = "<tr><td>Alloc.</td><td>Act.</td></tr>";
            public const string TH_INNERTABLE_TBODY_TR_TD_COLSPAN2 = "<th scope='col' colspan='2'><table class='innerTable' cellspacing='0'><tbody><tr><td class='upper' colspan='2'>";
            public const string INNERTABLE_TBODY_TABLE_TH_END = "</tbody></table></th>";

        }

        #endregion

        #region Dasboard Tabs"

        /// <summary>
        /// Constant class for Dashboard tabs
        /// </summary>
        public static class DashboardTabs
        {
            public static class ProjectDashboard
            {
                
            }


            public static class PeriodDashboard
            {
                public const string SUMMARY = "Summary";
                public const string PERIOD_EFFOR_PHASE = "PER_EP";
                public const string PERIOD_EFFORT_ROLE = "PER_ER";
                public const string PROJECT_EFFOR_PHASE = "PRJ_EP";
                public const string PROJECT_EFFOR_ROLE = "PRJ_ER";

                public static class Heading
                {
                    public const string BILLABLETOTAL = "BillableTotal";
                    public const string BUDGETTOTAL = "BudgetTotal";
                    public const string REVBUDGETTOTAL = "RevBudgTotal";
                    public const string ACTUALHOURSTOTAL = "ActualHrsTotal";
                    
                }
            }

            
        }

        public static class AddRole
        {
            public const string ADDROLE = "Please add role.";
            public const string SAVE = "Saved.";
            public const string DUPLICATE = "Role already exists.";
            public const string ESTIMATION = "Please add estimation percentage.";
            public const string SHORT = "Please add short name";
            public const string DUPLICATEEMPLOYEECODE = "Employee code already exists.";
            public const string ESTIMATIONERROR = "Maximum estimation hours for Selected Project Is Exceeded the Limit";
            public const string OLDPROJECT= "Its an old project / Check the estimation, billing hours of the project";
            public const string CLOSEDPROJECT = "CLOSED";
            public const string DATEFORMAT = "dd/MM/yyyy";
            public const string CREATEDDASHBOARD = "Dashboard has created successfully.";
            public const string WEEKLYHOURS = "Weekly hours exceeded 40. Please correct.";
            public const string ESTIMATIONPERCENT = "Estimation % should not be greater than 100. Please correct.";
            public const string RELOADDASHBOARD = "Dashboard has reloaded successfully.";
            public const string FINALIZEDDASHBOARD = "Dashboard has finalized successfully.";
            public const string STATUS = "Status has updated successfully.";
            public const string ZERO = "Weekly hours should not be zero. Please correct.";
        }
        #endregion
    }
}
