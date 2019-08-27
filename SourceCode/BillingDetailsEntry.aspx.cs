
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
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Globalization;
using PMOscar.DAL;
using PMOscar.Core;
using System.Web.Services;

namespace PMOscar
{
    public partial class BillingDetailsEntry : System.Web.UI.Page
    {
        bool isValid = false;

        #region"Declarations"
        public IList<SqlParameter> parameter;
        private string ResourceEditId;
        private int roleId;
        public int role;
        private int year;
        private int projectId;
        private int monthId;
        private int editid = 0;
        private int iFlag = 0;
        int index = 0;
        int bid = 0;
        string resourceName = string.Empty;
        string resourceId = string.Empty;
        string Role = string.Empty;
        string fromDate = string.Empty;
        string toDate = string.Empty;
        string planned = string.Empty;
        string actual = string.Empty;
        string comments = string.Empty;
        string actualSpent = string.Empty;
        string roleID = string.Empty;
        string projectID = string.Empty;
        string monthName = string.Empty;
        string projectName = string.Empty;
        bool finalize = false;
        private BillingDetailsDAL objBillingDAL = new BillingDetailsDAL();

        #endregion

        #region "Page load"
        protected void Page_Load(object sender, EventArgs e)
        {
           
            ddlRole.Enabled = true;
            txtActualSpent.Enabled = false;
            if (Session["UserName"] == null)
                Response.Redirect("Default.aspx");
            else
            {
                lblResourceExistMessage.Text = string.Empty;
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

                trProjectBilling.Visible = true;
                int selectedMonth = Convert.ToInt32(Request.QueryString["month"]);
                int selectedYear = Convert.ToInt32(Request.QueryString["year"]);
                DataSet dsCompanyUtilizationDetails;
                List<SqlParameter> parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@Year", selectedYear));
                parameter.Add(new SqlParameter("@Month", selectedMonth));
                dsCompanyUtilizationDetails = PMOscar.BaseDAL.ExecuteSPDataSet("[GetCompanyUtilization_SummaryByMonthYear]", parameter);
                if (dsCompanyUtilizationDetails.Tables.Count != 0)
                {
                    DataTable dataTableSummaryReport = dsCompanyUtilizationDetails.Tables[0];
                    if (dataTableSummaryReport.Rows.Count != 0)
                    {
                        finalize = Convert.ToBoolean(dataTableSummaryReport.Rows[0]["Finalize"].ToString());
                        if (finalize)
                        {
                            trProjectBilling.Visible = false;
                        }
                        else
                        {
                            trProjectBilling.Visible = true;
                        }
                    }
                }

            }
            try
            {

                mnthLabel.Value = Request.QueryString["month"] + '/' + Request.QueryString["year"];
                projectId = Request.QueryString["projectId"] != null ? Convert.ToInt32(Request.QueryString["projectId"]) : 0;
                hiddenProjectID.Value = projectId.ToString();
                if (!Page.IsPostBack)
                {
                    BindResourceDropDown(); // Method to bind projects in the DropDownList...
                    BindRoleDropDown();

                    index = Convert.ToInt16(Request.QueryString["month"]);
                    monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(index);

                    lblmonth.Text = monthName + " " + Request.QueryString["year"];
                    year = Request.QueryString["year"] != null ? Convert.ToInt32(Request.QueryString["year"]) : 0;

                    editid = Request.QueryString["ResourceEditId"] != null ? Convert.ToInt16(Request.QueryString["ResourceEditId"]) : 0;


                    monthId = Request.QueryString["month"] != null ? Convert.ToInt32(Request.QueryString["month"]) : 0;

                    roleId = Request.QueryString["RoleId"] != null ? Convert.ToInt32(Request.QueryString["RoleId"]) : 0;

                    // Set the selected month's start date and end date as default in From and To fields.
                    DateTime firstDayOfTheMonth = new DateTime(year, monthId, 1);
                    DateTime lastDayOfMonth = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
                    txtFromDate.Text = firstDayOfTheMonth.ToString("dd/MM/yyy");
                    txtToDate.Text = lastDayOfMonth.ToString("dd/MM/yyy");
                }

                lblerror.Visible = false;
                lblError2.Visible = false;
                ddlResources.Focus();
                string vResourceId = ddlResources.SelectedItem.Value.ToString();
                ResourceEditId = vResourceId;

                if (Request.QueryString["opmode"] == "Edit")  //display values on textbox on edit
                {

                    if (!Page.IsPostBack)
                    {
                        lblmonth.Text = monthName + " " + Request.QueryString["year"];
                        DataSet dsResource = objBillingDAL.GetResourcenameId(editid);
                        DataSet dsResourceDetails = objBillingDAL.GetbillingDetails(editid, Request.QueryString["BillingEditID"], projectId, monthId, year);
                        if (dsResource.Tables[0].Rows.Count >= 1)
                        {
                            resourceName = dsResource.Tables[0].Rows[0].ItemArray[0].ToString();
                            resourceId = dsResource.Tables[0].Rows[0].ItemArray[1].ToString();
                        }

                        if (dsResourceDetails.Tables[0].Rows.Count >= 1)
                        {
                            Role = dsResourceDetails.Tables[0].Rows[0].ItemArray[0].ToString();
                            fromDate = dsResourceDetails.Tables[0].Rows[0].ItemArray[1].ToString();
                            toDate = dsResourceDetails.Tables[0].Rows[0].ItemArray[2].ToString();
                            planned = dsResourceDetails.Tables[0].Rows[0].ItemArray[3].ToString();
                            actual = dsResourceDetails.Tables[0].Rows[0].ItemArray[4].ToString();
                            actualSpent = dsResourceDetails.Tables[0].Rows[0].ItemArray[11].ToString();
                            comments = dsResourceDetails.Tables[0].Rows[0].ItemArray[12].ToString();
                            roleID = dsResourceDetails.Tables[0].Rows[0].ItemArray[9].ToString();
                            projectID = dsResourceDetails.Tables[0].Rows[0].ItemArray[8].ToString();
                            lblProject.Text = dsResourceDetails.Tables[0].Rows[0].ItemArray[7].ToString();
                            if (dsResourceDetails.Tables[0].Rows[0].ItemArray[10].ToString() == "Yes")
                            {
                                chkValue.Checked = true;
                                //rfvResources.Enabled = false;
                            }

                        }

                        planned = string.IsNullOrEmpty(planned) ? "0" : planned;
                        actualSpent = string.IsNullOrEmpty(actualSpent) ? "0" : actualSpent;

                        ddlResources.SelectedValue = resourceId.ToString().Trim();
                        ddlRole.SelectedValue = roleId.ToString().Trim();
                        txtFromDate.Text = fromDate.ToString();
                        txtToDate.Text = toDate.ToString();
                        txtPlanned.Text = planned;
                        txtActual.Text = actual.ToString();
                        txtActualSpent.Text = actualSpent;
                        txtComments.Text = comments.ToString();
                        btnAdd.Text = "Update";
                        ddlResources.Enabled = true;
                        txtAvailableHour.Text = (Convert.ToDecimal(planned) - Convert.ToDecimal(actualSpent)).ToString();
                        DisplayResource();
                    }
                }
                else if (Request.QueryString["opmode"] == "Delete")
                {
                    if (!Page.IsPostBack)
                    {
                        DeleteResource();
                        Response.Redirect("BillingDetailsEntry.aspx?month=" + Request.QueryString["month"] + "&year=" + Request.QueryString["year"] + "&projectId=" + projectId);
                    }
                }

                else
                {

                    DataSet dsTimes2 = objBillingDAL.GetProjectNameById(projectId);

                    if (dsTimes2.Tables[0].Rows.Count >= 1)
                    {
                        projectName = dsTimes2.Tables[0].Rows[0].ItemArray[0].ToString();

                    }
                    lblProject.Text = projectName;
                    year = Convert.ToInt32(Request.QueryString["year"]);
                    role = Convert.ToInt32(ddlRole.SelectedValue.ToString());
                    DisplayResourcedetails();


                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception caught on page Load", ex);
            }


        }
        #endregion

        #region "ClearData"
        /// <summary>
        /// Clears the details.
        /// </summary>
        private void ClearDetails()
        {
            ddlResources.SelectedIndex = 0;
            ddlRole.SelectedIndex = 0;
            // Set the selected month's start date and end date as default in From and To fields.
            DateTime firstDayOfTheMonth = new DateTime(year, monthId, 1);
            DateTime lastDayOfMonth = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
            txtFromDate.Text = firstDayOfTheMonth.ToString("dd/MM/yyy");
            txtToDate.Text = lastDayOfMonth.ToString("dd/MM/yyy");
            txtPlanned.Text = "";
            txtActual.Text = "0";
            txtActualSpent.Text = "0";
            txtAvailableHour.Text = "0";
            txtComments.Text = "";
            chkValue.Checked = false;
        }
        #endregion

        #region "Delete billing details by resource"
        /// <summary>
        /// Deletes the Billing details by resource
        /// </summary>
        protected void DeleteResource()
        {
            try
            {
                objBillingDAL.InsertIntoBillingDetailsAudit(Request.QueryString["BillingEditID"]);
                DataSet dsDeleteId = objBillingDAL.DeleteBillingDetails(Request.QueryString["BillingEditID"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region "Add/Edit"
        /// <summary>
        /// Handles the Click event of the Add/Edit button click
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ResourceValidation.Visible = false;
            roleValidation.Visible = false;
            billableValidation.Visible = false;
            actualbillingValidator.Visible = false;
            actualValidator.Visible = false;
            try
            {
                if(ddlResources.SelectedValue =="0"&&!chkValue.Checked)
                {
                    ResourceValidation.Visible = true;
                    isValid = true;
                }
                if(ddlRole.SelectedValue=="0")
                {
                    roleValidation.Visible = true;
                    isValid = true;
                }
                if(txtPlanned.Text==string.Empty)
                {
                    billableValidation.Visible = true;
                    isValid = true;
                }
                if(txtActual.Text==string.Empty)
                {
                    actualbillingValidator.Visible = true;
                    isValid = true;
                }
                if(txtActualSpent.Text==string.Empty)
                {
                    actualValidator.Visible = true;
                    isValid = true;
                }
                if(isValid)
                {
                    return;
                }


                int id = Convert.ToInt16(Request.QueryString["ResourceEditId"]);
                int role = Convert.ToInt16(Request.QueryString["RoleId"]);
                year = Convert.ToInt16(Request.QueryString["Year"]);
                projectId = Convert.ToInt16(Request.QueryString["ProjectId"]);
                monthId = Convert.ToInt16(Request.QueryString["Month"]);
                string fromdate = txtFromDate.Text;
                string toDate = txtToDate.Text;

                int plannedBilling =Convert.ToInt16(txtPlanned.Text.Trim());
                string query = string.Format("select sum(BillableHours) from ProjectEstimation where ProjectID = '{0}'", Request.QueryString["ProjectId"]);
                int billable = Convert.ToInt16(PMOscar.BaseDAL.ExecuteScalar(query));
                query = string.Format("select sum(PlannedHours) from BillingDetails where projectID = '{0}'", Request.QueryString["ProjectId"]);
                string query2 = string.Format("select PlannedHours from BillingDetails where ResourceID = '{0}' and roleid = '{1}' and ProjectID = '{2}' and month(fromDate) = '{3}' and year(fromdate) = '{4}'", id,role,projectId,monthId,year);
                var plannsum = PMOscar.BaseDAL.ExecuteScalar(query);
                var planresource = PMOscar.BaseDAL.ExecuteScalar(query2);
                int plannedsum = 0;
                int plannedbyresource = 0;
                if(!(planresource is DBNull || planresource == null))
                {
                    plannedbyresource = Convert.ToInt16(planresource);
                }
                   
                if (!(plannsum is DBNull || plannsum == null))
                {
                    plannedsum = Convert.ToInt16(plannsum);
                }
                int totalplanned = 0;
                lblvalidationMsg.Text = string.Empty;
                totalplanned = (plannedsum - plannedbyresource) + plannedBilling;
                if(totalplanned > billable)
                {
                    lblvalidationMsg.Visible = true;
                    lblvalidationMsg.Text = "Planned Billing should not exceed project billable hours.Please update the project billable hours.";
              
                    
                }

                int actualBilling = Convert.ToInt16(txtActual.Text.Trim());
                lblvalidationMsg2.Text = string.Empty;
                if (plannedBilling != 0)
                {
                    if (actualBilling > plannedBilling)
                    {
                        lblvalidationMsg2.Visible = true;
                        lblvalidationMsg2.Text = "Actual Billing should not exceed Planned Billing.Please update the Actual Billing.";
                     
                    }
                }

                string fromDateMonth = fromdate.Substring(3, 2);
                string toDateMonth = toDate.Substring(3, 2);
                string toMonthSelected = toDateMonth.TrimStart('0');

                int dayFromDate = Convert.ToInt32(fromdate.Substring(0, 2));
                int dayToDate = Convert.ToInt32(toDate.Substring(0, 2));

                string yearFromDate = fromdate.Substring(6, 4);
                string yearToDate = toDate.Substring(6, 4);

                string monthSelected = fromDateMonth.TrimStart('0');

                IFormatProvider culture1 = new CultureInfo("fr-Fr", true);
                DateTime date1 = Convert.ToDateTime(txtFromDate.Text, culture1);
                DateTime date2 = Convert.ToDateTime(txtToDate.Text, culture1);               
              

                if (yearFromDate != Request.QueryString["year"].Trim())
                {
                    lblerror.Visible = true;
                    lblerror.Text = "Incorrect year selected";

                }
                else if (yearToDate != Request.QueryString["year"].Trim())
                {
                    lblError2.Visible = true;
                    lblError2.Text = "Incorrect year selected";
                }
                else if ((yearFromDate != Request.QueryString["year"].Trim()) && (yearToDate != Request.QueryString["year"].Trim()))
                {
                    dvError.Visible = true;
                    lblerror.Visible = true;
                    lblerror.Text = "Incorrect year selected";
                    dvError2.Visible = true;
                    lblError2.Visible = true;
                    lblError2.Text = "Incorrect year selected";
                }
                else
                {
                    if (monthSelected != Request.QueryString["month"].Trim())
                    {
                        dvError.Visible = true;
                        lblerror.Visible = true;
                        lblerror.Text = "Incorrect month selected";

                    }
                    else if (toMonthSelected != Request.QueryString["month"].Trim())
                    {
                        dvError2.Visible = true;
                        lblError2.Visible = true;
                        lblError2.Text = "Incorrect month selected";
                    }
                    else if ((monthSelected != Request.QueryString["month"].Trim()) && (toMonthSelected != Request.QueryString["month"].Trim()))
                    {
                        dvError.Visible = true;
                        lblerror.Visible = true;
                        lblerror.Text = "Incorrect Month Selected";
                        dvError2.Visible = true;
                        lblError2.Visible = true;
                        lblError2.Text = "Incorrect Month Selected";
                    }

                    else
                    {

                        if (date1.CompareTo(date2) > 0)
                        {
                            dvError2.Visible = true;
                            lblError2.Visible = true;
                            lblError2.Text = "From date greater than To Date";
                        }

                        else
                        {
                            dvError2.Visible = false;
                            dvError.Visible = false;
                            lblerror.Visible = false;
                            lblError2.Visible = false;

                            string idEdit = id.ToString();
                            string idRole = role.ToString();

                            string y = string.Empty;
                            string y1 = "0";

                            if (btnAdd.Text == "Update")
                            {

                                if (chkValue.Checked)
                                {
                                    iFlag = 1;
                                    //rfvResources.Enabled = false;
                                }
                                if (Convert.ToDecimal(txtActual.Text) > 0)
                                {

                                    bool validationNeeded = !chkValue.Checked;
                                    bool checkExistence = true;
                                    //To check whether validation is needed when an unplanned Resource is added
                                    if (validationNeeded)
                                        checkExistence = objBillingDAL.CheckResourceExistsORNot(projectId, year, monthId, Convert.ToInt32(ddlResources.SelectedValue.ToString()), Convert.ToInt32(ddlRole.SelectedValue.ToString()));
                                    //Check if an allocation exists in resource planning for the given project,month and year with roleId.
                                    if (checkExistence)
                                    {
                                        string billeditid = string.Empty;
                                        billeditid = Request.QueryString["BillingEditID"] != null ? Request.QueryString["BillingEditID"] : string.Empty;

                                        //Check Duplicate entry in BillingDetails And Count of BillingDetails by each Resource Role and Project
                                        if (BillingDetailsCheckDuplicateAndCount(id, projectId, role, billeditid, monthId, year))
                                        {
                                            InsertOrUpdateBillingDetailsEntry(projectId, id, role, "Edit", billeditid, iFlag);
                                        }
                                    }
                                    else
                                    {
                                        lblResourceExistMessage.Text = "This cannot be done because either this resource doesn't have time allocation at all or for the specific role in Resource Planning or the selected resource is not billable. Please correct it and try again.";
                                    }
                                }
                                else
                                {
                                    string billeditid = string.Empty;
                                    billeditid = Request.QueryString["BillingEditID"] != null ? Request.QueryString["BillingEditID"] : string.Empty;

                                    // Check Duplicate entry in BillingDetails And Count of BillingDetails by each Resource Role and Project
                                    if (BillingDetailsCheckDuplicateAndCount(id, projectId, role, billeditid, monthId, year))
                                    {
                                        InsertOrUpdateBillingDetailsEntry(projectId, id, role, "Edit", billeditid, iFlag);
                                    }
                                }
                            }

                            else if (btnAdd.Text == "Add")
                            {
                                if (chkValue.Checked)
                                {
                                    iFlag = 1;
                                    //rfvResources.Enabled = false;
                                }
                                    
                                if (Convert.ToDecimal(txtActual.Text) > 0)
                                {
                                    bool validationNeeded = !chkValue.Checked;
                                    bool checkExistence = true;
                                    if (validationNeeded)
                                        checkExistence = objBillingDAL.CheckResourceExistsORNot(projectId, year, monthId, Convert.ToInt32(ddlResources.SelectedValue.ToString()), Convert.ToInt32(ddlRole.SelectedValue.ToString()));


                                    //Check if an allocation exists in resource planning for the given project,month and year with roleId.
                                    if (checkExistence)
                                    {
                                        int countOfResource = objBillingDAL.CountofBillingDetailsbyResourceRoleProject(ddlResources.SelectedValue.ToString(), projectId, Convert.ToInt32(ddlRole.SelectedValue.ToString()), monthId, year);

                                        if (countOfResource == 1)
                                        {
                                            dvresource.Visible = true;
                                            lblResource.Visible = true;

                                        }
                                        else
                                        {

                                            InsertOrUpdateBillingDetailsEntry(projectId, id, role, "Add", "0", iFlag);

                                        }
                                    }
                                    else
                                    {


                                        lblResourceExistMessage.Text = "This cannot be done because either this resource doesn't have time allocation at all or for the specific role in Resource Planning or the selected resource is not billable. Please correct it and try again.";

                                    }

                                }
                                else
                                {
                                    int countOfResource = objBillingDAL.CountofBillingDetailsbyResourceRoleProject(ddlResources.SelectedValue.ToString(), projectId, Convert.ToInt32(ddlRole.SelectedValue.ToString()), monthId, year);

                                    if (countOfResource == 1)
                                    {
                                        dvresource.Visible = true;
                                        lblResource.Visible = true;

                                    }
                                    else
                                    {
                                        InsertOrUpdateBillingDetailsEntry(projectId, id, role, "Add", "0", iFlag);
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception caught on insert or edit", ex);
            }

        }
        #endregion

        #region"DisplayDetails after Add"
        private void DisplayResourcedetails()
        {
            try
            {
                DataSet dsProjectDetails = objBillingDAL.GetBillingDetailsByResourceID(ddlResources.SelectedItem.Value.ToString(), Convert.ToInt32(ddlRole.SelectedValue.ToString()), projectId, Request.QueryString["month"], Request.QueryString["year"]);
                Session["dgResourceDetails"] = dsProjectDetails;
                dgResourceDetails.DataSource = dsProjectDetails.Tables[0] as DataTable;
                dgResourceDetails.DataBind();
                SumOfPlannedActualBilling();               
            }
            catch (Exception ex)
            {
                Logger.Error("An exception caught on binding grid", ex);
            }
        }

        private void SumOfPlannedActualBilling()
        {
            monthId = Request.QueryString["month"] != null ? Convert.ToInt32(Request.QueryString["month"]) : 0;
            DataSet dsTimes = objBillingDAL.GetSumOfPlannedActualBilling(monthId, year, projectId);

            string plannedTotal = string.Empty;
            string actualTotal = string.Empty;

            if (dsTimes.Tables[0].Rows.Count >= 1)
            {
                plannedTotal = dsTimes.Tables[0].Rows[0].ItemArray[0].ToString();
                actualTotal = dsTimes.Tables[0].Rows[0].ItemArray[1].ToString();

            }
            lblTotalPlanned.Text = plannedTotal;
            lblTotalActual.Text = actualTotal;

            if (lblTotalPlanned.Text != string.Empty)
            {
                lblHidden.Visible = true;
                lblHidden.Text = "hours";
                lblHours.Visible = true;
                lblHours.Text = "hours";
            }
        }
        #endregion

        #region"Display after Edit details"
        private void DisplayResource()
        {
            try
            {
                DataSet dsProjectDetails = objBillingDAL.GetBillingDetailsByResourceID(ddlResources.SelectedItem.Value.ToString(), Convert.ToInt32(ddlRole.SelectedValue.ToString()), projectId, Request.QueryString["month"], Request.QueryString["year"]);

                dgResourceDetails.DataSource = dsProjectDetails.Tables[0] as DataTable;
                dgResourceDetails.DataBind();

                DataSet dsTimes = objBillingDAL.GetSumOfPlannedActualBilling(monthId, year, projectId);
                string plannedTotal = string.Empty;
                string actualTotal = string.Empty;

                if (dsTimes.Tables[0].Rows.Count >= 1)
                {
                    plannedTotal = dsTimes.Tables[0].Rows[0].ItemArray[0].ToString();
                    actualTotal = dsTimes.Tables[0].Rows[0].ItemArray[1].ToString();
                }
                lblTotalPlanned.Text = plannedTotal;
                lblTotalActual.Text = actualTotal;
                if (lblTotalPlanned.Text != string.Empty)
                {
                    lblHidden.Visible = true;
                    lblHidden.Text = "hours";
                    lblHours.Visible = true;
                    lblHours.Text = "hours";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception caught on binding grid", ex);
            }

        }
        #endregion

        #region"Back to addbillingdetails page"
        protected void lbBack_Click(object sender, EventArgs e)
        {
            try
            {
                year = Convert.ToInt32(Request.QueryString["year"]);
                Response.Redirect("AddBillingDetails.aspx?month=" + Request.QueryString["month"].ToString() + "&year=" + year + "&projectId=" + Request.QueryString["projectId"].ToString(),false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region"resource and role binding"
        private void BindResourceDropDown()
        {
            projectId = Request.QueryString["projectId"] != null ? Convert.ToInt32(Request.QueryString["projectId"]) : 0;
            if (projectId != 0)
            {
                DataTable dt = PMOscar.BaseDAL.ExecuteDataTable("select ResourceName,r.ResourceId from ProjectResources p join resource r on r.ResourceId = p.ResourceID where ProjectID =" + projectId + "order by ResourceName");
                ddlResources.DataSource = dt;
                ddlResources.DataTextField = "ResourceName";
                ddlResources.DataValueField = "ResourceId";
                ddlResources.DataBind();
                ddlResources.Items.Insert(0, new ListItem("Select Resource", "0"));
            }
        }

        private void BindRoleDropDown()
        {

            DataTable dtRoles = objBillingDAL.GetRole();
            ddlRole.DataSource = dtRoles;
            ddlRole.DataTextField = "Role";
            ddlRole.DataValueField = "RoleId";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("Select Role", "0"));
        }
        #endregion

        #region"resource selected index change"
        protected void ddlResources_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlResources.Text = ddlResources.SelectedItem.Value;
        }
        #endregion

        #region "giving padding to data coulmns in grid"
        double totalPlannedBillable = 0, totalActualBillable = 0, totalActualSpent = 0;

        protected void Grview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.DataItem != null)
                    {

                        e.Row.Cells[4].Attributes.Add("Style", "text-align:right;padding-right:15px;");
                        e.Row.Cells[5].Attributes.Add("Style", "text-align:right;padding-right:15px;");
                        e.Row.Cells[6].Attributes.Add("Style", "text-align:right;padding-right:15px;");
                        e.Row.Cells[7].Attributes.Add("Style", "text-align:right;padding-right:15px;");

                        totalPlannedBillable += Convert.ToDouble(e.Row.Cells[4].Text.ToString() == "" ? "0" : e.Row.Cells[4].Text.ToString());
                        totalActualBillable += Convert.ToDouble(e.Row.Cells[5].Text.ToString() == "" ? "0" : e.Row.Cells[5].Text.ToString());
                        totalActualSpent += Convert.ToDouble(e.Row.Cells[6].Text.ToString() == "" ? "0" : e.Row.Cells[6].Text.ToString());

                    }

                    if (finalize)
                    {
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[7].Visible = false;
                    }
                    else
                    {
                        e.Row.Cells[6].Visible = true;
                        e.Row.Cells[7].Visible = true;
                    }

                    //Format the Comments column
                    HyperLink ActLink14 = new HyperLink();
                    ActLink14.Text = Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[8].Text)).ToString().Length > 19 ? Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[8].Text)).Substring(0, 20) + "..." : e.Row.Cells[8].Text == "&nbsp;" ? "" : Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[8].Text));
                    e.Row.Cells[8].Controls.Add(ActLink14);

                    if (e.Row.Cells[8].Text != "&nbsp;")
                    {
                        e.Row.Cells[8].ToolTip = Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[8].Text)).Trim();
                        if (e.Row.Cells[8].Text.ToString() != "")
                        {

                            ActLink14.Attributes.Add("onclick", "showPopup('" + Server.HtmlDecode(Server.HtmlDecode(e.Row.Cells[8].Text)).Replace("\r\n", "<br/>").Replace("'", "\"\"").Trim() + "', event);return false;");
                        }
                    }

                }
                else if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (finalize)
                    {
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[7].Visible = false;
                    }
                    else
                    {
                        e.Row.Cells[6].Visible = true;
                        e.Row.Cells[7].Visible = true;
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer) // Applying blank spaces if the values are zero
                {

                    e.Row.Cells[4].Text = totalPlannedBillable.ToString();
                    e.Row.Cells[5].Text = totalActualBillable.ToString();
                    e.Row.Cells[6].Text = totalActualSpent.ToString();

                    e.Row.Cells[3].Text = "Total";
                    e.Row.Cells[3].Attributes.Add("style", "text-align: center;");
                    e.Row.Cells[4].Attributes.Add("style", "text-align: right;padding-right:15px;");
                    e.Row.Cells[5].Attributes.Add("style", "text-align: right;padding-right:15px;");
                    e.Row.Cells[6].Attributes.Add("style", "text-align: right;padding-right:15px;");
                    e.Row.Cells[7].Attributes.Add("style", "text-align: right;padding-right:15px;");
                }

            }
            catch (Exception ex)
            {
                Logger.Error("An exception on giving style to grid", ex);
            }
        }
        #endregion

