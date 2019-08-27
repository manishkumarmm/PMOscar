using PMOscar.Core;
using PMOscar.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PMOscar
{
    public partial class InvoiceSummary : System.Web.UI.Page
    {
        private BillingDetailsDAL objBillingDAL = new BillingDetailsDAL();

        #region Events

        /// <summary>
        /// Page load event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
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
                FillYears();
                BindProjectsDropDown();
            }
        }

        /// <summary>
        /// Project dropdown selected index change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindInvoiceSummaryGrid();
        }

        /// <summary>
        /// Year dropdown selected index change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindInvoiceSummaryGrid();
        }

        #endregion

        #region Bind Dropdown

        /// <summary>
        /// Fills the years.
        /// </summary>
        private void FillYears()
        {
            try
            {
                ListItem lstYears = new ListItem();
                lstYears.Value = "Select";
                lstYears.Text = "Select";
                ddlYear.Items.Add(lstYears);

                for (int yearCount = DateTime.Now.Year - 4; yearCount <= DateTime.Now.Year + 1; yearCount++)
                {
                    lstYears = new ListItem();
                    lstYears.Value = yearCount.ToString();
                    lstYears.Text = yearCount.ToString();
                    ddlYear.Items.Add(lstYears);
                    ddlYear.DataBind();
                }               
            }
            catch (Exception ex)
            {
                Logger.Error("Invoice Summary: An exception caught on populating year dropdown", ex);
            }
        }

        /// <summary>
        /// Bind project dropdown.
        /// </summary>
        private void BindProjectsDropDown()
        {
            ProjectDAL projectDAL = new ProjectDAL();
            DataTable dtProjects = projectDAL.GetAllProjects();
            ddlProject.DataSource = dtProjects;
            ddlProject.DataTextField = "ProjectName";
            ddlProject.DataValueField = "ProjectId";
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, new ListItem("Select", "0"));
        }

        /// <summary>
        /// Bind invoice summary grid.
        /// </summary>
        private void BindInvoiceSummaryGrid()
        {
            if (ddlProject.SelectedIndex > 0 && ddlYear.SelectedIndex > 0)
            {
                DataSet dsProjectDetails = objBillingDAL.GetYearlyProjectBillingDetails(Convert.ToInt32(ddlYear.SelectedValue), ddlProject.SelectedValue);
                dgInvoiceSummary.DataSource = dsProjectDetails.Tables[0] as DataTable;
                dgInvoiceSummary.DataBind();
            }
        }

        #endregion
    }
}