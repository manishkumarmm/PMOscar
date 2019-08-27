
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourcePlanning.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
//  Description  : Displays assignment of expected and actual hours by resource and project.
//  Author       : 
//  Created Date : 21 February 2011
//  Modified By  : Vibin M.B
//  Modified Date: 2-04-2018
// </summary>
// -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
using PMOscar.Core;

namespace PMOscar
{
    public partial class ResourcePlanning : System.Web.UI.Page
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
        string dayFrom = string.Empty;
        string dayTo = string.Empty;
        int fromMonth = 0;
        int toMonth = 0;
        int fromYear = 0;
        int toYear = 0;
        int month = 0;
        public int budgetHours;
        public int white;
        public int alter;
        public Decimal actualHours;
        private string prevResourceName = string.Empty;
        private string colorResourceName = string.Empty;
        private string indexValue = string.Empty;
        public int userid;
        public int rowIndex;
     
        

        #endregion

        #region"Page Events"

        protected void Page_Load(object sender, System.EventArgs e)
        {
            syncSuccess.Text = string.Empty;
            if (Session["UserName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                userid = Convert.ToInt16(Session["UserID"]);
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

                if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 4=Employee)
                {
                    #region
                    //Only User tab is Visible for Sys admin
                    btnClone.Visible = false;
                    btnExport.Visible = false;
                    btnAct_hour.Visible = false;
                    lnkChng.Visible = false;

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
                    //Response.Redirect("AccessDenied.aspx");
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
            if (!IsPostBack)
            {

                if ((Request.QueryString["redirect"] != null))
                {
                    FillYears(); // To fill the years in the DropDownList
                    string year = Request.QueryString["year"] != null ? Request.QueryString["year"].ToString() : null;
                    string month = Request.QueryString["month"] != null ? Request.QueryString["month"].ToString() : null;
                    ddlYear.SelectedValue = year;
                    ddlMonth.SelectedIndex = Convert.ToInt16(month);
                    GetWeek(); // To develop the week in the DropDownList
                    ddlWeek.SelectedIndex = Request.QueryString["weekindex"] != null ? Convert.ToInt16(Request.QueryString["weekindex"]) : 0;
                    BindGridView();  // To fill the resouce planning list in the grid                  
                }
                else
                {
                    FillYears();
                    if (Session["dtClonevalue"] != null)
                    {
                        DataTable dt = (DataTable)Session["dtClonevalue"];
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0].ItemArray[0].ToString() == "N")
                            {
                                string year = dt.Rows[0].ItemArray[1].ToString();
                                ddlYear.SelectedValue = year;
                                string month = dt.Rows[0].ItemArray[2].ToString();
                                ddlMonth.SelectedValue = month;
                                GetWeek();
                                ddlWeek.SelectedIndex = Convert.ToInt16(dt.Rows[0].ItemArray[3].ToString());

                            }
                            else
                            {
                                string year = DateTime.Now.Year.ToString();
                                ddlYear.SelectedValue = year;
                                string month = DateTime.Now.Month.ToString();
                                ddlMonth.SelectedValue = month;
                                GetWeek();

                            }
                        }

                        dt.Dispose();
                        Session["dtClonevalue"] = null;
                    }
                    else
                    {

                        string year = DateTime.Now.Year.ToString();
                        ddlYear.SelectedValue = year;
                        string month = DateTime.Now.Month.ToString();
                        ddlMonth.SelectedValue = month;
                        GetWeek();
                        string Currentmonth = ddlMonth.SelectedItem.Value.ToString();
                        string from = ddlWeek.SelectedItem.Text.ToString();
                        string yr = ddlYear.SelectedItem.Text.ToString();
                        string timeperiod = string.Empty;
                        string mnth = string.Empty;
                        object result = string.Empty;                       
                        if (from != string.Empty)
                        {
                            string[] arrfrom = from.Split('-');
                            if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.Last) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.First))
                            {
                                int Newmonth = Convert.ToInt16(Currentmonth) + 1;
                                mnth = Newmonth.ToString();
                                timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + mnth + "/" + yr;
                                string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                                result = PMOscar.BaseDAL.ExecuteScalar(query);

                            }
                            else if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) == (int)(PMOscar.Utility.EnumTypes.Month.Last))
                            {
                                string monthVal = PMOscar.Utility.EnumTypes.Month.First.ToString();
                                int yri = Convert.ToInt16(yr) + 1;
                                string yearVal = yri.ToString();
                                timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + monthVal + "/" + yearVal;
                                string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                                result = PMOscar.BaseDAL.ExecuteScalar(query);
                            }

                            else if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.Last) && Convert.ToInt16(Currentmonth) == (int)(PMOscar.Utility.EnumTypes.Month.First))
                            {
                                int Newmonth = (int)(PMOscar.Utility.EnumTypes.Month.Last);
                                int yri = Convert.ToInt16(yr) - 1;
                                mnth = Newmonth.ToString();
                                string yearVal = yri.ToString();
                                timeperiod = arrfrom[0] + "/" + mnth + "/" + yearVal + "-" + arrfrom[1] + "/" + Currentmonth + "/" + yr;
                                string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                                result = PMOscar.BaseDAL.ExecuteScalar(query);
                            }

                            else
                            {
                                timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + Currentmonth + "/" + yr;
                                string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                                result = PMOscar.BaseDAL.ExecuteScalar(query);
                            }

                            if (result != null)
                            {
                                if (result.ToString().Equals("F"))
                                {
                                    btnAct_hour.Enabled = false;
                                   // ImageButton1.enablled = false;
                                }
                                else
                                {
                                    btnAct_hour.Enabled = true;

                                }
                            }
                            else
                            {
                                btnAct_hour.Enabled = true;

                            }
                        }

                    }
                    BindGridView();
                }


            }
            Session["TimeTrackerId"] = null;

            btnClone.Attributes.Add("onclick", "window.open('ResourceClone.aspx','','height=100,width=500');return false");
            lblWeekStatus.Text = ddlWeek.SelectedItem.Text.ToString() + ',' + ' ' + ddlMonth.SelectedItem.Text.ToString() + ',' + ' ' + ddlYear.SelectedItem.Text.ToString();
            ddlYear.Focus();


        }

        #endregion

        #region"Methods"

        // Method for Populating Year Combobox...
        private void FillYears()
        {
            for (int yearCount = DateTime.Now.Year - 4; yearCount <= DateTime.Now.Year + 1; yearCount++)
            {
                ListItem lstYears = new ListItem();
                lstYears.Value = yearCount.ToString();
                lstYears.Text = yearCount.ToString();
                ddlYear.Items.Add(lstYears);
                ddlYear.DataBind();
            }
        }

        // Method for getting dates and months...
        private void SetPeriod()
        {

            int deductionFactor = 0;
            month = ddlWeek.SelectedIndex == 0 && Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0,
            ddlWeek.SelectedItem.ToString().IndexOf("-"))) > Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1,
            ddlWeek.SelectedItem.ToString().Length - (ddlWeek.SelectedItem.ToString().Length > 3 ? 3 : 2))) ? Convert.ToInt16(ddlMonth.SelectedIndex) : (ddlWeek.SelectedIndex != 0 &&
            Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) >
            Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1,
            ddlWeek.SelectedItem.ToString().Length - (ddlWeek.SelectedItem.ToString().Length > 3 ? 3 : 2))) ? Convert.ToInt16(ddlMonth.SelectedIndex + 2) :
            Convert.ToInt16(ddlMonth.SelectedIndex + 1));

            if (ddlWeek.SelectedItem.ToString().Length > 4)
                deductionFactor = 3;
            else if (ddlWeek.SelectedItem.ToString().Length == 3)
                deductionFactor = 2;
            else
                deductionFactor = ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().Length - 3, 1).ToString() == "-" ? 2 : 3;

            if (ddlWeek.SelectedIndex == 0 && Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) > Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - deductionFactor)))
            {
                fromMonth = Convert.ToInt16(ddlMonth.SelectedIndex) == 0 ? 12 : Convert.ToInt16(ddlMonth.SelectedIndex);
                toMonth = ddlMonth.SelectedIndex == 11 ? (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) : (ddlMonth.SelectedIndex == 11 ? (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) : (Convert.ToInt16(ddlMonth.SelectedIndex) + 1));
                fromYear = Convert.ToInt16(ddlMonth.SelectedIndex) == 0 ? (Convert.ToInt16(ddlYear.SelectedValue) - 1) : (Convert.ToInt16(ddlMonth.SelectedIndex) == 11 ? (Convert.ToInt16(ddlYear.SelectedValue)) : Convert.ToInt16(ddlYear.SelectedValue));
                toYear = Convert.ToInt16(ddlYear.SelectedValue);
                dayFrom = fromYear + "-" + fromMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-")));
                dayTo = toYear + "-" + toMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - deductionFactor));
            }
            else if (ddlWeek.SelectedIndex != 0 && Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) >
                                                   Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1,
                                                   ddlWeek.SelectedItem.ToString().Length - deductionFactor)))
            {

                fromMonth = ddlMonth.SelectedIndex == 11 ? (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) : (Convert.ToInt16(ddlMonth.SelectedIndex) + 1);
                toMonth = ddlMonth.SelectedIndex == 11 ? 1 : (Convert.ToInt16(ddlMonth.SelectedIndex) + 2);
                fromYear = Convert.ToInt16(ddlMonth.SelectedIndex) == 0 ? (Convert.ToInt16(ddlYear.SelectedValue)) : (Convert.ToInt16(ddlMonth.SelectedIndex) == 11 ? Convert.ToInt16(ddlYear.SelectedValue) : Convert.ToInt16(ddlYear.SelectedValue));
                toYear = Convert.ToInt16(ddlMonth.SelectedIndex) == 11 ? Convert.ToInt16(ddlYear.SelectedValue) + 1 : Convert.ToInt16(ddlYear.SelectedValue);
                dayFrom = fromYear + "-" + fromMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-")));
                dayTo = toYear + "-" + toMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - deductionFactor));

            }

            else
            {
                fromMonth = ddlMonth.SelectedIndex == 11 ? 1 : (Convert.ToInt16(ddlMonth.SelectedIndex) + 1);
                toMonth = ddlMonth.SelectedIndex == 11 ? 1 : (Convert.ToInt16(ddlMonth.SelectedIndex) + 1);
                fromYear = Convert.ToInt16(ddlYear.SelectedValue);
                toYear = Convert.ToInt16(ddlYear.SelectedValue);
                dayFrom = fromYear + "-" + (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-")));
                if ((Convert.ToInt16(ddlMonth.SelectedIndex) + 1) == 2)
                {
                    if (fromYear % 4 == 0 || (!(fromYear % 100 == 0)) && (fromYear % 400 == 0))
                    {
                        dayTo = fromYear + "-" + (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - deductionFactor));
                    }
                    else if (Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - deductionFactor)) == 29)
                        dayTo = fromYear + "-" + (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) + "-" + (Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - deductionFactor)) - 1);
                    else
                        dayTo = fromYear + "-" + (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - deductionFactor));
                }
                else
                    dayTo = fromYear + "-" + (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - deductionFactor));

            }
            DataTable dtpopup = new DataTable();
            dtpopup.Columns.Add("year");
            dtpopup.Columns.Add("month");
            dtpopup.Columns.Add("fromdate");
            dtpopup.Columns.Add("todate");
            DataRow dr = dtpopup.NewRow();
            dr["year"] = ddlYear.SelectedValue;
            dr["month"] = ddlMonth.SelectedValue;
            dr["fromdate"] = dayFrom;
            dr["todate"] = dayTo;
            dtpopup.Rows.Add(dr);
            Session["dtpopup"] = dtpopup;
            dtpopup.Dispose();
           
            
        }

        // Binding and displaying the resource details in the gridView...
        private void BindGridView()
        {
            var EmployeeCode = DBNull.Value.ToString();
            var UID = Session["UserRoleID"];

            BaseDAL.CopyEmployeeLeave();

            SetPeriod(); // Method for getting dates and months...          
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Year", ddlYear.SelectedValue));
            parameter.Add(new SqlParameter("@Month", ddlMonth.SelectedIndex + 1));
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));

            if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
            {
                var UName = Session["UserName"];
                string query = string.Format("select EmployeeCode  from [dbo].[User] where UserName='{0}'", UName);
                Object obj = PMOscar.BaseDAL.ExecuteScalar(query);
                EmployeeCode = obj.ToString();
                parameter.Add(new SqlParameter("@EmployeeCode", EmployeeCode));

            }

            DataSet dsTimeTrackerDetails = PMOscar.BaseDAL.ExecuteSPDataSet("[GetTimeTrackerDetails]", parameter);
            string month = string.Empty;
            DataTable timetrackerHistory = null;
                List<SqlParameter> parameter1 = new List<SqlParameter>();
                parameter1.Add(new SqlParameter("@Year", ddlYear.SelectedValue));
                parameter1.Add(new SqlParameter("@dayfrom", dayFrom));
                parameter1.Add(new SqlParameter("@dayto", dayTo));
                parameter1.Add(new SqlParameter("@UserId", UID));
                if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) //Checking role for Employee = 5
                {
                    timetrackerHistory = BaseDAL.ExecuteSPDataTable("GetTimeTrackerActualHoursHistory", parameter1);
                    if (timetrackerHistory != null)
                    {
                        gvTimeTrackerAudit.DataSource = timetrackerHistory;
                        gvTimeTrackerAudit.DataBind();
                    }

                }
               

            if (dsTimeTrackerDetails != null)
            {

                DataTable dt = dsTimeTrackerDetails.Tables[0];
                DataTable dtGrid = dt.DefaultView.ToTable(true, "ResourceName", "ResourceId", "Role", "RoleID", "DefaultRole");
                DataTable dtProjs = dt.DefaultView.ToTable(true, "ProjectName", "ProjectId");
                DataTable dtTotal = dt.DefaultView.ToTable(true, "ESTTotal", "ACTTotal");
                DataTable dtLeave = dt.DefaultView.ToTable(true, "ResourceId", "Leave");
                DataRow[] drProjects = dtProjs.Select("", "ProjectName ASC");
                foreach (DataRow drProj in drProjects)
                {
                    if (drProj["ProjectName"].ToString() != "" && drProj["ProjectName"].ToString() != "Open" && drProj["ProjectName"].ToString() != "Admin")
                    {
                        dtGrid.Columns.Add(drProj["ProjectName"].ToString() + "_Estimated" + "/" + drProj["ProjectId"].ToString(), typeof(string));
                        dtGrid.Columns.Add(drProj["ProjectName"].ToString() + "_Actual", typeof(string));
                        dtGrid.Columns.Add(drProj["ProjectName"].ToString() + "_BudgetHours", typeof(string));
                        dtGrid.Columns.Add(drProj["ProjectName"].ToString() + "_WeeklyComments", typeof(string));
                        dtGrid.Columns.Add(drProj["ProjectName"].ToString() + "_Status", typeof(string));
                    }
                }

                DataSet dsTimes = BaseDAL.ExecuteDataSet("Select ProjectId,ProjectName From dbo.Project Where ProjectName In ('Admin','Open')");

                string admin = string.Empty;
                string open = string.Empty;

                if (dsTimes.Tables[0].Rows.Count > 1)
                {
                    admin = dsTimes.Tables[0].Rows[0].ItemArray[0].ToString();
                    open = dsTimes.Tables[0].Rows[1].ItemArray[0].ToString();
                }

                if (dsTimes.Tables[0].Rows.Count > 0)
                {
                    if (dsTimes.Tables[0].Rows[0].ItemArray[1].ToString() == "Admin")
                        admin = dsTimes.Tables[0].Rows[0].ItemArray[0].ToString();

                    else
                        open = dsTimes.Tables[0].Rows[0].ItemArray[0].ToString();
                }

                // Developing and displaying columns of expected hours,actual hours,budjet hours,weekly comments,status in the grid view...

                if (admin != string.Empty)
                {
                    dtGrid.Columns.Add("Admin_Estimated/" + admin, typeof(string));
                    dtGrid.Columns.Add("Admin_Actual", typeof(string));
                    dtGrid.Columns.Add("Admin_BudgetHours", typeof(string));
                    dtGrid.Columns.Add("Admin_WeeklyComments", typeof(string));
                    dtGrid.Columns.Add("Admin_Status", typeof(string));

                }
                else
                {
                    dtGrid.Columns.Add("Admin_Estimated", typeof(string));
                    dtGrid.Columns.Add("Admin_Actual", typeof(string));
                    dtGrid.Columns.Add("Admin_BudgetHours", typeof(string));
                    dtGrid.Columns.Add("Admin_WeeklyComments", typeof(string));
                    dtGrid.Columns.Add("Admin_Status", typeof(string));
                }

                if (open != string.Empty)
                {
                    dtGrid.Columns.Add("Open_Estimated/" + open, typeof(string));
                    dtGrid.Columns.Add("Open_Actual", typeof(string));
                    dtGrid.Columns.Add("Open_BudgetHours", typeof(string));
                    dtGrid.Columns.Add("Open_WeeklyComments", typeof(string));
                    dtGrid.Columns.Add("Open_Status", typeof(string));

                }
                else
                {
                    dtGrid.Columns.Add("Open_Estimated", typeof(string));
                    dtGrid.Columns.Add("Open_Actual", typeof(string));
                    dtGrid.Columns.Add("Open_BudgetHours", typeof(string));
                    dtGrid.Columns.Add("Open_WeeklyComments", typeof(string));
                    dtGrid.Columns.Add("Open_Status", typeof(string));

                }

                dtGrid.Columns.Add("Leave", typeof(Double));
                dtGrid.Columns.Add(dtTotal.Columns[0].ToString(), typeof(Double));
                dtGrid.Columns.Add(dtTotal.Columns[1].ToString(), typeof(Double));
                dtGrid.Columns.Add("EstimationPercentage", typeof(Double));
                dtGrid.Columns.Add("EstimationRoleID", typeof(Double));

                string projectNameCheck = string.Empty;
                string role = string.Empty;

                // Developing and displaying values of expected and actual hours in the grid view...

                foreach (DataRow dr in dtGrid.Rows)
                {
                    DataRow[] drResourceProjects = dt.Select("ResourceId=" + dr["ResourceId"].ToString() + " And   Role='" + dr["Role"].ToString() + "' And  ProjectName <>''");
                    Double ESTtotal = 0.0;
                    Double ACTtotal = 0.0;
                    foreach (DataRow drRP in drResourceProjects)
                    {
                        dr[drRP["ProjectName"].ToString() + "_Estimated" + "/" + drRP["ProjectId"].ToString()] = drRP["Estimated"] == DBNull.Value ? 0 : Convert.ToInt64(drRP["Estimated"]);
                        dr[drRP["ProjectName"].ToString() + "_Actual"] = drRP["Actual"] == DBNull.Value ? "" : drRP["Actual"];
                        dr[drRP["ProjectName"].ToString() + "_BudgetHours"] = drRP["BudgetHours"] == DBNull.Value ? 0 : Convert.ToInt64(drRP["BudgetHours"]);
                        dr[drRP["ProjectName"].ToString() + "_WeeklyComments"] = drRP["WeeklyComments"] == DBNull.Value ? "" : drRP["WeeklyComments"];
                        dr[drRP["ProjectName"].ToString() + "_Status"] = drRP["Status"] == DBNull.Value ? "" : drRP["Status"];
                        dr["EstimationPercentage"] = drRP["EstimationPercentage"] == DBNull.Value ? "" : drRP["EstimationPercentage"];
                        dr["EstimationRoleID"] = drRP["EstimationRoleID"] == DBNull.Value ? "" : drRP["EstimationRoleID"];
                        dr["ESTTotal"] = System.Convert.ToInt64(ESTtotal) + System.Convert.ToInt64(drRP["ESTTotal"] == DBNull.Value ? 0 : drRP["ESTTotal"]);
                        dr["ACTTotal"] = System.Convert.ToDouble(ACTtotal) + System.Convert.ToDouble(drRP["ACTTotal"] == DBNull.Value ? 0 : drRP["ACTTotal"]);
                        ESTtotal = ESTtotal + System.Convert.ToInt64(drRP["ESTTotal"] == DBNull.Value ? 0 : drRP["ESTTotal"]);
                        ACTtotal = ACTtotal + System.Convert.ToDouble(drRP["ACTTotal"] == DBNull.Value ? 0 : drRP["ACTTotal"]);

                        projectNameCheck = dr["ResourceName"].ToString();
                        role = drRP["Role"].ToString();
                    }
                }

                foreach (DataRow dr in dtLeave.Rows)
                {
                    DataRow[] drRes = dtGrid.Select("ResourceId=" + dr["ResourceId"].ToString());
                    if (drRes.Length > 0)
                    {
                        decimal leave = Convert.ToDecimal(dr["Leave"]) * 8;

                        if (leave > 0)
                        {
                            if (drRes.Length == 1)
                            {
                                drRes[0]["Leave"] = leave;
                            }
                            else
                            {
                                for (int i = 0; i < drRes.Length; i++)
                                {
                                    if (drRes[i]["DefaultRole"] != DBNull.Value && drRes[i]["RoleID"] != DBNull.Value)
                                    {
                                        if (Convert.ToInt32(drRes[i]["DefaultRole"]) == Convert.ToInt32(drRes[i]["RoleID"]))
                                        {
                                            drRes[i]["Leave"] = leave;
                                            break;
                                        }
                                    }
                                }
                            }
                        }


                        if (Convert.ToDecimal(drRes[0]["Admin_Actual"].ToString() == "" ? 0 : drRes[0]["Admin_Actual"]) >= leave)
                        {
                            drRes[0]["Admin_Actual"] = Convert.ToDecimal(drRes[0]["Admin_Actual"].ToString() == "" ? 0 : drRes[0]["Admin_Actual"]) - leave;
                        }

                        drRes[0]["Admin_Actual"] = drRes[0]["Admin_Actual"].ToString() == "" || Convert.ToDecimal(drRes[0]["Admin_Actual"]) == 0 ? "" : drRes[0]["Admin_Actual"];
                    }
                }

                DataRow drTotal = dtGrid.NewRow();
                drTotal[2] = "Total";
                dtGrid.Rows.Add(drTotal);

                int lastrowIndex = dtGrid.Rows.Count - 1;
                DataRow drlastrow = dtGrid.Rows[lastrowIndex];
                DataRow[] drRows = dt.Select("ProjectName <>' '");

                // Developing and displaying total values of expected and actual hours in the grid view...

                foreach (DataRow drRP in drRows.Distinct())
                {
                    if (drlastrow.ItemArray[0].ToString() != string.Empty)
                    {
                        drlastrow["ESTTotal"] = System.Convert.ToInt64(drlastrow["ESTTotal"] == DBNull.Value ? 0 : drlastrow["ESTTotal"]) + System.Convert.ToInt64(drRP["ESTTotal"] == DBNull.Value ? 0 : drRP["ESTTotal"]);
                        drlastrow["ACTTotal"] = System.Convert.ToDouble(drlastrow["ACTTotal"] == DBNull.Value ? 0 : drlastrow["ACTTotal"]) + System.Convert.ToDouble(drRP["ACTTotal"] == DBNull.Value ? 0 : drlastrow["ACTTotal"]);
                        drlastrow[drRP["ProjectName"].ToString() + "_Estimated" + "/" + drRP["ProjectId"].ToString()] = System.Convert.ToInt64(drlastrow[drRP["ProjectName"].ToString() + "_Estimated"]) + System.Convert.ToInt64(drRP["Estimated"]);
                        drlastrow[drRP["ProjectName"].ToString() + "_Actual"] = System.Convert.ToDouble(drlastrow[drRP["ProjectName"].ToString() + "_Actual"]) + System.Convert.ToDouble(drRP["Actual"]);
                        drlastrow[drRP["ProjectName"].ToString() + "_BudgetHours"] = System.Convert.ToInt64(drlastrow[drRP["ProjectName"].ToString() + "_BudgetHours"]) + System.Convert.ToInt64(drRP["BudgetHours"]);
                    }
                    else
                    {
                        if (drlastrow["ESTTotal"].ToString() != string.Empty)
                        {
                            drlastrow["ESTTotal"] = System.Convert.ToInt64(drlastrow["ESTTotal"] == DBNull.Value ? 0 : drlastrow["ESTTotal"]) + (drRP["ESTTotal"].ToString() == string.Empty ? 0.0 : System.Convert.ToInt64(drRP["ESTTotal"]));
                            drlastrow["ACTTotal"] = System.Convert.ToDouble(drlastrow["ACTTotal"] == DBNull.Value ? 0 : drlastrow["ACTTotal"]) + (drRP["ACTTotal"].ToString() == string.Empty ? 0.0 : System.Convert.ToDouble(drRP["ACTTotal"]));
                        }
                        else
                        {
                            drlastrow["ESTTotal"] = drRP["ESTTotal"] == DBNull.Value ? 0 : System.Convert.ToInt64(drRP["ESTTotal"]);
                            drlastrow["ACTTotal"] = drRP["ACTTotal"] == DBNull.Value ? 0 : drRP["ACTTotal"];

                        }

                        if (drlastrow[drRP["ProjectName"].ToString() + "_Estimated" + "/" + drRP["ProjectId"].ToString()].ToString() != string.Empty)
                        {
                            drlastrow[drRP["ProjectName"].ToString() + "_Estimated" + "/" + drRP["ProjectId"].ToString()] = System.Convert.ToInt64(drlastrow[drRP["ProjectName"].ToString() + "_Estimated" + "/" + drRP["ProjectId"].ToString()]) + (drRP["Estimated"].ToString() != "" ? System.Convert.ToInt64(drRP["Estimated"]) : 0.0);
                            drlastrow[drRP["ProjectName"].ToString() + "_Actual"] = System.Convert.ToDouble(drlastrow[drRP["ProjectName"].ToString() + "_Actual"].ToString() == "" ? "0" : drlastrow[drRP["ProjectName"].ToString() + "_Actual"]) + (drRP["Actual"].ToString() != "" ? System.Convert.ToDouble(drRP["Actual"]) : 0.0);
                            drlastrow[drRP["ProjectName"].ToString() + "_BudgetHours"] = System.Convert.ToInt64(drlastrow[drRP["ProjectName"].ToString() + "_BudgetHours"]) + (drRP["BudgetHours"].ToString() != "" ? System.Convert.ToInt64(drRP["BudgetHours"]) : 0.0);
                        }
                        else
                        {
                            drlastrow[drRP["ProjectName"].ToString() + "_Estimated" + "/" + drRP["ProjectId"].ToString()] = drRP["Estimated"] == DBNull.Value ? 0 : System.Convert.ToInt64(drRP["Estimated"]);
                            drlastrow[drRP["ProjectName"].ToString() + "_Actual"] = drRP["Actual"] == DBNull.Value ? "" : drRP["Actual"];
                            drlastrow[drRP["ProjectName"].ToString() + "_BudgetHours"] = drRP["BudgetHours"] == DBNull.Value ? 0 : System.Convert.ToInt64(drRP["BudgetHours"]);
                            drlastrow[drRP["ProjectName"].ToString() + "_WeeklyComments"] = drRP["WeeklyComments"] == DBNull.Value ? "" : drRP["WeeklyComments"];
                            drlastrow[drRP["ProjectName"].ToString() + "_Status"] = drRP["Status"] == DBNull.Value ? "" : drRP["Status"];
                        }
                    }
                }
                drlastrow["Leave"] = dtGrid.Compute("sum(Leave)", string.Empty);
                dtGrid.Columns.Remove("RoleID");
                dtGrid.Columns.Remove("DefaultRole");

                gdResources.DataSource = dtGrid;
                gdResources.DataBind();

                TableCell tableCell = gdResources.HeaderRow.Cells[1];

                string headerText = @"<table width=100% cellspacing=0><tr><td colspan=2 Height=50px>" + tableCell.Text + "</td></tr>" +
                                        "<tr><td width=100%>&nbsp;</td><td width=100% ></td></tr></table>";
                tableCell.Text = headerText;

                tableCell = gdResources.HeaderRow.Cells[4];

                headerText = @"<table width=100% cellspacing=0><tr><td colspan=2 Height=50px>" + tableCell.Text + "</td></tr>" +
                                        "<tr><td width=100%>&nbsp;</td><td width=100% ></td></tr></table>";
                tableCell.Text = headerText;

                for (int rowCount = 5; rowCount < gdResources.HeaderRow.Cells.Count - 2; rowCount++)
                {
                    if (rowCount == gdResources.HeaderRow.Cells.Count - 4)
                    {
                        TableCell tcActual1 = gdResources.HeaderRow.Cells[rowCount];
                        TableCell tcEst1 = gdResources.HeaderRow.Cells[rowCount + 1];


                        // Developing and displaying header columns for expected and actual hours in the grid view...

                        string newHeaderText = "<table width=100% cellspacing=0><tr><td colspan=2 Height=50px>Total</td></tr>";
                        newHeaderText += "<tr><td width=100%>Exp.</td><td width=100% >Act.</td></tr></table>";
                        tcActual1.Text = newHeaderText;

                        tcActual1.ColumnSpan = 2;
                        if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 4=Employee)
                        {
                            TableCell tcSync1 = gdResources.HeaderRow.Cells[rowCount + 2];
                            tcSync1.Text = "Sync";
                            gdResources.HeaderRow.Cells.Remove(tcSync1);
                        }
                        gdResources.HeaderRow.Cells.Remove(tcEst1);

                        int headerRowCount = gdResources.HeaderRow.Cells.Count;
                    }
                    else if (rowCount == gdResources.HeaderRow.Cells.Count - 5)
                    {
                        TableCell tcLeave = gdResources.HeaderRow.Cells[rowCount];

                        // Developing and displaying header columns for expected and actual hours in the grid view...
                        string newHeaderText = @"<table width=100% cellspacing=0><tr><td colspan=2 Height=50px>Leave</td></tr>" +
                                        "<tr><td width=100%>&nbsp;</td><td width=100% ></td></tr></table>";
                        tcLeave.Text = newHeaderText;
                    }
                    else
                    {
                        TableCell tcActual = gdResources.HeaderRow.Cells[rowCount];
                        TableCell tcEst = gdResources.HeaderRow.Cells[rowCount + 1];
                        TableCell tcBud = gdResources.HeaderRow.Cells[rowCount + 2];
                        TableCell tcWe = gdResources.HeaderRow.Cells[rowCount + 3];
                        TableCell tcSt = gdResources.HeaderRow.Cells[rowCount + 4];


                        var array = tcActual.Text.Split('/');
                        string projectId = array[array.Length - 1].ToString();
                        string project = tcActual.Text.Remove(tcActual.Text.IndexOf('_'));

                        // Developing and displaying header columns for expected and actual hours in the grid view...

                        string newHeaderText = "<table  width=100% cellspacing=0><tr style=border-right-color: #333333><td colspan=3  Height=50px><a href=ProjectPlanningEntry.aspx?Id=" + projectId + " style=color:#fcff00;>" + project + "</a></td></tr>";
                        newHeaderText += "<tr style=border-right-color: #333333><td width=100%>Exp.</td><td width=100%>Act.</td><td style=display:none; width=100%>Bud.</td><td style=display:none; width=100%>We.</td><td style=display:none; width=100%>St.</td></tr></table>";
                        tcActual.Text = newHeaderText;


                        tcActual.ColumnSpan = 2;
                        gdResources.HeaderRow.Cells.Remove(tcEst);
                        gdResources.HeaderRow.Cells.Remove(tcBud);
                        gdResources.HeaderRow.Cells.Remove(tcWe);
                        gdResources.HeaderRow.Cells.Remove(tcSt);


                        int headerRowCount = gdResources.HeaderRow.Cells.Count;
                    }

                }
                int rowCount1 = gdResources.HeaderRow.Cells.Count - 1;
                TableCell tcSync = gdResources.HeaderRow.Cells[rowCount1];
                tcSync.Text = "Sync";
               // string newHeaderText1 = "<table width=100% cellspacing=0><tr><td colspan=2 Height=50px>Sync</td></tr>";
               //newHeaderText1 += "<tr><td width=100%></td><td width=100% ></td></tr></table>";
               // tcSync.Text = newHeaderText1;
                gdResources.HeaderRow.Cells.Add(tcSync);
                DataTableValues(); // Method for storing DataTableValues to session
                                   
            }
        }


        // Method for storing DataTableValues to session...
        private void DataTableValues()
        {
            DataTable dtdatas = new DataTable();
            dtdatas.Columns.Add("Year");
            dtdatas.Columns.Add("Month");
            dtdatas.Columns.Add("Period");
            dtdatas.Columns.Add("WeekIndex");
            dtdatas.Columns.Add("Week");

            DataRow dr = dtdatas.NewRow();
            dr["Year"] = ddlYear.SelectedItem.Text;
            dr["Month"] = ddlMonth.SelectedIndex;
            dr["WeekIndex"] = ddlWeek.SelectedIndex;
            dr["Week"] = ddlWeek.Text;
            dtdatas.Rows.Add(dr);
            Session["dtDatas"] = dtdatas;
            dtdatas.Dispose();
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

        // Method to Export Datatable to an excel file...
        public static void ExportToSpreadsheet(DataTable table, string name, string heading)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Write(heading);
            context.Response.Write(Environment.NewLine);
            context.Response.Write(Environment.NewLine);

            foreach (DataColumn column in table.Columns)
            {
                context.Response.Write(column.ColumnName + ",");
            }

            context.Response.Write(Environment.NewLine);

            foreach (DataRow row in table.Rows)
            {
                for (int tableColumnCount = 0; tableColumnCount < table.Columns.Count; tableColumnCount++)
                {
                    context.Response.Write(row[tableColumnCount].ToString().Replace(",", string.Empty) + ",");
                }
                context.Response.Write(Environment.NewLine);
            }

            context.Response.ContentType = "text/csv";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name + ".csv");
            context.Response.Charset = string.Empty;
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Expires = 1;
            context.Response.Buffer = true;
            context.Response.End();
        }

        #endregion

        #region"Control Events"  

        protected void gdResources_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.Cells.Count >= 1)
                e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            int cellCount = e.Row.Cells.Count;
            e.Row.Cells[cellCount - 2].Visible = false;
            if (e.Row.Cells[cellCount - 1].Text != string.Empty)
                e.Row.Cells[cellCount - 1].Text = string.Empty;
            if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 4=Employee)
            {
                e.Row.Cells[cellCount - 1].Visible = false;
            }
            e.Row.Cells[cellCount - 4].Font.Bold = true;
            e.Row.Cells[cellCount - 3].Font.Bold = true;
            e.Row.Cells[0].Width = 20;

            DataTable dt = gdResources.DataSource as DataTable;
            int count = dt.Columns.Count;

            if (count > 25)
            {
                e.Row.Cells[1].Width = 5000; //THis will increase the resource name to
            }
            else
            {
                e.Row.Cells[1].Width = 400;
            }

           
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Font.Bold = true;
                e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                e.Row.ForeColor = System.Drawing.Color.White;
                e.Row.Cells[0].Text = "";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string rowTotal = DataBinder.Eval(e.Row.DataItem, "Role") == DBNull.Value ? string.Empty : (String)DataBinder.Eval(e.Row.DataItem, "Role");

                int estimatedHoursColumn = 7;
                int actualHoursColumn = 8;
                int budjetHoursColumn = 9;

                e.Row.Cells[4].Width = 50;
               // e.Row.Cells[7].Width = 30;
                if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 4=Employee)
                {
                    ImageButton ImageButton1 = new ImageButton()
                    {
                        ID = "ImageButton1",
                        ImageUrl = "../Images/Sync.png",
                        AlternateText = "sync",
                        ToolTip = "Sync Hours",
                        Visible = true
                    };
                   


                   
                    ImageButton1.Click += btngetActualhours1;
                    DataRowView row = (DataRowView)e.Row.DataItem;
                    if (e.Row.RowIndex != row.DataView.Count - 1)
                    {
                        e.Row.Cells[cellCount - 1].Controls.Add(ImageButton1);
                        e.Row.Cells[cellCount - 1].Width = 30;



                        string Currentmonth = ddlMonth.SelectedItem.Value.ToString();
                        string from = ddlWeek.SelectedItem.Text.ToString();
                        string yr = ddlYear.SelectedItem.Text.ToString();
                        string timeperiod = string.Empty;
                        string mnth = string.Empty;
                        object result = string.Empty;
                        if (from != string.Empty)
                        {
                            string[] arrfrom = from.Split('-');
                            if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.Last) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.First))
                            {
                                int Newmonth = Convert.ToInt16(Currentmonth) + 1;
                                mnth = Newmonth.ToString();
                                timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + mnth + "/" + yr;
                                string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                                result = PMOscar.BaseDAL.ExecuteScalar(query);

                            }
                            else if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) == (int)(PMOscar.Utility.EnumTypes.Month.Last))
                            {
                                string monthVal = PMOscar.Utility.EnumTypes.Month.First.ToString();
                                int yri = Convert.ToInt16(yr) + 1;
                                string yearVal = yri.ToString();
                                timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + monthVal + "/" + yearVal;
                                string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                                result = PMOscar.BaseDAL.ExecuteScalar(query);
                            }

                            else if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.Last) && Convert.ToInt16(Currentmonth) == (int)(PMOscar.Utility.EnumTypes.Month.First))
                            {
                                int Newmonth = (int)(PMOscar.Utility.EnumTypes.Month.Last);
                                int yri = Convert.ToInt16(yr) - 1;
                                mnth = Newmonth.ToString();
                                string yearVal = yri.ToString();
                                timeperiod = arrfrom[0] + "/" + mnth + "/" + yearVal + "-" + arrfrom[1] + "/" + Currentmonth + "/" + yr;
                                string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                                result = PMOscar.BaseDAL.ExecuteScalar(query);
                            }

                            else
                            {
                                timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + Currentmonth + "/" + yr;
                                string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                                result = PMOscar.BaseDAL.ExecuteScalar(query);
                            }

                            if (result != null)
                            {
                                if (result.ToString().Equals("F"))
                                {
                                    btnAct_hour.Enabled = false;
                                    ImageButton1.Enabled = false;
                                    ImageButton1.CssClass= "disabledImageButton";
                                    ImageButton1.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
                                    
                                }
                                else
                                {
                                    btnAct_hour.Enabled = true;
                                    ImageButton1.Enabled = true;

                                }
                            }
                            else
                            {
                                btnAct_hour.Enabled = true;
                                ImageButton1.Enabled = true;

                            }
                        }


                    }
                    
                }

                for (int cellCountIncrement = 5; cellCountIncrement < cellCount - 1; cellCountIncrement++)
                {
                    e.Row.Cells[cellCountIncrement].Width = 50;
                    e.Row.Cells[cellCountIncrement].Attributes.Add("Style", "text-align:right;padding-right:5px;");

                    HyperLink actualHoursPopUpLink = new HyperLink();

                    if (e.Row.Cells[cellCountIncrement].Text == "N" && rowTotal != "Total")
                    {
                        if (e.Row.Cells[cellCountIncrement - 1].Text != "&nbsp;")
                        {
                            actualHoursPopUpLink.Text = e.Row.Cells[cellCountIncrement - 3].Text;
                            e.Row.Cells[cellCountIncrement - 3].Controls.Add(actualHoursPopUpLink);
                            actualHoursPopUpLink.Attributes.Add("Style", "text-align:right;text-decoration:underline;");
                            if (e.Row.Cells[cellCountIncrement - 1].Text.ToString() != string.Empty)
                            {
                               string replaced=  e.Row.Cells[cellCountIncrement - 1].Text.ToString().Replace("&amp;#x0D;\n"," ");
                                actualHoursPopUpLink.Attributes.Add("onclick", "showPopup('" + replaced.Replace("\r\n", "<br/>").Replace("\n","<br/>").Replace("'", "\"\"").Trim() + "', event);return false;");
                            }
                            }
                    }

                    if (e.Row.Cells[cellCountIncrement].Text == "R" && rowTotal != "Total")
                    {
                        if (e.Row.Cells[cellCountIncrement - 1].Text != "&nbsp;")
                        {
                            actualHoursPopUpLink.Text = e.Row.Cells[cellCountIncrement - 3].Text;
                            e.Row.Cells[cellCountIncrement - 3].Controls.Add(actualHoursPopUpLink);
                            actualHoursPopUpLink.Attributes.Add("Style", "Color:red;text-align:right;text-decoration:underline;");
                            if (e.Row.Cells[cellCountIncrement - 1].Text.ToString() != string.Empty)
                                actualHoursPopUpLink.Attributes.Add("onclick", "showPopup('" + e.Row.Cells[cellCountIncrement - 1].Text.Replace("\r\n", "<br/>").Trim() + "', event);return false;");
                        }

                        if (e.Row.Cells[cellCountIncrement - 3].Text == "&nbsp;")
                            e.Row.Cells[cellCountIncrement - 4].ForeColor = System.Drawing.Color.Red;
                        else
                            e.Row.Cells[cellCountIncrement - 3].ForeColor = System.Drawing.Color.Red;
                    }


                    if (cellCountIncrement % 5 == 1 && cellCountIncrement < cellCount - 1 && rowTotal != "Total")
                        e.Row.Cells[cellCountIncrement].Attributes.Add("Style", "border-right: 2px solid #539cd1;text-align:right;padding-right:5px;");

                    if (estimatedHoursColumn < cellCount - 4)
                    {
                        e.Row.Cells[estimatedHoursColumn].Visible = false;
                        estimatedHoursColumn = estimatedHoursColumn + 5;
                    }

                    if (actualHoursColumn < cellCount - 4)
                    {
                        e.Row.Cells[actualHoursColumn].Visible = false;
                        actualHoursColumn = actualHoursColumn + 5;
                    }

                    if (budjetHoursColumn < cellCount - 4)
                    {
                        e.Row.Cells[budjetHoursColumn].Visible = false;
                        budjetHoursColumn = budjetHoursColumn + 5;
                    }
                }

                if (prevResourceName == string.Empty)
                {
                    e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
                    indexValue = e.Row.Cells[0].Text;
                }

                else
                {
                    if (prevResourceName == e.Row.Cells[3].Text)
                    {
                        e.Row.Cells[1].Text = string.Empty;
                        e.Row.Cells[0].Text = string.Empty;
                    }
                    else
                    {
                        e.Row.Cells[0].Text = (Convert.ToInt16(indexValue) + 1).ToString();
                        indexValue = e.Row.Cells[0].Text;
                    }
                }

                prevResourceName = e.Row.Cells[3].Text;

                if (rowTotal == "Total")
                    e.Row.Cells[0].Text = string.Empty;

                // To develop the alternate colors in the grid view

                if (colorResourceName == string.Empty)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#eff3fb");
                    white = 0;  // White color (#ffffff) flag set to zero to show other color (#eff3fb) for the first row of the grid 
                    alter = 1;  // Other color (#eff3fb) flag set to 1 to showOther color (#eff3fb) for the first row of the grid 
                }

                else
                {
                    if (colorResourceName == e.Row.Cells[3].Text)
                        alter = 1;

                    if (colorResourceName != e.Row.Cells[3].Text)
                        alter = 2;

                    if (alter == 1)
                    {
                        if (white == 1)
                            e.Row.BackColor = System.Drawing.Color.FromName("#ffffff");
                        else
                            e.Row.BackColor = System.Drawing.Color.FromName("#eff3fb");
                    }

                    if (alter == 2)
                    {
                        if (white == 1)
                        {
                            e.Row.BackColor = System.Drawing.Color.FromName("#eff3fb");
                            white = 0;
                        }
                        else
                        {
                            e.Row.BackColor = System.Drawing.Color.FromName("#ffffff");
                            white = 1;
                        }
                    }

                }

                colorResourceName = e.Row.Cells[3].Text;

                if (rowTotal == "Total")
                {
                    e.Row.Font.Bold = true;
                    e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[0].Text = string.Empty;
                }

                // To develop the alternate colors in the grid view

            }
        }
        /// <summary>
        /// functiion for syncing actual hours from time tracker and resource planning.
        /// </summary>
        
        protected void btngetActualhours(object sender, EventArgs e)

        {
            var userid = Session["UserId"];
            List<SqlParameter> parameter = new List<SqlParameter>();
            SetPeriod();
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@FromDate", dayFrom));
            parameter.Add(new SqlParameter("@ToDate", dayTo));
            parameter.Add(new SqlParameter("@userId", userid));
            int value = BaseDAL.ExecuteSPNonQuery("SyncActualHoursInTimeTracker", parameter);
            BindGridView();
            if (value > 0)
            {

                Page.RegisterStartupScript("myScript", "<script language='JavaScript'>alert('Actual hours synced successfully.');</script>");
                syncSuccess.Text = "Synced successfully.";
                syncSuccess.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                syncSuccess.Text = "Sync failed.";
                syncSuccess.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// function for updating actual hours in mysqltimetracker.
        /// </summary>

        protected void btngetActualhours1(object sender, EventArgs e)
        {
            
            ImageButton btn = (ImageButton)sender;
           
         
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            rowIndex = Convert.ToInt16(gvr.RowIndex);
            var name = gdResources.Rows[rowIndex].Cells[2].Text;
           
            BindGridView();
            
            int value = BaseDAL.ExecuteSP("ImportMySQLTimeTracker");
            string ResourceId = gdResources.Rows[rowIndex].Cells[3].Text;
            if (ResourceId != null)
            {
                SetPeriod();
                string queryToRetrieveEmployeeCode = string.Format("select emp_Code from Resource where ResourceId='{0}'", Convert.ToInt16(ResourceId));
                Session["ResourceID"] = ResourceId;
                object result1 = PMOscar.BaseDAL.ExecuteScalar(queryToRetrieveEmployeeCode);
                string queryToExecute = string.Format("select ProjectName as [Project Name],FirstName as [Project Manager],case when Duration='00:00:00.0000000' then '24.0000000'  else cast(SUM(DATEDIFF(mi,CONVERT(time,'00:00:00:00'),CONVERT(time,Duration))/60.0)as decimal(10,2)) end as  [Actual Hours] from TimeTracker_MySQL t,Project p,[user] u where p.ProjectId=t.PMOscarProjectID  and   Date>='{0}'and Date<='{1}' and t.EmployeeCode='{2}'and u.UserId=p.ProjectManager group by ProjectName,FirstName,Duration", dayFrom, dayTo, result1);

                DataTable import = BaseDAL.ExecuteDataTable(queryToExecute);
                lblResourceName.Text = "Resource Name:" + name;
                lblDate.Text = "Week:"+lblWeekStatus.Text;
           
                if (import.Rows.Count>0)
                  {
                    btnOk.Visible = true;
                    lblSyn.Visible = false;
                    lblmsg.Text = Constants.SYNC;
                    lblmsg.Visible = true;
                    lbl1.Attributes.Add("style", "padding-top: 3px;");
                    lbl2.Attributes.Add("style", "padding-top: 3px;");

                }
                else
                {
                    btnOk.Visible = false;
                    lblmsg.Visible=false;
                    lblSyn.Visible = true;
                    lblSyn.Text = Constants.NOSYNC;
                    lbl1.Attributes.Add("style", "padding-top: 15px;");
                    lbl2.Attributes.Add("style", "padding-top: 15px;");
                }

                GridViewbind.DataSource = import;
                GridViewbind.DataBind();


                Panel1.Style.Add("display", "block");

               this.ModalPopupExtender2.Show();
         

            }
        }
        /// <summary>
        /// function for syncing actual hours.
        /// </summary> 
        protected void btnOK_Click(object sender, EventArgs e)
        {
           
                string resourceID = (string)Session["ResourceID"];
                int resourceId = Convert.ToInt16(resourceID);
                if (resourceId != 0)
                {


                    List<SqlParameter> parameter = new List<SqlParameter>();
                    SetPeriod();
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@ResourceID", resourceId));
                    parameter.Add(new SqlParameter("@FromDate", dayFrom));
                    parameter.Add(new SqlParameter("@ToDate", dayTo));
                    parameter.Add(new SqlParameter("@userId", userid));
                    int val = BaseDAL.ExecuteSPNonQuery("SyncActualHoursInTimeTracker", parameter);
                    MessageBox.Show("Syncing Completed...!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    BindGridView();
                
            }

        }
      
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlYear.SelectedItem.Value) != DateTime.Now.Year)
                ddlMonth.SelectedIndex = 0;
            else
                ddlMonth.SelectedIndex = DateTime.Now.Date.Month - 1;

            GetWeek();    // Method to populate the weeks in selected Month and Year...
            BindGridView();  // Binding and displaying the resource details in the gridView...
            lblWeekStatus.Text = ddlWeek.SelectedItem.Text.ToString() + ',' + ' ' + ddlMonth.SelectedItem.Text.ToString() + ',' + ' ' + ddlYear.SelectedItem.Text.ToString();
            string Currentmonth = ddlMonth.SelectedItem.Value.ToString();
            string from = ddlWeek.SelectedItem.Text.ToString();
            string yr = ddlYear.SelectedItem.Text.ToString();
            string timeperiod = string.Empty;
            string mnth = string.Empty;
            object result = string.Empty;
            if (from != string.Empty)
            {
                string[] arrfrom = from.Split('-');
                if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.Last) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.First))
                {
                    int Newmonth = Convert.ToInt16(Currentmonth) + 1;
                    mnth = Newmonth.ToString();
                    timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + mnth + "/" + yr;
                    string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                    result = PMOscar.BaseDAL.ExecuteScalar(query);

                }
                else if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) == (int)(PMOscar.Utility.EnumTypes.Month.Last))
                {
                    string month = PMOscar.Utility.EnumTypes.Month.First.ToString();
                    int yri = Convert.ToInt16(yr) + 1;
                    string year = yri.ToString();
                    timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + month + "/" + year;
                    string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                    result = PMOscar.BaseDAL.ExecuteScalar(query);
                }

                else if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.Last) && Convert.ToInt16(Currentmonth) == (int)(PMOscar.Utility.EnumTypes.Month.First))
                {
                    int Newmonth = (int)(PMOscar.Utility.EnumTypes.Month.Last);
                    int yri = Convert.ToInt16(yr) - 1;
                    mnth = Newmonth.ToString();
                    string year = yri.ToString();
                    timeperiod = arrfrom[0] + "/" + mnth + "/" + year + "-" + arrfrom[1] + "/" + Currentmonth + "/" + yr;
                    string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                    result = PMOscar.BaseDAL.ExecuteScalar(query);
                }

                else
                {
                    timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + Currentmonth + "/" + yr;
                    string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                    result = PMOscar.BaseDAL.ExecuteScalar(query);
                }

                if (result != null)
                {
                    if (result.ToString().Equals("F"))
                    {
                        btnAct_hour.Enabled = false;
                       // ImageButton1.enabled = false;


                    }
                    else
                    {
                        btnAct_hour.Enabled = true;
                       
                    }
                }
                else
                {
                    btnAct_hour.Enabled = true;
                   
                }
            }
        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetWeek();    // Method to populate the weeks in selected Month and Year...
            BindGridView();  // Binding and displaying the resource details in the gridView...
            lblWeekStatus.Text = ddlWeek.SelectedItem.Text.ToString() + ',' + ' ' + ddlMonth.SelectedItem.Text.ToString() + ',' + ' ' + ddlYear.SelectedItem.Text.ToString();
            string Currentmonth = ddlMonth.SelectedItem.Value.ToString();
            string from = ddlWeek.SelectedItem.Text.ToString();
            string yr = ddlYear.SelectedItem.Text.ToString();
            string timeperiod = string.Empty;
            string mnth = string.Empty;
            object result = string.Empty;
            if (from != string.Empty)
            {
                string[] arrfrom = from.Split('-');
                if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.Last) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.First))
                {
                    int Newmonth = Convert.ToInt16(Currentmonth) + 1;
                    mnth = Newmonth.ToString();
                    timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + mnth + "/" + yr;
                    string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                    result = PMOscar.BaseDAL.ExecuteScalar(query);

                }
                else if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) == (int)(PMOscar.Utility.EnumTypes.Month.Last))
                {
                    string month = PMOscar.Utility.EnumTypes.Month.First.ToString();
                    int yri = Convert.ToInt16(yr) + 1;
                    string year = yri.ToString();
                    timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + month + "/" + year;
                    string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                    result = PMOscar.BaseDAL.ExecuteScalar(query);
                }
                else if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.Last) && Convert.ToInt16(Currentmonth) == (int)(PMOscar.Utility.EnumTypes.Month.First))
                {
                    int Newmonth = (int)(PMOscar.Utility.EnumTypes.Month.Last);
                    int yri = Convert.ToInt16(yr) - 1;
                    mnth = Newmonth.ToString();
                    string year = yri.ToString();
                    timeperiod = arrfrom[0] + "/" + mnth + "/" + year + "-" + arrfrom[1] + "/" + Currentmonth + "/" + yr;
                    string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                    result = PMOscar.BaseDAL.ExecuteScalar(query);
                }
                else
                {
                    timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + Currentmonth + "/" + yr;
                    string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                    result = PMOscar.BaseDAL.ExecuteScalar(query);
                }

                if (result != null)
                {
                    if (result.ToString().Equals("F"))
                    {
                        btnAct_hour.Enabled = false;
                       
                    }
                    else
                    {
                        btnAct_hour.Enabled = true;
                        
                    }
                }
                else
                {
                    btnAct_hour.Enabled = true;
                   
                }
            }
        }
        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();  // Binding and displaying the resource details in the gridView...
            lblWeekStatus.Text = ddlWeek.SelectedItem.Text.ToString() + ',' + ' ' + ddlMonth.SelectedItem.Text.ToString() + ',' + ' ' + ddlYear.SelectedItem.Text.ToString();
            string Currentmonth = ddlMonth.SelectedItem.Value.ToString();
            string from = ddlWeek.SelectedItem.Text.ToString();
            string yr = ddlYear.SelectedItem.Text.ToString();
            string timeperiod = string.Empty;
            string mnth = string.Empty;
            object result = string.Empty;
            if (from != string.Empty)
            {
                string[] arrfrom = from.Split('-');
                if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.Last) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.First))
                {
                    int Newmonth = Convert.ToInt16(Currentmonth) + 1;
                    mnth = Newmonth.ToString();
                    timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + mnth + "/" + yr;
                    string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                    result = PMOscar.BaseDAL.ExecuteScalar(query);

                }
                else if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) == (int)(PMOscar.Utility.EnumTypes.Month.Last))
                {
                    string month = PMOscar.Utility.EnumTypes.Month.First.ToString();
                    int yri = Convert.ToInt16(yr) + 1;
                    string year = yri.ToString();
                    timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + month + "/" + year;
                    string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                    result = PMOscar.BaseDAL.ExecuteScalar(query);
                }
                else if (Convert.ToInt16(arrfrom[0]) > Convert.ToInt16(arrfrom[1]) && Convert.ToInt16(Currentmonth) != (int)(PMOscar.Utility.EnumTypes.Month.Last) && Convert.ToInt16(Currentmonth) == (int)(PMOscar.Utility.EnumTypes.Month.First))
                {
                    int Newmonth = (int)(PMOscar.Utility.EnumTypes.Month.Last);
                    int yri = Convert.ToInt16(yr) - 1;
                    mnth = Newmonth.ToString();
                    string year = yri.ToString();
                    timeperiod = arrfrom[0] + "/" + mnth + "/" + year + "-" + arrfrom[1] + "/" + Currentmonth + "/" + yr;
                    string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                    result = PMOscar.BaseDAL.ExecuteScalar(query);
                }
                else
                {
                    timeperiod = arrfrom[0] + "/" + Currentmonth + "/" + yr + "-" + arrfrom[1] + "/" + Currentmonth + "/" + yr;
                    string query = string.Format("select [Status] from [dbo].[Dashboard] where Name='{0}'", timeperiod);
                    result = PMOscar.BaseDAL.ExecuteScalar(query);
                }

                if (result != null)
                {
                    if (result.ToString().Equals("F"))
                    {
                        btnAct_hour.Enabled = false;
                       
                    }
                    else
                    {
                        btnAct_hour.Enabled = true;
                       
                    }
                }
                else
                {
                    btnAct_hour.Enabled = true;
                   
                }
            }
        }


        protected void gdResources_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Height = 30;
        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            SetPeriod(); // Method for getting dates and months...
            string uptodate = fromYear + "-" + fromMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-")));

            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", uptodate));
            parameter.Add(new SqlParameter("@status", '1'));
            DataTable dtExportProjectPlanningDashboard = PMOscar.BaseDAL.ExecuteSPDataSet("[ExportProjectPlanningDashboard]", parameter).Tables[0];

            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", uptodate));
            parameter.Add(new SqlParameter("@status", '2'));
            DataTable dtProjectRolesPlanningDashboard = PMOscar.BaseDAL.ExecuteSPDataSet("[ExportProjectPlanningDashboard]", parameter).Tables[0];

            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@dayfrom", dayFrom));
            parameter.Add(new SqlParameter("@dayto", dayTo));
            parameter.Add(new SqlParameter("@uptodate", uptodate));
            parameter.Add(new SqlParameter("@status", '3'));
            DataTable dtRolesPlanningDashboard = PMOscar.BaseDAL.ExecuteSPDataSet("[ExportProjectPlanningDashboard]", parameter).Tables[0];

            foreach (DataRow drProj in dtRolesPlanningDashboard.Rows)
            {
                if (drProj["Role"].ToString() != string.Empty)
                    dtExportProjectPlanningDashboard.Columns.Add(drProj["Role"].ToString(), typeof(string));

            }
            dtExportProjectPlanningDashboard.Columns.Add("Total", typeof(string));

            foreach (DataRow dr in dtExportProjectPlanningDashboard.Rows)
            {
                foreach (DataRow drrole in dtRolesPlanningDashboard.Rows)
                {
                    DataRow[] drRoleProjects = dtProjectRolesPlanningDashboard.Select(" Role='" + drrole["Role"].ToString() + "' and  ProjectName ='" + dr["ProjectName"].ToString() + "'");

                    foreach (DataRow drRP in drRoleProjects)
                    {
                        dr[drRP["Role"].ToString()] = drRP["SpentHours"];
                        dr["Total"] = System.Convert.ToInt64((dr["Total"].ToString() == "" ? 0 : dr["Total"])) + System.Convert.ToInt64(drRP["SpentHours"].ToString() == "" ? 0 : drRP["SpentHours"]);
                    }
                }
            }

            dtExportProjectPlanningDashboard.Columns["Allocated"].ColumnName = "Allocated[Last Week]";
            dtExportProjectPlanningDashboard.Columns["Actual"].ColumnName = "Actual[Last Week]";
            dtExportProjectPlanningDashboard.Columns["BudgetHours"].ColumnName = "Budget";
            dtExportProjectPlanningDashboard.Columns["RevisedBudgetHours"].ColumnName = "Revised Budget";
            dtExportProjectPlanningDashboard.Columns["SpentHours"].ColumnName = "Spent Hours";
            dtExportProjectPlanningDashboard.Columns["ProjectName"].ColumnName = "Name";

            ExportToSpreadsheet(dtExportProjectPlanningDashboard, "ProjectDashboard", lblWeekStatus.Text); // Method to Export Datatable to an excel file...
        }
       

        #endregion

    }
}
