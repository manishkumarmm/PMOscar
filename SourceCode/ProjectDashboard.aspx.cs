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
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using PMOscar.Core;
using PMOscar.DAL;
using PMOscar.Model;
using System.Web.Services;

namespace PMOscar
{
    public partial class ProjectDashboard1 : System.Web.UI.Page
    {
        # region"Declarations"

        string dayFrom = string.Empty;
        string dayTo = string.Empty;        
        DataTable dtReview;
        DataTable dtProject;
        DataTable dtPeriod;
        string fromName = string.Empty;
        string toName = string.Empty;    
        DataTable dtDashboard = new DataTable();
        DataTable dtEstimation = new DataTable();        
        #endregion

        #region"Page Events"     

        protected void Page_Load(object sender, EventArgs e)
        { 
            btnDashboard.Attributes.Add("onclick", "window.open('CreateDashboard.aspx','','height=200,width=600');return false");

            if ( Session["UserName"] == null )            
                Response.Redirect("Default.aspx");            
            else
            {
                if (Convert.ToInt16(Session["UserRoleID"]) == 4) // Checking role of user by UserRoleID ( 4=Sys Admin)
                {
                    #region
                    //Only User tab is Visible for Sys admin
                    HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                    if (hcliuser != null)
                        (Page.Master.FindControl("liuser") as HtmlControl).Visible = true;

                    HtmlControl hcauser = Page.Master.FindControl("auser") as HtmlControl;
                    if (hcauser != null)
                        (Page.Master.FindControl("auser") as HtmlControl).Attributes.Add("class", "active");
                    //Hides the tabs other than User for Sys Admin role
                    HtmlControl hcliadminWO = Page.Master.FindControl("liadminWO") as HtmlControl;
                    if (hcliadminWO != null)
                        (Page.Master.FindControl("liadminWO") as HtmlControl).Style.Add("display", "none");

                    HtmlControl hcliadminCR = Page.Master.FindControl("liadminCR") as HtmlControl;
                    if (hcliadminCR != null)
                        (Page.Master.FindControl("liadminCR") as HtmlControl).Style.Add("display", "none");

                    HtmlControl hcliadminPM = Page.Master.FindControl("liadminPM") as HtmlControl;
                    if (hcliadminPM != null)
                        (Page.Master.FindControl("liadminPM") as HtmlControl).Style.Add("display", "none");

                    HtmlControl hcliadminPD = Page.Master.FindControl("liadminPD") as HtmlControl;
                    if (hcliadminPD != null)
                        (Page.Master.FindControl("liadminPD") as HtmlControl).Style.Add("display", "none");

                    HtmlControl hcliadminRP = Page.Master.FindControl("liadminRP") as HtmlControl;
                    if (hcliadminRP != null)
                        (Page.Master.FindControl("liadminRP") as HtmlControl).Style.Add("display", "none");

                    HtmlControl hcliadminR = Page.Master.FindControl("liadminR") as HtmlControl;
                    if (hcliadminR != null)
                        (Page.Master.FindControl("liadminR") as HtmlControl).Style.Add("display", "none");

                    HtmlControl hcliadminBD = Page.Master.FindControl("liadminBD") as HtmlControl;
                    if (hcliadminBD != null)
                        (Page.Master.FindControl("liadminBD") as HtmlControl).Style.Add("display", "none");

                    HtmlControl hcliadminPL = Page.Master.FindControl("liadminPL") as HtmlControl;
                    if (hcliadminPL != null)
                        (Page.Master.FindControl("liadminPL") as HtmlControl).Style.Add("display", "none");

                    HtmlControl hcliadminRL = Page.Master.FindControl("liadminRL") as HtmlControl;
                    if (hcliadminRL != null)
                        (Page.Master.FindControl("liadminRL") as HtmlControl).Style.Add("display", "none");
                    #endregion
                    Response.Redirect("AccessDenied.aspx");
                }
                else if (Convert.ToInt16(Session["UserRoleID"]) == 1) // Checking role of user by UserRoleID ( 1 = ProjectOwner , 2 = ProjectManager )
                {
                    HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                    if (hcliuser != null)
                        (Page.Master.FindControl("liuser") as HtmlControl).Visible = true;

                    HtmlControl hcauser = Page.Master.FindControl("auser") as HtmlControl;
                    if (hcauser != null)
                        (Page.Master.FindControl("auser") as HtmlControl).Attributes.Add("class", "");
                }
                else
                {
                    HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                    if (hcliuser != null)
                        (Page.Master.FindControl("liuser") as HtmlControl).Visible = false;
                }
            }

            lblHeading.Text = string.Empty;

            if (Convert.ToInt32(Session["UserRoleID"]) == 2) // Checking role of user by UserRoleID ( 1 = ProjectOwner , 2 = ProjectManager )
            {
                btnDashboard.Enabled = false;
                btnFinalize.Enabled = false;
                //btnRefreshDropDownList.Enabled = false;  
            }
            else
            {
                //string selected = ddlReports.SelectedItem.Text;
                //DataTable dtDashBoardFinalize = PMOscar.BaseDAL.ExecuteDataTable("Select Status from Dashboard where DashboardID=" + ddlReports.SelectedItem.Value);
                //if (dtDashBoardFinalize.Rows.Count > 0)
                //{
                //    if (dtDashBoardFinalize.Rows[0]["Status"].ToString() == "F")
                //    {
                //        btnRefresh.Enabled = false;
                //        //btnRefreshDropDownList.Enabled = false;
                //        btnFinalize.Enabled = false;
                //    }
                //    else
                //    {
                //        btnRefresh.Enabled = true;
                //        btnFinalize.Enabled = true;
                //        //btnRefreshDropDownList.Enabled = true;
                //    }
                //}
                //  btnDashboard.Enabled = true;
                //  btnFinalize.Enabled = true;  
            }
            if ( !IsPostBack )
            {
                FillDashBoardWeek(0); // Method to fill the DashBoard week Details in DropDownList...
                BindProjectDashBoardDetailsGrid(); // Method to Bind ProjectDashBoard Details...

                if ( Session["dtDatas1"] != null )
                {
                    DataTable dtSessionData = (DataTable)Session["dtDatas1"];
                   
                    if ( dtSessionData.Rows.Count > 0 )
                    {
                        if (!String.IsNullOrEmpty(dtSessionData.Rows[0]["dashboardid"].ToString()))
                        {
                            ddlProjectPeriod.SelectedIndex = dtSessionData.Rows[0]["status"].ToString() == "W" ? 0 : 1;

                            FillDashBoardWeek(Convert.ToInt64(dtSessionData.Rows[0]["dashboardid"])); // Method to fill the DashBoard week Details in DropDownList...
                            DataTable dtDashBoardFinalize = PMOscar.BaseDAL.ExecuteDataTable("Select Status from Dashboard where DashboardID=" + dtSessionData.Rows[0]["dashboardid"]);
                            if (dtDashBoardFinalize.Rows.Count > 0)
                            {
                                if (dtDashBoardFinalize.Rows[0]["Status"].ToString() == "F")
                                {
                                    btnRefresh.Enabled = false;
                                    //btnRefreshDropDownList.Enabled = false;
                                    btnFinalize.Enabled = false;
                                }
                                else
                                {
                                    btnRefresh.Enabled = true;
                                    btnFinalize.Enabled = true;
                                    //btnRefreshDropDownList.Enabled = true;
                                }
                            }
                            DevelopingProjectDashBoardDetails();  // Method to develop project dashbaord details...
                        }
                    }                    
                }

             //   btnRefresh.Enabled = false;

                if (ddlReports.SelectedIndex == 1)
                {

                    DataTable dtDashBoardFinalize = PMOscar.BaseDAL.ExecuteDataTable("Select Status from Dashboard where DashboardID=" + ddlReports.SelectedItem.Value);
                    if (dtDashBoardFinalize.Rows.Count > 0)
                    {
                        if (dtDashBoardFinalize.Rows[0]["Status"].ToString() == "F")
                        {
                            btnRefresh.Enabled = false;
                            //btnRefreshDropDownList.Enabled = false;
                            btnFinalize.Enabled = false;
                        }
                        else
                        {
                            btnRefresh.Enabled = true;
                            btnFinalize.Enabled = true;
                            //btnRefreshDropDownList.Enabled = true;
                        }
                    }

                   // btnRefresh.Enabled = true;
                }
            }

            ddlProjectPeriod.Focus();
           
        }

        #endregion           

        #region"Methods"

        #region "Web Methods"

        #region Bind Dropdowns

        /// <summary>
        /// Static method to bind Project Name dropdown
        /// </summary>
       
        [WebMethod]
        public static List<Model.Project> BindProjectName(int programId)
        {
            DataTable dtProjects = ProjectDAL.GetProjectsByProgramId(programId);
            var projects = Enumerable.Select(dtProjects.AsEnumerable(), p => new Model.Project {ProjectId = (int) p[0], ProjectName = p[1].ToString()}).ToList();

            return projects;
        }

        /// <summary>
        /// Static method to bind Program Name dropdown
        /// </summary>
        [WebMethod]
        public static List<Program> BindProgram()
        {
            return ProgramDAL.GetAll();
        }

        /// <summary>
        /// Method to bind Project EndingWeek dropdown
        /// </summary>

        [WebMethod]
        public static List<Dashboard> BindStartWeek(int programId, int projectId)
        {
            return DashboardDAL.GetDashboardByProgramIDAndProjectID(programId, projectId);
        }
        /// <summary>
        /// Method to bind Project EndingWeek dropdown
        /// </summary>
        [WebMethod]
        public static List<Dashboard> BindEndWeek(int programId, int projectId, int startWeekId)
        {
            if(startWeekId > 0)
            {

                string query1 = String.Format("select FromDate from Dashboard where DashboardID={0}",startWeekId);
                object startweek= BaseDAL.ExecuteScalar(query1);
                var dashbords = DashboardDAL.GetDashboardByProgramIDAndProjectID(programId, projectId);
                return dashbords.Where(x => x.toSort >=Convert.ToDateTime(startweek)).ToList();
            }

            return DashboardDAL.GetDashboardByProgramIDAndProjectID(programId, projectId);
        }

        #endregion

        #region "Bind Period Dashborad Tabs"

        /// <summary>
        /// Web method to Bind Period Dashboard Summary details
        /// </summary>
        /// <returns>HTML string</returns>
        [WebMethod]
        public static List<string> BindPeriodDashboardTabs(int programId, int projectId, int startWeekId, int endWeekId, bool isChecked)
        {
            var dashboardHTMLTables = new List<string>();

            var dsPeriodDashboardDetails = ProjectDashboardDAL.GetPeriodDashboardDetails(programId, projectId, startWeekId, endWeekId, isChecked);

            if (dsPeriodDashboardDetails.Tables.Count > 0)
            {
                DataView summary = new DataView(dsPeriodDashboardDetails.Tables[0]);
                summary.Sort = Constants.ToSort;
                DataTable periodSummary = new DataTable();
                periodSummary = summary.ToTable();
                dashboardHTMLTables.Add(GetPeriodSummaryGrid(periodSummary));
                DataView periodPhase = new DataView(dsPeriodDashboardDetails.Tables[1]);
                periodPhase.Sort = Constants.ToSort;
                DataTable periodEffortPhase = new DataTable();
                periodEffortPhase = periodPhase.ToTable();
                dashboardHTMLTables.Add(GetPeriodEffortPhaseGrid(periodEffortPhase));
                DataView periodRole = new DataView(dsPeriodDashboardDetails.Tables[2]);
                periodRole.Sort = Constants.ToSort;
                DataTable periodEffortRole = new DataTable();
                periodEffortRole = periodRole.ToTable();
                dashboardHTMLTables.Add(GetPeriodEffortRoleGrid(periodEffortRole));
                DataView projectPhase = new DataView(dsPeriodDashboardDetails.Tables[3]);
                projectPhase.Sort = Constants.ToSort;
                DataTable projectEffortPhase = new DataTable();
                projectEffortPhase = projectPhase.ToTable();
                dashboardHTMLTables.Add(GetProjectEffortPhaseGrid(projectEffortPhase));
                DataView projectRole = new DataView(dsPeriodDashboardDetails.Tables[4]);
                projectRole.Sort = Constants.ToSort;
                DataTable projectEffortRole = new DataTable();
                projectEffortRole = projectRole.ToTable();
                dashboardHTMLTables.Add(GetProjectEffortRoleGrid(projectEffortRole));
                
            }

            return dashboardHTMLTables;
        }

