// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Resource.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
//  Description  : Manipulates the details of the resource.
//  Author       : Muralikrishnan
//  Created Date : 18 February 2011
//  Modified By  : Muralikrishnan
//  Modified Date:   
// </summary>
// -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PMOscar.Core;
using PMOscar.DAL;
using System.Web.UI;
using System.Globalization;

namespace PMOscar
{
    public partial class Resource : System.Web.UI.Page
    {
        # region"Declarations"
        public string roleName;
        //public DateTime joinDate;
        public IList<SqlParameter> parameter;
        private int resourceEditId = 0;
        private int status = 0;
        string employeeCode;

        #endregion

        public bool joinDateChanged;

        #region"Page Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            checkBox1.Visible = false;
            checkBoxText.Visible = false;
            var resource = Request.QueryString["ResEditId"];
            if (resource != null)
            {
                string query = string.Format("select Role from Role ro,Resource r where ro.RoleId=r.RoleId and r.ResourceId = {0}", resource);
                roleName = PMOscar.BaseDAL.ExecuteScalar(query).ToString();
            }
            txtemployeecode.Focus();

            if (Session["UserName"] == null)
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

            if (!Page.IsPostBack)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                resourceEditId = Request.QueryString["ResEditId"] != null ? Convert.ToInt32(Request.QueryString["ResEditId"]) : 0;
                BindDropDownRole();  // Method to bind the roles in the DropDownList
                BindDropDownTeam(); //Method to bind the Team in the DropDownList
                BindDropDownCostCentre();//Method to bind Cost Centre in the DropDownList
                BindYearDropDown(); // Fill year drop down
                txtemployeecode.Text = employeeCode;
                string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
                DateTime tmpDate = DateTime.ParseExact(CurrentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                for (int i = 0; i < 5; i++)
                {
                    tmpDate = tmpDate.AddDays(1);
                    if (tmpDate.DayOfWeek == DayOfWeek.Saturday || tmpDate.DayOfWeek == DayOfWeek.Sunday || !IsHoliday(tmpDate))
                    {
                        i--;
                    }
                }
                txtAvailableHours.Text = tmpDate.ToString("dd/MM/yyyy");

                if (resourceEditId != 0)
                {
                    lblResourceStatus.Text = "Edit Resource";
                    SetResourceDetails(); // Method to set the resource details...
                    txtAvailableHours.Enabled = false;
                }
            }

            txtResourceName.Attributes.Add("onkeypress", "return CheckTextValue(event,this);");
            errorSpan.InnerText = "";
            lnkAddRole.Attributes.Add("onclick", "window.open('AddRole.aspx','_blank','height=200,width=400,toolbar=yes,scrollbars=no,resizable=no,top=400,left=500');return false");
        }


        #endregion

        #region"Methods"

        // Method to set the resource details...
        public bool IsHoliday(DateTime date)
        {
            return date.Date.ToString("dd/MM") != "26/01" && date.Date.ToString("dd/MM") != "15/08" && date.Date.ToString("dd/MM") != "01/05" && date.Date.ToString("dd/MM") != "02/10";
        }

