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
using PMOscar.DAL;
using PMOscar.Core;

namespace PMOscar
{
    public partial class AddBillingDetails : System.Web.UI.Page
    {
        #region"Declarations"

        public IList<SqlParameter> parameter;
        public  string projectId = string.Empty ;
        public string monthPart;
        public int year;
        public string projectName =string.Empty ;
        public int addStatus = 0;
        private double totalPlanned = 0;
        private double totalActual = 0;
        private double totalActualSpent = 0;
        private double totalUBT = 0;
        private BillingDetailsDAL  objBillingDAL = new BillingDetailsDAL();

        #endregion

        #region "Page load"
        protected void Page_Load(object sender, EventArgs e)
        {

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
            try
            {
                if (!Page.IsPostBack)
                {
                    FillYears();
                    

                    addStatus = Request.QueryString["status"] != null ? Convert.ToInt32(Request.QueryString["status"]) : 0;
                    string  monthval = Request.QueryString["month"] != null ? Request.QueryString["month"].ToString() : string.Empty;
                    int yearval = Request.QueryString["year"] != null ? Convert.ToInt32(Request.QueryString["year"]) : 0;
                    BindProjectsDropDown(monthval, Convert.ToInt16(yearval));
                    // on Add Details button click & back button click 
                    if ((addStatus == 0))
                    {
                        projectId = Request.QueryString["projectId"].ToString() == "" ? "0".ToString() : Request.QueryString["projectId"].ToString();
                        ddlProjects.SelectedValue = projectId.ToString().Trim();
                        monthPart = Request.QueryString["month"] != null ? Request.QueryString["month"].ToString() : string.Empty;
                        year = Request.QueryString["year"] != null ? Convert.ToInt32(Request.QueryString["year"]) : 0;
                        ddlYear.SelectedValue = year.ToString();
                        ddlMonth.SelectedValue = monthPart.ToString();

                        DisplayBillingDetails();
                    }
                    ddlMonth.SelectedValue = Request.QueryString["month"].ToString();
                    ddlYear.SelectedValue = Request.QueryString["year"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception caught on page Load", ex);
            }

               
        }
        #endregion

        #region"gridRowCreation_giving row numbers"
        protected void Grview_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
           #endregion

        #region"BackClickEvent"
        /// <summary>
        /// Handles the Click event of the lbBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbBack_Click(object sender, EventArgs e)
        {
            string month = ddlMonth.SelectedValue;
            Response.Redirect("BillingDetails.aspx?redirect=true&month=" + ddlMonth.SelectedValue + "&year=" + ddlYear.SelectedValue);
        }
        #endregion

        #region"DisplayGrid"

        /// <summary>
        /// display billing details for the selected project on selected date
        /// </summary>
        private void DisplayBillingDetails()
        {
            DataSet dsProjectDetails = objBillingDAL.GetProjectWiseBillingDetails(monthPart,year,projectId);
            Session["gdResources"] = dsProjectDetails;
            gdResources.DataSource = dsProjectDetails.Tables[0] as DataTable;
            gdResources.DataBind();
            if (objBillingDAL.IsFreezed(Convert.ToInt32(projectId), year, monthPart))
            {
                btnAddEdit.Enabled = false;
            }
            else
            {
                btnAddEdit.Enabled = true;
            }
        }
            #endregion

        #region"bindYears"
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region"bindProjects"
        private void BindProjectsDropDown(string month,int year)
        {
            
            DataTable dtProjects = objBillingDAL.GetAllProjects(month,year);
            ddlProjects.DataSource = dtProjects;
            ddlProjects.DataTextField = "ProjectName";
            ddlProjects.DataValueField = "ProjectId";
            ddlProjects.DataBind();
            ddlProjects.Items.Insert(0, new ListItem("Select", "0"));
        }
              #endregion

        #region"Add Button Click"
        /// <summary>
        /// Handles the Add button click
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btn_addEdit(object sender, System.EventArgs e)
        {
           Response.Redirect("BillingDetailsEntry.aspx?month=" + ddlMonth.SelectedValue + "&year=" + ddlYear.SelectedValue + "&projectId=" + ddlProjects.SelectedValue);
        }
        #endregion

        #region"Go Button Click"
        /// <summary>
        /// Handles the Go button click
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnGo_Click(object sender, System.EventArgs e)
        {
            try
            {
                monthPart = ddlMonth.SelectedValue;
                year = Convert.ToInt32(ddlYear.SelectedValue);
                projectId = ddlProjects.SelectedValue;

                DisplayBillingDetails();
            }
            catch (Exception ex)
            {
                Logger.Error("An exception caught on Go button click", ex);
            }
        }
        #endregion

        #region "giving Total in grid's footer"
        protected void Grview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header) // Applying color to the header cells...
                {
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.FromName("#fcff00");

                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.DataItem != null)
                    {
                        e.Row.Cells[2].Attributes.Add("Style", "text-align:right;padding-right:15px;");
                        e.Row.Cells[3].Attributes.Add("Style", "text-align:right;padding-right:15px;");
                        e.Row.Cells[4].Attributes.Add("Style", "text-align:right;padding-right:15px;");
                        e.Row.Cells[5].Attributes.Add("Style", "text-align:right;padding-right:15px;");

                        totalPlanned += Convert.ToDouble(e.Row.Cells[2].Text.ToString() == "" ? "0" : e.Row.Cells[2].Text.ToString());
                        totalActual += Convert.ToDouble(e.Row.Cells[3].Text.ToString() == "" ? "0" : e.Row.Cells[3].Text.ToString());
                        totalUBT += Convert.ToDouble(e.Row.Cells[5].Text.ToString() == "" ? "0" : e.Row.Cells[5].Text.ToString());
                        totalActualSpent += Convert.ToDouble(e.Row.Cells[4].Text.ToString() == "" ? "0" : e.Row.Cells[4].Text.ToString());                        
                    }
                }