        /// <summary>
        /// Gets the period summary grid.
        /// </summary>
        /// <returns></returns>
        public static string GetPeriodSummaryGrid(DataTable summaryTable)
        {
            var sbSummary = new StringBuilder();

            try
            {
                sbSummary.Append(Constants.HTML.GRID_START_TABLE);
                sbSummary.Append(Constants.HTML.TBODY_TR_HEADERROW);
                sbSummary.Append(Constants.HTML.TH_SICOL + "#" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_PERIDCOL + "Period" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_PROJECTCOL + "Project Name" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_PHASECOL + "Category" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_PHASECOL + "Current Phase" + Constants.HTML.TH_END);

                sbSummary.Append(Constants.HTML.TH_INNERTABLE_TBODY_TR_TD_STATUSCOLSPAN4 + "Status" + Constants.HTML.TH_END + Constants.HTML.TR_END);
                sbSummary.Append(Constants.HTML.TR_START_STATUS);
                sbSummary.Append(Constants.HTML.TH_CLASS_CLIENTCOL + "Client" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_TIMELINECOL + "Timeline" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_BUDGETCOL + "Budget" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_ESCALATECOL + "Escalate" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TR_END);
                sbSummary.Append(Constants.HTML.INNERTABLE_TABLE_TH_END);

                sbSummary.Append(Constants.HTML.TH_CLASS_DATECOL + "Delivery Date" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_DATECOL + "Revised Delivery Date" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_DATECOL + "Maitenance Closing Date" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_COMMENTSCOL + "PM Comments" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_COMMENTSCOL + "PO Comments" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_COMMENTSCOL + "Delivery Comments" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TH_CLASS_COMMENTSCOL + "Weekly Comments" + Constants.HTML.TH_END);
                sbSummary.Append(Constants.HTML.TR_END);

                if(summaryTable.Rows.Count == 0)
                {
                    sbSummary.Append(Constants.HTML.TR_START);
                    sbSummary.Append("<td style='text-align: center' colspan='12'>No Records Found!");
                    sbSummary.Append(Constants.HTML.TD_END);
                    sbSummary.Append(Constants.HTML.TR_END);
                }
                else
                {
                    int slNo = 0;

                    foreach (DataRow drRow in summaryTable.Rows)
                    {
                        slNo++;
                        
                        sbSummary.Append(slNo % 2 != 0 ? Constants.HTML.TR_START : Constants.HTML.TR_CLASS_ALT);
                        sbSummary.Append(Constants.HTML.TD_START + slNo + Constants.HTML.TD_END);
                        sbSummary.Append(Constants.HTML.TD_START + drRow[Constants.FieldName.ProjectDashboard.PERIOD] + Constants.HTML.TD_END);
                        sbSummary.Append(Constants.HTML.TD_START + "<a href='Project.aspx?ProjectEditId=" + drRow[Constants.FieldName.ProjectDashboard.PROJECTID] + "'>" + drRow[Constants.FieldName.ProjectDashboard.PROJECTNAME] + "</a>" + Constants.HTML.TD_END);
                        sbSummary.Append(Constants.HTML.TD_START + drRow[Constants.FieldName.ProjectDashboard.CATEGORY] + Constants.HTML.TD_END);
                        sbSummary.Append(Constants.HTML.TD_START + drRow[Constants.FieldName.ProjectDashboard.CURRENTPHASE] + Constants.HTML.TD_END);

                        if(Convert.ToInt32(drRow[Constants.FieldName.ProjectDashboard.CLIENTSTATUS]) == 3)
                            sbSummary.Append(Constants.HTML.TD_START_STATUS + Constants.HTML.IMG_CLIENTGREEN + Constants.HTML.TD_END);
                        else if (Convert.ToInt32(drRow[Constants.FieldName.ProjectDashboard.CLIENTSTATUS]) == 1)
                            sbSummary.Append(Constants.HTML.TD_START_STATUS + Constants.HTML.IMG_CLIENTRED + Constants.HTML.TD_END);
                        else if (Convert.ToInt32(drRow[Constants.FieldName.ProjectDashboard.CLIENTSTATUS]) == 2)
                            sbSummary.Append(Constants.HTML.TD_START_STATUS + Constants.HTML.IMG_CLIENTYELLOW + Constants.HTML.TD_END);

                        if (Convert.ToInt32(drRow[Constants.FieldName.ProjectDashboard.TIMELINESTATUS]) == 3)
                            sbSummary.Append(Constants.HTML.TD_START_STATUS + Constants.HTML.IMG_TIMELINEGREEN + Constants.HTML.TD_END);
                        else if (Convert.ToInt32(drRow[Constants.FieldName.ProjectDashboard.TIMELINESTATUS]) == 1)
                            sbSummary.Append(Constants.HTML.TD_START_STATUS + Constants.HTML.IMG_TIMELINERED + Constants.HTML.TD_END);
                        else if (Convert.ToInt32(drRow[Constants.FieldName.ProjectDashboard.TIMELINESTATUS]) == 2)
                            sbSummary.Append(Constants.HTML.TD_START_STATUS + Constants.HTML.IMG_TIMELINEYELLOW + Constants.HTML.TD_END);

                        if (Convert.ToInt32(drRow[Constants.FieldName.ProjectDashboard.BUDGETSTATUS]) == 3)
                            sbSummary.Append(Constants.HTML.TD_START_STATUS + Constants.HTML.IMG_BUDGETGREEN + Constants.HTML.TD_END);
                        else if (Convert.ToInt32(drRow[Constants.FieldName.ProjectDashboard.BUDGETSTATUS]) == 1)
                            sbSummary.Append(Constants.HTML.TD_START_STATUS + Constants.HTML.IMG_BUDGETRED + Constants.HTML.TD_END);
                        else if (Convert.ToInt32(drRow[Constants.FieldName.ProjectDashboard.BUDGETSTATUS]) == 2)
                            sbSummary.Append(Constants.HTML.TD_START_STATUS + Constants.HTML.IMG_BUDGETYELLOW + Constants.HTML.TD_END);

                        if (Convert.ToInt32(drRow[Constants.FieldName.ProjectDashboard.ESCALATESTATUS]) == 3)
                            sbSummary.Append(Constants.HTML.TD_START_STATUS + Constants.HTML.IMG_ESCALATEGREEN + Constants.HTML.TD_END);
                        else if (Convert.ToInt32(drRow[Constants.FieldName.ProjectDashboard.ESCALATESTATUS]) == 1)
                            sbSummary.Append(Constants.HTML.TD_START_STATUS + Constants.HTML.IMG_ESCALATERED + Constants.HTML.TD_END);
                        else if (Convert.ToInt32(drRow[Constants.FieldName.ProjectDashboard.ESCALATESTATUS]) == 2)
                            sbSummary.Append(Constants.HTML.TD_START_STATUS + Constants.HTML.IMG_ESCALATEYELLOW + Constants.HTML.TD_END);

                        sbSummary.Append(Constants.HTML.TD_START + drRow[Constants.FieldName.ProjectDashboard.DELIVERYDATE] + Constants.HTML.TD_END);
                        sbSummary.Append(Constants.HTML.TD_START + drRow[Constants.FieldName.ProjectDashboard.REVISEDDELIVERYDATE] + Constants.HTML.TD_END);
                        sbSummary.Append(Constants.HTML.TD_START + drRow[Constants.FieldName.ProjectDashboard.MAINTENANCECLOSINGDATE] + Constants.HTML.TD_END);
                    
                        sbSummary.Append(Constants.HTML.TD_START);

                        const string commentLink = "<a onclick=\"showPopup('{0}', event);\" title=\"{1}\" >{2}...</a>";

                        // Format PMComments
                        var pmComment = String.IsNullOrEmpty(drRow[Constants.FieldName.ProjectDashboard.PMCOMMENTS].ToString()) ? string.Empty : HttpUtility.HtmlEncode(drRow[Constants.FieldName.ProjectDashboard.PMCOMMENTS].ToString()).Replace(" ", "&nbsp;");
                        if (HttpUtility.HtmlDecode(pmComment).Length > 19)
                        {
                            var pmCommentsForPopup = HttpUtility.HtmlDecode(drRow[Constants.FieldName.ProjectDashboard.PMCOMMENTS].ToString()).Replace("\r\n", "<br/>").Replace("'", "&rsquo;").Replace("\"", "&quot;").Trim();
                            var pmCommentLink = String.Format(commentLink, pmCommentsForPopup,
                                                              HttpUtility.HtmlDecode(pmComment),
                                                              HttpUtility.HtmlDecode(pmComment).Substring(0, 20));
                            sbSummary.Append(pmCommentLink);
                        }
                        else
                        {
                            sbSummary.Append(HttpUtility.HtmlDecode(pmComment));
                        }

                        sbSummary.Append(Constants.HTML.TD_END);

                        sbSummary.Append(Constants.HTML.TD_START);
                    
                        // Format POComments
                        var poComment = String.IsNullOrEmpty(drRow[Constants.FieldName.ProjectDashboard.POCOMMENTS].ToString()) ? string.Empty : HttpUtility.HtmlEncode(drRow[Constants.FieldName.ProjectDashboard.POCOMMENTS].ToString()).Replace(" ", "&nbsp;");
                        if(HttpUtility.HtmlDecode(poComment).Length > 19)
                        {
                            var poCommentsForPopup = HttpUtility.HtmlDecode(drRow[Constants.FieldName.ProjectDashboard.POCOMMENTS].ToString()).Replace("\r\n", "<br/>").Replace("'", "&rsquo;").Replace("\"", "&quot;").Trim();
                            var poCommentLink = String.Format(commentLink, poCommentsForPopup,
                                                              HttpUtility.HtmlDecode(poComment),
                                                              HttpUtility.HtmlDecode(poComment).Substring(0, 20));
                            sbSummary.Append(poCommentLink);
                        }
                        else
                        {
                            sbSummary.Append(HttpUtility.HtmlDecode(poComment));
                        }

                        sbSummary.Append(Constants.HTML.TD_END);
                        
                        sbSummary.Append(Constants.HTML.TD_START);

                        // Format Delivery Comments
                        var deliveryComment = String.IsNullOrEmpty(drRow[Constants.FieldName.ProjectDashboard.DELIVERYCOMMENTS].ToString()) ? string.Empty : HttpUtility.HtmlEncode(drRow[Constants.FieldName.ProjectDashboard.DELIVERYCOMMENTS].ToString()).Replace(" ", "&nbsp;");
                        if (HttpUtility.HtmlDecode(deliveryComment).Length > 19)
                        {
                            var deliveryCommentsForPopup = HttpUtility.HtmlDecode(drRow[Constants.FieldName.ProjectDashboard.DELIVERYCOMMENTS].ToString()).Replace("\r\n", "<br/>").Replace("'", "&rsquo;").Replace("\"", "&quot;").Trim();
                            var deliveryCommentLink = String.Format(commentLink, deliveryCommentsForPopup,
                                                              HttpUtility.HtmlDecode(deliveryComment),
                                                              HttpUtility.HtmlDecode(deliveryComment).Substring(0, 20));
                            sbSummary.Append(deliveryCommentLink);
                        }
                        else
                        {
                            sbSummary.Append(HttpUtility.HtmlDecode(deliveryComment));
                        }

                        sbSummary.Append(Constants.HTML.TD_END);

                        sbSummary.Append(Constants.HTML.TD_START);
                        
                        // Format Weekly Comments
                        var weeklyComment = String.IsNullOrEmpty(drRow[Constants.FieldName.ProjectDashboard.WEEKLYCOMMENTS].ToString()) ? string.Empty : HttpUtility.HtmlEncode(drRow[Constants.FieldName.ProjectDashboard.WEEKLYCOMMENTS].ToString()).Replace(" ", "&nbsp;");
                        if (HttpUtility.HtmlDecode(weeklyComment).Length > 19)
                        {
                            var weeklyCommentsForPopup = HttpUtility.HtmlDecode(drRow[Constants.FieldName.ProjectDashboard.WEEKLYCOMMENTS].ToString()).Replace("\r\n", "<br/>").Replace("'", "&rsquo;").Replace("\"", "&quot;").Trim();
                            var weeklyCommentLink = String.Format(commentLink, weeklyCommentsForPopup,
                                                             HttpUtility.HtmlDecode(weeklyComment),
                                                             HttpUtility.HtmlDecode(weeklyComment).Substring(0, 20));
                            sbSummary.Append(weeklyCommentLink);
                        }
                        else
                        {
                            sbSummary.Append(HttpUtility.HtmlDecode(weeklyComment));
                        }
                    
                        sbSummary.Append(Constants.HTML.TR_END);
                    }
                }
                sbSummary.Append(Constants.HTML.GRID_END_TABLE);
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return sbSummary.ToString();
        }

        /// <summary>
        /// Gets the period effort phase grid.
        /// </summary>
        /// <param name="periodEffortPhaseTable"></param>
        /// <returns></returns>
        public static string GetPeriodEffortPhaseGrid(DataTable periodEffortPhaseTable)
        {
            var sbPeriodEffortPhase = new StringBuilder();

            try
            {
                DataTable dtPeriodProject = periodEffortPhaseTable.DefaultView.ToTable(true, "DashboardID", "Period", "ProjectName", "ProjectID");
                DataTable dtPeriodPhase = periodEffortPhaseTable.DefaultView.ToTable(true, "Phase");
                DataTable dtPeriodTotal = periodEffortPhaseTable.DefaultView.ToTable(true, "AllocatedHrsTotal", "ActualHrsTotal");

                sbPeriodEffortPhase.Append(Constants.HTML.GRID_START_TABLE);
                sbPeriodEffortPhase.Append(Constants.HTML.TBODY_TR_HEADERROW);
                sbPeriodEffortPhase.Append(Constants.HTML.TH_SICOL + "#" + Constants.HTML.TH_END);
                sbPeriodEffortPhase.Append(Constants.HTML.TH_CLASS_PERIDCOL + "Period" + Constants.HTML.TH_END);
                sbPeriodEffortPhase.Append(Constants.HTML.TH_CLASS_PROJECTCOL + "Project Name" + Constants.HTML.TH_END);
                sbPeriodEffortPhase.Append(Constants.HTML.TH_CLASS_REVISEDCOMMENTS + "Revised<br/>Comments" + Constants.HTML.TH_END);

                dtPeriodProject.Columns.Add("RevisedComments", typeof(string));
                foreach (DataRow drProjectRows in dtPeriodPhase.Rows)
                {
                    if (drProjectRows["Phase"].ToString() != "")
                    {
                        dtPeriodProject.Columns.Add(drProjectRows["Phase"].ToString() + "_Allocated", typeof(decimal));
                        dtPeriodProject.Columns.Add(drProjectRows["Phase"].ToString() + "_Actual", typeof(decimal));

                        sbPeriodEffortPhase.Append(Constants.HTML.TH_INNERTABLE_TBODY_TR_TD_COLSPAN2 +
                                                   drProjectRows["Phase"].ToString() + Constants.HTML.TD_END +
                                                   Constants.HTML.TR_END + Constants.HTML.INNERTABLE_TR_2COL +
                                                   Constants.HTML.INNERTABLE_TBODY_TABLE_TH_END);
                    }
                }
                dtPeriodProject.Columns.Add(dtPeriodTotal.Columns[0].ToString(), typeof(Double));
                dtPeriodProject.Columns.Add(dtPeriodTotal.Columns[1].ToString(), typeof(Double));

                sbPeriodEffortPhase.Append(Constants.HTML.TH_INNERTABLE_TBODY_TR_TD_COLSPAN2 + "Total" + Constants.HTML.TD_END +
                                                   Constants.HTML.TR_END + Constants.HTML.INNERTABLE_TR_2COL +
                                                   Constants.HTML.INNERTABLE_TBODY_TABLE_TH_END);
                sbPeriodEffortPhase.Append(Constants.HTML.TR_END);

                if (dtPeriodProject.Rows.Count == 0)
                {
                    sbPeriodEffortPhase.Append(Constants.HTML.TR_START);
                    sbPeriodEffortPhase.Append("<td style='text-align: center' colspan='5'>No Records Found!");
                    sbPeriodEffortPhase.Append(Constants.HTML.TD_END);
                    sbPeriodEffortPhase.Append(Constants.HTML.TR_END);
                }
                else
                {
                    int slNo = 0;
                    foreach (DataRow drRow in dtPeriodProject.Rows)
                    {
                        DataRow[] drResourceProjects = periodEffortPhaseTable.Select("ProjectID=" + drRow["ProjectID"] + " And DashboardID=" + drRow["DashboardID"]);
                        Double AllocatedTotal = 0.0;
                        Double ActualTotal = 0.0;
                        Double allocatedHrsVal = 0.0;
                        Double actualHrsVal = 0.0;

                        Double allocatedHrs = 0.0;
                        Double actualHrs = 0.0;

                        StringBuilder revComments = new StringBuilder();
                        foreach (DataRow drRowCells in drResourceProjects)
                        {
                             allocatedHrs = String.IsNullOrEmpty(drRowCells["AllocatedHrs"].ToString()) ? 0 : Convert.ToInt64(drRowCells["AllocatedHrs"]);
                             actualHrs = String.IsNullOrEmpty(drRowCells["ActualHrs"].ToString()) ? 0 : Convert.ToDouble(drRowCells["ActualHrs"]);

                           // allocatedHrsVal= allocatedHrsVal+ System.Convert.ToInt64(allocatedHrs);
                           // actualHrsVal = actualHrsVal + System.Convert.ToDouble(actualHrs);

                            drRow[drRowCells["Phase"].ToString() + "_Allocated"] = allocatedHrs;
                            drRow[drRowCells["Phase"].ToString() + "_Actual"] = actualHrs;

                            AllocatedTotal = AllocatedTotal + System.Convert.ToInt64(allocatedHrs);
                            ActualTotal = ActualTotal + System.Convert.ToDouble(actualHrs);

                            if (drRowCells["RevisedComments"] != DBNull.Value && !string.IsNullOrEmpty(drRowCells["RevisedComments"].ToString()))
                            {
                                revComments.AppendLine(drRowCells["RevisedComments"].ToString());
                                revComments.AppendLine();
                            }
                        }

                        const string commentLink = "<a onclick=\"showPopup('{0}', event);\">{1}...</a>";

                        // Format Rev.Comments

                        var revisedComment = HttpUtility.HtmlDecode(HttpUtility.HtmlEncode(revComments.ToString()).Replace(" ", "&nbsp;"));
                        var revisedCommentsForPopup = revisedComment.Replace("\r\n", "<br/>").Replace("'", "&rsquo;").Replace("\"", "&quot;").Trim();

                        if (revisedComment.Length > 9)
                        {
                            drRow["RevisedComments"] = (revisedComment == string.Empty) ? string.Empty : string.Format(commentLink, revisedCommentsForPopup, revisedComment.Substring(0, 10));
                        }
                        else
                        {
                            drRow["RevisedComments"] = revisedComment;
                        }

                        drRow["AllocatedHrsTotal"] = AllocatedTotal;
                        drRow["ActualHrsTotal"] = ActualTotal;
                    }

                    foreach (DataRow drRow in dtPeriodProject.Rows)
                    {
                        slNo++;

                        sbPeriodEffortPhase.Append(slNo % 2 != 0 ? Constants.HTML.TR_START : Constants.HTML.TR_CLASS_ALT);
                        sbPeriodEffortPhase.Append(Constants.HTML.TD_CLASS_SICOL + slNo + Constants.HTML.TD_END);
                        sbPeriodEffortPhase.Append(Constants.HTML.TD_CLASS_PERIDCOL + drRow["Period"].ToString() + Constants.HTML.TD_END);
                        sbPeriodEffortPhase.Append(Constants.HTML.TD_CLASS_PROJECTCOL + "<a href='Project.aspx?ProjectEditId=" + drRow["ProjectID"] + "'>" + drRow["ProjectName"].ToString() + Constants.HTML.TD_END);
                        sbPeriodEffortPhase.Append(Constants.HTML.TD_CLASS_REVISEDCOMMENTS + drRow["RevisedComments"].ToString() + Constants.HTML.TD_END);

                        for (int i = 5; i < drRow.ItemArray.Length; i++)
                        {
                            var val = String.IsNullOrEmpty(drRow.ItemArray[i].ToString())
                                            ? 0
                                            : Convert.ToDouble(drRow[i].ToString());
                            sbPeriodEffortPhase.Append(Constants.HTML.TD_CLASS_NUMBER + val + Constants.HTML.TD_END);
                        }

                        sbPeriodEffortPhase.Append(Constants.HTML.TR_END);

                    }

                    sbPeriodEffortPhase.Append("<tr style='color:White;background-color:#539cd1;font-weight:bold;'>");
                    sbPeriodEffortPhase.Append(Constants.HTML.TD_CLASS_SICOL + Constants.HTML.TD_END);
                    sbPeriodEffortPhase.Append(Constants.HTML.TD_CLASS_PERIDCOL + Constants.HTML.TD_END);
                    sbPeriodEffortPhase.Append(Constants.HTML.TD_CLASS_PROJECTCOL + "Total" + Constants.HTML.TD_END);
                    sbPeriodEffortPhase.Append(Constants.HTML.TD_CLASS_REVISEDCOMMENTS + Constants.HTML.TD_END);
                    for (int i = 5; i < dtPeriodProject.Columns.Count; i++)
                    {
                        sbPeriodEffortPhase.Append(Constants.HTML.TD_CLASS_NUMBER + dtPeriodProject.Compute("sum(["+ dtPeriodProject.Columns[i].ColumnName +"])", null) + Constants.HTML.TD_END);
                    }
                    sbPeriodEffortPhase.Append(Constants.HTML.TR_END);
                }

                sbPeriodEffortPhase.Append(Constants.HTML.GRID_END_TABLE);
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return sbPeriodEffortPhase.ToString();
        }

        /// <summary>
        /// Gets the period effort role grid.
        /// </summary>
        /// <param name="periodEffortRoleTable"></param>
        /// <returns></returns>
        public static string GetPeriodEffortRoleGrid(DataTable periodEffortRoleTable)
        {
            var sbPeriodEffortRole = new StringBuilder();

            try
            {
                DataTable dtPeriodProject = periodEffortRoleTable.DefaultView.ToTable(true, "DashboardID", "Period","ProjectName", "ProjectID");
                DataTable dtPeriodRole = periodEffortRoleTable.DefaultView.ToTable(true, "RoleName");
                DataTable dtPeriodTotal = periodEffortRoleTable.DefaultView.ToTable(true, "AllocatedHrsTotal", "ActualHrsTotal");

                sbPeriodEffortRole.Append(Constants.HTML.GRID_START_TABLE);
                sbPeriodEffortRole.Append(Constants.HTML.TBODY_TR_HEADERROW);
                sbPeriodEffortRole.Append(Constants.HTML.TH_SICOL + "#" + Constants.HTML.TH_END);
                sbPeriodEffortRole.Append(Constants.HTML.TH_CLASS_PERIDCOL + "Period" + Constants.HTML.TH_END);
                sbPeriodEffortRole.Append(Constants.HTML.TH_CLASS_PROJECTCOL + "Project Name" + Constants.HTML.TH_END);
                sbPeriodEffortRole.Append(Constants.HTML.TH_CLASS_REVISEDCOMMENTS + "Revised<br/>Comments" + Constants.HTML.TH_END);

                dtPeriodProject.Columns.Add("RevisedComments", typeof(string));
                foreach (DataRow drProjectRows in dtPeriodRole.Rows)
                {
                    if (drProjectRows["RoleName"].ToString() != "")
                    {
                        dtPeriodProject.Columns.Add(drProjectRows["RoleName"].ToString() + "_Allocated", typeof(decimal));
                        dtPeriodProject.Columns.Add(drProjectRows["RoleName"].ToString() + "_Actual", typeof(decimal));

                        sbPeriodEffortRole.Append(Constants.HTML.TH_INNERTABLE_TBODY_TR_TD_COLSPAN2 +
                                                   drProjectRows["RoleName"].ToString() + Constants.HTML.TD_END +
                                                   Constants.HTML.TR_END + Constants.HTML.INNERTABLE_TR_2COL +
                                                   Constants.HTML.INNERTABLE_TBODY_TABLE_TH_END);
                    }
                }
                dtPeriodProject.Columns.Add(dtPeriodTotal.Columns[0].ToString(), typeof(Double));
                dtPeriodProject.Columns.Add(dtPeriodTotal.Columns[1].ToString(), typeof(Double));

                sbPeriodEffortRole.Append(Constants.HTML.TH_INNERTABLE_TBODY_TR_TD_COLSPAN2 + "Total" + Constants.HTML.TD_END +
                                                   Constants.HTML.TR_END + Constants.HTML.INNERTABLE_TR_2COL +
                                                   Constants.HTML.INNERTABLE_TBODY_TABLE_TH_END);
                sbPeriodEffortRole.Append(Constants.HTML.TR_END);
                
                if (dtPeriodProject.Rows.Count == 0)
                {
                    sbPeriodEffortRole.Append(Constants.HTML.TR_START);
                    sbPeriodEffortRole.Append("<td style='text-align: center' colspan='5'>No Records Found!");
                    sbPeriodEffortRole.Append(Constants.HTML.TD_END);
                    sbPeriodEffortRole.Append(Constants.HTML.TR_END);
                }
                else
                {
                    int slNo = 0;
                    foreach (DataRow drRow in dtPeriodProject.Rows)
                    {
                        DataRow[] drResourceProjects = periodEffortRoleTable.Select("ProjectID=" + drRow["ProjectID"] + " And DashboardID=" + drRow["DashboardID"]); 
                        Double AllocatedTotal = 0.0;
                        Double ActualTotal = 0.0;

                        StringBuilder revComments = new StringBuilder();

                        foreach (DataRow drRowCells in drResourceProjects)
                        {
                            var allocatedHrs = String.IsNullOrEmpty(drRowCells["AllocatedHrs"].ToString()) ? 0 : Convert.ToInt64(drRowCells["AllocatedHrs"]);
                            var actualHrs = String.IsNullOrEmpty(drRowCells["ActualHrs"].ToString()) ? 0 : Convert.ToDecimal(drRowCells["ActualHrs"]);

                            drRow[drRowCells["RoleName"].ToString() + "_Allocated"] = allocatedHrs;
                            drRow[drRowCells["RoleName"].ToString() + "_Actual"] = actualHrs;

                            AllocatedTotal = AllocatedTotal + System.Convert.ToInt64(allocatedHrs);
                            ActualTotal = ActualTotal + System.Convert.ToDouble(actualHrs);

                            if (drRowCells["RevisedComments"] != DBNull.Value && !string.IsNullOrEmpty(drRowCells["RevisedComments"].ToString()))
                            {
                                revComments.AppendLine(drRowCells["RevisedComments"].ToString());
                                revComments.AppendLine();
                            }
                        }

                        const string commentLink = "<a onclick=\"showPopup('{0}', event);\">{1}...</a>";

                        // Format Rev.Comments

                        var revisedComment = HttpUtility.HtmlDecode(HttpUtility.HtmlEncode(revComments.ToString()).Replace(" ", "&nbsp;"));
                        var revisedCommentsForPopup = revisedComment.Replace("\r\n", "<br/>").Replace("'", "&rsquo;").Replace("\"", "&quot;").Trim();

                        if (revisedComment.Length > 9)
                        {
                            drRow["RevisedComments"] = (revisedComment == string.Empty) ? string.Empty : string.Format(commentLink, revisedCommentsForPopup, revisedComment.Substring(0, 10));
                        }
                        else
                        {
                            drRow["RevisedComments"] = revisedComment;
                        }
                        
                        drRow["AllocatedHrsTotal"] = AllocatedTotal;
                        drRow["ActualHrsTotal"] = ActualTotal;
                    }

                    foreach (DataRow drRow in dtPeriodProject.Rows)
                    {
                        slNo++;

                        sbPeriodEffortRole.Append(slNo % 2 != 0 ? Constants.HTML.TR_START : Constants.HTML.TR_CLASS_ALT);
                        sbPeriodEffortRole.Append(Constants.HTML.TD_START + slNo + Constants.HTML.TD_END);
                        sbPeriodEffortRole.Append(Constants.HTML.TD_START + drRow["Period"].ToString() +
                                                    Constants.HTML.TD_END);
                        sbPeriodEffortRole.Append(Constants.HTML.TD_CLASS_PROJECTCOL + "<a href='Project.aspx?ProjectEditId=" + drRow["ProjectID"] + "'>" + drRow["ProjectName"].ToString() + Constants.HTML.TD_END);
                        sbPeriodEffortRole.Append(Constants.HTML.TD_CLASS_REVISEDCOMMENTS + drRow["RevisedComments"].ToString() + Constants.HTML.TD_END);

                        for (int i = 5; i < drRow.ItemArray.Length; i++)
                        {
                            var val = String.IsNullOrEmpty(drRow.ItemArray[i].ToString())? 0 : Convert.ToDouble(drRow[i].ToString());
                            sbPeriodEffortRole.Append(Constants.HTML.TD_CLASS_NUMBER + val + Constants.HTML.TD_END);
                        }

                        sbPeriodEffortRole.Append(Constants.HTML.TR_END);

                    }

                    sbPeriodEffortRole.Append("<tr style='color:White;background-color:#539cd1;font-weight:bold;'>");
                    sbPeriodEffortRole.Append(Constants.HTML.TD_START + Constants.HTML.TD_END);
                    sbPeriodEffortRole.Append(Constants.HTML.TD_START + Constants.HTML.TD_END);
                    sbPeriodEffortRole.Append(Constants.HTML.TD_CLASS_PROJECTCOL + "Total" + Constants.HTML.TD_END);
                    sbPeriodEffortRole.Append(Constants.HTML.TD_CLASS_REVISEDCOMMENTS + Constants.HTML.TD_END);
                    for (int i = 5; i < dtPeriodProject.Columns.Count; i++)
                    {
                        sbPeriodEffortRole.Append(Constants.HTML.TD_CLASS_NUMBER + dtPeriodProject.Compute("sum([" + dtPeriodProject.Columns[i].ColumnName + "])", null) + Constants.HTML.TD_END);
                    }
                    sbPeriodEffortRole.Append(Constants.HTML.TR_END);
                }


                sbPeriodEffortRole.Append(Constants.HTML.GRID_END_TABLE);

            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return sbPeriodEffortRole.ToString();
        }

        /// <summary>
        /// Gets the project effort phase grid.
        /// </summary>
        /// <returns></returns>
        public static string GetProjectEffortPhaseGrid(DataTable projectEffortPhaseTable)
        {
            var sbProjectEffortPhase = new StringBuilder();

            try
            {
                DataTable dtProject = projectEffortPhaseTable.DefaultView.ToTable(true, "DashboardID", "Period", "ProjectName", "ProjectID");
                DataTable dtProjectPhase = projectEffortPhaseTable.DefaultView.ToTable(true, "Phase");
                //DataTable dtProjectTotal = projectEffortPhaseTable.DefaultView.ToTable(true, "BillableTotal", "BudgetTotal", "RevBudgTotal", "ActualHrsTotal");

                sbProjectEffortPhase.Append(Constants.HTML.GRID_START_TABLE);
                sbProjectEffortPhase.Append(Constants.HTML.TBODY_TR_HEADERROW);
                sbProjectEffortPhase.Append(Constants.HTML.TH_SICOL + "#" + Constants.HTML.TH_END);
                sbProjectEffortPhase.Append(Constants.HTML.TH_CLASS_PERIDCOL + "Period" + Constants.HTML.TH_END);
                sbProjectEffortPhase.Append(Constants.HTML.TH_CLASS_PROJECTCOL + "Project Name" + Constants.HTML.TH_END);

                foreach (DataRow drProject in dtProjectPhase.Rows)
                {
                    if (drProject["Phase"].ToString() != "")
                    {
                        dtProject.Columns.Add(drProject["Phase"].ToString() + "_Billable", typeof(string));
                        dtProject.Columns.Add(drProject["Phase"].ToString() + "_Budgeted", typeof(string));
                        dtProject.Columns.Add(drProject["Phase"].ToString() + "_Rev.Budgeted", typeof(string));
                        dtProject.Columns.Add(drProject["Phase"].ToString() + "_Actual", typeof(string));

                        sbProjectEffortPhase.Append(Constants.HTML.TH_INNERTABLE_TBODY_TR_TD_COLSPAN4 +
                                                   drProject["Phase"].ToString() + Constants.HTML.TD_END +
                                                   Constants.HTML.TR_END + Constants.HTML.INNERTABLE_TR_4COL +
                                                   Constants.HTML.INNERTABLE_TBODY_TABLE_TH_END);
                    }
                }

                //dtProject.Columns.Add(dtProjectTotal.Columns[0].ToString(), typeof(Double));
                //dtProject.Columns.Add(dtProjectTotal.Columns[1].ToString(), typeof(Double));
                //dtProject.Columns.Add(dtProjectTotal.Columns[2].ToString(), typeof(Double));
                //dtProject.Columns.Add(dtProjectTotal.Columns[3].ToString(), typeof(Double));
                dtProject.Columns.Add(Constants.DashboardTabs.PeriodDashboard.Heading.BILLABLETOTAL, typeof(Double));
                dtProject.Columns.Add(Constants.DashboardTabs.PeriodDashboard.Heading.BUDGETTOTAL, typeof(Double));
                dtProject.Columns.Add(Constants.DashboardTabs.PeriodDashboard.Heading.REVBUDGETTOTAL, typeof(Double));
                dtProject.Columns.Add(Constants.DashboardTabs.PeriodDashboard.Heading.ACTUALHOURSTOTAL, typeof(Double));

                sbProjectEffortPhase.Append(Constants.HTML.TH_INNERTABLE_TBODY_TR_TD_COLSPAN4 + "Total" + Constants.HTML.TD_END +
                                                   Constants.HTML.TR_END + Constants.HTML.INNERTABLE_TR_4COL +
                                                   Constants.HTML.INNERTABLE_TBODY_TABLE_TH_END);
                sbProjectEffortPhase.Append(Constants.HTML.TR_END);

                if (dtProject.Rows.Count == 0)
                {
                    sbProjectEffortPhase.Append(Constants.HTML.TR_START);
                    sbProjectEffortPhase.Append("<td style='text-align: center' colspan='7'>No Records Found!");
                    sbProjectEffortPhase.Append(Constants.HTML.TD_END);
                    sbProjectEffortPhase.Append(Constants.HTML.TR_END);
                }
                else
                {
                    int slNo = 0;
                    foreach (DataRow drRow in dtProject.Rows)
                    {
                        DataRow[] drResourceProjects = projectEffortPhaseTable.Select("ProjectID=" + drRow["ProjectID"] + " And DashboardID=" + drRow["DashboardID"]); 
                        Double billableTotal = 0.0;
                        Double budgetedTotal = 0.0;
                        Double revBudgetedTotal = 0.0;
                        Double actualTotal = 0.0;

                        foreach (DataRow drRowCells in drResourceProjects)
                        {
                            var billableHrs = String.IsNullOrEmpty(drRowCells["BillableHours"].ToString()) ? 0 : Convert.ToInt64(drRowCells["BillableHours"]);
                            var budgetedHrs = String.IsNullOrEmpty(drRowCells["BudgetHours"].ToString()) ? 0 : Convert.ToInt64(drRowCells["BudgetHours"]);
                            var revBudgetedHrs = String.IsNullOrEmpty(drRowCells["RevisedBudgetHours"].ToString()) ? 0 : Convert.ToInt64(drRowCells["RevisedBudgetHours"]);
                            var actualHrs = String.IsNullOrEmpty(drRowCells["ActualHours"].ToString()) ? 0 : Convert.ToDecimal(drRowCells["ActualHours"]);

                            drRow[drRowCells["Phase"].ToString() + "_Billable"] = billableHrs;
                            drRow[drRowCells["Phase"].ToString() + "_Budgeted"] = budgetedHrs;

                            drRow[drRowCells["Phase"].ToString() + "_Rev.Budgeted"] = revBudgetedHrs;
                            drRow[drRowCells["Phase"].ToString() + "_Actual"] = actualHrs;

                            billableTotal = billableTotal + Convert.ToInt64(billableHrs);
                            budgetedTotal = budgetedTotal + Convert.ToInt64(budgetedHrs);
                            revBudgetedTotal = revBudgetedTotal + Convert.ToInt64(revBudgetedHrs);
                            actualTotal = actualTotal + Convert.ToDouble(actualHrs);

                        }

                        drRow[Constants.DashboardTabs.PeriodDashboard.Heading.BILLABLETOTAL] = billableTotal;
                        drRow[Constants.DashboardTabs.PeriodDashboard.Heading.BUDGETTOTAL] = budgetedTotal;
                        drRow[Constants.DashboardTabs.PeriodDashboard.Heading.REVBUDGETTOTAL] = revBudgetedTotal;
                        drRow[Constants.DashboardTabs.PeriodDashboard.Heading.ACTUALHOURSTOTAL] = actualTotal;

                        const string commentLink = "<a onclick=\"showPopup('{0}', event);\" >{1}</a>";

                        // Format Revised Comments
                        for (int i = 6; i < (dtProject.Columns.Count - 4); i += 4)
                        {
                            drResourceProjects = projectEffortPhaseTable.Select("Phase='" + dtProject.Columns[i].ColumnName.Replace("_Rev.Budgeted", "") + "' And DashboardID=" + drRow["DashboardID"]);

                            var revisedComment = string.Empty;
                            //foreach (DataRow drRowCells in drResourceProjects)
                            //{                            
                            //    revisedComment += (drRowCells["RevisedComments"] == DBNull.Value || string.IsNullOrEmpty(drRowCells["RevisedComments"].ToString())) ? string.Empty : (HttpUtility.HtmlEncode(drRowCells["RevisedComments"].ToString()).Replace(" ", "&nbsp;") + "<br /><br />");
                            //}
                            revisedComment = HttpUtility.HtmlDecode(revisedComment);
                            var revisedCommentsForPopup = revisedComment.Replace("\r\n", "<br/>").Replace("'", "&rsquo;").Replace("\"", "&quot;").Trim();
                            var revisedCommentLink = (revisedComment == string.Empty) ? drRow[dtProject.Columns[i].ColumnName].ToString() : string.Format(commentLink, revisedCommentsForPopup, drRow[dtProject.Columns[i].ColumnName].ToString());
                            drRow[dtProject.Columns[i].ColumnName] = revisedCommentLink;
                        }
                    }

                    foreach (DataRow drRow in dtProject.Rows)
                    {
                        slNo++;

                        sbProjectEffortPhase.Append(slNo % 2 != 0 ? Constants.HTML.TR_START : Constants.HTML.TR_CLASS_ALT);
                        sbProjectEffortPhase.Append(Constants.HTML.TD_START + slNo + Constants.HTML.TD_END);
                        sbProjectEffortPhase.Append(Constants.HTML.TD_START + drRow["Period"].ToString() +Constants.HTML.TD_END);
                        sbProjectEffortPhase.Append(Constants.HTML.TD_CLASS_PROJECTCOL + "<a href='Project.aspx?ProjectEditId=" + drRow["ProjectID"] + "'>" + drRow["ProjectName"].ToString() + Constants.HTML.TD_END);

                        for (int i = 4; i < drRow.ItemArray.Length; i++)
                        {
                            var val = (drRow.ItemArray[i] == DBNull.Value || drRow.ItemArray[i].ToString() == string.Empty) ? "0" : drRow[i].ToString();
                            sbProjectEffortPhase.Append(Constants.HTML.TD_CLASS_NUMBER4 + val + Constants.HTML.TD_END);
                        }

                        sbProjectEffortPhase.Append(Constants.HTML.TR_END);
                    }
                }

                sbProjectEffortPhase.Append(Constants.HTML.GRID_END_TABLE);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return sbProjectEffortPhase.ToString();
        }

        /// <summary>
        /// Gets the project effort role grid.
        /// </summary>
        /// <param name="projectEffortRoleTable"></param>
        /// <returns></returns>
        public static string GetProjectEffortRoleGrid(DataTable projectEffortRoleTable)
        {
            var sbProjectEffortRole = new StringBuilder();

            try
            {
                DataTable dtProject = projectEffortRoleTable.DefaultView.ToTable(true, "DashboardID", "Period", "ProjectName", "ProjectID");
                DataTable dtProjectRole = projectEffortRoleTable.DefaultView.ToTable(true, "RoleName");
                //DataTable dtProjectTotal = projectEffortRoleTable.DefaultView.ToTable(true, "BillableTotal", "BudgetTotal", "RevBudgTotal", "ActualHrsTotal");

                sbProjectEffortRole.Append(Constants.HTML.GRID_START_TABLE);
                sbProjectEffortRole.Append(Constants.HTML.TBODY_TR_HEADERROW);
                sbProjectEffortRole.Append(Constants.HTML.TH_SICOL + "#" + Constants.HTML.TH_END);
                sbProjectEffortRole.Append(Constants.HTML.TH_CLASS_PERIDCOL_20PERC + "Period" + Constants.HTML.TH_END);
                sbProjectEffortRole.Append(Constants.HTML.TH_CLASS_PROJECTCOL + "Project Name" + Constants.HTML.TH_END);

                foreach (DataRow drProject in dtProjectRole.Rows)
                {
                    if (drProject["RoleName"].ToString() != "")
                    {
                        dtProject.Columns.Add(drProject["RoleName"].ToString() + "_Billable", typeof(string));
                        dtProject.Columns.Add(drProject["RoleName"].ToString() + "_Budgeted", typeof(string));
                        dtProject.Columns.Add(drProject["RoleName"].ToString() + "_Rev.Budgeted", typeof(string));
                        dtProject.Columns.Add(drProject["RoleName"].ToString() + "_Actual", typeof(string));

                        sbProjectEffortRole.Append(Constants.HTML.TH_INNERTABLE_TBODY_TR_TD_COLSPAN4 +
                                                   drProject["RoleName"].ToString() + Constants.HTML.TD_END +
                                                   Constants.HTML.TR_END + Constants.HTML.INNERTABLE_TR_4COL +
                                                   Constants.HTML.INNERTABLE_TBODY_TABLE_TH_END);
                    }
                }

                //dtProject.Columns.Add(dtProjectTotal.Columns[0].ToString(), typeof(Double));
                //dtProject.Columns.Add(dtProjectTotal.Columns[1].ToString(), typeof(Double));
                //dtProject.Columns.Add(dtProjectTotal.Columns[2].ToString(), typeof(Double));
                //dtProject.Columns.Add(dtProjectTotal.Columns[3].ToString(), typeof(Double));

                dtProject.Columns.Add(Constants.DashboardTabs.PeriodDashboard.Heading.BILLABLETOTAL, typeof(Double));
                dtProject.Columns.Add(Constants.DashboardTabs.PeriodDashboard.Heading.BUDGETTOTAL, typeof(Double));
                dtProject.Columns.Add(Constants.DashboardTabs.PeriodDashboard.Heading.REVBUDGETTOTAL, typeof(Double));
                dtProject.Columns.Add(Constants.DashboardTabs.PeriodDashboard.Heading.ACTUALHOURSTOTAL, typeof(Double));

                sbProjectEffortRole.Append(Constants.HTML.TH_INNERTABLE_TBODY_TR_TD_COLSPAN4 + "Total" + Constants.HTML.TD_END +
                                                   Constants.HTML.TR_END + Constants.HTML.INNERTABLE_TR_4COL +
                                                   Constants.HTML.INNERTABLE_TBODY_TABLE_TH_END);
                sbProjectEffortRole.Append(Constants.HTML.TR_END);

                if (dtProject.Rows.Count == 0)
                {
                    sbProjectEffortRole.Append(Constants.HTML.TR_START);
                    sbProjectEffortRole.Append("<td style='text-align: center' colspan='7'>No Records Found!");
                    sbProjectEffortRole.Append(Constants.HTML.TD_END);
                    sbProjectEffortRole.Append(Constants.HTML.TR_END);
                }
                else
                {
                    int slNo = 0;
                    foreach (DataRow drRow in dtProject.Rows)
                    {
                        DataRow[] drResourceProjects = projectEffortRoleTable.Select("ProjectID=" + drRow["ProjectID"] + " And DashboardID=" + drRow["DashboardID"]); 
                        Double billableTotal = 0.0;
                        Double budgetedTotal = 0.0;
                        Double revBudgetedTotal = 0.0;
                        Double actualTotal = 0.0;

                        foreach (DataRow drRowCells in drResourceProjects)
                        {
                            var billableHrs = String.IsNullOrEmpty(drRowCells["BillableHours"].ToString()) ? 0 : Convert.ToInt64(drRowCells["BillableHours"]);
                            var budgetedHrs = String.IsNullOrEmpty(drRowCells["BudgetHours"].ToString()) ? 0 : Convert.ToInt64(drRowCells["BudgetHours"]);
                            var revBudgetedHrs = String.IsNullOrEmpty(drRowCells["RevisedBudgetHours"].ToString()) ? 0 : Convert.ToInt64(drRowCells["RevisedBudgetHours"]);
                            var actualHrs = String.IsNullOrEmpty(drRowCells["ActualHours"].ToString()) ? 0 : Convert.ToDecimal(drRowCells["ActualHours"]);

                            drRow[drRowCells["RoleName"].ToString() + "_Billable"] = billableHrs;
                            drRow[drRowCells["RoleName"].ToString() + "_Budgeted"] = budgetedHrs;
                            
                            drRow[drRowCells["RoleName"].ToString() + "_Rev.Budgeted"] = revBudgetedHrs;
                            drRow[drRowCells["RoleName"].ToString() + "_Actual"] = actualHrs;

                            billableTotal = billableTotal + Convert.ToInt64(billableHrs);
                            budgetedTotal = budgetedTotal + Convert.ToInt64(budgetedHrs);
                            revBudgetedTotal = revBudgetedTotal + Convert.ToInt64(revBudgetedHrs);
                            actualTotal = actualTotal + Convert.ToDouble(actualHrs);
                        }

                        drRow[Constants.DashboardTabs.PeriodDashboard.Heading.BILLABLETOTAL] = billableTotal;
                        drRow[Constants.DashboardTabs.PeriodDashboard.Heading.BUDGETTOTAL] = budgetedTotal;
                        drRow[Constants.DashboardTabs.PeriodDashboard.Heading.REVBUDGETTOTAL] = revBudgetedTotal;
                        drRow[Constants.DashboardTabs.PeriodDashboard.Heading.ACTUALHOURSTOTAL] = actualTotal;

                        const string commentLink = "<a onclick=\"showPopup('{0}', event);\" >{1}</a>";

                        // Format Revised Comments
                        for (int i = 6; i < (dtProject.Columns.Count - 4); i += 4)
                        {
                            drResourceProjects = projectEffortRoleTable.Select("RoleName='" + dtProject.Columns[i].ColumnName.Replace("_Rev.Budgeted", "") + "' And DashboardID=" + drRow["DashboardID"]);

                            var revisedComment = string.Empty;
                            foreach (DataRow drRowCells in drResourceProjects)
                            {
                                revisedComment += (drRowCells["RevisedComments"] == DBNull.Value || string.IsNullOrEmpty(drRowCells["RevisedComments"].ToString())) ? string.Empty : (HttpUtility.HtmlEncode(drRowCells["RevisedComments"].ToString()).Replace(" ", "&nbsp;") + "<br /><br />");
                            }
                            revisedComment = HttpUtility.HtmlDecode(revisedComment);
                            var revisedCommentsForPopup = revisedComment.Replace("\r\n", "<br/>").Replace("'", "&rsquo;").Replace("\"", "&quot;").Trim();
                            var revisedCommentLink = (revisedComment == string.Empty) ? drRow[dtProject.Columns[i].ColumnName].ToString() : string.Format(commentLink, revisedCommentsForPopup, drRow[dtProject.Columns[i].ColumnName].ToString());
                            drRow[dtProject.Columns[i].ColumnName] = revisedCommentLink;
                        }
                    }

                    foreach (DataRow drRow in dtProject.Rows)
                    {
                        slNo++;

                        sbProjectEffortRole.Append(slNo % 2 != 0 ? Constants.HTML.TR_START : Constants.HTML.TR_CLASS_ALT);
                        sbProjectEffortRole.Append(Constants.HTML.TD_START + slNo + Constants.HTML.TD_END);
                        sbProjectEffortRole.Append(Constants.HTML.TD_START + drRow["Period"].ToString() + Constants.HTML.TD_END);
                        sbProjectEffortRole.Append(Constants.HTML.TD_CLASS_PROJECTCOL + "<a href='Project.aspx?ProjectEditId=" + drRow["ProjectID"] + "'>" + drRow["ProjectName"].ToString() + Constants.HTML.TD_END);

                        for (int i = 4; i < drRow.ItemArray.Length; i++)
                        {
                            var val = String.IsNullOrEmpty(drRow.ItemArray[i].ToString()) ? "0" : drRow[i].ToString();
                            sbProjectEffortRole.Append(Constants.HTML.TD_CLASS_NUMBER4 + val + Constants.HTML.TD_END);
                        }

                        sbProjectEffortRole.Append(Constants.HTML.TR_END);
                    }
                }

                sbProjectEffortRole.Append(Constants.HTML.GRID_END_TABLE);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return sbProjectEffortRole.ToString();
        }
        #endregion


        /// <summary>
        /// Gets the project details by id.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        [WebMethod]
        public static Model.Project GetProjectById(int projectId)
        {
            return ProjectDAL.GetProjectDetailsById(projectId);
        }
        #endregion

        /// <summary>
        /// Method to fill the DashBoard week dropDownList.
        /// </summary>
        /// <param name="dashboardID"></param>
        private void FillDashBoardWeek(long dashboardID)
        {
            if (Session["HdnChkIncludeProposalVAS"] != null)
            {
                if (Session["HdnChkIncludeProposalVAS"].ToString() == "True")
                    cbProposalAndVas.Checked = true;
                else
                    cbProposalAndVas.Checked = false;
            }
            ddlReports.Items.Clear(); // Method to clear the DashBoard week Details...
            DataTable dtDashBoard = new DataTable();

            if (ddlProjectPeriod.SelectedItem.Text == "Weekly")
                dtDashBoard = PMOscar.BaseDAL.ExecuteDataTable("Select DashboardID,Name,FromDate,ToDate from dbo.Dashboard where [PeriodType]='W' order by FromDate DESC");
            else
                dtDashBoard = PMOscar.BaseDAL.ExecuteDataTable("Select DashboardID,Name,FromDate,ToDate from dbo.Dashboard where [PeriodType]='M' order by FromDate DESC");

            if (dtDashBoard.Rows.Count > 0)
            {
                ddlReports.DataTextField = "Name";
                ddlReports.DataValueField = "DashboardID";
                ddlReports.DataSource = dtDashBoard;
                ddlReports.DataBind();

                if (dashboardID != 0)
                {
                    ddlReports.SelectedValue = dashboardID.ToString();
                    DataTable dtDashBoardFinalize = PMOscar.BaseDAL.ExecuteDataTable("Select Status from Dashboard where DashboardID=" + ddlReports.SelectedItem.Value);
                    if (dtDashBoardFinalize.Rows.Count > 0)
                    {
                        if (dtDashBoardFinalize.Rows[0]["Status"].ToString() == "F")
                        {
                            btnRefresh.Enabled = false;
                            btnFinalize.Enabled = false;
                            //btnRefreshDropDownList.Enabled = false;
                        }
                        else
                        {
                            btnRefresh.Enabled = true;
                            btnFinalize.Enabled = true;
                            //btnRefreshDropDownList.Enabled = true;
                        }
                    }
                    DataRow[] dr = dtDashBoard.Select("DashboardID=" + dashboardID);
                    foreach (DataRow drrow in dr)
                    {
                        dayFrom = drrow["FromDate"].ToString();
                        dayTo = drrow["ToDate"].ToString();
                    }
                }
                dtDashBoard.Dispose();
            }
            else
                BindProjectDashBoardDetailsGrid(); // Method to Bind ProjectDashBoard Details...

            ddlReports.Items.Insert(0, "Select");
        }

        private void BindProjectDashBoardDetailsGrid()
        {
            dtReview = new DataTable();
            dtProject = new DataTable();
            dtPeriod = new DataTable();   
         
            dgOverView.DataSource = dtReview;
            dgOverView.DataBind();  // Binding Overview Tab...

            dgProjectEffortPhase.DataSource = dtPeriod;
            dgProjectEffortPhase.DataBind();  // Binding Project Effort Phase Tab...

            dgProjectEffortRole.DataSource = dtProject;
            dgProjectEffortRole.DataBind(); // Binding Project Effort Role Tab...

            dgPeriodEffortPhase.DataSource = dtPeriod;
            dgPeriodEffortPhase.DataBind(); // Binding Period Effort Phase Tab...

            dgPeriodEffortRole.DataSource = dtPeriod;
            dgPeriodEffortRole.DataBind(); // Binding Period Effort Role Tab...

            lblHeading.Text = string.Empty;          
        }

        // Method to develop project dashbaord details...
        private void DevelopingProjectDashBoardDetails()
        { 
            string uptoDate = dayFrom;       

            try
            {
                dayFrom = Convert.ToDateTime(dayFrom).ToString("yyyy/MM/dd");
                dayTo = Convert.ToDateTime(dayTo).ToString("yyyy/MM/dd");
            }
            catch
            {
            }

            BindProjectDashBoardDetailsGrid();   // Method to Bind ProjectDashBoard Details...                
                                  
            dtReview = new DataTable();
            List<SqlParameter> parameter = new List<SqlParameter>();    
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", uptoDate));
            parameter.Add(new SqlParameter("@tabType", "Preview"));
            parameter.Add(new SqlParameter("@Status", "Go"));
            parameter.Add(new SqlParameter("@DashboardID", ddlReports.SelectedItem.Value));
            parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", cbProposalAndVas.Checked));
            dtReview = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];
            Session["grdReview"] = dtReview;
            dgOverView.DataSource = dtReview;
            dgOverView.DataBind();

            /* Commenting this. The reload botton is active only for last week dashboard. Also, no need to check dashboard finalized or not.
             * 
             * DataTable dtDashBoard = PMOscar.BaseDAL.ExecuteDataTable("Select Status from Dashboard where DashboardID=" + ddlReports.SelectedItem.Value);
            if (dtDashBoard.Rows.Count > 0)
            {
                if (dtDashBoard.Rows[0]["Status"].ToString() == "F")
                {
                    btnRefresh.Enabled = false;
                    //btnRefreshDropDownList.Enabled = false;
                }
                else
                {
                    btnRefresh.Enabled = true;
                    //btnRefreshDropDownList.Enabled = true;
                }
            }*/


            FormatOverviewTab(); // Method to format the OverViewTab...


            dtProject = new DataTable();
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", uptoDate));
            parameter.Add(new SqlParameter("@tabType", "ProjectEffortPhase"));
            parameter.Add(new SqlParameter("@Status", "Go"));
            parameter.Add(new SqlParameter("@DashboardID", ddlReports.SelectedItem.Value));
            parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", cbProposalAndVas.Checked));
            dtProject = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];
            Session["ProjectEffortPhase"] = dtProject;

            FormatProjEffortPhaseTab(); // Method to format the ProjectEffortPhaseTab...


            dtPeriod = new DataTable();
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", uptoDate));
            parameter.Add(new SqlParameter("@tabType", "PeriodEffortPhase"));
            parameter.Add(new SqlParameter("@Status", "Go"));
            parameter.Add(new SqlParameter("@DashboardID", ddlReports.SelectedItem.Value));
            parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", cbProposalAndVas.Checked));
            dtPeriod = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];
            Session["PeriodEffortPhase"] = dtPeriod;

            FormatPeriodEffortPhaseTab();  // Method to format the PeriodEffortPhaseTab...

            dtPeriod = new DataTable();
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", uptoDate));
            parameter.Add(new SqlParameter("@tabType", "ProjectEffortRole"));
            parameter.Add(new SqlParameter("@Status", "Go"));
            parameter.Add(new SqlParameter("@DashboardID", ddlReports.SelectedItem.Value));
            parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", cbProposalAndVas.Checked));
            dtPeriod = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];
            Session["ProjectEffortRole"] = dtPeriod;

            FormatProjectEffortRoleTab(); // Method to format the ProjectEffortRoleTab...

            dtPeriod = new DataTable();
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", uptoDate));
            parameter.Add(new SqlParameter("@tabType", "PeriodEffortRole"));
            parameter.Add(new SqlParameter("@Status", "Go"));
            parameter.Add(new SqlParameter("@DashboardID", ddlReports.SelectedItem.Value));
            parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", cbProposalAndVas.Checked));
            dtPeriod = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];
            Session["PeriodEffortRole"] = dtPeriod;

            FormatPeriodEffortRole(); // Method to format the PeriodEffortRoleTab...

            DataTable dtDatas1 = new DataTable();          
            dtDatas1.Columns.Add("fromperiod");
            dtDatas1.Columns.Add("toperiod");
            dtDatas1.Columns.Add("dashboardid");
            dtDatas1.Columns.Add("status");   
            DataRow dr1 = dtDatas1.NewRow();           
            dr1["fromperiod"] = dayFrom;
            dr1["toperiod"] = dayTo;
            dr1["dashboardid"] = ddlReports.SelectedItem.Value;
            dr1["status"] = ddlProjectPeriod.SelectedItem.Text == "Weekly" ? "W" : "M";
            dtDatas1.Rows.Add(dr1);
            Session["dtDatas1"] = dtDatas1;

            lblHeading.Text = "From:" + ' ' + ddlReports.SelectedItem.Text.ToString().Substring(0, ddlReports.SelectedItem.Text.ToString().IndexOf("-")) + ' ' + '-' + ' ' + "To:" + ' ' + ddlReports.SelectedItem.Text.ToString().Substring(ddlReports.SelectedItem.Text.ToString().IndexOf("-") + 1, (ddlReports.SelectedItem.ToString().Length - ddlReports.SelectedItem.Text.ToString().IndexOf("-") - 1));
     

        }

