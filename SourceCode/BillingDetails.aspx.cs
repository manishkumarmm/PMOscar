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
    public partial class BillingDetails : System.Web.UI.Page
    {
        #region"Declarations"
        public IList<SqlParameter> parameter;
        private string month;
        private string year;
        public int status =0;
        public  int freezeFlag = 0;
        private BillingDetailsDAL objBillingDAL = new BillingDetailsDAL();
        #endregion
      
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            checkBoxCat.Visible = false;
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

                lblMsg.Text = string.Empty;
                lblerrors.Visible = false;
                if (!Page.IsPostBack)
                {
                    FillYears();
                }

                if ((Request.QueryString["redirect"] != null))  // on back to page
                {
                    if (!Page.IsPostBack)
                    {
                        try
                        {
                        month = Request.QueryString["month"] != null ? Request.QueryString["month"] : string.Empty;
                        year = Request.QueryString["year"] != null ? Request.QueryString["year"] : string.Empty;
                        ddlMonth.SelectedValue = month.ToString().Trim();
                        DataSet dsProjectDetails = objBillingDAL.GetBillingDetailsByMonthYear(month, year);
                        gdBillingDetails.DataSource = dsProjectDetails.Tables[0] as DataTable;
                        gdBillingDetails.DataBind();
                        ddlYear.SelectedValue = year;
                       }
                       catch (Exception ex)
                        {
                            Logger.Error("An exception caught on grid binding", ex);
                        }
                    }
                    else
                    {
                        month = ddlMonth.SelectedValue;
                        year = ddlYear.SelectedValue;
                    }
                }

                else
                {
                    month = ddlMonth.SelectedValue;
                    year = ddlYear.SelectedValue;
                }
                       

        }

        #endregion
       
        #region "bindYear"
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
                if (Request.QueryString["month"] != null && Request.QueryString["year"] != null)
                {
                    ddlMonth.SelectedValue = Request.QueryString["month"].ToString();
                    ddlYear.SelectedValue = Request.QueryString["year"].ToString();
                }
                else
                {
                    int currentYear = DateTime.Now.Year;
                    ddlYear.Items.FindByValue(currentYear.ToString()).Selected = true;

                    //select current month in month dropdown
                    ddlMonth.SelectedIndex = DateTime.Now.Month - 1;
                }
        
            }
            catch (Exception ex)
            {
                Logger.Error("An exception caught on populating year dropdown", ex);
            }
        }
        #endregion

        #region "Add button click"
        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            status = 1;
            Response.Redirect("AddBillingDetails.aspx?month=" +month+"&year="+year+"&status="+status );
        }
        #endregion

        #region "Export button click"
        /// <summary>
        /// Handles the Click event of the btnExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                checkBoxCat.Visible = true;
                string filename = "BillingDetails_" + ddlMonth.SelectedItem + "-" + ddlYear.SelectedValue;
                List<SqlParameter> parameter = new List<SqlParameter>();
                lblMsg.Text = string.Empty;
                DataTable dtExportProjectPlanningDashboard = objBillingDAL.ExportBillingDetails(month, year);
                if (dtExportProjectPlanningDashboard.Rows.Count == 0)   //check if data present in table 
                {
                    lblerrors.Visible = true;
                    lblerrors.Text = "No data to export";
                }
                else
                {
                    ExportToSpreadsheet(dtExportProjectPlanningDashboard, filename); // Method to Export Datatable to an excel file...
                    lblerrors.Visible = false;
                    lblerrors.Text = "";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception caught on export", ex);
            } 
        }

    // Method to Export Datatable to an excel file...
        public static void ExportToSpreadsheet(DataTable table, string name)
        {

            GridView gdReport = new GridView();  // copy from table to gridview
            gdReport.DataSource = table;
            gdReport.DataBind();
           
                gdReport.HeaderRow.Cells[0].Text = "Project Name";
                gdReport.HeaderRow.Cells[1].Text = "Month";
                gdReport.HeaderRow.Cells[2].Text = "Resource Name";
                gdReport.HeaderRow.Cells[3].Text = "Role";
                gdReport.HeaderRow.Cells[4].Text = "Planned Billable";
                gdReport.HeaderRow.Cells[5].Text = "Actual Billable";
                gdReport.HeaderRow.Cells[6].Text = "Actual Spent";

                //Color Formats
                gdReport.HeaderRow.Style.Add("background-color", "#FFFFFF");
                //Apply style to Individual Cells
                gdReport.HeaderRow.Cells[0].Style.Add("background-color", "#539cd1");
                gdReport.HeaderRow.Cells[1].Style.Add("background-color", "#539cd1");
                gdReport.HeaderRow.Cells[2].Style.Add("background-color", "#539cd1");
                gdReport.HeaderRow.Cells[3].Style.Add("background-color", "#539cd1");
                gdReport.HeaderRow.Cells[4].Style.Add("background-color", "#539cd1");
                gdReport.HeaderRow.Cells[5].Style.Add("background-color", "#539cd1");
                gdReport.HeaderRow.Cells[6].Style.Add("background-color", "#539cd1");



            HttpContext context = HttpContext.Current;
                context.Response.Clear();
                context.Response.Buffer = true;
                context.Response.AddHeader("content-disposition", "attachment;filename=" + name);
                context.Response.ContentType = "application/vnd.ms-excel";
                context.Response.Charset = "";
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                gdReport.RenderControl(oHtmlTextWriter);

                
                //removing hyperlinks if any
                String sTemp = oStringWriter.ToString();
                sTemp = sTemp.Replace("href=", "");
                context.Response.Write(sTemp);
                context.Response.End();
 
 }
        #endregion

        #region "Go button click"
        /// <summary>
        /// Handles the Go event of the btn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btn_Go(object sender, System.EventArgs e)
        {
            try
            {
                DataSet dsProjectDetails = objBillingDAL.GetBillingDetailsByMonthYear(month, year);
                Session["gdBillingDetails"] = dsProjectDetails;
                gdBillingDetails.DataSource = dsProjectDetails.Tables[0] as DataTable;
                if(dsProjectDetails.Tables[0].Rows.Count > 0)
                {
                    checkBoxCat.Visible = true;
                }
                gdBillingDetails.DataBind();
                lblMsg.Text = string.Empty;
                CheckBox1.Checked = false;
                CheckBox2.Checked = false;
                CheckBox3.Checked = false;
                CheckBox4.Checked = false;
                CheckBox5.Checked = false;
            }
            catch (Exception ex)
            {
                Logger.Error("An exception caught on Go button click", ex);
            }
        }
        #endregion

        #region "grid row creation"
        /// <summary>
        /// Handles the RowCreated event of the Grview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void Grview_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    e.Row.Cells[3].Attributes.Add("Style", "text-align:right;padding-right:15px;");
                    e.Row.Cells[4].Attributes.Add("Style", "text-align:right;padding-right:15px;");

                }
            }

        }
        #endregion

        double totalBillableHrs = 0, totalBilledTillLastMonth = 0, totalBilled = 0, totalPlanned = 0, totalActual = 0, totalBilledProject = 0;

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            var filterVal = string.Empty;
            if (CheckBox1.Checked)
            {
                filterVal += "'Commercial', ";
            }
            if (CheckBox2.Checked)
            {
                filterVal += "'Semi Commercial', ";
            }
            if (CheckBox3.Checked)
            {
                filterVal += "'Internal', ";
            }
            if (CheckBox4.Checked)
            {
                filterVal += "'GIS', ";
            }
            if (CheckBox5.Checked)
            {
                filterVal += "'Product', ";
            }

            filterVal = filterVal.Length > 0 ? filterVal.Remove(filterVal.Length - 2) : "";
            checkBoxCat.Visible = true;

            DataSet dataTable = Session["gdBillingDetails"] as DataSet;
            DataTable dt = new DataTable();
            dt = dataTable.Tables[0];
            if (dt != null)
            {
                if (filterVal.Length > 0)
                {
                    dt.DefaultView.RowFilter = "Utilization In ( " + filterVal + ") ";
                }
                else
                {
                    dt.DefaultView.RowFilter = "";
                }
                gdBillingDetails.DataSource = dt;
                gdBillingDetails.DataBind();
            }

        }


        #region "giving Total in grid's footer"
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
                        //e.Row.Cells[8].Attributes.Add("Style", "text-align:right;padding-right:15px;");
                        totalBillableHrs += Convert.ToDouble(e.Row.Cells[4].Text.ToString() == "" ? "0" : e.Row.Cells[4].Text.ToString());
                        totalBilledTillLastMonth += Convert.ToDouble(e.Row.Cells[5].Text.ToString() == "" ? "0" : e.Row.Cells[5].Text.ToString());
                        totalBilled += Convert.ToDouble(e.Row.Cells[6].Text.ToString() == "" ? "0" : e.Row.Cells[6].Text.ToString());  
                        totalPlanned += Convert.ToDouble(e.Row.Cells[7].Text.ToString() == "" ? "0" : e.Row.Cells[7].Text.ToString());
                        totalActual += Convert.ToDouble(e.Row.Cells[8].Text.ToString() == "" ? "0" : e.Row.Cells[8].Text.ToString());
                        totalBilledProject += Convert.ToDouble(e.Row.Cells[9].Text.ToString() == "" ? "0" : e.Row.Cells[9].Text.ToString());

                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer) // Applying blank spaces if the values are zero
                {
                    e.Row.Cells[4].Text = totalBillableHrs.ToString();
                    e.Row.Cells[5].Text = totalBilledTillLastMonth.ToString();
                    e.Row.Cells[6].Text = totalBilled.ToString();                  
                    e.Row.Cells[7].Text = totalPlanned.ToString();
                    e.Row.Cells[8].Text = totalActual.ToString();
                    e.Row.Cells[9].Text = totalBilledProject.ToString();


                    e.Row.Cells[3].Text = "Total";
                    e.Row.Cells[3].Attributes.Add("style", "text-align: center;");
                    e.Row.Cells[4].Attributes.Add("style", "text-align: right;padding-right:15px;");
                    e.Row.Cells[5].Attributes.Add("style", "text-align: right;padding-right:15px;");
                    e.Row.Cells[6].Attributes.Add("style", "text-align: right;padding-right:15px;");
                    e.Row.Cells[7].Attributes.Add("style", "text-align: right;padding-right:15px;");
                    e.Row.Cells[8].Attributes.Add("style", "text-align: right;padding-right:15px;");
                    e.Row.Cells[9].Attributes.Add("style", "text-align: right;padding-right:15px;");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception caught on footer <Total> Binding", ex);
            }
        }
        #endregion       
        

        #region "Finalize button click"
        /// <summary>
        /// Handles the Finalize event of the btn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btn_Finalize(object sender, System.EventArgs e)
        {
            try
            {               
                DataSet dsProjectDetails = objBillingDAL.GetBillingDetailsByMonthYear(month, year);
                if (dsProjectDetails.Tables[0].Rows.Count == 0)
                    freezeFlag = 2;

                //checking whether the project billing for the current period is already finalized
                               
                    foreach (DataRow dr in dsProjectDetails.Tables[0].Rows)
                    {                    
                        
                        if (Convert.ToBoolean(dr["Freeze"]))
                        {
                            freezeFlag = 1;
                        }
                    }

                
                if (freezeFlag == 1)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Project billing for the selected period has already been finalized!";
                }
                else if (freezeFlag == 0)
                {
                    objBillingDAL.FreezeProject(year, month);
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Project billing for the selected period has been finalized!!";
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "No records for the selected period! ";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An exception caught on Go button click", ex);
            }
        }
        #endregion

        protected void gdBillingDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataSet dataTable = Session["gdBillingDetails"] as DataSet;
            DataTable dt = new DataTable();
            dt = dataTable.Tables[0];
            if (dt != null)
            {

                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gdBillingDetails.DataSource = dt;
                gdBillingDetails.DataBind();
                checkBoxCat.Visible = true;
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

        protected void lnkInvoiceSummary_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoiceSummary.aspx");
        }

    }
}