
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceListing.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
//  Description  : Display and make active or inactive the resources.
//  Author       : Muralikrishnan
//  Created Date : 21 February 2011
//  Modified By  : Muralikrishnan
//  Modified Date:   
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
using PMOscar.Core;

namespace PMOscar
{
    public partial class ResourceListing : System.Web.UI.Page
    {
        # region"Declarations"

        public IList<SqlParameter> parameter;
        public  int resourceDeleteId;

        #endregion

        #region"Page Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            if ( Session["UserName"] == null )
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
                else if (Convert.ToInt16(Session["UserRoleID"]) == 1)  // Checking role of user by UserRoleID ( 1 = ProjectOwner , 2 = ProjectManager )
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

           
            lbMessage.Text = string.Empty;
           
            if ( !Page.IsPostBack )
            {
                ddlStatus.SelectedIndex = Convert.ToInt32(Session["activestatus"]);
                resourceDeleteId = Convert.ToInt32(Request.QueryString["ResDeleteId"]);

                if ( resourceDeleteId != 0 )
                    MakeActiveorInactive(resourceDeleteId);  // Method to active or inactive the resource...
            }
            DisplayResourceDetailsList(); // Method to Display Resource Details List...
        }

        #endregion

        #region"Methods"

        // Method to active or inactive the resource...
        private void MakeActiveorInactive(int resourceDeleteId)
        {
            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ResourceId", resourceDeleteId));           
            parameter.Add(new SqlParameter("@ResourceName", ""));
            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@IsActive", 1));
            parameter.Add(new SqlParameter("@RoleId", 1));
            parameter.Add(new SqlParameter("@TeamID", 1));
            parameter.Add(new SqlParameter("@OpMode", "DELETE"));
            parameter.Add(new SqlParameter("@ResourceStatus", ""));
            parameter.Add(new SqlParameter("@CostCentreID",1));
            parameter.Add(new SqlParameter("@emp_Code", ""));
            parameter.Add(new SqlParameter("@BillingStartDate", DateTime.Now));
            parameter.Add(new SqlParameter("@WeeklyHour", Convert.ToDecimal(0.0)));
            try
            {
                int timeTrackerId = PMOscar.BaseDAL.ExecuteSPScalar("ResourceOperations", parameter);
            }
            catch
            {
            }

            DisplayResourceDetailsList(); // Method to Display Resource Details List...

            lbMessage.Text = "Status has changed successfully."; 
        }

        // Method to Display Resource Details List...
        protected void DisplayResourceDetailsList()
        {             
            parameter = new List<SqlParameter>();
            if (ddlStatus.SelectedItem.Text == "Active")
                parameter.Add(new SqlParameter("@ResourceStatus", 1));//parameter to check whether resource is active or inactive

            else if (ddlStatus.SelectedItem.Text == "Inactive")
                parameter.Add(new SqlParameter("@ResourceStatus", 2));

            else
                parameter.Add(new SqlParameter("@ResourceStatus", 3));
            parameter.Add(new SqlParameter("@ResourceId", resourceDeleteId));
            parameter.Add(new SqlParameter("@ResourceName", ""));
            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@IsActive", 1));
            parameter.Add(new SqlParameter("@RoleId",1));
            parameter.Add(new SqlParameter("@TeamID", 1));
            parameter.Add(new SqlParameter("@OpMode", "SELECTALL"));
            parameter.Add(new SqlParameter("@CostCentreID", 1));
            parameter.Add(new SqlParameter("@emp_Code",""));
            parameter.Add(new SqlParameter("@WeeklyHour", 1));

            DataSet dsResorceDetailsList = PMOscar.BaseDAL.ExecuteSPDataSet("ResourceOperations", parameter);
            Session["dgResourceDetailsListing"] = dsResorceDetailsList;
            
            if ( dsResorceDetailsList.Tables[0].Rows.Count > 0 )
            {

                dgResourceDetails.DataSource = dsResorceDetailsList.Tables[0];
                dgResourceDetails.DataBind();
                dgResourceDetails.Columns[1].Visible = false;
            }

            for ( int resourcesCount = 0; resourcesCount < dsResorceDetailsList.Tables[0].Rows.Count; resourcesCount++ )
            {
                dgResourceDetails.Rows[resourcesCount].Cells[0].Text = (resourcesCount + 1).ToString();
            }

            dsResorceDetailsList.Dispose();
        }

        #endregion

        #region"Control Events"

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Resource.aspx"); // To add the Resource Details...
        }

        #endregion

        //This method will be called when the selected index of status dropdown changes

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["activestatus"] = ddlStatus.SelectedIndex.ToString();
            DisplayResourceDetailsList();  // Method to display project details...
        }


        protected void dgResourceDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataSet dgResourceDetailsDataSet = Session["dgResourceDetailsListing"] as DataSet;
            DataTable dgResourceDetailsdt = new DataTable();
            dgResourceDetailsdt = dgResourceDetailsDataSet.Tables[0];
            if (dgResourceDetailsdt != null)
            {

                dgResourceDetailsdt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                dgResourceDetails.DataSource = dgResourceDetailsdt;
                dgResourceDetails.DataBind();
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
        protected void dgResourceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string value = e.Row.Cells[5].Text;
                if(value==Constants.Active)

                {
                    
                    ((LinkButton)e.Row.FindControl("aResourceDelete")).Text = "Deactivate";

                }
                else if(value == Constants.InActive)
                {
                    ((LinkButton)e.Row.FindControl("aResourceDelete")).Text = "Activate";

                }


            }

        }
        public void OnConfirm(object sender, EventArgs e)
        {
            int resourceDeleteId = 0;
            string confirmvalue = Request.Form["confirm_value"];
            string ResourceId = Request.Form["resourceId"];
            if (ResourceId != null)
                resourceDeleteId = Convert.ToInt16(ResourceId);

            if (confirmvalue == "Yes")
            {
                MakeActiveorInactive(resourceDeleteId);
            }
        }
    }
}