        // Method to format the OverViewTab...
        private void FormatOverviewTab()
        {
            if ( dgOverView.Rows.Count == 0 )            
                return;

            for (int tableCellCount = 7; tableCellCount < 8; tableCellCount++)
            {
                TableCell tcClient = dgOverView.HeaderRow.Cells[tableCellCount];
                TableCell tcTimeline = dgOverView.HeaderRow.Cells[tableCellCount + 1];
                TableCell tcBudget = dgOverView.HeaderRow.Cells[tableCellCount + 2];
                TableCell tcEscalate = dgOverView.HeaderRow.Cells[tableCellCount + 3];

                string newHeaderText = "<table  width=100% cellspacing=0><tr style=border-right-color: #333333><td colspan=4 align=center>Status</td></tr>";
                newHeaderText += "<tr><td style=font-size:smaller;>Client</td><td style=font-size:smaller;>Timeline</td><td style=font-size:smaller;>Budget</td><td style=font-size:smaller;>Escalate</td></tr></table>";
                tcClient.Text = newHeaderText;
                tcClient.ColumnSpan = 4;

                dgOverView.HeaderRow.Cells.Remove(tcTimeline);
                dgOverView.HeaderRow.Cells.Remove(tcBudget);
                dgOverView.HeaderRow.Cells.Remove(tcEscalate);
            }
        }