        #region "BillingDetailsCheckDuplicateAndCount"

        /// <summary>
        /// Check Duplicate entry in BillingDetails And Count of BillingDetails by each Resource Role and Project
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="projectId">projectId</param>
        /// <param name="role">role</param>
        /// <param name="billeditid">billeditid</param>
        /// <param name="monthId">monthId</param>
        /// <param name="year">year</param>
        /// <returns>canUpdate</returns>
        protected bool BillingDetailsCheckDuplicateAndCount(int id, int projectId, int role, string billeditid, int monthId, int year)
        {
            string y = string.Empty;
            string y1 = "0";
            bool canUpdate = false;

            try
            {
                DataSet ds = objBillingDAL.BillingDetailsCheckDuplicate(id, projectId, role, billeditid, monthId, year);

                y = ds.Tables[0].Rows[0].ItemArray[0].ToString();

                DataSet ds1 = objBillingDAL.BillingDetailsbyResourceRoleProject(ddlResources.SelectedValue.ToString(), projectId, ddlRole.SelectedValue.ToString(), monthId, year);

                if (ds1.Tables[0].Rows.Count >= 1)
                {

                    y1 = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                }

                int count = objBillingDAL.CountofBillingDetailsbyResourceRoleProject(ddlResources.SelectedValue.ToString(), projectId, Convert.ToInt32(ddlRole.SelectedValue.ToString()), monthId, year);

                if (y != y1 && count == 1)
                {
                    dvresource.Visible = true;
                    lblResource.Visible = true;
                    canUpdate = false;
                }
                else
                {
                    canUpdate = true;
                }
            }
            catch (Exception exx)
            {
                Logger.Error("An exception caught on updating entry.", exx);
            }
            return canUpdate;
        }
        #endregion