        // Method to set the resource details...
        private void SetResourceDetails()
        {
            int resourceActiveStatus = 0;
            string query1 = string.Format("Select RoleId From Role where [Role]  = '{0}'", PMOscar.Core.Constants.AddRole.TRAINEE);
            object trainee = PMOscar.BaseDAL.ExecuteScalar(query1);
            string query2 = string.Format("Select RoleId From Role where [Role] = '{0}'", PMOscar.Core.Constants.AddRole.QATRAINEE);
            object qatrainee = PMOscar.BaseDAL.ExecuteScalar(query2);

            DataSet dsResourceDetails = ResourceDAL.GetResourceDetailsById(resourceEditId);
            DataSet dsResourceUtilizationDetails = ResourceDAL.GetResourceUtilizationPercentageById(resourceEditId);
            string query = string.Format("Select RoleId From Resource where ResourceId = {0}", resourceEditId);
            object resourcId = PMOscar.BaseDAL.ExecuteScalar(query);
            int rid = Convert.ToInt32(resourcId);
            if (dsResourceDetails.Tables[0].Rows.Count > 0)
            {
                txtemployeecode.Text = dsResourceDetails.Tables[0].Rows[0].ItemArray[6].ToString();
                txtResourceName.Text = dsResourceDetails.Tables[0].Rows[0].ItemArray[1].ToString();
                ddlRole.Text = dsResourceDetails.Tables[0].Rows[0].ItemArray[2].ToString();
                ddlTeam.Text = dsResourceDetails.Tables[0].Rows[0].ItemArray[3].ToString();
                resourceActiveStatus = Convert.ToInt32(dsResourceDetails.Tables[0].Rows[0].ItemArray[4]);
                ddlCostCentre.Text = dsResourceDetails.Tables[0].Rows[0].ItemArray[7].ToString();
                txtWeeklyHours.Text = dsResourceDetails.Tables[0].Rows[0].ItemArray[8].ToString();
                txtJoinDate.Text = dsResourceDetails.Tables[0].Rows[0].ItemArray[9].ToString();
                txtExitDate.Text = dsResourceDetails.Tables[0].Rows[0].ItemArray[10].ToString();
                txtAvailableHours.Text = dsResourceDetails.Tables[0].Rows[0].ItemArray[11].ToString();
            }
            if (rid == Convert.ToInt16(trainee) || rid == Convert.ToInt16(qatrainee))
            {
                if (dsResourceUtilizationDetails.Tables[0].Rows.Count > 0 && dsResourceUtilizationDetails.Tables[0].Rows.Count > 1)
                {
                    txtStartdate.Text = dsResourceUtilizationDetails.Tables[0].Rows[0].ItemArray[0].ToString();
                    txtEnddate.Text = dsResourceUtilizationDetails.Tables[0].Rows[0].ItemArray[1].ToString();
                    txtUtilization.Text = dsResourceUtilizationDetails.Tables[0].Rows[0].ItemArray[2].ToString();
                    txtStartDate1.Text = dsResourceUtilizationDetails.Tables[0].Rows[1].ItemArray[0].ToString();
                    txtEnddate1.Text = dsResourceUtilizationDetails.Tables[0].Rows[1].ItemArray[1].ToString();
                    txtUtilization1.Text = dsResourceUtilizationDetails.Tables[0].Rows[1].ItemArray[2].ToString();
                    txtStartDate2.Text = dsResourceUtilizationDetails.Tables[0].Rows[2].ItemArray[0].ToString();
                    txtEndDate2.Text = dsResourceUtilizationDetails.Tables[0].Rows[2].ItemArray[1].ToString();
                    txtUtilization2.Text = dsResourceUtilizationDetails.Tables[0].Rows[2].ItemArray[2].ToString();
                    txtStartDate3.Text = dsResourceUtilizationDetails.Tables[0].Rows[3].ItemArray[0].ToString();
                    txtEndDate3.Text = dsResourceUtilizationDetails.Tables[0].Rows[3].ItemArray[1].ToString();
                    txtUtilization3.Text = dsResourceUtilizationDetails.Tables[0].Rows[3].ItemArray[2].ToString();
                    txtStartDate4.Text = dsResourceUtilizationDetails.Tables[0].Rows[4].ItemArray[0].ToString();
                    txtEndDate4.Text = dsResourceUtilizationDetails.Tables[0].Rows[4].ItemArray[1].ToString();
                    txtUtilization4.Text = dsResourceUtilizationDetails.Tables[0].Rows[4].ItemArray[2].ToString();
                }
                else if (dsResourceUtilizationDetails.Tables[0].Rows.Count > 0 && dsResourceUtilizationDetails.Tables[0].Rows.Count < 2)
                {
                    txtStartdate.Text = dsResourceUtilizationDetails.Tables[0].Rows[0].ItemArray[0].ToString();
                    txtEnddate.Text = dsResourceUtilizationDetails.Tables[0].Rows[0].ItemArray[1].ToString();
                    txtUtilization.Text = dsResourceUtilizationDetails.Tables[0].Rows[0].ItemArray[2].ToString();
                }
            }

            DateTime billingStartDate = Convert.ToDateTime(dsResourceDetails.Tables[0].Rows[0].ItemArray[5]);
            var selectedYear = billingStartDate.Year.ToString();
            ddlMonth.SelectedValue = billingStartDate.Month.ToString();
            ddlYear.SelectedValue = selectedYear;

            if (resourceActiveStatus == 1)
                RdActive.Checked = true;
            else
                RdInActive.Checked = true;


        }