        // Method to format the ProjectEffortPhaseTab...
        private void FormatProjEffortPhaseTab()
        {
            DataTable dt = dtProject;
            DataTable dtProjectGrid = dt.DefaultView.ToTable(true, "ProjectName", "ProjectID");
            DataTable dtProjectPhaseGrid = dt.DefaultView.ToTable(true, "Phase");
            DataTable dtProjectTotal = dt.DefaultView.ToTable(true, "BillableTotal", "BudgetTotal", "RevBudgTotal", "ActualHrsTotal");
            DataTable dtProjectActEst = dt.DefaultView.ToTable(true, "Estimate", "Actual");

            foreach ( DataRow drProject in dtProjectPhaseGrid.Rows )
            {
                if ( drProject["Phase"].ToString() != "" )
                { 
                    dtProjectGrid.Columns.Add(drProject["Phase"].ToString() + "_Billable", typeof(string));
                    dtProjectGrid.Columns.Add(drProject["Phase"].ToString() + "_Budgeted", typeof(string));
                    dtProjectGrid.Columns.Add(drProject["Phase"].ToString() + "_Rev.Budgeted", typeof(string));
                    dtProjectGrid.Columns.Add(drProject["Phase"].ToString() + "_Actual", typeof(string));
                }
            }
            dtProjectGrid.Columns.Add(dtProjectTotal.Columns[0].ToString(), typeof(Double));
            dtProjectGrid.Columns.Add(dtProjectTotal.Columns[1].ToString(), typeof(Double));
            dtProjectGrid.Columns.Add(dtProjectTotal.Columns[2].ToString(), typeof(Double));
            dtProjectGrid.Columns.Add(dtProjectTotal.Columns[3].ToString(), typeof(Double));

            dtProjectGrid.Columns.Add(dtProjectActEst.Columns[0].ToString(), typeof(Double));
            dtProjectGrid.Columns.Add(dtProjectActEst.Columns[1].ToString(), typeof(Double));

            foreach (DataRow drRow in dtProjectGrid.Rows)
            {
                DataRow[] drResourceProjects = dt.Select("ProjectID=" + drRow["ProjectID"].ToString());
               
                foreach (DataRow drRowProject in drResourceProjects)
                {
                    if (drRowProject["Phase"].ToString() != "")
                    {
                        drRow[drRowProject["Phase"].ToString() + "_Billable"] = Convert.ToInt64(drRowProject["BillableHours"].ToString() == "" ? "0" : drRowProject["BillableHours"]);
                        drRow[drRowProject["Phase"].ToString() + "_Budgeted"] = Convert.ToInt64(drRowProject["BudgetHours"].ToString() == "" ? "0" : drRowProject["BudgetHours"]);
                        drRow[drRowProject["Phase"].ToString() + "_Rev.Budgeted"] = Convert.ToInt64(drRowProject["RevisedBudgetHours"].ToString() == "" ? "0" : drRowProject["RevisedBudgetHours"]);
                        drRow[drRowProject["Phase"].ToString() + "_Actual"] = Convert.ToDecimal(drRowProject["ActualHours"].ToString() == "" ? "0" : drRowProject["ActualHours"]);

                        drRow["BillableTotal"] = System.Convert.ToInt64(drRowProject["BillableTotal"].ToString() == "" ? "0" : drRowProject["BillableTotal"]);
                        drRow["BudgetTotal"] = System.Convert.ToInt64(drRowProject["BudgetTotal"].ToString() == "" ? "0" : drRowProject["BudgetTotal"]);
                        drRow["RevBudgTotal"] = System.Convert.ToInt64(drRowProject["RevBudgTotal"].ToString() == "" ? "0" : drRowProject["RevBudgTotal"]);
                        drRow["ActualHrsTotal"] = System.Convert.ToDecimal(drRowProject["ActualHrsTotal"].ToString() == "" ? "0" : drRowProject["ActualHrsTotal"]);
                        drRow["Actual"] = System.Convert.ToDecimal(drRowProject["Actual"].ToString() == "" ? "0" : drRowProject["Actual"]);
                        drRow["Estimate"] = System.Convert.ToInt64(drRowProject["Estimate"].ToString() == "" ? "0" : drRowProject["Estimate"]);

                    }
                    else
                    {
                        drRow["Actual"] = System.Convert.ToDecimal(drRowProject["Actual"].ToString() == "" ? "0" : drRowProject["Actual"]);
                        drRow["Estimate"] = System.Convert.ToInt64(drRowProject["Estimate"].ToString() == "" ? "0" : drRowProject["Estimate"]);
                    }
                }
            }

            DataRow drTotal = dtProjectGrid.NewRow();
            drTotal[0] = "Total";
            dtProjectGrid.Rows.Add(drTotal);

            int lastRowIndex = dtProjectGrid.Rows.Count - 1;
            DataRow drlastrow = dtProjectGrid.Rows[lastRowIndex];
            DataRow[] drRows = dt.Select("Phase <>' '");

            foreach (DataRow drRowProject in drRows)
            {
                if (drlastrow.ItemArray[2].ToString() != "")
                {                        

                    drlastrow[drRowProject["Phase"].ToString() + "_Billable"] = System.Convert.ToInt64(drlastrow[drRowProject["Phase"].ToString() + "_Billable"].ToString() == "" ? 0 : drlastrow[drRowProject["Phase"].ToString() + "_Billable"]) + System.Convert.ToInt64(drRowProject["BillableHours"].ToString()==""?"0":drRowProject["BillableHours"]);
                    drlastrow[drRowProject["Phase"].ToString() + "_Budgeted"] = System.Convert.ToInt64(drlastrow[drRowProject["Phase"].ToString() + "_Budgeted"].ToString() == "" ? 0 : drlastrow[drRowProject["Phase"].ToString() + "_Budgeted"]) + System.Convert.ToInt64(drRowProject["BudgetHours"].ToString()==""?"0":drRowProject["BudgetHours"]);
                    drlastrow[drRowProject["Phase"].ToString() + "_Rev.Budgeted"] = System.Convert.ToInt64(drlastrow[drRowProject["Phase"].ToString() + "_Rev.Budgeted"].ToString() == "" ? 0 : drlastrow[drRowProject["Phase"].ToString() + "_Rev.Budgeted"]) + System.Convert.ToInt64(drRowProject["RevisedBudgetHours"].ToString()==""?"0":drRowProject["RevisedBudgetHours"]);
                    drlastrow[drRowProject["Phase"].ToString() + "_Actual"] = System.Convert.ToDecimal(drlastrow[drRowProject["Phase"].ToString() + "_Actual"].ToString()==""?0:drlastrow[drRowProject["Phase"].ToString() + "_Actual"]) + System.Convert.ToDecimal(drRowProject["ActualHours"].ToString()==""?"0":drRowProject["ActualHours"]);
                }
                else
                {                    
                    if ( drlastrow[drRowProject["Phase"].ToString() + "_Billable"].ToString() != "" )
                    {
                        drlastrow[drRowProject["Phase"].ToString() + "_Billable"] = System.Convert.ToInt64(drlastrow[drRowProject["Phase"].ToString() + "_Billable"]) + (drRowProject["BillableHours"].ToString() != "" ? System.Convert.ToInt64(drRowProject["BillableHours"]) : 0.0);
                        drlastrow[drRowProject["Phase"].ToString() + "_Budgeted"] = System.Convert.ToInt64(drlastrow[drRowProject["Phase"].ToString() + "_Budgeted"]) + (drRowProject["BudgetHours"].ToString() != "" ? System.Convert.ToInt64(drRowProject["BudgetHours"]) : 0.0);
                        drlastrow[drRowProject["Phase"].ToString() + "_Rev.Budgeted"] = System.Convert.ToInt64(drlastrow[drRowProject["Phase"].ToString() + "_Rev.Budgeted"]) + (drRowProject["RevisedBudgetHours"].ToString() != "" ? System.Convert.ToInt64(drRowProject["RevisedBudgetHours"]) : 0.0);
                        drlastrow[drRowProject["Phase"].ToString() + "_Actual"] = System.Convert.ToDouble(drlastrow[drRowProject["Phase"].ToString() + "_Actual"]) + (drRowProject["ActualHours"].ToString() != "" ? System.Convert.ToDouble(drRowProject["ActualHours"]) : 0.0);
                    }
                    else
                    { 
                        drlastrow[drRowProject["Phase"].ToString() + "_Billable"] = System.Convert.ToInt64(drRowProject["BillableHours"].ToString() == "" ? "0" : drRowProject["BillableHours"]);
                        drlastrow[drRowProject["Phase"].ToString() + "_Budgeted"] = System.Convert.ToInt64(drRowProject["BudgetHours"].ToString() == "" ? "0" : drRowProject["BudgetHours"]);
                        drlastrow[drRowProject["Phase"].ToString() + "_Rev.Budgeted"] = System.Convert.ToInt64(drRowProject["RevisedBudgetHours"].ToString() == "" ? "0" : drRowProject["RevisedBudgetHours"]);
                        drlastrow[drRowProject["Phase"].ToString() + "_Actual"] = System.Convert.ToDecimal(drRowProject["ActualHours"].ToString() == "" ? "0" : drRowProject["ActualHours"]);
                            //drlastrow[drRowProject["Phase"].ToString() + "_Billable"] = System.Convert.ToInt64(drRowProject["BillableHours"]);
                            //drlastrow[drRowProject["Phase"].ToString() + "_Budgeted"] = System.Convert.ToInt64(drRowProject["BudgetHours"]);
                            //drlastrow[drRowProject["Phase"].ToString() + "_Rev.Budgeted"] = System.Convert.ToInt64(drRowProject["RevisedBudgetHours"]);
                            //drlastrow[drRowProject["Phase"].ToString() + "_Actual"] = System.Convert.ToInt64(drRowProject["ActualHours"]);
                        
                    }
                }
            }

            drRows = dtProjectGrid.Select("ProjectName <>'Total'");

            foreach (DataRow drRow in drRows)
            {
                    if (drlastrow["BillableTotal"].ToString() != "")
                    {
                        drlastrow["BillableTotal"] = System.Convert.ToInt64(drlastrow["BillableTotal"]) + (drRow["BillableTotal"].ToString() == "" ? 0.0 : System.Convert.ToInt64(drRow["BillableTotal"]));
                        drlastrow["BudgetTotal"] = System.Convert.ToInt64(drlastrow["BudgetTotal"]) + (drRow["BudgetTotal"].ToString() == "" ? 0.0 : System.Convert.ToInt64(drRow["BudgetTotal"]));
                        drlastrow["RevBudgTotal"] = System.Convert.ToInt64(drlastrow["RevBudgTotal"]) + (drRow["RevBudgTotal"].ToString() == "" ? 0.0 : System.Convert.ToInt64(drRow["RevBudgTotal"]));
                        drlastrow["ActualHrsTotal"] = System.Convert.ToDouble(drlastrow["ActualHrsTotal"]) + (drRow["ActualHrsTotal"].ToString() == "" ? 0.0 : System.Convert.ToDouble(drRow["ActualHrsTotal"]));
                    }
                    else
                    {
                        drlastrow["BillableTotal"] = System.Convert.ToInt64(drRow["BillableTotal"].ToString() == "" ? "0" : drRow["BillableTotal"]);
                        drlastrow["BudgetTotal"] = System.Convert.ToInt64(drRow["BudgetTotal"].ToString() == "" ? "0" : drRow["BudgetTotal"]);
                        drlastrow["RevBudgTotal"] = System.Convert.ToInt64(drRow["RevBudgTotal"].ToString() == "" ? "0" :drRow["RevBudgTotal"] );
                        drlastrow["ActualHrsTotal"] = System.Convert.ToDouble(drRow["ActualHrsTotal"].ToString() == "" ? "0" : drRow["ActualHrsTotal"]);
                    }
                    foreach (DataRow drRow1 in dtProjectPhaseGrid.Rows)
                    {
                        if (drRow1["Phase"].ToString() != "")
                        { 
                            if (drRow[drRow1["Phase"].ToString() + "_Billable"].ToString() == "")
                                drRow[drRow1["Phase"].ToString() + "_Billable"] = 0;
                            if (drRow[drRow1["Phase"].ToString() + "_Budgeted"].ToString() == "")
                                drRow[drRow1["Phase"].ToString() + "_Budgeted"] = 0;
                            if (drRow[drRow1["Phase"].ToString() + "_Rev.Budgeted"].ToString() == "")
                                drRow[drRow1["Phase"].ToString() + "_Rev.Budgeted"] = 0;
                            if (drRow[drRow1["Phase"].ToString() + "_Actual"].ToString() == "")
                                drRow[drRow1["Phase"].ToString() + "_Actual"] = 0; 
                        }
                    }
                    if (drRow["BillableTotal"].ToString() == "")
                        drRow["BillableTotal"] = 0;
                    if (drRow["BudgetTotal"].ToString() == "")
                        drRow["BudgetTotal"] = 0;
                    if (drRow["RevBudgTotal"].ToString() == "")
                        drRow["RevBudgTotal"] = 0;
                    if (drRow["ActualHrsTotal"].ToString() == "")
                        drRow["ActualHrsTotal"] = 0;
             }

 
            dgProjectEffortPhase.DataSource = dtProjectGrid;
            dgProjectEffortPhase.DataBind();

           
            for (int tableCell = 4; tableCell < dgProjectEffortPhase.HeaderRow.Cells.Count - 2; tableCell++)
            {
               
                    
                if (tableCell == dgProjectEffortPhase.HeaderRow.Cells.Count - 6)
                {
                    TableCell tcBillableCell = dgProjectEffortPhase.HeaderRow.Cells[tableCell];
                    TableCell tcBudgetedCell = dgProjectEffortPhase.HeaderRow.Cells[tableCell + 1];
                    TableCell tcRevBudgetedCell = dgProjectEffortPhase.HeaderRow.Cells[tableCell + 2];
                    TableCell tcActualCell = dgProjectEffortPhase.HeaderRow.Cells[tableCell + 3];

                    string newHeaderText = "<table width=100% cellspacing=0><tr><td colspan=4 align=center>Total</td></tr>";
                    newHeaderText += "<tr><td width=50 align=center>Bill.</td><td width=50 align=center>Bud.</td><td width=50 align=center>Rev.</td><td width=50 align=center>Act.</td></tr></table>";
                    tcBillableCell.Text = newHeaderText;
                    tcBillableCell.ColumnSpan = 4;

                    dgProjectEffortPhase.HeaderRow.Cells.Remove(tcBudgetedCell);
                    dgProjectEffortPhase.HeaderRow.Cells.Remove(tcRevBudgetedCell);
                    dgProjectEffortPhase.HeaderRow.Cells.Remove(tcActualCell);
                }
                else
                {
                    TableCell tcBillableCell = dgProjectEffortPhase.HeaderRow.Cells[tableCell];
                    TableCell tcBudgetedCell = dgProjectEffortPhase.HeaderRow.Cells[tableCell + 1];
                    TableCell tcRevBudgetedCell = dgProjectEffortPhase.HeaderRow.Cells[tableCell + 2];
                    TableCell tcActualCell = dgProjectEffortPhase.HeaderRow.Cells[tableCell + 3];

                    if (tcActualCell.Text.Contains("_"))
                    {
                    string project = tcActualCell.Text.Remove(tcActualCell.Text.IndexOf('_'));
                    string newHeaderText = "<table  width=100% cellspacing=0><tr style=border-right-color: #333333><td colspan=4 align=center>" + project + "</td></tr>";
                    newHeaderText += "<tr><td width=50 align=center>Bill.</td><td width=50 align=center>Bud.</td><td width=50 align=center>Rev.</td><td width=50 align=center>Act.</td></tr></table>";

                    tcBillableCell.Text = newHeaderText;
                    tcBillableCell.ColumnSpan = 4;

                    dgProjectEffortPhase.HeaderRow.Cells.Remove(tcBudgetedCell);
                    dgProjectEffortPhase.HeaderRow.Cells.Remove(tcRevBudgetedCell);
                    dgProjectEffortPhase.HeaderRow.Cells.Remove(tcActualCell);
                    }
                    else
                    {
                        string project = tcActualCell.Text;
                        string newHeaderText = "<table  width=100% cellspacing=0><tr style=border-right-color: #333333><td colspan=4 align=center>" + project + "</td></tr>";
                        newHeaderText += "<tr><td width=50 align=center>Bill.</td><td width=50 align=center>Bud.</td><td width=50 align=center>Rev.</td><td width=50 align=center>Act.</td></tr></table>";

                        tcBillableCell.Text = newHeaderText;
                        tcBillableCell.ColumnSpan = 4;

                    }
                }
                
            }
            dtProjectGrid.Dispose();
        }

        // Method to format the PeriodEffortPhaseTab...
        private void FormatPeriodEffortPhaseTab()
        {
            DataTable dt = dtPeriod;
            DataTable dtPerioGrid = dt.DefaultView.ToTable(true, "ProjectName", "ProjectID", "CurrPhase");
            DataTable dtPeriodPhase = dt.DefaultView.ToTable(true, "Phase");
            DataTable dtPeriodTotal = dt.DefaultView.ToTable(true, "PeriodEstHrsTotal", "PeriodActualHrsTotal");

            foreach ( DataRow drProjectRows in dtPeriodPhase.Rows )
            {
                if ( drProjectRows["Phase"].ToString() != "" )
                {
                    dtPerioGrid.Columns.Add(drProjectRows["Phase"].ToString() + "_PeriodEstimetedHrs", typeof(string));
                    dtPerioGrid.Columns.Add(drProjectRows["Phase"].ToString() + "_PeriodActualHrs", typeof(string));
                }
            }
            dtPerioGrid.Columns.Add(dtPeriodTotal.Columns[0].ToString(), typeof(Double));
            dtPerioGrid.Columns.Add(dtPeriodTotal.Columns[1].ToString(), typeof(Double));

            foreach ( DataRow drRow in dtPerioGrid.Rows )
            {
                DataRow[] drResourceProjects = dt.Select("ProjectID=" + drRow["ProjectID"].ToString());
                Double EstTotal = 0.0;
                Double ActTotal = 0.0;

                foreach ( DataRow drRowCells in drResourceProjects )
                {
                    if (drRowCells["Phase"].ToString() != "")
                    {
                        drRow[drRowCells["Phase"].ToString() + "_PeriodEstimetedHrs"] = Convert.ToInt64(drRowCells["PeriodEstimetedHrs"].ToString() == "" ? "0" : drRowCells["PeriodEstimetedHrs"]);
                        drRow[drRowCells["Phase"].ToString() + "_PeriodActualHrs"] = Convert.ToDouble(drRowCells["PeriodActualHrs"].ToString() == "" ? "0" : drRowCells["PeriodActualHrs"]);
                        drRow["PeriodEstHrsTotal"] = System.Convert.ToInt64(drRowCells["PeriodEstHrsTotal"].ToString() == "" ? "0" : drRowCells["PeriodEstHrsTotal"]);
                        drRow["PeriodActualHrsTotal"] = System.Convert.ToDouble(drRowCells["PeriodActualHrsTotal"].ToString() == "" ? "0" : drRowCells["PeriodActualHrsTotal"]);
                        //drRow[drRowCells["Phase"].ToString() + "_PeriodEstimetedHrs"] = Convert.ToInt64(drRowCells["PeriodEstimetedHrs"]);
                        //drRow[drRowCells["Phase"].ToString() + "_PeriodActualHrs"] = Convert.ToDouble(drRowCells["PeriodActualHrs"]);  
                        //drRow["PeriodEstHrsTotal"] = System.Convert.ToInt64(drRowCells["PeriodEstHrsTotal"]);
                        //drRow["PeriodActualHrsTotal"] =System.Convert.ToDouble(drRowCells["PeriodActualHrsTotal"]);
                        EstTotal = EstTotal + System.Convert.ToInt64(drRowCells["PeriodEstimetedHrs"]);
                        ActTotal = ActTotal + System.Convert.ToDouble(drRowCells["PeriodActualHrs"]);
                    }
                }
            }
                DataRow drTotal = dtPerioGrid.NewRow();
                drTotal[0] = "Total";
                dtPerioGrid.Rows.Add(drTotal);

                int lastRowIndex = dtPerioGrid.Rows.Count - 1;
                DataRow drlastRow = dtPerioGrid.Rows[lastRowIndex];
                DataRow[] drRows = dt.Select("Phase <>' '");

                foreach ( DataRow drRow in drRows )
                {
                    if ( drlastRow.ItemArray[2].ToString() != "" )
                    {               
                        drlastRow[drRow["Phase"].ToString() + "_PeriodEstimetedHrs"] = System.Convert.ToInt64(drlastRow[drRow["Phase"].ToString() + "_PeriodEstimetedHrs"]) + System.Convert.ToInt64(drRow["PeriodEstimetedHrs"]);
                        drlastRow[drRow["Phase"].ToString() + "_PeriodActualHrs"] = System.Convert.ToDouble(drlastRow[drRow["Phase"].ToString() + "_PeriodActualHrs"]) + System.Convert.ToDouble(drRow["PeriodActualHrs"]);                        
                    }
                    else
                    {                      
                        if ( drlastRow[drRow["Phase"].ToString() + "_PeriodEstimetedHrs"].ToString() != "" )
                        {
                            drlastRow[drRow["Phase"].ToString() + "_PeriodEstimetedHrs"] = System.Convert.ToInt64(drlastRow[drRow["Phase"].ToString() + "_PeriodEstimetedHrs"]) + (drRow["PeriodEstimetedHrs"].ToString() != "" ? System.Convert.ToInt64(drRow["PeriodEstimetedHrs"]) : 0.0);
                            drlastRow[drRow["Phase"].ToString() + "_PeriodActualHrs"] = System.Convert.ToDouble(drlastRow[drRow["Phase"].ToString() + "_PeriodActualHrs"]) + (drRow["PeriodActualHrs"].ToString() != "" ? System.Convert.ToDouble(drRow["PeriodActualHrs"]) : 0.0);
                        }
                        else
                        {
                            drlastRow[drRow["Phase"].ToString() + "_PeriodEstimetedHrs"] = System.Convert.ToInt64(drRow["PeriodEstimetedHrs"]);
                            drlastRow[drRow["Phase"].ToString() + "_PeriodActualHrs"] = System.Convert.ToDouble(drRow["PeriodActualHrs"]);
                        }
                    }
                    
                }
               
                drRows = dtPerioGrid.Select("CurrPhase <>'Total'");
                foreach ( DataRow drRow in drRows )
                {
                    if ( drlastRow["PeriodEstHrsTotal"].ToString() != "" )
                    {
                        drlastRow["PeriodEstHrsTotal"] = System.Convert.ToInt64(drlastRow["PeriodEstHrsTotal"]) + (drRow["PeriodEstHrsTotal"].ToString() == "" ? 0.0 : System.Convert.ToInt64(drRow["PeriodEstHrsTotal"]));
                        drlastRow["PeriodActualHrsTotal"] = System.Convert.ToDouble(drlastRow["PeriodActualHrsTotal"]) + (drRow["PeriodActualHrsTotal"].ToString() == "" ? 0.0 : System.Convert.ToDouble(drRow["PeriodActualHrsTotal"]));                     
                    }
                    else
                    {
                        if(drRow["PeriodEstHrsTotal"].ToString()!="")
                            drlastRow["PeriodEstHrsTotal"] = System.Convert.ToInt64(drRow["PeriodEstHrsTotal"]);
                        if (drRow["PeriodActualHrsTotal"].ToString() != "")
                        drlastRow["PeriodActualHrsTotal"] = System.Convert.ToDouble(drRow["PeriodActualHrsTotal"]);
                    }
                }
                foreach (DataRow drRow1 in dtPerioGrid.Rows)
                {
                    foreach (DataRow drRow2 in dtPeriodPhase.Rows)
                    {
                        if (drRow2["Phase"].ToString() != "")
                        {
                            if (drRow1[drRow2["Phase"].ToString() + "_PeriodEstimetedHrs"].ToString() == "")
                                drRow1[drRow2["Phase"].ToString() + "_PeriodEstimetedHrs"] = 0;
                            if (drRow1[drRow2["Phase"].ToString() + "_PeriodActualHrs"].ToString() == "")
                                drRow1[drRow2["Phase"].ToString() + "_PeriodActualHrs"] = 0;
                        }
                    }
                    if (drRow1["PeriodEstHrsTotal"].ToString() == "")
                        drRow1["PeriodEstHrsTotal"] = 0;
                    if (drRow1["PeriodActualHrsTotal"].ToString() == "")
                        drRow1["PeriodActualHrsTotal"] = 0;
                }

                dgPeriodEffortPhase.DataSource = dtPerioGrid;
                dgPeriodEffortPhase.DataBind();

                for (int cellsCount = 5; cellsCount < dgPeriodEffortPhase.HeaderRow.Cells.Count - 1; cellsCount++)
                {
                    if (cellsCount == dgPeriodEffortPhase.HeaderRow.Cells.Count - 2)
                    {
                        TableCell tcBillableCell = dgPeriodEffortPhase.HeaderRow.Cells[cellsCount];
                        TableCell tcbudgetedCell = dgPeriodEffortPhase.HeaderRow.Cells[cellsCount + 1];

                        string newHeaderText = "<table width=100% cellspacing=0><tr><td colspan=2 align=center>Total</td></tr>";
                        newHeaderText += "<tr><td >Alloc.</td><td >Act.</td></tr></table>";
                        tcBillableCell.Text = newHeaderText;
                        tcBillableCell.ColumnSpan = 2;
                        dgPeriodEffortPhase.HeaderRow.Cells.Remove(tcbudgetedCell);
                    }
                    else
                    {
                        TableCell tcBillable = dgPeriodEffortPhase.HeaderRow.Cells[cellsCount];
                        TableCell tcbudgeted = dgPeriodEffortPhase.HeaderRow.Cells[cellsCount + 1];


                        string project = tcBillable.Text.Remove(tcBillable.Text.IndexOf('_'));
                        string newHeaderText = "<table  width=100% cellspacing=0><tr style=border-right-color: #333333><td colspan=2 align=center>" + project + "</td></tr>";
                        newHeaderText += "<tr><td >Alloc.</td><td >Act.</td></tr></table>";

                        tcBillable.Text = newHeaderText;
                        tcBillable.ColumnSpan = 2;

                        dgPeriodEffortPhase.HeaderRow.Cells.Remove(tcbudgeted);
                    }                
            }
        }

