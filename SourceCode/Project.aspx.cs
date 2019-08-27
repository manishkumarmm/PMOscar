// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Project.aspx.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
//  Description  : 
//  Author       : 
//  Created Date : 
//  Modified By  : Joshwa George
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
using PMOscar.Core;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;


namespace PMOscar
{
    public partial class Project : System.Web.UI.Page
    {
        #region"Declarations"
        private string chkvalueid = "0";
        private int ProjectEditId = 0;
        private static int ProjectId = 0;
        public IList<SqlParameter> parameter;
        private DataTable dtEstimation = new DataTable();
        private int totalBillable = 0;
        private int totalBudgeted = 0;
        private int totalRevisedBudgeted = 0;
        private Double totalActualHrs = 0;
        private Double totalActualHoursOriginal = 0;
        private int iFlag = 0;
        private int iFlagChck = 0;
        private int ShowInDashBoard = 0;
        private long editid = 0;
        private int year = 0;
        private int month = 0;
        private int weekindex = 0;
        private int EditFlag = 0;
        private string mode = string.Empty;
        private int editClickFlag = 0;
        #endregion
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            //chkValue.Enabled = true ;  //checking;
            iFlag = 2;
            //LinkButton1.Attributes.Add("onclick", "window.open('ProjectEstimationAudit.aspx','','height=600,width=500');return false");
            //LinkButton2.Attributes.Add("onclick", "window.open('ProjectAudit.aspx','','height=600,width=950');return false");
            txtProjectName.Focus();
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
                else if (Convert.ToInt16(Session["UserRoleID"]) == 1)
                {
                    HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                    if (hcliuser != null)
                        (Page.Master.FindControl("liuser") as HtmlControl).Visible = true;

                    HtmlControl hcauser = Page.Master.FindControl("auser") as HtmlControl;
                    if (hcauser != null)
                        (Page.Master.FindControl("auser") as HtmlControl).Attributes.Add("class", "");
                }

                else if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees)
                {
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
                }