                if (e.Row.RowType == DataControlRowType.Footer)    // Applying blank spaces if the values are zero
                {

                    e.Row.Cells[3].Text = totalActual.ToString();
                    e.Row.Cells[2].Text = totalPlanned.ToString();
                    e.Row.Cells[4].Text = totalActualSpent.ToString();
                    e.Row.Cells[5].Text = totalUBT.ToString();

                    e.Row.Cells[1].Text = "Total";
                    e.Row.Cells[1].Attributes.Add("style", "text-align: center;");
                    e.Row.Cells[2].Attributes.Add("style", "text-align: right;padding-right:15px;");
                    e.Row.Cells[3].Attributes.Add("style", "text-align: right;padding-right:15px;");
                    e.Row.Cells[4].Attributes.Add("style", "text-align: right;padding-right:15px;");
                    e.Row.Cells[5].Attributes.Add("style", "text-align: right;padding-right:15px;");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception caught on footer <Total> Binding", ex);
            }
        }
        #endregion

        protected void gdResources_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataSet gdResourcesDataTable = Session["gdResources"] as DataSet;
            DataTable gdResourcesdt = new DataTable();
            gdResourcesdt = gdResourcesDataTable.Tables[0];
            if (gdResourcesdt != null)
            {

                gdResourcesdt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gdResources.DataSource = gdResourcesdt;
                gdResources.DataBind();
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
        /// event fired dropdownlist year changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
        {

            string month=ddlMonth.SelectedValue;
            int year = Convert.ToInt16(ddlYear.SelectedValue);
            BindProjectsDropDown(month,year);

        }
        /// <summary>
        /// event fired dropdownlist month changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
        {

            string month = ddlMonth.SelectedValue;
            int year = Convert.ToInt16(ddlYear.SelectedValue);
            BindProjectsDropDown(month, year);

        }
    }
}