        // Method to format the ProjectEffortRoleTab...
        private void FormatProjectEffortRoleTab()
        {
            DataTable dt = dtPeriod;
            DataTable dtProjectGrid = dt.DefaultView.ToTable(true, "ProjectName", "ProjectID");
            DataTable dtProjectRoles = dt.DefaultView.ToTable(true, "RoleName");
            DataTable dtProjectTotal = dt.DefaultView.ToTable(true, "RevBudgTotal", "ActualHrsTotal");

            DataTable dtprojectSort = dt.DefaultView.ToTable(true, "Estimate","SortOrder");

            foreach ( DataRow drProject in dtProjectRoles.Rows )
            {
                if ( drProject["RoleName"].ToString() != "" )
                {
                    dtProjectGrid.Columns.Add(drProject["RoleName"].ToString() + "_RevisedBudgetHours", typeof(string));
                    dtProjectGrid.Columns.Add(drProject["RoleName"].ToString() + "_ActualHours", typeof(string));
                }
            }
            dtProjectGrid.Columns.Add(dtProjectTotal.Columns[0].ToString(), typeof(Double));
            dtProjectGrid.Columns.Add(dtProjectTotal.Columns[1].ToString(), typeof(Double));

            dtProjectGrid.Columns.Add(dtprojectSort.Columns[0].ToString(), typeof(Double));
            dtProjectGrid.Columns.Add(dtprojectSort.Columns[1].ToString(), typeof(Double));


            foreach (DataRow drRow in dtProjectGrid.Rows)
            {
                DataRow[] drResourceProjects = dt.Select("ProjectID=" + drRow["ProjectID"].ToString());
                Double estimatedTotal = 0.0;
                Double actualTotal = 0.0;

                foreach (DataRow drRowCells in drResourceProjects)
                {
                    if (drRowCells["RoleName"].ToString() != "")
                    {
                        drRow[drRowCells["RoleName"].ToString() + "_RevisedBudgetHours"] = Convert.ToInt64(drRowCells["RevisedBudgetHours"].ToString() == "" ? "0" : drRowCells["RevisedBudgetHours"]);
                        drRow[drRowCells["RoleName"].ToString() + "_ActualHours"] = Convert.ToDouble(drRowCells["ActualHours"].ToString() == "" ? "0" : drRowCells["ActualHours"]);
                    }
                    drRow["RevBudgTotal"] = System.Convert.ToInt64(estimatedTotal) + System.Convert.ToInt64(drRowCells["RevisedBudgetHours"].ToString() == "" ? "0" : drRowCells["RevisedBudgetHours"]);
                    drRow["ActualHrsTotal"] = System.Convert.ToDouble(actualTotal) + System.Convert.ToDouble(drRowCells["ActualHours"].ToString() == "" ? "0" : drRowCells["ActualHours"]);
                    estimatedTotal = estimatedTotal + System.Convert.ToInt64(drRowCells["RevisedBudgetHours"].ToString() == "" ? "0" : drRowCells["RevisedBudgetHours"]);
                    actualTotal = actualTotal + System.Convert.ToDouble(drRowCells["ActualHours"].ToString() == "" ? "0" : drRowCells["ActualHours"]);
                    //drRow[drRowCells["RoleName"].ToString() + "_RevisedBudgetHours"] = Convert.ToInt64(drRowCells["RevisedBudgetHours"]);
                    //drRow[drRowCells["RoleName"].ToString() + "_ActualHours"] = Convert.ToDouble(drRowCells["ActualHours"]);
                    //drRow["RevBudgTotal"] = System.Convert.ToInt64(estimatedTotal) + System.Convert.ToInt64(drRowCells["RevisedBudgetHours"]);
                    //drRow["ActualHrsTotal"] = System.Convert.ToDouble(actualTotal) + System.Convert.ToDouble(drRowCells["ActualHours"]);
                    //estimatedTotal = estimatedTotal + System.Convert.ToInt64(drRowCells["RevisedBudgetHours"]);
                    //actualTotal = actualTotal + System.Convert.ToDouble(drRowCells["ActualHours"]);

                  drRow["SortOrder"] = System.Convert.ToInt64(drRowCells["SortOrder"].ToString() == "" ? "0" : drRowCells["SortOrder"]);
                  drRow["Estimate"] = System.Convert.ToInt64(drRowCells["Estimate"].ToString() == "" ? "0" : drRowCells["Estimate"]);
                } 
            }

            DataRow drTotal = dtProjectGrid.NewRow();
            drTotal[0] = "Total";
            dtProjectGrid.Rows.Add(drTotal);

            int lastRowIndex = dtProjectGrid.Rows.Count - 1;
            DataRow drLastRow = dtProjectGrid.Rows[lastRowIndex];
            DataRow[] drRows = dt.Select("RoleName <>' '");

            foreach ( DataRow drRow in drRows )
            {
                if ( drLastRow.ItemArray[2].ToString() != "" )
                { 
                    drLastRow[drRow["RoleName"].ToString() + "_RevisedBudgetHours"] = System.Convert.ToInt64(drLastRow[drRow["RoleName"].ToString() + "_RevisedBudgetHours"].ToString() != "" ? drLastRow[drRow["RoleName"].ToString() + "_RevisedBudgetHours"] : 0) + System.Convert.ToInt64(drRow["RevisedBudgetHours"].ToString() == "" ? "0" : drRow["RevisedBudgetHours"]);
                    drLastRow[drRow["RoleName"].ToString() + "_ActualHours"] = System.Convert.ToDouble(drLastRow[drRow["RoleName"].ToString() + "_ActualHours"].ToString() != "" ? drLastRow[drRow["RoleName"].ToString() + "_ActualHours"] : 0) + System.Convert.ToDouble(drRow["ActualHours"]);
                }
                else
                {                     
                    if ( drLastRow[drRow["RoleName"].ToString() + "_RevisedBudgetHours"].ToString() != "" )
                    {
                        drLastRow[drRow["RoleName"].ToString() + "_RevisedBudgetHours"] = System.Convert.ToInt64(drLastRow[drRow["RoleName"].ToString() + "_RevisedBudgetHours"]) + (drRow["RevisedBudgetHours"].ToString() != "" ? System.Convert.ToInt64(drRow["RevisedBudgetHours"]) : 0.0);
                        drLastRow[drRow["RoleName"].ToString() + "_ActualHours"] = System.Convert.ToDouble(drLastRow[drRow["RoleName"].ToString() + "_ActualHours"]) + (drRow["ActualHours"].ToString() != "" ? System.Convert.ToDouble(drRow["ActualHours"]) : 0.0);
                    }
                    else
                    {
                        drLastRow[drRow["RoleName"].ToString() + "_RevisedBudgetHours"] = System.Convert.ToInt64(drRow["RevisedBudgetHours"].ToString() == "" ? "0" : drRow["RevisedBudgetHours"]);
                        drLastRow[drRow["RoleName"].ToString() + "_ActualHours"] = System.Convert.ToDouble(drRow["ActualHours"].ToString() == "" ? "0" : drRow["ActualHours"]);
                   }
                }
            }
         

             drRows = dtProjectGrid.Select("ProjectName <>'Total'");

             foreach ( DataRow drRP in drRows )
             {
                 if ( drLastRow["RevBudgTotal"].ToString() != "" )
                 {
                     drLastRow["RevBudgTotal"] = System.Convert.ToInt64(drLastRow["RevBudgTotal"]) + System.Convert.ToInt64(drRP["RevBudgTotal"]);
                     drLastRow["ActualHrsTotal"] = System.Convert.ToDouble(drLastRow["ActualHrsTotal"]) + System.Convert.ToDouble(drRP["ActualHrsTotal"]);
                 }
                 else
                 {
                     drLastRow["RevBudgTotal"] = System.Convert.ToInt64(drRP["RevBudgTotal"]);
                     drLastRow["ActualHrsTotal"] = System.Convert.ToDouble(drRP["ActualHrsTotal"]);
                 }
             }
             foreach (DataRow drRow in drRows)
             {
                 foreach (DataRow drRow1 in dtProjectRoles.Rows)
                 {
                     if (drRow1["RoleName"].ToString() != "")
                     {
                         if (drRow[drRow1["RoleName"].ToString() + "_RevisedBudgetHours"].ToString() == "")
                             drRow[drRow1["RoleName"].ToString() + "_RevisedBudgetHours"] = 0;
                         if (drRow[drRow1["RoleName"].ToString() + "_ActualHours"].ToString() == "")
                             drRow[drRow1["RoleName"].ToString() + "_ActualHours"] = 0;
                     }
                 }
             }

             int countBeforeDelete = dtProjectGrid.Columns.Count - 1;
             int afterDel = 0;
             int j;
             for (int k = 2; k <= countBeforeDelete; k = k + 1)
             {
                 for (j = 2; j <= countBeforeDelete; j++)
                 {

                     foreach (DataRow drProject in dtProjectRoles.Rows)
                     {

                         if ((drLastRow.ItemArray[k].ToString() == "0") && (drLastRow.ItemArray[k + 1].ToString() == "0"))
                         {

                             if (drProject["RoleName"].ToString() != "")
                             {

                                 if (j == k)
                                 {
                                     dtProjectGrid.Columns.Remove(dtProjectGrid.Columns[k]);
                                     dtProjectGrid.Columns.Remove(dtProjectGrid.Columns[k]);
                                     afterDel = dtProjectGrid.Columns.Count - 1;
                                     countBeforeDelete = afterDel;
                                 }
                             }
                         }

                     }


                 }

             }
             
            dgProjectEffortRole.DataSource = dtProjectGrid;
            dgProjectEffortRole.DataBind();


            for (int tableCellCount = 4; tableCellCount < dgProjectEffortRole.HeaderRow.Cells.Count - 2; tableCellCount++)
            {
                if (tableCellCount == dgProjectEffortRole.HeaderRow.Cells.Count - 4)
                {
                    TableCell tcBillableCell = dgProjectEffortRole.HeaderRow.Cells[tableCellCount];
                    TableCell tcbudgetedCell = dgProjectEffortRole.HeaderRow.Cells[tableCellCount + 1];

                    string newHeaderText = "<table width=100% cellspacing=0><tr><td colspan=2 align=center>Total</td></tr>";
                    newHeaderText += "<tr><td >Rev.</td><td >Act.</td></tr></table>";

                    tcBillableCell.Text = newHeaderText;
                    tcBillableCell.ColumnSpan = 2;

                    dgProjectEffortRole.HeaderRow.Cells.Remove(tcbudgetedCell);
                }
                else
                {
                    TableCell tcBillable = dgProjectEffortRole.HeaderRow.Cells[tableCellCount];
                    TableCell tcbudgeted = dgProjectEffortRole.HeaderRow.Cells[tableCellCount + 1];

                    //string proj = tcBillable.Text.Remove(tcBillable.Text.IndexOf('_'));
                    //string newHeaderText = "<table  width=100% cellspacing=0><tr style=border-right-color: #333333><td colspan=2 align=center>" + proj + "</td></tr>";
                    //newHeaderText += "<tr><td >Rev.</td><td >Act.</td></tr></table>";

                    //tcBillable.Text = newHeaderText;
                    //tcBillable.ColumnSpan = 2;

                    //dgProjectEffortRole.HeaderRow.Cells.Remove(tcbudgeted);

                    if (tcBillable.Text.Contains("_"))
                    {
                        string proj = tcBillable.Text.Remove(tcBillable.Text.IndexOf('_'));
                        string newHeaderText = "<table  width=100% cellspacing=0><tr style=border-right-color: #333333><td colspan=2 align=center>" + proj + "</td></tr>";
                        newHeaderText += "<tr><td >Rev.</td><td >Act.</td></tr></table>";

                        tcBillable.Text = newHeaderText;
                        tcBillable.ColumnSpan = 2;

                        dgProjectEffortRole.HeaderRow.Cells.Remove(tcbudgeted);
                    }
                    else
                    {
                        string proj = tcBillable.Text;
                        string newHeaderText = "<table  width=100% cellspacing=0><tr style=border-right-color: #333333><td colspan=2 align=center>" + proj + "</td></tr>";
                        newHeaderText += "<tr><td >Rev.</td><td >Act.</td></tr></table>";

                        tcBillable.Text = newHeaderText;
                        tcBillable.ColumnSpan = 2;
                    }
                }
            }

                        
        }

        // Method to format the PeriodEffortRoleTab...
        private void FormatPeriodEffortRole()
        {
            DataTable dt = dtPeriod;
            DataTable dtPeriodGrid = dt.DefaultView.ToTable(true, "ProjectName", "ProjectID");
            DataTable dtPeriodRoles = dt.DefaultView.ToTable(true, "RoleName");
            DataTable dtPeriodTotal = dt.DefaultView.ToTable(true, "EstTotal", "ActTotal");

            foreach ( DataRow drProject in dtPeriodRoles.Rows )
            {
                if ( drProject["RoleName"].ToString() != "" )
                {
                    dtPeriodGrid.Columns.Add(drProject["RoleName"].ToString() + "_PeriodEstimetedHrs", typeof(string));
                    dtPeriodGrid.Columns.Add(drProject["RoleName"].ToString() + "_PeriodActualHrs", typeof(string));
                }
            }
            dtPeriodGrid.Columns.Add(dtPeriodTotal.Columns[0].ToString(), typeof(Double));
            dtPeriodGrid.Columns.Add(dtPeriodTotal.Columns[1].ToString(), typeof(Double));

            foreach (DataRow drRow in dtPeriodGrid.Rows)
            {
                DataRow[] drResourceProjects = dt.Select("ProjectID=" + drRow["ProjectID"].ToString());
                Double EstTotal = 0.0;
                Double ActTotal = 0.0;

                foreach (DataRow drRowCells in drResourceProjects)
                {
                    if (drRowCells["RoleName"].ToString() != "")
                    {
                        drRow[drRowCells["RoleName"].ToString() + "_PeriodEstimetedHrs"] = Convert.ToInt64(drRowCells["PeriodEstimetedHrs"].ToString() == "" ? "0" : drRowCells["PeriodEstimetedHrs"]);
                        drRow[drRowCells["RoleName"].ToString() + "_PeriodActualHrs"] = Convert.ToDecimal(drRowCells["PeriodActualHrs"].ToString() == "" ? "0" : drRowCells["PeriodActualHrs"]);
                    }
                    drRow["EstTotal"] = System.Convert.ToInt64(EstTotal) + System.Convert.ToInt64(drRowCells["PeriodEstimetedHrs"].ToString() == "" ? "0" : drRowCells["PeriodEstimetedHrs"]);
                    drRow["ActTotal"] = System.Convert.ToDecimal(ActTotal) + System.Convert.ToDecimal(drRowCells["PeriodActualHrs"].ToString() == "" ? "0" : drRowCells["PeriodActualHrs"]);
                    EstTotal = EstTotal + System.Convert.ToInt64(drRowCells["PeriodEstimetedHrs"].ToString() == "" ? "0" : drRowCells["PeriodEstimetedHrs"]);
                    ActTotal = ActTotal + System.Convert.ToDouble(drRowCells["PeriodActualHrs"].ToString()==""?"0":drRowCells["PeriodActualHrs"]);
                }

            }

            DataRow drPeriodTotal = dtPeriodGrid.NewRow();
            drPeriodTotal[0] = "Total";
            dtPeriodGrid.Rows.Add(drPeriodTotal);

            int lastRowIndex = dtPeriodGrid.Rows.Count - 1;
            DataRow drlastRow = dtPeriodGrid.Rows[lastRowIndex];
            DataRow[] drRows = dt.Select("RoleName <>' '");

            foreach ( DataRow drRow in drRows )
            {
                if ( drlastRow.ItemArray[2].ToString() != "" )
                {
                    drlastRow[drRow["RoleName"].ToString() + "_PeriodEstimetedHrs"] = System.Convert.ToInt64(drlastRow[drRow["RoleName"].ToString() + "_PeriodEstimetedHrs"].ToString()!=""?drlastRow[drRow["RoleName"].ToString() + "_PeriodEstimetedHrs"]:0) + System.Convert.ToInt64(drRow["PeriodEstimetedHrs"]);
                    drlastRow[drRow["RoleName"].ToString() + "_PeriodActualHrs"] = System.Convert.ToDecimal(drlastRow[drRow["RoleName"].ToString() + "_PeriodActualHrs"].ToString()!=""?drlastRow[drRow["RoleName"].ToString() + "_PeriodActualHrs"]:0) + System.Convert.ToDecimal(drRow["PeriodActualHrs"]);
                }
                else
                {                   
                    if ( drlastRow[drRow["RoleName"].ToString() + "_PeriodEstimetedHrs"].ToString() != "" )
                    {
                        drlastRow[drRow["RoleName"].ToString() + "_PeriodEstimetedHrs"] = System.Convert.ToInt64(drlastRow[drRow["RoleName"].ToString() + "_PeriodEstimetedHrs"]) + (drRow["PeriodEstimetedHrs"].ToString() != "" ? System.Convert.ToInt64(drRow["PeriodEstimetedHrs"]) : 0.0);
                        drlastRow[drRow["RoleName"].ToString() + "_PeriodActualHrs"] = System.Convert.ToDouble(drlastRow[drRow["RoleName"].ToString() + "_PeriodActualHrs"]) + (drRow["PeriodActualHrs"].ToString() != "" ? System.Convert.ToDouble(drRow["PeriodActualHrs"]) : 0.0);
                    }
                    else
                    {
                        drlastRow[drRow["RoleName"].ToString() + "_PeriodEstimetedHrs"] = System.Convert.ToInt64(drRow["PeriodEstimetedHrs"]);
                        drlastRow[drRow["RoleName"].ToString() + "_PeriodActualHrs"] = System.Convert.ToDecimal(drRow["PeriodActualHrs"]);
                    }
                   
                }
            }
                  
             drRows = dtPeriodGrid.Select("ProjectName <>'Total'");

            foreach (DataRow drRow in drRows)
            {
                if (drlastRow["EstTotal"].ToString() != "")
                {
                    drlastRow["EstTotal"] = System.Convert.ToInt64(drlastRow["EstTotal"]) + (drRow["EstTotal"].ToString() == "" ? 0.0 : System.Convert.ToInt64(drRow["EstTotal"]));
                    drlastRow["ActTotal"] = System.Convert.ToInt64(drlastRow["ActTotal"]) + (drRow["ActTotal"].ToString() == "" ? 0.0 : System.Convert.ToInt64(drRow["ActTotal"]));
                }
                else
                {
                    drlastRow["EstTotal"] = System.Convert.ToInt64(drRow["EstTotal"]);
                    drlastRow["ActTotal"] = System.Convert.ToInt64(drRow["ActTotal"]);
                }
            }
            foreach (DataRow drRow in drRows)
            {
                foreach (DataRow drRow1 in dtPeriodRoles.Rows)
                {
                    if (drRow1["RoleName"].ToString() != "")
                    {
                        if (drRow[drRow1["RoleName"].ToString() + "_PeriodEstimetedHrs"].ToString() == "")
                            drRow[drRow1["RoleName"].ToString() + "_PeriodEstimetedHrs"] = 0;
                        if (drRow[drRow1["RoleName"].ToString() + "_PeriodActualHrs"].ToString() == "")
                            drRow[drRow1["RoleName"].ToString() + "_PeriodActualHrs"] = 0;
                    }
                }
            }
            int countBeforeDelete = dtPeriodGrid.Columns.Count - 1;
            int j;
            for (int k = 2; k < countBeforeDelete; k = k + 1)
            {
                for (j = 2; j < countBeforeDelete; j++)
                {

                    foreach (DataRow drProject in dtPeriodRoles.Rows)
                    {

                        if ((drlastRow.ItemArray[k].ToString() == "0") && (drlastRow.ItemArray[k + 1].ToString() == "0"))
                        {

                            if (drProject["RoleName"].ToString() != "")
                            {

                                if (j == k)
                                {
                                    dtPeriodGrid.Columns.Remove(dtPeriodGrid.Columns[k]);
                                    dtPeriodGrid.Columns.Remove(dtPeriodGrid.Columns[k]);
                                    int afterDel = dtPeriodGrid.Columns.Count - 1;
                                    countBeforeDelete = afterDel;
                                    k = afterDel;
                                }
                            }
                        }

                    }


                }

            }
            dgPeriodEffortRole.DataSource = dtPeriodGrid;
            dgPeriodEffortRole.DataBind();


            for (int tableCellCount = 4; tableCellCount < dgPeriodEffortRole.HeaderRow.Cells.Count - 1; tableCellCount++)
            {
                if (tableCellCount == dgPeriodEffortRole.HeaderRow.Cells.Count - 2)
                {
                    TableCell tcBillable = dgPeriodEffortRole.HeaderRow.Cells[tableCellCount];
                    TableCell tcbudgeted = dgPeriodEffortRole.HeaderRow.Cells[tableCellCount + 1];

                    string newHeaderText = "<table width=100% cellspacing=0><tr><td colspan=2 align=center>Total</td></tr>";
                    newHeaderText += "<tr><td >Alloc.</td><td >Act.</td></tr></table>";
                    tcBillable.Text = newHeaderText;
                    tcBillable.ColumnSpan = 2;
                    dgPeriodEffortRole.HeaderRow.Cells.Remove(tcbudgeted);
                }
                else
                {
                    TableCell tcBillable = dgPeriodEffortRole.HeaderRow.Cells[tableCellCount];
                    TableCell tcbudgeted = dgPeriodEffortRole.HeaderRow.Cells[tableCellCount + 1];

                    string proj = tcBillable.Text.Remove(tcBillable.Text.IndexOf('_'));
                    string newHeaderText = "<table  width=100% cellspacing=0><tr style=border-right-color: #333333><td colspan=2 align=center>" + proj + "</td></tr>";
                    newHeaderText += "<tr><td >Alloc.</td><td >Act.</td></tr></table>";
                    tcBillable.Text = newHeaderText;
                    tcBillable.ColumnSpan = 2;
                    dgPeriodEffortRole.HeaderRow.Cells.Remove(tcbudgeted);
                }
            }
        }