        // Method to bind the roles in the DropDownList
        private void BindDropDownRole()
        {
            parameter = new List<SqlParameter>();
            DataTable dt = BaseDAL.ExecuteSPDataTable("GetRole", parameter);
            if (dt != null)
            {
                ddlRole.DataSource = dt;
                ddlRole.DataTextField = "Role";
                ddlRole.DataValueField = "RoleId";
                ddlRole.DataBind();
                ddlRole.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        private void BindDropDownTeam()
        {
            parameter = new List<SqlParameter>();
            DataTable dtTeam = BaseDAL.ExecuteSPDataTable("GetTeam", parameter);
            if (dtTeam != null)
            {
                ddlTeam.DataSource = dtTeam;
                ddlTeam.DataTextField = "Team";
                ddlTeam.DataValueField = "TeamID";
                ddlTeam.DataBind();
                ddlTeam.Items.Insert(0, new ListItem("Select", "0"));


            }

        }
        //Method to bind Cost Centre in the drop downlist
        private void BindDropDownCostCentre()
        {
            parameter = new List<SqlParameter>();
            DataTable dtCostCentre = BaseDAL.ExecuteSPDataTable("GetCostCentre", parameter);
            if (dtCostCentre != null)
            {
                ddlCostCentre.DataSource = dtCostCentre;
                ddlCostCentre.DataTextField = "CostCentre";
                ddlCostCentre.DataValueField = "CostCentreID";
                ddlCostCentre.DataBind();
                ddlCostCentre.Items.Insert(0, new ListItem("Select", "0"));


            }

        }

        // Method for Populating Year Combobox...
        /// <summary>
        /// Fills the years.
        /// </summary>
        private void BindYearDropDown()
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

                //Select current year
                int resourceEditId = Request.QueryString["ResEditId"] != null ? Convert.ToInt32(Request.QueryString["ResEditId"]) : 0;
                //For adding new resource Setting current year
                if (resourceEditId == 0)
                {
                    int currentYear = DateTime.Now.Year;
                    ddlYear.Items.FindByValue(currentYear.ToString()).Selected = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }
        //selection change in default role
        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["ResEditId"] != null)
            {

                if ((ddlRole.SelectedItem.Text != PMOscar.Core.Constants.AddRole.TRAINEE && (ddlRole.SelectedItem.Text != PMOscar.Core.Constants.AddRole.QATRAINEE &&
                    roleName == PMOscar.Core.Constants.AddRole.TRAINEE)) ||
                    ((ddlRole.SelectedItem.Text != PMOscar.Core.Constants.AddRole.QATRAINEE && ddlRole.SelectedItem.Text != PMOscar.Core.Constants.AddRole.TRAINEE) && roleName == PMOscar.Core.Constants.AddRole.QATRAINEE))
                {
                    checkBox1.Visible = true;
                    checkBox1.Checked = true;
                    checkBoxText.Visible = true;
                }


            }
        }