        #region "InsertOrUpdateBillingDetailsEntry"

        /// <summary>
        /// Insert Or Update BillingDetails Entry
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// <param name="id">id</param>
        /// <param name="role">role</param>
        /// <param name="mode">mode Edit/Add</param>
        /// <param name="billeditid">billeditid</param>
        protected void InsertOrUpdateBillingDetailsEntry(int projectId, int id, int role, string mode, string billeditid, int iFlag)
        {
            dvresource.Visible = false;
            lblResource.Visible = false;
            IFormatProvider culture = new CultureInfo("fr-Fr", true);

            DataSet dtInsert = objBillingDAL.InsertBillingDetails(ddlResources.SelectedValue.ToString(), projectId, ddlRole.SelectedValue.ToString(),
            txtPlanned.Text, txtActual.Text, txtActualSpent.Text, txtComments.Text, Convert.ToDateTime(txtFromDate.Text, culture), Convert.ToDateTime(txtToDate.Text, culture),
            Session["UserID"], DateTime.Now, Session["UserID"], DateTime.Now, id, "1", role, mode, billeditid, iFlag);

            if (mode == "Edit")
            {
                DisplayResource();
                btnAdd.Text = "Add";
                ddlResources.Enabled = true;
                ddlRole.Enabled = true;
                ClearDetails();
              //Response.Redirect("BillingDetailsEntry.aspx?projectId=" + projectId + "&month=" + monthId + "&year=" + year); ---> commented due to the issue not showing the validation message while editing.
            }
            else if (mode == "Add")
            {
                //Clearing the total values before adding
                totalPlannedBillable = 0;
                totalActualBillable = 0;
                totalActualSpent = 0;

                DisplayResourcedetails();
                ClearDetails();
            }
        }
        #endregion