        // Method to insert DashboardDetails...
        private void InsertDashboardDetails()
        {
            string uptoDate = string.Empty;

            dayFrom = Convert.ToDateTime(dayFrom).ToString("yyyy/MM/dd");
            dayTo = Convert.ToDateTime(dayTo).ToString("yyyy/MM/dd");

            uptoDate = dayFrom;
            List<SqlParameter> parameter = new List<SqlParameter>();

            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Name", fromName + "-" + toName));
            parameter.Add(new SqlParameter("@FromDate", dayFrom));
            parameter.Add(new SqlParameter("@ToDate", dayTo));
            parameter.Add(new SqlParameter("@PeriodType", ddlProjectPeriod.SelectedItem.Text == "Weekly" ? "W" : "M"));
            parameter.Add(new SqlParameter("@Status", "I")); //I:-Inprogress,F:-Finalized
            SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;
            parameter.Add(returnValue);
            PMOscar.BaseDAL.ExecuteSPNonQuery("DashboardOperations", parameter);
            int dashBoardId = Convert.ToInt16(returnValue.Value);
            DataTable dtDashBoard = PMOscar.BaseDAL.ExecuteDataTable("Select Status from Dashboard where DashboardID=" + dashBoardId);
                    
            if (dtDashBoard.Rows.Count > 0)
            {
                if (dtDashBoard.Rows[0]["Status"].ToString() == "F")
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Dashboard for the selected period has already been finalized."; 
                } 
                else
                {
                    DataTable dtDashBoardstatus = PMOscar.BaseDAL.ExecuteDataTable("Select * from ProjectDashboard where DashboardID=" + dashBoardId);
                    List<DataRow> list = new List<DataRow>();
                    foreach (DataRow dr in dtDashBoardstatus.Rows)
                    {
                        list.Add(dr);
                    }

                    PMOscar.BaseDAL.ExecuteNonQuery("Delete from dbo.ProjectDashboardEstimation where DashboardID=" + dashBoardId);
                    dtDashboard = new DataTable();
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@dayfrom", dayFrom));
                    parameter.Add(new SqlParameter("@dayto", dayTo));
                    parameter.Add(new SqlParameter("@uptodate", uptoDate));
                    parameter.Add(new SqlParameter("@tabType", "dashboard"));
                    parameter.Add(new SqlParameter("@Status", "Create"));
                    parameter.Add(new SqlParameter("@DashboardID", dashBoardId));
                    parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", true));
                    dtDashboard = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];
                    
                    dtEstimation = new DataTable();
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@dayfrom", dayFrom));
                    parameter.Add(new SqlParameter("@dayto", dayTo));
                    parameter.Add(new SqlParameter("@uptodate", uptoDate));
                    parameter.Add(new SqlParameter("@tabType", "Estimation"));
                    parameter.Add(new SqlParameter("@Status", "Create"));
                    parameter.Add(new SqlParameter("@DashboardID", "1"));
                    parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", true));
                    dtEstimation = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];

                    for (int dashBoardCount = 0; dashBoardCount < dtDashboard.Rows.Count; dashBoardCount++)
                    {
                       if (HiddenField1.Value == "R")
                       {
                           parameter = new List<SqlParameter>();
                           parameter.Add(new SqlParameter("@ProjectID", dtDashboard.Rows[dashBoardCount]["ProjectID"]));
                           parameter.Add(new SqlParameter("@PhaseID", dtDashboard.Rows[dashBoardCount]["PhaseID"]));
                           parameter.Add(new SqlParameter("@ClientStatus",3));
                           parameter.Add(new SqlParameter("@TimelineStatus",3));
                           parameter.Add(new SqlParameter("@BudgetStatus",3));
                           parameter.Add(new SqlParameter("@EscalateStatus",3));
                           parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
                           parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                           parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
                           parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                           parameter.Add(new SqlParameter("@Status", "I"));
                           parameter.Add(new SqlParameter("@DashboardID", dashBoardId));
                           parameter.Add(new SqlParameter("@ProjectName", dtDashboard.Rows[dashBoardCount]["ProjectName"]));
                           parameter.Add(new SqlParameter("@ShortName", dtDashboard.Rows[dashBoardCount]["ShortName"]));
                           parameter.Add(new SqlParameter("@ProjectType", dtDashboard.Rows[dashBoardCount]["ProjectType"]));
                           parameter.Add(new SqlParameter("@ProjectOwner", dtDashboard.Rows[dashBoardCount]["ProjectOwnerID"]));
                           parameter.Add(new SqlParameter("@ProjectManager", dtDashboard.Rows[dashBoardCount]["ProjectManagerID"]));
                           parameter.Add(new SqlParameter("@DeliveryDate", dtDashboard.Rows[dashBoardCount]["DeliveryDate"]));
                           parameter.Add(new SqlParameter("@RevisedDeliveryDate", dtDashboard.Rows[dashBoardCount]["RevisedDeliveryDate"]));
                           parameter.Add(new SqlParameter("@PMComments", dtDashboard.Rows[dashBoardCount]["PMComments"]));
                           parameter.Add(new SqlParameter("@POComments", dtDashboard.Rows[dashBoardCount]["POComments"]));
                           parameter.Add(new SqlParameter("@DeliveryComments", dtDashboard.Rows[dashBoardCount]["DeliveryComments"]));
                           parameter.Add(new SqlParameter("@Comments", ""));
                           parameter.Add(new SqlParameter("@isActive", 1));
                           parameter.Add(new SqlParameter("@Utilization", dtDashboard.Rows[dashBoardCount]["Utilization"]));
                           PMOscar.BaseDAL.ExecuteSPNonQuery("[ProjectDashboardOperations]", parameter);
                           
                           DataTable dtDashBoardstatusnew = PMOscar.BaseDAL.ExecuteDataTable("Select * from ProjectDashboard where DashboardID=" + dashBoardId);
                           foreach (var j in list)
                           {
                               if (j.ItemArray[1].ToString() == dtDashBoardstatusnew.Rows[dtDashBoardstatusnew.Rows.Count-1]["ProjectID"].ToString())
                               {
                                   if (j.ItemArray[3] != dtDashBoardstatusnew.Rows[dtDashBoardstatusnew.Rows.Count - 1]["ClientStatus"] || j.ItemArray[4].ToString() != dtDashBoardstatusnew.Rows[dtDashBoardstatusnew.Rows.Count - 1]["TimelineStatus"].ToString() || j.ItemArray[5].ToString() != dtDashBoardstatusnew.Rows[dtDashBoardstatusnew.Rows.Count - 1]["BudgetStatus"].ToString() || j.ItemArray[6].ToString() != dtDashBoardstatusnew.Rows[dtDashBoardstatusnew.Rows.Count - 1]["EscalateStatus"].ToString())
                                   {
                                       int dashboardId = Convert.ToInt32(dtDashBoardstatusnew.Rows[dtDashBoardstatusnew.Rows.Count - 1]["ProjectDashboardID"].ToString());
                                       int clientStatus = Convert.ToInt32(j.ItemArray[3]);
                                       int timelineStatus = Convert.ToInt32(j.ItemArray[4]);
                                       int budgetStatus = Convert.ToInt32(j.ItemArray[5]);
                                       int escalateStatus = Convert.ToInt32(j.ItemArray[6]);
                                       string weeklyComment = j.ItemArray[11].ToString();

                                       // Update projectdashboard status and weekly comment
                                       UpdateProjectDashboardStatus(dashboardId, clientStatus,timelineStatus,budgetStatus,escalateStatus,weeklyComment);
                                   } 
                                }
                                 
                            } 
                        }
                       else
                        {
                            parameter = new List<SqlParameter>();
                            parameter.Add(new SqlParameter("@ProjectID", dtDashboard.Rows[dashBoardCount]["ProjectID"]));
                            parameter.Add(new SqlParameter("@PhaseID", dtDashboard.Rows[dashBoardCount]["PhaseID"]));
                            parameter.Add(new SqlParameter("@ClientStatus", 3));
                            parameter.Add(new SqlParameter("@TimelineStatus", 3));
                            parameter.Add(new SqlParameter("@BudgetStatus", 3));
                            parameter.Add(new SqlParameter("@EscalateStatus", 3));
                            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@Status", "I"));
                            parameter.Add(new SqlParameter("@DashboardID", dashBoardId));
                            parameter.Add(new SqlParameter("@ProjectName", dtDashboard.Rows[dashBoardCount]["ProjectName"]));
                            parameter.Add(new SqlParameter("@ShortName", dtDashboard.Rows[dashBoardCount]["ShortName"]));
                            parameter.Add(new SqlParameter("@ProjectType", dtDashboard.Rows[dashBoardCount]["ProjectType"]));
                            parameter.Add(new SqlParameter("@ProjectOwner", dtDashboard.Rows[dashBoardCount]["ProjectOwnerID"]));
                            parameter.Add(new SqlParameter("@ProjectManager", dtDashboard.Rows[dashBoardCount]["ProjectManagerID"]));
                            parameter.Add(new SqlParameter("@DeliveryDate", dtDashboard.Rows[dashBoardCount]["DeliveryDate"]));
                            parameter.Add(new SqlParameter("@RevisedDeliveryDate", dtDashboard.Rows[dashBoardCount]["RevisedDeliveryDate"]));
                            parameter.Add(new SqlParameter("@PMComments", dtDashboard.Rows[dashBoardCount]["PMComments"]));
                            parameter.Add(new SqlParameter("@POComments", dtDashboard.Rows[dashBoardCount]["POComments"]));
                            parameter.Add(new SqlParameter("@DeliveryComments", dtDashboard.Rows[dashBoardCount]["DeliveryComments"]));
                            parameter.Add(new SqlParameter("@Comments", ""));
                            parameter.Add(new SqlParameter("@isActive", 1));
                            parameter.Add(new SqlParameter("@Utilization", dtDashboard.Rows[dashBoardCount]["Utilization"]));
                            PMOscar.BaseDAL.ExecuteSPNonQuery("[ProjectDashboardOperations]", parameter);
                         }
                    }