        // Method to add the resource details...
        private void AddInfo()
        {
            IFormatProvider culture = new CultureInfo("fr-Fr", true);
            DateTime dtAvailableHours = DateTime.MinValue;
            int ResourceId = 0;

            if (RdActive.Checked == true)
                status = 1;
            else
                status = 0;

            object exists = null;
            string query = string.Format("select ResourceId from Resource where emp_Code='{0}'", txtemployeecode.Text.Trim());
            exists = PMOscar.BaseDAL.ExecuteScalar(query);
            int resource = Convert.ToInt16(exists);
            if (exists != null && resource != resourceEditId)
            {
                lblEmployeecode.Visible = true;
                lblEmployeecode.Text = PMOscar.Core.Constants.AddRole.DUPLICATEEMPLOYEECODE;
                return;
            }
            if (!string.IsNullOrEmpty(txtAvailableHours.Text) && !DateTime.TryParseExact(txtAvailableHours.Text, "dd/MM/yyyy", culture, DateTimeStyles.None, out dtAvailableHours))
            {
                lblDateError.Visible = true;
                lblDateError.Text = "Available Hours Start Date format is incorrect.";
                return;
            }
            else
            {
                DateTime endDate;
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                DateTime joinDate = DateTime.ParseExact(txtJoinDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime availableHours = DateTime.ParseExact(txtAvailableHours.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                endDate = (txtExitDate.Text.ToString() != string.Empty) ?
                     DateTime.ParseExact(txtExitDate.Text.Trim(), "dd/M/yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact("31/12/2099", "dd/M/yyyy", CultureInfo.InvariantCulture);
                lblEmployeecode.Visible = false;
                lblDateError.Visible = false;
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ResourceId", 1));
                parameter.Add(new SqlParameter("@ResourceStatus", ""));
                parameter.Add(new SqlParameter("@ResourceName", txtResourceName.Text.Trim()));
                parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@IsActive", status));
                parameter.Add(new SqlParameter("@RoleId", Convert.ToInt32(ddlRole.SelectedValue.ToString())));
                parameter.Add(new SqlParameter("@TeamID", Convert.ToInt32(ddlTeam.SelectedValue.ToString())));
                parameter.Add(new SqlParameter("@BillingStartDate", new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1)));
                parameter.Add(new SqlParameter("@OpMode", "INSERT"));
                parameter.Add(new SqlParameter("@CostCentreID", Convert.ToInt32(ddlCostCentre.SelectedValue.ToString())));
                parameter.Add(new SqlParameter("@emp_Code", txtemployeecode.Text.Trim()));
                parameter.Add(new SqlParameter("@WeeklyHour", txtWeeklyHours.Text.Trim()));
                parameter.Add(new SqlParameter("@JoinDate", joinDate));
                parameter.Add(new SqlParameter("@AvailableHours", availableHours));
                parameter.Add(new SqlParameter("@ExitDate", endDate));
                try
                {
                    ResourceId = PMOscar.BaseDAL.ExecuteSPScalar("ResourceOperations", parameter);
                }
                catch (Exception ex)
                {
                    ResourceId = 0;
                    throw ex;
                }
            }
        }

        // Method to update the resource details...
        private void UpdateInfo()
        {
            int ResourceId = 0;

            if (RdActive.Checked == true)
                status = 1;
            else
                status = 0;

            object exists = null;
            string query = string.Format("select ResourceId from Resource where emp_Code='{0}'", txtemployeecode.Text.Trim());
            exists = PMOscar.BaseDAL.ExecuteScalar(query);
            int resource = Convert.ToInt16(exists);
            if (exists != null && resource != resourceEditId)
            {
                lblEmployeecode.Visible = true;
                lblEmployeecode.Text = PMOscar.Core.Constants.AddRole.DUPLICATEEMPLOYEECODE;
                return;
            }

            else
            {
                DateTime endDate;
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                DateTime joinDate = DateTime.ParseExact(txtJoinDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime availableHours = DateTime.ParseExact(txtAvailableHours.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                endDate = (txtExitDate.Text.ToString() != string.Empty) ?
                   DateTime.ParseExact(txtExitDate.Text.Trim(), "dd/M/yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact("31/12/2099", "dd/M/yyyy", CultureInfo.InvariantCulture);
                lblEmployeecode.Visible = false;
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ResourceId", resource));
                parameter.Add(new SqlParameter("@ResourceStatus", ""));
                parameter.Add(new SqlParameter("@ResourceName", txtResourceName.Text.Trim()));
                parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@IsActive", status));
                parameter.Add(new SqlParameter("@RoleId", Convert.ToInt32(ddlRole.SelectedValue.ToString())));
                parameter.Add(new SqlParameter("@TeamID", Convert.ToInt32(ddlTeam.SelectedValue.ToString())));
                parameter.Add(new SqlParameter("@BillingStartDate", new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1)));
                parameter.Add(new SqlParameter("@OpMode", "UPDATE"));
                parameter.Add(new SqlParameter("@CostCentreID", Convert.ToInt32(ddlCostCentre.SelectedValue.ToString())));
                parameter.Add(new SqlParameter("@emp_Code", txtemployeecode.Text.Trim()));
                parameter.Add(new SqlParameter("@WeeklyHour", txtWeeklyHours.Text.Trim()));
                parameter.Add(new SqlParameter("@JoinDate", joinDate));
                parameter.Add(new SqlParameter("@AvailableHours", availableHours));
                parameter.Add(new SqlParameter("@ExitDate", endDate));
                try
                {
                    ResourceId = PMOscar.BaseDAL.ExecuteSPScalar("ResourceOperations", parameter);
                }
                catch (Exception ex)
                {
                    ResourceId = 0;
                    throw ex;
                }
            }
        }

        // Method to clear textbox
        private void Clear()
        {
            txtResourceName.Text = string.Empty;
        }

        private bool ResourceStartMonthValidation(DateTime selectedDate, DateTime resourceStartDate)
        {
            resourceStartDate = resourceStartDate.Date;

            if (selectedDate.Month == resourceStartDate.Month && selectedDate.Year == resourceStartDate.Year)
            {
                return false;
            }

            int selectedValue = DateTime.Compare(selectedDate, resourceStartDate);

            if (selectedValue < 0)
            {
                errorSpan.InnerText = "Billing Start Month should be past resource start month.";
                return true;
            }

            return false;
        }

        #endregion

        #region"Control Events"
        protected void btnSave_Click(object sender, EventArgs e)
        {
            resourceEditId = Request.QueryString["ResEditId"] != null ? Convert.ToInt32(Request.QueryString["ResEditId"]) : 0;
            errorSpan.InnerText = "";
            DateTime resourceStartDate;
            DateTime selectedDate = new DateTime(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue), 1);
            if (decimal.Parse(txtWeeklyHours.Text) > Convert.ToDecimal(PMOscar.Utility.EnumTypes.Estimation.WeeklyHours))
            {
                errorSpanweekly.InnerText = PMOscar.Core.Constants.AddRole.WEEKLYHOURS;
                return;
            }
            else if (decimal.Parse(txtWeeklyHours.Text) == 0)
            {
                errorSpanweekly.InnerText = PMOscar.Core.Constants.AddRole.ZERO;
                return;
            }

            else
            {
                errorSpanweekly.InnerText = string.Empty;
            }


            if (resourceEditId != 0)
            {
                var parameter = new List<SqlParameter> { new SqlParameter("@ResourceID", resourceEditId) };
                var dtResources = PMOscar.BaseDAL.ExecuteSPDataSet("GetResourceById", parameter);
                resourceStartDate = DateTime.Parse(dtResources.Tables[0].Rows[0].ItemArray[3].ToString());
                resourceStartDate = resourceStartDate.AddDays(-1 * (resourceStartDate.Day - 1));

                bool validationStatus = ResourceStartMonthValidation(selectedDate, resourceStartDate);

                if (validationStatus)
                {
                    return;
                }



                UpdateUtilizationPercentage();
                UpdateInfo();  // Method to update the resource details...   

            }
            else
            {
                bool validationStatus = ResourceStartMonthValidation(selectedDate, DateTime.Now);

                if (validationStatus)
                {
                    return;
                }

                AddInfo();  // Method to add the resource details...
                try
                {

                    SaveUtilizationPercentage();
                }
                catch (Exception ex)
                {

                }

            }
            if (!lblEmployeecode.Visible)
            {

                if (resourceEditId != 0)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Resource has updated successfully.');window.location ='ResourceListing.aspx';",
                    true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Resource has created successfully.');window.location ='ResourceListing.aspx';",
                    true);

                if (ddlRole.SelectedItem.Text == PMOscar.Core.Constants.AddRole.TRAINEE || ddlRole.SelectedItem.Text == PMOscar.Core.Constants.AddRole.QATRAINEE || checkBox1.Checked == true)
                {
                    PnlEdit.Visible = true;
                }
                else
                {
                    PnlEdit.Visible = false;
                }

                //Response.Redirect("ResourceListing.aspx");
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResourceListing.aspx");
        }



        public void SaveUtilizationPercentage()
        {
            if ((ddlRole.SelectedItem.Text != "Select" && ddlRole.SelectedItem.Text == PMOscar.Core.Constants.AddRole.TRAINEE) || (ddlRole.SelectedItem.Text != "Select" && ddlRole.SelectedItem.Text == PMOscar.Core.Constants.AddRole.QATRAINEE))
            {

                var adjustParam = 0;
                var isTrainee = 1;
                string query = string.Format("Select ResourceId from Resource where emp_Code='{0}'", txtemployeecode.Text);
                object resid = PMOscar.BaseDAL.ExecuteScalar(query);
                int resource = Convert.ToInt32(resid);
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                DateTime joinDate = DateTime.ParseExact(txtStartdate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime joinDate1 = DateTime.ParseExact(txtStartDate1.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime joinDate2 = DateTime.ParseExact(txtStartDate2.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime joinDate3 = DateTime.ParseExact(txtStartDate3.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime joinDate4 = DateTime.ParseExact(txtStartDate4.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(txtEnddate.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate1 = DateTime.ParseExact(txtEnddate1.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate2 = DateTime.ParseExact(txtEndDate2.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate3 = DateTime.ParseExact(txtEndDate3.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate4 = DateTime.ParseExact(txtEndDate4.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                int userid = Convert.ToInt16(Session["UserID"]);
                DateTime currentDate = DateTime.UtcNow.Date;
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ResourceId", resource));
                parameter.Add(new SqlParameter("@StartDate", joinDate));
                parameter.Add(new SqlParameter("@StartDate1", joinDate1));
                parameter.Add(new SqlParameter("@StartDate2", joinDate2));
                parameter.Add(new SqlParameter("@StartDate3", joinDate3));
                parameter.Add(new SqlParameter("@StartDate4", joinDate4));
                parameter.Add(new SqlParameter("@EndDate", endDate));
                parameter.Add(new SqlParameter("@EndDate1", endDate1));
                parameter.Add(new SqlParameter("@EndDate2", endDate2));
                parameter.Add(new SqlParameter("@EndDate3", endDate3));
                parameter.Add(new SqlParameter("@EndDate4", endDate4));
                parameter.Add(new SqlParameter("@UtilizationPercentage", Convert.ToInt32(txtUtilization.Text)));
                parameter.Add(new SqlParameter("@UtilizationPercentage1", Convert.ToInt32(txtUtilization1.Text)));
                parameter.Add(new SqlParameter("@UtilizationPercentage2", Convert.ToInt32(txtUtilization2.Text)));
                parameter.Add(new SqlParameter("@UtilizationPercentage3", Convert.ToInt32(txtUtilization3.Text)));
                parameter.Add(new SqlParameter("@UtilizationPercentage4", Convert.ToInt32(txtUtilization4.Text)));
                parameter.Add(new SqlParameter("@AdjustmentFactor", adjustParam));
                parameter.Add(new SqlParameter("@CreatedBy", userid));
                parameter.Add(new SqlParameter("@Createddate", currentDate));
                parameter.Add(new SqlParameter("@UpdatedBy", ""));
                parameter.Add(new SqlParameter("@UpdatedDate", null));
                parameter.Add(new SqlParameter("@IsTrainee", isTrainee));
                BaseDAL.ExecuteSPDataTable("spInsertUtilisationPercentage", parameter);


            }
            else if (ddlRole.SelectedItem.Text != "Select" && ddlRole.SelectedItem.Text != PMOscar.Core.Constants.AddRole.TRAINEE)
            {
                DateTime endDate;
                var adjustParam = 0;
                int utilization = Convert.ToInt32(PMOscar.Utility.EnumTypes.Estimation.EstimationPercentage);
                var isTrainee = 0;
                string query = string.Format("Select ResourceId from Resource where emp_Code='{0}'", txtemployeecode.Text);
                object resid = PMOscar.BaseDAL.ExecuteScalar(query);
                int resource = Convert.ToInt32(resid);
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                DateTime joinDate = DateTime.ParseExact(txtJoinDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                endDate = (txtExitDate.Text.ToString() != string.Empty) ?
                    DateTime.ParseExact(txtExitDate.Text.Trim(), "dd/M/yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact("31/12/2099", "dd/M/yyyy", CultureInfo.InvariantCulture);
                int userid = Convert.ToInt16(Session["UserID"]);
                DateTime currentDate = DateTime.UtcNow.Date;
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ResourceId", resource));
                parameter.Add(new SqlParameter("@StartDate", joinDate));
                parameter.Add(new SqlParameter("@EndDate", endDate));
                parameter.Add(new SqlParameter("@UtilizationPercentage", utilization));
                parameter.Add(new SqlParameter("@AdjustmentFactor", adjustParam));
                parameter.Add(new SqlParameter("@CreatedBy", userid));
                parameter.Add(new SqlParameter("@Createddate", currentDate));
                parameter.Add(new SqlParameter("@UpdatedBy", ""));
                parameter.Add(new SqlParameter("@UpdatedDate", null));
                parameter.Add(new SqlParameter("@IsTrainee", isTrainee));

                BaseDAL.ExecuteSPDataTable("spInsertUtilisationPercentage", parameter);
            }

        }
        public void UpdateUtilizationPercentage()
        {
            string query = string.Format("Select ResourceId from Resource where emp_Code='{0}'", txtemployeecode.Text);
            object resid = PMOscar.BaseDAL.ExecuteScalar(query);
            int resource = Convert.ToInt32(resid);
            //converting trainee to any other rule
            if (!checkBox1.Checked && ((ddlRole.SelectedItem.Text != PMOscar.Core.Constants.AddRole.TRAINEE &&
                 roleName == PMOscar.Core.Constants.AddRole.TRAINEE) ||
                (ddlRole.SelectedItem.Text != PMOscar.Core.Constants.AddRole.QATRAINEE && roleName == PMOscar.Core.Constants.AddRole.QATRAINEE)))
            {
                DateTime endDate;
                var adjustParam = 0;
                int utilization = Convert.ToInt32(PMOscar.Utility.EnumTypes.Estimation.EstimationPercentage);
                var isTrainee = 0;
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                DateTime joinDate = DateTime.ParseExact(txtJoinDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                endDate = (txtExitDate.Text.ToString() != string.Empty) ?
                    DateTime.ParseExact(txtExitDate.Text.Trim(), "dd/M/yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact("31/12/2099", "dd/M/yyyy", CultureInfo.InvariantCulture);
                int userid = Convert.ToInt16(Session["UserID"]);
                DateTime currentDate = DateTime.UtcNow.Date;
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ResourceId", resource));
                parameter.Add(new SqlParameter("@StartDate", joinDate));
                parameter.Add(new SqlParameter("@EndDate", endDate));
                parameter.Add(new SqlParameter("@UtilizationPercentage", utilization));
                parameter.Add(new SqlParameter("@AdjustmentFactor", adjustParam));
                parameter.Add(new SqlParameter("@CreatedBy", userid));
                parameter.Add(new SqlParameter("@Createddate", currentDate));
                parameter.Add(new SqlParameter("@UpdatedBy", ""));
                parameter.Add(new SqlParameter("@UpdatedDate", null));
                parameter.Add(new SqlParameter("@IsTrainee", isTrainee));
                parameter.Add(new SqlParameter("@Status", 2));
                BaseDAL.ExecuteSPDataTable("UpdateUtilisationPercentage", parameter);
            }
            else if (((ddlRole.SelectedItem.Text == PMOscar.Core.Constants.AddRole.TRAINEE &&
                 roleName != PMOscar.Core.Constants.AddRole.TRAINEE) ||
                (ddlRole.SelectedItem.Text == PMOscar.Core.Constants.AddRole.QATRAINEE && roleName != PMOscar.Core.Constants.AddRole.QATRAINEE))) //converting any other rules to trainee
            {
                var adjustParam = 0;
                var isTrainee = 1;
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                DateTime joinDate = DateTime.ParseExact(txtStartdate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime joinDate1 = DateTime.ParseExact(txtStartDate1.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime joinDate2 = DateTime.ParseExact(txtStartDate2.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime joinDate3 = DateTime.ParseExact(txtStartDate3.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime joinDate4 = DateTime.ParseExact(txtStartDate4.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(txtEnddate.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate1 = DateTime.ParseExact(txtEnddate1.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate2 = DateTime.ParseExact(txtEndDate2.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate3 = DateTime.ParseExact(txtEndDate3.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate4 = DateTime.ParseExact(txtEndDate4.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                int userid = Convert.ToInt16(Session["UserID"]);
                DateTime currentDate = DateTime.UtcNow.Date;
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ResourceId", resource));
                parameter.Add(new SqlParameter("@StartDate", joinDate));
                parameter.Add(new SqlParameter("@StartDate1", joinDate1));
                parameter.Add(new SqlParameter("@StartDate2", joinDate2));
                parameter.Add(new SqlParameter("@StartDate3", joinDate3));
                parameter.Add(new SqlParameter("@StartDate4", joinDate4));
                parameter.Add(new SqlParameter("@EndDate", endDate));
                parameter.Add(new SqlParameter("@EndDate1", endDate1));
                parameter.Add(new SqlParameter("@EndDate2", endDate2));
                parameter.Add(new SqlParameter("@EndDate3", endDate3));
                parameter.Add(new SqlParameter("@EndDate4", endDate4));
                parameter.Add(new SqlParameter("@UtilizationPercentage", Convert.ToInt32(txtUtilization.Text)));
                parameter.Add(new SqlParameter("@UtilizationPercentage1", Convert.ToInt32(txtUtilization1.Text)));
                parameter.Add(new SqlParameter("@UtilizationPercentage2", Convert.ToInt32(txtUtilization2.Text)));
                parameter.Add(new SqlParameter("@UtilizationPercentage3", Convert.ToInt32(txtUtilization3.Text)));
                parameter.Add(new SqlParameter("@UtilizationPercentage4", Convert.ToInt32(txtUtilization4.Text)));
                parameter.Add(new SqlParameter("@AdjustmentFactor", adjustParam));
                parameter.Add(new SqlParameter("@CreatedBy", userid));
                parameter.Add(new SqlParameter("@Createddate", currentDate));
                parameter.Add(new SqlParameter("@UpdatedBy", ""));
                parameter.Add(new SqlParameter("@UpdatedDate", null));
                parameter.Add(new SqlParameter("@IsTrainee", isTrainee));
                parameter.Add(new SqlParameter("@Status", 1));
                BaseDAL.ExecuteSPDataTable("UpdateUtilisationPercentage", parameter);
            }
            else // all other cases
            {
                if (ddlRole.SelectedItem.Text == PMOscar.Core.Constants.AddRole.TRAINEE)
                {
                    var adjustParam = 0;
                    var isTrainee = 1;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    DateTime joinDate = DateTime.ParseExact(txtStartdate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime joinDate1 = DateTime.ParseExact(txtStartDate1.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                    DateTime joinDate2 = DateTime.ParseExact(txtStartDate2.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                    DateTime joinDate3 = DateTime.ParseExact(txtStartDate3.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                    DateTime joinDate4 = DateTime.ParseExact(txtStartDate4.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.ParseExact(txtEnddate.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                    DateTime endDate1 = DateTime.ParseExact(txtEnddate1.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                    DateTime endDate2 = DateTime.ParseExact(txtEndDate2.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                    DateTime endDate3 = DateTime.ParseExact(txtEndDate3.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                    DateTime endDate4 = DateTime.ParseExact(txtEndDate4.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
                    int userid = Convert.ToInt16(Session["UserID"]);
                    DateTime currentDate = DateTime.UtcNow.Date;
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@ResourceId", resource));
                    parameter.Add(new SqlParameter("@StartDate", joinDate));
                    parameter.Add(new SqlParameter("@StartDate1", joinDate1));
                    parameter.Add(new SqlParameter("@StartDate2", joinDate2));
                    parameter.Add(new SqlParameter("@StartDate3", joinDate3));
                    parameter.Add(new SqlParameter("@StartDate4", joinDate4));
                    parameter.Add(new SqlParameter("@EndDate", endDate));
                    parameter.Add(new SqlParameter("@EndDate1", endDate1));
                    parameter.Add(new SqlParameter("@EndDate2", endDate2));
                    parameter.Add(new SqlParameter("@EndDate3", endDate3));
                    parameter.Add(new SqlParameter("@EndDate4", endDate4));
                    parameter.Add(new SqlParameter("@UtilizationPercentage", Convert.ToInt32(txtUtilization.Text)));
                    parameter.Add(new SqlParameter("@UtilizationPercentage1", Convert.ToInt32(txtUtilization1.Text)));
                    parameter.Add(new SqlParameter("@UtilizationPercentage2", Convert.ToInt32(txtUtilization2.Text)));
                    parameter.Add(new SqlParameter("@UtilizationPercentage3", Convert.ToInt32(txtUtilization3.Text)));
                    parameter.Add(new SqlParameter("@UtilizationPercentage4", Convert.ToInt32(txtUtilization4.Text)));
                    parameter.Add(new SqlParameter("@AdjustmentFactor", adjustParam));
                    parameter.Add(new SqlParameter("@CreatedBy", userid));
                    parameter.Add(new SqlParameter("@Createddate", currentDate));
                    parameter.Add(new SqlParameter("@UpdatedBy", ""));
                    parameter.Add(new SqlParameter("@UpdatedDate", null));
                    parameter.Add(new SqlParameter("@IsTrainee", isTrainee));
                    BaseDAL.ExecuteSPDataTable("UpdateUtilisationPercentage", parameter);
                }
                else
                {
                    DateTime endDate;
                    var adjustParam = 0;
                    int utilization = Convert.ToInt32(PMOscar.Utility.EnumTypes.Estimation.EstimationPercentage);
                    var isTrainee = 0;
                    System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    DateTime joinDate = DateTime.ParseExact(txtJoinDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    endDate = (txtExitDate.Text.ToString() != string.Empty) ?
              DateTime.ParseExact(txtExitDate.Text.Trim(), "dd/M/yyyy", CultureInfo.InvariantCulture) : DateTime.ParseExact("31/12/2099", "dd/M/yyyy", CultureInfo.InvariantCulture);
                    int userid = Convert.ToInt16(Session["UserID"]);
                    DateTime currentDate = DateTime.UtcNow.Date;
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@ResourceId", resource));
                    parameter.Add(new SqlParameter("@StartDate", joinDate));
                    parameter.Add(new SqlParameter("@EndDate", endDate));
                    parameter.Add(new SqlParameter("@UtilizationPercentage", utilization));
                    parameter.Add(new SqlParameter("@AdjustmentFactor", adjustParam));
                    parameter.Add(new SqlParameter("@CreatedBy", userid));
                    parameter.Add(new SqlParameter("@Createddate", currentDate));
                    parameter.Add(new SqlParameter("@UpdatedBy", ""));
                    parameter.Add(new SqlParameter("@UpdatedDate", null));
                    parameter.Add(new SqlParameter("@IsTrainee", isTrainee));
                    BaseDAL.ExecuteSPDataTable("UpdateUtilisationPercentage", parameter);
                }
            }
        }

        #endregion
        protected void ImgButton_Click(object sender, ImageClickEventArgs e)
        {
            BindDropDownRole();
        }
    }
}