        protected void dgResourceDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataSet dgResourceDetailsDataSet = Session["dgResourceDetails"] as DataSet;
            DataTable dgResourceDetailsdt = new DataTable();
            dgResourceDetailsdt = dgResourceDetailsDataSet.Tables[0];
            if (dgResourceDetailsdt != null)
            {

                dgResourceDetailsdt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                dgResourceDetails.DataSource = dgResourceDetailsdt;
                dgResourceDetails.DataBind();
                SumOfPlannedActualBilling();
            }

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

        /// <summary>
        /// Get Sum Of Actual Spent Hours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSelectedIndexChanged_GetSumOfActualSpentHours(object sender, EventArgs e)
        {
            string resourceId = ddlResources.SelectedItem.Value.ToString();
            if (txtFromDate.Text != string.Empty && txtToDate.Text != string.Empty)
            {
                decimal ?actualSpentTime = objBillingDAL.GetSumOfActualSpentHours(projectId,Convert.ToInt32(ddlResources.SelectedItem.Value.ToString()), txtFromDate.Text, txtToDate.Text);
                if (actualSpentTime!=null)
                {
                    txtActualSpent.Text = actualSpentTime.ToString();
                }
            }

            if (resourceId != null)
            {
                DataTable dtRole = PMOscar.BaseDAL.ExecuteDataTable("Select RoleId from [Resource] where ResourceId=" + resourceId);
                if (dtRole.Rows.Count > 0)
                {
                    ddlRole.Text = dtRole.Rows[0].ItemArray[0].ToString();
                }
            }

        }

        /// <summary>
        /// Get Sum Of Actual Spent Hours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnTextChanged_GetSumOfActualSpentHours(object sender, EventArgs e)
        {
            if (txtFromDate.Text != string.Empty && txtToDate.Text != string.Empty)
            {
                decimal ? actualSpentTime = objBillingDAL.GetSumOfActualSpentHours(projectId,Convert.ToInt32(ddlResources.SelectedItem.Value.ToString()), txtFromDate.Text, txtToDate.Text);
                if (actualSpentTime != null)
                {
                    txtActualSpent.Text = actualSpentTime.ToString();
                }
            }
        }

        /// <summary>
        /// Get Sum Of Actual Spent Hours
        /// </summary>
        [WebMethod]
        public static decimal GetSumOfActualSpentHours(int projectID, int resourceID, int fromMonth, int fromDay, int fromYear, int toMonth, int toDay, int toYear)
        {
            string fromDate = fromDay + "/" + fromMonth + "/" + fromYear;
            string toDate = toDay + "/" + toMonth + "/" + toYear;
            BillingDetailsDAL billingDetailsDAL = new BillingDetailsDAL();
            return billingDetailsDAL.GetSumOfActualSpentHours(projectID, resourceID, fromDate, toDate);
        }
    }
}