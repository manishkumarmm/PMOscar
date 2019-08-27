// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectPlanningEntry.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
//  Description  : Uses to assign resources to the particular project.
//  Author       : 
//  Created Date : 
//  Modified By  : Vibin MB
//  Modified Date: 11 April 2018
// </summary>
// -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;
using PMOscar.Core;

namespace PMOscar
{
    public partial class ProjectPlanningEntry : System.Web.UI.Page
    {
        #region"Declarations"

        public string previousMonthWeekText = string.Empty;
        public string currentMonthWeekText = string.Empty;
        public string previousMonthDay = string.Empty;
        public string currentMonthDay = string.Empty;
        public string currentMonthLastDay = string.Empty;
        public string nextMonthDay = string.Empty;
        public int startDay = 0, endDay = 0, status = 0;
        public int currentMonthDayNo = 1;
        public int previousMonthDayNo = 0;
        public int nextMonth = 0;
        public int nextYear = 0;
        public int nextMonthStartingDay = 1;
        public int previousMonthDays = 0;
        public int currentMonthDays = 0;
        public int previousYear = 2011;
        public int currentYear = 0;
        public int currentMonth = 0;
        private int editEstimatedHours;
        private decimal editActualHours;
        private int updateFlag = 0;
        private decimal rolePercentage;
        public IList<SqlParameter> parameter;
        private int editStatusId;
        private long timeTrackerId;
        private int deleteProjectId;
        public string projectId;
        double totalEstimated = 0;
        double totalActual = 0;
        private int year = 0;
        private int month = 0;
        private string period = null;
        private int weekIndex = 0;
        string dayfrom = string.Empty;
        string dayto = string.Empty;
        int frommonth = 0;
        int tomonth = 0;
        int fromyear = 0;
        int toyear = 0;
        private Boolean statusFlag = false;
        public static int currentItemIndex;
        public static string currentItemText;
        public static int BudgetHours;
        public static string ResName;
        private static bool isOverlappingWeek = false;
        #endregion

        #region"Page Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            dgProjectDetails.Enabled = true;
            ddlResources.Focus();

            txtEHours.Enabled = true;
            txtAHours.Enabled = true;
            txtEHours2.Enabled = true;
            txtAHours2.Enabled = true;

            if (Session["UserName"] == null)
                Response.Redirect("Default.aspx");

            else
            {
                if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 5=Employee)
                {
                    #region
                    //Only User tab is Visible for Sys admin

                    HtmlControl hcliadminRP = Page.Master.FindControl("liadminRP") as HtmlControl;
                    if (hcliadminRP != null)
                        (Page.Master.FindControl("liadminRP") as HtmlControl).Visible = true;

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

                    HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                    if (hcliuser != null)
                        (Page.Master.FindControl("liuser") as HtmlControl).Style.Add("display", "none");

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

                    Response.Redirect("AccessDenied.aspx");
                    #endregion
                }

                else if (Convert.ToInt16(Session["UserRoleID"]) == 4) // Checking role of user by UserRoleID ( 4=Sys Admin)
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

            txtEHours.Attributes.Add("onkeypress", "return CheckNemericValue(event,this);");
            txtAHours.Attributes.Add("onkeypress", "return CheckNemericValue(event,this);");
            //txtComments.Attributes.Add("onkeypress", "return CheckTextValue(event,this);");

            if(Session["Period"] != null)            
            {
                period = Session["Period"].ToString();
                year = Convert.ToInt32(Session["Year"]);
                month = Convert.ToInt32(Session["Month"]);
                period = Session["Period"].ToString();
                weekIndex = Convert.ToInt16(Session["WeekIndex"]);
            }
           
            if (!Page.IsPostBack)
            {
                FillYears();  // Method to bind year in DropDownList...
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString()); //........

                if (Request.QueryString["op"] != "E" && Request.QueryString["op"] != "D")
                {
                    if (Session["dtDatas"] != null)
                    {
                        DataTable dtSessionData = (DataTable)Session["dtDatas"];
                        if (dtSessionData.Rows.Count > 0)
                        {
                            weekIndex = Convert.ToInt32(dtSessionData.Rows[0]["WeekIndex"].ToString());
                            month = Convert.ToInt32(dtSessionData.Rows[0]["Month"].ToString());
                            year = Convert.ToInt32(dtSessionData.Rows[0]["Year"].ToString());
                            period = dtSessionData.Rows[0]["Week"].ToString();
                            period = period + " ";
                            ddlYear.SelectedValue = year.ToString();
                            ddlMonth.SelectedValue = (month + 1).ToString();

                            GetWeek();  // Method to populate the weeks in selected Month and Year...

                            ddlWeek.SelectedIndex = weekIndex;
                            Session["Year"] = year;
                            Session["Month"] = month;
                            Session["Period"] = period;
                            Session["WeekIndex"] = weekIndex;
                        }

                        dtSessionData.Dispose();
                    }
                }

                BindDropDownResources();   // Method to bind resources in the DropDownList...
                BindDropDownRole(); // Method to bind roles in the DropDownList...
                BindDropDownPhase(); // Method to bind phases in the DropDownList...

                BindDropDownTeams();//Method to bind teams in the DropdownList..


                timeTrackerId = (Request.QueryString["TTId"] != null) ? Convert.ToInt32(Request.QueryString["TTId"]) : 0;
                deleteProjectId = (Request.QueryString["DelProjId"] != null) ? Convert.ToInt32(Request.QueryString["DelProjId"]) : 0;

                projectId = (Request.QueryString["Id"] != null) ? Request.QueryString["Id"].ToString() : "0";
                Session["ProjectId"] = projectId;

                CheckInActiveProject(projectId);

                GetProjectNames();  // Method to get the project name and status...

                if (projectId != "0")
                    DisplayResourcedetails();   // Method to display resource details...

                SplitOverlappingWeek();  // Method to split overlapping week
                System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
                lblWeek.Text = period + ' ' + ',' + ' ' + mfi.GetMonthName(month + 1).ToString() + ' ' + ',' + ' ' + year;

                if (Request.QueryString["op"] != null && Request.QueryString["op"] == "E" && timeTrackerId != 0)
                {
                    int newmonth = Convert.ToInt32(Session["Month"]);
                    ddlYear.SelectedValue = Session["Year"].ToString();
                    ddlMonth.SelectedValue = (newmonth + 1).ToString();
                    GetWeek();  // Method to populate the weeks in selected Month and Year...
                    ddlWeek.SelectedIndex = Convert.ToInt32(Session["WeekIndex"]);
                    period = Session["Period"].ToString();
                    year = Convert.ToInt32(Session["Year"]);
                    month = Convert.ToInt32(Session["Month"]);
                    period = Session["Period"].ToString();
                    weekIndex = Convert.ToInt16(Session["WeekIndex"]);
                    projectId = Request.QueryString["ProjectEditId"] == "" ? Session["ProjectEditId"].ToString() : Request.QueryString["ProjectEditId"].ToString();
                    GetProjectAssignedResourceDetails(); // Method to get Project Assigned Resource Details...                 

                    if (editStatusId == 1)
                        timeTrackerId = 0;

                    else
                    {
                        ddlRole.Focus();
                        btnSave.Text = "Update";
                        Session["TimeTrackerId"] = timeTrackerId;
                    }

                    lblWeek.Text = period + ' ' + ',' + ' ' + mfi.GetMonthName(month + 1).ToString() + ' ' + ',' + ' ' + year;
                    lblProject.Text = (Request.QueryString["Projectname"] != null) ? Convert.ToString(Request.QueryString["Projectname"]) : string.Empty;

                    dgProjectDetails.Enabled = false;
                    DisplayResourcedetails();  // Method to display resource details...
                }

                if (Request.QueryString["op"] != null && Request.QueryString["op"] == "D" && timeTrackerId != 0)
                {
                    int newmonth = Convert.ToInt32(Session["Month"]);
                    ddlYear.SelectedValue = Session["Year"].ToString();
                    ddlMonth.SelectedValue = (newmonth + 1).ToString();
                    GetWeek();  // Method to populate the weeks in selected Month and Year...
                    ddlWeek.SelectedIndex = Convert.ToInt32(Session["WeekIndex"]);
                    period = Session["Period"].ToString();
                    year = Convert.ToInt32(Session["Year"]);
                    month = Convert.ToInt32(Session["Month"]);
                    period = Session["Period"].ToString();
                    weekIndex = Convert.ToInt16(Session["WeekIndex"]);
                    projectId = Request.QueryString["ProjectDeleteId"] == "" ? Session["ProjectDeleteId"].ToString() : Request.QueryString["ProjectDeleteId"].ToString();
                    if (statusFlag == false)
                        DeleteResourceDetails();   // Method to delete the Resource Details...                      

                    else
                    {
                        lblPjct.Visible = true;
                        lblPjct.Text = "Entry cannot be deleted since the Project is Inactive";
                    }

                    lblWeek.Text = period + ' ' + ',' + ' ' + mfi.GetMonthName(month + 1).ToString() + ' ' + ',' + ' ' + year;
                    lblProject.Text = (Request.QueryString["Projectname"] != null) ? Convert.ToString(Request.QueryString["Projectname"]) : string.Empty;
                    timeTrackerId = 0;
                    Session["TimeTrackerId"] = timeTrackerId; // ......To be changed if needed 13/06/2011
                    DisplayResourcedetails(); // Method to display resource details...
                }
            }
            else
            {

                timeTrackerId = Session["TimeTrackerId"] != null ? Convert.ToInt64(Session["TimeTrackerId"]) : 0;
                projectId = (Request.QueryString["Id"] != null) ? Request.QueryString["Id"].ToString() : Session["ProjectId"].ToString();
                CheckInActiveProject(projectId);
                DisplayResourcedetails();

            }

            if (lblProject.Text == "Admin" || lblProject.Text == "Open")
            {
                ddlPhase.Enabled = false;
                RqResEntry.Enabled = false;
            }
            else
                RqResEntry.Enabled = true;

