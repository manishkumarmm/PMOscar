// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="UserListing.cs" company="Naico IT Services Pvt. Ltd">
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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.UI.HtmlControls;
using PMOscar.Core;
using System.Windows.Forms;

namespace PMOscar
{
    public partial class UserListing : System.Web.UI.Page
    {
        #region"Declaration"

        public IList<SqlParameter> parameter;
        private int userDeleteId = 0;

        #endregion

        #region"Page Events"

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

                }
                else if (Convert.ToInt16(Session["UserRoleID"]) == 1) // Checking role of user by UserRoleID ( 1 = ProjectOwner , 2 = ProjectManager )
                {
                    HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                    if (hcliuser != null)
                        (Page.Master.FindControl("liuser") as HtmlControl).Visible = true;

                    HtmlControl hcauser = Page.Master.FindControl("auser") as HtmlControl;
                    if (hcauser != null)
                        (Page.Master.FindControl("auser") as HtmlControl).Attributes.Add("class", "active");
                }
                else
                {
                    HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                    if (hcliuser != null)
                        (Page.Master.FindControl("liuser") as HtmlControl).Visible = false;
                    Response.Redirect("AccessDenied.aspx"); // Going to AccessDenied page if user does not have permission to view this page...                            
                }
            }

            lbMessage.Text = string.Empty;
            userDeleteId = Convert.ToInt32(Request.QueryString["UserDeleteId"]);

            if (!Page.IsPostBack)
            {
                ddlStatus.SelectedIndex = Convert.ToInt32(Session["activestatus"]);
                if (userDeleteId != 0)
                    MakeActiveorInactive(userDeleteId);  // Method to active or inactive the user...   
            }

            DisplayUserDetailsList(); // Method to Display User Details List...
        }

        #endregion

        #region"Methods"

        // Method to active or inactive the user...
        private void MakeActiveorInactive(int userDeleteId)
        {            
            SqlParameter[] parameters = new SqlParameter[15];
            parameters[0] = new SqlParameter("@FirstName", "");
            parameters[1] = new SqlParameter("@LastName", "");
            parameters[2] = new SqlParameter("@UserName", "");
            parameters[3] = new SqlParameter("@Password", "");
            parameters[4] = new SqlParameter("@EmailId", "");
            parameters[5] = new SqlParameter("@Status", 'D');
            parameters[6] = new SqlParameter("@UpdatedBy", Session["UserID"]);
            parameters[7] = new SqlParameter("@UpdatedDate", DateTime.Now);
            parameters[8] = new SqlParameter("@CreatedBy", Session["UserID"]);
            parameters[9] = new SqlParameter("@Createddate", DateTime.Now);
            parameters[10] = new SqlParameter("@middleName", "");
            parameters[11] = new SqlParameter("@userID", userDeleteId);
            parameters[12] = new SqlParameter("@IsActive", 1);
            parameters[13] = new SqlParameter("@UserRoleID", 1);
            parameters[14] = new SqlParameter("@EmployeeCode", "");
            parameters[5].Direction = ParameterDirection.InputOutput;
            BaseDAL.ExecuteSPNonQuery("[UserOperations]", parameters);

            DisplayUserDetailsList(); // Method to Display User Details List...

            lbMessage.Text = PMOscar.Core.Constants.AddRole.STATUS;
        }

        // Method to Display User Details List...
        protected void DisplayUserDetailsList()
        {
            parameter = new List<SqlParameter>();

            if (ddlStatus.SelectedItem.Text == "Active")
                parameter.Add(new SqlParameter("@UserStatus", 1));//parameter for checking whether the user is Active or Inactive

            else if (ddlStatus.SelectedItem.Text == "Inactive")
                parameter.Add(new SqlParameter("@UserStatus", 2));//parameter for checking whether the user is Active or Inactive

            else
                parameter.Add(new SqlParameter("@UserStatus", 3));//parameter for checking whether the user is Active or Inactive
            DataSet dsUserDetails = PMOscar.BaseDAL.ExecuteSPDataSet("GetUserList", parameter);
            Session["dgusers"] = dsUserDetails;
            if (dsUserDetails.Tables[0].Rows.Count > 0)
            {
                dgusers.DataSource = dsUserDetails.Tables[0];
                dgusers.DataBind();
                dgusers.Columns[2].Visible = false;
            }

            for (int userCount = 0; userCount < dsUserDetails.Tables[0].Rows.Count; userCount++)
            {
                dgusers.Rows[userCount].Cells[0].Text = (userCount + 1).ToString();
            }

            dsUserDetails.Dispose();
        }

        #endregion

        #region"Control Events"

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserCreation.aspx"); // To add users...
        }

        #endregion

        //This method will be called when the selected index of status dropdown changes
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["activestatus"] = ddlStatus.SelectedIndex.ToString();
            DisplayUserDetailsList();  // Method to display project details...
        }

        protected void dgusers_Sorting(object sender, GridViewSortEventArgs e)
        {


            DataSet dgusersDataSet = Session["dgusers"] as DataSet;
            DataTable dgusersdt = new DataTable();
            dgusersdt = dgusersDataSet.Tables[0];
            if (dgusersdt != null)
            {

                dgusersdt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                dgusers.DataSource = dgusersdt;
                dgusers.DataBind();
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

        protected void dgusers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string value = e.Row.Cells[4].Text;
                if (value == Constants.Active)
                {
                    ((LinkButton)e.Row.FindControl("aResourceDelete")).Text = "Deactivate";

                }
                else if (value == Constants.InActive)
                {
                    ((LinkButton)e.Row.FindControl("aResourceDelete")).Text = "Activate";

                }


            }

        }
        
        public void OnConfirm(object sender, EventArgs e)
        {
            int userDeleteId = 0;
            string confirmvalue = Request.Form["confirm_value"];
            string userId = Request.Form["userid"];
            if (userId != null)
             userDeleteId = Convert.ToInt16(userId);

            if (confirmvalue == "Yes")
            {
                MakeActiveorInactive(userDeleteId);
            }
        }

    }
}

