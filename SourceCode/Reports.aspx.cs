
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
//  Description  : Displays various reports
//  Author       : Abid
//  Created Date : 26 April 2012
//  Modified By  : Riya Francis
//  Modified Date: 18 March 2013  
// </summary>
// -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Services;
using PMOscar.Core;
using PMOscar.Model;
using PMOscar.DAL;
namespace PMOscar
{
    public partial class Reports : System.Web.UI.Page
    {

        # region"Declarations"

        public int white;
        public int alter;
        private string colorResourceName = string.Empty;
        private string commandName_Refresh = "Refresh";
        private string commandName_Finalize = "Finalize";
        #endregion

        #region"Page Events"

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
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
                else if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.ProjectOwner) // Checking role of user by UserRoleID ( 1 = ProjectOwner , 2 = ProjectManager )
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

                //disable export button if no report generated
                if (lblReportTitle.Text.Equals(""))
                {
                    // btnExport.Enabled = false;
                }

            }

            if (!IsPostBack)
            {

                if ((Request.QueryString["redirect"] != null))
                {
                    ///////////////////////

                    //rdbMonthly.

                    DataTable dtDashBoard = new DataTable();
                    dtDashBoard = PMOscar.BaseDAL.ExecuteDataTable("Select DashboardID,Name,FromDate,ToDate from dbo.Dashboard where [PeriodType]='W' order by FromDate DESC");
                    if (dtDashBoard.Rows.Count > 0)
                    {

                        hiddenendWeek.Value = dtDashBoard.Rows[0]["Name"].ToString();


                    }
                    ///////////////////

                    FillYears(); // To fill the years in the DropDownList
                    string year = Request.QueryString["year"] != null ? Request.QueryString["year"].ToString() : null;
                    string month = Request.QueryString["month"] != null ? Request.QueryString["month"].ToString() : null;
                    ddlYear.SelectedValue = year;
                    ddlMonth.SelectedIndex = Convert.ToInt16(month);
                    BindGridViewForUtilizationReport("ProjectName", "ASC");  // To fill the resouce planning list in the grid (projectwise)                 

                   
                }
                else
                {

                    ///////////////////////

                    DataTable dtDashBoard = new DataTable();
                    dtDashBoard = PMOscar.BaseDAL.ExecuteDataTable("Select DashboardID,Name,FromDate,ToDate from dbo.Dashboard where [PeriodType]='W' order by FromDate DESC");
                    if (dtDashBoard.Rows.Count > 0)
                    {
                        //initial value of hiddenEndWeek
                        hiddenendWeek.Value = dtDashBoard.Rows[0]["Name"].ToString();

                    }
                    ///////////////////


                    FillYears();

                    //keeping reports in session (needs to finalize
                    if (Session["dtDatasReports"] != null)
                    {
                        DataTable dt = (DataTable)Session["dtDatasReports"];
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0].ItemArray[0].ToString() == "N")
                            {
                                string year = dt.Rows[0].ItemArray[1].ToString();
                                ddlYear.SelectedValue = year;
                                string month = dt.Rows[0].ItemArray[2].ToString();
                                ddlMonth.SelectedValue = month;

                            }
                            else
                            {
                                string year = DateTime.Now.Year.ToString();
                                ddlYear.SelectedValue = year;
                                string month = DateTime.Now.Month.ToString();
                                ddlMonth.SelectedValue = month;
                            }
                        }

                        dt.Dispose();

                    }
                }

                if (Request.QueryString["reportId"] !=null && Request.QueryString["reportId"].ToString() == "6")
                {
                    DropDownListReports.SelectedValue = "6";
                    btnExport.Visible = false;
                    tdOtherReports.Visible = false;
                    tdCompanyUtilizationReport.Visible = false;
                    tdopenHoursReport.Visible = true;
                }
            }

            ddlYear.Focus();

        }

        #endregion

        #region"Methods"


        // Method for Populating Year Combobox...
        /// <summary>
        /// Fills the years.
        /// </summary>
        private void FillYears()
        {
            try
            {

                for (int yearCount = DateTime.Now.Year - 4; yearCount <= DateTime.Now.Year + 1; yearCount++)
                {
                    ListItem lstYears = new ListItem();
                    lstYears.Value = yearCount.ToString();
                    lstYears.Text = yearCount.ToString();
                    ddlYear.Items.Add(lstYears);
                    ddlYear.DataBind();
                }

                //select current year
                int currentYear = DateTime.Now.Year;
                ddlYear.Items.FindByValue(currentYear.ToString()).Selected = true;

                //select current month in month dropdown
                ddlMonth.SelectedIndex = DateTime.Now.Month - 1;

                //Fill report drop down
                FillReportList();

                //DataTable dtProject = PMOscar.BaseDAL.ExecuteDataTable("SELECT ProjectId,ProjectName FROM Project ORDER BY ProjectName");
                //ddlProject.DataSource = dtProject;
                //ddlProject.DataTextField = "ProjectName";
                //ddlProject.DataValueField = "ProjectId";
                //ddlProject.DataBind();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }


        /// <summary>
        /// Fiils the report list.
        /// </summary>
        private void FillReportList()
        {
            try
            {

                //clear existing dropdown values
                DropDownListReports.Items.Clear();
                //bind by removing "_" in between
                foreach (int reportValue in Enum.GetValues(typeof(PMOscar.Utility.EnumTypes.Reports)))
                {
                    ListItem reportsItem = new ListItem(Enum.GetName(typeof(PMOscar.Utility.EnumTypes.Reports), reportValue).Replace("_", " "), reportValue.ToString());
                    DropDownListReports.Items.Add(reportsItem);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

        }


        // Binding and displaying the resource details in the gridView...
        /// <summary>
        /// Binds the grid view for project utilization report.
        /// </summary>
        private void BindGridViewForUtilizationReport(string sortColumn, string sortDirection)
        {
            try
            {

                List<SqlParameter> parameter = new List<SqlParameter>();
                DataSet dsTimeTrackerDetails;
                if (rdbMonthly.Checked.Equals(true))  //MonthWise Report
                {
                    parameter.Add(new SqlParameter("@Year", ddlYear.SelectedValue));
                    parameter.Add(new SqlParameter("@Month", ddlMonth.SelectedIndex + 1));
                    dsTimeTrackerDetails = PMOscar.BaseDAL.ExecuteSPDataSet("[GetProjectUtilization]", parameter);

                }
                else //Period Wise Report
                {
                    //get from and to dates
                    string fromDate = hiddenstartWeek.Value;
                    string endDate = hiddenendWeek.Value;

                    fromDate = ExtractDateAndFormat(fromDate); //format date as "year/month/day"
                    endDate = ExtractDateAndFormat(endDate);  //format date as "year/month/day"

                    parameter.Add(new SqlParameter("@FromDate", fromDate));
                    parameter.Add(new SqlParameter("@ToDate", endDate));
                    dsTimeTrackerDetails = PMOscar.BaseDAL.ExecuteSPDataSet("[GetProjectUtilizationByPeriod]", parameter);

                }


                if (dsTimeTrackerDetails.Tables.Count > 0) //initial validation
                {

                    DataTable dt = dsTimeTrackerDetails.Tables[0];
                    DataTable dtProjs = dt.DefaultView.ToTable(true, PMOscar.Core.Constants.FieldName.Project.PROJECTNAME);
                    var distinct = dtProjs.AsEnumerable().Distinct(DataRowComparer.Default); //get distinct rows
                    List<string> projectNames = new List<string>();

                    foreach (DataRow drProj in distinct)
                    {
                        projectNames.Add(drProj[PMOscar.Core.Constants.FieldName.Project.PROJECTNAME].ToString());   //get distinct project names

                    }

                    //sorting
                    if (sortColumn.Equals("ProjectName") && sortDirection.Equals("DESC"))
                    {
                        projectNames.Reverse();
                    }

                    DataTable dtProjectIds = dt.DefaultView.ToTable(true, PMOscar.Core.Constants.FieldName.Project.PROJECTID);
                    var distinctProjids = dtProjectIds.AsEnumerable().Distinct(DataRowComparer.Default); //get distinct rows
                    List<string> projectIds = new List<string>();

                    foreach (DataRow drProj in distinctProjids)
                    {
                        projectIds.Add(drProj[PMOscar.Core.Constants.FieldName.Project.PROJECTID].ToString());   //get distinct project names

                    }

                    //get project estimation details for these projects
                    List<SqlParameter> parameterItems = new List<SqlParameter>();
                    string idString = String.Join(",", projectIds.ToArray());

                    //get estimated values for these projects
                    bool estimationFlag = true;
                    DataSet dsProjectEstimationDetails = new DataSet();
                    DataTable dtEstimated = new DataTable();
                    if (!idString.Equals(""))
                    {
                        parameterItems.Add(new SqlParameter("@ProjectIDList", idString));
                        dsProjectEstimationDetails = PMOscar.BaseDAL.ExecuteSPDataSet("[GetEstimationForProjects]", parameterItems);

                        //get table of this result
                        dtEstimated = dsProjectEstimationDetails.Tables[0];
                    }
                    else
                    {
                        estimationFlag = false;
                    }

                    //get distinct phases
                    DataTable dtPhase = dt.DefaultView.ToTable(true, "Phase");

                    //get distinct rows
                    var distinctPhase = dtPhase.AsEnumerable().Distinct(DataRowComparer.Default);
                    List<string> phases = new List<string>();

                    foreach (DataRow drPhase in distinctPhase)
                    {
                        //get distinct project names
                        phases.Add(drPhase["Phase"].ToString());
                    }

                    //get distinct roles
                    DataTable dtRole = dt.DefaultView.ToTable(true, PMOscar.Core.Constants.FieldName.User.ROLE);
                    var distinctRole = dtRole.AsEnumerable().Distinct(DataRowComparer.Default); //get distinct rows
                    List<string> roles = new List<string>();

                    foreach (DataRow drRole in distinctRole)
                    {
                        //get distinct roles
                        roles.Add(drRole[PMOscar.Core.Constants.FieldName.User.ROLE].ToString());
                    }

                    //get distinct reources
                    DataTable dtResources = dt.DefaultView.ToTable(true, "ResourceName");
                    var distinctResource = dtResources.AsEnumerable().Distinct(DataRowComparer.Default); //get distinct rows
                    List<string> resources = new List<string>();
                    foreach (DataRow drResources in distinctResource)
                    {
                        //get distinct resource names
                        resources.Add(drResources["ResourceName"].ToString());
                    }

                    DataRow[] drProjects = dt.Select("", "ProjectName ASC");
                    DataTable tableFinal = dtProjs.Clone(); //get the structure

                    //add count column
                    DataColumn workCol2 = tableFinal.Columns.Add("count", typeof(Int32));
                    workCol2.AllowDBNull = true;
                    workCol2.Unique = false;
                    workCol2.SetOrdinal(0);

                    int count = 0;
                    double grandTotal = 0.0;

                    foreach (String project in projectNames)
                    {


                        count = count + 1;
                        IEnumerable<DataRow> queryProjectName = from myRow in dt.AsEnumerable()
                                                                where myRow.Field<string>("ProjectName").Equals(project)
                                                                select myRow;
                        DataTable tableInitial = queryProjectName.CopyToDataTable();  //get table for each projectnames for initial structure

                        //add a column (number)
                        DataColumn workCol = tableInitial.Columns.Add("count", typeof(Int32));
                        workCol.AllowDBNull = true;
                        workCol.Unique = false;
                        workCol.SetOrdinal(0);

                        //calculate sum of actual hours for each project 
                        var sum = tableInitial.AsEnumerable().Sum(x => x.Field<decimal?>("ActualHours"));   //toal actual hours

                        //add this as a row
                        DataRow drTotal = tableInitial.NewRow();
                        drTotal[0] = 1;
                        drTotal[5] = "Total"; //inserted at the revised budget position
                        drTotal[9] = sum;  //last column

                        drTotal[8] = project + "#";
                        drTotal[7] = project + "#";
                        drTotal[6] = project + "#";

                        tableInitial.Rows.Add(drTotal);

                        //calculate grand total               
                        grandTotal = grandTotal + Convert.ToDouble(sum);  //grand total 

                        //Adding numbers in the first row (index number)
                        int r = 0;
                        foreach (DataRow myRow in tableInitial.Rows)
                        {

                            if (tableInitial.Rows[r].ItemArray.GetValue(0).ToString() == "")
                            {
                                myRow[0] = count;
                            }
                            r++;
                        }

                        //merge each table to final table structure  
                        tableFinal.Merge(tableInitial); //merging 

                    }//for loop for each project name

                    //add grand total row to the final table
                    if (tableFinal.Rows.Count != 0)
                    {
                        //add grand total to bottom
                        DataRow drGrandTotal = tableFinal.NewRow();
                        drGrandTotal[1] = "grand";//dummy for merging separation
                        drGrandTotal[2] = "grand";
                        drGrandTotal[4] = "grand";
                        drGrandTotal[5] = "Grand Total"; //inserted at the revised budget position
                        drGrandTotal[9] = grandTotal;  //last column
                        tableFinal.Rows.Add(drGrandTotal);
                    }

                    //merge week rows to single row
                    foreach (string projectName in projectNames)
                    {
                        foreach (string phase in phases)
                        {
                            foreach (string role in roles)
                            {
                                foreach (string resource in resources)
                                {
                                    //calculate billable,budgeted and rev.budgeted
                                    //query to data table of estimated
                                    double billable = 0.00;
                                    double budgeted = 0.00;
                                    double revbudgeted = 0.00;

                                    //data view from table
                                    DataView dv = new DataView(tableFinal);
                                    dv.RowFilter = "ProjectName = '" + projectName + "' and Phase = '" + phase
                                                   + "' and Role = '" + role + "' and ResourceName = '" + resource + "'";

                                    List<string> phasesWorked = new List<string>();

                                    foreach (DataRowView drv in dv)
                                    {
                                        //get phases
                                        if (!(drv["Phase"] is DBNull))
                                        {
                                            phasesWorked.Add(Convert.ToString(drv["Phase"])); //add this to list
                                        }
                                    }

                                    //remove duplicates
                                    phasesWorked = RemoveDuplicatesFromList(phasesWorked);

                                    //generate string from list (separated by comma)
                                    //string phasesJoined = string.Join(",", phasesWorked.ToArray());

                                    if (dv.Count > 0)
                                    {
                                        //calculte billable, budget and rev.budget from estimation dataset
                                        if (estimationFlag)
                                        {
                                            DataView dvEstimatedTable = new DataView(dtEstimated);

                                            foreach (string phas in phasesWorked)
                                            {
                                                dvEstimatedTable.RowFilter = "ProjectName = '" + projectName + "' and Phase = '" + phas
                                                           + "' and RoleName = '" + role + "'";

                                                if (dvEstimatedTable.Count > 0)
                                                {
                                                    foreach (DataRowView drv in dvEstimatedTable)
                                                    {
                                                        billable = billable + Convert.ToDouble(drv["BillableHours"]);
                                                        budgeted = budgeted + Convert.ToDouble(drv["BudgetHours"]);
                                                        revbudgeted = revbudgeted + Convert.ToDouble(drv["RevisedBudgetHours"]);

                                                    }
                                                }

                                            }

                                        }


                                    }//if dv.Count>1

                                    /////////////furnish columns with hours///////////////////

                                    //single result
                                    if (dv.Count == 1)
                                    {
                                        foreach (DataRowView drv in dv)
                                        {
                                            drv["BillableHours"] = billable;
                                            drv["BudgetHours"] = budgeted;
                                            drv["RevisedBudgetHours"] = revbudgeted;
                                        }
                                    }

                                    //multiple row for the result
                                    if (dv.Count > 1)
                                    {
                                        //calculate the sum of actual hours here
                                        double? actualHours = 0.0;

                                        foreach (DataRowView drv in dv)
                                        {
                                            //get hours total
                                            if (!(drv["ActualHours"] is DBNull))
                                            {
                                                actualHours += Convert.ToDouble(drv["ActualHours"]);
                                            }

                                        }

                                        //update this total to first row and delete others
                                        int j = 1;
                                        foreach (DataRowView drv in dv)
                                        {
                                            if (j == 1) //first row
                                            {
                                                drv["ActualHours"] = actualHours;
                                                drv["BillableHours"] = billable;
                                                drv["BudgetHours"] = budgeted;
                                                drv["RevisedBudgetHours"] = revbudgeted;
                                                j = j + 1;
                                            }
                                            else
                                            {
                                                //remove rows (from 2nd rows onwards)
                                                tableFinal.Rows.Remove(drv.Row);
                                            }
                                        }


                                    }//if dv.Count>1

                                    //////End///////furnish columns with hours///////////////////

                                }//resources (for loop)

                            }//roles (for loop)

                        }//phases (for loop)

                    }//project names   (for loop)



                    //////////////////////////
                    //update rows with average (Billable,Budget,RevBudget)

                    double averageBillable = 0.00;
                    double averageBudgeted = 0.00;
                    double averageRevisedBudgeted = 0.00;
                    foreach (string projectName in projectNames)
                    {
                        foreach (string phase in phases)
                        {

                            foreach (string role in roles)
                            {


                                DataView dv = new DataView(tableFinal);
                                dv.RowFilter = "ProjectName='" + projectName + "' AND Phase = '" + phase + "' AND Role='" + role + "'";
                                averageBillable = 0.00;
                                averageBudgeted = 0.00;
                                averageRevisedBudgeted = 0.00;
                                if (dv.Count > 1)
                                {
                                    //get sum for average
                                    foreach (DataRowView drv in dv)
                                    {
                                        averageBillable = (Convert.ToDouble(drv["BillableHours"]));
                                        averageBudgeted = (Convert.ToDouble(drv["BudgetHours"]));
                                        averageRevisedBudgeted = (Convert.ToDouble(drv["RevisedBudgetHours"]));
                                    }

                                    averageBillable =Math.Round( (averageBillable / dv.Count),2);
                                    averageBudgeted =Math.Round(( averageBudgeted / dv.Count),2);
                                    averageRevisedBudgeted =Math.Round(( averageRevisedBudgeted / dv.Count),2);

                                    //update rows
                                    DataRow[] rows = tableFinal.Select("ProjectName='" + projectName + "' AND Phase = '" + phase + "' AND Role='" + role + "'");
                                    if (rows.Count() > 1)
                                    {
                                        for (int i = 0; i < rows.Count(); ++i)
                                        {
                                            rows[i]["BillableHours"] = averageBillable.ToString();
                                            rows[i]["BudgetHours"] = averageBudgeted.ToString();
                                            rows[i]["RevisedBudgetHours"] = averageRevisedBudgeted.ToString();
                                        }
                                    }

                                }//if

                            }//role

                        }//phase
                    }//project name

                    ///////////////////////////


                    //update total rows (billable,budget,rev.budget)
                    double? gandtotalBillable = 0.0;
                    double? gandtotalBudgeted = 0.0;
                    double? gandtotalRevisedBudgeted = 0.0;

                    foreach (string projectName in projectNames)
                    {
                        double? totalBillable = 0.0;
                        double? totalBudgeted = 0.0;
                        double? totalRevisedBudgeted = 0.0;

                        DataView dv = new DataView(tableFinal);
                        dv.RowFilter = "ProjectName = '" + projectName + "'";

                        foreach (DataRowView drv in dv)
                        {
                            totalBillable = totalBillable + (Convert.ToDouble(drv["BillableHours"]));
                            totalBudgeted = totalBudgeted + (Convert.ToDouble(drv["BudgetHours"]));
                            totalRevisedBudgeted = totalRevisedBudgeted + (Convert.ToDouble(drv["RevisedBudgetHours"]));
                        }

                        gandtotalBillable += totalBillable;
                        gandtotalBudgeted += totalBudgeted;
                        gandtotalRevisedBudgeted += totalRevisedBudgeted;

                        string flagProjectTotalField = projectName + "#"; //false data for separation
                        DataRow[] rows = tableFinal.Select("ResourceName='Total' AND BillableHours = '" + flagProjectTotalField + "'");

                        ////update totals
                        if (rows.Count() > 0)
                        {
                            rows[0]["BillableHours"] = totalBillable.ToString();
                            rows[0]["BudgetHours"] = totalBudgeted.ToString();
                            rows[0]["RevisedBudgetHours"] = totalRevisedBudgeted.ToString();
                        }

                    }

                    //grand totals at the ned of the row
                    if (tableFinal.Rows.Count > 0)
                    {
                        tableFinal.Rows[tableFinal.Rows.Count - 1]["BillableHours"] = gandtotalBillable.ToString();
                        tableFinal.Rows[tableFinal.Rows.Count - 1]["BudgetHours"] = gandtotalBudgeted.ToString();
                        tableFinal.Rows[tableFinal.Rows.Count - 1]["RevisedBudgetHours"] = gandtotalRevisedBudgeted.ToString();
                    }

                    //add final table to datasetReport
                    DataSet dsReport = new DataSet();
                    dsReport.Tables.Add(tableFinal); //add all these merged table to dataset

                    //bind the dataset to datagrid
                    gdReport.DataSource = dsReport;
                    gdReport.DataBind();

                    MergeRows(gdReport); //merging row wise (vertical) 

                    DataTableValues(); // Method for storing DataTableValues to session

                }//table count validation end
                else
                {
                    gdReport.DataSource = null;
                    gdReport.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }


        }

        /// <summary>
        /// Extracts the date and format.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <returns></returns>
        private string ExtractDateAndFormat(string dateToFormat)
        {
            try
            {
                string dayPart;
                string monthPart;
                string yearPart;

                string[] splitted = dateToFormat.Split('/');
                dayPart = splitted[0];
                monthPart = splitted[1];
                yearPart = splitted[2];

                return yearPart + "/" + monthPart + "/" + dayPart;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return dateToFormat;
            }

        }

        /// <summary>
        /// Binds the grid view for resource report.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="p_2">The P_2.</param>
        private void BindGridViewForResourceReport(string sortColumn, string sortDirection)
        {
            try
            {
                DataSet dsResourceDetails;
                List<SqlParameter> parameter = new List<SqlParameter>();

                if (rdbMonthly.Checked.Equals(true))  //MonthWise Report
                {
                    parameter.Add(new SqlParameter("@Year", ddlYear.SelectedValue));
                    parameter.Add(new SqlParameter("@Month", ddlMonth.SelectedIndex + 1));
                    dsResourceDetails = PMOscar.BaseDAL.ExecuteSPDataSet("[GetResourceUtilization]", parameter);

                }
                else  //Periodwise report
                {

                    //get from and to dates
                    string fromDate = hiddenstartWeek.Value;
                    string endDate = hiddenendWeek.Value;

                    fromDate = ExtractDateAndFormat(fromDate); //format date as "year/month/day"
                    endDate = ExtractDateAndFormat(endDate);  //format date as "year/month/day"

                    parameter.Add(new SqlParameter("@FromDate", fromDate));
                    parameter.Add(new SqlParameter("@ToDate", endDate));
                    dsResourceDetails = PMOscar.BaseDAL.ExecuteSPDataSet("[GetResourceUtilizationByPeriod]", parameter);

                }

                //Process this for report
                if (dsResourceDetails.Tables.Count > 0)
                {


                    //get unique resource names
                    DataTable dt = dsResourceDetails.Tables[0];

                    DataTable dtResource = dt.DefaultView.ToTable(true, PMOscar.Core.Constants.FieldName.Team.RESOURCENAME);
                    var distinct = dtResource.AsEnumerable().Distinct(DataRowComparer.Default); //get distinct rows
                    List<string> resourceNames = new List<string>();

                    foreach (DataRow drResource in distinct)
                    {
                        resourceNames.Add(drResource[PMOscar.Core.Constants.FieldName.Team.RESOURCENAME].ToString());   //get distinct resource names
                    }

                    //sorting
                    if (sortColumn.Equals(PMOscar.Core.Constants.FieldName.Team.RESOURCENAME) && sortDirection.Equals("DESC"))
                    {
                        resourceNames.Reverse();
                    }

                    DataTable dtProjs = dt.DefaultView.ToTable(true, PMOscar.Core.Constants.FieldName.Project.PROJECTNAME);
                    var distinctProject = dtProjs.AsEnumerable().Distinct(DataRowComparer.Default); //get distinct rows
                    List<string> projectNames = new List<string>();

                    foreach (DataRow drProj in distinctProject)
                    {
                        projectNames.Add(drProj[PMOscar.Core.Constants.FieldName.Project.PROJECTNAME].ToString());   //get distinct project names

                    }

                    //Calculate sum of hours
                    DataTable tableFinal = dtResource.Clone(); //get the structure

                    foreach (String resource in resourceNames)
                    {

                        IEnumerable<DataRow> queryResourceName = from myRow in dt.AsEnumerable()
                                                                 where myRow.Field<string>(PMOscar.Core.Constants.FieldName.Team.RESOURCENAME).Equals(resource)
                                                                 select myRow;
                        DataTable tableInitial = queryResourceName.CopyToDataTable();  //get table for each resourcename for initial structure

                        //calculate sum of actual hours for each project 
                        var sumPlannedHours = tableInitial.AsEnumerable().Sum(x => x.Field<decimal?>("PlannedHours"));   //toal actual hours
                        var sumActualHours = tableInitial.AsEnumerable().Sum(x => x.Field<decimal?>("ActualHours"));

                        //add this as a row
                        DataRow drTotal = tableInitial.NewRow();
                        drTotal[0] = resource + "#"; //dummy value for merging separation & grand total
                        drTotal[2] = "Total"; //inserted at the Phase name
                        drTotal[3] = sumPlannedHours;  //last column
                        drTotal[4] = sumActualHours;

                        tableInitial.Rows.Add(drTotal);

                        //merge each table to final table structure  
                        tableFinal.Merge(tableInitial); //merging 
                    }//for loop for each team name

                    //merge week rows to single row

                    //grand total column adding as the last row
                    double? gandtotalPlanned = 0.0;
                    double? gandtotalActual = 0.0;
                    if (tableFinal.Rows.Count > 0)
                    {

                        foreach (String resource in resourceNames)
                        {

                            string flagTeamNameTotalField = resource + "#"; //false data for separation
                            DataRow[] rows = tableFinal.Select("ResourceName='" + flagTeamNameTotalField + "'");
                            ////update totals
                            if (rows.Count() > 0)
                            {
                                gandtotalPlanned += Convert.ToDouble(rows[0]["PlannedHours"]);
                                gandtotalActual += Convert.ToDouble(rows[0]["ActualHours"]);
                            }
                        }

                        //add grand total to bottom
                        DataRow drGrandTotal = tableFinal.NewRow();
                        drGrandTotal[2] = "Grand Total"; //inserted at the Phase
                        drGrandTotal[3] = gandtotalPlanned;  //last column
                        drGrandTotal[4] = gandtotalActual;  //last column
                        tableFinal.Rows.Add(drGrandTotal);

                    }//table has rows

                    DataSet dsReport = new DataSet();
                    dsReport.Tables.Add(tableFinal); //add all these merged table to dataset
                    gdReport.DataSource = dsReport;
                    gdReport.DataBind();

                    MergeRowsForResourceReport(gdReport); //Merge the data in the grid

                }//has data
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

        }

        /// <summary>
        /// Binds the grid view for team report.
        /// </summary>
        private void BindGridViewForTeamReport(string sortColumn, string sortDirection)
        {
            try
            {
                DataSet dsTeamDetails;
                List<SqlParameter> parameter = new List<SqlParameter>();
                if (rdbMonthly.Checked.Equals(true))  //MonthWise Report
                {
                    parameter.Add(new SqlParameter("@Year", ddlYear.SelectedValue));
                    parameter.Add(new SqlParameter("@Month", ddlMonth.SelectedIndex + 1));
                    dsTeamDetails = PMOscar.BaseDAL.ExecuteSPDataSet("[GetTeamUtilization]", parameter);

                }
                else  //Periodwise report
                {
                    //get from and to dates
                    string fromDate = hiddenstartWeek.Value;
                    string endDate = hiddenendWeek.Value;

                    fromDate = ExtractDateAndFormat(fromDate); //format date as "year/month/day"
                    endDate = ExtractDateAndFormat(endDate);  //format date as "year/month/day"

                    parameter.Add(new SqlParameter("@FromDate", fromDate));
                    parameter.Add(new SqlParameter("@ToDate", endDate));
                    dsTeamDetails = PMOscar.BaseDAL.ExecuteSPDataSet("[GetTeamUtilizationbyPeriod]", parameter);

                }

                if (dsTeamDetails.Tables.Count > 0)
                {
                    //get unique team names
                    DataTable dt = dsTeamDetails.Tables[0];

                    DataTable dtTeam = dt.DefaultView.ToTable(true, PMOscar.Core.Constants.FieldName.Team.TEAM);
                    var distinct = dtTeam.AsEnumerable().Distinct(DataRowComparer.Default); //get distinct rows
                    List<string> teamNames = new List<string>();

                    foreach (DataRow drTeam in distinct)
                    {
                        teamNames.Add(drTeam[PMOscar.Core.Constants.FieldName.Team.TEAM].ToString());   //get distinct team names
                    }

                    //sorting
                    if (sortColumn.Equals("Team") && sortDirection.Equals("DESC"))
                    {
                        teamNames.Reverse();
                    }

                    //get unique resource names
                    DataTable dtResources = dt.DefaultView.ToTable(true, PMOscar.Core.Constants.FieldName.Team.RESOURCENAME);
                    var distinctResource = dtResources.AsEnumerable().Distinct(DataRowComparer.Default); //get distinct rows
                    List<string> resourceNames = new List<string>();

                    foreach (DataRow drResource in distinctResource)
                    {
                        resourceNames.Add(drResource[PMOscar.Core.Constants.FieldName.Team.RESOURCENAME].ToString());   //get distinct team names

                    }

                    int count = 0;
                    DataTable tableFinal = dtTeam.Clone(); //get the structure

                    //add resource count column
                    DataColumn resourceCountColumn = tableFinal.Columns.Add("ResourceCount", typeof(Int32));
                    resourceCountColumn.AllowDBNull = true;
                    resourceCountColumn.Unique = false;
                    resourceCountColumn.SetOrdinal(1);

                    foreach (String team in teamNames)
                    {
                        count = count + 1;
                        IEnumerable<DataRow> queryTeamName = from myRow in dt.AsEnumerable()
                                                             where myRow.Field<string>("Team").Equals(team)
                                                             select myRow;
                        DataTable tableInitial = queryTeamName.CopyToDataTable();  //get table for each teamname for initial structure

                        //calculate sum of actual hours for each project 
                        var sumPlannedHours = tableInitial.AsEnumerable().Sum(x => x.Field<decimal?>("PlannedHours"));   //toal actual hours
                        var sumActualHours = tableInitial.AsEnumerable().Sum(x => x.Field<decimal?>("ActualHours"));

                        //add this as a row
                        DataRow drTotal = tableInitial.NewRow();
                        drTotal[0] = team + "#"; //dummy value for merging separation & grand total
                        drTotal[1] = "Total"; //inserted at the resource name
                        drTotal[2] = sumPlannedHours;  //last column
                        drTotal[3] = sumActualHours;

                        tableInitial.Rows.Add(drTotal);

                        //merge each table to final table structure  
                        tableFinal.Merge(tableInitial); //merging 
                    }//for loop for each team name

                    //merge week rows to single row
                    foreach (string teamName in teamNames)
                    {
                        foreach (string resource in resourceNames)
                        {
                            //data view from table
                            DataView dv = new DataView(tableFinal);
                            dv.RowFilter = "Team = '" + teamName + "' and ResourceName = '" + resource + "'";

                            if (dv.Count > 0)
                            {
                                //multiple row for the result
                                if (dv.Count > 1)
                                {
                                    //calculate the sum of actual hours here
                                    double? actualHours = 0.0;
                                    double? plannedHours = 0.0;
                                    foreach (DataRowView drv in dv)
                                    {
                                        //get hours total
                                        if (!(drv["ActualHours"] is DBNull))
                                        {
                                            actualHours += Convert.ToDouble(drv["ActualHours"]);
                                        }

                                        //get planned hours total
                                        if (!(drv["PlannedHours"] is DBNull))
                                        {
                                            plannedHours += Convert.ToDouble(drv["PlannedHours"]);
                                        }

                                    }
                                    //update this total to first row and delete others
                                    int j = 1;
                                    foreach (DataRowView drv in dv)
                                    {
                                        if (j == 1) //first row
                                        {
                                            drv["ActualHours"] = actualHours;
                                            drv["PlannedHours"] = plannedHours;
                                            j = j + 1;
                                        }
                                        else
                                        {
                                            //remove rows (from 2nd rows onwards)
                                            tableFinal.Rows.Remove(drv.Row);
                                        }
                                    }

                                }//dv.count>1

                            }//dv.Count > 0

                        }//resource name (for loop)

                    }//team name (for loop)

                    //updating number of resource row for resource count
                    int grandTotalResourceCount = 0;
                    if (tableFinal.Rows.Count > 0)
                    {
                        //add resource count column
                        DataColumn utilPercent = tableFinal.Columns.Add("UtilizationPercent");
                        utilPercent.AllowDBNull = true;
                        utilPercent.Unique = false;

                        foreach (string teamName in teamNames)
                        {
                            //get count
                            DataRow[] rows = tableFinal.Select("Team='" + teamName + "'");
                            if (rows.Count() > 0)
                            {
                                for (int i = 0; i < rows.Count(); ++i)
                                {
                                    rows[i][1] = rows.Count();
                                    //resource count for this month from first row only (since it will merge later) 
                                    if (i == 0)
                                    {
                                        grandTotalResourceCount += rows.Count(); //total resources in this month
                                    }
                                    try
                                    {   //Utilization Percentage
                                        if ((string.IsNullOrEmpty(Convert.ToString(rows[i]["ActualHours"])) || Convert.ToString(rows[i]["ActualHours"]) != "0") && (string.IsNullOrEmpty(Convert.ToString(rows[i]["PlannedHours"]))) || Convert.ToString(rows[i]["PlannedHours"]) != "0")
                                        {
                                            rows[i]["UtilizationPercent"] = Math.Round((Convert.ToDouble(rows[i]["ActualHours"]) / Convert.ToDouble(rows[i]["PlannedHours"])) * 100, 2) + "%";
                                        }
                                        else if ((string.IsNullOrEmpty(Convert.ToString(rows[i]["ActualHours"])) || Convert.ToString(rows[i]["ActualHours"]) != "0") && (string.IsNullOrEmpty(Convert.ToString(rows[i]["PlannedHours"]))) || Convert.ToString(rows[i]["PlannedHours"]) == "0")
                                        {
                                            rows[i]["UtilizationPercent"] = Math.Round((Convert.ToDouble(rows[i]["ActualHours"]) / Convert.ToDouble(rows[i]["PlannedHours"])) * 100, 2) + "%";
                                        }
                                        else
                                        {
                                            rows[i]["UtilizationPercent"] = "0%";
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                }
                            }

                        }

                    }

                    //grand total column adding as the last row
                    double? gandtotalPlanned = 0.0;
                    double? gandtotalActual = 0.0;
                    if (tableFinal.Rows.Count > 0)
                    {

                        foreach (string teamName in teamNames)
                        {

                            string flagTeamNameTotalField = teamName + "#"; //false data for separation
                            DataRow[] rows = tableFinal.Select("Team='" + flagTeamNameTotalField + "'");
                            ////update totals
                            if (rows.Count() > 0)
                            {
                                gandtotalPlanned += Convert.ToDouble(rows[0]["PlannedHours"]);
                                gandtotalActual += Convert.ToDouble(rows[0]["ActualHours"]);

                                try
                                {   //Utilization Percentage
                                    if ((string.IsNullOrEmpty(Convert.ToString(rows[0]["ActualHours"])) || Convert.ToString(rows[0]["ActualHours"]) != "0") && (string.IsNullOrEmpty(Convert.ToString(rows[0]["PlannedHours"]))) || Convert.ToString(rows[0]["PlannedHours"]) != "0")
                                    {
                                        rows[0]["UtilizationPercent"] = Math.Round((Convert.ToDouble(rows[0]["ActualHours"]) / Convert.ToDouble(rows[0]["PlannedHours"])) * 100, 2) + "%";
                                    }
                                    else
                                    {
                                        rows[0]["UtilizationPercent"] = "0%";
                                    }
                                }
                                catch (Exception ex)
                                {

                                }

                            }
                        }
                        decimal utilizationGrandPercent = 0;
                        if (gandtotalPlanned != 0)
                        {
                            //Utilization Percentage for grand total
                            utilizationGrandPercent = Convert.ToDecimal((gandtotalActual / gandtotalPlanned) * 100);
                        }
                        else
                        {
                            utilizationGrandPercent = 0;
                        }
                        

                        //add grand total to bottom
                        DataRow drGrandTotal = tableFinal.NewRow();
                        drGrandTotal[1] = grandTotalResourceCount; //dummy value for merging separation
                        drGrandTotal[0] = "Grand Total"; //inserted at the revised budget position
                        drGrandTotal[3] = gandtotalPlanned;  //last column
                        drGrandTotal[4] = gandtotalActual;  //last column
                        drGrandTotal[5] = Math.Round(utilizationGrandPercent, 2).ToString() + "%";
                        tableFinal.Rows.Add(drGrandTotal);

                    }//table has rows

                    //add final table to datasetReport
                    DataSet dsReport = new DataSet();
                    dsReport.Tables.Add(tableFinal); //add all these merged table to dataset
                    gdReport.DataSource = dsReport;
                    gdReport.DataBind();

                    //Merging rows 
                    MergeTeamRows(gdReport);

                }//dataset has values
                else
                {
                    gdReport.DataSource = null;  //empty data to grid
                    gdReport.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Binds the grid view for Company report.
        /// </summary>
        private void BindGridViewForCompanyReport(string sortColumn, string sortDirection)
        {
            try
            {
                DataSet dsCompanyUtilizationDetails;
                List<SqlParameter> parameter = new List<SqlParameter>();
                if (rdbSummaryReport.Checked.Equals(true))
                {
                    dsCompanyUtilizationDetails = PMOscar.BaseDAL.ExecuteSPDataSet("[GetCompanyUtilization_Summary]", parameter);

                }
                else  //Compnay report
                {
                    //get Month and Year
                    parameter.Add(new SqlParameter("@Year", ddlYear.SelectedValue));
                    parameter.Add(new SqlParameter("@Month", ddlMonth.SelectedIndex + 1));
                    dsCompanyUtilizationDetails = PMOscar.BaseDAL.ExecuteSPDataSet("[GetCompanyUtilization_Detailed]", parameter);

                }

                if (dsCompanyUtilizationDetails.Tables.Count != 0)
                {
                    if (rdbSummaryReport.Checked.Equals(true))
                    {
                        BindSummaryReport(dsCompanyUtilizationDetails);
                    }
                    else
                    {
                        BindDetailedReport(dsCompanyUtilizationDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Binds the grid view for budget revision report.
        /// </summary>
        private void BindGridViewForBudgetRevisionReport()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);                
            }
        }

        /// <summary>
        /// Removes the duplicates from list.
        /// </summary>
        /// <param name="phasesWorked">The phases worked.</param>
        private List<string> RemoveDuplicatesFromList(List<string> phasesWorked)
        {
            List<string> distinct = phasesWorked;
            try
            {
                distinct = phasesWorked.Distinct().ToList();
                return distinct;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return distinct;
            }
        }


        /// <summary>
        /// Merges the rows for resource report.
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        private void MergeRowsForResourceReport(GridView gridView)
        {
            try
            {

                for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                {
                    GridViewRow row = gridView.Rows[rowIndex];
                    GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                    //(merging Resource name column rows)
                    if (row.Cells[0].Text == previousRow.Cells[0].Text)
                    {
                        row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                               previousRow.Cells[0].RowSpan + 1;
                        previousRow.Cells[0].Visible = false;
                    }

                    // (Merging projectNames rows)
                    if (row.Cells[1].Text == previousRow.Cells[1].Text)
                    {
                        row.Cells[1].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 :
                                               previousRow.Cells[1].RowSpan + 1;
                        previousRow.Cells[1].Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Merges the team rows.
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        public static void MergeTeamRows(GridView gridView)
        {
            try
            {

                for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                {
                    GridViewRow row = gridView.Rows[rowIndex];
                    GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                    //(merging Team name column rows)
                    if (row.Cells[0].Text == previousRow.Cells[0].Text)
                    {
                        row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                               previousRow.Cells[0].RowSpan + 1;
                        previousRow.Cells[0].Visible = false;
                    }

                    // (Merging ResourceCount rows)
                    if (row.Cells[1].Text == previousRow.Cells[1].Text)
                    {
                        row.Cells[1].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 :
                                               previousRow.Cells[1].RowSpan + 1;
                        previousRow.Cells[1].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Merges the rows.
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        public static void MergeRows(GridView gridView)
        {
            try
            {
                for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                {
                    GridViewRow row = gridView.Rows[rowIndex];
                    GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                    //(merging Count column rows)
                    if (row.Cells[0].Text == previousRow.Cells[0].Text)
                    {
                        row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                               previousRow.Cells[0].RowSpan + 1;
                        previousRow.Cells[0].Visible = false;
                    }

                    //Project Name (Merging ProjectNames rows)
                    if (row.Cells[1].Text == previousRow.Cells[1].Text)
                    {
                        row.Cells[1].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 :
                                               previousRow.Cells[1].RowSpan + 1;
                        previousRow.Cells[1].Visible = false;
                    }


                    ////Role (Merging Role)
                    if ((row.Cells[2].Text == previousRow.Cells[2].Text)) //if it's in same phase
                    {

                        if (row.Cells[4].Text == previousRow.Cells[4].Text) //merge roles
                        {
                            row.Cells[4].RowSpan = previousRow.Cells[4].RowSpan < 2 ? 2 :
                                                   previousRow.Cells[4].RowSpan + 1;
                            previousRow.Cells[4].Visible = false;
                        }
                    }

                    //Phase (Merging Phase)
                    if (row.Cells[2].Text == previousRow.Cells[2].Text)
                    {
                        row.Cells[2].RowSpan = previousRow.Cells[2].RowSpan < 2 ? 2 :
                                               previousRow.Cells[2].RowSpan + 1;
                        previousRow.Cells[2].Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

        }

        /// <summary>
        /// Merge Rows For Company utilization Detailed Report
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        public static void MergeRowsForCmpyDetailedReport(GridView gridView)
        {
            try
            {
                for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                {
                    GridViewRow row = gridView.Rows[rowIndex];
                    GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                    //(merging Team column rows)
                    if (row.Cells[0].Text == previousRow.Cells[0].Text)
                    {
                        row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                               previousRow.Cells[0].RowSpan + 1;
                        previousRow.Cells[0].Visible = false;
                        //row.Cells[5].Text = row.Cells[5].Text;

                        //if (row.Cells[5].Text == previousRow.Cells[5].Text)
                        //{
                        //    row.Cells[5].RowSpan = previousRow.Cells[5].RowSpan < 2 ? 2 :
                        //                           previousRow.Cells[5].RowSpan + 1;
                        //    previousRow.Cells[5].Visible = false;
                        //}
                    }

                    //(merging Role column rows)
                    //if (row.Cells[1].Text == previousRow.Cells[1].Text)
                    //{
                    //    row.Cells[1].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 :
                    //                           previousRow.Cells[1].RowSpan + 1;
                    //    previousRow.Cells[1].Visible = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        // Method for storing DataTableValues to session...
        /// <summary>
        /// Datas the table values.
        /// </summary>
        private void DataTableValues()
        {
            DataTable dtdatas = new DataTable();
            dtdatas.Columns.Add("Year");
            dtdatas.Columns.Add("Month");
            DataRow dr = dtdatas.NewRow();
            dr["Year"] = ddlYear.SelectedItem.Text;
            dtdatas.Rows.Add(dr);
            Session["dtDatasReports"] = dtdatas;
            dtdatas.Dispose();
        }

        /// <summary>
        /// Extracts the date from title.
        /// </summary>
        /// <param name="reportTitle">The report title.</param>
        /// <returns></returns>
        private string ExtractDateFromTitle(string reportTitle)
        {
            try
            {
                return reportTitle.Substring((reportTitle.IndexOf('-', 0)) + 1, (reportTitle.Length - 1) - (reportTitle.IndexOf('-', 0)));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return String.Empty;
            }
        }

        /// <summary>
        /// Confirms that an <see cref="T:System.Web.UI.HtmlControls.HtmlForm"/> control is rendered for the specified ASP.NET server control at run time.
        /// </summary>
        /// <param name="control">The ASP.NET server control that is required in the <see cref="T:System.Web.UI.HtmlControls.HtmlForm"/> control.</param>
        /// <exception cref="T:System.Web.HttpException">
        /// The specified server control is not contained between the opening and closing tags of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm"/> server control at run time.
        ///   </exception>
        ///   
        /// <exception cref="T:System.ArgumentNullException">
        /// The control to verify is null.
        ///   </exception>
        public override void VerifyRenderingInServerForm(Control control) //required this override for excel export
        {

        }

        /// <summary>
        /// Exports to excel.
        /// </summary>
        /// <param name="strFileName">Name of the STR file.</param>
        /// <param name="dg">The dg.</param>
        private void ExportToExcel(string strFileName, GridView GridExport)
        {
            try
            {
                if (GridExport.Rows.Count > 0)
                {
                    //Hide columns from ProjectWise Report (Hiding earlier won't effect in export)
                    if (strFileName.Contains("ProjectWiseUtilizationReport"))
                    {
                        GridExport.HeaderRow.Cells[0].Text = "#";
                        GridExport.HeaderRow.Cells[4].Text = "Role";
                        GridExport.HeaderRow.Cells[5].Text = "Resource Name";
                        GridExport.HeaderRow.Cells[6].Text = "Billable Hours";
                        GridExport.HeaderRow.Cells[7].Text = "Budget Hours";
                        GridExport.HeaderRow.Cells[8].Text = "Revised Budget Hours";
                        GridExport.HeaderRow.Cells[9].Text = "Actual Hours";


                        GridExport.HeaderRow.Cells[3].Visible = false; //Hide ProjectId Column 
                        GridExport.HeaderRow.Cells[10].Visible = false;//hide phaseid column 

                        for (int i = 0; i < GridExport.Rows.Count; i++)
                        {
                            GridExport.Rows[i].Cells[3].Visible = false; //hide projectid column 
                            GridExport.Rows[i].Cells[10].Visible = false; //hide phaseid column 
                        }


                        //Color Formats
                        GridExport.HeaderRow.Style.Add("background-color", "#FFFFFF");
                        //Apply style to Individual Cells
                        GridExport.HeaderRow.Cells[0].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[1].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[2].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[4].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[5].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[6].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[7].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[8].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[9].Style.Add("background-color", "#539cd1");
                        for (int i = 0; i < GridExport.Rows.Count; i++)
                        {

                            GridViewRow row = GridExport.Rows[i];
                            //Change Color back to white
                            row.BackColor = System.Drawing.Color.White;
                            //Apply text style to each Row
                            row.Attributes.Add("class", "textmode");

                            if (row.Cells[5].Text.Equals("Total") || row.Cells[5].Text.Equals("Grand Total"))
                            {
                                row.Cells[0].Style.Add("background-color", "#b2d2e9");
                                row.Cells[1].Style.Add("background-color", "#b2d2e9");
                                row.Cells[2].Style.Add("background-color", "#b2d2e9");
                                row.Cells[4].Style.Add("background-color", "#b2d2e9");
                                row.Cells[5].Style.Add("background-color", "#b2d2e9");
                                row.Cells[6].Style.Add("background-color", "#b2d2e9");
                                row.Cells[7].Style.Add("background-color", "#b2d2e9");
                                row.Cells[8].Style.Add("background-color", "#b2d2e9");
                                row.Cells[9].Style.Add("background-color", "#b2d2e9");
                            }
                            else
                            {
                                row.Cells[0].Style.Add("background-color", "#eff3fb");
                                row.Cells[1].Style.Add("background-color", "#eff3fb");
                                row.Cells[2].Style.Add("background-color", "#eff3fb");
                                row.Cells[4].Style.Add("background-color", "#eff3fb");
                                row.Cells[5].Style.Add("background-color", "#eff3fb");
                                row.Cells[6].Style.Add("background-color", "#eff3fb");
                                row.Cells[7].Style.Add("background-color", "#eff3fb");
                                row.Cells[8].Style.Add("background-color", "#eff3fb");
                                row.Cells[9].Style.Add("background-color", "#eff3fb");
                            }

                        }


                    }
                    else if (strFileName.Contains("TeamWiseUtilizationReport"))
                    {
                        GridExport.HeaderRow.Cells[0].Text = "Team";
                        GridExport.HeaderRow.Cells[1].Text = "Number of Resources";
                        GridExport.HeaderRow.Cells[2].Text = "Resource Name";
                        GridExport.HeaderRow.Cells[3].Text = "Planned Hours";
                        GridExport.HeaderRow.Cells[4].Text = "Actual Hours";
                        GridExport.HeaderRow.Cells[5].Text = "Utilization %";

                        //Color Formats
                        GridExport.HeaderRow.Style.Add("background-color", "#FFFFFF");
                        //Apply style to Individual Cells
                        GridExport.HeaderRow.Cells[0].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[1].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[2].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[3].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[4].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[5].Style.Add("background-color", "#539cd1");

                        for (int i = 0; i < GridExport.Rows.Count; i++)
                        {

                            GridViewRow row = GridExport.Rows[i];
                            //Change Color back to white
                            row.BackColor = System.Drawing.Color.White;
                            //Apply text style to each Row
                            row.Attributes.Add("class", "textmode");

                            if (row.Cells[2].Text.Equals("Total") || row.Cells[0].Text.Equals("Grand Total"))
                            {
                                row.Cells[0].Style.Add("background-color", "#b2d2e9");
                                row.Cells[1].Style.Add("background-color", "#b2d2e9");
                                row.Cells[2].Style.Add("background-color", "#b2d2e9");
                                row.Cells[3].Style.Add("background-color", "#b2d2e9");
                                row.Cells[4].Style.Add("background-color", "#b2d2e9");
                                row.Cells[5].Style.Add("background-color", "#b2d2e9");

                            }
                            else
                            {
                                row.Cells[0].Style.Add("background-color", "#eff3fb");
                                row.Cells[1].Style.Add("background-color", "#eff3fb");
                                row.Cells[2].Style.Add("background-color", "#eff3fb");
                                row.Cells[3].Style.Add("background-color", "#eff3fb");
                                row.Cells[4].Style.Add("background-color", "#eff3fb");
                                row.Cells[5].Style.Add("background-color", "#eff3fb");

                            }

                        }

                    }
                    else if (strFileName.Contains("ResourceWiseUtilizationReport"))//Resource Wise Report
                    {

                        GridExport.HeaderRow.Cells[0].Text = "Resource Name";
                        GridExport.HeaderRow.Cells[1].Text = "Project Name";
                        GridExport.HeaderRow.Cells[2].Text = "Phase";
                        GridExport.HeaderRow.Cells[3].Text = "Planned Hours";
                        GridExport.HeaderRow.Cells[4].Text = "Actual Hours";

                        GridExport.HeaderRow.Cells[5].Visible = false; //Hide resourceid Column Header
                        GridExport.HeaderRow.Cells[6].Visible = false; //Hide projectid Column Header
                        GridExport.HeaderRow.Cells[7].Visible = false; //Hide count Column Header
                        GridExport.HeaderRow.Cells[8].Visible = false; //Hide PhaseID Column Header

                        //Color Formats
                        GridExport.HeaderRow.Style.Add("background-color", "#FFFFFF");
                        //Apply style to Individual Cells
                        GridExport.HeaderRow.Cells[0].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[1].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[2].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[3].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[4].Style.Add("background-color", "#539cd1");

                        for (int i = 0; i < GridExport.Rows.Count; i++)
                        {

                            GridExport.Rows[i].Cells[5].Visible = false; //hide phaseid rows 
                            GridExport.Rows[i].Cells[5].Visible = false; //Hide resourceid Column Header
                            GridExport.Rows[i].Cells[6].Visible = false; //Hide projectid Column Header
                            GridExport.Rows[i].Cells[7].Visible = false; //Hide count Column Header
                            GridExport.Rows[i].Cells[8].Visible = false; //Hide PhaseID Column Header


                            GridViewRow row = GridExport.Rows[i];
                            //Change Color back to white
                            row.BackColor = System.Drawing.Color.White;
                            //Apply text style to each Row
                            row.Attributes.Add("class", "textmode");

                            if (row.Cells[2].Text.Equals("Total") || row.Cells[2].Text.Equals("Grand Total"))
                            {
                                row.Cells[0].Style.Add("background-color", "#b2d2e9");
                                row.Cells[1].Style.Add("background-color", "#b2d2e9");
                                row.Cells[2].Style.Add("background-color", "#b2d2e9");
                                row.Cells[3].Style.Add("background-color", "#b2d2e9");
                                row.Cells[4].Style.Add("background-color", "#b2d2e9");

                            }
                            else
                            {
                                row.Cells[0].Style.Add("background-color", "#eff3fb");
                                row.Cells[1].Style.Add("background-color", "#eff3fb");
                                row.Cells[2].Style.Add("background-color", "#eff3fb");
                                row.Cells[3].Style.Add("background-color", "#eff3fb");
                                row.Cells[4].Style.Add("background-color", "#eff3fb");

                            }

                        }
                    }
                    else if (strFileName.Contains("CompanyUtilizationSummaryReport"))//Company Utilization Summary Report
                    {
                        GridExport.HeaderRow.Cells[0].Text = "Month Year";
                        GridExport.HeaderRow.Cells[1].Text = "Available";
                        GridExport.HeaderRow.Cells[2].Text = "Billed";
                        GridExport.HeaderRow.Cells[3].Text = "Utilized But Not Billed";
                        GridExport.HeaderRow.Cells[4].Text = "Admin";
                        GridExport.HeaderRow.Cells[5].Text = "Open+VAS";
                        GridExport.HeaderRow.Cells[6].Text = "Proposal";
                        GridExport.HeaderRow.Cells[7].Text = "Utilization %";

                        GridExport.HeaderRow.Cells[8].Visible = false;//hide Action rows 

                        //Color Formats
                        GridExport.HeaderRow.Style.Add("background-color", "#FFFFFF");
                        //Apply style to Individual Cells
                        GridExport.HeaderRow.Cells[0].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[1].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[2].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[3].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[4].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[5].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[6].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[7].Style.Add("background-color", "#539cd1");

                        for (int i = 0; i < GridExport.Rows.Count; i++)
                        {
                            GridExport.Rows[i].Cells[8].Visible = false;

                            GridViewRow row = GridExport.Rows[i];
                            //Change Color back to white
                            row.BackColor = System.Drawing.Color.White;
                            //Apply text style to each Row
                            row.Attributes.Add("class", "textmode");
                        }

                    }
                    else if (strFileName.Contains("CompanyUtilizationDetailedReport"))//Company Utilization Detailed Report
                    {
                        GridExport.HeaderRow.Cells[0].Text = "Team";
                        GridExport.HeaderRow.Cells[1].Text = "Role";

                        GridExport.HeaderRow.Cells[2].Visible = false;//hide RoleId rows

                        GridExport.HeaderRow.Cells[3].Text = "Available";
                        GridExport.HeaderRow.Cells[4].Text = "Billed";
                        GridExport.HeaderRow.Cells[5].Text = "Utilized But Not Billed";
                        GridExport.HeaderRow.Cells[6].Text = "Admin";
                        GridExport.HeaderRow.Cells[7].Text = "Open+VAS";
                        GridExport.HeaderRow.Cells[8].Text = "Proposal";
                        GridExport.HeaderRow.Cells[9].Text = "Utilization";


                        //Color Formats
                        GridExport.HeaderRow.Style.Add("background-color", "#FFFFFF");
                        //Apply style to Individual Cells
                        GridExport.HeaderRow.Cells[0].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[1].Style.Add("background-color", "#539cd1");

                        GridExport.HeaderRow.Cells[3].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[4].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[5].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[6].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[7].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[8].Style.Add("background-color", "#539cd1");
                        GridExport.HeaderRow.Cells[9].Style.Add("background-color", "#539cd1");
                        for (int i = 0; i < GridExport.Rows.Count; i++)
                        {
                            GridExport.Rows[i].Cells[2].Visible = false;
                            GridViewRow row = GridExport.Rows[i];
                            //Change Color back to white
                            row.BackColor = System.Drawing.Color.White;
                            //Apply text style to each Row
                            row.Attributes.Add("class", "textmode");
                        }
                    }
                    //custom header
                    GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    Literal newCells = new Literal();
                    newCells.Text = lblReportTitle.Text;
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.Controls.Add(newCells);
                    // headerCell.ColumnSpan = GridExport.Columns.Count;
                       
                    if (strFileName.Contains("ProjectWiseUtilizationReport"))
                    {
                        headerCell.ColumnSpan = 9;
                    }
                    else if (strFileName.Contains("TeamWiseUtilizationReport"))
                    {
                        headerCell.ColumnSpan = 6;
                    }
                    else if (strFileName.Contains("ResourceWiseUtilizationReport"))//Resource Wise Report
                    {
                        headerCell.ColumnSpan = 5;
                    }
                    else if (strFileName.Contains("CompanyUtilizationSummaryReport"))//Resource Wise Report
                    {
                        headerCell.ColumnSpan = 8;
                        headerCell.BackColor = System.Drawing.Color.White;
                        headerCell.ForeColor = System.Drawing.Color.Black;
                    }
                    else if (strFileName.Contains("CompanyUtilizationDetailedReport"))//Resource Wise Report
                    {
                        headerCell.ColumnSpan = 9;
                    }
                    rowHeader.Cells.Add(headerCell);
                    rowHeader.Visible = true;
                    ((Table)GridExport.Controls[0]).Rows.AddAt(0, rowHeader);

                    //Export 
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.Charset = "";
                    this.EnableViewState = false;
                    System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                    GridExport.RenderControl(oHtmlTextWriter);



                    //removing hyperlinks if any
                    String sTemp = oStringWriter.ToString();
                    sTemp = sTemp.Replace("href=", "");
                    Response.Write(sTemp);
                    Response.End();
                }//grid has values
                else
                {


                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the UpdateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void goButton_Click(object sender, EventArgs e)
        {
            try
            {

                PMOscar.Utility.EnumTypes.Reports SelectedReport = (PMOscar.Utility.EnumTypes.Reports)Enum.Parse(typeof(PMOscar.Utility.EnumTypes.Reports), DropDownListReports.SelectedValue);
                string monthSelected = string.Empty;
                string yearSelected = string.Empty;

                if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Project_Wise_Utilization_Report))    //("project Utilization Report"))
                {
                    gdReport.Visible = true;
                    gdCmpnySummReport.Visible = false;
                    if (rdbMonthly.Checked.Equals(true))
                    {
                        monthSelected = ddlMonth.SelectedItem.Text;
                        yearSelected = ddlYear.SelectedItem.Text;
                        lblReportTitle.Text = "Project Wise Utilization Report - " + monthSelected + " " + yearSelected;

                    }
                    else
                    {
                        string fromDate = hiddenstartWeek.Value;
                        string endDate = hiddenendWeek.Value;
                        lblReportTitle.Text = "Project Wise Utilization Report - " + fromDate + " - " + endDate;
                    }

                    BindGridViewForUtilizationReport("ProjectName", "ASC");
                    btnExport.Enabled = true;


                }
                else if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Team_Wise_Utilization_Report))
                {
                    gdReport.Visible = true;
                    gdCmpnySummReport.Visible = false;
                    if (rdbMonthly.Checked.Equals(true))
                    {
                        monthSelected = ddlMonth.SelectedItem.Text;
                        yearSelected = ddlYear.SelectedItem.Text;
                        lblReportTitle.Text = "Team Wise Utilization Report - " + monthSelected + " " + yearSelected;
                    }
                    else
                    {

                        string fromDate = hiddenstartWeek.Value;
                        string endDate = hiddenendWeek.Value;
                        lblReportTitle.Text = "Team Wise Utilization Report - " + fromDate + " - " + endDate;
                    }

                    BindGridViewForTeamReport("Team", "ASC");
                    btnExport.Enabled = true;
                }
                else if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Resource_Wise_Utilization_Report)) //new report resource wise
                {
                    gdReport.Visible = true;
                    gdCmpnySummReport.Visible = false;
                    if (rdbMonthly.Checked.Equals(true))
                    {
                        monthSelected = ddlMonth.SelectedItem.Text;
                        yearSelected = ddlYear.SelectedItem.Text;
                        lblReportTitle.Text = "Resource Wise Utilization Report - " + monthSelected + " " + yearSelected;
                    }
                    else
                    {

                        string fromDate = hiddenstartWeek.Value;
                        string endDate = hiddenendWeek.Value;
                        lblReportTitle.Text = "Resource Wise Utilization Report - " + fromDate + " - " + endDate;
                    }

                    BindGridViewForResourceReport("Resource", "ASC");
                    btnExport.Enabled = true;


                }
                //else if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Budget_Revision_Report))
                //{
                //    gdReport.Visible = true;
                //    gdCmpnySummReport.Visible = false;
                //    if (rdbMonthly.Checked.Equals(true))
                //    {
                //        monthSelected = ddlMonth.SelectedItem.Text;
                //        yearSelected = ddlYear.SelectedItem.Text;
                //        lblReportTitle.Text = "Budget Revision Report - " + monthSelected + " " + yearSelected;
                //    }
                //    else
                //    {

                //        string fromDate = hiddenstartWeek.Value;
                //        string endDate = hiddenendWeek.Value;
                //        lblReportTitle.Text = "Budget Revision Report - " + fromDate + " - " + endDate;
                //    }

                //    BindGridViewForBudgetRevisionReport();
                //    btnExport.Enabled = true;
                //}
                else if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Company_Utilization_Report))
                {
                    if (rdbSummaryReport.Checked.Equals(true))
                    {
                        gdReport.Visible = false;
                        gdCmpnySummReport.Visible = true;
                        lblReportTitle.Text = "Company Utilization Summary Report ";
                    }
                    else
                    {
                        gdReport.Visible = true;
                        gdCmpnySummReport.Visible = false;
                        monthSelected = ddlMonth.SelectedItem.Text;
                        yearSelected = ddlYear.SelectedItem.Text;
                        lblReportTitle.Text = "Company Utilization Report - " + monthSelected + " " + yearSelected;
                    }

                    BindGridViewForCompanyReport("Date", "ASC");
                    btnExport.Enabled = true;


                }
                else if (SelectedReport.Equals(Utility.EnumTypes.Reports.Open_Hours_Report))
                {
                    gdReport.Visible = false;
                    btnExport.Enabled = false;
                    string fromDate = hiddenstartWeek.Value;
                    string endDate = hiddenendWeek.Value;
                    lblReportTitle.Text = "Open Hours Report - " + fromDate + " - " + endDate;
                }
                else if (SelectedReport.Equals(Utility.EnumTypes.Reports.Open_Hours_Report_With_Break_Up))
                {
                    gdReport.Visible = false;
                    btnExport.Enabled = false;
                    string fromDate = hiddenstartWeek.Value;
                    string endDate = hiddenendWeek.Value;
                    lblReportTitle.Text = "Open Hours Report With Break Up - " + fromDate + " - " + endDate;
                }
                else if (SelectedReport.Equals(Utility.EnumTypes.Reports.Project_Wise_Consolidated_Report))
                {
                    gdReport.Visible = false;
                    btnExport.Enabled = false;
                    lblReportTitle.Text = "Project Wise Consolidated Report";
                }
                else //select 
                {
                    gdCmpnySummReport.Visible = false;
                    lblReportTitle.Text = "Please select a report!!";
                    gdReport.DataSource = null;
                    gdReport.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

        }

        /// <summary>
        /// Gets the sort direction.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "DESC";

            try
            {
                // Retrieve the last column that was sorted.
                string sortExpression = ViewState["SortExpression"] as string;

                if (sortExpression != null)
                {
                    // Check if the same column is being sorted.
                    // Otherwise, the default value can be returned.
                    if (sortExpression == column)
                    {
                        string lastDirection = ViewState["SortDirection"] as string;
                        if ((lastDirection != null) && (lastDirection == "DESC"))
                            sortDirection = "ASC";
                    }
                }

                // Save new values in ViewState.
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = column;

                return sortDirection;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return sortDirection;
            }

        }

        /// <summary>
        /// Bind Company utilization Summary Report
        /// </summary>
        /// <param name="dsCompanyUtilizationDetails">DataSet dsCompanyUtilizationDetails</param>
        private void BindSummaryReport(DataSet dsCompanyUtilizationDetails)
        {
            DataTable dtSummaryTable = dsCompanyUtilizationDetails.Tables[0];
            DateTime reportStartDate = PMOscar.Utility.Utility.CompanyUtiliztionSummartReportStartDate;
            DateTime currentDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 01);

            if (dsCompanyUtilizationDetails.Tables[0].Rows.Count == 0)
            {
                while (reportStartDate <= currentDate)
                {
                    string monthName = reportStartDate.ToString("MMMM");
                    string year = Convert.ToString(reportStartDate.Year);
                    dtSummaryTable = CreateSummaryReportTableRows(dtSummaryTable, reportStartDate, monthName, year);
                    reportStartDate = reportStartDate.AddMonths(1);
                }
                gdCmpnySummReport.DataSource = GetSummaryReportUtilizationPercentage(dtSummaryTable);
                gdCmpnySummReport.DataBind();
            }
            else
            {
                DataTable dtSummaryReport = dtSummaryTable.Clone();
                DataTable dtSummaryReportNew = dtSummaryTable.Clone();

                while (reportStartDate <= currentDate)
                {
                    string getMonth = reportStartDate.ToString("MMMM");
                    string getYear = Convert.ToString(reportStartDate.Year);
                    DataRow[] drArrTemp = dtSummaryTable.Select("Date='" + reportStartDate + "'");
                    if (drArrTemp.Length > 0)
                    {
                        foreach (DataRow temp in drArrTemp)
                        {
                            dtSummaryReportNew.ImportRow(temp);
                        }
                        reportStartDate = reportStartDate.AddMonths(1);
                    }
                    else
                    {
                        dtSummaryReport = CreateSummaryReportTableRows(dtSummaryReport, reportStartDate, getMonth, getYear);
                        reportStartDate = reportStartDate.AddMonths(1);
                    }
                }
                dtSummaryReportNew.Merge(dtSummaryReport);
                dtSummaryReportNew = GetSummaryReportUtilizationPercentage(dtSummaryReportNew);
                dtSummaryReportNew.DefaultView.Sort = "Date ASC";
                gdCmpnySummReport.DataSource = dtSummaryReportNew;
                gdCmpnySummReport.DataBind();
            }
        }

        /// <summary>
        /// Bind Company utilization Summary Report
        /// </summary>
        /// <param name="dsCompanyUtilizationDetails">DataSet dsCompanyUtilizationDetails</param>
        private void BindDetailedReport(DataSet dsCompanyUtilizationDetails)
        {
            DataTable dt = dsCompanyUtilizationDetails.Tables[0];
            
            //get distinct Team
            DataTable dtTeam = dt.DefaultView.ToTable(true, "Team");
            var distinctTeam = dtTeam.AsEnumerable().Distinct(DataRowComparer.Default); //get distinct rows
            List<string> team = new List<string>();
            foreach (DataRow drTeam in distinctTeam)
            {
                //get distinct Team names
                team.Add(drTeam["Team"].ToString());
            }

            DataTable tableFinal = dt;
            if (tableFinal.Rows.Count > 0)
            {
                //add resource count column
                DataColumn utilPercent = tableFinal.Columns.Add("Utilization %");
                utilPercent.AllowDBNull = true;
                utilPercent.Unique = false;

                foreach (string teams in team)
                {
                    //get count
                    DataRow[] rows = tableFinal.Select("Team='" + teams + "'");
                    if (rows.Count() > 0)
                    {
                        for (int i = 0; i < rows.Count(); ++i)
                        {

                            if ((Convert.ToInt32(rows[i]["BilledHours"]) == 0) || (Convert.ToInt32(rows[i]["AvailableHours"]) == 0))
                            {
                                rows[i]["Utilization %"] = "0.00 %";
                            }
                            else
                            {
                                //Utilization Percentage
                                rows[i]["Utilization %"] = Math.Round((Convert.ToDouble(rows[i]["BilledHours"]) / Convert.ToDouble(rows[i]["AvailableHours"])) * 100, 2) + "%";
                            }
                        }
                    }

                }
            }

            gdReport.DataSource = dsCompanyUtilizationDetails;
            gdReport.DataBind();

            MergeRowsForCmpyDetailedReport(gdReport);
        }

        /// <summary>
        /// Get Summary Report By each month
        /// </summary>
        /// <param name="year">int year</param>
        /// <param name="month">int month</param>
        /// <returns></returns>
        private DataSet GetSummaryReportByMonthYear(int year, int month)
        {
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Year", year));
            parameter.Add(new SqlParameter("@Month", month));
            return PMOscar.BaseDAL.ExecuteSPDataSet("[GetSummaryReportByMonthYear]", parameter);
        }

        /// <summary>
        /// Create SummaryReport Table Rows with initial values
        /// </summary>
        /// <param name="dataTableSummaryReport">DataTable dataTableSummaryReport</param>
        /// <param name="date">DateTime date</param>
        /// <param name="month">string month</param>
        /// <param name="year">string year</param>
        /// <returns>DataTable</returns>
        private DataTable CreateSummaryReportTableRows(DataTable dataTableSummaryReport, DateTime date, string month, string year)
        {
            DataTable dtSummaryReport = dataTableSummaryReport;
            DataRow drSummaryReportRow;
            drSummaryReportRow = dtSummaryReport.NewRow();
            drSummaryReportRow["Date"] = date;
            drSummaryReportRow["Month_Year"] = month + " " + year;
            drSummaryReportRow["AvailableHours"] = 0;
            drSummaryReportRow["BilledHours"] = 0;
            drSummaryReportRow["UtilizedButNotBilledHours"] = 0;
            drSummaryReportRow["Admin"] = 0;
            drSummaryReportRow["Open+VAS"] = 0;
            drSummaryReportRow["Proposal"] = 0;
            drSummaryReportRow["Finalize"] = 0;
            dtSummaryReport.Rows.Add(drSummaryReportRow);
            return dtSummaryReport;
        }

        /// <summary>
        /// Insert Summary Report Details to table 
        /// </summary>
        /// <param name="dsSummaryReport">DataSet dsSummaryReport</param>
        private void InsertSummaryReportDetails(DataSet dsSummaryReport)
        {
            int noOfRowsAffected = 0;

            if (dsSummaryReport.Tables.Count != 0)
            {
                DataTable dataTableSummaryReport = dsSummaryReport.Tables[0];
                if (dataTableSummaryReport.Rows.Count != 0)
                {
                    for (int i = 0; i < dataTableSummaryReport.Rows.Count; i++)
                    {
                        List<SqlParameter> parameter = new List<SqlParameter>();
                        parameter.Add(new SqlParameter("@Date", Convert.ToDateTime(dataTableSummaryReport.Rows[i]["Date"])));
                        parameter.Add(new SqlParameter("@ProjectID", Convert.ToInt32(dataTableSummaryReport.Rows[i]["ProjectId"])));
                        //parameter.Add(new SqlParameter("@PhaseID", Convert.ToInt32(dataTableSummaryReport.Rows[i]["PhaseId"])));
                        parameter.Add(new SqlParameter("@ResourceID", Convert.ToInt32(dataTableSummaryReport.Rows[i]["ResourceId"])));
                        parameter.Add(new SqlParameter("@RoleID", Convert.ToInt32(dataTableSummaryReport.Rows[i]["RoleId"])));
                        parameter.Add(new SqlParameter("@TeamID", Convert.ToInt32(dataTableSummaryReport.Rows[i]["TeamId"])));

                        if (!string.IsNullOrEmpty(dataTableSummaryReport.Rows[i]["AvailableHours"].ToString()))
                        {
                            parameter.Add(new SqlParameter("@AvailableHours", Convert.ToInt32(dataTableSummaryReport.Rows[i]["AvailableHours"])));
                        }
                        if (!string.IsNullOrEmpty(dataTableSummaryReport.Rows[i]["BilledHours"].ToString()))
                        {
                            parameter.Add(new SqlParameter("@BilledHours", Convert.ToInt32(dataTableSummaryReport.Rows[i]["BilledHours"])));
                        }
                        parameter.Add(new SqlParameter("@Finalize", false));

                        if (!string.IsNullOrEmpty(dataTableSummaryReport.Rows[i]["Admin"].ToString()))
                        {
                            parameter.Add(new SqlParameter("@Admin", Convert.ToInt32(dataTableSummaryReport.Rows[i]["Admin"])));
                        }
                        if (!string.IsNullOrEmpty(dataTableSummaryReport.Rows[i]["Open"].ToString()))
                        {
                            parameter.Add(new SqlParameter("@Open", Convert.ToInt32(dataTableSummaryReport.Rows[i]["Open"])));
                        }
                        if (!string.IsNullOrEmpty(dataTableSummaryReport.Rows[i]["VAS"].ToString()))
                        {
                            parameter.Add(new SqlParameter("@VAS", Convert.ToInt32(dataTableSummaryReport.Rows[i]["VAS"])));
                        }
                        if (!string.IsNullOrEmpty(dataTableSummaryReport.Rows[i]["Proposal"].ToString()))
                        {
                            parameter.Add(new SqlParameter("@Proposal", Convert.ToInt32(dataTableSummaryReport.Rows[i]["Proposal"])));
                        }
                        if (!string.IsNullOrEmpty(dataTableSummaryReport.Rows[i]["ActualHours"].ToString()))
                        {
                            parameter.Add(new SqlParameter("@ActualHours", Convert.ToInt32(dataTableSummaryReport.Rows[i]["ActualHours"])));
                        }
                        noOfRowsAffected = BaseDAL.ExecuteSPNonQuery("InsertSummaryReportDetails", parameter);
                    }
                }
            }
        }

        /// <summary>
        /// Get SummaryRepor tUtilization Percentage for each month
        /// </summary>
        /// <param name="dtSummaryReportNew">DataTable dtSummaryReportNew</param>
        /// <returns>DataTable</returns>
        private DataTable GetSummaryReportUtilizationPercentage(DataTable dtSummaryReportNew)
        {
            if (dtSummaryReportNew.Rows.Count > 0)
            {
                DataColumn utilPercent = dtSummaryReportNew.Columns.Add("UtilizationPer");
                utilPercent.AllowDBNull = true;
                utilPercent.Unique = false;

                DataRow[] rows = dtSummaryReportNew.Select();
                if (rows.Count() > 0)
                {
                    for (int i = 0; i < rows.Count(); ++i)
                    {
                        try
                        {
                            if ((Convert.ToInt32(rows[i]["BilledHours"]) == 0) || (Convert.ToInt32(rows[i]["AvailableHours"]) == 0))
                            {
                                rows[i]["UtilizationPer"] = "0.00 %";
                            }
                            else
                            {
                                //Utilization Percentage
                                rows[i]["UtilizationPer"] = Math.Round((Convert.ToDouble(rows[i]["BilledHours"]) / Convert.ToDouble(rows[i]["AvailableHours"])) * 100, 2) + "%";
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
            }
            return dtSummaryReportNew;
        }
        #endregion
        #region"Control Events"



        /// <summary>
        /// Handles the Click event of the btnExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                string reportDate = ExtractDateFromTitle(lblReportTitle.Text); //get dates from title
                if (!String.IsNullOrEmpty(reportDate))
                {
                    reportDate = reportDate.Replace(" ", ""); //replace space with "_"
                    string reportName = string.Empty;
                    if (lblReportTitle.Text.Contains("Project")) //Project Wise Report 
                    {
                        reportName = "ProjectWiseUtilizationReport_" + reportDate + ".xls";
                        ExportToExcel(reportName, gdReport);
                    }
                    else if (lblReportTitle.Text.Contains("Team")) //Team Wise Report 
                    {
                        reportName = "TeamWiseUtilizationReport_" + reportDate + ".xls";
                        ExportToExcel(reportName, gdReport);
                    }
                    else if (lblReportTitle.Text.Contains("Resource"))//Resource Wise Report 
                    {
                        reportName = "ResourceWiseUtilizationReport_" + reportDate + ".xls";
                        ExportToExcel(reportName, gdReport);
                    }
                    else //Company utilization Wise Report 
                    {
                        if (lblReportTitle.Text.Contains("Summary")) //Company utilization Wise - Summary Report
                        {
                            reportName = "CompanyUtilizationSummaryReport.xls";
                            ExportToExcel(reportName, gdCmpnySummReport);
                        }
                        else
                        {
                            string cmpyReportDate = ExtractDateFromTitle(lblReportTitle.Text); //Company utilization Wise - Detailed Report
                            if (!String.IsNullOrEmpty(cmpyReportDate))
                            {
                                cmpyReportDate = cmpyReportDate.Replace(" ", ""); //replace space with "_"
                                reportName = "CompanyUtilizationDetailedReport_" + cmpyReportDate + ".xls";
                                ExportToExcel(reportName, gdReport);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

        }

        /// <summary>
        /// Handles the RowDataBound event of the gdReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gdReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int cellCount = e.Row.Cells.Count;
                PMOscar.Utility.EnumTypes.Reports SelectedReport = (PMOscar.Utility.EnumTypes.Reports)Enum.Parse(typeof(PMOscar.Utility.EnumTypes.Reports), DropDownListReports.SelectedValue);

                #region "Project Wise Report Row Data Bound"

                if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Project_Wise_Utilization_Report))
                {

                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        LinkButton LnkHeaderTextProjectName = e.Row.Cells[1].Controls[0] as LinkButton;
                        LnkHeaderTextProjectName.Text = "Project Name";
                        LnkHeaderTextProjectName.ForeColor = System.Drawing.Color.FromName("#fcff00");

                        e.Row.Font.Bold = true;
                        e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                        e.Row.ForeColor = System.Drawing.Color.White;
                        e.Row.Cells[0].Text = "";

                        //header texts
                        e.Row.Cells[2].Text = "Phase"; //this will disable sort capability for Phase column
                        e.Row.Cells[4].Text = "Role"; //this will disable sort capability also for this column
                        e.Row.Cells[5].Text = "Resource Name"; //this will disable sort capability also for this column
                        e.Row.Cells[6].Text = "Billable Hours";
                        e.Row.Cells[7].Text = "Budget Hours";
                        e.Row.Cells[8].Text = "Revised Budget Hours";
                        e.Row.Cells[9].Text = "Actual Hours";

                    }

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        for (int cellsCount = 8; cellsCount < cellCount; cellsCount++)
                        {
                            e.Row.Cells[cellsCount].Width = 150;
                            e.Row.Cells[cellsCount].Attributes.Add("Style", "text-align:left;padding-right:5px;");

                        }


                        //style for project name rows
                        e.Row.Cells[1].Attributes.Add("Style", "text-align:left;padding-right:5px;font-weight:bold;");

                        ////////////////////////////////////////////////////////////
                        //Alternate colors to rows 
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
                        //////////////////////////////////////////////


                        string rowTotal = DataBinder.Eval(e.Row.DataItem, "ResourceName") == DBNull.Value ? "" : (String)DataBinder.Eval(e.Row.DataItem, "ResourceName");
                        if (rowTotal == "Total")
                        {
                            e.Row.Font.Bold = true;
                            e.Row.BackColor = System.Drawing.Color.FromName("#b2d2e9");
                            e.Row.ForeColor = System.Drawing.Color.White;
                            e.Row.Cells[0].Text = "";

                        }

                        if (rowTotal == "Grand Total")
                        {
                            e.Row.Font.Bold = true;
                            e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                            e.Row.ForeColor = System.Drawing.Color.White;
                            e.Row.Cells[1].Text = "";
                            e.Row.Cells[2].Text = "";
                            e.Row.Cells[4].Text = "";

                        }

                    }


                }//project wise report
                #endregion

                #region "Team Wise Report Row Data Bound"

                if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Team_Wise_Utilization_Report))
                {

                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Font.Bold = true;
                        e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                        e.Row.ForeColor = System.Drawing.Color.White;

                        //team name header color change for sorting
                        LinkButton LnkHeaderTeamName = e.Row.Cells[0].Controls[0] as LinkButton;
                        LnkHeaderTeamName.ForeColor = System.Drawing.Color.FromName("#fcff00");

                        //header texts
                        e.Row.Cells[1].Text = "Number of Resources";
                        e.Row.Cells[2].Text = "Resource Name";
                        e.Row.Cells[3].Text = "Planned Hours";
                        e.Row.Cells[4].Text = "Actual Hours";
                        e.Row.Cells[5].Text = "Utilization %";

                    }

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //style for team name and resource name
                        e.Row.Cells[0].Attributes.Add("Style", "text-align:left;padding-right:5px;font-weight:bold;");
                        e.Row.Cells[1].Attributes.Add("Style", "text-align:left;padding-right:5px;font-weight:bold;");

                        //Alternate colors to rows 
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
                        //alternate rows
                        string rowTotal = DataBinder.Eval(e.Row.DataItem, "ResourceName") == DBNull.Value ? "" : (String)DataBinder.Eval(e.Row.DataItem, "ResourceName");
                        if (rowTotal == "Total")
                        {
                            e.Row.Font.Bold = true;
                            e.Row.BackColor = System.Drawing.Color.FromName("#b2d2e9");
                            e.Row.ForeColor = System.Drawing.Color.White;
                            e.Row.Cells[0].Text = "";

                        }
                        string rowGrandTotal = DataBinder.Eval(e.Row.DataItem, "Team") == DBNull.Value ? "" : (String)DataBinder.Eval(e.Row.DataItem, "Team");
                        if (rowGrandTotal == "Grand Total")
                        {
                            e.Row.Font.Bold = true;
                            e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                            e.Row.ForeColor = System.Drawing.Color.White;

                        }

                    }

                }

                #endregion

                #region "Resource Wise Report Row Created"

                if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Resource_Wise_Utilization_Report))
                {

                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Font.Bold = true;
                        e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                        e.Row.ForeColor = System.Drawing.Color.White;

                        //team name header color change for sorting
                        LinkButton LnkHeaderTextResourceName = e.Row.Cells[0].Controls[0] as LinkButton;
                        LnkHeaderTextResourceName.Text = "Resource Name";
                        LnkHeaderTextResourceName.ForeColor = System.Drawing.Color.FromName("#fcff00");

                        //header texts
                        e.Row.Cells[1].Text = "Project Name";
                        e.Row.Cells[2].Text = "Phase";
                        e.Row.Cells[3].Text = "Planned Hours";
                        e.Row.Cells[4].Text = "Actual Hours";

                    }

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //style for team name and resource name
                        e.Row.Cells[0].Attributes.Add("Style", "text-align:left;padding-right:5px;font-weight:bold;");


                        //Alternate colors to rows 
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
                        //alternate rows
                        string rowTotal = DataBinder.Eval(e.Row.DataItem, "Phase") == DBNull.Value ? "" : (String)DataBinder.Eval(e.Row.DataItem, "Phase");
                        if (rowTotal == "Total")
                        {
                            e.Row.Font.Bold = true;
                            e.Row.BackColor = System.Drawing.Color.FromName("#b2d2e9");
                            e.Row.ForeColor = System.Drawing.Color.White;
                            e.Row.Cells[0].Text = "";

                        }

                        if (rowTotal == "Grand Total")
                        {
                            e.Row.Font.Bold = true;
                            e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                            e.Row.ForeColor = System.Drawing.Color.White;
                            //e.Row.Cells[0].Text = "";
                            e.Row.Cells[1].Text = "";

                        }


                    }


                }

                #endregion

                #region "Company Wise Utilization Detailed Report Row Data Bound"

                if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Company_Utilization_Report))
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Font.Bold = true;
                        e.Row.BackColor = System.Drawing.Color.FromName("#539cd1");
                        e.Row.ForeColor = System.Drawing.Color.White;

                        //header texts
                        e.Row.Cells[0].Text = "Team";
                        e.Row.Cells[1].Text = "Role"; 
                        e.Row.Cells[2].Text = "RoleId";
                        e.Row.Cells[3].Text = "Available";
                        e.Row.Cells[4].Text = "Billed";
                        e.Row.Cells[5].Text = "Utilized But Not Billed";
                        e.Row.Cells[6].Text = "Admin";
                        e.Row.Cells[7].Text = "Open+VAS";
                        e.Row.Cells[8].Text = "Proposal";
                        e.Row.Cells[9].Text = "Utilization %";
                    }

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        for (int cellsCount = 3; cellsCount < cellCount; cellsCount++)
                        {
                            e.Row.Cells[cellsCount].Width = 150;
                            e.Row.Cells[cellsCount].Attributes.Add("Style", "text-align:Right;padding-right:25px;");

                        }

                        e.Row.Cells[0].Attributes.Add("Style", "text-align:left;padding-right:25px;");
                        e.Row.Cells[1].Attributes.Add("Style", "text-align:left;padding-right:25px;");
                        ////////////////////////////////////////////////////////////
                        //Alternate colors to rows 
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
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }


        /// <summary>
        /// Handles the RowCreated event of the gdReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gdReport_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {

                PMOscar.Utility.EnumTypes.Reports SelectedReport = (PMOscar.Utility.EnumTypes.Reports)Enum.Parse(typeof(PMOscar.Utility.EnumTypes.Reports), DropDownListReports.SelectedValue);

                #region "Prject Wise Report Row Created"

                //cutomize columns 
                if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Project_Wise_Utilization_Report))
                {
                    if (e.Row.Cells.Count > 1)
                    {

                        e.Row.Cells[3].Visible = false;  //projectid
                        e.Row.Cells[10].Visible = false;  //phaseid
                    }
                }

                #endregion

                #region "Team Wise Report Row Created"
                //cutomize columns 
                if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Team_Wise_Utilization_Report))
                {

                    if (e.Row.Cells.Count > 1)
                    {
                        //e.Row.Cells[1].Visible = false; //resource name

                    }

                }
                #endregion

                #region "Resource Wise Report Row Created"
                //cutomize columns 
                if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Resource_Wise_Utilization_Report))
                {

                    if (e.Row.Cells.Count > 1)
                    {
                        e.Row.Cells[5].Visible = false; //resourceid
                        e.Row.Cells[6].Visible = false; //projectid
                        e.Row.Cells[7].Visible = false; //count of time tracker entry
                        e.Row.Cells[8].Visible = false; //phaseid
                    }

                }
                #endregion

                #region "Company Utilization Detailed Report Row Created"
                if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Company_Utilization_Report))
                {
                    if (e.Row.Cells.Count > 1)
                    {
                        e.Row.Cells[2].Visible = false;  //RoleId
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

        }

        /// <summary>
        /// Handles the Sorting event of the gdReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void gdReport_Sorting(object sender, GridViewSortEventArgs e)
        {

            try
            {

                PMOscar.Utility.EnumTypes.Reports SelectedReport = (PMOscar.Utility.EnumTypes.Reports)Enum.Parse(typeof(PMOscar.Utility.EnumTypes.Reports), DropDownListReports.SelectedValue);
                string sortExpression = e.SortExpression;
                string sortDirection = GetSortDirection(e.SortExpression);

                if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Project_Wise_Utilization_Report))
                {
                    BindGridViewForUtilizationReport(sortExpression, sortDirection);
                }
                else if (SelectedReport.Equals(PMOscar.Utility.EnumTypes.Reports.Team_Wise_Utilization_Report))
                {
                    BindGridViewForTeamReport(sortExpression, sortDirection);
                }
                else //resource wise report
                {
                    BindGridViewForResourceReport(sortExpression, sortDirection);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

        }

        /// <summary>
        /// Handles the Index changed event of ddlReports control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void ddlReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            gdCmpnySummReport.Visible = false;
            gdReport.Visible = false;
            lblReportTitle.Text = string.Empty;
            btnExport.Visible = true;
            rdbMonthly.Disabled = false;
            tdopenHoursReport.Visible = false;
            int ddlReportsSelectedValue = Convert.ToInt32(DropDownListReports.SelectedValue);
            if (ddlReportsSelectedValue == Convert.ToInt32(PMOscar.Utility.EnumTypes.Reports.Company_Utilization_Report))
            {
                tdOtherReports.Visible = false;
                tdCompanyUtilizationReport.Visible = true;
            }
            else if (ddlReportsSelectedValue == Convert.ToInt32(Utility.EnumTypes.Reports.Open_Hours_Report)
                || ddlReportsSelectedValue == Convert.ToInt32(Utility.EnumTypes.Reports.Open_Hours_Report_With_Break_Up))
            {
                btnExport.Visible = false;
                tdOtherReports.Visible = false;
                tdCompanyUtilizationReport.Visible = false;
                tdopenHoursReport.Visible = true;
            }
            else if (ddlReportsSelectedValue == Convert.ToInt32(Utility.EnumTypes.Reports.Project_Wise_Consolidated_Report))
            {
                rdbMonthly.Disabled = true;
                btnExport.Visible = false;
                tdOtherReports.Visible = false;
                tdCompanyUtilizationReport.Visible = false;
            }
            else
            {
                tdOtherReports.Visible = true;
                tdCompanyUtilizationReport.Visible = false;
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the gdCmpnySummReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void gdCmpnySummReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkRefresh = (LinkButton)e.Row.FindControl("lnkRefresh");
                LinkButton lnkFinalize = (LinkButton)e.Row.FindControl("lnkFinalize");
                bool finalize = Convert.ToBoolean(gdCmpnySummReport.DataKeys[e.Row.RowIndex].Values[1].ToString());
                if (Convert.ToInt16(Session["UserRoleID"]) != (int)PMOscar.Utility.EnumTypes.UserRoles.ProjectOwner) // Checking role of user by UserRoleID ( 1 = ProjectOwner , 2 = ProjectManager )
                {
                    lnkFinalize.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Handles the RowCommand event of the gdCmpnySummReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void gdCmpnySummReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataSet dsSummaryReport;
                GridViewRow row = ((Control)(e.CommandSource)).NamingContainer as GridViewRow;
                DateTime date = Convert.ToDateTime(gdCmpnySummReport.DataKeys[row.RowIndex].Values[0]);
                int year = date.Year;
                int month = date.Month;

                if (e.CommandName.Equals(commandName_Refresh))
                {
                    dsSummaryReport = GetSummaryReportByMonthYear(year, month);
                    List<SqlParameter> parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@Year", year));
                    parameter.Add(new SqlParameter("@Month", month));
                    int affectedRow = BaseDAL.ExecuteSPNonQuery("DeleteSummaryReportDetails", parameter);
                    InsertSummaryReportDetails(dsSummaryReport);
                    BindGridViewForCompanyReport("Date", "ASC");
                }
                else if (e.CommandName.Equals(commandName_Finalize))
                {
                    dsSummaryReport = GetSummaryReportByMonthYear(year, month);
                    List<SqlParameter> parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@Year", year));
                    parameter.Add(new SqlParameter("@Month", month));
                    int affectedRow = BaseDAL.ExecuteSPNonQuery("DeleteSummaryReportDetails", parameter);
                    InsertSummaryReportDetails(dsSummaryReport);

                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@Year", year));
                    parameter.Add(new SqlParameter("@Month", month));                  
                    int noOfRowsAffected = BaseDAL.ExecuteSPNonQuery("FinalizeSummaryReportDetails", parameter);
                    if (noOfRowsAffected > 0)
                    {
                        BindGridViewForCompanyReport("Date", "ASC");
                    }
                }
            }
            catch (Exception exx)
            {
                Logger.Error(exx.Message, exx);
            }
        }
        #endregion

    }
}