            //diasble finalized time period
            if (!IsPostBack)
            {
                int week = ddlWeek.SelectedIndex;
                string dayfrom = Session["dayfrom"].ToString();
                string dayto = Session["dayto"].ToString();
                int mon = Convert.ToInt32(ddlMonth.SelectedItem.Value);
                string yea = ddlYear.SelectedItem.Text;
                string day = ddlWeek.SelectedItem.Text;
                string[] arrday = day.Split('-');
                string timeperiod = null;
                if (week == 0)
                {
                    if (Convert.ToInt32(arrday[0]) > Convert.ToInt32(arrday[1]))
                    {
                        timeperiod = arrday[0] + "/" + (mon - 1) + "/" + yea + "-" + arrday[1] + "/" + mon + "/" + yea;
                    }
                    else
                    {
                        timeperiod = arrday[0] + "/" + mon + "/" + yea + "-" + arrday[1] + "/" + mon + "/" + yea;
                    }
                }
                else if (week > 0)
                {
                    if (Convert.ToInt32(arrday[0]) > Convert.ToInt32(arrday[1]))
                    {
                        timeperiod = arrday[0] + "/" + mon + "/" + yea + "-" + arrday[1] + "/" + (mon + 1) + "/" + yea;
                    }
                    else
                    {
                        timeperiod = arrday[0] + "/" + mon + "/" + yea + "-" + arrday[1] + "/" + mon + "/" + yea;
                    }
                }
                DataTable dt = PMOscar.BaseDAL.ExecuteDataTable("SELECT Status FROM Dashboard where Name ='" + timeperiod + "'");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Status"].ToString() == "F")
                    {
                        btnSave.Enabled = false;
                        Label7.Text = "Dashboard for this period has already been finalized, so you won't be able to make any edits here";
                        dgProjectDetails.Columns[8].Visible = false;
                        dgProjectDetails.Columns[9].Visible = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        Label7.Text = "";
                        if (statusFlag == false)
                        {
                            dgProjectDetails.Columns[8].Visible = true;
                            dgProjectDetails.Columns[9].Visible = true;
                        }
                        else
                        {
                            dgProjectDetails.Columns[8].Visible = false;
                            dgProjectDetails.Columns[9].Visible = false;
                        }
                    }
                }
                else
                {
                    btnSave.Enabled = true;
                    Label7.Text = "";
                }
            }
            //diasble finalized time period
        }

        #endregion
        #region"Methods"
        //function to disable edit and delete for inactive project
        private void CheckInActiveProject(string projectId)
        {
            try
            {
                var dsTimeTrackerDetails = new DataSet();

                var parameter = new List<SqlParameter> { new SqlParameter("@ProjectID", projectId) };
                dsTimeTrackerDetails = PMOscar.BaseDAL.ExecuteSPDataSet("GetProjectById", parameter);


                if (dsTimeTrackerDetails.Tables[0].Rows.Count > 0)
                {
                    lblPjct.Visible = true;
                    lblPjct.Text = "Entry cannot be updated since project is Inactive";
                    dgProjectDetails.Columns[8].Visible = false;
                    dgProjectDetails.Columns[9].Visible = false;
                    statusFlag = true;

                }
                else
                {
                    dgProjectDetails.Columns[8].Visible = true;
                    dgProjectDetails.Columns[9].Visible = true;
                }

                Session["ProjectIdTA"] = projectId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }


        /// <summary>
        /// Binds the drop down teams.
        /// </summary>
        private void BindDropDownTeams()
        {
            try
            {

                ddlTeam.ClearSelection();

                parameter = new List<SqlParameter>();
                DataTable dtTeam = BaseDAL.ExecuteSPDataTable("GetTeam", parameter);
                ddlTeam.DataSource = dtTeam;
                ddlTeam.DataTextField = "Team";
                ddlTeam.DataValueField = "TeamID";
                ddlTeam.DataBind();
                ddlTeam.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception ex)
            {

            }

        }


        // Method to bind year in DropDownList...
        private void FillYears()
        {
            for (int yearCount = DateTime.Now.Year - 4; yearCount <= DateTime.Now.Year + 1; yearCount++)
            {
                ListItem ltYear = new ListItem();
                ltYear.Value = yearCount.ToString();
                ltYear.Text = yearCount.ToString();
                ddlYear.Items.Add(ltYear);
                ddlYear.DataBind();
            }
        }

        // Method to delete the Resource Details...
        private void DeleteResourceDetails()
        {

            int timeTrackerId = (Request.QueryString["TTId"] != null) ? Convert.ToInt32(Request.QueryString["TTId"]) : 0;

            if (timeTrackerId > 0)
            {
                DataSet dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select TimeTracker.ResourceId,TimeTracker.RoleId,Project.ProjectName,Role.Role," +
                 " isnull(TimeTracker.EstimatedHours,0) As EstimatedHours,TimeTracker.ActualHours As ActualHours,Phase.PhaseID,TimeTracker.WeeklyComments " +
                 " From [Resource] as R left Join TimeTracker on TimeTracker.ResourceId = R.ResourceId " +
                 " Inner join Role on TimeTracker.RoleId = Role.RoleId inner join Project on Project.ProjectId = TimeTracker.ProjectId " +
                 " left Join Phase on TimeTracker.PhaseID = Phase.PhaseID Where R.IsActive = 1 and Project.IsActive=1 And " +
                 " TimeTracker.TimeTrackerId = " + timeTrackerId + " AND Project.ProjectId = " + projectId);

                if (dsTimeTrackerDetails.Tables[0].Rows.Count > 0)
                {
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@TimeTrackerID", timeTrackerId));
                    parameter.Add(new SqlParameter("@Proj_Id", projectId));
                    parameter.Add(new SqlParameter("@Res_Id", Convert.ToInt32(ddlResources.SelectedValue.ToString())));
                    parameter.Add(new SqlParameter("@Role_Id", Convert.ToInt32(ddlRole.SelectedValue.ToString())));
                    parameter.Add(new SqlParameter("@Time_EHours", 1));
                    parameter.Add(new SqlParameter("@Time_AHours", 1));
                    parameter.Add(new SqlParameter("@Time_UpdatedBy", Session["UserID"]));
                    parameter.Add(new SqlParameter("@Time_UpdatedDate", DateTime.Now));
                    parameter.Add(new SqlParameter("@Time_FromDate", DateTime.Now));
                    parameter.Add(new SqlParameter("@Time_ToDate", DateTime.Now));
                    parameter.Add(new SqlParameter("@Time_CreatedBy", Session["UserID"]));
                    parameter.Add(new SqlParameter("@Time_CreatedDate", DateTime.Now));
                    parameter.Add(new SqlParameter("@PhaseId", Convert.ToInt32(ddlPhase.SelectedValue.ToString())));
                    parameter.Add(new SqlParameter("@WeeklyComments", "Test"));
                    parameter.Add(new SqlParameter("@OpMode", "DELETE"));
                    parameter.Add(new SqlParameter("@TeamId", ddlTeam.SelectedValue));
                    try
                    {
                        int TimeTrackerId = BaseDAL.ExecuteSPNonQuery("ResourcePlanningOperations", parameter);
                    }
                    catch
                    {

                    }
                    lblPjct.Visible = false;
                }

                else
                {
                    DataTable dtSessionProjectDetails = new DataTable();
                    dtSessionProjectDetails = (DataTable)Session["gvProjectDetails"];

                    if (dtSessionProjectDetails.Rows.Count > 0)
                    {
                        lblPjct.Visible = true;
                        lblPjct.Text = "Entry cannot be deleted since Resource is Inactive";
                    }
                    else
                        lblPjct.Visible = false;
                }
            }
        }

        // Method to bind roles in the DropDownList...
        private void BindDropDownRole()
        {
            parameter = new List<SqlParameter>();
            DataTable dtRoles = BaseDAL.ExecuteSPDataTable("GetRole", parameter);
            ddlRole.DataSource = dtRoles;
            ddlRole.DataTextField = "Role";
            ddlRole.DataValueField = "RoleId";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("Select", "0"));
        }

        // Method to bind phases in the DropDownList...
        private void BindDropDownPhase()
        {
            parameter = new List<SqlParameter>();
            DataTable dtPhases = BaseDAL.ExecuteSPPhaseDataTable("GetPhase", parameter);
            ddlPhase.DataSource = dtPhases;
            ddlPhase.DataTextField = "Phase";
            ddlPhase.DataValueField = "PhaseID";
            ddlPhase.DataBind();
            ddlPhase.Items.Insert(0, new ListItem("Select", "0"));
        }

        // Method to get Project Assigned Resource Details...
        private void GetProjectAssignedResourceDetails()
        {
            DataSet dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select TimeTracker.ResourceId,TimeTracker.RoleId,Project.ProjectName,Role.Role," +
                 " isnull(TimeTracker.EstimatedHours,0) As EstimatedHours,TimeTracker.ActualHours As ActualHours,Phase.PhaseID,TimeTracker.WeeklyComments,TimeTracker.TeamID,TimeTracker.FromDate,TimeTracker.ToDate,R.IsActive As ResourceISActive" +
                 " From [Resource] as R left Join TimeTracker on TimeTracker.ResourceId = R.ResourceId " +
                 " Inner join Role on TimeTracker.RoleId = Role.RoleId inner join Project on Project.ProjectId = TimeTracker.ProjectId " +
                 " left Join Phase on TimeTracker.PhaseID = Phase.PhaseID Where Project.IsActive=1 And " +
                 " TimeTracker.TimeTrackerId = " + timeTrackerId + " AND Project.ProjectId = " + projectId);

            if (dsTimeTrackerDetails.Tables[0].Rows.Count > 0)
            {
                statusFlag = false;
                if (Convert.ToBoolean(dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[11].ToString()))
                {
                    txtAHours.Text = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[5].ToString();
                    txtEHours.Text = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[4].ToString();
                    ddlResources.Text = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[0].ToString();
                    ddlRole.Text = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[1].ToString();
                    ddlPhase.Text = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[6].ToString();

                    /*** Configure week details for Edit mode ***/
                    DateTime fromDate = Convert.ToDateTime(dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[9].ToString());
                    DateTime toDate = Convert.ToDateTime(dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[10].ToString());

                    lblWeek1.Text = fromDate.Day + "-" + toDate.Day;
                    lblMonth1.Text = fromDate.ToString("MMMM");

                    trWeek2.Visible = false;
                    isOverlappingWeek = false;

                    // Set dates to hidden field
                    hdnWeek1StartDate.Value = fromDate.ToShortDateString();
                    hdnWeek1EndDate.Value = toDate.ToShortDateString();
                    hdnWeek2StartDate.Value = string.Empty;
                    hdnWeek2EndDate.Value = string.Empty;

                    if (dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[4].ToString() == string.Empty)
                        editEstimatedHours = 0;

                    else
                        editEstimatedHours = Convert.ToInt32(dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[4].ToString());

                    if (dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[5].ToString() == string.Empty)
                        editActualHours = 0;

                    else
                        editActualHours = Convert.ToDecimal(dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[5].ToString());

                    txtComments.Text = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[7].ToString();
                    editStatusId = 0;


                    // select team details
                    string team = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[8].ToString();
                    try
                    {
                        if (!string.IsNullOrEmpty(team))
                        {
                            ddlTeam.ClearSelection();
                            ddlTeam.Items.FindByValue(team).Selected = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message, ex);
                    }

                }

                else
                {
                    editStatusId = 1;
                    lblPjct.Visible = true;
                    lblPjct.Text = "Entry cannot be updated since Resource is Inactive";
                }
            }

            else
            {
                lblPjct.Visible = true;
                lblPjct.Text = "Entry cannot be updated since project is Inactive";
                statusFlag = true;
            }
        }

        // Method to get the project name and status...
        private void GetProjectNames()
        {
            projectId = Session["ProjectId"].ToString();

            DataSet dsProjectDetails = BaseDAL.ExecuteDataSet("Select ProjectName,Isactive From dbo.Project Where ProjectId = " + projectId);

            if (dsProjectDetails.Tables[0].Rows.Count > 0)
            {
                lblProject.Text = dsProjectDetails.Tables[0].Rows[0].ItemArray[0].ToString();

                if (dsProjectDetails.Tables[0].Rows[0].ItemArray[1].ToString() == "False")
                    statusFlag = true;

                else
                    statusFlag = false;
            }
        }

        // Method to bind resources in the DropDownList...
        private void BindDropDownResources()
        {
            parameter = new List<SqlParameter>();
            setPeriod();
            string fromDate = Convert.ToString(dayfrom);
            string toDate = Convert.ToString(dayto);
            parameter.Add(new SqlParameter("@dayfrom", fromDate));
            parameter.Add(new SqlParameter("@dayto", toDate));
            DataTable dtResources = BaseDAL.ExecuteSPDataTable("GetResources", parameter);
            ddlResources.DataSource = dtResources;
            ddlResources.DataTextField = "ResourceName";
            ddlResources.DataValueField = "ResourceId";
            ddlResources.DataBind();
            ddlResources.Items.Insert(0, new ListItem("Select", "0"));
        }

        // Method to save the resource details...
        private void SaveResourceDetails()
        {
            int resourceId = Convert.ToInt32(ddlResources.SelectedValue.ToString());
            int phaseId = Convert.ToInt32(ddlPhase.SelectedValue.ToString());
            int saveFlag = 1;

            if (phaseId == 0)
                phaseId = 7;

            int roleId = Convert.ToInt32(ddlRole.SelectedValue.ToString());
            if (string.IsNullOrEmpty(hdnSaveCheck.Value))
            {
                DataSet dsTimeTrackerDetails;

                if (lblProject.Text != "Admin" && lblProject.Text != "Open")
                {
                    dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + resourceId + " " +
                    " And ProjectId =  " + projectId + " And RoleId = " + roleId + " And PhaseID = " + phaseId + " And (Year(FromDate)= " + Session["Year"] + " " +
                    " or Year(ToDate)=" + Session["Year"] + ") and FromDate>='" + Session["dayfrom"] + "' and  ToDate<='" + Session["dayto"] + "'");
                }
                else
                {
                    dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + resourceId + " " +
                    " And ProjectId =  " + projectId + " And RoleId = " + roleId + " And (Year(FromDate)= " + Session["Year"] + " " +
                    " or Year(ToDate)=" + Session["Year"] + ") and FromDate>='" + Session["dayfrom"] + "' and  ToDate<='" + Session["dayto"] + "'");
                }

                if (dsTimeTrackerDetails.Tables[0].Rows.Count > 0)
                {
                    lblMsgStatus.Text = "There is already an allocation for this resource against the same Phase and Role. Please update the existing entry.";
                    lblMsgStatus.Visible = true;
                    lblPjct.Visible = false;
                    saveFlag = 0;
                }
                else
                {
                    saveFlag = 1;
                }
            }

            if (saveFlag == 1)
            {
                lblMsgStatus.Visible = false;
                lblMsgStatus.Text = string.Empty;
                updateFlag = 0;

                GetRolePercentageValue();
                ValidationCheckingOnHours();

                phaseId = Convert.ToInt32(ddlPhase.SelectedValue.ToString());

                try
                {
                    if (isOverlappingWeek) // Overlapping week
                    {
                        int exceededHours = 0;
                        bool canInsert = GetTotalHoursOfResourceInProject(Convert.ToInt32(ddlResources.SelectedValue.ToString()), 0, Convert.ToDateTime(hdnWeek1StartDate.Value.ToString()), Convert.ToDateTime(hdnWeek1EndDate.Value.ToString()), txtEHours.Text.Trim(), txtAHours.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Insert,out exceededHours);
                        int hoursExceeded1 = exceededHours;
                        bool canInsert2 = GetTotalHoursOfResourceInProject(Convert.ToInt32(ddlResources.SelectedValue.ToString()), 0, Convert.ToDateTime(hdnWeek2StartDate.Value.ToString()), Convert.ToDateTime(hdnWeek2EndDate.Value.ToString()), txtEHours2.Text.Trim(), txtAHours2.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Insert,out exceededHours);
                        int hoursExceeded2 = exceededHours;

                        // Here we have to insert multiple entry for split dates (for two months)

                        // First entry for week1
                        if (canInsert && canInsert2)
                        {
                            string fromDate = hdnWeek1StartDate.Value; //Session["dayfrom"].ToString();
                            string toDate = hdnWeek1EndDate.Value; //Session["dayto"].ToString();
                            parameter = new List<SqlParameter>();
                            parameter.Add(new SqlParameter("@TimeTrackerID", 1));
                            parameter.Add(new SqlParameter("@Proj_Id", projectId));
                            parameter.Add(new SqlParameter("@Res_Id", ddlResources.SelectedValue.ToString()));
                            parameter.Add(new SqlParameter("@Role_Id", Convert.ToInt32(ddlRole.SelectedValue.ToString())));

                            parameter.Add(txtEHours.Text == string.Empty
                                               ? new SqlParameter("@Time_EHours", DBNull.Value)
                                               : new SqlParameter("@Time_EHours", txtEHours.Text.Trim()));

                            parameter.Add(txtAHours.Text == string.Empty
                                                ? new SqlParameter("@Time_AHours", DBNull.Value)
                                                : new SqlParameter("@Time_AHours", txtAHours.Text.Trim()));

                            parameter.Add(new SqlParameter("@Time_UpdatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@Time_UpdatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@Time_FromDate", Convert.ToDateTime(fromDate)));
                            parameter.Add(new SqlParameter("@Time_ToDate", Convert.ToDateTime(toDate)));
                            parameter.Add(new SqlParameter("@Time_CreatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@Time_CreatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@PhaseId", phaseId));
                            parameter.Add(new SqlParameter("@WeeklyComments", txtComments.Text.Trim()));
                            parameter.Add(new SqlParameter("@TeamId", ddlTeam.SelectedValue));
                            parameter.Add(new SqlParameter("@OpMode", "INSERT"));
                            SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
                            returnValue.Direction = ParameterDirection.ReturnValue;
                            parameter.Add(returnValue);
                            timeTrackerId = BaseDAL.ExecuteSPScalar("ResourcePlanningOperations", parameter);

                            /***********************************************************************/
                            if (txtEHours2.Text.Trim() != string.Empty || txtAHours2.Text.Trim() != string.Empty)
                            {
                                // Second entry for week2
                                fromDate = hdnWeek2StartDate.Value; //Session["dayfrom"].ToString();
                                toDate = hdnWeek2EndDate.Value; //Session["dayto"].ToString();

                                parameter = new List<SqlParameter>();
                                parameter.Add(new SqlParameter("@TimeTrackerID", 1));
                                parameter.Add(new SqlParameter("@Proj_Id", projectId));
                                parameter.Add(new SqlParameter("@Res_Id", ddlResources.SelectedValue.ToString()));
                                parameter.Add(new SqlParameter("@Role_Id", Convert.ToInt32(ddlRole.SelectedValue.ToString())));

                                parameter.Add(txtEHours.Text == string.Empty
                                                   ? new SqlParameter("@Time_EHours", "0")
                                                   : new SqlParameter("@Time_EHours", txtEHours2.Text.Trim()));

                                parameter.Add(txtAHours.Text == string.Empty
                                                    ? new SqlParameter("@Time_AHours", DBNull.Value)
                                                    : new SqlParameter("@Time_AHours", txtAHours2.Text.Trim()));

                                parameter.Add(new SqlParameter("@Time_UpdatedBy", Session["UserID"]));
                                parameter.Add(new SqlParameter("@Time_UpdatedDate", DateTime.Now));
                                parameter.Add(new SqlParameter("@Time_FromDate", Convert.ToDateTime(fromDate)));
                                parameter.Add(new SqlParameter("@Time_ToDate", Convert.ToDateTime(toDate)));
                                parameter.Add(new SqlParameter("@Time_CreatedBy", Session["UserID"]));
                                parameter.Add(new SqlParameter("@Time_CreatedDate", DateTime.Now));
                                parameter.Add(new SqlParameter("@PhaseId", phaseId));
                                parameter.Add(new SqlParameter("@WeeklyComments", txtComments.Text.Trim()));
                                parameter.Add(new SqlParameter("@TeamId", ddlTeam.SelectedValue));
                                parameter.Add(new SqlParameter("@OpMode", "INSERT"));
                                returnValue = new SqlParameter("returnVal", SqlDbType.Int);
                                returnValue.Direction = ParameterDirection.ReturnValue;
                                parameter.Add(returnValue);
                                timeTrackerId = BaseDAL.ExecuteSPScalar("ResourcePlanningOperations", parameter);
                            }
                        }
                        else
                        {
                            lblMsgStatus.Text = string.Empty;
                            lblMsgStatus1.Text = string.Empty;
                            if (!canInsert && !canInsert2)
                            {
                                lblMsgStatus.Text = "Estimated/Actual hours exceeds for both the weeks. Please correct.";
                            }
                            else if (!canInsert)
                            {
                                lblMsgStatus.Text = "Estimated/Actual hours for the week " + lblWeek1.Text + " (" + lblMonth1.Text + ") exceeds " + hoursExceeded1 + " hours. Please correct.";
                            }
                            else if (!canInsert2)
                            {
                                lblMsgStatus.Text = "Estimated/Actual hours for the week " + lblWeek2.Text + " (" + lblMonth2.Text + ") exceeds " + hoursExceeded2 + " hours. Please correct.";
                            }
                        }
                    }
                    else // Not overlapping week
                    {
                        int exceededHours = 0;
                        bool canInsert = GetTotalHoursOfResourceInProject(Convert.ToInt32(ddlResources.SelectedValue.ToString()), 0, Convert.ToDateTime(hdnWeek1StartDate.Value.ToString()), Convert.ToDateTime(hdnWeek1EndDate.Value.ToString()), txtEHours.Text.Trim(), txtAHours.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Insert, out exceededHours);

                        if (canInsert)
                        {
                            // Here we have only one entry with week start date and week end date
                            string fromDate = hdnWeek1StartDate.Value; //Session["dayfrom"].ToString();
                            string toDate = hdnWeek1EndDate.Value; //Session["dayto"].ToString();

                            parameter = new List<SqlParameter>();
                            parameter.Add(new SqlParameter("@TimeTrackerID", 1));
                            parameter.Add(new SqlParameter("@Proj_Id", projectId));
                            parameter.Add(new SqlParameter("@Res_Id", ddlResources.SelectedValue.ToString()));
                            parameter.Add(new SqlParameter("@Role_Id", Convert.ToInt32(ddlRole.SelectedValue.ToString())));

                            parameter.Add(txtEHours.Text == string.Empty
                                               ? new SqlParameter("@Time_EHours", DBNull.Value)
                                               : new SqlParameter("@Time_EHours", txtEHours.Text.Trim()));

                            parameter.Add(txtAHours.Text == string.Empty
                                                ? new SqlParameter("@Time_AHours", DBNull.Value)
                                                : new SqlParameter("@Time_AHours", txtAHours.Text.Trim()));

                            parameter.Add(new SqlParameter("@Time_UpdatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@Time_UpdatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@Time_FromDate", Convert.ToDateTime(fromDate)));
                            parameter.Add(new SqlParameter("@Time_ToDate", Convert.ToDateTime(toDate)));
                            parameter.Add(new SqlParameter("@Time_CreatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@Time_CreatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@PhaseId", phaseId));
                            parameter.Add(new SqlParameter("@WeeklyComments", txtComments.Text.Trim()));
                            parameter.Add(new SqlParameter("@TeamId", ddlTeam.SelectedValue));
                            parameter.Add(new SqlParameter("@OpMode", "INSERT"));
                            SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
                            returnValue.Direction = ParameterDirection.ReturnValue;
                            parameter.Add(returnValue);
                            timeTrackerId = BaseDAL.ExecuteSPScalar("ResourcePlanningOperations", parameter);

                        }
                        else
                        {
                            lblMsgStatus.Text = string.Empty;
                            lblMsgStatus1.Text = string.Empty;
                            lblMsgStatus.Text = "Estimated/Actual hours for the week " + lblWeek1.Text + " (" + lblMonth1.Text + ") exceeds " + exceededHours + " hours. Please correct.";
                        }
                    }   
                }
                catch
                {

                }
            }
        }

        // Method to update the resource details...
        private void UpdateResourceDetails()
        {
            if (statusFlag == true)
                return;

            int resourceId = Convert.ToInt32(ddlResources.SelectedValue.ToString());
            int phaseId = Convert.ToInt32(ddlPhase.SelectedValue.ToString());
            int timeTrackerId = (Request.QueryString["TTId"] != null) ? Convert.ToInt32(Request.QueryString["TTId"]) : 0;
            int roleId = Convert.ToInt32(ddlRole.SelectedValue.ToString());

            string fromDate = hdnWeek1StartDate.Value;
            string toDate = hdnWeek1EndDate.Value;    

            DataSet dsTimeTrackerDetails;

            if (lblProject.Text != "Admin" && lblProject.Text != "Open")
            {
                dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + resourceId + " " +
                " And TimeTrackerId <> " + timeTrackerId + " And ProjectId =  " + projectId + " And RoleId = " + roleId + " And PhaseID = " + phaseId + "" +
                " And (Year(FromDate)= " + Convert.ToDateTime(fromDate).Year + " " +
                " or Year(ToDate)=" + Convert.ToDateTime(toDate).Year + ") and FromDate>='" + fromDate + "' and  ToDate<='" + toDate + "'");

            }
            else
            {
                dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + resourceId + " " +
                " And TimeTrackerId <> " + timeTrackerId + " And ProjectId =  " + projectId + " And RoleId = " + roleId + " " +
                " And (Year(FromDate)= " + Convert.ToDateTime(fromDate).Year + " " +
                " or Year(ToDate)=" + Convert.ToDateTime(toDate).Year + ") and FromDate>='" + fromDate + "' and  ToDate<='" + toDate + "'");
            }

            if (dsTimeTrackerDetails.Tables[0].Rows.Count > 0)
            {
                lblMsgStatus.Text = "There is already an allocation for this resource against the same Phase and Role. Please update the existing entry.";
                lblMsgStatus.Visible = true;
            }

            else
            {
                projectId = Request.QueryString["ProjectEditId"];

                updateFlag = 1;
                GetRolePercentageValue();
                ValidationCheckingOnHours();
                phaseId = Convert.ToInt32(ddlPhase.SelectedValue.ToString());

                try
                {
                    int exceededHours = 0;
                    bool canInsert = GetTotalHoursOfResourceInProject(resourceId,timeTrackerId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), txtEHours.Text.Trim(), txtAHours.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Update, out exceededHours);
                     if (canInsert)
                     {

                         parameter = new List<SqlParameter>();
                         parameter.Add(new SqlParameter("@TimeTrackerID", timeTrackerId));
                         parameter.Add(new SqlParameter("@Proj_Id", projectId));
                         parameter.Add(new SqlParameter("@Res_Id", ddlResources.SelectedValue.ToString()));
                         parameter.Add(new SqlParameter("@Role_Id", Convert.ToInt32(ddlRole.SelectedValue.ToString())));

                         if (txtEHours.Text == string.Empty)
                             parameter.Add(new SqlParameter("@Time_EHours", DBNull.Value));

                         else
                             parameter.Add(new SqlParameter("@Time_EHours", txtEHours.Text.Trim()));

                         if (txtAHours.Text == string.Empty)
                             parameter.Add(new SqlParameter("@Time_AHours", DBNull.Value));

                         else
                             parameter.Add(new SqlParameter("@Time_AHours", txtAHours.Text.Trim()));

                         parameter.Add(new SqlParameter("@Time_UpdatedBy", Session["UserID"]));
                         parameter.Add(new SqlParameter("@Time_UpdatedDate", DateTime.Now));
                         parameter.Add(new SqlParameter("@Time_FromDate", fromDate));
                         parameter.Add(new SqlParameter("@Time_ToDate", toDate));
                         parameter.Add(new SqlParameter("@Time_CreatedBy", Session["UserID"]));
                         parameter.Add(new SqlParameter("@Time_CreatedDate", DateTime.Now));
                         parameter.Add(new SqlParameter("@PhaseId", phaseId));
                         parameter.Add(new SqlParameter("@WeeklyComments", txtComments.Text.Trim()));
                         parameter.Add(new SqlParameter("@OpMode", "UPDATE"));
                         parameter.Add(new SqlParameter("@TeamId", ddlTeam.SelectedValue));
                         // Return value as parameter
                         SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
                         returnValue.Direction = ParameterDirection.ReturnValue;
                         parameter.Add(returnValue);
                         int timeTrackerIdReturnValue = BaseDAL.ExecuteSPScalar("ResourcePlanningOperations", parameter);
                     }
                     else
                     {
                         lblMsgStatus.Text = string.Empty;
                         lblMsgStatus1.Text = string.Empty;
                         lblMsgStatus.Text = "Estimated/Actual hours for the week " + lblWeek1.Text + " (" + lblMonth1.Text + ") exceeds " + exceededHours + " hours. Please correct.";
                         lblMsgStatus.Visible = true;
                     }

                }
                catch
                {

                }
                // function to call TimeTrackerAudit on updation to populate grid
                DisplayResourcedetails();
                TimeTrackerAudit1.SetResourceId = projectId;
                TimeTrackerAudit1.SetYear = year;
                TimeTrackerAudit1.SetDayFrom = dayfrom;
                TimeTrackerAudit1.SetDayTo = dayto;
                TimeTrackerAudit1.SetStatus = status;
                TimeTrackerAudit1.BindGridView();


            }
        }

        // Method to get Dates in the specified year and month...
        private void setPeriod()
        {
            if (weekIndex == 0 && Convert.ToInt16(period.Substring(0, period.IndexOf("-"))) >
                Convert.ToInt16(period.Substring(period.IndexOf("-") + 1, period.Length - (period.Length > 3 ? 3 : 2))))
            {
                frommonth = Convert.ToInt16(month) == 0 ? 12 : Convert.ToInt16(month);
                tomonth = month == 11 ? (Convert.ToInt16(month) + 1) : (month == 11 ? (Convert.ToInt16(month) + 1) : (Convert.ToInt16(month) + 1));
                fromyear = Convert.ToInt16(month) == 0 ? (Convert.ToInt16(year) - 1) : (Convert.ToInt16(month) == 11 ? (Convert.ToInt16(year)) : Convert.ToInt16(year));
                toyear = Convert.ToInt16(year);

                dayfrom = fromyear + "-" + frommonth + "-" + Convert.ToInt16(period.Substring(0, period.IndexOf("-")));
                dayto = toyear + "-" + tomonth + "-" + Convert.ToInt16(period.Substring(period.IndexOf("-") + 1, period.Length - (period.Length > 3 ? 3 : 2)));
            }
            else if (weekIndex != 0 && Convert.ToInt16(period.Substring(0, period.IndexOf("-"))) >
                                                   Convert.ToInt16(period.Substring(period.IndexOf("-") + 1,
                                                   period.Length - (period.Length > 3 ? 3 : 2))))
            {
                frommonth = month == 11 ? (Convert.ToInt16(month) + 1) : (Convert.ToInt16(month) + 1);
                tomonth = month == 11 ? 1 : (Convert.ToInt16(month) + 2);
                fromyear = Convert.ToInt16(month) == 0 ? (Convert.ToInt16(year)) : (Convert.ToInt16(month) == 11 ? Convert.ToInt16(year) : Convert.ToInt16(year));
                toyear = Convert.ToInt16(month) == 11 ? Convert.ToInt16(year) + 1 : Convert.ToInt16(year);

                dayfrom = fromyear + "-" + frommonth + "-" + Convert.ToInt16(period.Substring(0, period.IndexOf("-")));
                dayto = toyear + "-" + tomonth + "-" + Convert.ToInt16(period.Substring(period.IndexOf("-") + 1, period.Length - (period.Length > 3 ? 3 : 2)));
            }

            else
            {
                frommonth = month == 11 ? 1 : (Convert.ToInt16(month) + 1);
                tomonth = month == 11 ? 1 : (Convert.ToInt16(month) + 1);
                fromyear = Convert.ToInt16(year);
                toyear = Convert.ToInt16(year);

                dayfrom = fromyear + "-" + (Convert.ToInt16(month) + 1) + "-" + Convert.ToInt16(period.Substring(0, period.IndexOf("-")));
                dayto = fromyear + "-" + (Convert.ToInt16(month) + 1) + "-" + Convert.ToInt16(period.Substring(period.IndexOf("-") + 1, period.Length - (period.Length > 3 ? 3 : 2)));
            }

            Session["dayfrom"] = dayfrom;
            Session["dayto"] = dayto;
        }

        #region"Week Development Methods"

        // To get the number of days in the specified year and month...
        private int GetMonthDays(int monthId, int yearId)
        {
            int monthDays = 0;
            switch (Convert.ToInt32(monthId))
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    monthDays = 31;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    monthDays = 30;
                    break;
                case 2:
                    if (((Convert.ToInt32(yearId) % 4 == 0 & Convert.ToInt32(yearId) % 100 != 0) | (Convert.ToInt32(yearId) % 400 == 0)))
                    {
                        monthDays = 29;
                    }
                    else
                    {
                        monthDays = 28;
                    }
                    break;
            }
            return monthDays;
        }

        // To get the start day and end day for the first week text in the dropdownlist... 
        private void GetFirstWeekText()
        {
            if (currentMonth == 12)
            {
                nextYear = currentYear + 1;
                nextMonth = 1;
            }
            else
            {
                previousYear = currentYear;
                nextMonth = currentMonth + 1;
                nextYear = currentYear;
            }
            if (previousMonthDay == "Sunday")
                startDay = 1;

            if (currentMonthDay == "Sunday")
                endDay = currentMonthDayNo + 7;

            if (previousMonthDay == "Monday")
                startDay = previousMonthDayNo;

            if (currentMonthDay == "Monday")
                endDay = currentMonthDayNo + 6;

            if (previousMonthDay == "Tuesday")
                startDay = previousMonthDayNo - 1;

            if (currentMonthDay == "Tuesday")
                endDay = currentMonthDayNo + 5;

            if (previousMonthDay == "Wednesday")
                startDay = previousMonthDayNo - 2;

            if (currentMonthDay == "Wednesday")
                endDay = currentMonthDayNo + 4;

            if (previousMonthDay == "Thursday")
                startDay = previousMonthDayNo - 3;

            if (currentMonthDay == "Thursday")
                endDay = currentMonthDayNo + 3;

            if (previousMonthDay == "Friday")
                startDay = previousMonthDayNo - 4;

            if (currentMonthDay == "Friday")
                endDay = currentMonthDayNo + 2;

            if (previousMonthDay == "Saturday")
                startDay = currentMonthDayNo + 1;

            status = 0;

            if (currentMonthDay == "Saturday")
                endDay = currentMonthDayNo + 1;

            previousMonthWeekText = startDay.ToString() + '-' + endDay.ToString();
            ListItem lstFirstWeekText = new ListItem();
            lstFirstWeekText.Text = previousMonthWeekText;
            ddlWeek.Items.Add(lstFirstWeekText);
            ddlWeek.DataBind();
        }

        // To get the start day and end day for the middle week texts in the dropdownlist...
        private void GetMiddleWeekText()
        {
            for (int middleWeekTextsCount = 0; middleWeekTextsCount <= 5; middleWeekTextsCount++)
            {
                startDay = endDay + 1;
                endDay = startDay + 6;

                if (endDay > currentMonthDays)
                    break;

                currentMonthWeekText = startDay.ToString() + '-' + endDay.ToString();
                ListItem lstMiddleWeekTexts = new ListItem();
                lstMiddleWeekTexts.Text = currentMonthWeekText;
                ddlWeek.Items.Add(lstMiddleWeekTexts);
                ddlWeek.DataBind();
                if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Day <= endDay && DateTime.Now.Date.Month == ddlMonth.SelectedIndex + 1)
                    ddlWeek.SelectedIndex = middleWeekTextsCount + 1;
            }
        }

        // To get the start day and end day for the last week text in the dropdownlist...
        private void GetLastWeekText()
        {
            status = 0;

            if (currentMonthLastDay == "Sunday")
            {
                startDay = 1;
                status = 1;
            }

            if (nextMonthDay == "Sunday")
                endDay = 1;

            if (currentMonthLastDay == "Monday")
                startDay = previousMonthDayNo;

            if (nextMonthDay == "Monday")
                endDay = currentMonthDayNo + 6;

            if (currentMonthLastDay == "Tuesday")
                startDay = previousMonthDayNo - 1;

            if (nextMonthDay == "Tuesday")
                endDay = currentMonthDayNo + 5;

            if (currentMonthLastDay == "Wednesday")
                startDay = previousMonthDayNo - 2;

            if (nextMonthDay == "Wednesday")
                endDay = currentMonthDayNo + 4;

            if (currentMonthLastDay == "Thursday")
                startDay = previousMonthDayNo - 3;

            if (nextMonthDay == "Thursday")
                endDay = currentMonthDayNo + 3;

            if (currentMonthLastDay == "Friday")
                startDay = previousMonthDayNo - 4;

            if (nextMonthDay == "Friday")
                endDay = currentMonthDayNo + 2;

            if (currentMonthLastDay == "Saturday")
                startDay = previousMonthDayNo - 5;

            if (nextMonthDay == "Saturday")
                endDay = currentMonthDayNo + 1;

            if (status == 0)
            {
                previousMonthWeekText = startDay.ToString() + '-' + endDay.ToString();
                ListItem lstLastWeekText = new ListItem();
                lstLastWeekText.Text = previousMonthWeekText;
                ddlWeek.Items.Add(lstLastWeekText);
                if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Month == ddlMonth.SelectedIndex + 1)
                {
                    lstLastWeekText.Selected = true;
                }
                ddlWeek.DataBind();
            }
        }

        // Method to populate the weeks in selected Month and Year...
        private void GetWeek()
        {
            ddlWeek.Items.Clear();
            int currentMonthStartingDay = 1;
            int previousMonth = 1;

            currentYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            currentMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());

            if (currentMonth == 1)
            {
                previousYear = currentYear - 1;
                previousMonth = 12;
            }
            else
            {
                previousYear = currentYear;
                previousMonth = currentMonth - 1;
            }

            previousMonthDays = GetMonthDays(previousMonth, previousYear); // To get the number of days in the previous month.

            DateTime previousMonthDate = new DateTime(previousYear, previousMonth, previousMonthDays);
            previousMonthDay = Convert.ToString(previousMonthDate.DayOfWeek.ToString());
            DateTime currentMonthDate = new DateTime(currentYear, currentMonth, currentMonthStartingDay);
            currentMonthDay = Convert.ToString(currentMonthDate.DayOfWeek.ToString());
            previousMonthDayNo = previousMonthDays;

            GetFirstWeekText();    // To get the start day and end day for the first week text in the dropdownlist...            

            if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Day <= endDay && DateTime.Now.Date.Month == ddlMonth.SelectedIndex + 1)
            {
                // Modified  ddlWeek.SelectedIndex = 0 as a quick fix to resolve server error while choosing May from Month dropdown
                // NOTE that there is no previous month period is listing as the first item while choosing May from Month dropdown.
                ddlWeek.SelectedIndex = 0;
            }

            currentMonthDays = GetMonthDays(currentMonth, currentYear); // To get the number of days in the current month.
            GetMiddleWeekText();  // To get the start day and end day for the middle week texts in the dropdownlist...

            DateTime currentMonthLastDayDate = new DateTime(currentYear, currentMonth, currentMonthDays);
            currentMonthLastDay = Convert.ToString(currentMonthLastDayDate.DayOfWeek.ToString());
            DateTime nextMonthDayDate = new DateTime(nextYear, nextMonth, nextMonthStartingDay);
            nextMonthDay = Convert.ToString(nextMonthDayDate.DayOfWeek.ToString());

            currentMonthDayNo = 1;
            previousMonthDayNo = currentMonthDays;

            GetLastWeekText(); // To get the start day and end day for the last week text in the dropdownlist...
        }

        #endregion

        // Method to display resource details...
        private void DisplayResourcedetails()
        {
            totalEstimated = 0;
            totalActual = 0;

            if (projectId == "0" || projectId == null)

                projectId = Request.QueryString["ProjectEditId"];

            if (projectId == "0" || projectId == null)
                projectId = Request.QueryString["ProjectDeleteId"];

            period = period + " ";

            setPeriod();  // Method to get Dates in the specified year and month...

            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@ProjectID", projectId);
            parameter[1] = new SqlParameter("@Year", Session["Year"]);
            parameter[2] = new SqlParameter("@Month", Convert.ToInt16(Session["Month"]) + 1);
            parameter[3] = new SqlParameter("@dayfrom", dayfrom);
            parameter[4] = new SqlParameter("@dayto", dayto);

            DataSet dsProjectDetails = BaseDAL.ExecuteSPDataSet("GetProjectDetailsForProject", parameter);
            dgProjectDetails.DataSource = dsProjectDetails.Tables[0] as DataTable;
            Session["gvProjectDetails"] = dsProjectDetails.Tables[0] as DataTable;
            dgProjectDetails.DataBind();
            dgProjectDetails.Columns[0].Visible = false;
            Session["dayfrom"] = dayfrom;
            Session["dayto"] = dayto;
            dsProjectDetails.Dispose();

            Session["ProjectIdTA"] = projectId;
            Session["dayfromTA"] = dayfrom;
            Session["daytoTA"] = dayto;
            Session["Status"] = 2;
        }

        // Method to clear controls...
        private void ClearControls()
        {
            BindDropDownResources();  // Method to bind resources in the DropDownList...
            BindDropDownRole();      // Method to bind roles in the DropDownList...
            BindDropDownPhase();    // Method to bind phases in the DropDownList...

            BindDropDownTeams();

            ddlResources.Enabled = true;
            ddlRole.Enabled = true;
            ddlPhase.Enabled = true;
            btnSave.Text = "Save";
            txtAHours.Text = string.Empty;
            txtEHours.Text = string.Empty;
            txtAHours2.Text = string.Empty;
            txtEHours2.Text = string.Empty;
            txtComments.Text = string.Empty;

            if (lblProject.Text == "Admin" || lblProject.Text == "Open")
            {
                ddlPhase.Enabled = false;
                RqResEntry.Enabled = false;
            }
            else
                RqResEntry.Enabled = true;

            lblPjct.Text = string.Empty;
        }

        // Method to get the percentage values of the roles...
        private void GetRolePercentageValue()
        {
            int roleId = Convert.ToInt32(ddlRole.SelectedValue.ToString());

            DataSet dsRolePercentage = BaseDAL.ExecuteDataSet("Select EstimationPercentage From dbo.Role Where RoleId  =" + roleId);

            if (dsRolePercentage.Tables[0].Rows.Count > 0)
                rolePercentage = Convert.ToDecimal(dsRolePercentage.Tables[0].Rows[0].ItemArray[0]);
        }

        // Method to check the validation on hours to apply red colur on values..
        private void ValidationCheckingOnHours()
        {
            int revisedBudgetHours = 0;
            int totalEstimatedHours = 0;
            decimal totalActualHoursForEstimated = 0;
            decimal totalActualHoursForActual = 0;
            int estimationRoleId = 0;
            int resourceId = Convert.ToInt32(ddlResources.SelectedValue.ToString());
            string projectName = lblProject.Text;

            int roleId = Convert.ToInt32(ddlRole.SelectedValue.ToString());
            int TimeTrackerId = (Request.QueryString["TTId"] != null) ? Convert.ToInt32(Request.QueryString["TTId"]) : 0;

            if (projectName == "Open" || projectName == "Admin")
                return;

            DataSet dsEstimationRole = BaseDAL.ExecuteDataSet("Select EstimationRoleID From Role Where RoleId =" + roleId);

            if (dsEstimationRole.Tables[0].Rows.Count > 0)
                estimationRoleId = Convert.ToInt32(dsEstimationRole.Tables[0].Rows[0].ItemArray[0].ToString());

            int phaseId = Convert.ToInt32(ddlPhase.SelectedValue.ToString());

            DataSet dsAcutalHoursForEstimated = BaseDAL.ExecuteDataSet("select isnull(sum(case when ActualHours is null " +
                " then EstimatedHours *  r.EstimationPercentage " +
            " else ActualHours *  r.EstimationPercentage  end),0) as TotHours from  Timetracker tt join [Role] r on r.RoleID=tt.RoleID " +
            " join EstimationRole er on er.EstimationRoleID=r.EstimationRoleID where tt.ProjectID= " + projectId + "  and tt.PhaseID= " + phaseId + " " +
            " and er.EstimationRoleID=" + estimationRoleId + " And tt.TimeTrackerId <> " + TimeTrackerId + " group by  tt.ProjectID ,tt.PhaseID ,er.EstimationRoleID,  r.EstimationPercentage");

            if (dsAcutalHoursForEstimated.Tables[0].Rows.Count > 0)
                totalActualHoursForEstimated = Convert.ToDecimal(dsAcutalHoursForEstimated.Tables[0].Rows[0].ItemArray[0]);

            else
                totalActualHoursForEstimated = 0;

            DataSet dsAcutalHours = BaseDAL.ExecuteDataSet("select Isnull(sum(ActualHours * r.EstimationPercentage),0) as ActualHours " +
               " from TimeTracker tt Join [Role] r on r.roleID=tt.RoleID join EstimationRole er on er.EstimationRoleID=r.EstimationRoleID" +
               " where tt.ProjectID= " + projectId + "  and tt.PhaseID= " + phaseId + " and er.EstimationRoleID=" + estimationRoleId + " " +
               " group by tt.ProjectID,tt.PhaseID,er.[RoleName]");

            if (dsAcutalHours.Tables[0].Rows.Count > 0)
                totalActualHoursForActual = Convert.ToDecimal(dsAcutalHours.Tables[0].Rows[0].ItemArray[0]);

            else
                totalActualHoursForActual = 0;

            DataSet dsEstimatedHours = BaseDAL.ExecuteDataSet("select Isnull(sum(EstimatedHours),0) as EstimatedHours " +
               " from TimeTracker tt Join [Role] r on r.roleID=tt.RoleID join EstimationRole er on er.EstimationRoleID=r.EstimationRoleID" +
               " where tt.ProjectID= " + projectId + " and tt.PhaseID= " + phaseId + " and er.EstimationRoleID=" + estimationRoleId + " " +
               " And (Year(FromDate)= " + Session["Year"] + " " +
               " or Year(ToDate)=" + Session["Year"] + ") and FromDate>='" + Session["dayfrom"] + "' and  ToDate<='" + Session["dayto"] + "'" +
               " group by tt.ProjectID,tt.PhaseID,er.[RoleName]");

            if (dsEstimatedHours.Tables[0].Rows.Count > 0)
                totalEstimatedHours = Convert.ToInt32(dsEstimatedHours.Tables[0].Rows[0].ItemArray[0]);

            else
                totalEstimatedHours = 0;

            DataSet dsRevisedBudgetHours = BaseDAL.ExecuteDataSet("Select Isnull(Sum(RevisedBudgetHours),0) From ProjectEstimation " +
                    " Where ProjectID = " + projectId + " And PhaseID =  " + phaseId + " And EstimationRoleID = " + estimationRoleId + "");

            if (dsRevisedBudgetHours.Tables[0].Rows.Count > 0)
                revisedBudgetHours = Convert.ToInt32(dsRevisedBudgetHours.Tables[0].Rows[0].ItemArray[0]);

            else
                revisedBudgetHours = 0;

            int EstimatedHours = 0;
            decimal ActualHours = 0;

            if (txtEHours.Text == string.Empty)
                EstimatedHours = 0;

            else
                EstimatedHours = Convert.ToInt32(txtEHours.Text);

            if (txtAHours.Text == string.Empty)
                ActualHours = 0;

            else
                ActualHours = Convert.ToDecimal(txtAHours.Text);

            decimal EstimatedHoursPer = EstimatedHours * rolePercentage;
            decimal ActualHoursPer = ActualHours * rolePercentage;
            decimal CheckEstHours = EstimatedHoursPer + totalActualHoursForEstimated;
            decimal CheckActHours = ActualHoursPer + totalActualHoursForEstimated;

            if ((txtAHours.Text == string.Empty) && (CheckEstHours > 0))
            {
                if (revisedBudgetHours < CheckEstHours)
                    lblMsgStatus.Text = "Estimated Hours for " + lblProject.Text + "-" + ddlPhase.SelectedItem.Text + "-" + ddlRole.SelectedItem.Text + " has exceeded the Revised Budget Hours";
            }
            else if (CheckActHours > 0)
            {
                if (revisedBudgetHours < CheckActHours)
                {
                    lblMsgStatus1.Text = "Actual Hours for " + lblProject.Text + "-" + ddlPhase.SelectedItem.Text + "-" + ddlRole.SelectedItem.Text + " has exceeded the Revised Budget Hours";
                }
            }
            lblMsgStatus.Visible = true;
            lblMsgStatus1.Visible = true;
        }

        /// <summary>
        /// Method to split overlapping week and show/hide set of additional fields for adding "Estimated Time" and "Actual Time".
        /// </summary>
        private void SplitOverlappingWeek()
        {
            period = period + " ";
            setPeriod();
            DateTime fromDate = Convert.ToDateTime(dayfrom);
            DateTime toDate = Convert.ToDateTime(dayto);
            DateTime week1StartDate, week1EndDate, week2StartDate, week2EndDate;

            // Display week details
            if (fromDate.Month != toDate.Month)
            {
                week1StartDate = fromDate;
                week1EndDate = new DateTime(fromDate.Year, fromDate.Month, 1).AddMonths(1).AddDays(-1);
                week2StartDate = new DateTime(toDate.Year, toDate.Month, 1);
                week2EndDate = toDate;

                lblWeek1.Text = week1StartDate.Day + "-" + week1EndDate.Day;
                lblWeek2.Text = week2StartDate.Day + "-" + week2EndDate.Day;
                lblMonth1.Text = fromDate.ToString("MMMM");
                lblMonth2.Text = toDate.ToString("MMMM");

                trWeek2.Visible = true;
                isOverlappingWeek = true;

                txtEHours2.CausesValidation = false;
                rfvEHours2.Enabled = false;

                // Set split dates to hidden field
                hdnWeek1StartDate.Value = week1StartDate.ToShortDateString();
                hdnWeek1EndDate.Value = week1EndDate.ToShortDateString();
                hdnWeek2StartDate.Value = week2StartDate.ToShortDateString();
                hdnWeek2EndDate.Value = week2EndDate.ToShortDateString();
                CheckEntryForResource();
            }
            else
            {
                lblWeek1.Text = fromDate.Day + "-" + toDate.Day;
                lblMonth1.Text = fromDate.ToString("MMMM");

                trWeek2.Visible = false;
                isOverlappingWeek = false;

                // Set split dates to hidden field
                hdnWeek1StartDate.Value = fromDate.ToShortDateString();
                hdnWeek1EndDate.Value = toDate.ToShortDateString();
                hdnWeek2StartDate.Value = string.Empty;
                hdnWeek2EndDate.Value = string.Empty;
            }
        }

        //Method to Check the entried for resource in dropdown change
        private void CheckEntryForResource()
        {
            if (isOverlappingWeek)
            {
                int resourceId = Convert.ToInt32(ddlResources.SelectedValue);
                int roleId = Convert.ToInt32(ddlRole.SelectedValue);
                int phaseId = Convert.ToInt32(ddlPhase.SelectedValue);

                DateTime week1StartDate = Convert.ToDateTime(hdnWeek1StartDate.Value);
                DateTime week1EndDate = Convert.ToDateTime(hdnWeek1EndDate.Value);
                int year1 = week1StartDate.Year;

                DateTime week2StartDate = Convert.ToDateTime(hdnWeek2StartDate.Value);
                DateTime week2EndDate = Convert.ToDateTime(hdnWeek2EndDate.Value);
                int year2 = week2StartDate.Year;

                int[] year = new int[] { year1, year2 };
                DateTime[] weekStartDate = new DateTime[] { week1StartDate, week2StartDate };
                DateTime[] weekEndDate = new DateTime[] { week1EndDate, week2EndDate };

                int hasEntry = -1;
                int entryFlag = 0;

                if (resourceId != 0 && roleId != 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        DataSet dsTimeTrackerDetails = new DataSet();
                        if (lblProject.Text != "Admin" && lblProject.Text != "Open")
                        {
                            if (Convert.ToInt32(ddlPhase.SelectedValue) != 0)
                            {
                                dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + resourceId + " " +
                                " And ProjectId =  " + projectId + " And RoleId = " + roleId + " And PhaseID = " + phaseId + " And (Year(FromDate)= " + year[i] + " " +
                                " or Year(ToDate)=" + year[i] + ") and FromDate>='" + weekStartDate[i] + "' and  ToDate<='" + weekEndDate[i] + "'");
                            }
                        }
                        else
                        {
                            dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + resourceId + " " +
                            " And ProjectId =  " + projectId + " And RoleId = " + roleId + " And (Year(FromDate)= " + year[i] + " " +
                            " or Year(ToDate)=" + year[i] + ") and FromDate>='" + weekStartDate[i] + "' and  ToDate<='" + weekEndDate[i] + "'");
                        }
                        if (dsTimeTrackerDetails.Tables.Count != 0)
                        {
                            if (dsTimeTrackerDetails.Tables[0].Rows.Count > 0)
                            {
                                hasEntry = i;
                                entryFlag++;
                                lblMsgStatus.Visible = true;
                                lblPjct.Visible = false;
                            }
                        }
                    }
                    HideOrDisableTextBoxes(entryFlag, hasEntry);
                }
            }
            else
            {
                hdnSaveCheck.Value = string.Empty;
            }
        }

        //Method to hide or disable the textboxes if the entries for resource found
        private void HideOrDisableTextBoxes(int entryFlag, int hasEntry)
        {
            DateTime week1StartDate = Convert.ToDateTime(hdnWeek1StartDate.Value);
            DateTime week1EndDate = Convert.ToDateTime(hdnWeek1EndDate.Value);
            DateTime week2StartDate = Convert.ToDateTime(hdnWeek2StartDate.Value);
            DateTime week2EndDate = Convert.ToDateTime(hdnWeek2EndDate.Value);
            if (entryFlag != 0 && entryFlag != 2)
            {
                if (hasEntry == 0)
                {
                    lblWeek1.Text = week2StartDate.Day + "-" + week2EndDate.Day;
                    lblMonth1.Text = week2StartDate.ToString("MMMM");
                    lblMsgStatus.Text = "There is already an allocation for this resource against the same Phase and Role in this week(" + week1StartDate.Day + "-" + week1EndDate.Day + ").";
                    trWeek2.Visible = false;
                    isOverlappingWeek = false;
                    hdnSaveCheck.Value = "true";
                    hdnWeek1StartDate.Value = week2StartDate.ToShortDateString();
                    hdnWeek1EndDate.Value = week2EndDate.ToShortDateString();
                    hdnWeek2StartDate.Value = string.Empty;
                    hdnWeek2EndDate.Value = string.Empty;
                }
                else
                {
                    lblWeek1.Text = week1StartDate.Day + "-" + week1EndDate.Day;
                    lblMonth1.Text = week1StartDate.ToString("MMMM");
                    lblMsgStatus.Text = "There is already an allocation for this resource against the same Phase and Role in this week(" + week2StartDate.Day + "-" + week2EndDate.Day + ").";
                    trWeek2.Visible = false;
                    isOverlappingWeek = false;
                    hdnSaveCheck.Value = "true";
                    hdnWeek1StartDate.Value = week1StartDate.ToShortDateString();
                    hdnWeek1EndDate.Value = week1EndDate.ToShortDateString();
                    hdnWeek2StartDate.Value = string.Empty;
                    hdnWeek2EndDate.Value = string.Empty;
                }
            }
            else if (entryFlag == 2)
            {
                lblMsgStatus.Text = "There is already an allocation for this resource against the same Phase and Role. Please update the existing entry.";
                txtEHours.Enabled = false;
                txtAHours.Enabled = false;
                txtEHours2.Enabled = false;
                txtAHours2.Enabled = false;
                btnSave.Enabled = false;
            }
            else if (entryFlag == 0)
            {
                hdnSaveCheck.Value = "true";
            }
        }
        #endregion

        #region"Control Events"

        protected override void OnPreRender(EventArgs e)
        {
            ViewState["update"] = Session["update"];
            base.OnPreRender(e);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Label7.Text != string.Empty)
            {
                dgProjectDetails.Columns[8].Visible = false;
                dgProjectDetails.Columns[9].Visible = false;
            }
            ClearControls(); // Method to clear controls...    

            year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            month = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
            month = month - 1;
            period = ddlWeek.SelectedItem.Text.ToString();
            weekIndex = ddlWeek.SelectedIndex;

            lblMsgStatus.Text = string.Empty;
            lblMsgStatus1.Text = string.Empty;
            lblPjct.Text = string.Empty;

            SplitOverlappingWeek(); // Method to split overlapping week
            DisplayResourcedetails();  // Method to display resource details...

            timeTrackerId = 0;
            Session["TimeTrackerId"] = null;
            ddlResources.Focus();



        }
        protected void lbBack_Click(object sender, EventArgs e)
        {
            year = Convert.ToInt32(Session["Year"]);
            month = Convert.ToInt32(Session["Month"]);
            weekIndex = Convert.ToInt16(Session["WeekIndex"]);
            Response.Redirect("ResourcePlanning.aspx?redirect=true&weekindex=" + weekIndex + "&month=" + month + "&year=" + year);
        }
        protected void gvProjectDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    if (e.Row.Cells[6].Text == "&nbsp;")
                        e.Row.Cells[6].Text = "";
                    if (e.Row.Cells[7].Text == "&nbsp;")
                        e.Row.Cells[7].Text = "";

                    if (e.Row.Cells[11].Text == "False")
                    {
                        e.Row.Cells[8].Text = "";
                        e.Row.Cells[9].Text = "";
                    }
                    totalEstimated += Convert.ToDouble(e.Row.Cells[6].Text.ToString() == "" ? "0" : e.Row.Cells[6].Text.ToString());
                    totalActual += Convert.ToDouble(e.Row.Cells[7].Text.ToString() == "" ? "0" : e.Row.Cells[7].Text.ToString());
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (totalEstimated == 0)
                    e.Row.Cells[6].Text = "";
                else
                    e.Row.Cells[6].Text = totalEstimated.ToString();

                if (totalActual == 0)
                    e.Row.Cells[7].Text = "";
                else
                    e.Row.Cells[7].Text = totalActual.ToString();

                e.Row.Cells[5].Text = "Total";
            }

            // Portion to  apply red color to the actual and estimated values...

            e.Row.Cells[10].Visible = false;

            e.Row.Cells[11].Visible = false;

            int cellcount = e.Row.Cells.Count;
            for (int i = 4; i < cellcount; i++)
            {

                if (e.Row.Cells[i].Text == "R")
                {

                    if (e.Row.Cells[i - 3].Text == "")
                    {
                        e.Row.Cells[i - 4].ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        e.Row.Cells[i - 3].ForeColor = System.Drawing.Color.Red;
                    }
                }

            }

            // Portion to  apply red color to the actual and estimated values...
        }
        protected void lblProject_Click(object sender, EventArgs e)
        {
            projectId = Session["ProjectId"].ToString();
            Response.Redirect("Project.aspx?ProjectEditId=" + projectId);
        }
        private string GetSortDirection(string column)
        {
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
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlYear.SelectedItem.Value) != DateTime.Now.Year)
                ddlMonth.SelectedIndex = 0;
            else
                ddlMonth.SelectedIndex = DateTime.Now.Date.Month - 1;

            GetWeek(); // Method to populate the weeks in selected Month and Year...
            year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            month = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
            month = month - 1;
            period = ddlWeek.SelectedItem.Text.ToString();
            weekIndex = ddlWeek.SelectedIndex;
            Label7.Text = "";
            lblMsgStatus.Text = string.Empty;
            lblMsgStatus1.Text = string.Empty;
            lblPjct.Text = string.Empty;

            Session["Year"] = year;
            Session["Month"] = month;
            Session["Period"] = period;
            Session["WeekIndex"] = weekIndex;

            ClearControls(); // Method to clear controls...           
            DisplayResourcedetails();  // Method to display resource details...
            SplitOverlappingWeek();   // Method to split overlapping week
            lblWeek.Text = ddlWeek.SelectedItem.Text.ToString() + ' ' + ',' + ' ' + ddlMonth.SelectedItem.Text.ToString() + ' ' + ',' + ' ' + ddlYear.SelectedItem.Text.ToString();
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetWeek();  // Method to populate the weeks in selected Month and Year...

            year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            month = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
            month = month - 1;
            period = ddlWeek.SelectedItem.Text.ToString();

            weekIndex = ddlWeek.SelectedIndex;
            Label7.Text = "";
            Session["Year"] = year;
            Session["Month"] = month;
            Session["Period"] = period;
            Session["WeekIndex"] = weekIndex;

            lblMsgStatus.Text = string.Empty;
            lblMsgStatus1.Text = string.Empty;
            lblPjct.Text = string.Empty;

            ClearControls(); // Method to clear controls...
            DisplayResourcedetails(); // Method to display resource details...
            SplitOverlappingWeek();  // Method to split overlapping week

            lblWeek.Text = ddlWeek.SelectedItem.Text.ToString() + ' ' + ',' + ' ' + ddlMonth.SelectedItem.Text.ToString() + ' ' + ',' + ' ' + ddlYear.SelectedItem.Text.ToString();
        }
        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label7.Text = "";
            year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            month = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
            month = month - 1;
            period = ddlWeek.SelectedItem.Text.ToString();
            weekIndex = ddlWeek.SelectedIndex;

            lblMsgStatus.Text = string.Empty;
            lblMsgStatus1.Text = string.Empty;
            lblPjct.Text = string.Empty;

            Session["Year"] = year;
            Session["Month"] = month;
            Session["Period"] = period;
            Session["WeekIndex"] = weekIndex;

            ClearControls(); // Method to clear controls...
            DisplayResourcedetails();  // Method to display resource details...

            lblWeek.Text = ddlWeek.SelectedItem.Text.ToString() + ' ' + ',' + ' ' + ddlMonth.SelectedItem.Text.ToString() + ' ' + ',' + ' ' + ddlYear.SelectedItem.Text.ToString();

            //diasble finalized time period

            string from = Session["dayfrom"].ToString();
            string to = Session["dayto"].ToString();
            string[] arrfrom = from.Split('-');
            string[] arrto = to.Split('-');
            string timeperiod = arrfrom[2] + "/" + arrfrom[1] + "/" + arrfrom[0] + "-" + arrto[2] + "/" + arrto[1] + "/" + arrto[0];
            DataTable dt = PMOscar.BaseDAL.ExecuteDataTable("SELECT Status FROM Dashboard where Name ='" + timeperiod + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Status"].ToString() == "F")
                {
                    btnSave.Enabled = false;
                    Label7.Text = "Dashboard for this period has already been finalized, so you won't be able to make any edits here";
                    dgProjectDetails.Columns[8].Visible = false;
                    dgProjectDetails.Columns[9].Visible = false;
                }
                else
                {
                    btnSave.Enabled = true;
                    Label7.Text = "";
                    dgProjectDetails.Columns[8].Visible = true;
                    dgProjectDetails.Columns[9].Visible = true;
                }
            }
            else
            {
                Label7.Text = "";
                btnSave.Enabled = true;
            }
            //diasble finalized time period
            SplitOverlappingWeek();  // Method to split overlapping week
        }
        protected void gvProjectDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtProjectDetails = Session["gvProjectDetails"] as DataTable;

            if (dtProjectDetails != null)
            {
                dtProjectDetails.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                dgProjectDetails.DataSource = dtProjectDetails;
                if (Label7.Text != string.Empty)
                {
                    dgProjectDetails.Columns[8].Visible = false;
                    dgProjectDetails.Columns[9].Visible = false;
                }
                totalActual = 0;
                totalEstimated = 0;
                dgProjectDetails.DataBind();
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlResources control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlResources_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dtRole = PMOscar.BaseDAL.ExecuteDataTable("Select RoleId from [Resource] where ResourceId=" + ddlResources.SelectedItem.Value);
            if (dtRole.Rows.Count > 0)
                ddlRole.Text = dtRole.Rows[0].ItemArray[0].ToString();


            //select team for the resource
            ddlTeam.ClearSelection();
            string resourceId = ddlResources.SelectedValue;
            List<SqlParameter> parameterTeam = new List<SqlParameter>();
            parameterTeam.Add(new SqlParameter("@ResourceID", resourceId.ToString()));
            DataSet dsTeamDetails = PMOscar.BaseDAL.ExecuteSPDataSet("[GetTeamByResourceID]", parameterTeam);
            if (dsTeamDetails.Tables.Count > 0)
            {
                DataTable dt = dsTeamDetails.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string team = dt.Rows[0]["Team"].ToString();
                    ddlTeam.Items.FindByText(team).Selected = true;
                }
            }

            CheckEntryForResource();
        }
        protected void lnkChng_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())    // If page not Refreshed
            {
                //=============== On click event code =========================
                if (timeTrackerId == 0)
                {
                    if (statusFlag == false)
                        SaveResourceDetails();     // Method to save the resource details...                 
                    else
                    {
                        lblPjct.Visible = true;
                        lblPjct.Text = "Entry cannot be Added since Project is Inactive";

                    }

                }
                else
                {
                    UpdateResourceDetails();   // Method to update the resource details...
                    timeTrackerId = 0;
                    Session["TimeTrackerId"] = null;
                }

                //=============== End of On click event code ==================

                // After the event/ method, again update the session  
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                DisplayResourcedetails();   // Method to display resource details...
            }
            else  // If Page Refreshed
            {
                // Do nothing 
            }

            if (statusFlag == false)
            {
                ClearControls(); // Method to clear controls...
            }

            year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            month = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
            month = month - 1;
            period = ddlWeek.SelectedItem.Text.ToString();
            weekIndex = ddlWeek.SelectedIndex;
            ddlResources.Focus();
            dgProjectDetails.Enabled = true;
            SplitOverlappingWeek();
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            int resourceId = Convert.ToInt32(ddlResources.SelectedValue);
            if (resourceId != 0)
            {
                string from = Session["dayfrom"].ToString();
                string to = Session["dayto"].ToString();
                string[] arrfrom = from.Split('-');
                string[] arrto = to.Split('-');
                string timeperiod = arrfrom[2] + "/" + arrfrom[1] + "/" + arrfrom[0] + "-" + arrto[2] + "/" + arrto[1] + "/" + arrto[0];
                DataTable dt = PMOscar.BaseDAL.ExecuteDataTable("SELECT Status FROM Dashboard where Name ='" + timeperiod + "'");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Status"].ToString() == "F")
                    {
                        btnSave.Enabled = false;
                        Label7.Text = "Dashboard for this period has already been finalized, so you won't be able to make any edits here";
                        dgProjectDetails.Columns[8].Visible = false;
                        dgProjectDetails.Columns[9].Visible = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        Label7.Text = "";

                    }
                }
                else
                {
                    Label7.Text = "";
                    btnSave.Enabled = true;
                }
            }

            CheckEntryForResource();
            SplitOverlappingWeek();
        }

        protected void ddlPhase_SelectedIndexChanged(object sender, EventArgs e)
        {

            int resourceId = Convert.ToInt32(ddlResources.SelectedValue);
            if (resourceId != 0)
            {
                string from = Session["dayfrom"].ToString();
                string to = Session["dayto"].ToString();
                string[] arrfrom = from.Split('-');
                string[] arrto = to.Split('-');
                string timeperiod = arrfrom[2] + "/" + arrfrom[1] + "/" + arrfrom[0] + "-" + arrto[2] + "/" + arrto[1] + "/" + arrto[0];
                DataTable dt = PMOscar.BaseDAL.ExecuteDataTable("SELECT Status FROM Dashboard where Name ='" + timeperiod + "'");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Status"].ToString() == "F")
                    {
                        btnSave.Enabled = false;
                        Label7.Text = "Dashboard for this period has already been finalized, so you won't be able to make any edits here";
                        dgProjectDetails.Columns[8].Visible = false;
                        dgProjectDetails.Columns[9].Visible = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        Label7.Text = "";
                        dgProjectDetails.Columns[8].Visible = true;
                        dgProjectDetails.Columns[9].Visible = true;
                    }
                }
                else
                {
                    Label7.Text = "";
                    btnSave.Enabled = true;
                }
            }
            CheckEntryForResource();
            SplitOverlappingWeek();
        }

        protected bool GetTotalHoursOfResourceInProject(int resourceId, int timeTrackerId, DateTime fromDate, DateTime toDate, string estimatedTime, string actualTime, int mode, out int exceededHours)
        {
            exceededHours = 0;
            bool canInsert = false;
            int estTime = estimatedTime == string.Empty ? 0 : Convert.ToInt32(estimatedTime);
            decimal actTime = actualTime == string.Empty ? 0 : Convert.ToDecimal(actualTime);
            try
            {
                int totalHours = 0;
                int estimatedHourValue = 0;
                decimal actualHourValue = 0;
                DateTime d1 = Convert.ToDateTime(fromDate);
                DateTime d2 = Convert.ToDateTime(toDate).AddDays(1);

                TimeSpan t = d2 - d1;
                double NrOfDays = t.TotalDays;
                int numOfWorkingDays = 0;
                numOfWorkingDays = Convert.ToInt32(NrOfDays);
                totalHours = numOfWorkingDays * 8;
                if (numOfWorkingDays > 0)
                {
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@ResourceId", resourceId));
                    parameter.Add(new SqlParameter("@TimeTrackerId", timeTrackerId));
                    parameter.Add(new SqlParameter("@FromDate", fromDate));
                    parameter.Add(new SqlParameter("@ToDate", toDate));
                    parameter.Add(new SqlParameter("@Mode", mode));
                    DataTable dtTimeTrackerDetails = BaseDAL.ExecuteSPDataTable("GetTotalHoursOfResource", parameter);

                    if (dtTimeTrackerDetails.Rows.Count > 0)
                    {
                        estimatedHourValue = Convert.ToInt32(dtTimeTrackerDetails.Rows[0]["EstimatedHours"].ToString());
                        actualHourValue = Convert.ToDecimal(dtTimeTrackerDetails.Rows[0]["ActualHours"].ToString());

                        if (estTime != 0 && actTime != 0)
                        {
                            if (estimatedHourValue <= totalHours && actualHourValue <= totalHours)
                            {
                                int totalEstimatedHours = estimatedHourValue + estTime;
                                decimal totalActualHours = actualHourValue + actTime;

                                if (totalEstimatedHours <= totalHours && totalActualHours <= totalHours)
                                {
                                    canInsert = true;
                                }
                                else
                                {
                                    canInsert = false;
                                    exceededHours = totalHours;
                                }
                            }
                            else
                            {
                                canInsert = false;
                                exceededHours = totalHours;
                            }
                        }
                        else if (estTime == 0)
                        {
                            if (actualHourValue <= totalHours)
                            {
                                decimal totalActualHours = actualHourValue + actTime;

                                if (totalActualHours <= totalHours)
                                {
                                    canInsert = true;
                                }
                                else
                                {
                                    canInsert = false;
                                    exceededHours = totalHours;
                                }
                            }
                            else
                            {
                                canInsert = false;
                                exceededHours = totalHours;
                            }
                        }
                        else if (actTime == 0)
                        {
                            if (actualHourValue <= totalHours)
                            {
                                int totalEstimatedHours = estimatedHourValue + estTime;

                                if (totalEstimatedHours <= totalHours)
                                {
                                    canInsert = true;
                                }
                                else
                                {
                                    canInsert = false;
                                    exceededHours = totalHours;
                                }
                            }
                            else
                            {
                                canInsert = false;
                                exceededHours = totalHours;
                            }
                        }
                    }
                    else
                    {
                        if (estTime <= totalHours && actTime <= totalHours)
                        {
                            canInsert = true;
                        }
                        else
                        {
                            canInsert = false;
                            exceededHours = totalHours;
                        }
                    }
                }
                else
                {
                    if (estTime <= totalHours && actTime <= totalHours)
                    {
                        canInsert = true;
                    }
                    else
                    {
                        canInsert = false;
                        exceededHours = totalHours;
                    } 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
            return canInsert;
        }
        #endregion
    }
}