                    for (int estimationCount = 0; estimationCount < dtEstimation.Rows.Count; estimationCount++)
                    {
                        parameter = new List<SqlParameter>();
                        parameter.Add(new SqlParameter("@DashboardID", dashBoardId));
                        parameter.Add(new SqlParameter("@ProjectID", dtEstimation.Rows[estimationCount]["ProjectID"]));
                        parameter.Add(new SqlParameter("@PhaseID", dtEstimation.Rows[estimationCount]["PhaseID"]));
                        parameter.Add(new SqlParameter("@EstimationRoleID", dtEstimation.Rows[estimationCount]["EstimationRoleID"]));
                        parameter.Add(new SqlParameter("@BillableHours", dtEstimation.Rows[estimationCount]["BillableHours"]));
                        parameter.Add(new SqlParameter("@BudgetHours", dtEstimation.Rows[estimationCount]["BudgetHours"]));
                        parameter.Add(new SqlParameter("@RevisedBudgetHours", dtEstimation.Rows[estimationCount]["RevisedBudgetHours"]));
                        parameter.Add(new SqlParameter("@ActualHrs", dtEstimation.Rows[estimationCount]["ActualHours"]));
                        parameter.Add(new SqlParameter("@PeriodEstimetedHrs", dtEstimation.Rows[estimationCount]["periodEstimatedHours"]));
                        parameter.Add(new SqlParameter("@PeriodActualHrs", dtEstimation.Rows[estimationCount]["PeriodActualHours"]));
                        parameter.Add(new SqlParameter("@PeriodEstimetedHrsAdj", Convert.ToDouble(dtEstimation.Rows[estimationCount]["periodEstimatedHours"])));
                        parameter.Add(new SqlParameter("@PeriodActualHrsAdj", Convert.ToDouble(dtEstimation.Rows[estimationCount]["PeriodActualHours"])));
                        parameter.Add(new SqlParameter("@EstRoleName", dtEstimation.Rows[estimationCount]["EstRoleName"]));
                        parameter.Add(new SqlParameter("@EstRoleShrtName", dtEstimation.Rows[estimationCount]["EstRoleshortName"]));
                        parameter.Add(new SqlParameter("@RoleName", dtEstimation.Rows[estimationCount]["Role"]));
                        parameter.Add(new SqlParameter("@RoleShrtName", dtEstimation.Rows[estimationCount]["ShortName"]));
                        parameter.Add(new SqlParameter("@ActualHrsCorrected", Convert.ToDouble(dtEstimation.Rows[estimationCount]["ActualHours"])));
                        parameter.Add(new SqlParameter("@RevisedComments", Convert.ToString(dtEstimation.Rows[estimationCount]["RevisedComments"])));
                        PMOscar.BaseDAL.ExecuteSPNonQuery("[ProjectDashboardEstimationOperations]", parameter);
                    }
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = PMOscar.Core.Constants.AddRole.RELOADDASHBOARD;
                }
            }
            dtDashBoard.Dispose();
        }

        // Method to apply sort functionality...
        private string GetSortDirection(string column)
        {


            //LosFormatter objFormatter = new LosFormatter();
            //object obj = objFormatter.Deserialize("/wEPDwUKMTY1NDU2MTA1Mg8WAh4IVXNlck5hbWUFA3JhbWRkDvYzPkcPmjxL5DxLYraiJbrD7wqe9bYTDBb3lW5yxhQ=");
            
            
            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;




            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                        sortDirection = "DESC";
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        /// <summary>
        /// Method to update projectdashboard status and weekly comments
        /// </summary>
        /// <param name="dashboardId">The dashboard id.</param>
        /// <param name="clientStatus">The client status.</param>
        /// <param name="timeLineStatus">The time line status.</param>
        /// <param name="budgetStatus">The budget status.</param>
        /// <param name="escalateStatus">The escalate status.</param>
        /// <param name="weeklyComment">The weekly comment.</param>
        private void UpdateProjectDashboardStatus(int dashboardId, int clientStatus, int timeLineStatus, int budgetStatus, int escalateStatus, string weeklyComment)
        {
            try
            {
                // Note: SP 'ProjectDashboardOperations' only updates ClientStatus, TimeLineStatus, BudgetStatus, EscalateStatus and WeeklyComments values for Status='U' and takes 'DashboardID' as parameter. Other values given below are dummy values.

                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ProjectID", 1));
                parameter.Add(new SqlParameter("@PhaseID", 1));
                parameter.Add(new SqlParameter("@ClientStatus", clientStatus));
                parameter.Add(new SqlParameter("@TimelineStatus", timeLineStatus));
                parameter.Add(new SqlParameter("@BudgetStatus", budgetStatus));
                parameter.Add(new SqlParameter("@EscalateStatus", escalateStatus));
                parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@Status", "U"));
                parameter.Add(new SqlParameter("@DashboardID", dashboardId));
                parameter.Add(new SqlParameter("@ProjectName", ""));
                parameter.Add(new SqlParameter("@ShortName", ""));
                parameter.Add(new SqlParameter("@ProjectType", ""));
                parameter.Add(new SqlParameter("@ProjectOwner", 1));
                parameter.Add(new SqlParameter("@ProjectManager", 1));
                parameter.Add(new SqlParameter("@DeliveryDate", DateTime.Now.Date));
                parameter.Add(new SqlParameter("@RevisedDeliveryDate", DateTime.Now.Date));
                parameter.Add(new SqlParameter("@PMComments", ""));
                parameter.Add(new SqlParameter("@DeliveryComments", ""));
                parameter.Add(new SqlParameter("@POComments", ""));
                parameter.Add(new SqlParameter("@IsActive", 1));
                parameter.Add(new SqlParameter("@Comments", weeklyComment.Trim()));

                // Return value as parameter
                SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
                returnValue.Direction = ParameterDirection.ReturnValue;
                parameter.Add(returnValue);
                
                PMOscar.BaseDAL.ExecuteSPScalar("ProjectDashboardOperations", parameter);
            }
            catch(Exception ex)
            {
                //ToDo: Implement error log
            }

        }

        #region Export To Excel
        /*
        public static void CreateWorkBook()
        {
            string attachment = "attachment; filename=\"Test.xml\"";

            HttpContext.Current.Response.ClearContent();

            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            System.IO.StringWriter sw = new System.IO.StringWriter();
            sw.WriteLine("<?xml version=\"1.0\"?>");

            sw.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
            sw.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");

            sw.WriteLine("xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            sw.WriteLine("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");

            sw.WriteLine("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sw.WriteLine("xmlns:html=\"http://www.w3.org/TR/REC-html40\">");

            sw.WriteLine("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            sw.WriteLine("<LastAuthor>Try Not Catch</LastAuthor>");

            sw.WriteLine("<Created>2010-05-15T19:14:19Z</Created>");
            sw.WriteLine("<Version>11.9999</Version>");

            sw.WriteLine("</DocumentProperties>");
            sw.WriteLine("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">");

            sw.WriteLine("<WindowHeight>9210</WindowHeight>");
            sw.WriteLine("<WindowWidth>19035</WindowWidth>");

            sw.WriteLine("<WindowTopX>0</WindowTopX>");
            sw.WriteLine("<WindowTopY>90</WindowTopY>");

            sw.WriteLine("<ProtectStructure>False</ProtectStructure>");
            sw.WriteLine("<ProtectWindows>False</ProtectWindows>");

            sw.WriteLine("</ExcelWorkbook>");
            sw.WriteLine("<Styles>");

            sw.WriteLine("<Style ss:ID=\"Default\" ss:Name=\"Normal\">");
            sw.WriteLine("<Alignment ss:Vertical=\"Bottom\"/>");

            sw.WriteLine("<Borders/>");
            sw.WriteLine("<Font/>");

            sw.WriteLine("<Interior/>");
            sw.WriteLine("<NumberFormat/>");

            sw.WriteLine("<Protection/>");
            sw.WriteLine("</Style>");

            sw.WriteLine("<Style ss:ID=\"s22\">");
            sw.WriteLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>");

            sw.WriteLine("<Borders>");
            sw.WriteLine("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");

            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");

            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");

            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");

            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("</Borders>");

            sw.WriteLine("<Font ss:Bold=\"1\"/>");
            sw.WriteLine("</Style>");

            sw.WriteLine("<Style ss:ID=\"s23\">");
            sw.WriteLine("<Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");

            sw.WriteLine("<Borders>");
            sw.WriteLine("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");

            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");

            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");

            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");

            sw.WriteLine("ss:Color=\"#000000\"/>");
            sw.WriteLine("</Borders>");

            sw.WriteLine("</Style>");
            sw.WriteLine("<Style ss:ID=\"s24\">");

            sw.WriteLine("<Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
            sw.WriteLine("<Borders>");

            sw.WriteLine("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");

            sw.WriteLine("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");

            sw.WriteLine("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");

            sw.WriteLine("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"");
            sw.WriteLine("ss:Color=\"#000000\"/>");

            sw.WriteLine("</Borders>");
            sw.WriteLine("<Font ss:Color=\"#FFFFFF\"/>");

            sw.WriteLine("<Interior ss:Color=\"#FF6A6A\" ss:Pattern=\"Solid\"/>");
            //set header colour here
            sw.WriteLine("</Style>");

            sw.WriteLine("</Styles>");
            //foreach (GridView gView in cList)
            //{
            //    CreateWorkSheet(gView.ID.ToString, sw, gView, CellWidth);
            //}


            sw.WriteLine("<Worksheet ss:Name=\" Test \">");
            sw.WriteLine("<Table ss:ExpandedColumnCount=\"" + 5 + "\" ss:ExpandedRowCount=\"" + 3 + "\" x:FullColumns=\"1\"");
            sw.WriteLine("x:FullRows=\"1\">");

            //Row
            sw.WriteLine("<Row>");

            for (int col = 0; col < 5; col++ )
            {
                sw.WriteLine("<Cell ss:StyleID=\"s24\"><Data ss:Type=\"String\">" + col + "</Data></Cell>");
            }
            sw.WriteLine("</Row>");
            
            sw.WriteLine("</Table>");
            sw.WriteLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            sw.WriteLine("<Selected/>");
            sw.WriteLine("<DoNotDisplayGridlines/>");
            sw.WriteLine("<ProtectObjects>False</ProtectObjects>");
            sw.WriteLine("<ProtectScenarios>False</ProtectScenarios>");
            sw.WriteLine("</WorksheetOptions>");
            sw.WriteLine("</Worksheet>");
            sw.WriteLine("</Workbook>");
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();

        }

        
        private static void CreateWorkSheet(string wsName, System.IO.StringWriter sw, GridView gv, int cellwidth)
        {
            if ((gv.HeaderRow == null) == false)
            {
                sw.WriteLine("<Worksheet ss:Name=\"" + wsName + "\">");
                int cCount = gv.HeaderRow.Cells.Count;
                long rCount = gv.Rows.Count + 1;
                sw.WriteLine("<Table ss:ExpandedColumnCount=\"" + cCount + "\" ss:ExpandedRowCount=\"" + rCount + "\" x:FullColumns=\"1\"");
                sw.WriteLine("x:FullRows=\"1\">");
                for (int i = (cCount - cCount); i <= (cCount - 1); i++)
                {
                    sw.WriteLine("<Column ss:AutoFitWidth=\"1\" ss:Width=\"" + cellwidth + "\"/>");
                }

                GridRowIterate(gv, sw);
                sw.WriteLine("</Table>");
                sw.WriteLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");

                sw.WriteLine("<Selected/>");
                sw.WriteLine("<DoNotDisplayGridlines/>");

                sw.WriteLine("<ProtectObjects>False</ProtectObjects>");
                sw.WriteLine("<ProtectScenarios>False</ProtectScenarios>");

                sw.WriteLine("</WorksheetOptions>");
                sw.WriteLine("</Worksheet>");
            }
        }
        private static void GridRowIterate(GridView gv, System.IO.StringWriter sw)
        {
            sw.WriteLine("<Row>");

            foreach (TableCell tc in gv.HeaderRow.Cells)
            {
                string tcText = tc.Text;

                string tcWidth = gv.Width.Value;
                string dType = "String";


                if (Information.IsNumeric(tcText) == true)
                {
                    dType = "Number";

                }
                sw.WriteLine("<Cell ss:StyleID=\"s24\"><Data ss:Type=\"String\">" + tcText + "</Data></Cell>");

            }
            sw.WriteLine("</Row>");

            foreach (GridViewRow gr in gv.Rows)
            {
                sw.WriteLine("<Row>");

                foreach (TableCell gc in gr.Cells)
                {
                    string gcText = gc.Text;
                    string dType = "String";


                    if (Information.IsNumeric(gcText) == true)
                    {
                        dType = "Number";
                        gcText = Convert.ToDouble(gcText);

                    }
                    sw.WriteLine("<Cell ss:StyleID=\"s23\"><Data ss:Type=\"" + dType + "\">" + gcText + "</Data></Cell>");

                }
                sw.WriteLine("</Row>");
            }

        }
        */
        #endregion

        #endregion

        #region"Control Events"

        protected void dgOverView_RowDataBound(object sender, GridViewRowEventArgs e)
        {  
            e.Row.Cells[20].Visible = false; //sort order
            e.Row.Cells[21].Visible = false; // hide estimate column
                

            if (e.Row.RowType == DataControlRowType.Header)
            {
                 e.Row.Cells[14].Text = "PO Comments";
                 e.Row.Cells[15].Text = "PM Comments";
                 e.Row.Cells[16].Text = "Delivery Comments";
                 e.Row.Cells[17].Text = "Weekly Comments";
                 
                 e.Row.Cells[1].Attributes.Add("Style", "Color:red;");
                 e.Row.Cells[1].ForeColor = System.Drawing.Color.FromName("#fcff00"); //Utilization
                
                 e.Row.Cells[5].ForeColor = System.Drawing.Color.FromName("#fcff00");
                 e.Row.Cells[10].ForeColor = System.Drawing.Color.FromName("#fcff00");   
                
                 e.Row.Cells[6].ForeColor = System.Drawing.Color.FromName("#fcff00");  //Project Type 
                           
                 e.Row.Cells[11].ForeColor = System.Drawing.Color.FromName("#fcff00"); //project Owner
                 e.Row.Cells[12].ForeColor = System.Drawing.Color.FromName("#fcff00"); //Project Manager
                 e.Row.Cells[13].ForeColor = System.Drawing.Color.FromName("#fcff00");  //Current Phase
            } 
            if (Convert.ToInt32(Session["UserRoleID"]) == 2)           //need confirmation about this column (when project manager login)
                e.Row.Cells[14].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.HyperLink hlImg = new System.Web.UI.WebControls.HyperLink();
                if (e.Row.Cells[7].Text == "3")
                {
                    hlImg.ImageUrl = "../images/clientgreen.png";
                    hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashboardId=" + ddlReports.SelectedValue + "&ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                }
                else if (e.Row.Cells[7].Text == "1")
                {
                    hlImg.ImageUrl = "../images/clientred.png";
                    hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashboardId=" + ddlReports.SelectedValue + "&ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                }
                else if (e.Row.Cells[7].Text == "2")
                {
                    hlImg.ImageUrl = "../images/clientyellow.png";
                    hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashboardId=" + ddlReports.SelectedValue + "&ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                }

                e.Row.Cells[7].Controls.Add(hlImg);
                e.Row.Cells[7].Attributes.Add("align", "Center");

                hlImg = new System.Web.UI.WebControls.HyperLink();
                if (e.Row.Cells[8].Text == "3")
                {
                    hlImg.ImageUrl = "../images/timelinegreen.png";
                    hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashboardId=" + ddlReports.SelectedValue + "&ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;

                }
                else if (e.Row.Cells[8].Text == "1")
                {
                    hlImg.ImageUrl = "../images/timelinered.png";
                    hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashboardId=" + ddlReports.SelectedValue + "&ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                }
                else if (e.Row.Cells[8].Text == "2")
                {
                    hlImg.ImageUrl = "../images/timelineyellow.png";
                    hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashboardId=" + ddlReports.SelectedValue + "&ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                }
                e.Row.Cells[8].Controls.Add(hlImg);
                e.Row.Cells[8].Attributes.Add("align", "Center");
                hlImg = new System.Web.UI.WebControls.HyperLink();
             
                if (e.Row.Cells[9].Text == "3")
                {
                    hlImg.ImageUrl = "../images/budgetgreen.png";
                    //hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                    hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashboardId=" + ddlReports.SelectedValue + "&ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                }
                else if (e.Row.Cells[9].Text == "1")
                {
                    hlImg.ImageUrl = "../images/budgetred.png";
                    //hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                    hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashboardId=" + ddlReports.SelectedValue + "&ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                }
                else if (e.Row.Cells[9].Text == "2")
                {
                    hlImg.ImageUrl = "../images/budgetyellow.png";
                   // hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                    hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashboardId=" + ddlReports.SelectedValue + "&ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                }
                
                e.Row.Cells[9].Controls.Add(hlImg);
                e.Row.Cells[9].Attributes.Add("align", "Center");

                hlImg = new System.Web.UI.WebControls.HyperLink();
                if (e.Row.Cells[10].Text == "3")
                {
                    hlImg.ImageUrl = "../images/escalategreen.png";
                    //hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;   
                    hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashboardId=" + ddlReports.SelectedValue + "&ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                }
                else if (e.Row.Cells[10].Text == "1")
                {
                    hlImg.ImageUrl = "../images/escalatered.png";
                    //hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;   
                    hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashboardId=" + ddlReports.SelectedValue + "&ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                }
                else if (e.Row.Cells[10].Text == "2")
                {
                    hlImg.ImageUrl = "../images/escalateyellow.png";
                    //hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;   
                    hlImg.NavigateUrl = "ProjectDashboardEntry.aspx?ProjectDashboardId=" + ddlReports.SelectedValue + "&ProjectDashEditId=" + e.Row.Cells[2].Text + "&projectID=" + e.Row.Cells[0].Text + "&projectName=" + e.Row.Cells[4].Text;
                }
                e.Row.Cells[10].Controls.Add(hlImg);
                e.Row.Cells[10].Attributes.Add("align", "Center");

                HyperLink ActLink13 = new HyperLink();
                ActLink13.Text = Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[14].Text.ToString())).Length > 19 ? Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[14].Text)).Substring(0, 20) + "..." : e.Row.Cells[14].Text == "&nbsp;" ? "" : Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[14].Text));
                e.Row.Cells[14].Controls.Add(ActLink13);

                if (e.Row.Cells[14].Text != "&nbsp;")
                {
                    e.Row.Cells[14].ToolTip = Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[14].Text)).Trim();
                    if (e.Row.Cells[14].Text.ToString() != "")                    
                        ActLink13.Attributes.Add("onclick", "showPopup('" + Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[14].Text)).Replace("\r\n", "<br/>").Replace("'", "\"\"").Trim() + "', event);return false;");
                }

                HyperLink ActLink14 = new HyperLink();
                ActLink14.Text = Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[15].Text)).ToString().Length > 19 ? Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[15].Text)).Substring(0, 20) + "..." : e.Row.Cells[15].Text == "&nbsp;" ? "" : Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[15].Text));
                e.Row.Cells[15].Controls.Add(ActLink14);   

                if (e.Row.Cells[15].Text != "&nbsp;")
                {
                    e.Row.Cells[15].ToolTip = Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[15].Text)).Trim();
                    if (e.Row.Cells[15].Text.ToString() != "")
                    {

                        ActLink14.Attributes.Add("onclick", "showPopup('" + Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[15].Text)).Replace("\r\n", "<br/>").Replace("'", "\"\"").Trim() + "', event);return false;");
                    }
                }
           
                //   e.Row.Cells[14].Text = e.Row.Cells[14].Text.ToString().Length > 19 ? e.Row.Cells[14].Text.Substring(0, 20) + "..." : e.Row.Cells[14].Text == "&nbsp;" ? "" : e.Row.Cells[14].Text;

               
                HyperLink ActLink15 = new HyperLink(); 
               // ActLink15.Text = e.Row.Cells[15].Text;
                ActLink15.Text = Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[16].Text)).ToString().Length > 19 ? Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[16].Text)).Substring(0, 20) + "..." : e.Row.Cells[16].Text == "&nbsp;" ? "" : Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[16].Text));
                e.Row.Cells[16].Controls.Add(ActLink15);                 

                   if (e.Row.Cells[16].Text != "&nbsp;")
                   {
                       e.Row.Cells[16].ToolTip = Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[16].Text.Trim()));
                       if (e.Row.Cells[16].Text.ToString() != "")
                       {

                           ActLink15.Attributes.Add("onclick", "showPopup('" + Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[16].Text)).Replace("\r\n", "<br/>").Replace("'", "\"\"").Trim() + "', event);return false;");
                           
                       }                                            
                   }

                 //e.Row.Cells[15].Text = e.Row.Cells[15].Text.ToString().Length > 19 ? e.Row.Cells[15].Text.Substring(0, 20) + "..." : e.Row.Cells[15].Text == "&nbsp;" ? "" : e.Row.Cells[15].Text;


                   HyperLink ActLink16 = new HyperLink();
                   //ActLink16.Text = e.Row.Cells[16].Text;
                   ActLink16.Text = Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[17].Text)).ToString().Length > 19 ? Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[17].Text)).Substring(0, 20) + "..." : e.Row.Cells[17].Text == "&nbsp;" ? "" : Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[17].Text));
                   e.Row.Cells[17].Controls.Add(ActLink16);

                   if (e.Row.Cells[17].Text != "&nbsp;")
                   {
                       e.Row.Cells[17].ToolTip = Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[17].Text.Trim()));
                       if (e.Row.Cells[17].Text.ToString() != "")
                       {

                           ActLink16.Attributes.Add("onclick", "showPopup('" + Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[17].Text)).Replace("\r\n", "<br/>").Replace("'", "\"\"").Trim() + "', event);return false;");

                       }
                   }
                                
                                  
                   //code to highlight projects with 0 actual and alloc
                   if ((e.Row.Cells[20].Text == "0.0000") &&(e.Row.Cells[21].Text == "0.0000"))
                   {
                         e.Row.BackColor = System.Drawing.Color.FromName("#DDDDDD");
                    }
                   

            }
             
        }

        protected void dgOverView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)            
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[18].Visible = false; // Hide BudgetStatus column
                e.Row.Cells[19].Visible = false;//Hide Actualhrs column
            }

            e.Row.Cells[14].Width = 160;
            e.Row.Cells[15].Width = 160;
            e.Row.Cells[16].Width = 160;
            e.Row.Cells[17].Width = 160;
            e.Row.Cells[1].Width = 175;
        }

        protected void dgProjectEffortPhase_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            }
            int cellCount = e.Row.Cells.Count;
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells[3].Visible = false;

                e.Row.Cells[cellCount - 1].Visible = false;
                e.Row.Cells[cellCount - 2].Visible = false;
            }
            e.Row.Cells[1].Width = 175;
        }

        protected void dgProjectEffortPhase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 1)
                e.Row.Cells[2].Visible = false;

            int cellCount = e.Row.Cells.Count;           
            e.Row.Cells[cellCount -3].Font.Bold = true;
            e.Row.Cells[cellCount - 4].Font.Bold = true;
            e.Row.Cells[cellCount - 5].Font.Bold = true;
            e.Row.Cells[cellCount - 6].Font.Bold = true;

            if ( e.Row.RowType == DataControlRowType.DataRow )
            {
                for (int cellsCount = 3; cellsCount < cellCount; cellsCount++)
                {
                    e.Row.Cells[cellsCount].Width = 50;
                    e.Row.Cells[cellsCount].Attributes.Add("Style", "text-align:right;padding-right:5px;");
                    
                    //Highlight the project whose actual hours overan the revised hours                   
                    if (cellsCount == cellCount - 2)
                    {
                        if (e.Row.Cells[2].Text != "Total")
                        {
                            string actualValue = e.Row.Cells[1].Text == "&nbsp;" ? "" : e.Row.Cells[1].Text;
                            string revValue = e.Row.Cells[2].Text == "&nbsp;" ? "" : e.Row.Cells[2].Text;
                            if (e.Row.Cells[cellsCount - 1].Text != "&nbsp;" && e.Row.Cells[cellsCount - 2].Text != "&nbsp;")
                            {
                               // if(actualValue!=string.Empty&&revValue!=string.Empty)
                              //  {
                                //Double actual = Convert.ToDouble(e.Row.Cells[cellsCount - 1].Text);
                                //Double rev = Convert.ToDouble(e.Row.Cells[cellsCount - 2].Text);                     
                                    decimal actual = Convert.ToDecimal(e.Row.Cells[cellsCount - 1].Text);
                                    int rev = Convert.ToInt32(e.Row.Cells[cellsCount - 2].Text);

                                if (actual > rev)
                                {
                                    e.Row.Cells[cellsCount - 1].ForeColor = System.Drawing.Color.FromName("#FF4D4D");
                                    e.Row.Cells[cellsCount - 1].Attributes.Add("style", "border-color: black;text-align: right;padding-right:5px");
                                    e.Row.Cells[cellsCount - 2].ForeColor = System.Drawing.Color.FromName("#FF4D4D");
                                    e.Row.Cells[cellsCount - 2].Attributes.Add("style", "border-color: black;text-align: right;padding-right:5px");

                                }
                                }
                           // }
                        }
                        //code to highlight projects with 0 actual and alloc     
                        if ((e.Row.Cells[cellsCount].Text == "0") && (e.Row.Cells[cellsCount + 1].Text == "0"))
                        {

                            e.Row.BackColor = System.Drawing.Color.FromName("#DDDDDD");
                        }
                    }

                }
                string rowtotal = DataBinder.Eval(e.Row.DataItem, "ProjectName") == DBNull.Value ? "" : (String)DataBinder.Eval(e.Row.DataItem, "ProjectName");

                if (rowtotal == "Total")
                {
                    e.Row.Font.Bold = true;
                    e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "Total";
                }
            }
        }

        protected void cbProposalAndVas_CheckedChanged(object sender, EventArgs e)
        { 
            Session["HdnChkIncludeProposalVAS"] = cbProposalAndVas.Checked.ToString(); 
            if (ddlReports.SelectedItem.Text != "Select")
            {
                FillDashBoardWeek(Convert.ToInt64(ddlReports.SelectedItem.Value));
                DevelopingProjectDashBoardDetails();
            }
            else
                BindProjectDashBoardDetailsGrid();

            lblMsg.Text = string.Empty;
        }

        protected void ddlReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;

            if (ddlReports.SelectedIndex == 1)
            {
                DataTable dtDashBoardFinalize = PMOscar.BaseDAL.ExecuteDataTable("Select Status from Dashboard where DashboardID=" + ddlReports.SelectedItem.Value);
                if (dtDashBoardFinalize.Rows.Count > 0)
                {
                    if (dtDashBoardFinalize.Rows[0]["Status"].ToString() == "F")
                    {
                        btnRefresh.Enabled = false;
                        btnFinalize.Enabled = false;
                        //btnRefreshDropDownList.Enabled = false;
                    }
                    else
                    {
                        btnRefresh.Enabled = true;
                        btnFinalize.Enabled = true;
                        //btnRefreshDropDownList.Enabled = true;
                    }
                }
                ///btnRefresh.Enabled = true;
            }           

            if (ddlReports.SelectedItem.Text != "Select")
            {
                FillDashBoardWeek(Convert.ToInt64(ddlReports.SelectedItem.Value));
                DevelopingProjectDashBoardDetails();
            }
            else            
                BindProjectDashBoardDetailsGrid();

            lblMsg.Text = string.Empty;
        }

        protected void dgPeriodEffortPhase_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if ( e.Row.RowType == DataControlRowType.DataRow )            
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
                      
            e.Row.Cells[1].Width = 175;                     
        }

        protected void dgPeriodEffortPhase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ( e.Row.Cells.Count >= 1 )
            e.Row.Cells[2].Visible = false;

            e.Row.Cells[3].Visible = false;

            int cellCount = e.Row.Cells.Count;
            e.Row.Cells[cellCount - 1].Font.Bold = true;
            e.Row.Cells[cellCount - 2].Font.Bold = true;

            if ( e.Row.RowType == DataControlRowType.Header )            
                e.Row.Cells[1].Text = "Project Name"; 
           
            if ( e.Row.RowType == DataControlRowType.DataRow )
            {
                               
                for ( int CellsCount = 5; CellsCount < cellCount; CellsCount++)
                {
                    e.Row.Cells[CellsCount].Width = 75;
                    e.Row.Cells[CellsCount].Attributes.Add("Style", "text-align:right;padding-right:5px;");
                    //code to highlight projects with 0 actual and alloc
                    if (CellsCount == cellCount - 2)
                        
                    {
                        if ((e.Row.Cells[CellsCount].Text == "0") && (e.Row.Cells[CellsCount + 1].Text == "0"))
                        {

                            e.Row.BackColor = System.Drawing.Color.FromName("#DDDDDD");

                        }
                    }
                }

                string rowTotal = DataBinder.Eval(e.Row.DataItem, "ProjectName") == DBNull.Value ? "" : (String)DataBinder.Eval(e.Row.DataItem, "ProjectName");

                if ( rowTotal == "Total" )
                {
                    e.Row.Font.Bold = true;
                    e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "Total";                    
                }
            }
        }

        protected void dgProjectEffortRole_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if ( e.Row.RowType == DataControlRowType.DataRow )            
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            int cellCount = e.Row.Cells.Count;
            if ( e.Row.Cells.Count > 1 )
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[cellCount - 1].Visible = false;
                e.Row.Cells[cellCount - 2].Visible = false;

            }
            e.Row.Cells[1].Width = 175;
        }  

        protected void dgProjectEffortRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ( e.Row.Cells.Count >= 1 )
                e.Row.Cells[2].Visible = false;

            int cellCount = e.Row.Cells.Count;
            e.Row.Cells[cellCount - 3].Font.Bold = true;
            e.Row.Cells[cellCount - 4].Font.Bold = true;

            if ( e.Row.RowType == DataControlRowType.Header )            
                e.Row.Cells[1].Text = "Project Name";

            if ( e.Row.RowType == DataControlRowType.DataRow )
            {
                for (int cellsCount = 4; cellsCount < cellCount; cellsCount++)
                {
                    e.Row.Cells[cellsCount].Width = 50;
                    e.Row.Cells[cellsCount].Attributes.Add("Style", "text-align:right;padding-right:5px;");

                   //code to highlight projects whose actual overran the revised.
                    if (cellsCount == cellCount - 2)
                    {
                        string actualValue = e.Row.Cells[cellsCount - 1].Text == "&nbsp;" ? "" : e.Row.Cells[cellsCount - 1].Text;
                        string revValue = e.Row.Cells[cellsCount - 2].Text == "&nbsp;" ? "" : e.Row.Cells[cellsCount - 2].Text;
                        if (e.Row.Cells[2].Text != "Total")
                        {
                            //if (actualValue != string.Empty && revValue != string.Empty)
                           // {
                                //Double actual = Convert.ToDouble(actualValue);
                                //Double rev = Convert.ToDouble(revValue);
                                decimal actual = Convert.ToDecimal(e.Row.Cells[cellsCount - 1].Text);
                                int rev = Convert.ToInt32(e.Row.Cells[cellsCount - 2].Text);

                                if (actual > rev)
                                {

                                    e.Row.Cells[cellsCount - 1].ForeColor = System.Drawing.Color.FromName("#FF4D4D");
                                    e.Row.Cells[cellsCount - 1].Attributes.Add("style", "border-color: black;text-align: right;padding-right:5px");
                                    e.Row.Cells[cellsCount - 2].ForeColor = System.Drawing.Color.FromName("#FF4D4D");
                                    e.Row.Cells[cellsCount - 2].Attributes.Add("style", "border-color: black;text-align: right;padding-right:5px");

                                }
                           // }
                            //code to highlight projects with 0 actual and alloc
                        }
                        if ((e.Row.Cells[cellsCount].Text == "0") && (e.Row.Cells[cellsCount+1].Text == "0"))
                        {
                           e.Row.BackColor = System.Drawing.Color.FromName("#DDDDDD");
                         }
                    }
                }
                string rowTotal = DataBinder.Eval(e.Row.DataItem, "ProjectName") == DBNull.Value ? "" : (String)DataBinder.Eval(e.Row.DataItem, "ProjectName");

                if ( rowTotal == "Total" )
                {
                    e.Row.Font.Bold = true;
                    e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "Total";
                }
            }
        }

        protected void dgPeriodEffortRole_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if ( e.Row.RowType == DataControlRowType.DataRow )
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            
            if ( e.Row.Cells.Count > 1 )
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
            e.Row.Cells[1].Width = 175;
        }

        protected void dgPeriodEffortRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ( e.Row.Cells.Count >= 1 )
                e.Row.Cells[2].Visible = false;

            int cellCount = e.Row.Cells.Count;
            e.Row.Cells[cellCount - 1].Font.Bold = true;
            e.Row.Cells[cellCount - 2].Font.Bold = true;

            if (e.Row.RowType == DataControlRowType.Header)            
                e.Row.Cells[1].Text = "Project Name";           

            if (e.Row.RowType == DataControlRowType.DataRow)
            {               
                for (int cellsCount = 4; cellsCount < cellCount; cellsCount++)
                {
                    e.Row.Cells[cellsCount].Width = 50;
                    e.Row.Cells[cellsCount].Attributes.Add("Style", "text-align:right;padding-right:5px;");

                    //code to highlight projects with 0 actual and alloc
                    if (cellsCount == cellCount - 2)
                    {
                        if ((e.Row.Cells[cellsCount].Text == "0") && (e.Row.Cells[cellsCount + 1].Text == "0"))
                        {
                            e.Row.BackColor = System.Drawing.Color.FromName("#DDDDDD");
                            e.Row.Cells[cellsCount].Width = 60;
                        }
                    }
                }
                string rowTotal = DataBinder.Eval(e.Row.DataItem, "ProjectName") == DBNull.Value ? "" : (String)DataBinder.Eval(e.Row.DataItem, "ProjectName");

                if (rowTotal == "Total")
                {
                    e.Row.Font.Bold = true;
                    e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "Total";
                }
            }
        }

        protected void ddlProjectPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDashBoardWeek(0);
            BindProjectDashBoardDetailsGrid();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            HiddenField1.Value = "R";
            if ( ddlReports.SelectedItem.Text != "Select" )
            {
                FillDashBoardWeek(Convert.ToInt64(ddlReports.SelectedItem.Value));
                InsertDashboardDetails();
                DevelopingProjectDashBoardDetails();
            }
            else            
                BindProjectDashBoardDetailsGrid();         
        } 

        protected void dgOverView_Sorting(object sender, GridViewSortEventArgs e)
        {
            //Retrieve the table from the session object.
            DataTable dtSession = Session["grdReview"] as DataTable;

            if (dtSession != null)
            {
                //Sort the data.
                BindProjectDashBoardDetailsGrid(); // Method to Bind ProjectDashBoard Details...
               
                dtSession.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                dgOverView.DataSource = Session["grdReview"];
                dgOverView.DataBind();

                FormatOverviewTab();  // Method to format the OverViewTab...

                dgProjectEffortPhase.DataSource = Session["ProjectEffortPhase"];
                dgProjectEffortPhase.DataBind();
                dtProject = Session["ProjectEffortPhase"] as DataTable;

                FormatProjEffortPhaseTab();  // Method to format the ProjectEffortPhaseTab...

                dgPeriodEffortPhase.DataSource = Session["PeriodEffortPhase"];
                dgPeriodEffortPhase.DataBind();
                dtPeriod = Session["PeriodEffortPhase"] as DataTable;

                FormatPeriodEffortPhaseTab();  // Method to format the PeriodEffortPhaseTab...

                dgProjectEffortRole.DataSource = Session["ProjectEffortRole"];
                dgProjectEffortRole.DataBind();
                dtPeriod = Session["ProjectEffortRole"] as DataTable;
                FormatProjectEffortRoleTab();

                dgPeriodEffortRole.DataSource = Session["PeriodEffortRole"];
                dgPeriodEffortRole.DataBind();
                dtPeriod = Session["PeriodEffortRole"] as DataTable;

                FormatPeriodEffortRole(); // Method to format the PeriodEffortRoleTab...

                lblHeading.Text = "From:" + ' ' + ddlReports.SelectedItem.Text.ToString().Substring(0, ddlReports.SelectedItem.Text.ToString().IndexOf("-")) + ' ' + '-' + ' ' + "To:" + ' ' + ddlReports.SelectedItem.Text.ToString().Substring(ddlReports.SelectedItem.Text.ToString().IndexOf("-") + 1, (ddlReports.SelectedItem.ToString().Length - ddlReports.SelectedItem.Text.ToString().IndexOf("-") - 1));
           }           
        }      

        protected void btnFinalize_Click(object sender, EventArgs e)
        {
            
            if ( ddlReports.SelectedItem.Text != "Select" )
            {
                DataTable dtDashBoard = PMOscar.BaseDAL.ExecuteDataTable("Select Status from Dashboard where DashboardID=" + ddlReports.SelectedItem.Value);
                if (dtDashBoard.Rows.Count > 0)
                {
                    if (dtDashBoard.Rows[0]["Status"].ToString() == "F")
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Dashboard for the selected period has already been finalized.";
                    }
                    else
                    {
                        int indexOfHypen = ddlReports.SelectedItem.Text.IndexOf('-');

                        string[] fromDay = (ddlReports.SelectedItem.Text.Substring(0, indexOfHypen)).Split('/');
                        string dayFrom = fromDay[2] + '-' + fromDay[1] + '-' + fromDay[0];

                        string[] toDay = (ddlReports.SelectedItem.Text.Substring(indexOfHypen + 1)).Split('/');
                        string dayTo = toDay[2] + '-' + toDay[1] + '-' + toDay[0];

                        List<SqlParameter> parameter = new List<SqlParameter>();
                        parameter.Add(new SqlParameter("@dayfrom",dayFrom));
                        parameter.Add(new SqlParameter("@dayto", dayTo));
                        DataTable dtFinalizeTotalHours = PMOscar.BaseDAL.ExecuteSPDataTable("FinalizeDashBoard_GetTotalHours", parameter);
                        if (dtFinalizeTotalHours.Rows.Count > 0)
                        {
                            int totalResourceHours = Convert.ToInt32(dtFinalizeTotalHours.Rows[0]["TotalHours"]);
                            int estimatedHours = Convert.ToInt32(dtFinalizeTotalHours.Rows[0]["EstimatedHours"]);
                            decimal actualHours = Convert.ToDecimal(dtFinalizeTotalHours.Rows[0]["ActualHours"]);

                            if (estimatedHours > totalResourceHours)
                            {
                                string alertScript = "<script language='javascript'>alert('Dashboard cannot be finalize because total expected hours is greater than the total resource hours for this week. Please update the expected hours.');</script>";
                                Page.RegisterStartupScript("showalert", alertScript);
                            }
                            else
                            {
                                if (actualHours < estimatedHours)
                                {
                                    string alertScript = "<script language='javascript'>alert('Dashboard cannot be finalize because total expected hours is greater than the total actual hours for this week. Please update the expected hours.');</script>";
                                    Page.RegisterStartupScript("showalert", alertScript);
                                }
                                else
                                {
                                    PMOscar.BaseDAL.ExecuteNonQuery("Update Dashboard set [Status]='F' where DashboardID=" + ddlReports.SelectedItem.Value);
                                    lblMsg.Text = PMOscar.Core.Constants.AddRole.FINALIZEDDASHBOARD;
                                }
                            }

                            #region unsued code
                            /*if (estimatedHours >= totalHours)
                            {
                                if (actualHours >= totalHours)
                                {
                                    PMOscar.BaseDAL.ExecuteNonQuery("Update Dashboard set [Status]='F' where DashboardID=" + ddlReports.SelectedItem.Value);
                                    lblMsg.Text = "Dashboard for the selected week has been finalized!!";
                                }
                                else if (actualHours < totalHours)
                                {
                                    string alertScript = "<script language='javascript'>alert('Dashboard cannot be finalize because total actual hours is less than the total hours for this. Please update the actual hours.');</script>";
                                    Page.RegisterStartupScript("showalert", alertScript);
                                }
                                //else
                                //{
                                //    string alertScript = "<script language='javascript'>alert('Dashboard cannot be finalize because total actual hours is greater than the total hours for this. Please update the actual hours.');</script>";
                                //    Page.RegisterStartupScript("showalert", alertScript);
                                //}
                            }
                            else if (estimatedHours < totalHours)
                            {
                                string alertScript = "<script language='javascript'>alert('Dashboard cannot be finalize because total expected hours is less than the total hours for this week. Please update the expected hours.');</script>";
                                Page.RegisterStartupScript("showalert", alertScript);
                            }
                            //else
                            //{
                            //    string alertScript = "<script language='javascript'>alert('Dashboard cannot be finalize because total expected hours is greater than the total hours for this week. Please update the expected hours.');</script>";
                            //    Page.RegisterStartupScript("showalert", alertScript);
                            //}*/
                            #endregion
                        }
                    }
               } 
            }
            if ( ddlReports.SelectedItem.Text != "Select" )
            {
                FillDashBoardWeek(Convert.ToInt64(ddlReports.SelectedItem.Value));
                DevelopingProjectDashBoardDetails();
                FillDashboard();
            }
            else            
                BindProjectDashBoardDetailsGrid();            
        }

        private void FillDashboard()
        {
            DataTable  ds = (DataTable) Session["grdReview"];
        }

        //protected void btnRefreshDropDownList_Click(object sender, EventArgs e)
        //{
        //    HiddenField2.Value = "R";
        //    //FillDashBoardWeek(0);
        //    //BindProjectDashBoardDetailsGrid();
        //    if (ddlReports.SelectedItem.Text != "Select")
        //    {
        //        FillDashBoardWeek(Convert.ToInt64(ddlReports.SelectedItem.Value));
        //        refreshDashboard();
        //        DevelopingProjectDashBoardDetailsAfterRefresh();
        //    }
        //    else
        //        BindProjectDashBoardDetailsGrid();  
        //}

        private void refreshDashboard()
        {
            string uptoDate = string.Empty;

            dayFrom = Convert.ToDateTime(dayFrom).ToString("yyyy/MM/dd");
            dayTo = Convert.ToDateTime(dayTo).ToString("yyyy/MM/dd");

            uptoDate = dayFrom;
            List<SqlParameter> parameter = new List<SqlParameter>();

            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Name", fromName + "-" + toName));
            parameter.Add(new SqlParameter("@FromDate", dayFrom));
            parameter.Add(new SqlParameter("@ToDate", dayTo));
            parameter.Add(new SqlParameter("@PeriodType", ddlProjectPeriod.SelectedItem.Text == "Weekly" ? "W" : "M"));
            parameter.Add(new SqlParameter("@Status", "I")); //I:-Inprogress,F:-Finalized
            SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;
            parameter.Add(returnValue);
            PMOscar.BaseDAL.ExecuteSPNonQuery("DashboardOperations", parameter);
            int dashBoardId = Convert.ToInt16(returnValue.Value);
            DataTable dtDashBoard = PMOscar.BaseDAL.ExecuteDataTable("Select Status from Dashboard where DashboardID=" + dashBoardId);

            //if (dtDashBoard.Rows.Count > 0)
            //{
                //if (dtDashBoard.Rows[0]["Status"].ToString() == "F")
                //{
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //    lblMsg.Text = "Dashboard for the selected period has already been finalized!";
                //}
                //else
                //{
                    DataTable dtDashBoardstatus = PMOscar.BaseDAL.ExecuteDataTable("Select * from ProjectDashboard where DashboardID=" + dashBoardId);
                    List<DataRow> list = new List<DataRow>();
                    foreach (DataRow dr in dtDashBoardstatus.Rows)
                    {
                        list.Add(dr);
                    }
                    PMOscar.BaseDAL.ExecuteNonQuery("Delete from dbo.ProjectDashboardEstimation where DashboardID=" + dashBoardId);
                    dtDashboard = new DataTable();
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@dayfrom", dayFrom));
                    parameter.Add(new SqlParameter("@dayto", dayTo));
                    parameter.Add(new SqlParameter("@uptodate", uptoDate));
                    parameter.Add(new SqlParameter("@tabType", "dashboard"));
                    parameter.Add(new SqlParameter("@Status", "Create"));
                    parameter.Add(new SqlParameter("@DashboardID", dashBoardId));
                    parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", true));
                    dtDashboard = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];

                    dtEstimation = new DataTable();
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@dayfrom", dayFrom));
                    parameter.Add(new SqlParameter("@dayto", dayTo));
                    parameter.Add(new SqlParameter("@uptodate", uptoDate));
                    parameter.Add(new SqlParameter("@tabType", "Estimation"));
                    parameter.Add(new SqlParameter("@Status", "Create"));
                    parameter.Add(new SqlParameter("@DashboardID", "1"));
                    parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", true));
                    dtEstimation = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];

                    for (int dashBoardCount = 0; dashBoardCount < dtDashboard.Rows.Count; dashBoardCount++)
                    {
                        if (HiddenField2.Value == "R")
                        {
                            parameter = new List<SqlParameter>();
                            parameter.Add(new SqlParameter("@ProjectID", dtDashboard.Rows[dashBoardCount]["ProjectID"]));
                            parameter.Add(new SqlParameter("@PhaseID", dtDashboard.Rows[dashBoardCount]["PhaseID"]));
                            parameter.Add(new SqlParameter("@ClientStatus", 3));
                            parameter.Add(new SqlParameter("@TimelineStatus", 3));
                            parameter.Add(new SqlParameter("@BudgetStatus", 3));
                            parameter.Add(new SqlParameter("@EscalateStatus", 3));
                            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@Status", "I"));
                            parameter.Add(new SqlParameter("@DashboardID", dashBoardId));
                            parameter.Add(new SqlParameter("@ProjectName", dtDashboard.Rows[dashBoardCount]["ProjectName"]));
                            parameter.Add(new SqlParameter("@ShortName", dtDashboard.Rows[dashBoardCount]["ShortName"]));
                            parameter.Add(new SqlParameter("@ProjectType", dtDashboard.Rows[dashBoardCount]["ProjectType"]));
                            parameter.Add(new SqlParameter("@ProjectOwner", dtDashboard.Rows[dashBoardCount]["ProjectOwnerID"]));
                            parameter.Add(new SqlParameter("@ProjectManager", dtDashboard.Rows[dashBoardCount]["ProjectManagerID"]));
                            parameter.Add(new SqlParameter("@DeliveryDate", dtDashboard.Rows[dashBoardCount]["DeliveryDate"]));
                            parameter.Add(new SqlParameter("@RevisedDeliveryDate", dtDashboard.Rows[dashBoardCount]["RevisedDeliveryDate"]));
                            parameter.Add(new SqlParameter("@PMComments", dtDashboard.Rows[dashBoardCount]["PMComments"]));
                            parameter.Add(new SqlParameter("@POComments", dtDashboard.Rows[dashBoardCount]["POComments"]));
                            parameter.Add(new SqlParameter("@DeliveryComments", dtDashboard.Rows[dashBoardCount]["DeliveryComments"]));
                            parameter.Add(new SqlParameter("@Comments", ""));
                            parameter.Add(new SqlParameter("@isActive", 1));
                            PMOscar.BaseDAL.ExecuteSPNonQuery("[ProjectDashboardOperations]", parameter);

                            DataTable dtDashBoardstatusnew = PMOscar.BaseDAL.ExecuteDataTable("Select * from ProjectDashboard where DashboardID=" + dashBoardId);
                            foreach (var j in list)
                            {
                                if (j.ItemArray[1].ToString() == dtDashBoardstatusnew.Rows[dtDashBoardstatusnew.Rows.Count - 1]["ProjectID"].ToString())
                                {
                                    if (j.ItemArray[3] != dtDashBoardstatusnew.Rows[dtDashBoardstatusnew.Rows.Count - 1]["ClientStatus"] || j.ItemArray[4].ToString() != dtDashBoardstatusnew.Rows[dtDashBoardstatusnew.Rows.Count - 1]["TimelineStatus"].ToString() || j.ItemArray[5].ToString() != dtDashBoardstatusnew.Rows[dtDashBoardstatusnew.Rows.Count - 1]["BudgetStatus"].ToString() || j.ItemArray[6].ToString() != dtDashBoardstatusnew.Rows[dtDashBoardstatusnew.Rows.Count - 1]["EscalateStatus"].ToString())
                                    {
                                        int dashboardId = Convert.ToInt32(dtDashBoardstatusnew.Rows[dtDashBoardstatusnew.Rows.Count - 1]["ProjectDashboardID"].ToString());
                                        int clientStatus = Convert.ToInt32(j.ItemArray[3]);
                                        int timelineStatus = Convert.ToInt32(j.ItemArray[4]);
                                        int budgetStatus = Convert.ToInt32(j.ItemArray[5]);
                                        int escalateStatus = Convert.ToInt32(j.ItemArray[6]);
                                        string weeklyComment = j.ItemArray[11].ToString();

                                        // Update projectdashboard status and weekly comment
                                        UpdateProjectDashboardStatus(dashboardId, clientStatus, timelineStatus, budgetStatus, escalateStatus, weeklyComment);
                                    }
                                }

                            }
                        }
                        else
                        {
                            parameter = new List<SqlParameter>();
                            parameter.Add(new SqlParameter("@ProjectID", dtDashboard.Rows[dashBoardCount]["ProjectID"]));
                            parameter.Add(new SqlParameter("@PhaseID", dtDashboard.Rows[dashBoardCount]["PhaseID"]));
                            parameter.Add(new SqlParameter("@ClientStatus", 3));
                            parameter.Add(new SqlParameter("@TimelineStatus", 3));
                            parameter.Add(new SqlParameter("@BudgetStatus", 3));
                            parameter.Add(new SqlParameter("@EscalateStatus", 3));
                            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@Status", "I"));
                            parameter.Add(new SqlParameter("@DashboardID", dashBoardId));
                            parameter.Add(new SqlParameter("@ProjectName", dtDashboard.Rows[dashBoardCount]["ProjectName"]));
                            parameter.Add(new SqlParameter("@ShortName", dtDashboard.Rows[dashBoardCount]["ShortName"]));
                            parameter.Add(new SqlParameter("@ProjectType", dtDashboard.Rows[dashBoardCount]["ProjectType"]));
                            parameter.Add(new SqlParameter("@ProjectOwner", dtDashboard.Rows[dashBoardCount]["ProjectOwnerID"]));
                            parameter.Add(new SqlParameter("@ProjectManager", dtDashboard.Rows[dashBoardCount]["ProjectManagerID"]));
                            parameter.Add(new SqlParameter("@DeliveryDate", dtDashboard.Rows[dashBoardCount]["DeliveryDate"]));
                            parameter.Add(new SqlParameter("@RevisedDeliveryDate", dtDashboard.Rows[dashBoardCount]["RevisedDeliveryDate"]));
                            parameter.Add(new SqlParameter("@PMComments", dtDashboard.Rows[dashBoardCount]["PMComments"]));
                            parameter.Add(new SqlParameter("@POComments", dtDashboard.Rows[dashBoardCount]["POComments"]));
                            parameter.Add(new SqlParameter("@DeliveryComments", dtDashboard.Rows[dashBoardCount]["DeliveryComments"]));
                            parameter.Add(new SqlParameter("@Comments", ""));
                            parameter.Add(new SqlParameter("@isActive", 1));
                            PMOscar.BaseDAL.ExecuteSPNonQuery("[ProjectDashboardOperations]", parameter);
                        }
                    }

                    for (int estimationCount = 0; estimationCount < dtEstimation.Rows.Count; estimationCount++)
                    {
                        parameter = new List<SqlParameter>();
                        parameter.Add(new SqlParameter("@DashboardID", dashBoardId));
                        parameter.Add(new SqlParameter("@ProjectID", dtEstimation.Rows[estimationCount]["ProjectID"]));
                        parameter.Add(new SqlParameter("@PhaseID", dtEstimation.Rows[estimationCount]["PhaseID"]));
                        parameter.Add(new SqlParameter("@EstimationRoleID", dtEstimation.Rows[estimationCount]["EstimationRoleID"]));
                        parameter.Add(new SqlParameter("@BillableHours", dtEstimation.Rows[estimationCount]["BillableHours"]));
                        parameter.Add(new SqlParameter("@BudgetHours", dtEstimation.Rows[estimationCount]["BudgetHours"]));
                        parameter.Add(new SqlParameter("@RevisedBudgetHours", dtEstimation.Rows[estimationCount]["RevisedBudgetHours"]));
                        parameter.Add(new SqlParameter("@ActualHrs", dtEstimation.Rows[estimationCount]["ActualHours"]));
                        parameter.Add(new SqlParameter("@PeriodEstimetedHrs", dtEstimation.Rows[estimationCount]["periodEstimatedHours"]));
                        parameter.Add(new SqlParameter("@PeriodActualHrs", dtEstimation.Rows[estimationCount]["PeriodActualHours"]));
                        parameter.Add(new SqlParameter("@PeriodEstimetedHrsAdj", Convert.ToDouble(dtEstimation.Rows[estimationCount]["periodEstimatedHours"])));
                        parameter.Add(new SqlParameter("@PeriodActualHrsAdj", Convert.ToDouble(dtEstimation.Rows[estimationCount]["PeriodActualHours"])));
                        parameter.Add(new SqlParameter("@EstRoleName", dtEstimation.Rows[estimationCount]["EstRoleName"]));
                        parameter.Add(new SqlParameter("@EstRoleShrtName", dtEstimation.Rows[estimationCount]["EstRoleshortName"]));
                        parameter.Add(new SqlParameter("@RoleName", dtEstimation.Rows[estimationCount]["Role"]));
                        parameter.Add(new SqlParameter("@RoleShrtName", dtEstimation.Rows[estimationCount]["ShortName"]));
                        parameter.Add(new SqlParameter("@ActualHrsCorrected", Convert.ToDouble(dtEstimation.Rows[estimationCount]["ActualHours"])));
                        PMOscar.BaseDAL.ExecuteSPNonQuery("[ProjectDashboardEstimationOperations]", parameter);
                    }
                    //lblMsg.ForeColor = System.Drawing.Color.Green;
                    //lblMsg.Text = "Dashboard reloaded successfully!!";
            //    }
            //}
            dtDashBoard.Dispose();
        }
                      

        private void DevelopingProjectDashBoardDetailsAfterRefresh()
        {
            string uptoDate = dayFrom;       

            try
            {
                dayFrom = Convert.ToDateTime(dayFrom).ToString("yyyy/MM/dd");
                dayTo = Convert.ToDateTime(dayTo).ToString("yyyy/MM/dd");
            }
            catch
            {
            }

            BindProjectDashBoardDetailsGrid();   // Method to Bind ProjectDashBoard Details...

            dtReview = new DataTable();
            List<SqlParameter> parameter = new List<SqlParameter>();
            //parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            //parameter.Add(new SqlParameter("@dayto", dayTo));
            //parameter.Add(new SqlParameter("@uptodate", uptoDate));
            //parameter.Add(new SqlParameter("@tabType", "Preview"));
            //parameter.Add(new SqlParameter("@Status", "Go"));
            //parameter.Add(new SqlParameter("@DashboardID", ddlReports.SelectedItem.Value));
            //parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", cbProposalAndVas.Checked));
            //dtReview = PMOscar.DataContract.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];
            dtReview =(DataTable) Session["grdReview"] ;  
            dgOverView.DataSource = dtReview;
            dgOverView.DataBind();

            FormatOverviewTab(); // Method to format the OverViewTab...


            dtProject = new DataTable();
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", uptoDate));
            parameter.Add(new SqlParameter("@tabType", "ProjectEffortPhase"));
            parameter.Add(new SqlParameter("@Status", "Go"));
            parameter.Add(new SqlParameter("@DashboardID", ddlReports.SelectedItem.Value));
            parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", cbProposalAndVas.Checked));
            dtProject = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];
            Session["ProjectEffortPhase"] = dtProject;

            FormatProjEffortPhaseTab(); // Method to format the ProjectEffortPhaseTab...

            dtPeriod = new DataTable();
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", uptoDate));
            parameter.Add(new SqlParameter("@tabType", "PeriodEffortPhase"));
            parameter.Add(new SqlParameter("@Status", "Go"));
            parameter.Add(new SqlParameter("@DashboardID", ddlReports.SelectedItem.Value));
            parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", cbProposalAndVas.Checked));
            dtPeriod = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];
            Session["PeriodEffortPhase"] = dtPeriod;

            FormatPeriodEffortPhaseTab();  // Method to format the PeriodEffortPhaseTab...

            dtPeriod = new DataTable();
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", uptoDate));
            parameter.Add(new SqlParameter("@tabType", "ProjectEffortRole"));
            parameter.Add(new SqlParameter("@Status", "Go"));
            parameter.Add(new SqlParameter("@DashboardID", ddlReports.SelectedItem.Value));
            parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", cbProposalAndVas.Checked));
            dtPeriod = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];
            Session["ProjectEffortRole"] = dtPeriod;

            FormatProjectEffortRoleTab(); // Method to format the ProjectEffortRoleTab...

            dtPeriod = new DataTable();
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", uptoDate));
            parameter.Add(new SqlParameter("@tabType", "PeriodEffortRole"));
            parameter.Add(new SqlParameter("@Status", "Go"));
            parameter.Add(new SqlParameter("@DashboardID", ddlReports.SelectedItem.Value));
            parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", cbProposalAndVas.Checked));
            dtPeriod = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];
            Session["PeriodEffortRole"] = dtPeriod;

            FormatPeriodEffortRole(); // Method to format the PeriodEffortRoleTab...

            DataTable dtDatas1 = new DataTable();          
            dtDatas1.Columns.Add("fromperiod");
            dtDatas1.Columns.Add("toperiod");
            dtDatas1.Columns.Add("dashboardid");
            dtDatas1.Columns.Add("status");   
            DataRow dr1 = dtDatas1.NewRow();           
            dr1["fromperiod"] = dayFrom;
            dr1["toperiod"] = dayTo;
            dr1["dashboardid"] = ddlReports.SelectedItem.Value;
            dr1["status"] = ddlProjectPeriod.SelectedItem.Text == "Weekly" ? "W" : "M";
            dtDatas1.Rows.Add(dr1);
            Session["dtDatas1"] = dtDatas1;

            lblHeading.Text = "From:" + ' ' + ddlReports.SelectedItem.Text.ToString().Substring(0, ddlReports.SelectedItem.Text.ToString().IndexOf("-")) + ' ' + '-' + ' ' + "To:" + ' ' + ddlReports.SelectedItem.Text.ToString().Substring(ddlReports.SelectedItem.Text.ToString().IndexOf("-") + 1, (ddlReports.SelectedItem.ToString().Length - ddlReports.SelectedItem.Text.ToString().IndexOf("-") - 1));
        }

        //TODO:
        /*
        protected void btnExportProjectDashboard_Click(object sender, EventArgs e)
        {
            ExcelPackage pck = new ExcelPackage();

            #region Overview Sheet

            var wsOverview = pck.Workbook.Worksheets.Add("Overview");

            dtReview = new DataTable();
            int dasboardId = Convert.ToInt32(ddlReports.SelectedItem.Value);
            var dashboard = DashboardDAL.GetDashboradById(dasboardId);
            dayFrom = Convert.ToDateTime(dashboard.FromDate).ToString("yyyy/MM/dd");
            dayTo = Convert.ToDateTime(dashboard.ToDate).ToString("yyyy/MM/dd");

            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", dayFrom));
            parameter.Add(new SqlParameter("@tabType", "Preview"));
            parameter.Add(new SqlParameter("@Status", "Go"));
            parameter.Add(new SqlParameter("@DashboardID", ddlReports.SelectedItem.Value));
            parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", cbProposalAndVas.Checked));
            dtReview = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];

            wsOverview.Cells["A1"].LoadFromDataTable(dtReview, true);

            #endregion

            #region Project Effort (Phase) Sheet

            var wsProjectEffortPhase = pck.Workbook.Worksheets.Add("Project Effort (Phase)");

            dtProject = new DataTable();
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", dayFrom));
            parameter.Add(new SqlParameter("@tabType", "ProjectEffortPhase"));
            parameter.Add(new SqlParameter("@Status", "Go"));
            parameter.Add(new SqlParameter("@DashboardID", ddlReports.SelectedItem.Value));
            parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", cbProposalAndVas.Checked));
            dtProject = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];

            wsProjectEffortPhase.Cells["A2"].LoadFromDataTable(dtProject, true);

            #endregion

            #region Period Effort (Phase) Sheet

            var wsPeriodEffortPhase = pck.Workbook.Worksheets.Add("Period Effort (Phase)");

            

            #endregion

            #region Project Effort (Role) Sheet

            var wsProjectEffortRole = pck.Workbook.Worksheets.Add("Project Effort (Role)");

            #endregion

            #region Period Effort (Role) Sheet

            var wsPeriodEffortRole = pck.Workbook.Worksheets.Add("Period Effort (Role)");


            #endregion

            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;  filename=ExcelDemo.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.Flush();
            Response.End();

        }
         */

   #endregion   
        
    }
}
