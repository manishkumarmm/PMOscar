// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourcePlanningEntry.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
//  Description  : 
//  Author       : 
//  Created Date : 20 April 2011
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
    public partial class ResourcePlanningEntry : System.Web.UI.Page
    {
        #region"Declarations"

        public int editFlag = 0;
        public int sessionTimeTrackerId = 0;
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
        public int maxEstimation = 0;
        public int calcEstimation = 0;
        string dayFrom = string.Empty;
        string dayTo = string.Empty;
        int actualHourUpdate = 0;

        public IList<SqlParameter> parameter;
        private int editEstimatedHours;
        private float editActualHours;
        private int updateFlag = 0;
        private string Status = string.Empty;
        private decimal RolePercentage;
        private int EditStatusId;
        private long timeTrackerId;
        private int deleteProjectId;
        public string resourceId;
        public string resourceID;
        public string statusOfproject;
        public bool isValid = false;
        public int functioncount = 0;

        double totalEstimated = 0;
        double totalActual = 0;
        private int year = 0;
        private int month = 0;
        private string period = null;
        private int weekindex = 0;
        string url = string.Empty;
        int frommonth = 0;
        int tomonth = 0;
        int fromyear = 0;
        int toyear = 0;
        private Boolean statusFlag = false;
        private static bool isOverlappingWeek = false;
        Object PID;
        public int validation1 = 0;
        public int validation2 = 0;
       

        #endregion

        #region"Page Events"

        protected void Page_Load(object sender, EventArgs e)
        {

            lblInActiveRes.Visible = false;
            dgProjectDetails.Enabled = true;
            txtEHours.Enabled = false;
            txtEHours2.Enabled = false;
            txtAHours.Enabled = false;
            txtAHours2.Enabled = false;
            lblesimation2.Visible = false;
            lblesimation2.Visible = false;
            lblFuture.Visible = false;
            if (Session["UserName"] == null)
                Response.Redirect("Default.aspx");
            else
            {

                if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 4=Employee)
                {
                    #region
                    //Only User tab is Visible for Sys admin
                    ddlPhase.Enabled = false;
                    ddlRole.Enabled = false;
                    ddlTeam.Enabled = false;
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
            txtEHours2.Attributes.Add("onkeypress", "return CheckNemericValue(event,this);");

            try
            {

                if (ddlYear.SelectedValue.Trim() != Session["Year"].ToString().Trim() && ddlYear.SelectedValue.Trim() != "")

                {
                    year = Convert.ToInt32(ddlYear.SelectedValue);
                }
                else
                {
                    year = Convert.ToInt32(Session["Year"]);
                }
                if (ddlMonth.SelectedValue.Trim() != Session["Month"].ToString().Trim() && ddlMonth.SelectedValue.Trim() != "")

                {


                    month = Convert.ToInt32(ddlMonth.SelectedIndex);
                    ViewState["index"] = ddlMonth.SelectedValue;

                }
                else
                {
                    month = Convert.ToInt32(Session["Month"]);
                    ViewState["index"] = ddlMonth.SelectedValue;

                }
                if (ddlWeek.SelectedValue.Trim() != Session["Period"].ToString().Trim() && ddlWeek.SelectedValue.Trim() != "")

                {
                    period = ddlWeek.SelectedValue;
                }
                else
                {
                    period = Session["Period"].ToString();
                }
                if (ddlWeek.SelectedIndex != Convert.ToInt16(Session["WeekIndex"]))

                {
                    weekindex = 0;

                }
                else
                {
                    weekindex = Convert.ToInt16(Session["WeekIndex"]);

                }
                if ((ddlWeek.SelectedValue.Trim() != Session["Period"].ToString().Trim() && ddlWeek.SelectedValue.Trim() != "") || (ddlYear.SelectedValue.Trim() != Session["Year"].ToString().Trim() && ddlYear.SelectedValue.Trim() != "") || (ddlMonth.SelectedValue.Trim() != Session["Month"].ToString().Trim() && ddlMonth.SelectedValue.Trim() != ""))
                {
                    period = period + " ";
                    SetPeriod();
                    Session["dayfromTA"] = Session["dayfrom"];
                    Session["daytoTA"] = Session["dayto"];

                }
            }
            catch (Exception ex)
            {

            }

            string resourceIDTeam = string.Empty;
            try
            {
                if (Request.QueryString["op"] == "E")
                {
                    resourceId = Request.QueryString["ResEditId"].ToString() == "" ? "0".ToString() : Request.QueryString["ResEditId"].ToString();
                }
                else if (Request.QueryString["op"] == "D")
                {
                    resourceId = Request.QueryString["ResDeleteId"].ToString() == "" ? "0".ToString() : Request.QueryString["ResDeleteId"].ToString();
                }
                else
                {
                    resourceId = Request.QueryString["Id"].ToString() == "" ? "0".ToString() : Request.QueryString["id"].ToString();
                }
            }
            catch
            {
                resourceIDTeam = "0".ToString();
                Response.Redirect("Default.aspx");
            }

            if (!Page.IsPostBack)
            {
                FillYears();  // Method to bind year in DropDownList...

                ddlProjects.Focus();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString()); //........

                year = (Request.QueryString["year"] != null) ? Convert.ToInt32(Request.QueryString["year"]) : 0;
                month = (Request.QueryString["month"] != null) ? Convert.ToInt32(Request.QueryString["month"]) : 0;
                period = (Request.QueryString["week"] != null) ? Convert.ToString(Request.QueryString["week"]) : string.Empty;
                weekindex = (Request.QueryString["index"] != null) ? Convert.ToInt16(Request.QueryString["index"]) : 0;

                BindDropDownResources(); // Method to bind resources in the DropDownList...
                BindDropDownProjects(); // Method to bind projects in the DropDownList...
                BindDropDownRole(); // Method to bind roles in the DropDownList...
                BindDropDownPhase();// Method to bind phases in the DropDownList...

                //Setting default team for the resource

                BindDropDownTeams(resourceId);//Method to bind teams in the DropdownList..

                if (ddlResources.Items.FindByValue(resourceId.Trim()) != null && !string.IsNullOrEmpty(resourceId.Trim()))
                {
                    ddlResources.Items.FindByValue(resourceId.Trim()).Selected = true;
                }

                timeTrackerId = (Request.QueryString["TTId"] != null) ? Convert.ToInt32(Request.QueryString["TTId"]) : 0;
                deleteProjectId = (Request.QueryString["DelProjId"] != null) ? Convert.ToInt32(Request.QueryString["DelProjId"]) : 0;


                weekindex = (Request.QueryString["index"] != null) ? Convert.ToInt16(Request.QueryString["index"]) : 0;

                ConfigureForInActiveResource(resourceId);  // Method to find Inactive resources...
                GetWeek(); // Method to populate the weeks in selected Month and Year...

                ddlResources.SelectedValue = resourceId;

                if (Request.QueryString["op"] != "E" && Request.QueryString["op"] != "D")
                {
                    year = (Request.QueryString["year"] != null) ? Convert.ToInt32(Request.QueryString["year"]) : 0;
                    month = (Request.QueryString["month"] != null) ? Convert.ToInt32(Request.QueryString["month"]) : 0;
                    period = (Request.QueryString["week"] != null) ? Convert.ToString(Request.QueryString["week"]) : string.Empty;
                    weekindex = (Request.QueryString["index"] != null) ? Convert.ToInt16(Request.QueryString["index"]) : 0;

                    int newmonth = month + 1;
                    ddlYear.SelectedValue = year.ToString();
                    ddlMonth.SelectedValue = (month + 1).ToString();

                    GetWeek(); // Method to populate the weeks in selected Month and Year...

                    ddlWeek.SelectedIndex = weekindex;

                    Session["Year"] = year;
                    Session["Month"] = month;
                    Session["Period"] = period;
                    Session["WeekIndex"] = weekindex;

                    DisplayProjectDetails(resourceId); // Method to display project details...
                    if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 4=Employee)
                    {
                        ddlPhase.Enabled = false;
                    }
                    else if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 4=Employee)
                    {
                        if (ddlProjects.SelectedItem.Text == "Admin" || ddlProjects.SelectedItem.Text == "Open")
                        {
                            ddlPhase.Enabled = true;
                        }
                        else
                        {
                            ddlPhase.Enabled = true;
                        }
                    }

                    BindGridView(resourceId, year);

                }

                SplitOverlappingWeek(); // Method to split overlapping week
                System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
                lblWeek.Text = period + ' ' + ',' + ' ' + mfi.GetMonthName(month + 1).ToString() + ' ' + ',' + ' ' + year;
                //edit
                if (Request.QueryString["op"] != null && Request.QueryString["op"] == "E" && timeTrackerId != 0)
                {

                    year = (Request.QueryString["year"] != null) ? Convert.ToInt32(Request.QueryString["year"]) : 0;
                    month = (Request.QueryString["month"] != null) ? Convert.ToInt32(Request.QueryString["month"]) : 0;
                    period = (Request.QueryString["week"] != null) ? Convert.ToString(Request.QueryString["week"]) : string.Empty;
                    weekindex = (Request.QueryString["index"] != null) ? Convert.ToInt16(Request.QueryString["index"]) : 0;

                    int newmonth = Convert.ToInt32(Session["Month"]);
                    ddlYear.SelectedValue = Session["Year"].ToString();
                    ddlMonth.SelectedValue = (newmonth + 1).ToString();

                    GetWeek();  // Method to populate the weeks in selected Month and Year...

                    ddlWeek.SelectedIndex = Convert.ToInt16(Session["WeekIndex"]);
                    period = Session["Period"].ToString();
                    year = Convert.ToInt32(Session["Year"]);
                    month = Convert.ToInt32(Session["Month"]);
                    period = Session["Period"].ToString();
                    weekindex = Convert.ToInt16(Session["WeekIndex"]);

                    lblWeek.Text = period + ' ' + ',' + ' ' + mfi.GetMonthName(month + 1).ToString() + ' ' + ',' + ' ' + year;

                    resourceId = Request.QueryString["ResEditId"].ToString() == "" ? resourceId : Request.QueryString["ResEditId"];


                    ConfigureForInActiveResource(resourceId);  // Method to find Inactive resources...
                    GetResourceAssignedProjectDetails(); // Method to get Resource Assigned Project Details...

                    if (EditStatusId == 1)
                        timeTrackerId = 0;
                    else
                    {
                        ddlRole.Focus();
                        btnSave.Text = "Update";
                        Session["TimeTrackerId"] = timeTrackerId;
                    }


                    Session["sessionTimeTrackerId"] = 1;

                    dgProjectDetails.Enabled = false;
                    DisplayProjectDetails(resourceId); // Method to display project details...
                    CheckAllDropdownSelected();
                    if (lblesimation2.Visible)
                    {
                        txtEHours2.Enabled = false;
                        txtAHours2.Enabled = false;
                        lblEstimation.Visible = false;
                        lblMsgStatus.Visible = false;
                    }
                    if (lblEstimation.Visible)
                    {
                        txtEHours2.Enabled = false;
                        txtAHours2.Enabled = false;
                        txtEHours.Enabled = false;
                        txtAHours.Enabled = false;
                    }
                    if (lblFuture.Visible)
                    {
                        txtEHours2.Enabled = false;
                        txtAHours2.Enabled = false;
                        txtEHours.Enabled = false;
                        txtAHours.Enabled = false;
                        lblEstimation.Visible = false;
                        lblesimation2.Visible = false;
                    }
                    if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 4=Employee)
                    {
                        ddlPhase.Enabled = false;
                    }
                    else if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 4=Employee)
                    {
                        if (ddlProjects.SelectedItem.Text == "Admin" || ddlProjects.SelectedItem.Text == "Open")
                        {
                            ddlPhase.Enabled = true;
                        }
                        else
                        {
                            ddlPhase.Enabled = true;
                        }
                    }



                }

                if (Request.QueryString["op"] != null && Request.QueryString["op"] == "D" && timeTrackerId != 0)
                {
                    year = (Request.QueryString["year"] != null) ? Convert.ToInt32(Request.QueryString["year"]) : 0;
                    month = (Request.QueryString["month"] != null) ? Convert.ToInt32(Request.QueryString["month"]) : 0;
                    period = (Request.QueryString["week"] != null) ? Convert.ToString(Request.QueryString["week"]) : string.Empty;
                    weekindex = (Request.QueryString["index"] != null) ? Convert.ToInt16(Request.QueryString["index"]) : 0;

                    int newmonth = Convert.ToInt32(Session["Month"]);
                    ddlYear.SelectedValue = Session["Year"].ToString();
                    ddlMonth.SelectedValue = (newmonth + 1).ToString();

                    GetWeek();  // Method to populate the weeks in selected Month and Year...

                    ddlWeek.SelectedIndex = Convert.ToInt32(Session["WeekIndex"]);

                    period = Session["Period"].ToString();
                    year = Convert.ToInt32(Session["Year"]);
                    month = Convert.ToInt32(Session["Month"]);
                    period = Session["Period"].ToString();
                    weekindex = Convert.ToInt16(Session["WeekIndex"]);
                    lblWeek.Text = period + ' ' + ',' + ' ' + mfi.GetMonthName(month + 1).ToString() + ' ' + ',' + ' ' + year;

                    resourceId = Request.QueryString["ResDeleteId"].ToString() == "" ? resourceId : Request.QueryString["ResDeleteId"];

                    ConfigureForInActiveResource(resourceId); // Method to find Inactive resources...

                    if (statusFlag == false)
                        GetInActiveProject(); // Method to find Inactive projects...
                    else
                    {
                        lblPjct.Visible = true;
                        lblPjct.Text = "Entry cannot be deleted since Resource is inactive.";
                    }

                    timeTrackerId = 0;

                    Session["sessionTimeTrackerId"] = 0;

                    DisplayProjectDetails(resourceId); // Method to display project details...
                    BindGridView(resourceId, year);
                }
            }

            else
            {
                timeTrackerId = Session["TimeTrackerId"] != null ? Convert.ToInt32(Session["TimeTrackerId"]) : 0;
                lblMsgStatus.Text = "";
                lblMsgStatus1.Text = "";
                lblError.Text = string.Empty;
                lblError1.Text = string.Empty;

                if (ddlProjects.SelectedItem.Text == "Admin" || ddlProjects.SelectedItem.Text == "Open")
                {
                    ddlPhase.Enabled = true;
                    RqResEntry.Enabled = true;
                }
                else
                    RqResEntry.Enabled = true;

                ConfigureForInActiveResource(resourceId); // Method to find Inactive resources...

            }
            //diasble finalized tiem period
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
                        Label7.Text = "Dashboard for this period has already been finalized, so you won't be able to make any edits here.";
                        dgProjectDetails.Columns[8].Visible = false;
                        dgProjectDetails.Columns[9].Visible = false;
                        lblError2.Text = "";
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        Label7.Text = "";
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

        /// <summary>
        /// Binds the drop down teams.
        /// </summary>
        private void BindDropDownTeams(string resourceId)
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

                //default binding for teams
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

            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region"Methods"

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

        // Method to do the sorting...
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

        // Method to delete the Resource Assigned Details in TimeTracker...
        private void DeleteOperation()
        {
            resourceId = ddlResources.SelectedValue.ToString();
            int timeTrackerId = (Request.QueryString["TTId"] != null) ? Convert.ToInt32(Request.QueryString["TTId"]) : 0;

            if (timeTrackerId > 0)
            {
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@TimeTrackerID", timeTrackerId));
                parameter.Add(new SqlParameter("@Proj_Id", Convert.ToInt32(ddlProjects.SelectedValue.ToString())));
                parameter.Add(new SqlParameter("@Res_Id", Convert.ToInt32(resourceId)));
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
                parameter.Add(new SqlParameter("@ActualHours_Updated", actualHourUpdate));
                try
                {
                    int TimeTrackerId = BaseDAL.ExecuteSPNonQuery("ResourcePlanningOperations", parameter);
                }
                catch (Exception ex)
                {

                }
            }
        }

        // Method to bind roles in the DropDownList...
        private void BindDropDownRole()
        {
            var UID = Session["UserRoleID"];
            parameter = new List<SqlParameter>();
            DataTable dtRoles = BaseDAL.ExecuteSPDataTable("GetRole", parameter);
            ddlRole.DataSource = dtRoles;
            ddlRole.DataTextField = "Role";
            ddlRole.DataValueField = "RoleId";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("Select", "0"));
            if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {
                ddlTeam.Enabled = false;
            }
        }

        // Method to bind phases in the DropDownList...


        private void BindDropDownPhase()
        {
            parameter = new List<SqlParameter>();
            var UID = Session["UserRoleID"];
           

            DataTable dtPhases = BaseDAL.ExecuteSPPhaseDataTable("GetPhase", parameter);
            ddlPhase.DataSource = dtPhases;
            ddlPhase.DataTextField = "Phase";
            ddlPhase.DataValueField = "PhaseID";
            ddlPhase.DataBind();
            ddlPhase.Items.Insert(0, new ListItem("Select", "0"));

            if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)

            {
                var Project = ddlProjects.SelectedItem.Text;
                string query = string.Format("select PhaseID  from [dbo].[Project] where ProjectName='{0}'", Project);
                PID = PMOscar.BaseDAL.ExecuteScalar(query);
                if (PID != null)
                {
                      ddlPhase.SelectedValue = PID.ToString();
                }
            }
            else if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)

            {
                ddlRole.Enabled = false;
                var Project = ddlProjects.SelectedItem.Text;
                string query = string.Format("select PhaseID  from [dbo].[Project] where ProjectName='{0}'", Project);
                PID = PMOscar.BaseDAL.ExecuteScalar(query);
               // parameter.Add(new SqlParameter("@PhaseID", PID));

                if (PID != null)
                {
                    ddlPhase.SelectedValue = PID.ToString();
                    ddlRole.Enabled = true;
                }
               
            }
        }

        public void BindGridView(string ResourceId, int Year)
        {


            SqlParameter[] parameter = new SqlParameter[6];


            parameter[0] = new SqlParameter("@ResourceID", ResourceId);
            parameter[1] = new SqlParameter("@dayfrom", Session["dayfrom"]);
            parameter[2] = new SqlParameter("@dayto", Session["dayto"]);
            parameter[3] = new SqlParameter("@Year", Year);
            parameter[4] = new SqlParameter("@Status", 1);
            parameter[5] = new SqlParameter("@ProjectID", Convert.ToInt32(Session["ProjectIdTA"]));


            DataSet dsProjectDetails = BaseDAL.ExecuteSPDataSet("GetTimeTrackerAudit", parameter);

            if (dsProjectDetails != null && dsProjectDetails.Tables.Count > 0)
            {
                gvResource.DataSource = dsProjectDetails.Tables[0] as DataTable;
                gvResource.DataBind();
            }

        }


        // Method to find Inactive projects...
        private void GetInActiveProject()
        {
            DataSet dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select TimeTracker.ProjectId,TimeTracker.RoleId,Project.ProjectName,Role.Role," +
                " TimeTracker.EstimatedHours As EstimatedHours,TimeTracker.ActualHours As ActualHours,Phase.PhaseID,TimeTracker.WeeklyComments" +
                " From [Resource] as R left Join TimeTracker on TimeTracker.ResourceId = R.ResourceId" +
               " Inner join Role on TimeTracker.RoleId = Role.RoleId inner join Project on Project.ProjectId = TimeTracker.ProjectId" +
              " left Join Phase on TimeTracker.PhaseID = Phase.PhaseID Where Project.IsActive=1 And TimeTracker.TimeTrackerId=" + timeTrackerId + " AND R.ResourceId = " + Convert.ToInt32(ddlResources.SelectedValue));

            if (dsTimeTrackerDetails.Tables[0].Rows.Count > 0)
            {
                DeleteOperation();
                EditStatusId = 0;
            }

            else
            {
                EditStatusId = 0;
                lblPjct.Visible = true;
                lblPjct.Text = "Project cannot be deleted since project is Inactive.";
            }
        }

        // Method to get Resource Assigned Project Details...
        private void GetResourceAssignedProjectDetails()
        {
            if (statusFlag == true)
            {
                lblPjct.Visible = true;
                lblPjct.Text = "Project Entry cannot be updated since Resource is Inactive.";
                return;
            }

            DataSet dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select TimeTracker.ProjectId,TimeTracker.RoleId,Project.ProjectName,Role.Role," +
                " TimeTracker.EstimatedHours As EstimatedHours,TimeTracker.ActualHours As ActualHours,Phase.PhaseID,TimeTracker.WeeklyComments,TimeTracker.TeamID,TimeTracker.FromDate,TimeTracker.ToDate" +
                " From [Resource] as R left Join TimeTracker on TimeTracker.ResourceId = R.ResourceId" +
               " Inner join Role on TimeTracker.RoleId = Role.RoleId inner join Project on Project.ProjectId = TimeTracker.ProjectId" +
              " left Join Phase on TimeTracker.PhaseID = Phase.PhaseID Where Project.IsActive=1 And TimeTracker.TimeTrackerId=" + timeTrackerId + " AND R.ResourceId = " + Convert.ToInt32(ddlResources.SelectedValue));

            if (dsTimeTrackerDetails.Tables[0].Rows.Count > 0)
            {
                txtAHours.Text = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[5].ToString();
                Session["actualHour"] = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[5].ToString();
                txtEHours.Text = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[4].ToString();
                ddlProjects.Text = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[0].ToString();
                ddlRole.Text = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[1].ToString();

                if (ddlProjects.SelectedItem.Text == "Open" || ddlProjects.SelectedItem.Text == "Admin")
                    ddlPhase.Enabled = true;

                else
                    ddlPhase.Enabled = true;

                ddlPhase.Text = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[6].ToString();



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
                /*** Configuration Ends***/

                if (dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[4].ToString() == string.Empty)
                    editEstimatedHours = 0;

                else
                    editEstimatedHours = Convert.ToInt32(dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[4].ToString());

                if (dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[5].ToString() == string.Empty)
                    editActualHours = 0;

                else
                    editActualHours = float.Parse(dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[5].ToString());
                string replaced = dsTimeTrackerDetails.Tables[0].Rows[0].ItemArray[7].ToString().Replace("&#x0D;\n", " ");
                txtComments.Text = replaced;
                EditStatusId = 0;
            }

            else
            {
                EditStatusId = 1;
                lblPjct.Visible = true;
                lblPjct.Text = "Project cannot be updated since project is Inactive.";

            }

            if (ddlProjects.SelectedItem.Text == "Admin" || ddlProjects.SelectedItem.Text == "Open")
            {
                ddlPhase.Enabled = true;
                RqResEntry.Enabled = false;
            }
            else
                RqResEntry.Enabled = true;
        }

        // Method to set the actual and budjet hours to hidden fields...
        private void GetHours()
        {
            int projectId = Convert.ToInt32(ddlProjects.SelectedValue.ToString());
            DataSet dsBudgetHours = BaseDAL.ExecuteDataSet("Select Sum(BudgetHours) From dbo.ProjectEstimation Where ProjectID =" + projectId);
            DataSet dsActualHours = BaseDAL.ExecuteDataSet("Select Sum(ActualHours) From dbo.TimeTracker Where ProjectID =" + projectId);

            if (dsBudgetHours.Tables[0].Rows.Count > 0)
                HfBudgetHours.Value = dsBudgetHours.Tables[0].Rows[0].ItemArray[0].ToString();

            if (dsActualHours.Tables[0].Rows.Count > 0)
                HFActualHours.Value = dsActualHours.Tables[0].Rows[0].ItemArray[0].ToString();
        }

        // Method to check the validation on hours to apply red colur on values...
        private void ValidationCheckingOnHours()
        {
            lblError1.Visible = false;
            lblMsgStatus.Visible = false;
            lblMsgStatus1.Visible = false;
            int revisedBudgetHours = 0;
            int billableHours = 0;
            decimal totalActualHoursForActual = 0;
            decimal totalActualHoursForEstimated = 0;
            int totalEstimatedHours = 0;
            decimal estimatedHoursCheck = 0;
            int estimationRoleId = 0;
            int projectId = Convert.ToInt32(ddlProjects.SelectedValue.ToString());
            string projectName = ddlProjects.SelectedItem.Text.ToString();
            int roleId = Convert.ToInt32(ddlRole.SelectedValue.ToString());
            string month = (ddlMonth.SelectedIndex + 1).ToString();

            DataSet dsEstimationRoleId = BaseDAL.ExecuteDataSet("Select EstimationRoleID From Role Where RoleId =" + roleId);

            if (dsEstimationRoleId.Tables[0].Rows.Count > 0)
                estimationRoleId = Convert.ToInt32(dsEstimationRoleId.Tables[0].Rows[0].ItemArray[0].ToString());

            int TTId = (Request.QueryString["TTId"] != null) ? Convert.ToInt32(Request.QueryString["TTId"]) : 0;
            int PhaseId = Convert.ToInt32(ddlPhase.SelectedValue.ToString());

            DataSet dsAcutalHoursForEstimated = BaseDAL.ExecuteDataSet("select isnull(sum(case when ActualHours is null then EstimatedHours *  r.EstimationPercentage " +
            " else ActualHours *  r.EstimationPercentage  end),0) as TotHours from  Timetracker tt join [Role] r on r.RoleID=tt.RoleID " +
            " join EstimationRole er on er.EstimationRoleID=r.EstimationRoleID where tt.ProjectID= " + projectId + " and tt.PhaseID= " + PhaseId + "and MONTH(tt.CreatedDate)= " + month + " " +
            " and er.EstimationRoleID=" + estimationRoleId + " And tt.TimeTrackerId <> " + TTId + " group by  tt.ProjectID ,tt.PhaseID ,er.EstimationRoleID,  r.EstimationPercentage");

            DataSet Estimated = BaseDAL.ExecuteDataSet("select isnull(sum(EstimatedHours *  r.EstimationPercentage),0) as TotHours from  Timetracker tt join [Role] r on r.RoleID=tt.RoleID " +
            " join EstimationRole er on er.EstimationRoleID=r.EstimationRoleID where tt.ProjectID= " + projectId + " and tt.PhaseID= " + PhaseId + "and MONTH(tt.CreatedDate)= " + month + " " +
            " and er.EstimationRoleID=" + estimationRoleId + " And tt.TimeTrackerId <> " + TTId + " group by  tt.ProjectID ,tt.PhaseID ,er.EstimationRoleID,  r.EstimationPercentage");

            if (Estimated.Tables[0].Rows.Count > 0)
                estimatedHoursCheck = Convert.ToDecimal(Estimated.Tables[0].Rows[0].ItemArray[0]);


            if (dsAcutalHoursForEstimated.Tables[0].Rows.Count > 0)
                totalActualHoursForEstimated = Convert.ToDecimal(dsAcutalHoursForEstimated.Tables[0].Rows[0].ItemArray[0]);

            else
                totalActualHoursForEstimated = 0;

            DataSet dsAcutalHours = BaseDAL.ExecuteDataSet("select Isnull(sum(ActualHours * r.EstimationPercentage),0) as ActualHours " +
            " from TimeTracker tt Join [Role] r on r.roleID=tt.RoleID join EstimationRole er on er.EstimationRoleID=r.EstimationRoleID" +
            " where tt.ProjectID= " + projectId + " and tt.PhaseID= " + PhaseId + "and MONTH(tt.CreatedDate)= " + month + " and er.EstimationRoleID=" + estimationRoleId + " ");


            if (dsAcutalHours.Tables[0].Rows.Count > 0)
                totalActualHoursForActual = Convert.ToDecimal(dsAcutalHours.Tables[0].Rows[0].ItemArray[0]);

            else
                totalActualHoursForActual = 0;

            DataSet dsEstimatedHours = BaseDAL.ExecuteDataSet("select Isnull(sum(EstimatedHours),0) as EstimatedHours " +
            " from TimeTracker tt Join [Role] r on r.roleID=tt.RoleID join EstimationRole er on er.EstimationRoleID=r.EstimationRoleID" +
            " where tt.ProjectID= " + projectId + " and tt.PhaseID= " + PhaseId + " and er.EstimationRoleID=" + estimationRoleId + " " +
            " And (Year(FromDate)= " + Session["Year"] + " " +
            " or Year(ToDate)=" + Session["Year"] + " and FromDate>='" + Session["dayfrom"] + "' and  ToDate<='" + Session["dayto"] + "')" +
            " group by tt.ProjectID,tt.PhaseID,er.[RoleName]");

            if (dsEstimatedHours.Tables[0].Rows.Count > 0)
                totalEstimatedHours = Convert.ToInt32(dsEstimatedHours.Tables[0].Rows[0].ItemArray[0]);

            else
                totalEstimatedHours = 0;

            DataSet dsRevisedBudgetHours = BaseDAL.ExecuteDataSet("Select Isnull(Sum(RevisedBudgetHours),0) From ProjectEstimation " +
            " Where ProjectID = " + projectId + " And PhaseID =  " + PhaseId + " And EstimationRoleID = " + estimationRoleId + "");

            if (dsRevisedBudgetHours.Tables[0].Rows.Count > 0)
                revisedBudgetHours = Convert.ToInt32(dsRevisedBudgetHours.Tables[0].Rows[0].ItemArray[0]);

            else
                revisedBudgetHours = 0;

            DataSet dsBillableHours = BaseDAL.ExecuteDataSet("SELECT  SUM(ISNULL(BillableHours,0))  from ProjectEstimation " +
                                              "WHERE ProjectID = " + projectId + " AND PhaseID = " + PhaseId + " AND  EstimationRoleID = " + estimationRoleId +
                                              " GROUP BY ProjectID, PhaseID, EstimationRoleID");

            if (dsBillableHours.Tables[0].Rows.Count > 0)
                billableHours = Convert.ToInt32(dsBillableHours.Tables[0].Rows[0].ItemArray[0]);

            else
                billableHours = 0;

            int estimatedHours = 0;
            decimal actualHours = 0;

            if (txtEHours.Text == string.Empty)
                estimatedHours = 0;

            else
                estimatedHours = Convert.ToInt32(txtEHours.Text);

            if (txtAHours.Text == string.Empty)
                actualHours = 0;

            else
            {
                actualHours = Convert.ToDecimal(txtAHours.Text);
            }

            decimal estimatedHoursPercentage = estimatedHours * RolePercentage;
            decimal actualHoursPercentage = actualHours * RolePercentage;
            decimal checkEstimatedHours = estimatedHoursPercentage + totalActualHoursForEstimated;
            decimal checkActualHours = actualHoursPercentage + totalActualHoursForEstimated;

            if (projectName == "Open" || projectName == "Admin")
                return;

            if ((txtAHours.Text == string.Empty) && (checkEstimatedHours > 0) && projectName != "Open" && projectName != "Admin")
            {
                if (revisedBudgetHours < (estimatedHoursCheck + estimatedHours))
                {
                    lblMsgStatus.Visible = true;
                    lblMsgStatus.Text = "Estimated Hours for " + ddlProjects.SelectedItem.Text + "-" + ddlPhase.SelectedItem.Text + "-" + ddlRole.SelectedItem.Text + " has exceeded the Revised Budget Hours.";
                    Status = "E";
                }

                if (billableHours < (estimatedHoursCheck + estimatedHours))
                {
                    lblError1.Visible = true;
                    lblError1.Text = "Estimated Hours for " + ddlProjects.SelectedItem.Text + "-" + ddlPhase.SelectedItem.Text + "-" + ddlRole.SelectedItem.Text + " has exceeded the Billable Hours.";

                }
            }

            else if (checkActualHours > 0 && projectName != "Open" && projectName != "Admin")
            {
                if (revisedBudgetHours < checkActualHours)
                {
                    lblMsgStatus1.Visible = true;
                    lblMsgStatus1.Text = "Actual Hours for " + ddlProjects.SelectedItem.Text + "-" + ddlPhase.SelectedItem.Text + "-" + ddlRole.SelectedItem.Text + " has exceeded the Revised Budget Hours.";
                    Status = "A";
                }



                if (billableHours < (estimatedHoursCheck + estimatedHours))
                {
                    lblError1.Visible = true;
                    lblError1.Text = "Estimated Hours for " + ddlProjects.SelectedItem.Text + "-" + ddlPhase.SelectedItem.Text + "-" + ddlRole.SelectedItem.Text + " has exceeded the Billable Hours.";

                }

                if (revisedBudgetHours < (estimatedHoursCheck + estimatedHours))
                {
                    lblMsgStatus.Visible = true;
                    lblMsgStatus.Text = "Estimated Hours for " + ddlProjects.SelectedItem.Text + "-" + ddlPhase.SelectedItem.Text + "-" + ddlRole.SelectedItem.Text + " has exceeded the Revised Budget Hours.";
                    Status = "E";
                }
            }

        }

        // Method to get the percentage values of the roles...
        private void GetRolePercentageValue()
        {
            int roleId = Convert.ToInt32(ddlRole.SelectedValue.ToString());
            DataSet dsRolePercentage = BaseDAL.ExecuteDataSet("Select EstimationPercentage From dbo.Role Where RoleId  =" + roleId);

            if (dsRolePercentage.Tables[0].Rows.Count > 0)
                RolePercentage = Convert.ToDecimal(dsRolePercentage.Tables[0].Rows[0].ItemArray[0]);
        }

        // Method to bind projects in the DropDownList...
        private void BindDropDownProjects()
        {
            var EmployeeCode = DBNull.Value.ToString();
            var UID = Session["UserRoleID"];
            object resourceID = null;

            //SetPeriod();
            parameter = new List<SqlParameter>();
            if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {

                var UName = Session["UserName"];
                string query = string.Format("select EmployeeCode  from [dbo].[User] where UserName='{0}'", UName);
                Object obj = PMOscar.BaseDAL.ExecuteScalar(query);
                EmployeeCode = obj.ToString();
                parameter.Add(new SqlParameter("@EmployeeCode", EmployeeCode));
                parameter.Add(new SqlParameter("@ProgramID", 0));
                // parameter.Add(new SqlParameter("@CloseDate", Convert.ToDateTime(dayTo).Date));

            }

            else
            {
                if (resourceId.ToString() != "0")
                {
                    string query = string.Format("select emp_Code  from [dbo].[Resource] where ResourceId='{0}'", resourceId);
                    Object obj = PMOscar.BaseDAL.ExecuteScalar(query);
                    EmployeeCode = obj.ToString();
                    parameter.Add(new SqlParameter("@EmployeeCode", EmployeeCode));
                    parameter.Add(new SqlParameter("@ProgramID", 0));
                }

                //parameter.Add(new SqlParameter("@CloseDate",Convert.ToDateTime( dayTo).Date));
            }
            DataTable dtProjects = BaseDAL.ExecuteSPDataTable("GetProjects", parameter);
            ddlProjects.DataSource = dtProjects;
            ddlProjects.DataTextField = "ProjectName";
            ddlProjects.DataValueField = "ProjectId";
            ddlProjects.DataBind();
            ddlProjects.Items.Insert(0, new ListItem("Select", "0"));


        }

        private void BindDropDownProjects(string resourceId)
        {
            var EmployeeCode = DBNull.Value.ToString();
            var UID = Session["UserRoleID"];
            object resourceID = null;

            //SetPeriod();
            parameter = new List<SqlParameter>();
            if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {

                var UName = Session["UserName"];
                string query = string.Format("select EmployeeCode  from [dbo].[User] where UserName='{0}'", UName);
                Object obj = PMOscar.BaseDAL.ExecuteScalar(query);
                EmployeeCode = obj.ToString();
                parameter.Add(new SqlParameter("@EmployeeCode", EmployeeCode));
                parameter.Add(new SqlParameter("@ProgramID", 0));
                // parameter.Add(new SqlParameter("@CloseDate", Convert.ToDateTime(dayTo).Date));

            }

            else
            {
                if (resourceId.ToString() != "0")
                {
                    string query = string.Format("select emp_Code  from [dbo].[Resource] where ResourceId='{0}'", resourceId);
                    Object obj = PMOscar.BaseDAL.ExecuteScalar(query);
                    EmployeeCode = obj.ToString();
                    parameter.Add(new SqlParameter("@EmployeeCode", EmployeeCode));
                    parameter.Add(new SqlParameter("@ProgramID", 0));
                }

                //parameter.Add(new SqlParameter("@CloseDate",Convert.ToDateTime( dayTo).Date));
            }
            DataTable dtProjects = BaseDAL.ExecuteSPDataTable("GetProjects", parameter);
            ddlProjects.DataSource = dtProjects;
            ddlProjects.DataTextField = "ProjectName";
            ddlProjects.DataValueField = "ProjectId";
            ddlProjects.DataBind();
            ddlProjects.Items.Insert(0, new ListItem("Select", "0"));


        }

        // Method to save the project details...
        private void SaveProjectDetails()
        {
            int projectId = Convert.ToInt32(ddlProjects.SelectedValue);
            int phaseId = Convert.ToInt32(ddlPhase.SelectedValue);
            int roleId = Convert.ToInt32(ddlRole.SelectedValue);
            int ddlResourceId = Convert.ToInt32(ddlResources.SelectedValue.ToString());
            int saveFlag = 1;

            if (string.IsNullOrEmpty(hdnSaveCheck.Value))
            {
                DataSet dsTimeTrackerData;
                if (ddlProjects.SelectedItem.Text != "Admin" && ddlProjects.SelectedItem.Text != "Open")
                {
                    dsTimeTrackerData = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + ddlResourceId + " " +
                         " And ProjectId =  " + projectId + " And RoleId = " + roleId + " And PhaseID = " + phaseId + " And (Year(FromDate)= " + Session["Year"] + " " +
                         " or Year(ToDate)=" + Session["Year"] + ") and FromDate>='" + Session["dayfrom"] + "' and  ToDate<='" + Session["dayto"] + "'");
                }
                else
                {
                    if (phaseId == 0)
                    {
                        dsTimeTrackerData = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + ddlResourceId + " " +
                     " And ProjectId =  " + projectId + " And RoleId = " + roleId + " And (Year(FromDate)= " + Session["Year"] + " " +
                     " or Year(ToDate)=" + Session["Year"] + ") and FromDate>='" + Session["dayfrom"] + "' and  ToDate<='" + Session["dayto"] + "' and phaseID is null");

                    }
                    else
                    {
                        dsTimeTrackerData = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + ddlResourceId + " " +
                          " And ProjectId =  " + projectId + " And RoleId = " + roleId + "And PhaseID = " + phaseId + "" + " And (Year(FromDate)= " + Session["Year"] + " " +
                          " or Year(ToDate)=" + Session["Year"] + ") and FromDate>='" + Session["dayfrom"] + "' and  ToDate<='" + Session["dayto"] + "'");
                    }
                }

                if (dsTimeTrackerData.Tables[0].Rows.Count > 0)
                {
                    lblMsgStatus.Text = "There is already an allocation for this resource against the same Phase and Role. Please update the existing entry.";
                    validation1 = 1;
                    lblMsgStatus.Visible = true;
                    lblPjct.Visible = false;
                    saveFlag = 0;
                    validation1 = 1;
                }
                else
                {
                    saveFlag = 1;
                }
            }
            if (saveFlag == 1)
            {
                updateFlag = 0;

                GetRolePercentageValue();  // Method to get the percentage values of the roles...
                ValidationCheckingOnHours();
                resourceId = Request.QueryString["Id"] ?? "0";

                if (resourceId == null || resourceId == "0")
                    resourceId = Request.QueryString["ResEditId"];

                if (resourceId == null || resourceId == "0")
                    resourceId = Request.QueryString["ResDeleteId"];

                phaseId = Convert.ToInt32(ddlPhase.SelectedValue);

                try
                {
                    if (isOverlappingWeek) // Overlapping week
                    {
                        // Here we have to insert multiple entry for split dates (for two months)

                        int exceededHours = 0;
                        bool canInsert = true;
                        int hoursExceeded1 = 0;

                        bool canInsert2 = true;
                        int hoursExceeded2 = 0;
                        if ((txtEHours.Text == string.Empty || txtEHours.Text == "0") && (txtEHours2.Text == string.Empty || txtEHours2.Text == "0") && (txtAHours.Text == string.Empty || txtAHours.Text == "0") && (txtAHours2.Text == string.Empty || txtAHours2.Text == "0")) //Checking all fields are empty.
                        {
                            lblEstimation.Visible = true;
                            lblEstimation.Text = "Enter Estimation/Actual Hours..!";
                        }
                        calcEstimation = 0;
                        if (txtEHours.Text != string.Empty && txtEHours.Text != "0") //Checking text box for estimation is empty or not.
                        {
                            canInsert = GetTotalHoursOfResource(ddlResourceId, 0, Convert.ToDateTime(hdnWeek1StartDate.Value.ToString()), Convert.ToDateTime(hdnWeek1EndDate.Value.ToString()), txtEHours.Text.Trim(), txtAHours.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Insert, out exceededHours);
                            hoursExceeded1 = exceededHours;
                            if (!canInsert && txtEHours2.Text != string.Empty && txtEHours2.Text != "0") //Checking text box for estimation is empty or not.
                            {
                                canInsert2 = GetTotalHoursOfResource(ddlResourceId, 0, Convert.ToDateTime(hdnWeek2StartDate.Value.ToString()), Convert.ToDateTime(hdnWeek2EndDate.Value.ToString()), txtEHours2.Text.Trim(), txtAHours2.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Insert, out exceededHours);
                                hoursExceeded2 = exceededHours;
                            }
                        }
                        else if (txtAHours.Text != string.Empty && txtAHours.Text != "0") // checking Text box for actual hours empty or not.
                        {
                            canInsert = GetTotalHoursOfResource(ddlResourceId, 0, Convert.ToDateTime(hdnWeek1StartDate.Value.ToString()), Convert.ToDateTime(hdnWeek1EndDate.Value.ToString()), txtEHours.Text.Trim(), txtAHours.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Insert, out exceededHours);
                            hoursExceeded1 = exceededHours;
                            if (!canInsert && txtAHours2.Text != string.Empty && txtAHours2.Text != "0") //Checking text box for estimation is empty or not.
                            {
                                canInsert2 = GetTotalHoursOfResource(ddlResourceId, 0, Convert.ToDateTime(hdnWeek2StartDate.Value.ToString()), Convert.ToDateTime(hdnWeek2EndDate.Value.ToString()), txtEHours2.Text.Trim(), txtAHours2.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Insert, out exceededHours);
                                hoursExceeded2 = exceededHours;
                            }

                        }

                        if (canInsert && txtEHours2.Text != string.Empty && txtEHours2.Text != "0") //Checking text box for estimation is empty or not.
                        {
                            canInsert2 = GetTotalHoursOfResource(ddlResourceId, 0, Convert.ToDateTime(hdnWeek2StartDate.Value.ToString()), Convert.ToDateTime(hdnWeek2EndDate.Value.ToString()), txtEHours2.Text.Trim(), txtAHours2.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Insert, out exceededHours);
                            hoursExceeded2 = exceededHours;
                        }

                        else if (canInsert && txtAHours2.Text != string.Empty && txtAHours2.Text != "0") // checking Text box for actual hours empty or not.
                        {
                            canInsert2 = GetTotalHoursOfResource(ddlResourceId, 0, Convert.ToDateTime(hdnWeek2StartDate.Value.ToString()), Convert.ToDateTime(hdnWeek2EndDate.Value.ToString()), txtEHours2.Text.Trim(), txtAHours2.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Insert, out exceededHours);
                            hoursExceeded2 = exceededHours;
                        }

                        if (canInsert && canInsert2)
                        {
                            // First entry for week1
                            string fromDate = hdnWeek1StartDate.Value; //Session["dayfrom"].ToString();
                            string toDate = hdnWeek1EndDate.Value; //Session["dayto"].ToString();

                            parameter = new List<SqlParameter>();
                            parameter.Add(new SqlParameter("@TimeTrackerID", 1));
                            parameter.Add(new SqlParameter("@Proj_Id", ddlProjects.SelectedValue));
                            parameter.Add(new SqlParameter("@Res_Id", Convert.ToInt32(ddlResources.SelectedValue)));
                            parameter.Add(new SqlParameter("@Role_Id", Convert.ToInt32(ddlRole.SelectedValue)));

                            parameter.Add(txtEHours.Text == string.Empty
                                              ? new SqlParameter("@Time_EHours", DBNull.Value)
                                              : new SqlParameter("@Time_EHours", txtEHours.Text.Trim()));

                            if (txtAHours.Text == string.Empty)
                            {
                                parameter.Add(new SqlParameter("@Time_AHours", DBNull.Value));
                                parameter.Add(new SqlParameter("@ActualHours_Updated", actualHourUpdate));
                            }
                            else
                            {
                                parameter.Add(new SqlParameter("@Time_AHours", txtAHours.Text.Trim()));
                                if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
                                {
                                    parameter.Add(new SqlParameter("@ActualHours_Updated", 1));
                                }
                                else
                                {
                                    parameter.Add(new SqlParameter("@ActualHours_Updated", actualHourUpdate));

                                }
                            }


                            parameter.Add(new SqlParameter("@Time_UpdatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@Time_UpdatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@Time_FromDate", Convert.ToDateTime(fromDate)));
                            parameter.Add(new SqlParameter("@Time_ToDate", Convert.ToDateTime(toDate)));
                            parameter.Add(new SqlParameter("@Time_CreatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@Time_CreatedDate", DateTime.Now));


                            parameter.Add(new SqlParameter("@TeamId", ddlTeam.SelectedValue));

                            parameter.Add(phaseId == 0
                                              ? new SqlParameter("@PhaseId", DBNull.Value)
                                              : new SqlParameter("@PhaseId", phaseId));

                            parameter.Add(new SqlParameter("@WeeklyComments", txtComments.Text.Trim()));
                            parameter.Add(new SqlParameter("@OpMode", "INSERT"));
                            SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
                            returnValue.Direction = ParameterDirection.ReturnValue;
                            parameter.Add(returnValue);
                            if (txtEHours.Text.Trim() != string.Empty || txtAHours.Text.Trim() != string.Empty)
                            {
                                timeTrackerId = BaseDAL.ExecuteSPScalar("ResourcePlanningOperations", parameter);
                            }

                            /***********************************************************************/

                            if (txtEHours2.Text.Trim() != string.Empty || txtAHours2.Text.Trim() != string.Empty)
                            {
                                // Second entry for week2
                                fromDate = hdnWeek2StartDate.Value;
                                toDate = hdnWeek2EndDate.Value;
                                parameter = new List<SqlParameter>();
                                parameter.Add(new SqlParameter("@TimeTrackerID", 1));
                                parameter.Add(new SqlParameter("@Proj_Id", ddlProjects.SelectedValue));
                                parameter.Add(new SqlParameter("@Res_Id", Convert.ToInt32(ddlResources.SelectedValue)));
                                parameter.Add(new SqlParameter("@Role_Id", Convert.ToInt32(ddlRole.SelectedValue)));

                                parameter.Add(txtEHours2.Text == string.Empty
                                                  ? new SqlParameter("@Time_EHours", "0")
                                                  : new SqlParameter("@Time_EHours", txtEHours2.Text.Trim()));

                                if (txtAHours2.Text == string.Empty)
                                {
                                    parameter.Add(new SqlParameter("@Time_AHours", DBNull.Value));
                                    parameter.Add(new SqlParameter("@ActualHours_Updated", actualHourUpdate));
                                }
                                else
                                {
                                    parameter.Add(new SqlParameter("@Time_AHours", txtAHours2.Text.Trim()));
                                    if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
                                    {
                                        parameter.Add(new SqlParameter("@ActualHours_Updated", 1));
                                    }
                                    else
                                    {
                                        parameter.Add(new SqlParameter("@ActualHours_Updated", actualHourUpdate));
                                    }
                                }


                                parameter.Add(new SqlParameter("@Time_UpdatedBy", Session["UserID"]));
                                parameter.Add(new SqlParameter("@Time_UpdatedDate", DateTime.Now));
                                parameter.Add(new SqlParameter("@Time_FromDate", Convert.ToDateTime(fromDate)));
                                parameter.Add(new SqlParameter("@Time_ToDate", Convert.ToDateTime(toDate)));
                                parameter.Add(new SqlParameter("@Time_CreatedBy", Session["UserID"]));
                                parameter.Add(new SqlParameter("@Time_CreatedDate", DateTime.Now));


                                parameter.Add(new SqlParameter("@TeamId", ddlTeam.SelectedValue));

                                parameter.Add(phaseId == 0
                                                  ? new SqlParameter("@PhaseId", DBNull.Value)
                                                  : new SqlParameter("@PhaseId", phaseId));

                                parameter.Add(new SqlParameter("@WeeklyComments", txtComments.Text.Trim()));
                                parameter.Add(new SqlParameter("@OpMode", "INSERT"));
                                returnValue = new SqlParameter("returnVal", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                                parameter.Add(returnValue);
                                timeTrackerId = BaseDAL.ExecuteSPScalar("ResourcePlanningOperations", parameter);
                            }
                            lblesimation2.Visible = false;

                        }
                        else
                        {
                            lblMsgStatus.Text = string.Empty;
                            lblMsgStatus1.Text = string.Empty;
                            lblError.Text = string.Empty;
                            lblError1.Text = string.Empty;

                            if (!canInsert && !canInsert2)
                            {


                                lblMsgStatus.Visible = true;
                                lblMsgStatus.Text = "Estimated hours exceeds for both weeks.";
                                validation1 = 1;
                                if (lblFuture.Visible || lblesimation2.Visible || lblEstimation.Visible)
                                {
                                    lblMsgStatus.Visible = false;
                                }


                            }
                            else if (!canInsert)
                            {

                                lblMsgStatus.Visible = true;
                                lblMsgStatus.Text = "Estimated hours for the week " + lblWeek1.Text + " (" + lblMonth2.Text + ") exceeds " + hoursExceeded1 + " hours. Please correct.";
                                validation1 = 1;
                                if (lblesimation2.Visible || lblFuture.Visible || lblEstimation.Visible && txtEHours2.Text != string.Empty && txtEHours.Text != string.Empty)
                                {
                                    lblMsgStatus.Visible = false;
                                }

                                else if (lblesimation2.Visible || lblFuture.Visible || lblEstimation.Visible)
                                {
                                    lblMsgStatus.Visible = false;
                                }
                                else if (lblesimation2.Visible || lblFuture.Visible || lblEstimation.Visible)
                                {
                                    lblMsgStatus.Visible = false;
                                }

                                else
                                {
                                    lblesimation2.Visible = false;
                                    lblEstimation.Visible = false;
                                }
                            }
                            else if (!canInsert2)
                            {
                                if (!lblesimation2.Visible && !lblEstimation.Visible || hoursExceeded2 != 0)
                                {
                                    lblMsgStatus.Visible = true;
                                    lblMsgStatus.Text = "Estimated hours for the week " + lblWeek2.Text + " (" + lblMonth2.Text + ") exceeds " + hoursExceeded2 + " hours. Please correct.";
                                    validation1 = 1;
                                }
                                else
                                {
                                    lblMsgStatus.Visible = false;
                                }

                            }
                            else
                            {
                                lblMsgStatus.Visible = false;
                            }
                        }
                    }
                    else // Not overlapping week
                    {
                        // Here we have only one entry with week start date and week end date
                        int exceededHours = 0;
                        bool canInsert = true;
                        if (txtEHours.Text != string.Empty && txtEHours.Text != "0") // checking text box for estimation is empty or not.
                        {
                            canInsert = GetTotalHoursOfResource(ddlResourceId, 0, Convert.ToDateTime(hdnWeek1StartDate.Value.ToString()), Convert.ToDateTime(hdnWeek1EndDate.Value.ToString()), txtEHours.Text.Trim(), txtAHours.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Update, out exceededHours);
                        }

                        else if (txtAHours.Text != string.Empty && txtAHours.Text != "0") // checking text boxes for actual hours is empty or not.
                        {
                            canInsert = GetTotalHoursOfResource(ddlResourceId, 0, Convert.ToDateTime(hdnWeek1StartDate.Value.ToString()), Convert.ToDateTime(hdnWeek1EndDate.Value.ToString()), txtEHours.Text.Trim(), txtAHours.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Update, out exceededHours);
                        }
                        else
                        {
                            lblEstimation.Visible = true;
                            lblEstimation.Text = "Enter Estimation/Actual Hours..";
                            canInsert = false;
                        }
                        if (canInsert)
                        {
                            string fromDate = hdnWeek1StartDate.Value; //Session["dayfrom"].ToString();
                            string toDate = hdnWeek1EndDate.Value; //Session["dayto"].ToString();
                            parameter = new List<SqlParameter>();
                            parameter.Add(new SqlParameter("@TimeTrackerID", 1));
                            parameter.Add(new SqlParameter("@Proj_Id", ddlProjects.SelectedValue));
                            parameter.Add(new SqlParameter("@Res_Id", Convert.ToInt32(ddlResources.SelectedValue)));
                            parameter.Add(new SqlParameter("@Role_Id", Convert.ToInt32(ddlRole.SelectedValue)));

                            parameter.Add(txtEHours.Text == string.Empty
                                              ? new SqlParameter("@Time_EHours", DBNull.Value)
                                              : new SqlParameter("@Time_EHours", txtEHours.Text.Trim()));

                            if (txtAHours.Text == string.Empty)
                            {
                                parameter.Add(new SqlParameter("@Time_AHours", DBNull.Value));
                                parameter.Add(new SqlParameter("@ActualHours_Updated", actualHourUpdate));
                            }
                            else
                            {
                                parameter.Add(new SqlParameter("@Time_AHours", txtAHours.Text.Trim()));
                                if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
                                {
                                    parameter.Add(new SqlParameter("@ActualHours_Updated", 1));
                                }
                                else
                                {
                                    parameter.Add(new SqlParameter("@ActualHours_Updated", actualHourUpdate));
                                }
                            }

                            parameter.Add(new SqlParameter("@Time_UpdatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@Time_UpdatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@Time_FromDate", Convert.ToDateTime(fromDate)));
                            parameter.Add(new SqlParameter("@Time_ToDate", Convert.ToDateTime(toDate)));
                            parameter.Add(new SqlParameter("@Time_CreatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@Time_CreatedDate", DateTime.Now));


                            parameter.Add(new SqlParameter("@TeamId", ddlTeam.SelectedValue));

                            parameter.Add(phaseId == 0
                                              ? new SqlParameter("@PhaseId", DBNull.Value)
                                              : new SqlParameter("@PhaseId", phaseId));

                            parameter.Add(new SqlParameter("@WeeklyComments", txtComments.Text.Trim()));
                            parameter.Add(new SqlParameter("@OpMode", "INSERT"));
                            SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
                            returnValue.Direction = ParameterDirection.ReturnValue;
                            parameter.Add(returnValue);
                            timeTrackerId = BaseDAL.ExecuteSPScalar("ResourcePlanningOperations", parameter);
                        }
                        else
                        {
                            lblError.Text = string.Empty;
                            if (exceededHours == 0)
                            {
                                if (lblEstimation.Visible || lblesimation2.Visible)
                                {
                                    lblMsgStatus.Visible = false;
                                    lblMsgStatus1.Visible = false;
                                    lblError1.Visible = false;
                                }
                                else
                                {
                                    lblMsgStatus.Visible = true;
                                    lblMsgStatus1.Visible = true;
                                    lblError1.Visible = true;
                                }

                            }
                            else
                            {
                                lblMsgStatus.Text = "Estimated hours for the week " + lblWeek1.Text + " (" + lblMonth1.Text + ") exceeds " + exceededHours + " hours. Please correct.";
                                lblMsgStatus.Visible = true;
                                lblError1.Visible = false;
                                validation1 = 1;


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Error while inserting Resource Planning entry", ex);
                }
            }
        }

        // Method to update the project details...
        private void UpdateProjectDetails()
        {
            int projectId = Convert.ToInt32(ddlProjects.SelectedValue.ToString());
            int phaseId = Convert.ToInt32(ddlPhase.SelectedValue.ToString());

            string fromDate = hdnWeek1StartDate.Value;
            string toDate = hdnWeek1EndDate.Value;

            int timetrackerId = (Request.QueryString["TTId"] != null) ? Convert.ToInt32(Request.QueryString["TTId"]) : 0;

            int roleId = Convert.ToInt32(ddlRole.SelectedValue.ToString());
            DataSet dsTimeTrackerDetails;

            if (ddlProjects.SelectedItem.Text != "Admin" && ddlProjects.SelectedItem.Text != "Open")
            {
                dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + Convert.ToInt32(ddlResources.SelectedValue.ToString()) + " " +
                " And TimeTrackerId <> " + timetrackerId + " And ProjectId =  " + projectId + " And RoleId = " + roleId + " And PhaseID = " + phaseId + "" +
                " And (Year(FromDate)= " + Convert.ToDateTime(fromDate).Year + " " +
                " or Year(ToDate)=" + Convert.ToDateTime(toDate).Year + ") and FromDate>='" + fromDate + "' and  ToDate<='" + toDate + "'");
            }

            else
            {
                if (phaseId == 0)
                {
                    dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + Convert.ToInt32(ddlResources.SelectedValue.ToString()) + " " +
     " And TimeTrackerId <> " + timetrackerId + " And ProjectId =  " + projectId + " And RoleId = " + roleId +
     " And (Year(FromDate)= " + Convert.ToDateTime(fromDate).Year + " " +
     " or Year(ToDate)=" + Convert.ToDateTime(toDate).Year + ") and FromDate>='" + fromDate + "' and  ToDate<='" + toDate + "'and PhaseId is null");

                }
                else
                {
                    dsTimeTrackerDetails = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + Convert.ToInt32(ddlResources.SelectedValue.ToString()) + " " +
                    " And TimeTrackerId <> " + timetrackerId + " And ProjectId =  " + projectId + " And RoleId = " + roleId + "And PhaseID = " + phaseId + "" +
                    " And (Year(FromDate)= " + Convert.ToDateTime(fromDate).Year + " " +
                    " or Year(ToDate)=" + Convert.ToDateTime(toDate).Year + ") and FromDate>='" + fromDate + "' and  ToDate<='" + toDate + "'");
                }
            }

            if (dsTimeTrackerDetails.Tables[0].Rows.Count > 0)
            {
                lblMsgStatus.Text = "There is already an allocation for this resource against the same Phase and Role. Please update the existing entry.";
                lblMsgStatus.Visible = true;
                lblPjct.Visible = false;
                validation1 = 1;
            }

            else
            {
                //resourceId = Request.QueryString["ResEditId"];
                resourceId = ddlResources.SelectedValue.ToString();
                updateFlag = 1;
                GetRolePercentageValue();      // Method to get the percentage values of the roles...
                ValidationCheckingOnHours();  // Method to check the validation on hours to apply red colur on values

                phaseId = Convert.ToInt32(ddlPhase.SelectedValue.ToString());

                try
                {
                    int actHrsUpdatedFlag = 0;
                    int exceededHours = 0;
                    bool canInsert = GetTotalHoursOfResource(Convert.ToInt32(resourceId), timetrackerId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), txtEHours.Text.Trim(), txtAHours.Text.Trim(), (int)PMOscar.Utility.EnumTypes.Mode.Update, out exceededHours);
                    int hoursExceeded = exceededHours;
                    if (canInsert)
                    {
                        parameter = new List<SqlParameter>();
                        parameter.Add(new SqlParameter("@TimeTrackerID", timetrackerId));
                        parameter.Add(new SqlParameter("@Proj_Id", Convert.ToInt32(ddlProjects.SelectedValue.ToString())));
                        parameter.Add(new SqlParameter("@Res_Id", Convert.ToInt32(resourceId)));
                        parameter.Add(new SqlParameter("@Role_Id", Convert.ToInt32(ddlRole.SelectedValue.ToString())));

                        if (txtEHours.Text == string.Empty)
                            parameter.Add(new SqlParameter("@Time_EHours", DBNull.Value));
                        else
                            parameter.Add(new SqlParameter("@Time_EHours", txtEHours.Text.Trim()));

                        if (txtAHours.Text == string.Empty)
                        {
                            parameter.Add(new SqlParameter("@Time_AHours", DBNull.Value));
                            parameter.Add(new SqlParameter("@ActualHours_Updated", actHrsUpdatedFlag));
                        }
                        else
                        {
                            parameter.Add(new SqlParameter("@Time_AHours", txtAHours.Text.Trim()));
                            if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
                            {
                                if (txtAHours.Text.Trim() != Session["actualHour"].ToString())
                                {
                                    parameter.Add(new SqlParameter("@ActualHours_Updated", 1));
                                }
                                else
                                {
                                    parameter.Add(new SqlParameter("@ActualHours_Updated", actHrsUpdatedFlag));
                                }
                            }
                        }

                        parameter.Add(new SqlParameter("@Time_UpdatedBy", Session["UserID"]));
                        parameter.Add(new SqlParameter("@Time_UpdatedDate", DateTime.Now));
                        parameter.Add(new SqlParameter("@Time_FromDate", fromDate));
                        parameter.Add(new SqlParameter("@Time_ToDate", toDate));
                        parameter.Add(new SqlParameter("@Time_CreatedBy", Session["UserID"]));
                        parameter.Add(new SqlParameter("@Time_CreatedDate", DateTime.Now));

                        parameter.Add(new SqlParameter("@TeamId", ddlTeam.SelectedValue));

                        if (phaseId == 0)
                            parameter.Add(new SqlParameter("@PhaseId", DBNull.Value));

                        else
                            parameter.Add(new SqlParameter("@PhaseId", phaseId));

                        parameter.Add(new SqlParameter("@WeeklyComments", txtComments.Text.Trim()));
                        parameter.Add(new SqlParameter("@OpMode", "UPDATE"));
                        SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        parameter.Add(returnValue);
                        int timeTrackerId = BaseDAL.ExecuteSPScalar("ResourcePlanningOperations", parameter);
                    }
                    else
                    {
                        lblMsgStatus.Text = string.Empty;
                        lblMsgStatus1.Text = string.Empty;
                        lblError.Text = string.Empty;
                        lblError1.Text = string.Empty;
                        if (!lblesimation2.Visible&&validation2==0)
                        {
                            lblMsgStatus.Text = "Estimated hours for the week " + lblWeek1.Text + " (" + lblMonth1.Text + ") exceeds " + exceededHours + " hours. Please correct.";
                            lblMsgStatus.Visible = true;
                            validation1 = 1;
                        }
                        else
                        {
                            lblMsgStatus.Visible = false;
                        }






                    }
                }
                catch
                {

                }

                //function to call TimeTrackeraudit on Updation 
                DisplayProjectDetails(resourceId);

                //TimeTrackerAudit1.SetResourceId = resourceId;
                //TimeTrackerAudit1.SetYear = year;
                //TimeTrackerAudit1.SetDayFrom = dayFrom;
                //TimeTrackerAudit1.SetDayTo = dayTo;
                //TimeTrackerAudit1.SetStatus = status;
                //TimeTrackerAudit1.BindGridView();

                BindGridView(resourceId, year);
            }
        }

        // Method to get Dates in the specified year and month...
        private void SetPeriod()
        {
            if (weekindex == 0 && Convert.ToInt16(period.Substring(0, period.IndexOf("-"))) > Convert.ToInt16(period.Substring(period.IndexOf("-") + 1, period.Length - (period.Length > 3 ? 3 : 2))))
            {
                frommonth = Convert.ToInt16(month) == 0 ? 12 : Convert.ToInt16(month);
                tomonth = month == 11 ? (Convert.ToInt16(month) + 1) : (month == 11 ? (Convert.ToInt16(month) + 1) : (Convert.ToInt16(month) + 1));
                fromyear = Convert.ToInt16(month) == 0 ? (Convert.ToInt16(year) - 1) : (Convert.ToInt16(month) == 11 ? (Convert.ToInt16(year)) : Convert.ToInt16(year));
                toyear = Convert.ToInt16(year);
                dayFrom = fromyear + "-" + frommonth + "-" + Convert.ToInt16(period.Substring(0, period.IndexOf("-")));
                dayTo = toyear + "-" + tomonth + "-" + Convert.ToInt16(period.Substring(period.IndexOf("-") + 1, period.Length - (period.Length > 3 ? 3 : 2)));
            }
            else if (weekindex != 0 && Convert.ToInt16(period.Substring(0, period.IndexOf("-"))) >
                                                   Convert.ToInt16(period.Substring(period.IndexOf("-") + 1,
                                                   period.Length - (period.Length > 3 ? 3 : 2))))
            {
                frommonth = month == 11 ? (Convert.ToInt16(month) + 1) : (Convert.ToInt16(month) + 1);
                tomonth = month == 11 ? 1 : (Convert.ToInt16(month) + 2);
                fromyear = Convert.ToInt16(month) == 0 ? (Convert.ToInt16(year)) : (Convert.ToInt16(month) == 11 ? Convert.ToInt16(year) : Convert.ToInt16(year));
                toyear = Convert.ToInt16(month) == 11 ? Convert.ToInt16(year) + 1 : Convert.ToInt16(year);
                dayFrom = fromyear + "-" + frommonth + "-" + Convert.ToInt16(period.Substring(0, period.IndexOf("-")));
                dayTo = toyear + "-" + tomonth + "-" + Convert.ToInt16(period.Substring(period.IndexOf("-") + 1, period.Length - (period.Length > 3 ? 3 : 2)));
            }
            else
            {
                frommonth = month == 11 ? 1 : (Convert.ToInt16(month) + 1);
                tomonth = month == 11 ? 1 : (Convert.ToInt16(month) + 1);
                fromyear = Convert.ToInt16(year);
                toyear = Convert.ToInt16(year);
                dayFrom = fromyear + "-" + (Convert.ToInt16(month) + 1) + "-" + Convert.ToInt16(period.Substring(0, period.IndexOf("-")));
                dayTo = fromyear + "-" + (Convert.ToInt16(month) + 1) + "-" + Convert.ToInt16(period.Substring(period.IndexOf("-") + 1, period.Length - (period.Length > 3 ? 3 : 2)));
            }
            Session["dayfrom"] = dayFrom;
            Session["dayto"] = dayTo;
        }

        // Method to display project details...
        private void DisplayProjectDetails(string resourceId)
        {
            totalEstimated = 0;
            totalActual = 0;
            period = period + " ";


            if (resourceId == null || resourceId == "0")
                resourceId = Request.QueryString["ResEditId"];

            if (resourceId == null || resourceId == "0")
                resourceId = Request.QueryString["ResDeleteId"];

            if (resourceId == null || resourceId == "0")
                resourceId = "0";
            SetPeriod(); // Method to get Dates in the specified year and month...

            SqlParameter[] parameter = new SqlParameter[5];
            parameter[0] = new SqlParameter("@ResourceID", resourceId);
            parameter[1] = new SqlParameter("@Year", Session["Year"]);
            parameter[2] = new SqlParameter("@Month", Convert.ToInt16(Session["Month"]) + 1);
            parameter[3] = new SqlParameter("@dayfrom", dayFrom);
            parameter[4] = new SqlParameter("@dayto", dayTo);

            DataSet dsProjectDetails = BaseDAL.ExecuteSPDataSet("GetProjectDetailsForResource", parameter);
            dgProjectDetails.DataSource = dsProjectDetails.Tables[0] as DataTable;
            Session["gvResourceDetails"] = dsProjectDetails.Tables[0] as DataTable;
            dgProjectDetails.DataBind();
            dgProjectDetails.Columns[0].Visible = false;
            Session["dayfrom"] = dayFrom;
            Session["dayto"] = dayTo;

            if (resourceId != null && Request.QueryString["op"] != "E")
            {
                DataTable dtRole = PMOscar.BaseDAL.ExecuteDataTable("Select RoleId from [Resource] where ResourceId=" + resourceId);
                if (dtRole.Rows.Count > 0)
                {
                    ddlRole.Text = dtRole.Rows[0].ItemArray[0].ToString();
                    Session["RoleText"] = dtRole.Rows[0].ItemArray[0].ToString();
                }
            }

            Session["ResourceIdTA"] = ddlResources.SelectedValue.ToString();
            Session["dayfromTA"] = dayFrom;
            Session["daytoTA"] = dayTo;
            Session["Status"] = 1;
        }

        // Method to clear controls...
        private void ClearControls()
        {

            string resourceId = String.Empty;
            if (Request.QueryString["op"] == "E")
            {
                resourceId = Request.QueryString["ResEditId"].ToString() == "" ? "0".ToString() : Request.QueryString["ResEditId"].ToString();
            }
            else if (Request.QueryString["op"] == "D")
            {
                resourceId = Request.QueryString["ResDeleteId"].ToString() == "" ? "0".ToString() : Request.QueryString["ResDeleteId"].ToString();
            }
            else
            {

                resourceId = Request.QueryString["Id"].ToString() == "" ? "0".ToString() : Request.QueryString["Id"].ToString();
                if (resourceId != ddlResources.SelectedItem.Value)
                {
                    resourceId = ddlResources.SelectedItem.Value;
                }
            }
            BindDropDownProjects(resourceId);   // Method to bind projects in the DropDownList...        
            BindDropDownPhase();     // Method to bind phases in the DropDownList...

            BindDropDownTeams(resourceId);  // Method to bind teams in the DropDownList...
            if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {
                ddlRole.Enabled = false;
                ddlPhase.Enabled = false;
            }
            else
            {
                ddlProjects.Enabled = true;
                ddlRole.Enabled = true;
                ddlPhase.Enabled = true;
            }

            btnSave.Text = "Save";  // Changing the button text to 'save'...


            txtAHours.Text = string.Empty;
            txtEHours.Text = string.Empty;
            txtAHours2.Text = string.Empty;
            txtEHours2.Text = string.Empty;
            txtComments.Text = string.Empty;
            lblPjct.Text = string.Empty;

            if (lblEstimation.Visible || lblesimation2.Visible || lblFuture.Visible)
            {
                lblMsgStatus.Visible = false;
                lblMsgStatus1.Visible = false;
                lblError1.Visible = false;
            }
        }

        // Method to find Inactive resources...
        private void ConfigureForInActiveResource(string resourceId)
        {

            try
            {
                var dtResources = new DataSet();
                var parameter = new List<SqlParameter> { new SqlParameter("@ResourceID", resourceId) };
                dtResources = PMOscar.BaseDAL.ExecuteSPDataSet("GetResourceById", parameter);

                if (dtResources.Tables[0].Rows.Count > 0)
                {
                    lblPjct.Text = string.Empty;
                    lblPjct.Visible = false;
                    btnSave.Enabled = true;
                    dgProjectDetails.Columns[8].Visible = true;
                    dgProjectDetails.Columns[9].Visible = true;
                    statusFlag = false;
                }
                else
                {

                    btnSave.Enabled = false;
                    lblPjct.Visible = true;
                    lblPjct.Text = "Entry cannot be added since Resource is Inactive.";
                    dgProjectDetails.Columns[8].Visible = false;
                    dgProjectDetails.Columns[9].Visible = false;

                    statusFlag = true;
                }
                Session["ResourceIdTA"] = ddlResources.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

        }

        /// <summary>
        /// Method to split overlapping week and show/hide set of additional fields for adding "Estimated Time" and "Actual Time".
        /// </summary>
        private void SplitOverlappingWeek()
        {

            period = period + " ";
            SetPeriod();
            DateTime fromDate = Convert.ToDateTime(dayFrom);
            DateTime toDate = Convert.ToDateTime(dayTo);
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


                int selectedMonth;
                int.TryParse(ddlMonth.SelectedValue, out selectedMonth);
                if (selectedMonth == fromDate.Month)
                {

                    txtEHours2.CausesValidation = false;
                    RequiredFieldValidator1.Enabled = false;
                }
                else
                {
                    txtEHours.CausesValidation = false;
                    rfvEHours.Enabled = false;
                }

                if (week2StartDate.Date == week2EndDate.Date)
                {
                    DayOfWeek day = Convert.ToDateTime(week2EndDate).DayOfWeek;
                    if (day == DayOfWeek.Sunday)
                    {
                        txtEHours2.Enabled = false;
                        txtAHours2.Enabled = false;
                        lblError2.Visible = true;
                        lblError2.Text = week2EndDate.ToString(PMOscar.Core.Constants.AddRole.DATEFORMAT) + " Is Sunday, You cannot add an estimation for sunday.";
                    }
                }

                else
                {

                    lblError2.Visible = false;
                }

                // Set split dates to hidden field
                hdnWeek1StartDate.Value = week1StartDate.ToShortDateString();
                hdnWeek1EndDate.Value = week1EndDate.ToShortDateString();
                hdnWeek2StartDate.Value = week2StartDate.ToShortDateString();
                hdnWeek2EndDate.Value = week2EndDate.ToShortDateString();
                CheckEntryForProject();

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


        //Method to Check the entries for project in dropdown change
        private void CheckEntryForProject()
        {
            if (isOverlappingWeek && !string.IsNullOrEmpty(hdnWeek1StartDate.Value) && !string.IsNullOrEmpty(hdnWeek1EndDate.Value) && !string.IsNullOrEmpty(hdnWeek2StartDate.Value) && !string.IsNullOrEmpty(hdnWeek2EndDate.Value))
            {
                int projectId = Convert.ToInt32(ddlProjects.SelectedValue);
                int roleId = Convert.ToInt32(ddlRole.SelectedValue);
                int phaseId = Convert.ToInt32(ddlPhase.SelectedValue);

                DateTime week1StartDate = Convert.ToDateTime(hdnWeek1StartDate.Value);
                DateTime week1EndDate = Convert.ToDateTime(hdnWeek1EndDate.Value);
                int year1 = week1StartDate.Year;

                // DateTime week2StartDate = Convert.ToDateTime(hdnWeek2StartDate.Value);
                DateTime week2StartDate = DateTime.Parse(hdnWeek2StartDate.Value);
                DateTime week2EndDate = DateTime.Parse(hdnWeek2EndDate.Value);
                int year2 = week2StartDate.Year;

                int[] year = new int[] { year1, year2 };
                DateTime[] weekStartDate = new DateTime[] { week1StartDate, week2StartDate };
                DateTime[] weekEndDate = new DateTime[] { week1EndDate, week2EndDate };

                int hasEntry = -1;
                int entryFlag = 0;

                if (projectId != 0 && roleId != 0)
                {
                    for (int i = 0; i < year.Length; i++)
                    {
                        DataSet dsTimeTrackerData = new DataSet();
                        if (ddlProjects.SelectedItem.Text != "Admin" && ddlProjects.SelectedItem.Text != "Open")
                        {
                            if (Convert.ToInt32(ddlPhase.SelectedValue) != 0)
                            {

                                dsTimeTrackerData = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + Convert.ToInt32(ddlResources.SelectedValue.ToString()) + " " +
                                     " And ProjectId =  " + projectId + " And RoleId = " + roleId + " And PhaseID = " + phaseId + " And (Year(FromDate)= " + year[i] + " " +
                                     " or Year(ToDate)=" + year[i] + ") and FromDate>='" + weekStartDate[i] + "' and  ToDate<='" + weekEndDate[i] + "'");
                            }
                        }
                        else
                        {
                            if (phaseId == 0)
                            {
                                dsTimeTrackerData = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + Convert.ToInt32(ddlResources.SelectedValue.ToString()) + " " +
                                  " And ProjectId =  " + projectId + " And RoleId = " + roleId + "And (Year(FromDate)= " + year[i] + " " +
                                  " or Year(ToDate)=" + year[i] + ") and FromDate>='" + weekStartDate[i] + "' and  ToDate<='" + weekEndDate[i] + "'and phaseID is null");
                            }
                            else
                            {
                                dsTimeTrackerData = BaseDAL.ExecuteDataSet("Select * From dbo.TimeTracker Where ResourceId  = " + Convert.ToInt32(ddlResources.SelectedValue.ToString()) + " " +
                                  " And ProjectId =  " + projectId + " And RoleId = " + roleId + " And PhaseID = " + phaseId + " And (Year(FromDate)= " + year[i] + " " +
                                  " or Year(ToDate)=" + year[i] + ") and FromDate>='" + weekStartDate[i] + "' and  ToDate<='" + weekEndDate[i] + "'");

                            }
                        }
                        if (dsTimeTrackerData.Tables.Count != 0)
                        {
                            if (dsTimeTrackerData.Tables[0].Rows.Count > 0)
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

        //Method to hide or disable the textboxes if the entries for project found.
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
                    validation1 = 1;
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
                    validation1 = 1;
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
                validation1 = 1;
            }
            else if (entryFlag == 0)
            {
                hdnSaveCheck.Value = "true";
            }
        }

        #endregion

        #region"Control Events"

        protected void dgProjectDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            DisplayProjectDetails(ddlResources.SelectedValue.ToString());
            DataTable dataTable = Session["gvResourceDetails"] as DataTable;
            if (dataTable != null)
            {
                dataTable.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                dgProjectDetails.DataSource = dataTable;
                if (Label7.Text != string.Empty)
                {
                    dgProjectDetails.Columns[8].Visible = false;
                    dgProjectDetails.Columns[9].Visible = false;
                }
                totalEstimated = 0;
                totalActual = 0;
                dgProjectDetails.DataBind();

            }

        }
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
            ClearControls();  // Method to clear controls...            

            year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            month = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
            month = month - 1;
            period = ddlWeek.SelectedItem.Text.ToString();
            weekindex = ddlWeek.SelectedIndex;

            lblMsgStatus.Text = string.Empty;
            lblMsgStatus1.Text = string.Empty;
            lblPjct.Text = string.Empty;

            SplitOverlappingWeek(); // Method to split overlapping week
            DisplayProjectDetails(ddlResources.SelectedValue.ToString()); // Method to display project details...
            timeTrackerId = 0;
            Session["TimeTrackerId"] = null;
            ddlProjects.Focus();
            if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {
                ddlPhase.Enabled = false;
                ddlRole.Enabled = false;
                txtAHours.Enabled = false;
                txtEHours2.Enabled = false;
            }
            BindGridView(resourceId, year);
        }
        protected void lbBack_Click(object sender, EventArgs e)
        {
            year = Convert.ToInt32(Session["Year"]);
            month = Convert.ToInt32(Session["Month"]);
            weekindex = Convert.ToInt16(Session["WeekIndex"]);
            Response.Redirect("ResourcePlanning.aspx?redirect=true&weekindex=" + weekindex + "&month=" + month + "&year=" + year);
        }
        protected void dgProjectDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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



            // Portion to give red color on the estimated or actual values...

            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            int cellCount = e.Row.Cells.Count;
            for (int cellIncrement = 4; cellIncrement < cellCount; cellIncrement++)
            {
                if (e.Row.Cells[cellIncrement].Text == "R")
                {
                    if (e.Row.Cells[cellIncrement - 3].Text == "")
                        e.Row.Cells[cellIncrement - 4].ForeColor = System.Drawing.Color.Red;

                    else
                        e.Row.Cells[cellIncrement - 3].ForeColor = System.Drawing.Color.Red;
                }
            }

            // Portion to give red color on the estimated or actual values...

            if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {
                SetPeriod();
                DateTime fromDate = Convert.ToDateTime(dayFrom);
                DateTime toDate = Convert.ToDateTime(dayTo);
                DateTime week1StartDate, week1EndDate, week2StartDate, week2EndDate, today;
                week1StartDate = fromDate;
                week1EndDate = new DateTime(fromDate.Year, fromDate.Month, 1).AddMonths(1).AddDays(-1);
                week2StartDate = new DateTime(toDate.Year, toDate.Month, 1);
                week2EndDate = toDate;
                today = DateTime.Now.Date;
                ddlPhase.Enabled = false;
                ddlRole.Enabled = false;
                if (today > week2EndDate)
                {
                    dgProjectDetails.Columns[8].Visible = false;
                    dgProjectDetails.Columns[9].Visible = false;
                }
            }
            else
            {
                ddlPhase.Enabled = true;
                ddlRole.Enabled = true;
            }
        }

        private void CheckAllDropdownSelected()
        {
            bool isAllSelected = false;
            txtAHours.Enabled = false;
            txtAHours2.Enabled = false;


            if (ddlProjects.SelectedItem.Text == "Admin" || ddlProjects.SelectedItem.Text == "Open")
            {
                if (ddlProjects.SelectedIndex > 0 && ddlRole.SelectedIndex > 0 && ddlPhase.SelectedIndex > 0)
                {
                    isAllSelected = true;
                }
            }
            else
            {
                if (ddlProjects.SelectedIndex > 0 && ddlPhase.SelectedIndex > 0 && ddlRole.SelectedIndex > 0)
                {
                    isAllSelected = true;
                }
            }

            if (isAllSelected)
            {
                string query = @"SELECT A.RevisedBudgetHours FROM dbo.ProjectEstimation A
                                    JOIN [Role] R ON R.EstimationRoleID = A.EstimationRoleID
                                WHERE A.ProjectID = " + ddlProjects.SelectedValue + " And A.PhaseID = " + ddlPhase.SelectedValue + "  And R.RoleID = " + ddlRole.SelectedValue;
                var budgetedHourObject = PMOscar.BaseDAL.ExecuteScalar(query);
                int budgetedHour = budgetedHourObject == null ? 0 : (int)budgetedHourObject;

                if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
                {
                    txtAHours.Enabled = true;
                    txtAHours2.Enabled = true;
                    txtEHours.Enabled = true;
                    txtEHours2.Enabled = true;

                }
                else
                {
                    if (lblEstimation.Visible || lblesimation2.Visible)
                    {
                        txtEHours.Enabled = false;
                        txtEHours2.Enabled = false;
                    }
                    else
                    {
                        txtEHours.Enabled = true;
                        txtEHours2.Enabled = true;
                    }

                }

            }
        }

        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label7.Text = "";
            var UID = Session["UserRoleID"];
            if (ddlProjects.SelectedItem.Text == "Admin" || ddlProjects.SelectedItem.Text == "Open")
            {
                BindDropDownPhase();
                ddlPhase.Enabled = true;
                RqResEntry.Enabled = false;
              //  ddlPhase.SelectedValue = "0";
                lblEstimation.Visible = false;
                lblError2.Text = "";
                if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
                {
                    ddlPhase.Enabled = false;
                    txtEHours.Enabled = true;
                    txtEHours2.Enabled = true;
                }


            }

            else if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {
                ddlPhase.Enabled = true;
                RqResEntry.Enabled = true;
            }

            if (ddlProjects.SelectedItem.Text != "Admin" && ddlProjects.SelectedItem.Text != "Open")
            {

                lblEstimation.Visible = false;
                lblError2.Text = "";
                int month = ddlMonth.SelectedIndex + 1;
                int year = Convert.ToInt16(ddlYear.SelectedItem.Text);
                var projectname = ddlProjects.SelectedItem.Text;
                string mclosingDate = string.Empty;
                string[] arrweek = ddlWeek.SelectedItem.Text.Split('-');
                string weekFirstdate = year.ToString() + "/" + month.ToString() + "/" + arrweek[0];

                SetPeriod();
                DateTime fromDate = Convert.ToDateTime(dayFrom);
                DateTime toDate = Convert.ToDateTime(dayTo);
                DateTime week1StartDate, week1EndDate, week2StartDate, week2EndDate;
                week1StartDate = fromDate;
                week1EndDate = new DateTime(fromDate.Year, fromDate.Month, 1).AddMonths(1).AddDays(-1);
                week2StartDate = new DateTime(toDate.Year, toDate.Month, 1);
                week2EndDate = toDate;


                if (Convert.ToInt16(arrweek[0]) > Convert.ToInt16(arrweek[1]) && month != 12)
                {
                    if (DateTime.Now.Date > Convert.ToDateTime(week1EndDate))
                    {
                        month = ddlMonth.SelectedIndex;
                    }
                    else
                    {
                        month = month + 1;
                    }

                }
                else if (Convert.ToInt16(arrweek[0]) > Convert.ToInt16(arrweek[1]) && month == 12)
                {
                    month = 1;
                    year = year + 1;
                }
                string weekDate = year.ToString() + "/" + month.ToString() + "/" + arrweek[1];
                BindDropDownPhase();
                if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees && ddlProjects.SelectedItem.Text != "Admin" && ddlProjects.SelectedItem.Text != "Open")
                {
                    ddlPhase.Enabled = false;
                }

                if (projectname.Equals("Select"))
                {
                    lblEstimation.Visible = false;
                }
                else
                {
                    string query = string.Format("SELECT MaintClosingDate FROM Project where ProjectName = '{0}'", projectname);
                    object mcDate = PMOscar.BaseDAL.ExecuteScalar(query);
                    mclosingDate = mcDate.ToString();
                }

            }




            CheckAllDropdownSelected();
            if (lblesimation2.Visible)
            {
                txtEHours2.Enabled = false;
                txtAHours2.Enabled = false;
                lblEstimation.Visible = false;
                lblMsgStatus.Visible = false;
            }
            if (lblEstimation.Visible)
            {
                txtEHours2.Enabled = false;
                txtAHours2.Enabled = false;
                txtEHours.Enabled = false;
                txtAHours.Enabled = false;
            }
            if (lblFuture.Visible)
            {
                txtEHours2.Enabled = false;
                txtAHours2.Enabled = false;
                txtEHours.Enabled = false;
                txtAHours.Enabled = false;
                lblEstimation.Visible = false;
                lblesimation2.Visible = false;
            }
            resourceId = (Request.QueryString["Id"] != null) ? Request.QueryString["Id"].ToString() : "0";
            DataTable dtRole = PMOscar.BaseDAL.ExecuteDataTable("Select RoleId from [Resource] where ResourceId=" + ddlResources.SelectedItem.Value.ToString());
            if (dtRole.Rows.Count > 0)
                ddlRole.Text = dtRole.Rows[0].ItemArray[0].ToString();

            dtRole.Dispose();
            ddlRole.Focus();

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
                    Label7.Text = "Dashboard for this period has already been finalized, so you won't be able to make any edits here.";
                    dgProjectDetails.Columns[8].Visible = false;
                    dgProjectDetails.Columns[9].Visible = false;
                    lblError2.Text = "";
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

            DisplayProjectDetails(ddlResources.SelectedValue.ToString());
            CheckEntryForProject();

            if (Request.QueryString["op"] != null && Request.QueryString["op"] == "E")
            {
                if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 4=Employee)
                {
                    ddlPhase.Enabled = false;
                }
                else if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 4=Employee)
                {
                    if (ddlProjects.SelectedItem.Text == "Admin" || ddlProjects.SelectedItem.Text == "Open")
                    {
                        ddlPhase.Enabled = true;
                    }
                    else
                    {
                        ddlPhase.Enabled = true;
                    }
                }
            }
            else
            {
                SplitOverlappingWeek();
            }
            //  SplitOverlappingWeek();
            //diasble finalized time period

        }
        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {


            year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            month = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
            month = month - 1;
            period = ddlWeek.SelectedItem.Text.ToString();
            resourceId = ddlResources.SelectedItem.Value;
            Label7.Text = "";
            weekindex = ddlWeek.SelectedIndex;

            lblMsgStatus.Text = string.Empty;
            lblMsgStatus1.Text = string.Empty;
            lblPjct.Text = string.Empty;
            lblError2.Text = "";
            lblEstimation.Visible = false;


            Session["Year"] = year;
            Session["Month"] = month;
            Session["Period"] = period;
            Session["WeekIndex"] = weekindex;

            SetPeriod();
            DateTime fromDate = Convert.ToDateTime(dayFrom);
            DateTime toDate = Convert.ToDateTime(dayTo);
            DateTime week1StartDate, week1EndDate, week2StartDate, week2EndDate, today;
            week1StartDate = fromDate;
            week1EndDate = new DateTime(fromDate.Year, fromDate.Month, 1).AddMonths(1).AddDays(-1);
            week2StartDate = new DateTime(toDate.Year, toDate.Month, 1);
            week2EndDate = toDate;
            today = DateTime.Now.Date;


            ClearControls();  // Method to clear controls...
            DisplayProjectDetails(ddlResources.SelectedValue.ToString()); // Method to display project details...

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
                    Label7.Text = "Dashboard for this period has already been finalized, so you won't be able to make any edits here.";
                    dgProjectDetails.Columns[8].Visible = false;
                    dgProjectDetails.Columns[9].Visible = false;
                    lblError2.Text = "";
                }
                else
                {
                    ConfigureForInActiveResource(ddlResources.SelectedItem.Value.ToString());
                    if (statusFlag != true)
                    {
                        btnSave.Enabled = true;
                        Label7.Text = "";
                        dgProjectDetails.Columns[8].Visible = true;
                        dgProjectDetails.Columns[9].Visible = true;
                    }
                }
            }
            else
            {
                Label7.Text = "";
                btnSave.Enabled = true;
            }
            //diasble finalized time period ends


            SplitOverlappingWeek(); // Split overlapping week and show/hide additional field for entering Estimated and Actual values.
            if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {
                ddlPhase.Enabled = false;
                ddlRole.Enabled = false;
                if (today > week2EndDate)
                {
                    dgProjectDetails.Columns[8].Visible = false;
                    dgProjectDetails.Columns[9].Visible = false;
                }

            }
            else if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {
                ddlPhase.Enabled = true;
            }
            BindGridView(resourceId, year);
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {
                ddlPhase.Enabled = false;
                ddlRole.Enabled = false;
            }
            if (Convert.ToInt32(ddlYear.SelectedItem.Value) != DateTime.Now.Year)
                ddlMonth.SelectedIndex = 0;

            else
                ddlMonth.SelectedIndex = DateTime.Now.Date.Month - 1;

            GetWeek(); // Method to populate the weeks in selected Month and Year...
            year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            month = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
            month = month - 1;
            period = ddlWeek.SelectedItem.Text.ToString();
            weekindex = ddlWeek.SelectedIndex;
            Label7.Text = "";
            resourceId = ddlResources.SelectedItem.Value;
            lblMsgStatus.Text = string.Empty;
            lblMsgStatus1.Text = string.Empty;
            lblPjct.Text = string.Empty;
            lblError2.Text = "";
            lblEstimation.Visible = false;

            Session["Year"] = year;
            Session["Month"] = month;
            Session["Period"] = period;
            Session["WeekIndex"] = weekindex;


            ClearControls();    // Method to clear controls...
            DisplayProjectDetails(ddlResources.SelectedValue.ToString()); // Method to display project details...
            SplitOverlappingWeek(); // Split overlapping week and show/hide additional field for entering Estimated and Actual values.

            lblWeek.Text = ddlWeek.SelectedItem.Text.ToString() + ' ' + ',' + ' ' + ddlMonth.SelectedItem.Text.ToString() + ' ' + ',' + ' ' + ddlYear.SelectedItem.Text.ToString();
            BindGridView(resourceId, year);
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {
                ddlPhase.Enabled = false;
                ddlRole.Enabled = false;
            }
            GetWeek(); // Method to populate the weeks in selected Month and Year...

            year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            month = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
            month = month - 1;
            period = ddlWeek.SelectedItem.Text.ToString();
            weekindex = ddlWeek.SelectedIndex;
            resourceId = ddlResources.SelectedItem.Value;
            Label7.Text = "";



            lblMsgStatus.Text = string.Empty;
            lblMsgStatus1.Text = string.Empty;
            lblPjct.Text = string.Empty;
            lblError2.Text = "";
            lblEstimation.Visible = false;

            Session["Year"] = year;
            Session["Month"] = month;
            Session["Period"] = period;
            Session["WeekIndex"] = weekindex;

            ClearControls(); // Method to clear controls...
            DisplayProjectDetails(ddlResources.SelectedValue.ToString()); // Method to display project details...                             
            SplitOverlappingWeek(); // Split overlapping week and show/hide additional field for entering Estimated and Actual values.

            lblWeek.Text = ddlWeek.SelectedItem.Text.ToString() + ' ' + ',' + ' ' + ddlMonth.SelectedItem.Text.ToString() + ' ' + ',' + ' ' + ddlYear.SelectedItem.Text.ToString();

            BindGridView(resourceId, year);
        }
        protected void lnkChng_Click1(object sender, EventArgs e)
        {

            if (statusFlag == true)
            {
                btnSave.Enabled = true;

            }
            else
            {
                btnSave.Enabled = false;

            }
            // ModalPopupExtender1.Show();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            validation1 = 0;
           
            lblesimation2.Visible = false;
            if (Session["update"].ToString() == ViewState["update"].ToString())    // If page not Refreshed
            {
                //=============== On click event code =========================
                if (Request.QueryString["op"] == "E" && btnSave.Text == "Update")
                {
                    timeTrackerId = 1;


                }
                if (Convert.ToInt32(Session["sessionTimeTrackerId"]) == 0)
                {
                    if (statusFlag == false)
                        SaveProjectDetails();   // Method to save the project details...

                    else
                    {
                        lblPjct.Visible = true;
                        lblPjct.Text = "Project Entry cannot be Added since Resource is Inactive.";
                        dgProjectDetails.Columns[8].Visible = false;
                        dgProjectDetails.Columns[9].Visible = false;

                    }

                }
                else
                {

                    if (timeTrackerId != 0)
                    {
                        if (statusFlag == false)
                        {
                            UpdateProjectDetails();  // Method to update the project details...
                            lblPjct.Visible = false;
                            timeTrackerId = 0;
                            Session["TimeTrackerId"] = 0;

                        }

                        else
                        {
                            lblPjct.Visible = true;
                            lblPjct.Text = "Project Entry cannot be updated since Resource is Inactive.";
                        }
                    }

                    else
                    {
                        if (statusFlag == false)
                            SaveProjectDetails();   // Method to save the project details...

                        else
                        {
                            lblPjct.Visible = true;
                            lblPjct.Text = "Project Entry cannot be Added since Resource is Inactive.";
                            dgProjectDetails.Columns[8].Visible = false;
                            dgProjectDetails.Columns[9].Visible = false;
                        }
                    }

                }
                //=============== End of On click event code ==================

                // After the event/ method, again update the session  
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                DisplayProjectDetails(ddlResources.SelectedValue.ToString()); // Method to display project details...
            }

            else  // If Page Refreshed
            {
                // Do nothing 
            }
            if (statusFlag == false &&validation1==0)
            {
                ClearControls();  // Method to clear controls...
            }
            if (validation1 == 1 )
            {
                txtAHours.Enabled = true;
                txtEHours.Enabled = true;
                txtAHours2.Enabled = true;
                txtEHours2.Enabled = true;

            }


            year = Convert.ToInt32(Session["Year"]);
            month = Convert.ToInt32(Session["Month"]);
            period = Session["Period"].ToString();
            weekindex = Convert.ToInt16(Session["WeekIndex"]);
            ddlRole.Text = Session["RoleText"].ToString();
            int newmonth = Convert.ToInt32(Session["Month"]);
            ddlYear.SelectedValue = Session["Year"].ToString();
            ddlMonth.SelectedValue = (newmonth + 1).ToString();

            GetWeek(); // Method to populate the weeks in selected Month and Year...

            ddlWeek.SelectedIndex = Convert.ToInt32(Session["WeekIndex"]);
            ddlProjects.Focus();

            dgProjectDetails.Enabled = true;
            //SplitOverlappingWeek();
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            int projectId = Convert.ToInt32(ddlProjects.SelectedValue);

            if (projectId != 0)
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
                        Label7.Text = "Dashboard for this period has already been finalized, so you won't be able to make any edits here.";
                        dgProjectDetails.Columns[8].Visible = false;
                        dgProjectDetails.Columns[9].Visible = false;
                        lblError2.Text = "";
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
            CheckAllDropdownSelected();
            if (lblesimation2.Visible)
            {
                txtEHours2.Enabled = false;
                txtAHours2.Enabled = false;
                lblEstimation.Visible = false;
                lblMsgStatus.Visible = false;
            }
            if (lblEstimation.Visible)
            {
                txtEHours2.Enabled = false;
                txtAHours2.Enabled = false;
                txtEHours.Enabled = false;
                txtAHours.Enabled = false;
            }
            if (lblFuture.Visible)
            {
                txtEHours2.Enabled = false;
                txtAHours2.Enabled = false;
                txtEHours.Enabled = false;
                txtAHours.Enabled = false;
                lblEstimation.Visible = false;
                lblesimation2.Visible = false;
            }
            if (Request.QueryString["op"] != null && Request.QueryString["op"] == "E")
            {
            }
            else
            {
                SplitOverlappingWeek();
            }
            CheckEntryForProject();
            // SplitOverlappingWeek();
        }

        protected void ddlPhase_SelectedIndexChanged(object sender, EventArgs e)
        {
            int projectId = Convert.ToInt32(ddlProjects.SelectedValue);
            if (projectId != 0)
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
                        Label7.Text = "Dashboard for this period has already been finalized, so you won't be able to make any edits here.";
                        dgProjectDetails.Columns[8].Visible = false;
                        dgProjectDetails.Columns[9].Visible = false;
                        lblError2.Text = "";
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

            CheckAllDropdownSelected();
            if (lblesimation2.Visible)
            {
                txtEHours2.Enabled = false;
                txtAHours2.Enabled = false;
                lblEstimation.Visible = false;
                lblMsgStatus.Visible = false;
            }
            if (lblEstimation.Visible)
            {
                txtEHours2.Enabled = false;
                txtAHours2.Enabled = false;
                txtEHours.Enabled = false;
                txtAHours.Enabled = false;
            }
            if (lblFuture.Visible)
            {
                txtEHours2.Enabled = false;
                txtAHours2.Enabled = false;
                txtEHours.Enabled = false;
                txtAHours.Enabled = false;
                lblEstimation.Visible = false;
                lblesimation2.Visible = false;
            }
            CheckEntryForProject();
            if (Request.QueryString["op"] != null && Request.QueryString["op"] == "E")
            {
            }
            else
            {
                SplitOverlappingWeek();
            }
            //SplitOverlappingWeek();
        }



        /// <summary>
        /// Get Total Hours Of a Resource of a week
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="timeTrackerId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="estimatedTime"></param>
        /// <param name="actualTime"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        protected bool GetTotalHoursOfResource(int resourceId, int timeTrackerId, DateTime fromDate, DateTime toDate, string estimatedTime, string actualTime, int mode, out int exceededHours)
        {
            validation2 = 0;
            int maxActuals = 0;
            object closingDate = null;
            object startingDate = null;
            bool canInsert = false;
            functioncount += 1; // this function will called twice if it is a split week.
            exceededHours = 0;
            int estTime = estimatedTime == string.Empty ? 0 : Convert.ToInt32(estimatedTime);
            decimal actTime = actualTime == string.Empty ? 0 : Convert.ToDecimal(actualTime);
            try
            {
                int totalHours = 0;
                string dateClosed = string.Empty;
                int estimatedHourValue = 0;
                decimal actualHourValue = 0;
                DateTime d1 = Convert.ToDateTime(fromDate);
                DateTime d2 = Convert.ToDateTime(toDate).AddDays(1);
                double NrOfDays = 0;
                int numOfWorkingDays = 0;
                TimeSpan t = d2 - d1;
                int diff = 0;
                var toDay = DateTime.Now.Date;
                string query = string.Format("select MaintClosingDate from [dbo].[Project] where ProjectName='{0}'", ddlProjects.SelectedItem.Text);
                closingDate = PMOscar.BaseDAL.ExecuteScalar(query);
                string query1 = string.Format("select ProjStartDate from [dbo].[Project] where ProjectName='{0}'", ddlProjects.SelectedItem.Text);
                startingDate = PMOscar.BaseDAL.ExecuteScalar(query1);

                int diff1 = 0;
                for (var dayCount = 0; dayCount <= t.Days - 1; dayCount++)
                {
                    var testDate = fromDate.AddDays(dayCount);
                    DayOfWeek day = Convert.ToDateTime(testDate).DayOfWeek;
                    if (day == DayOfWeek.Saturday)
                    {
                        diff1 += 1;
                    }
                    else if (day == DayOfWeek.Sunday)
                    {
                        diff1 += 1;
                    }
                }
                maxEstimation = (t.Days - diff1) * 8;


                for (var dayCount = 0; dayCount <= t.Days - 1; dayCount++)
                {
                    var testDate = fromDate.AddDays(dayCount);
                    DayOfWeek day = Convert.ToDateTime(testDate).DayOfWeek;

                }
                maxActuals = (t.Days) * 8;
                // Validatio for estimation hours

                if (Convert.ToDateTime(closingDate) >= d1 && Convert.ToDateTime(closingDate) >= Convert.ToDateTime(toDate) && Convert.ToDateTime(startingDate) <= d1 && Convert.ToDateTime(startingDate) <= Convert.ToDateTime(toDate)) //Project which is already started but not yet closed.
                {
                    for (var dayCount = 0; dayCount <= t.Days - 1; dayCount++)
                    {
                        var testDate = fromDate.AddDays(dayCount);
                        DayOfWeek day = Convert.ToDateTime(testDate).DayOfWeek;
                        if (day == DayOfWeek.Saturday)
                        {
                            diff += 1;
                        }
                        else if (day == DayOfWeek.Sunday)
                        {
                            diff += 1;
                        }
                    }
                    NrOfDays = t.Days - diff;
                    numOfWorkingDays = Convert.ToInt32(NrOfDays);
                    totalHours = numOfWorkingDays * Convert.ToInt16(PMOscar.Utility.EnumTypes.Estimation.MinEstimation);
                }

                else if (Convert.ToDateTime(closingDate) >= d1 && Convert.ToDateTime(closingDate) <= Convert.ToDateTime(toDate) && Convert.ToDateTime(startingDate) <= d1 && Convert.ToDateTime(startingDate) <= Convert.ToDateTime(toDate)) //Project closing date falls between the selected week.
                {
                    TimeSpan T1 = Convert.ToDateTime(closingDate) - d1;
                    for (var dayCount = 0; dayCount <= T1.Days; dayCount++)
                    {
                        var testDate = fromDate.AddDays(dayCount);
                        DayOfWeek day = Convert.ToDateTime(testDate).DayOfWeek;
                        if (day == DayOfWeek.Saturday)
                        {
                            diff += 1;
                        }
                        else if (day == DayOfWeek.Sunday)
                        {
                            diff += 1;
                        }
                    }
                    NrOfDays = (T1.Days - diff) + 1;
                    numOfWorkingDays = Convert.ToInt32(NrOfDays);
                    totalHours = numOfWorkingDays * Convert.ToInt16(PMOscar.Utility.EnumTypes.Estimation.MinEstimation);
                }

                else if (Convert.ToDateTime(closingDate) >= d1 && Convert.ToDateTime(closingDate) >= Convert.ToDateTime(toDate) && Convert.ToDateTime(startingDate) > d1 && Convert.ToDateTime(startingDate) <= Convert.ToDateTime(toDate)) // Project start date falls in between the selected week.
                {

                    TimeSpan T1 = Convert.ToDateTime(toDate) - Convert.ToDateTime(startingDate);
                    for (var dayCount = 0; dayCount <= T1.Days; dayCount++)
                    {
                        var testDate = Convert.ToDateTime(startingDate).AddDays(dayCount);
                        DayOfWeek day = Convert.ToDateTime(testDate).DayOfWeek;
                        if (day == DayOfWeek.Saturday)
                        {
                            diff += 1;
                        }
                        else if (day == DayOfWeek.Sunday)
                        {
                            diff += 1;
                        }
                    }
                    NrOfDays = (T1.Days - diff) + 1;
                    numOfWorkingDays = Convert.ToInt32(NrOfDays);
                    totalHours = numOfWorkingDays * Convert.ToInt16(PMOscar.Utility.EnumTypes.Estimation.MinEstimation);
                }

                else if (Convert.ToDateTime(startingDate) > d1 && Convert.ToDateTime(startingDate) > Convert.ToDateTime(toDate)) // project which is not started
                {
                    lblFuture.Visible = true;
                    lblFuture.Text = "This Project Starts on " + Convert.ToDateTime(startingDate).ToString(PMOscar.Core.Constants.AddRole.DATEFORMAT) + ".";
                    totalHours = 0;
                }

                else if (Convert.ToDateTime(closingDate) < d1 && Convert.ToDateTime(closingDate) < Convert.ToDateTime(toDate)) // closed projects
                {
                    lblEstimation.Visible = true;
                    lblEstimation.Text = "This Project closed on " + Convert.ToDateTime(closingDate).ToString(PMOscar.Core.Constants.AddRole.DATEFORMAT) + ".";
                    validation2 = 1;
                    totalHours = 0;
                    if (functioncount == 2 && txtEHours2.Text == string.Empty && isValid) //makes the message disabled in split week cases.
                    {
                        lblEstimation.Visible = false;
                    }
                }




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
                        actualHourValue = 0;

                        if (estTime == 0 && actTime == 0)
                        {
                            lblEstimation.Visible = true;
                            lblEstimation.Text = "Enter Estimation/Actual Hours..!";
                            canInsert = false;
                        }

                        if (estTime != 0 && actTime != 0)
                        {
                            if (estimatedHourValue <= maxEstimation)
                            {
                                int totalEstimatedHours = estimatedHourValue + estTime;
                                decimal totalActualHours = actualHourValue + actTime;

                                if (totalEstimatedHours <= maxEstimation)
                                {
                                    if (estTime <= totalHours)
                                    {
                                        canInsert = true;
                                        isValid = canInsert;

                                    }
                                    else
                                    {
                                        canInsert = false;
                                        exceededHours = totalHours; ;
                                    }
                                }
                                else
                                {
                                    canInsert = false;
                                    exceededHours = maxEstimation;
                                }
                            }
                            else
                            {
                                canInsert = false;
                                exceededHours = maxEstimation;

                            }
                        }
                        else if (estTime == 0)
                        {
                            //    if (actualHourValue <= maxActuals)
                            //    {
                            //        decimal totalActualHours = actualHourValue + actTime;

                            //        if (totalActualHours <= maxActuals)
                            //        {
                            //            canInsert = true;
                            //            isValid = canInsert;
                            //        }
                            //        else
                            //        {
                            //            canInsert = false;
                            //            exceededHours = totalHours;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        canInsert = false;
                            //        exceededHours = maxEstimation;
                            //    }
                            canInsert = true;
                            isValid = canInsert;
                        }
                        else if (actTime == 0)
                        {
                            if (actualHourValue <= totalHours)
                            {
                                int totalEstimatedHours = estimatedHourValue + estTime;
                                calcEstimation += estTime;

                                if (totalEstimatedHours <= maxEstimation)
                                {
                                    if (estTime <= totalHours)
                                    {
                                        canInsert = true;
                                        isValid = canInsert;

                                    }
                                    else
                                    {
                                        canInsert = false;
                                        exceededHours = totalHours; ;
                                    }
                                }
                                else
                                {
                                    canInsert = false;
                                    exceededHours = maxEstimation;
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
                        if (estTime <= totalHours)
                        {
                            canInsert = true;
                            isValid = canInsert;
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
                    if (estTime <= totalHours)
                    {
                        canInsert = true;
                        isValid = canInsert;
                    }
                    else
                    {
                        canInsert = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

            return canInsert;
        }

        // Method to bind resources in the DropDownList...
        private void BindDropDownResources()
        {
            var UID = Session["UserRoleID"];
            int selectedResourceId = Convert.ToInt32(resourceId);
            parameter = new List<SqlParameter>();
            SetPeriod();
            string fromDate = Convert.ToString(dayFrom);
            string toDate = Convert.ToString(dayTo);
            parameter.Add(new SqlParameter("@dayfrom", fromDate));
            parameter.Add(new SqlParameter("@dayto", toDate));
            DataTable dtResources = BaseDAL.ExecuteSPDataTable("GetResources", parameter);
            ddlResources.DataSource = dtResources;
            ddlResources.DataTextField = "ResourceName";
            ddlResources.DataValueField = "ResourceId";
            ddlResources.DataBind();
            ddlResources.Items.Insert(0, new ListItem("Select", "0"));
            if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {
                ddlResources.Enabled = false;
            }

        }

        protected void ddlResources_SelectedIndexChanged(object sender, EventArgs e)
        {
            //select team for the resource
            ddlTeam.ClearSelection();
            resourceId = ddlResources.SelectedItem.Value;
            string resorceName = ddlResources.SelectedItem.Text;
            ClearControls(); // Method to clear controls...
            BindDropDownTeams(resourceId);
            DisplayProjectDetails(resourceId); // Method to display project details...                             
            SplitOverlappingWeek(); // Split overlapping week and show/hide additional field for entering Estimated and Actual values.
            BindGridView(resourceId, year);
        }

        #endregion

    }
}