                else
                {
                    HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                    if (hcliuser != null)
                        (Page.Master.FindControl("liuser") as HtmlControl).Visible = false;

                }
            }

            txtBudHours.Attributes.Add("onkeypress", "return CheckNemericValue(event,this);");
            txtRevBudHours.Attributes.Add("onkeypress", "return CheckNemericValue(event,this);");
            txtBillableHrs.Attributes.Add("onkeypress", "return CheckNemericValue(event,this);");

            lblValPhase.Text = "";
            lblValRole.Text = "";
            lblComments.Text = "";
            dtEstimation = Session["dtEstimation"] != null ? (DataTable)Session["dtEstimation"] : dtEstimation;
            btnEstSave.Text = "Save";

            // Get operation mode
            if (Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["mode"].Equals("add", StringComparison.OrdinalIgnoreCase))
                {
                    mode = "Add";
                    Session["ProjectEditId"] = null;
                }
                else
                {
                    if (Request.QueryString["mode"].Equals("edit", StringComparison.OrdinalIgnoreCase))
                    {
                        mode = "Edit";
                    }
                    else if (Request.QueryString["mode"].Equals("delete", StringComparison.OrdinalIgnoreCase))
                    {
                        mode = "Delete";
                    }
                }
            }
            if (mode != "Add")
            {
                if (Request.QueryString["ProjectEditId"] != null)
                {
                    ProjectEditId = Convert.ToInt32(Request.QueryString["ProjectEditId"]);
                    ProjectId = ProjectEditId;
                }
                else if (Session["ProjectEditId"] != null)
                {
                    ProjectEditId = Convert.ToInt16(Session["ProjectEditId"]);
                    ProjectId = ProjectEditId;
                }
            }
            if (Convert.ToInt32(Session["UserRoleID"]) == 1 && mode != "Add")
            {
                //ProjectEditId = Convert.ToInt32(Request.QueryString["ProjectEditId"]);
                //Session["ProjectEditId"] = ProjectEditId;//CHK123               
                btnEdit.Visible = true;
                btnClone.Visible = true;
                pnlCloneProject.Visible = true;
                //pnlCloneProject.Style.Add("display","block");
            }
            if (!Page.IsPostBack)
            {
                lblPctName.Text = "";
                lblSName.Text = "";
                lblPjctCode.Text = "";
                panEdit.Visible = false;
                Panel1.Visible = false;
                pnlPopup1.Visible = false;
                pnlPopup2.Visible = false;

                //lnkChangeHistory.Visible = false;

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString()); //........

                //clearcontrols();
                PnlEdit.Enabled = false;
                pnlEst.Enabled = false;
                pnlEst2.Enabled = false;

                BindComboDetails();

                if (Request.QueryString["ProjectEditId"] != null)
                {
                    lblProjectStatus.Text = "Project Details";
                    Session["ProjectEditId"] = ProjectEditId; //CHK123
                    ProjectId = ProjectEditId;
                    pnlEst.Visible = true;
                    pnlEst2.Visible = true;

                }
                else
                {
                    pnlEst.Visible = false;
                    pnlEst2.Visible = false;
                    btnEdit.Visible = false;
                    btnClone.Visible = false;
                    pnlCloneProject.Style.Add("display", "none");
                    PnlEdit.Enabled = true;
                    pnlEst.Enabled = false;
                    pnlEst2.Enabled = false;
                    editClickFlag = 0;
                    ProjectEditId = Convert.ToInt16(Session["ProjectEditId"]);// CHK123
                    ProjectId = ProjectEditId;
                }
                ClDelivery.SelectedDate = DateTime.Now.Date;
                ClRevisedDelivery.SelectedDate = DateTime.Now.Date;
                ClProjectStart.SelectedDate = DateTime.Now.Date;
                ClMaintClosing.SelectedDate = DateTime.Now.Date;
                if (ProjectEditId != 0)
                {
                    try
                    {
                        if (Session[chkvalueid].ToString() == "1")
                            chkValue.Checked = true;
                        else
                            chkValue.Checked = false;
                    }
                    catch
                    {
                    }

                    totalActualHrs = 0.00;
                    totalBillable = 0;
                    totalBudgeted = 0;
                    totalRevisedBudgeted = 0;
                    GetProjectDetails();



                }

                if (Request.QueryString["opmode"] != null)
                {
                    editid = 0;
                    UpdateDeleteProjectEstimation();
                }
                pnlEst2.Enabled = true;
                chkValue.Enabled = true;

            }
            else
            {
                if (mode != "Add")
                    lnkChangeHistory.Visible = true;

            }
            Session["AudProjectId"] = Convert.ToInt32(ProjectEditId);
            if (Convert.ToInt32(Session["UserRoleID"]) == 2)
            {
                txtPOComments.Visible = false;
                Label5.Visible = false;

                DataSet dsTimes = BaseDAL.ExecuteDataSet("Select * From Project Where ProjectManager = '" + Session["UserID"] + "' And ProjectId ='" + ProjectEditId + "'");

                if (dsTimes.Tables[0].Rows.Count > 0 && mode != "Add")
                {
                    btnEdit.Visible = true;
                    btnClone.Visible = true;
                    //pnlCloneProject.Visible = true;
                    pnlCloneProject.Style.Add("display", "block");
                }

                else
                {
                    btnEdit.Visible = false;
                    btnClone.Visible = false;
                    //pnlCloneProject.Visible = false;
                    pnlCloneProject.Style.Add("display", "none");
                }

                try
                {
                    EditFlag = Convert.ToInt32(Session[EditFlag]);
                }
                catch
                {
                    EditFlag = 0;
                }

            }

            if (Convert.ToInt32(Session["EditFlag"]) == 1)
            {
                Panel1.Visible = true;
                panEdit.Visible = true;
                pnlPopup1.Visible = true;
                pnlPopup2.Visible = true;
                LinkButton1.Enabled = true;
                editClickFlag = 1;
            }
            txtProjectName.Attributes.Add("onkeypress", "return CheckTextValue(event,this);");
            txtProjectCode.Attributes.Add("onkeypress", "return CheckTextValue(event,this);");
            txtProjectShortName.Attributes.Add("onkeypress", "return CheckTextValue(event,this);");
            txtPMComments.Attributes.Add("onkeypress", "return CheckTextValue(event,this);");
            txtDeliveryCommments.Attributes.Add("onkeypress", "return CheckTextValue(event,this);");
            txtdevURL.Attributes.Add("onkeypress", "return CheckTextValue(event,this);");
            txtDemoUrl.Attributes.Add("onkeypress", "return CheckTextValue(event,this);");
            txtQAUrl.Attributes.Add("onkeypress", "return CheckTextValue(event,this);");
            txtProductionurl.Attributes.Add("onkeypress", "return CheckTextValue(event,this);");
            ResourceHistoryLogic();



        }
        [System.Web.Services.WebMethod]
        public static string CheckProjectDuplication(string projectName, string projectShortName)
        {
            int projId = ProjectId;

            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProjectName", projectName.Trim()));
            parameter.Add(new SqlParameter("@ProjectShortName", projectShortName.Trim()));
            // Return value as parameter
            parameter.Add(new SqlParameter("@Status", 1));
            parameter[2].Direction = ParameterDirection.InputOutput;

            BaseDAL.ExecuteSPScalar("CheckProjectDuplication", parameter);
            string status = parameter[2].Value.ToString();

            return status;
        }

        #endregion
        #region"Control Events"

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IFormatProvider culture = new CultureInfo("fr-Fr", true);
            bool isDatesValid = true;
            String formats = "dd/MM/yyyy";

            /* Validating dates */
            string strProjectStartDate = txtProjStartdate.Text;
            string strProjectDeliverydate = txtDelCal.Text;
            string strRevisedDeliveryDate = txtRDel.Text;
            string strMaintClosingDate = txtMaintClosingDate.Text;

            lblDateError.Text = string.Empty;
            lblDateError2.Text = string.Empty;
            lblDateError3.Text = string.Empty;
            lblDateError4.Text = string.Empty;

            DateTime dtProjectStartDate = DateTime.MinValue;
            DateTime dtProjectDeliverydate = DateTime.MinValue;
            DateTime dtRevisedDeliveryDate = DateTime.MinValue;
            DateTime dtMaintClosingDate = DateTime.MinValue;

            //Validating Project Start Date and Delivery Date
            if (!string.IsNullOrEmpty(strProjectStartDate) && !DateTime.TryParseExact(strProjectStartDate, formats, culture, DateTimeStyles.None, out dtProjectStartDate))
            {
                lblDateError.Text = "Project Start Date format is incorrect.";
                return;
            }
            if (!string.IsNullOrEmpty(strProjectDeliverydate) && !DateTime.TryParseExact(strProjectDeliverydate, formats, culture, DateTimeStyles.None, out dtProjectDeliverydate))
            {
                lblDateError.Text = "Delivery Date  format is incorrect.";
                return;
            }

            if (!string.IsNullOrEmpty(strProjectStartDate) && !string.IsNullOrEmpty(strProjectDeliverydate) && dtProjectStartDate > dtProjectDeliverydate)
            {
                isDatesValid = false;
                lblDateError2.Text = "Delivery Date should be greater than Project Start Date.";
            }

            // Validating Revised Delivery Date and Maintanance Closing Date
            if (!string.IsNullOrEmpty(strRevisedDeliveryDate) && !DateTime.TryParseExact(strRevisedDeliveryDate, formats, culture, DateTimeStyles.None, out dtRevisedDeliveryDate))
            {
                lblDateError.Text = "Revised Delivery Date  format is incorrect.";
                return;
            }

            if (!string.IsNullOrEmpty(strMaintClosingDate) && !DateTime.TryParseExact(strMaintClosingDate, formats, culture, DateTimeStyles.None, out dtMaintClosingDate))
            {
                lblDateError.Text = "Maintanance Closing Date  format is incorrect.";
                return;
            }

            if (!string.IsNullOrEmpty(strProjectStartDate) && !string.IsNullOrEmpty(strRevisedDeliveryDate) && dtProjectStartDate > dtRevisedDeliveryDate)
            {
                isDatesValid = false;
                lblDateError4.Text = "Revised Delivery Date should be greater than Project Start Date.";
            }

            if (!string.IsNullOrEmpty(strRevisedDeliveryDate))
            {
                if (!string.IsNullOrEmpty(strProjectDeliverydate) && !string.IsNullOrEmpty(strMaintClosingDate) && dtRevisedDeliveryDate > dtMaintClosingDate)
                {
                    isDatesValid = false;
                    lblDateError3.Text = "Maintenance Closing Date should be greater than Revised Delivery Date.";
                }
            }

            else
            {
                if (!string.IsNullOrEmpty(strMaintClosingDate) && dtProjectDeliverydate > dtMaintClosingDate)
                {
                    isDatesValid = false;
                    lblDateError3.Text = "Maintenance Closing Date should be greater than Delivery Date.";
                }
                else
                {
                    if (!string.IsNullOrEmpty(strMaintClosingDate) && dtProjectStartDate > dtMaintClosingDate)
                    {
                        isDatesValid = false;
                        lblDateError3.Text = "Maintenance Closing Date should be greater than ProjectStartDate.";
                    }

                }
            }

            if (!isDatesValid)
                return;
            /* Date validation ends here */

            if (mode == "Add")
            {
                if (!string.IsNullOrEmpty(strMaintClosingDate))
                {
                    if (dtMaintClosingDate.Date < DateTime.Today.Date)
                    {
                        lblDateError3.Text = "Maintenance Closing Date should not be a past date.";
                        return;
                    }
                    else
                    {
                        AddInfo();
                    }
                }


            }
            if (Session["ProjectEditId"] != null || (Convert.ToInt16(Request.QueryString["ProjectEditId"]) != 0 && mode == "Edit"))
            {
                string query = string.Format("select IsActive from Project where ProjectName='{0}'", txtProjectName.Text);
                object status = PMOscar.BaseDAL.ExecuteScalar(query);
                if (optActive.Checked)
                {
                    optActive.Checked = true;
                }
                //if(Convert.ToInt16(status)==1)
                //{
                //    optActive.Checked = true;
                //}
                else
                {
                    OptInactive.Checked = false;
                }

                String dateString = txtProjStartdate.Text;
                DateTime dateValue;

                if (dateString != "" && DateTime.TryParseExact(dateString, formats,
                                                               new CultureInfo("en-US"),
                                                               DateTimeStyles.None,
                                                               out dateValue))
                {
                    DateTime dt = DateTime.ParseExact(dateString, "dd/MM/yyyy", culture, DateTimeStyles.NoCurrentDateDefault);
                    ClProjectStart.SelectedDate = dt;
                }
                else if (dateString != "")
                {

                    lblDateError.Text = "Project StartDate format is incorrect.";
                    return;
                }
                dateString = txtDelCal.Text;
                if (dateString != "" && DateTime.TryParseExact(dateString, formats,
                                                               new CultureInfo("en-US"),
                                                               DateTimeStyles.None,
                                                               out dateValue))
                {
                    DateTime dt = DateTime.ParseExact(dateString, "dd/MM/yyyy", culture, DateTimeStyles.NoCurrentDateDefault);
                    ClDelivery.SelectedDate = dt;
                }
                else if (dateString != "")
                {

                    lblDateError.Text = "Delivery Date  format is incorrect.";
                    return;
                }
                dateString = txtRDel.Text;
                if (dateString != "" && DateTime.TryParseExact(dateString, formats,
                                                               new CultureInfo("en-US"),
                                                               DateTimeStyles.None,
                                                               out dateValue))
                {
                    DateTime dt = DateTime.ParseExact(dateString, "dd/MM/yyyy", culture, DateTimeStyles.NoCurrentDateDefault);
                    ClRevisedDelivery.SelectedDate = dt;
                }
                else if (dateString != "")
                {

                    lblDateError.Text = "Revised Delivery Date  format is incorrect.";
                    return;
                }
                dateString = txtMaintClosingDate.Text;
                if (dateString != "" && DateTime.TryParseExact(dateString, formats,
                                                               new CultureInfo("en-US"),
                                                               DateTimeStyles.None,
                                                               out dateValue))
                {
                    DateTime dt = DateTime.ParseExact(dateString, "dd/MM/yyyy", culture, DateTimeStyles.NoCurrentDateDefault);
                    ClMaintClosing.SelectedDate = dt;
                }
                else if (dateString != "")
                {
                    lblDateError.Text = "Maintanance Closing Date  format is incorrect.";
                    return;
                }

                //This the case of normal estimation edit.
                if (ProjectEditId == 0 && Convert.ToInt16(Request.QueryString["ProjectEstimationID"]) != 0)
                {
                    ProjectEditId = Convert.ToInt16(Session["ProjectEditId"]);//CHK123

                    //  ProjectEditId = Convert.ToInt16(ViewState["ProjectEditId"]);//CHK124
                    UpdateInfo();
                }
                //This is the case of normal project details update
                else if (ProjectEditId > 0)
                {
                    UpdateInfo();
                }
                //This is the case of adding new project, where sessionID is there with another projects'multi session issue
                else
                {
                    AddInfo();

                }
            }
            //CHK123
            else if (Session["ProjectEditId"] != null || (Convert.ToInt16(Request.QueryString["ProjectEditId"]) != 0 && Request.QueryString["opmode"] == "Delete"))
            {
                //This the case of normal estimation edit.
                if (ProjectEditId == 0 && Convert.ToInt16(Request.QueryString["ProjectEstimationID"]) != 0)
                {
                    ProjectEditId = Convert.ToInt16(Session["ProjectEditId"]);//CHK123

                    //  ProjectEditId = Convert.ToInt16(ViewState["ProjectEditId"]);//CHK124
                    UpdateInfo();
                }
                //This is the case of normal project details update
                else if (ProjectEditId > 0)
                {

                    UpdateInfo();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            //Page.RegisterStartupScript("myScript", "<script language=JavaScript>Alert('yyyyggg');</script>");
            //Page.RegisterStartupScript("myScript", "<script language='JavaScript'>alert('yyyyggg');</script>");
            Response.Redirect("ProjectListing.aspx");
            EditFlag = 1;
            editClickFlag = 1;
            Session["EditFlag"] = EditFlag;
            Session["EditClickFlag"] = editClickFlag;
            BindEstimationGrid();

        }

        protected void btnEstimationSave_Click(object sender, EventArgs e)
        {
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProjectId", 1));
            parameter.Add(new SqlParameter("@ProjectName", txtProjectName.Text.Trim()));
            parameter.Add(new SqlParameter("@BillableHours", Convert.ToDecimal(txtBillableHrs.Text.Trim())));
            parameter.Add(new SqlParameter("@BudgetHours", Convert.ToDecimal(txtBudHours.Text.Trim())));
            parameter.Add(new SqlParameter("@RevisedBudgetHours", Convert.ToDecimal(txtRevBudHours.Text.Trim())));
            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@ShortName", txtProjectShortName.Text.Trim()));
            parameter.Add(new SqlParameter("@OpMode", "INSERT"));
            parameter.Add(new SqlParameter("@status", "Insert"));
            parameter[11].Direction = ParameterDirection.Output;
            try
            {
                int ProjectId = PMOscar.BaseDAL.ExecuteSPScalar("[ProjectEstimationOperations]", parameter);
                string status = parameter[11].Value.ToString();
                if (status == "5")
                {
                    lbErorMsg.Text = "Project Name already Exists.";
                }
                else
                {
                    Response.Redirect("ProjectListing.aspx");
                    Session[chkvalueid] = "0";
                }
            }
            catch
            {

            }

        }
        protected void btnEstCancel_Click(object sender, EventArgs e)
        {
            ClearEstimationDetails();
            ddlPhase.Focus();
            EditFlag = 1;
            editClickFlag = 1;
            Session["EditFlag"] = EditFlag;
            Session["EditClickFlag"] = editClickFlag;
            BindEstimationGrid();

            btnEdit.Enabled = true; //Checking
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BindEstimationDT(); //..Checking

            EditFlag = 1;
            editClickFlag = 1;
            bool isComment = true;
            Session["EditFlag"] = EditFlag;
            Session["EditClickFlag"] = editClickFlag;
            if (txtEstID.Text != "")
            {
                if (string.IsNullOrEmpty(txtComments.Text.ToString()))
                {
                    lblComments.Text = "Please Enter a Comment.";
                    isComment = false;
                }
            }
            if (txtRevBudHours.Text == "" || txtRevBudHours.Text == "0")
            {
                txtRevBudHours.Text = txtBudHours.Text;
            }

            if (ddlPhase.SelectedItem.Text == "Select")
            {
                lblValPhase.Text = "Select Phase";
            }
            if (ddlRole.SelectedItem.Text == "Select")
            {
                lblValRole.Text = "Select Role";
            }
            else if (ddlPhase.SelectedItem.Text != "Select" && ddlRole.SelectedItem.Text != "Select" && isComment)
            {

                dtEstimation = Session["dtEstimation"] != null ? (DataTable)Session["dtEstimation"] : dtEstimation;
                if (dtEstimation.Rows.Count > 0)
                {
                    DataRow[] dr11;
                    if (txtEstID.Text != "")
                    {
                        editid = Convert.ToInt64(txtEstID.Text);
                    }
                    if (editid != 0)
                    {
                        dr11 = dtEstimation.Select("PhaseID=" + ddlPhase.SelectedItem.Value + " And   RoleId=" + ddlRole.SelectedItem.Value + "  and ProjectEstimationID <>" + editid);
                    }
                    else
                    {
                        dr11 = dtEstimation.Select("PhaseID=" + ddlPhase.SelectedItem.Value + " And   RoleId=" + ddlRole.SelectedItem.Value);
                    }
                    lblesterror.Text = "";
                    if (dr11.Length > 0)
                    {
                        lblesterror.Text = "Same phase and role cannot be repeatedly added for the same project.";
                        if (Session["update"].ToString() == ViewState["update"].ToString())    // If page not Refreshed
                        {

                            TextBox1.Focus();

                        }

                        else  // If Page Refreshed
                        {

                            TextBox1.Focus();

                        }
                        return;
                    }

                    int ProjectEstimationID = Convert.ToInt32(Request.QueryString["ProjectEstimationID"]);
                    string query = "DELETE FROM projectestimation WHERE ProjectEstimationID = " + ProjectEstimationID;
                    DataTable dt = PMOscar.BaseDAL.ExecuteDataTable(query);
                    insertEstimationDetails();
                    BindEstimationGrid();
                    ClearEstimationDetails();

                }

                else
                {
                    insertEstimationDetails();
                    BindEstimationGrid();
                    ClearEstimationDetails();

                }

            }

            if (Session["update"].ToString() == ViewState["update"].ToString())    // If page not Refreshed
            {

                TextBox1.Focus();

            }

            else  // If Page Refreshed
            {

                TextBox1.Focus();

            }

            ddlPhase.Focus();
            BindEstimationGrid();

            LinkButton1.Enabled = true; //checking;
            btnEdit.Enabled = true; //checking;
        }
        protected override void OnPreRender(EventArgs e)
        {
            ViewState["update"] = Session["update"];
            base.OnPreRender(e);
        }
        protected void grdEstimation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[11].Visible = false;
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    totalBillable += Convert.ToInt32(e.Row.Cells[4].Text.ToString());
                    totalBudgeted += Convert.ToInt32(e.Row.Cells[5].Text.ToString());
                    totalRevisedBudgeted += Convert.ToInt32(e.Row.Cells[6].Text.ToString());
                    LinkButton lnk = (LinkButton)(e.Row.FindControl("lnkActualHours"));
                    LinkButton lnk1 = (LinkButton)(e.Row.FindControl("lnkActualHours1"));
                    totalActualHoursOriginal += lnk1.Text.ToString() != "" ? Convert.ToDouble(lnk1.Text) * 1.000 : 0;

                    if (iFlag == 1)
                    {
                        totalActualHrs += lnk.Text.ToString() != "" ? Convert.ToDouble(lnk.Text) * 1.000 : 0;
                    }

                    if (iFlag == 2)
                    {
                        if (e.Row.Cells[2].Text == "Proposal" || e.Row.Cells[2].Text == "Value Added Services")
                            totalActualHrs += 0;
                        else
                            totalActualHrs += lnk.Text.ToString() != "" ? Convert.ToDouble(lnk.Text) * 1.000 : 0;
                    }

                }
                e.Row.Cells[11].Visible = false;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "Total";
                e.Row.Cells[4].Text = totalBillable.ToString();
                e.Row.Cells[5].Text = totalBudgeted.ToString();
                e.Row.Cells[6].Text = totalRevisedBudgeted.ToString();
                e.Row.Cells[10].Text = totalActualHoursOriginal.ToString(); // Changed now
                e.Row.Cells[11].Text = totalActualHrs.ToString(); // added now              
                e.Row.Cells[11].Visible = false;
            }

            e.Row.Cells[1].Visible = false;

            if (EditFlag == 1 && editClickFlag == 1)
            {
                e.Row.Cells[13].Visible = true;
                e.Row.Cells[14].Visible = true;
            }
            else if (EditFlag == 1 && editClickFlag == 0)
            {
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
            }

            if (EditFlag == 0)
            {
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
            }

            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
        }
        protected void grdEstimation_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            ClProjectStart.Visible = false;
            ClRevisedDelivery.Visible = false;
            ClMaintClosing.Visible = false;
            ClDelivery.SelectedDates.Clear();
            ClDelivery.Visible = true;

            Panel1.Visible = false;
            panEdit.Visible = false;

            if (ProjectEditId == 0)
            {
                lnkChangeHistory.Visible = false;
                btnEdit.Visible = false;
                btnClone.Visible = false;
                pnlCloneProject.Style.Add("display", "none");
            }
            pnlPopup1.Style.Add("display", "none");
            pnlPopup2.Style.Add("display", "none");
            txtDelCal.Focus();
        }
        protected void ClDelivery_SelectionChanged(object sender, EventArgs e)
        {
            txtDelCal.Text = ClDelivery.SelectedDate.ToString("dd/MM/yyyy");
            ClDelivery.Visible = false;

            if (ProjectEditId == 0)
            {
                lnkChangeHistory.Visible = false;
                btnEdit.Visible = false;
                btnClone.Visible = false;
                pnlCloneProject.Style.Add("display", "none");
            }
            pnlPopup1.Style.Add("display", "none");
            pnlPopup2.Style.Add("display", "none");
            txtDelCal.Focus();
        }
        protected void btnRDel_Click(object sender, EventArgs e)
        {
            ClProjectStart.Visible = false;
            ClMaintClosing.Visible = false;
            ClDelivery.Visible = false;
            ClRevisedDelivery.SelectedDates.Clear();
            ClRevisedDelivery.Visible = true;
            Panel1.Visible = false;
            panEdit.Visible = false;

            if (ProjectEditId == 0)
            {
                lnkChangeHistory.Visible = false;
                btnEdit.Visible = false;
                btnClone.Visible = false;
                pnlCloneProject.Style.Add("display", "none");
            }
            pnlPopup1.Style.Add("display", "none");
            pnlPopup2.Style.Add("display", "none");
            txtRDel.Focus();
        }
        protected void ClRevisedDelivery_SelectionChanged(object sender, EventArgs e)
        {
            txtRDel.Text = ClRevisedDelivery.SelectedDate.ToString("dd/MM/yyyy");
            ClRevisedDelivery.Visible = false;
            ClMaintClosing.SelectedDate = ClRevisedDelivery.SelectedDate.AddDays(91);
            txtMaintClosingDate.Text = ClMaintClosing.SelectedDate.ToString("dd/MM/yyyy");

            if (ProjectEditId == 0)
            {
                lnkChangeHistory.Visible = false;
                btnEdit.Visible = false;
                btnClone.Visible = false;
                pnlCloneProject.Style.Add("display", "none");
            }
            pnlPopup1.Style.Add("display", "none");
            pnlPopup2.Style.Add("display", "none");
            txtRDel.Focus();
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            lblProjectStatus.Text = "Edit Project";
            PnlEdit.Enabled = true;
            pnlEst.Enabled = true;
            pnlEst2.Enabled = true;

            Session[chkvalueid] = "0";
            chkValue.Checked = false;

            EditFlag = 1;
            editClickFlag = 1;
            Session["EditFlag"] = EditFlag;
            Session["EditClickFlag"] = editClickFlag;
            BindEstimationGrid();

            Panel1.Visible = true;
            panEdit.Visible = true;
            pnlPopup1.Visible = true;
            pnlPopup2.Visible = true;

        }
        protected void txtProjectCode_TextChanged(object sender, EventArgs e)
        {
            lblPctName.Visible = false;
            lblSName.Visible = false;
            lblPjctCode.Visible = false;
        }
        protected void lbBack_Click(object sender, EventArgs e)
        {

            if (Session["dtDatas"] != null)
            {
                DataTable dtDatas = (DataTable)Session["dtDatas"];
                if (dtDatas.Rows.Count > 0)
                {
                    weekindex = Convert.ToInt32(dtDatas.Rows[0]["WeekIndex"].ToString());
                    month = Convert.ToInt32(dtDatas.Rows[0]["Month"].ToString());
                    year = Convert.ToInt32(dtDatas.Rows[0]["Year"].ToString());

                }
            }

            Response.Redirect("ResourcePlanning.aspx?redirect=true&weekindex=" + weekindex + "&month=" + month + "&year=" + year);
        }
        protected void lnlDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectDashboard.aspx");
        }
        protected void btnEstSave_Click(object sender, EventArgs e)
        {
            if (ProjectEditId != 0)
            {
                deleteEstimationDetails();
            }
            insertEstimationDetails();

        }
        protected void btnProjStart_Click(object sender, EventArgs e)
        {

            ClRevisedDelivery.Visible = false;
            ClMaintClosing.Visible = false;
            ClDelivery.Visible = false;
            ClProjectStart.SelectedDates.Clear();
            ClProjectStart.Visible = true;

            Panel1.Visible = false;

            panEdit.Visible = false;

            if (ProjectEditId == 0)
            {
                lnkChangeHistory.Visible = false;
                btnEdit.Visible = false;
                btnClone.Visible = false;
                pnlCloneProject.Style.Add("display", "none");
            }

            pnlPopup1.Style.Add("display", "none");
            pnlPopup2.Style.Add("display", "none");
            txtProjStartdate.Focus();
        }
        protected void btnManiteClosing_Click(object sender, EventArgs e)
        {

            ClRevisedDelivery.Visible = false;
            ClDelivery.Visible = false;
            ClProjectStart.Visible = false;
            ClMaintClosing.SelectedDates.Clear();
            ClMaintClosing.Visible = true;
            Panel1.Visible = false;
            panEdit.Visible = false;

            if (ProjectEditId == 0)
            {
                lnkChangeHistory.Visible = false;
                btnEdit.Visible = false;
                btnClone.Visible = false;
                pnlCloneProject.Style.Add("display", "none");
            }
            pnlPopup1.Style.Add("display", "none");
            pnlPopup2.Style.Add("display", "none");
            txtMaintClosingDate.Focus();
        }
        protected void ClProjectStart_SelectionChanged(object sender, EventArgs e)
        {
            txtProjStartdate.Text = ClProjectStart.SelectedDate.ToString("dd/MM/yyyy");
            ClProjectStart.Visible = false;

            if (ProjectEditId == 0)
            {
                lnkChangeHistory.Visible = false;
                btnEdit.Visible = false;
                btnClone.Visible = false;
                pnlCloneProject.Style.Add("display", "none");
            }
            pnlPopup1.Style.Add("display", "none");
            pnlPopup2.Style.Add("display", "none");
            txtProjStartdate.Focus();

        }

        protected void ClRevisedDelivery_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            txtRDel.Focus();
        }

        protected void ClDelivery_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            txtDelCal.Focus();
        }
        //For setting focus on month change.
        protected void ClProjectStart_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            txtProjStartdate.Focus();
        }

        //For setting focus on month change.
        protected void ClMaintClosing_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            txtMaintClosingDate.Focus();
        }
        protected void ClMaintClosing_SelectionChanged(object sender, EventArgs e)
        {
            txtMaintClosingDate.Text = ClMaintClosing.SelectedDate.ToString("dd/MM/yyyy");
            ClMaintClosing.Visible = false;

            if (ProjectEditId == 0)
            {
                lnkChangeHistory.Visible = false;
                btnEdit.Visible = false;
                btnClone.Visible = false;
                pnlCloneProject.Style.Add("display", "none");
            }
            pnlPopup1.Style.Add("display", "none");
            pnlPopup2.Style.Add("display", "none");
            txtMaintClosingDate.Focus();
        }
        protected void grdEstimation_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = Session["dtEstimation"] as DataTable;

            if (dataTable != null)
            {
                EditFlag = 1;
                editClickFlag = 1;
                dataTable.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                grdEstimation.DataSource = dataTable;
                grdEstimation.DataBind();
            }
        }

        protected void btnCloneProject_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["ProjectEditId"] != null)
                {
                    ProjectEditId = Convert.ToInt32(Request.QueryString["ProjectEditId"]);
                    ProjectId = ProjectEditId;
                }
                else
                {
                    return;
                }

                string query = string.Format("select ResourceID from ProjectResources where ProjectID ='{0}' and IsActive='{1}'", ProjectId, 1);
                string query1 = string.Format("select ProjStartDate from Project where ProjectId='{0}'", ProjectId);
                string query2 = string.Format("select MaintClosingDate from Project where ProjectId='{0}'", ProjectId);
                DataTable projectResources = PMOscar.BaseDAL.ExecuteDataTable(query);

                object cloneprojstrtdate = PMOscar.BaseDAL.ExecuteScalar(query1);
                object cloneMaintclsdate = PMOscar.BaseDAL.ExecuteScalar(query2);

                string Prjstrtdate = Convert.ToString(cloneprojstrtdate);
                string Maintclsdate = Convert.ToString(cloneMaintclsdate);

                DateTime DtProjectStartdate = Convert.ToDateTime(Prjstrtdate);
                DateTime DtMaintclsdate = Convert.ToDateTime(Maintclsdate);


                DateTime DTIncprojstrtdate = DtProjectStartdate.AddMonths(1);
                DateTime DTIncMaintclsdate = DtMaintclsdate.AddMonths(1);

                DateTime date = DtProjectStartdate;
                DateTime firstOfNextMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1);
                DateTime lastDatmaintcls = firstOfNextMonth.AddMonths(1).AddDays(-1);

                int workOrderid = Convert.ToInt16(workOrderList.SelectedValue);
                string ClnIncprojstrtdate = firstOfNextMonth.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                string ClnIncMaintclsdate = lastDatmaintcls.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                // Clone project
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ProjectID", ProjectEditId));
                parameter.Add(new SqlParameter("@ProjectName", txtCloneProjectName.Text.Trim()));
                parameter.Add(new SqlParameter("@ProjectShortName", txtCloneProjectShortName.Text.Trim()));
                parameter.Add(new SqlParameter("@UserID", Convert.ToInt32(Session["UserID"])));
                // Return value as parameter
                parameter.Add(new SqlParameter("@Status", 1));
                parameter.Add(new SqlParameter("@StartDate", ClnIncprojstrtdate));
                parameter.Add(new SqlParameter("@EndDate", ClnIncMaintclsdate));
                parameter.Add(new SqlParameter("@WorkorderID", workOrderid));
                if (BugzillaList.SelectedValue != null)
                {
                    parameter.Add(new SqlParameter("@BugzillaProjectId", BugzillaList.SelectedValue));
                }
                else
                {
                    parameter.Add(new SqlParameter("@BugzillaProjectId", DBNull.Value));
                }

                parameter[4].Direction = ParameterDirection.InputOutput;

                BaseDAL.ExecuteSPScalar("CloneProject", parameter);
                string status = parameter[4].Value.ToString();

                if (status == "1") // Project Name duplicated
                {
                    Page.RegisterStartupScript("myScript", "<script language='JavaScript'>alert('Project name already exists!');</script>");
                    mdlClonePopup.Show();
                    Page.RegisterStartupScript("myScript", "<script language='JavaScript'>return false;</script>");

                }
                else if (status == "2") // Project Short Name duplicated
                {
                    Page.RegisterStartupScript("myScript", "<script language='JavaScript'>alert('Project short name already exists!'); </script>");
                    mdlClonePopup.Show();
                    Page.RegisterStartupScript("myScript", "<script language='JavaScript'>return false;</script>");
                }
                else
                {





                    Object projectid = PMOscar.BaseDAL.ExecuteScalar("select max(projectid) from Project");

                    string phase = ddlCurrentPhase.SelectedValue;
                    int phaseId = Convert.ToInt16(phase);
                    string clnIncProjstrtdate1 = DTIncprojstrtdate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    string clnIncMaintclsdate2 = lastDatmaintcls.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    int isupdate = 0;
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@ProjectName", txtCloneProjectName.Text.Trim()));
                    parameter.Add(new SqlParameter("@ProjectID", projectid));
                    parameter.Add(new SqlParameter("@StartDate", clnIncProjstrtdate1));
                    parameter.Add(new SqlParameter("@EndDate", clnIncMaintclsdate2));
                    parameter.Add(new SqlParameter("@ISUpdate", isupdate));
                    parameter.Add(new SqlParameter("@isActive", optActive.Checked == true ? 1 : 0));
                    parameter.Add(new SqlParameter("@phaseId", phaseId));



                    try
                    {
                        PMOscar.BaseDAL.ExecuteSPScalar("CreateProjectInTimeTracker", parameter);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }




                    List<int> ids = new List<int>();
                    foreach (ListItem lst in lstResource.Items)
                    {
                        if (lst.Selected)
                        {
                            ids.Add(Convert.ToInt32(lst.Value));
                        }
                    }
                    string result = string.Join(",", ids.Select(n => n.ToString()).ToArray());
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@ProjectId", projectid));
                    parameter.Add(new SqlParameter("@ResourceID", result));
                    if (result != String.Empty)
                    {
                        int value = PMOscar.BaseDAL.ExecuteSPScalar("AddProjectResources", parameter);
                    }

                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@ProjectID", projectid));

                    PMOscar.BaseDAL.ExecuteSPScalar("UpdateTimeTrackerProject", parameter);
                    Session[chkvalueid] = "0";
                    // Redirect to project listing page
                    SendEmail(txtCloneProjectName.Text.Trim(), ddlProjectType.SelectedItem.Text, ClnIncprojstrtdate, ClnIncMaintclsdate, ddlProjectOwner.SelectedItem.Text, workOrderList.SelectedItem.Text);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Project has cloned successfully.');window.location ='ProjectListing.aspx';",
                    true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region"Methods"
        private void GetProjectDetails()
        {
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProjectId", ProjectEditId));
            parameter.Add(new SqlParameter("@ProjectCode", 1));
            parameter.Add(new SqlParameter("@ProjectName", ""));
            parameter.Add(new SqlParameter("@ShortName", ""));
            parameter.Add(new SqlParameter("@PhaseID", 1));
            parameter.Add(new SqlParameter("@ProjectType", ""));
            parameter.Add(new SqlParameter("@ProjectOwner", 1));
            parameter.Add(new SqlParameter("@ProjectManager", 1));
            parameter.Add(new SqlParameter("@ProjectManagerName", ""));
            parameter.Add(new SqlParameter("@DeliveryDateText", DateTime.Now));
            parameter.Add(new SqlParameter("@RevisedDeliveryDateText", DateTime.Now));
            parameter.Add(new SqlParameter("@ApprvChangeRequest", 1));
            parameter.Add(new SqlParameter("@PMComments", ""));
            parameter.Add(new SqlParameter("@POComments", ""));
            parameter.Add(new SqlParameter("@DeliveryComments", ""));
            parameter.Add(new SqlParameter("@isActive", 1));
            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@OpMode", "SELECTALL"));
            parameter.Add(new SqlParameter("@status", "Insert"));
            parameter.Add(new SqlParameter("@ProjStartDateText", DateTime.Now));
            parameter.Add(new SqlParameter("@MaintClosingDateText", DateTime.Now));
            parameter.Add(new SqlParameter("@devurl", txtdevURL.Text));
            parameter.Add(new SqlParameter("@qaurl", txtQAUrl.Text));
            parameter.Add(new SqlParameter("@demourl", txtDemoUrl.Text));
            parameter.Add(new SqlParameter("@productionurl", txtProductionurl.Text));
            parameter.Add(new SqlParameter("@ClientId", ddlClient.SelectedItem.Value));
            parameter.Add(new SqlParameter("@ProgramId", ddlProgram.SelectedItem.Value));
            parameter.Add(new SqlParameter("@Utilization", ddlUtilization.SelectedItem.Value));
            parameter.Add(new SqlParameter("CostCentreID", 1));
            parameter.Add(new SqlParameter("@ShowInDashboard", 1));
            parameter.Add(new SqlParameter("@SVNPath", txtSVNUrl.Text));
            parameter.Add(new SqlParameter("@ProjectDetails", txtProjectDetails.Text));
            parameter.Add(new SqlParameter("@WorkOrderID", workOrderList.SelectedValue));

            parameter[21].Direction = ParameterDirection.InputOutput;

            DataSet dst = PMOscar.BaseDAL.ExecuteSPDataSet("ProjectOperations", parameter);
            if (dst.Tables[0].Rows.Count > 0)
            {
                txtProjectName.Text = dst.Tables[0].Rows[0].ItemArray[0].ToString();
                txtProjectShortName.Text = dst.Tables[0].Rows[0].ItemArray[1].ToString();
                ddlProjectType.SelectedValue = dst.Tables[0].Rows[0].ItemArray[2].ToString();
                ddlProjectOwner.SelectedValue = dst.Tables[0].Rows[0].ItemArray[3].ToString();
                ddlProjectManager.SelectedValue = dst.Tables[0].Rows[0].ItemArray[4].ToString();
                txtDelCal.Text = Convert.ToString(dst.Tables[0].Rows[0].ItemArray[5]);
                txtRDel.Text = Convert.ToString(dst.Tables[0].Rows[0].ItemArray[6]);
                txtProjStartdate.Text = Convert.ToString(dst.Tables[0].Rows[0].ItemArray[13]);
                txtMaintClosingDate.Text = Convert.ToString(dst.Tables[0].Rows[0].ItemArray[14]);
                if (txtDelCal.Text == String.Empty || txtDelCal.Text == "01/01/1900 12:00:00 AM" || txtDelCal.Text == "1/1/1900 12:00:00 AM")
                {
                    txtDelCal.Text = "";
                }
                else
                {
                    ClDelivery.SelectedDate = Convert.ToDateTime(dst.Tables[0].Rows[0].ItemArray[5]);
                    txtDelCal.Text = ClDelivery.SelectedDate.ToString("dd/MM/yyyy");
                }

                if (txtRDel.Text == String.Empty || txtRDel.Text == "1900/01/01 12:00:00 AM" || txtRDel.Text == "1900 12:00:00 AM")
                {
                    txtRDel.Text = "";
                }

                else
                {
                    ClRevisedDelivery.SelectedDate = Convert.ToDateTime(dst.Tables[0].Rows[0].ItemArray[6]);
                    txtRDel.Text = ClRevisedDelivery.SelectedDate.ToString("dd/MM/yyyy");
                }

                if (txtProjStartdate.Text == String.Empty || txtProjStartdate.Text == "01/01/1900 12:00:00 AM" || txtProjStartdate.Text == "1/1/1900 12:00:00 AM")
                {
                    txtProjStartdate.Text = "";
                }

                else
                {
                    ClProjectStart.SelectedDate = Convert.ToDateTime(dst.Tables[0].Rows[0].ItemArray[13]);
                    txtProjStartdate.Text = ClProjectStart.SelectedDate.ToString("dd/MM/yyyy");
                }
                if (txtMaintClosingDate.Text == String.Empty || txtMaintClosingDate.Text == "01/01/1900 12:00:00 AM" || txtMaintClosingDate.Text == "1/1/1900 12:00:00 AM")
                {
                    txtMaintClosingDate.Text = "";
                }

                else
                {
                    ClMaintClosing.SelectedDate = Convert.ToDateTime(dst.Tables[0].Rows[0].ItemArray[14]);
                    txtMaintClosingDate.Text = ClMaintClosing.SelectedDate.ToString("dd/MM/yyyy");
                }

                ddlCurrentPhase.SelectedValue = dst.Tables[0].Rows[0].ItemArray[7].ToString();

                txtPMComments.Text = dst.Tables[0].Rows[0].ItemArray[9].ToString();
                txtDeliveryCommments.Text = dst.Tables[0].Rows[0].ItemArray[10].ToString();
                if (Convert.ToBoolean(dst.Tables[0].Rows[0].ItemArray[11]) == false)
                {
                    optActive.Checked = false;
                    OptInactive.Checked = true;
                }
                else
                {
                    optActive.Checked = true;
                    OptInactive.Checked = false;
                }

             
                txtProjectCode.Text = dst.Tables[0].Rows[0].ItemArray[12].ToString();
                txtPOComments.Text = dst.Tables[0].Rows[0].ItemArray[15].ToString();
                txtdevURL.Text = dst.Tables[0].Rows[0].ItemArray[16].ToString();
                txtDemoUrl.Text = dst.Tables[0].Rows[0].ItemArray[17].ToString();
                txtQAUrl.Text = dst.Tables[0].Rows[0].ItemArray[18].ToString();
                txtProductionurl.Text = dst.Tables[0].Rows[0].ItemArray[19].ToString();
                ddlClient.SelectedValue = dst.Tables[0].Rows[0].ItemArray[20].ToString();
                if (ddlClient.SelectedValue!="0")
                {
                    DataTable dt = PMOscar.BaseDAL.ExecuteDataTable("Select ProgId,ProgName from Program where ClientId=" + Convert.ToInt32(dst.Tables[0].Rows[0].ItemArray[20]) + " order by ProgName");
                    ddlProgram.DataSource = dt;
                    ddlProgram.DataTextField = "ProgName";
                    ddlProgram.DataValueField = "ProgId";
                    ddlProgram.DataBind();
                    ddlProgram.Items.Insert(0, new ListItem("Select", "0"));
                    dt.Dispose();
                }
                ddlProgram.SelectedValue = dst.Tables[0].Rows[0].ItemArray[21].ToString();
                ddlUtilization.SelectedValue = dst.Tables[0].Rows[0].ItemArray[22].ToString();
                ddlcostcentre.SelectedValue = dst.Tables[0].Rows[0].ItemArray[23].ToString();
                workOrderList.SelectedValue = dst.Tables[0].Rows[0].ItemArray[27].ToString();
                BugzillaList.SelectedValue = dst.Tables[0].Rows[0].ItemArray[28].ToString();
                if (workOrderList.SelectedValue != null && workOrderList.SelectedValue != "")
                    Session[Constants.SessionName.WORKORDERID] = workOrderList.SelectedValue;

                //To check whether show in dashboard checkbox should be checked or not
                if (dst.Tables[0].Rows[0].ItemArray[24].ToString() == "1")
                {
                    chkShowinDashboard.Checked = true;
                }
                else
                {
                    chkShowinDashboard.Checked = false;
                }

                txtSVNUrl.Text = dst.Tables[0].Rows[0].ItemArray[25].ToString();
                txtProjectDetails.Text = dst.Tables[0].Rows[0].ItemArray[26].ToString();

            }
            if (Request.QueryString["opmode"] == null)
            {
                BindEstimationGrid();
            }
            else
            {
                dtEstimation = (DataTable)Session["dtEstimation"];
            }

        }

        /// <summary>
        /// Method to get project detals by projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        private DataSet GetProjectDetails(int projectId)
        {
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProjectId", projectId));
            parameter.Add(new SqlParameter("@ProjectCode", 1));
            parameter.Add(new SqlParameter("@ProjectName", ""));
            parameter.Add(new SqlParameter("@ShortName", ""));
            parameter.Add(new SqlParameter("@PhaseID", 1));
            parameter.Add(new SqlParameter("@ProjectType", ""));
            parameter.Add(new SqlParameter("@ProjectManagerName", ""));
            parameter.Add(new SqlParameter("@ProjectOwner", 1));
            parameter.Add(new SqlParameter("@ProjectManager", 1));
            parameter.Add(new SqlParameter("@DeliveryDateText", DateTime.Now));
            parameter.Add(new SqlParameter("@RevisedDeliveryDateText", DateTime.Now));
            parameter.Add(new SqlParameter("@ApprvChangeRequest", 1));
            parameter.Add(new SqlParameter("@PMComments", ""));
            parameter.Add(new SqlParameter("@POComments", ""));
            parameter.Add(new SqlParameter("@DeliveryComments", ""));
            parameter.Add(new SqlParameter("@isActive", 1));
            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@OpMode", "SELECTALL"));
            parameter.Add(new SqlParameter("@status", "Insert"));
            parameter.Add(new SqlParameter("@ProjStartDateText", DateTime.Now));
            parameter.Add(new SqlParameter("@MaintClosingDateText", DateTime.Now));
            parameter.Add(new SqlParameter("@devurl", txtdevURL.Text));
            parameter.Add(new SqlParameter("@qaurl", txtQAUrl.Text));
            parameter.Add(new SqlParameter("@demourl", txtDemoUrl.Text));
            parameter.Add(new SqlParameter("@productionurl", txtProductionurl.Text));
            parameter.Add(new SqlParameter("@ClientId", ddlClient.SelectedItem.Value));
            parameter.Add(new SqlParameter("@ProgramId", ddlProgram.SelectedItem.Value));
            parameter.Add(new SqlParameter("@Utilization", ddlUtilization.SelectedItem.Value));
            parameter.Add(new SqlParameter("@CostCentreID", ddlcostcentre.SelectedItem.Value));
            parameter.Add(new SqlParameter("@ShowInDashboard", 1));
            parameter.Add(new SqlParameter("@WorkOrderID", workOrderList.SelectedValue));
            if (BugzillaList.SelectedValue != null)
            {
                parameter.Add(new SqlParameter("@BugzillaProjetId", BugzillaList.SelectedValue));
            }
            else
            {
                parameter.Add(new SqlParameter("@BugzillaProjetId", DBNull.Value));
            }

            parameter[21].Direction = ParameterDirection.InputOutput;

            DataSet dsProjectDetails = BaseDAL.ExecuteSPDataSet("ProjectOperations", parameter);

            return dsProjectDetails;
        }

        private void AddInfo()
        {

            if (txtBudHours.Text.Trim() == "")
                txtBudHours.Text = "0";

            if (txtRevBudHours.Text.Trim() == "")
                txtRevBudHours.Text = "0";


            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProjectCode", txtProjectCode.Text.Trim()));
            parameter.Add(new SqlParameter("@ProjectId", 1));
            parameter.Add(new SqlParameter("@ProjectName", txtProjectName.Text.Trim()));
            parameter.Add(new SqlParameter("@ShortName", txtProjectShortName.Text.Trim()));
            parameter.Add(new SqlParameter("@PhaseID", ddlCurrentPhase.SelectedItem.Value));
            parameter.Add(new SqlParameter("@ProjectType", ddlProjectType.SelectedItem.Value));
            parameter.Add(new SqlParameter("@ProjectOwner", ddlProjectOwner.SelectedItem.Value));
            parameter.Add(new SqlParameter("@ProjectManagerName", ""));
            parameter.Add(new SqlParameter("@ProjectManager", ddlProjectManager.SelectedItem.Value));

            if (txtDelCal.Text == "")
            {
                parameter.Add(new SqlParameter("@DeliveryDateText", DBNull.Value));
            }
            else
            {
                parameter.Add(new SqlParameter("@DeliveryDateText", txtDelCal.Text));
            }


            if (txtRDel.Text == "")
            {
                parameter.Add(new SqlParameter("@RevisedDeliveryDateText", DBNull.Value));
            }
            else
            {
                parameter.Add(new SqlParameter("@RevisedDeliveryDateText", txtRDel.Text));
            }


            parameter.Add(new SqlParameter("@ApprvChangeRequest", "0"));
            parameter.Add(new SqlParameter("@PMComments", txtPMComments.Text));
            parameter.Add(new SqlParameter("@POComments", txtPOComments.Text));
            parameter.Add(new SqlParameter("@DeliveryComments", txtDeliveryCommments.Text));
            parameter.Add(new SqlParameter("@isActive", optActive.Checked == true ? 1 : 0));
            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@OpMode", "INSERT"));
            parameter.Add(new SqlParameter("@status", "Insert"));
            if (txtProjStartdate.Text == "")
            {
                parameter.Add(new SqlParameter("@ProjStartDateText", DBNull.Value));
            }
            else
            {
                parameter.Add(new SqlParameter("@ProjStartDateText", txtProjStartdate.Text));
            }


            if (txtMaintClosingDate.Text == "")
            {
                parameter.Add(new SqlParameter("@MaintClosingDateText", DBNull.Value));
            }
            else
            {
                parameter.Add(new SqlParameter("@MaintClosingDateText", txtMaintClosingDate.Text));
            }
            parameter.Add(new SqlParameter("@devurl", txtdevURL.Text));
            parameter.Add(new SqlParameter("@qaurl", txtQAUrl.Text));
            parameter.Add(new SqlParameter("@demourl", txtDemoUrl.Text));
            parameter.Add(new SqlParameter("@productionurl", txtProductionurl.Text));


            parameter.Add(new SqlParameter("@ClientId", ddlClient.SelectedItem.Value));
            parameter.Add(new SqlParameter("@ProgramId", ddlProgram.SelectedItem.Value));
            parameter.Add(new SqlParameter("@Utilization", ddlUtilization.SelectedItem.Value));
            parameter.Add(new SqlParameter("@CostCentreID", ddlcostcentre.SelectedItem.Value));
            //To check whether the project should be shown in dashboard
            if (chkShowinDashboard.Checked == true)

                ShowInDashBoard = 1;
            else

                ShowInDashBoard = 0;
            parameter.Add(new SqlParameter("@ShowInDashboard", ShowInDashBoard));
            parameter.Add(new SqlParameter("@SVNPath", txtSVNUrl.Text));
            parameter.Add(new SqlParameter("@ProjectDetails", txtProjectDetails.Text));
            parameter.Add(new SqlParameter("@WorkOrderID", workOrderList.SelectedValue));
            if (BugzillaList.SelectedValue != null)
            {
                parameter.Add(new SqlParameter("@BugzillaProjectId", BugzillaList.SelectedValue));
            }
            else
            {
                parameter.Add(new SqlParameter("@BugzillaProjectId", DBNull.Value));
            }
            parameter[21].Direction = ParameterDirection.InputOutput;

            // Return value as parameter
            SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;
            parameter.Add(returnValue);


            try
            {
                int ProjectId = PMOscar.BaseDAL.ExecuteSPScalar("ProjectOperations", parameter);
                ProjectEditId = Convert.ToInt16(returnValue.Value);
                string status = parameter[21].Value.ToString();



                if (status == "1")
                {

                    Page.RegisterStartupScript("myScript", "<script language='JavaScript'>alert('Project Name already Exists!');</script>");

                }
                else if (status == "2")
                {

                    Page.RegisterStartupScript("myScript", "<script language='JavaScript'>alert('Short name already Exists!');</script>");

                }
                else if (status == "3")
                {

                    Page.RegisterStartupScript("myScript", "<script language='JavaScript'>alert('Project Code already Exists!');</script>");

                }
                else
                {
                    ProjdetailsToTimetracker(0);
                    UpdateTimeTrackerProject();
                    Object projectid = PMOscar.BaseDAL.ExecuteScalar("select max(projectid) from Project");
                    List<int> ids = new List<int>();
                    foreach (ListItem lst in lstResource.Items)
                    {
                        if (lst.Selected)
                        {
                            ids.Add(Convert.ToInt32(lst.Value));
                        }
                    }
                    string result = string.Join(",", ids.Select(n => n.ToString()).ToArray());
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@ProjectId", projectid));
                    parameter.Add(new SqlParameter("@ResourceID", result));
                    if (result != String.Empty)
                    {
                        int value = PMOscar.BaseDAL.ExecuteSPScalar("AddProjectResources", parameter);
                    }
                    if (workOrderList.SelectedValue != null && workOrderList.SelectedValue != "")
                        Session[Constants.SessionName.WORKORDERID] = workOrderList.SelectedValue;
                    SendEmail(txtProjectName.Text.Trim(), ddlProjectType.SelectedItem.Text, txtProjStartdate.Text, txtMaintClosingDate.Text, ddlProjectOwner.SelectedItem.Text, workOrderList.SelectedItem.Text);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Project has created successfully.');window.location ='ProjectListing.aspx';",
                    true);



                    Session[chkvalueid] = "0";
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        ///Method to update project in a time tracker.
        /// </summary>
        /// <param name="ProjectID"></param>
        private void UpdateTimeTrackerProject()
        {

            string projectname = txtProjectName.Text.Trim();
            string query;
            query = string.Format("select ProjectId from Project where ProjectName='{0}'", projectname);
            object project = PMOscar.BaseDAL.ExecuteScalar(query);

            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProjectID", project));

            PMOscar.BaseDAL.ExecuteSPScalar("UpdateTimeTrackerProject", parameter);

        }

        private void UpdateInfo()
        {

            if (txtBudHours.Text.Trim() == "")
                txtBudHours.Text = "0";

            if (txtRevBudHours.Text.Trim() == "")
                txtRevBudHours.Text = "0";


            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProjectId", ProjectEditId));
            parameter.Add(new SqlParameter("@ProjectCode", txtProjectCode.Text.Trim()));
            parameter.Add(new SqlParameter("@ProjectName", txtProjectName.Text.Trim()));
            parameter.Add(new SqlParameter("@ShortName", txtProjectShortName.Text.Trim()));
            parameter.Add(new SqlParameter("@PhaseID", ddlCurrentPhase.SelectedItem.Value));
            parameter.Add(new SqlParameter("@ProjectType", ddlProjectType.SelectedItem.Value));
            parameter.Add(new SqlParameter("@ProjectOwner", ddlProjectOwner.SelectedItem.Value));
            parameter.Add(new SqlParameter("@ProjectManager", ddlProjectManager.SelectedItem.Value));
            parameter.Add(new SqlParameter("@ProjectManagerName", ""));

            if (txtDelCal.Text == "")
            {
                parameter.Add(new SqlParameter("@DeliveryDateText", ""));
            }
            else
            {
                parameter.Add(new SqlParameter("@DeliveryDateText", txtDelCal.Text));
            }


            if (txtRDel.Text == "")
            {
                parameter.Add(new SqlParameter("@RevisedDeliveryDateText", ""));
            }
            else
            {
                parameter.Add(new SqlParameter("@RevisedDeliveryDateText", txtRDel.Text));
            }

            parameter.Add(new SqlParameter("@ApprvChangeRequest", "0"));
            parameter.Add(new SqlParameter("@PMComments", txtPMComments.Text));
            parameter.Add(new SqlParameter("@POComments", txtPOComments.Text));
            parameter.Add(new SqlParameter("@DeliveryComments", txtDeliveryCommments.Text));
            parameter.Add(new SqlParameter("@isActive", optActive.Checked == true ? 1 : 0));
            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@OpMode", "UPDATE"));
            parameter.Add(new SqlParameter("@status", "Insert"));
            if (txtProjStartdate.Text == "")
            {
                parameter.Add(new SqlParameter("@ProjStartDateText", ""));
            }
            else
            {
                parameter.Add(new SqlParameter("@ProjStartDateText", txtProjStartdate.Text));
            }


            if (txtMaintClosingDate.Text == "")
            {
                parameter.Add(new SqlParameter("@MaintClosingDateText", ""));
            }
            else
            {
                parameter.Add(new SqlParameter("@MaintClosingDateText", txtMaintClosingDate.Text));
            }
            parameter.Add(new SqlParameter("@devurl", txtdevURL.Text));
            parameter.Add(new SqlParameter("@qaurl", txtQAUrl.Text));
            parameter.Add(new SqlParameter("@demourl", txtDemoUrl.Text));
            parameter.Add(new SqlParameter("@productionurl", txtProductionurl.Text));


            parameter.Add(new SqlParameter("@ClientId", ddlClient.SelectedItem.Value));
            parameter.Add(new SqlParameter("@ProgramId", ddlProgram.SelectedItem.Value));
            parameter.Add(new SqlParameter("@Utilization", ddlUtilization.SelectedItem.Value));
            parameter.Add(new SqlParameter("@CostCentreID", ddlcostcentre.SelectedItem.Value));
            //To check whether the project should be shown in dashboard
            if (chkShowinDashboard.Checked == true)

                ShowInDashBoard = 1;
            else

                ShowInDashBoard = 0;
            parameter.Add(new SqlParameter("@ShowInDashboard", ShowInDashBoard));
            parameter.Add(new SqlParameter("@SVNPath", txtSVNUrl.Text));
            parameter.Add(new SqlParameter("@ProjectDetails", txtProjectDetails.Text));
            parameter.Add(new SqlParameter("@WorkOrderID", workOrderList.SelectedValue));
            if (BugzillaList.SelectedValue != null)
            {
                parameter.Add(new SqlParameter("@BugzillaProjectId", BugzillaList.SelectedValue));
            }
            else
            {
                parameter.Add(new SqlParameter("@BugzillaProjectId", DBNull.Value));
            }
            parameter[21].Direction = ParameterDirection.InputOutput;
            // Return value as parameter
            SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;
            parameter.Add(returnValue);


            try
            {
                int ProjectId = PMOscar.BaseDAL.ExecuteSPScalar("ProjectOperations", parameter);
                ProjectEditId = Convert.ToInt16(returnValue.Value);
                string status = parameter[21].Value.ToString();
                if (status == "1")
                {

                    Page.RegisterStartupScript("myScript", "<script language='JavaScript'>alert('Project Name already Exists!');</script>");

                }
                else if (status == "2")
                {

                    Page.RegisterStartupScript("myScript", "<script language='JavaScript'>alert('Short name already Exists!');</script>");

                }
                else if (status == "3")
                {

                    Page.RegisterStartupScript("myScript", "<script language='JavaScript'>alert('Project Code already Exists!');</script>");
                }


                else
                {
                    List<int> ids = new List<int>();
                    foreach (ListItem lst in lstResource.Items)
                    {
                        if (lst.Selected)
                        {
                            ids.Add(Convert.ToInt32(lst.Value));
                        }
                    }

                    string result = string.Join(",", ids.Select(n => n.ToString()).ToArray());
                    parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@ProjectId", ProjectEditId));
                    parameter.Add(new SqlParameter("@ResourceID", result));
                    if (result != String.Empty)
                    {
                        int value = PMOscar.BaseDAL.ExecuteSPScalar("AddProjectResources", parameter);
                    }
                    ProjdetailsToTimetracker(1);
                    if (workOrderList.SelectedValue != null && workOrderList.SelectedValue != "")
                        Session[Constants.SessionName.WORKORDERID] = workOrderList.SelectedValue;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Project has updated successfully.');window.location ='ProjectListing.aspx';",
                    true);
                    Session[chkvalueid] = "0";
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Method to create project in time tracker using project name.
        /// </summary>
        /// <param name="ProjectName"></param>
        private void ProjdetailsToTimetracker(int ISUpdate)
        {
            int isupdate = ISUpdate;
            string strProjectStartDate = txtProjStartdate.Text;
            string strMaintClosingDate = txtMaintClosingDate.Text;
            string projectname = txtProjectName.Text.Trim();
            string query = string.Format("select ProjectId from Project where ProjectName='{0}'", projectname);
            object project = PMOscar.BaseDAL.ExecuteScalar(query);
            int projectid = Convert.ToInt16(project);
            string phase = ddlCurrentPhase.SelectedValue;
            int phaseId = Convert.ToInt16(phase);

            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProjectName", txtProjectName.Text.Trim()));
            parameter.Add(new SqlParameter("@ProjectID", projectid));
            parameter.Add(new SqlParameter("@StartDate", strProjectStartDate));
            parameter.Add(new SqlParameter("@EndDate", strMaintClosingDate));
            parameter.Add(new SqlParameter("@ISUpdate", isupdate));
            parameter.Add(new SqlParameter("@isActive", optActive.Checked == true ? 1 : 0));
            parameter.Add(new SqlParameter("@phaseId", phaseId));

            try
            {
                int ProjectId = PMOscar.BaseDAL.ExecuteSPScalar("CreateProjectInTimeTracker", parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void clearcontrols()
        {
            txtProjectName.Text = "";
            txtProjectShortName.Text = "";
            txtBudHours.Text = "";
            txtRevBudHours.Text = "";
            lbErorMsg.Text = "";

        }
        private void BindComboDetails()
        {

            String query = "Select UserId,FirstName from [User] where  Isactive = 1 and UserRoleID in (1,2) order by FirstName";
            DataTable dt = PMOscar.BaseDAL.ExecuteDataTable(query);
            ddlProjectManager.DataSource = dt;
            ddlProjectManager.DataBind();
            ddlProjectManager.Items.Insert(0, new ListItem("Select", "0"));

            dt.Dispose();
            query = "Select UserId,FirstName from [User] where  Isactive = 1 and UserRoleID=1 order by FirstName";
            dt = PMOscar.BaseDAL.ExecuteDataTable(query);
            ddlProjectOwner.DataSource = dt;
            ddlProjectOwner.DataBind();
            ddlProjectOwner.Items.Insert(0, new ListItem("Select", "0"));
            dt.Dispose();

            parameter = new List<SqlParameter>();
            dt = BaseDAL.ExecuteSPPhaseDataTable("GetPhase", parameter);
            ddlPhase.DataSource = dt;
            ddlPhase.DataTextField = "Phase";
            ddlPhase.DataValueField = "PhaseID";
            ddlPhase.DataBind();

            ddlPhase.Items.Insert(0, new ListItem("Select", "0"));
            ddlCurrentPhase.DataSource = dt;
            ddlCurrentPhase.DataTextField = "Phase";
            ddlCurrentPhase.DataValueField = "PhaseID";
            ddlCurrentPhase.DataBind();
            ddlCurrentPhase.Items.Insert(0, new ListItem("Select", "0"));
            dt.Dispose();

            dt = PMOscar.BaseDAL.ExecuteDataTable("Select EstimationRoleID,RoleName from EstimationRole order by RoleName");
            ddlRole.DataSource = dt;
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "EstimationRoleID";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("Select", "0"));
            dt.Dispose();


            dt = PMOscar.BaseDAL.ExecuteDataTable("Select ClientId,ClientName from Client order by ClientName");
            ddlClient.DataSource = dt;
            ddlClient.DataTextField = "ClientName";
            ddlClient.DataValueField = "ClientId";
            ddlClient.DataBind();
            ddlClient.Items.Insert(0, new ListItem("Select", "0"));
            dt.Dispose();
            if (Convert.ToInt16(ddlClient.SelectedValue) > 0)
            {
                dt = PMOscar.BaseDAL.ExecuteDataTable("Select ProgId,ProgName from Program where ClientId=" + ddlClient.SelectedValue + " order by ProgName");
                ddlProgram.DataSource = dt;
                ddlProgram.DataTextField = "ProgName";
                ddlProgram.DataValueField = "ProgId";
                ddlProgram.DataBind();
                ddlProgram.Items.Insert(0, new ListItem("Select", "0"));
                dt.Dispose();

            }
            else
            {
                ddlProgram.Items.Clear();
                ddlProgram.Items.Insert(0, new ListItem("Select", "0"));
            }
           

            //Bind CostCentre dropdownlist with data
            dt = PMOscar.BaseDAL.ExecuteDataTable("Select CostCentreID,CostCentre from CostCentre order by CostCentre");
            ddlcostcentre.DataSource = dt;
            ddlcostcentre.DataTextField = "CostCentre";
            ddlcostcentre.DataValueField = "CostCentreID";
            ddlcostcentre.DataBind();
            ddlcostcentre.Items.Insert(0, new ListItem("Select", "0"));
            dt.Dispose();

            dt = PMOscar.BaseDAL.ExecuteDataTable("Select WorkOrderID,WorkOrderName from WorkOrder where status!=3 order by WorkOrderName");
            workOrderList.DataSource = dt;
            workOrderList.DataTextField = "WorkOrderName";
            workOrderList.DataValueField = "WorkOrderID";
            workOrderList.DataBind();
            workOrderList.Items.Insert(0, new ListItem("Select", "0"));
            dt.Dispose();

            //Bugzilla Projects

            DataTable dtBugzilla = BaseDAL.CopyBugzillaProject();
            BugzillaList.DataSource = dtBugzilla;
            BugzillaList.DataTextField = "name";
            BugzillaList.DataValueField = "id";
            BugzillaList.DataBind();
            BugzillaList.Items.Insert(0, new ListItem("Select", "0"));
            dtBugzilla.Dispose();

            //populating the list box

            dt = PMOscar.BaseDAL.ExecuteDataTable("Select Resourceid,ResourceName from Resource  where Isactive=1 order by ResourceName");
            lstResource.DataSource = dt;
            lstResource.DataTextField = "ResourceName";
            lstResource.DataValueField = "Resourceid";
            lstResource.DataBind();
            lstResource.Items.Insert(0, new ListItem("Select", "0"));
            lstResource.Items.FindByValue("0").Enabled = false;

            dt.Dispose();
        }
        private void BindEstimationGrid()
        {
            totalActualHoursOriginal = 0.00;
            totalActualHrs = 0.00;
            totalBillable = 0;
            totalBudgeted = 0;
            totalRevisedBudgeted = 0;
            if (ProjectEditId == 0)
            {
                if (Request.QueryString["ProjectEditId"] != null)
                {
                    ProjectEditId = Convert.ToInt32(Request.QueryString["ProjectEditId"]);
                }
                else
                {
                    ProjectEditId = Convert.ToInt16(Session["ProjectEditId"]);//CHK123
                }
            }
            string query = string.Format("Select * from ProjectResources where  ProjectID={0} and isactive=1", ProjectEditId); ;

            DataTable projectResources = PMOscar.BaseDAL.ExecuteDataTable(query);
            List<int> ids = new List<int>(projectResources.Rows.Count);
            //retrieving the selected resouce from database
            foreach (DataRow row in projectResources.Rows)
            {
                ids.Add((int)row["ResourceID"]);
            }

            for (int i = 0; i < lstResource.Items.Count; i++)
            {
                if (ids.Contains(System.Convert.ToInt32(lstResource.Items[i].Value)))
                    lstResource.Items[i].Selected = true;

            }

            string Query = "";

            if (chkValue.Checked == true)

                Query = "Select A.ProjectID,A.PhaseID,B.Phase,A.BillableHours,A.BudgetHours,A.RevisedBudgetHours,RowNumber = ROW_NUMBER(" +
                  " ) OVER 	(ORDER BY A.ProjectEstimationID ASC),C.RoleName as Role,A.EstimationRoleID as RoleID,isnull(T.ActualHours,0)ActualHours1," +
                  " isnull(D.ActualHours,0)ActualHours," +
                  " A.ProjectEstimationID," + " A.Comments" +
                  " from 	dbo.ProjectEstimation A inner join Phase B on A.PhaseID=B.PhaseID" +
                  " inner join EstimationRole C on  C.EstimationRoleID=A.EstimationRoleID 	left join (   SELECT  CASE P.ProjectType" +
                  " WHEN  'F'  THEN isnull(sum(A.ActualHours* B.EstimationPercentage),0) ELSE isnull(sum(A.ActualHours),0)" +
                  " END ActualHours,A.PhaseID,A.ProjectId,b.EstimationRoleID from TimeTracker A inner join Role B on A.RoleID=B.RoleID" +
                  " inner join Project P on P.ProjectId = A.ProjectId  group by b.EstimationRoleID,A.phaseID,A.ProjectId, P.ProjectType" +
                  ") as D on   A.PhaseID=D.PhaseID and A.ProjectId=D.ProjectId and D.EstimationRoleID=A.EstimationRoleID " +
                  " left join (   SELECT   isnull(sum(A.ActualHours),0)ActualHours,A.PhaseID,A.ProjectId,b.EstimationRoleID" +
                  " from TimeTracker A inner join Role B on A.RoleID=B.RoleID" +
                  " inner join Project P on P.ProjectId = A.ProjectId  group by b.EstimationRoleID,A.phaseID,A.ProjectId, P.ProjectType" +
                  " ) as T on   A.PhaseID=T.PhaseID and A.ProjectId=T.ProjectId and T.EstimationRoleID=A.EstimationRoleID" +
                  " where A.ProjectID= " + ProjectEditId + " order by A.PhaseID";  // To Incluse VAS and Proposal

            else


                Query = "Select A.ProjectID,A.PhaseID,B.Phase,A.BillableHours,A.BudgetHours,A.RevisedBudgetHours,RowNumber = ROW_NUMBER(" +
                " ) OVER 	(ORDER BY A.ProjectEstimationID ASC),C.RoleName as Role,A.EstimationRoleID as RoleID,isnull(T.ActualHours,0)ActualHours1," +
                " isnull(D.ActualHours,0)ActualHours," +
                " A.ProjectEstimationID," + " A.Comments" +
                " from 	dbo.ProjectEstimation A inner join Phase B on A.PhaseID=B.PhaseID" +
                " inner join EstimationRole C on  C.EstimationRoleID=A.EstimationRoleID 	left join (   SELECT  CASE P.ProjectType" +
                " WHEN  'F'  THEN isnull(sum(A.ActualHours* B.EstimationPercentage),0) ELSE isnull(sum(A.ActualHours),0)" +
                " END ActualHours,A.PhaseID,A.ProjectId,b.EstimationRoleID from TimeTracker A inner join Role B on A.RoleID=B.RoleID" +
                " inner join Project P on P.ProjectId = A.ProjectId  group by b.EstimationRoleID,A.phaseID,A.ProjectId, P.ProjectType" +
                ") as D on   A.PhaseID=D.PhaseID and A.ProjectId=D.ProjectId and D.EstimationRoleID=A.EstimationRoleID " +
                " left join (   SELECT   isnull(sum(A.ActualHours),0)ActualHours,A.PhaseID,A.ProjectId,b.EstimationRoleID" +
                " from TimeTracker A inner join Role B on A.RoleID=B.RoleID" +
                " inner join Project P on P.ProjectId = A.ProjectId  group by b.EstimationRoleID,A.phaseID,A.ProjectId, P.ProjectType" +
                " ) as T on   A.PhaseID=T.PhaseID and A.ProjectId=T.ProjectId and T.EstimationRoleID=A.EstimationRoleID" +
                " where A.ProjectID= " + ProjectEditId + " And A.PhaseID <> 1  And A.PhaseID <> 6 order by A.PhaseID";


            dtEstimation = PMOscar.BaseDAL.ExecuteDataTable(Query);
            grdEstimation.DataSource = dtEstimation;
            grdEstimation.DataBind();
            Session["dtEstimation"] = dtEstimation;
            Session["EstAudProjectId"] = ProjectEditId;

        }

        private void BindEstimationDT()
        {


            totalActualHoursOriginal = 0.00;
            totalActualHrs = 0.00;
            totalBillable = 0;
            totalBudgeted = 0;
            totalRevisedBudgeted = 0;
            if (ProjectEditId == 0)
            {
                if (Request.QueryString["ProjectEditId"] != null)
                {
                    ProjectEditId = Convert.ToInt32(Request.QueryString["ProjectEditId"]);
                }
                else
                {
                    ProjectEditId = Convert.ToInt16(Session["ProjectEditId"]);//CHK123
                    // ProjectEditId = Convert.ToInt16(ViewState["ProjectEditId"]);//CHK124
                }
            }




            string Query = "Select A.ProjectID,A.PhaseID,B.Phase,A.BillableHours,A.BudgetHours,A.RevisedBudgetHours,RowNumber = ROW_NUMBER(" +
               " ) OVER  (ORDER BY A.ProjectEstimationID ASC),C.RoleName as Role,A.EstimationRoleID as RoleID,round(isnull(T.ActualHours,0),0)ActualHours1," +
               " round(isnull(D.ActualHours,0),0)ActualHours," +
               " A.ProjectEstimationID" +
               " from  dbo.ProjectEstimation A inner join Phase B on A.PhaseID=B.PhaseID" +
               " inner join EstimationRole C on  C.EstimationRoleID=A.EstimationRoleID  left join (   SELECT  CASE P.ProjectType" +
               " WHEN  'F'  THEN round(isnull(sum(A.ActualHours* B.EstimationPercentage),0),0) ELSE round(isnull(sum(A.ActualHours),0),0)" +
               " END ActualHours,A.PhaseID,A.ProjectId,b.EstimationRoleID from TimeTracker A inner join Role B on A.RoleID=B.RoleID" +
               " inner join Project P on P.ProjectId = A.ProjectId  group by b.EstimationRoleID,A.phaseID,A.ProjectId, P.ProjectType" +
               ") as D on   A.PhaseID=D.PhaseID and A.ProjectId=D.ProjectId and D.EstimationRoleID=A.EstimationRoleID " +
               " left join (   SELECT   round(isnull(sum(A.ActualHours),0),0)ActualHours,A.PhaseID,A.ProjectId,b.EstimationRoleID" +
               " from TimeTracker A inner join Role B on A.RoleID=B.RoleID" +
               " inner join Project P on P.ProjectId = A.ProjectId  group by b.EstimationRoleID,A.phaseID,A.ProjectId, P.ProjectType" +
               " ) as T on   A.PhaseID=T.PhaseID and A.ProjectId=T.ProjectId and T.EstimationRoleID=A.EstimationRoleID" +
               " where A.ProjectID= " + ProjectEditId + " order by A.PhaseID";  // To Incluse VAS and Proposal



            dtEstimation = PMOscar.BaseDAL.ExecuteDataTable(Query);
            Session["dtEstimation"] = dtEstimation;

        }
        private void insertEstimationDetails()
        {
            if (ProjectEditId == 0)
            {
                ProjectEditId = Convert.ToInt16(Session["ProjectEditId"]);
            }

            if (txtBillableHrs.Text == "")
                txtBillableHrs.Text = "0";
            if (txtBudHours.Text == "")
                txtBudHours.Text = "0";
            if (txtRevBudHours.Text == "")
                txtRevBudHours.Text = "0";

            if (editid != 0)
            {
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ProjectId", ProjectEditId));
                parameter.Add(new SqlParameter("@PhaseID", Convert.ToInt64(ddlPhase.SelectedItem.Value)));
                parameter.Add(new SqlParameter("@RoleID", Convert.ToInt64(ddlRole.SelectedItem.Value)));
                parameter.Add(new SqlParameter("@BillableHours", Convert.ToInt64(txtBillableHrs.Text)));
                parameter.Add(new SqlParameter("@BudgetHours", Convert.ToInt64(txtBudHours.Text)));
                parameter.Add(new SqlParameter("@RevisedBudgetHours ", Convert.ToInt64(txtRevBudHours.Text)));
                parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@status", "U"));
                parameter.Add(new SqlParameter("@ProjectEstimationID", editid));
                parameter.Add(new SqlParameter("@Comments", txtComments.Text.Trim()));
                try
                {
                    int ProjectId = PMOscar.BaseDAL.ExecuteSPScalar("[ProjectEstimationOperations]", parameter);

                }
                catch
                {

                }
            }
            else
            {
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ProjectId", ProjectEditId));
                parameter.Add(new SqlParameter("@PhaseID", Convert.ToInt64(ddlPhase.SelectedItem.Value)));
                parameter.Add(new SqlParameter("@RoleID", Convert.ToInt64(ddlRole.SelectedItem.Value)));
                parameter.Add(new SqlParameter("@BillableHours", Convert.ToInt64(txtBillableHrs.Text == "" ? "0" : txtBillableHrs.Text)));
                parameter.Add(new SqlParameter("@BudgetHours", Convert.ToInt64(txtBudHours.Text == "" ? "0" : txtBudHours.Text)));
                parameter.Add(new SqlParameter("@RevisedBudgetHours ", Convert.ToInt64(txtRevBudHours.Text == "" ? "0" : txtRevBudHours.Text)));
                parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@status", "I"));
                parameter.Add(new SqlParameter("@ProjectEstimationID", 1));
                if (!string.IsNullOrEmpty(txtComments.Text.ToString()))
                {
                    parameter.Add(new SqlParameter("@Comments", txtComments.Text.Trim()));
                }
                try
                {
                    int ProjectId = PMOscar.BaseDAL.ExecuteSPScalar("[ProjectEstimationOperations]", parameter);

                }
                catch
                {

                }
            }
        }
        private void deleteEstimationDetails()
        {
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProjectId", ProjectEditId));
            parameter.Add(new SqlParameter("@PhaseID", 1));
            parameter.Add(new SqlParameter("@RoleID", 1));
            parameter.Add(new SqlParameter("@BillableHours", 1));
            parameter.Add(new SqlParameter("@BudgetHours", 1));
            parameter.Add(new SqlParameter("@RevisedBudgetHours ", 1));
            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@status", "D"));
            parameter.Add(new SqlParameter("@ProjectEstimationID", editid));
            try
            {
                int ProjectId = PMOscar.BaseDAL.ExecuteSPScalar("[ProjectEstimationOperations]", parameter);

            }
            catch
            {

            }

            EditFlag = 1;
            editClickFlag = 1;
            Session["EditFlag"] = EditFlag;
            Session["EditClickFlag"] = editClickFlag;
            BindEstimationGrid();

        }
        private void ClearEstimationDetails()
        {
            ddlPhase.SelectedIndex = 0;
            ddlRole.SelectedIndex = 0;
            lblValPhase.Text = "";
            lblValRole.Text = "";
            txtBudHours.Text = "";
            txtBillableHrs.Text = "";
            txtBudHours.Text = "";
            txtRevBudHours.Text = "";
            txtEstID.Text = "";
            lblComments.Text = "";
            txtComments.Text = "";
        }
        private void UpdateDeleteProjectEstimation()
        {

            editid = Convert.ToInt32(Request.QueryString["ProjectEstimationID"].Trim());
            //Session["editid"] = editid;

            BindEstimationGrid();
            Session["dtEstimation"] = dtEstimation;

            if (editid != 0)
            {
                lblProjectStatus.Text = "Project Details";
                pnlEst.Visible = true;
                pnlEst.Enabled = true;
                pnlEst2.Visible = true;
                pnlEst2.Enabled = true;
                editClickFlag = 1;
            }

            if (Request.QueryString["opmode"] == "Edit")
            {
                btnEdit.Enabled = false; //checking;
                chkValue.Enabled = false;  //checking;
                PnlEdit.Enabled = false;//checking;
                LinkButton1.Enabled = false;//checking;

                DataRow[] dr = dtEstimation.Select("ProjectEstimationID=" + editid);
                foreach (DataRow newrow in dr)
                {
                    ddlPhase.SelectedValue = newrow["PhaseID"].ToString();
                    ddlRole.SelectedValue = newrow["RoleID"].ToString();
                    txtBillableHrs.Text = newrow["BillableHours"].ToString();
                    txtBudHours.Text = newrow["BudgetHours"].ToString();
                    txtRevBudHours.Text = newrow["RevisedBudgetHours"].ToString();
                    txtEstID.Text = newrow["ProjectEstimationID"].ToString();
                    txtComments.Text = newrow["Comments"].ToString();
                }
                btnEstSave.Text = "Update";
            }
            else
            {
                DataTable dt = PMOscar.BaseDAL.ExecuteDataTable("Select  A.ProjectID,A.PhaseID,B.Phase,A.BillableHours,A.BudgetHours,A.RevisedBudgetHours ,round(isnull(D.ActualHours,0),0)ActualHours,A.ProjectEstimationID from dbo.ProjectEstimation A  inner join Phase B on A.PhaseID=B.PhaseID " +
                                                                      " inner join EstimationRole C on  C.EstimationRoleID=A.EstimationRoleID  left join ( Select round(isnull(sum(A.ActualHours* B.EstimationPercentage),0),0) as  ActualHours,A.PhaseID,A.ProjectId,b.EstimationRoleID  from TimeTracker A inner " +
                                                                      " join Role B on A.RoleID=B.RoleID  group by b.EstimationRoleID,A.phaseID,A.ProjectId ) as D on   A.PhaseID=D.PhaseID and A.ProjectId=D.ProjectId and D.EstimationRoleID=A.EstimationRoleID where A.ProjectEstimationID=" + editid + " order by A.PhaseID ");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ActualHours"].ToString() != "0.00000")
                    {
                        lblesterror.Text = "Entries with actual hours cannot be deleted.";
                        EditFlag = 1;
                        editClickFlag = 1;
                        Session["EditFlag"] = EditFlag;
                        Session["EditClickFlag"] = editClickFlag;
                    }
                    else
                    {
                        deleteEstimationDetails();
                    }
                }
            }


            totalActualHrs = 0.00;
            totalBillable = 0;
            totalBudgeted = 0;
            totalRevisedBudgeted = 0;
            TextBox1.Focus();
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
        #endregion
        protected void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            //lblPctName.Visible = false;
            //lblSName.Visible = false;
            //lblPjctCode.Visible = false;
        }
        protected void txtProjectShortName_TextChanged(object sender, EventArgs e)
        {
            //lblPctName.Visible = false;
            //lblSName.Visible = false;
            //lblPjctCode.Visible = false;
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Session["AudProjectId"] = Convert.ToInt32(Request.QueryString["ProjectEditId"] == null ? Session["ProjectEditId"] : Request.QueryString["ProjectEditId"]);
            //LinkButton2.Attributes.Add("onclick", "window.open('ProjectAudit.aspx','','height=200,width=600');return false");
            ModalPopupExtender1.Show();
            ProjectAudituserctrl1.BindGridView();

            EditFlag = 1;
            if (Convert.ToInt32(Session["EditClickFlag"]) == 1)
            {
                editClickFlag = 1;
            }
            Session["EditFlag"] = EditFlag;
            Session["EditClickFlag"] = editClickFlag;
            Panel1.Visible = true;
            panEdit.Visible = true;
            pnlPopup1.Visible = true;
            pnlPopup2.Visible = true;

            BindEstimationGrid();
        }

        protected void btnCancelpopup_Click(object sender, EventArgs e)
        {
            this.ModalPopupExtender3.Hide();
            Panel2.Style.Add("display", "none");
        }

        protected void ResourceHistory(object sender, EventArgs e)
        {
            this.ModalPopupExtender3.Show();
            Panel2.Style.Add("display", "block");
            ResourceHistoryLogic();
        }

        public void ResourceHistoryLogic()
        {
            Session["AudProjectId"] = Convert.ToInt32(Request.QueryString["ProjectEditId"] == null ? Session["ProjectEditId"] : Request.QueryString["ProjectEditId"]);
            EditFlag = 1;
            if (Convert.ToInt32(Session["EditClickFlag"]) == 1)
            {
                editClickFlag = 1;
            }
            Session["EditFlag"] = EditFlag;
            Session["EditClickFlag"] = editClickFlag;
            panEdit.Visible = true;
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProjectID", Session["AudProjectId"]));
            DataTable dsProjectDetails = BaseDAL.ExecuteSPDataTable("GetProjectResourceAudit", parameter);
            if (dsProjectDetails.Rows.Count != 0)
            {
                GridViewbind.DataSource = dsProjectDetails;
                GridViewbind.DataBind();
            }
        }



        //To view Workorder
        protected void LinkButtonToViewWorkorder_Click(object sender, EventArgs e)
        {
            //string url = System.Configuration.ConfigurationManager.AppSettings["PMOscarV2_Url"]+
            //    "Account/LoginSession?Email="Session["UserName"]&Password=Session["EncryptedPassword"] &returnUrl=/WorkOrder";
            //string redirectUrl = "";
            //Response.Redirect("ProjectListing.aspx");
        }

        protected void BtnViewDetails_Click(object sender, EventArgs e)
        {
            //  get the gridviewrow from the sender so we can get the datakey we need

            LinkButton btnDetails = sender as LinkButton;
            GridViewRow row = (GridViewRow)btnDetails.NamingContainer;

            // object obj = this.grdEstimation.DataKeys[row.RowIndex].Value;

            string hiddenFieldValue = ((HiddenField)grdEstimation.Rows[0].FindControl("hdnPhase")).Value;
            // Button Button1 = (Button)o;
            GridViewRow grdRow = (GridViewRow)btnDetails.Parent.Parent;

            Session["ActProjectId"] = Convert.ToInt32(grdRow.Cells[7].Text);
            Session["ActPhaseId"] = Convert.ToInt32(grdRow.Cells[8].Text);
            Session["ActEstRoleId"] = Convert.ToInt32(grdRow.Cells[9].Text);

            ////  update the contents in the detail panel
            //    this.updPnlCustomerDetail.Update();
            ////  show the modal popup

            pnlPopup1.Style.Add("display", "block");
            this.mdlPopup.Show();
            ActualHoursuctrl1.BindGridView();


            EditFlag = 1;
            editClickFlag = 1;
            Session["EditFlag"] = EditFlag;
            Session["EditClickFlag"] = editClickFlag;
            BindEstimationGrid();


        }
        protected void BtnViewDetails1_Click(object sender, EventArgs e)
        {
            //  get the gridviewrow from the sender so we can get the datakey we need

            LinkButton btnDetails = sender as LinkButton;
            GridViewRow row = (GridViewRow)btnDetails.NamingContainer;

            // object obj = this.grdEstimation.DataKeys[row.RowIndex].Value;

            string hiddenFieldValue = ((HiddenField)grdEstimation.Rows[0].FindControl("hdnPhase1")).Value;
            // Button Button1 = (Button)o;
            GridViewRow grdRow = (GridViewRow)btnDetails.Parent.Parent;

            Session["ActProjectId"] = Convert.ToInt32(grdRow.Cells[7].Text);
            Session["ActPhaseId"] = Convert.ToInt32(grdRow.Cells[8].Text);
            Session["ActEstRoleId"] = Convert.ToInt32(grdRow.Cells[9].Text);

            ////  update the contents in the detail panel
            //    this.updPnlCustomerDetail.Update();
            ////  show the modal popup

            pnlPopup2.Style.Add("display", "block");
            this.mdlPopup1.Show();
            NotAdjustedActualHours1.BindGridView();


            EditFlag = 1;
            editClickFlag = 1;
            Session["EditFlag"] = EditFlag;
            Session["EditClickFlag"] = editClickFlag;
            BindEstimationGrid();


        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["EstAudProjectId"] = Convert.ToInt32(Request.QueryString["ProjectEditId"] == null ? Session["ProjectEditId"] : Request.QueryString["ProjectEditId"]);
            //LinkButton1.Attributes.Add("onclick", "window.open('ProjectEstimationAudit.aspx','','height=200,width=600');return false");
            ModalPopupExtender2.Show();
            ProjectEstimationAuditusrctrl1.BindGridView();

            EditFlag = 1;
            if (Convert.ToInt32(Session["EditClickFlag"]) == 1)
            {
                editClickFlag = 1;
            }
            Session["EditFlag"] = EditFlag;
            Session["EditClickFlag"] = editClickFlag;

            Panel1.Visible = true;
            panEdit.Visible = true;
            pnlPopup1.Visible = true;
            pnlPopup2.Visible = true;

            BindEstimationGrid();
        }
        protected void chkValue_CheckedChanged(object sender, EventArgs e)
        {
            EditFlag = Convert.ToInt32(Session["EditFlag"]);

            if (chkValue.Checked == true)
            {
                iFlag = 1;
                Session[chkvalueid] = "1";
            }
            else
            {
                iFlag = 2;
                Session[chkvalueid] = "0";
            }


            lblProjectStatus.Text = "Edit Project";
            //PnlEdit.Enabled = true;
            //pnlEst.Enabled = true;
            //pnlEst2.Enabled = true;
            editClickFlag = 1;
            //ProjectEditId = Convert.ToInt32(Request.QueryString["ProjectEditId"]);
            //Session["ProjectEditId"] = ProjectEditId;
            //EditFlag = 1;
            Session["EditFlag"] = EditFlag;
            Session["EditClickFlag"] = editClickFlag;
            BindEstimationGrid();
            Panel1.Visible = true;
            panEdit.Visible = true;
            pnlPopup1.Visible = true;
            pnlPopup2.Visible = true;

            ClearEstimationDetails();
            ddlPhase.Focus();
            //EditFlag = 1;
            editClickFlag = 1;
            Session["EditFlag"] = EditFlag;
            Session["EditClickFlag"] = editClickFlag;
            BindEstimationGrid();
            TextBox1.Focus();


            //if (Request.QueryString["opmode"] == "Edit")
            //{
            //    btnEdit.Enabled = false; //checking;
            //    PnlEdit.Enabled = false;
            //}


        }

        protected void btnClone_Click(object sender, EventArgs e)
        {
            //pnlCloneProject.Visible = true;
            mdlClonePopup.Show();
        }


        //Method when show in dashboard index changes

        //private void checkdateformat()
        //{
        //    String dateString = txtProjStartdate.Text;
        //    String formats = "MM/dd/yyyy";
        //    DateTime dateValue;

        //    if (DateTime.TryParseExact(dateString, formats,
        //                                  new CultureInfo("en-US"),
        //                                  DateTimeStyles.None,
        //                                  out dateValue))
        //    {
        //       lblDateError.Text="Project StartDate format is incorrect!";
        //       return;
        //    }
        //     dateString=txtDelCal.Text;
        //     if (DateTime.TryParseExact(dateString, formats,
        //                                  new CultureInfo("en-US"),
        //                                  DateTimeStyles.None,
        //                                  out dateValue))
        //    {
        //        lblDateError.Text = "Delivery Date  format is incorrect!";
        //    }
        //     dateString = txtRDel.Text;
        //     if (DateTime.TryParseExact(dateString, formats,
        //                                   new CultureInfo("en-US"),
        //                                   DateTimeStyles.None,
        //                                   out dateValue))
        //     {
        //         lblDateError.Text = "Revised Delivery Date  format is incorrect!";
        //     }
        //     dateString = txtMaintClosingDate.Text;
        //     if (DateTime.TryParseExact(dateString, formats,
        //                                   new CultureInfo("en-US"),
        //                                   DateTimeStyles.None,
        //                                   out dateValue))
        //     {
        //         lblDateError.Text = "Maintanance Closing Date  format is incorrect!";
        //     }
        //}

        /// <summary>
        /// method to send email after the creation of project
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="projecttype"></param>
        /// <param name="projectstartdate"></param>
        /// <param name="projectenddate"></param>
        /// <param name="owner"></param>
        /// <param name="workorder"></param>
        /// <returns>boolean value</returns>
        public bool SendEmail(string projectName, string projectType, string projectStartDate, string projectEndDate, string owner, string workOrder)
        {
            string senderMailID = ConfigurationManager.AppSettings["senderMailID"];
            string recipients = ConfigurationManager.AppSettings["recipientMailID"];
            string senderPassword = ConfigurationManager.AppSettings["senderMailPassword"];
            string[] toEmailsArray;
            string separator = ";'";
            try
            {
                if (projectName != string.Empty && projectType != string.Empty && projectEndDate != string.Empty && owner != string.Empty && workOrder != string.Empty)
                {

                    char[] toEmailsSplitters = separator.ToCharArray();
                    toEmailsArray = recipients.Split(toEmailsSplitters);
                    MailMessage mailObj = new MailMessage();
                    mailObj.From = new MailAddress(senderMailID);//your mail id
                    mailObj.Subject = ConfigurationManager.AppSettings["subject"];
                    mailObj.IsBodyHtml = true;
                    mailObj.Body = getMailContent(projectName, projectType, projectStartDate, projectEndDate, owner, workOrder);
                    string Host = Constants.host;
                    int Port = Constants.port;
                    SmtpClient smtpClient = new SmtpClient(Host, Port);
                    System.Net.NetworkCredential myCredential = new System.Net.NetworkCredential(senderMailID, senderPassword);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = myCredential;
                    if (toEmailsArray.Length > 0)
                    {
                        foreach (var mail in toEmailsArray)
                        {
                            mailObj.To.Add(new MailAddress(mail));
                        }

                    }
                    smtpClient.Send(mailObj);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// method to bind the contents for mail
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="projecttype"></param>
        /// <param name="projectstartdate"></param>
        /// <param name="projectenddate"></param>
        /// <param name="owner"></param>
        /// <param name="workorder"></param>
        /// <returns>template for mail</returns>
        public string getMailContent(string projectName, string projectType, string projectStartDate, string projectEndDate, string owner, string workOrder)
        {
            string template = string.Empty;
            string startDateReplaced = string.Empty;
            string endDateReplaced = string.Empty;
            try
            {
                // SimpleDateFormat format1 = new SimpleDateFormat("yyyy-MM-dd");
                string content = File.ReadAllText(Server.MapPath("~/Template/demowork/index.html"));
                string nameReplaced = content.Replace("/**name**/", projectName);
                string typeReplaced = nameReplaced.Replace("/**projecttype**/", projectType);

                if (Request.QueryString["mode"].Equals("add", StringComparison.OrdinalIgnoreCase))
                {
                    DateTime startDate = DateTime.ParseExact(projectStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.ParseExact(projectEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    startDateReplaced = typeReplaced.Replace("/**startdate**/", startDate.ToString("dd/MM/yyyy"));
                    endDateReplaced = startDateReplaced.Replace("/**enddate**/", endDate.ToString("dd/MM/yyyy"));
                }
                else
                {
                    startDateReplaced = typeReplaced.Replace("/**startdate**/", Convert.ToDateTime(projectStartDate).ToString("dd/MM/yyyy"));
                    endDateReplaced = startDateReplaced.Replace("/**enddate**/", Convert.ToDateTime(projectEndDate).ToString("dd/MM/yyyy"));
                }

                string ownerReplaced = endDateReplaced.Replace("/**projectowner**/", owner);
                string workOrderReplaced = ownerReplaced.Replace("/**workorder**/", workOrder);
                string createdDate = workOrderReplaced.Replace("/**createdDate**/", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                string query = string.Format("select FirstName from [dbo].[User] where UserName='{0}'", (string)Session["userName"]);
                object status = PMOscar.BaseDAL.ExecuteScalar(query);
                if (status != null)
                {
                    template = createdDate.Replace("/**createdUser**/", status.ToString());
                }
                return template;
            }
            catch (Exception)
            {
                throw;
            }
        }



        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt=null;
            if (ddlClient.SelectedValue != "0")
            {
                dt = PMOscar.BaseDAL.ExecuteDataTable("Select ProgId,ProgName from Program where ClientId=" + ddlClient.SelectedValue + " order by ProgName");



                ddlProgram.DataSource = dt;
                ddlProgram.DataTextField = "ProgName";
                ddlProgram.DataValueField = "ProgId";
                ddlProgram.DataBind();
                ddlProgram.Items.Insert(0, new ListItem("Select", "0"));
                dt.Dispose();
            }
            else
            {
                ddlProgram.Items.Clear();
                ddlProgram.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClient.SelectedValue == "0")
            {
                ddlProgram.SelectedValue = "0";
            }
        }
       
    }
}
