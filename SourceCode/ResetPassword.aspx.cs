
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ResetPassword.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
//  Description  : Uses to change password.
//  Author       : 
//  Created Date : 
//  Modified By  : Vibin MB
//  Modified Date: 12/04/2018
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
using System.Collections.Generic;
using PMOscar.Core;

namespace PMDashBoard
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        #region "Declarations"

            public IList<SqlParameter> parameter;

        #endregion

        #region"Page Events"

            protected void Page_Load(object sender, EventArgs e)
            {
                if ( Session["UserName"] == null ) 
                    Response.Redirect("Default.aspx");            
                else
                {

                if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 5=Employee)
                {
                    #region
                    //Only resource planning tab is visible for employees

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
                }


               else if (Convert.ToInt16(Session["UserRoleID"]) == 4) // Checking role of user by UserRoleID ( 4=Sys Admin)
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

                txtUserName.Text = Session["UserName"].ToString();
                txtOldPwd.Focus();
            }

        #endregion

        #region "ControlEvents"

            protected void btnChange_Click(object sender, EventArgs e)
            {
                if ( txtConfirmPwd.Text == string.Empty )
                {
                    lblErrorMsg.Text = "Password Mismatch";
                    return;
                }

                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@UserId",Session["UserID"]));
                parameter.Add(new SqlParameter("@OldPassword",  PMOscar.BaseDAL.EncryptText(txtOldPwd.Text.Trim())));
                parameter.Add(new SqlParameter("@NewPassword", PMOscar.BaseDAL.EncryptText(txtNewPwd.Text.Trim())));
                parameter.Add(new SqlParameter("@status", "Insert"));
                parameter[3].Direction = ParameterDirection.Output;
                int returnVal = PMOscar.BaseDAL.ExecuteSPScalar("ResetPassword", parameter);
                string returnStatus = parameter[3].Value.ToString();

                if ( returnStatus == "1" )
                    lblErrorMsg.Text = "Password reset successfully";
            
                else           
                    lblErrorMsg.Text = "Invalid old password"; 
            }

            protected void btnCancel_Click(object sender, EventArgs e)
            {
                if (Convert.ToInt32(Session[Constants.SessionName.USERROLEID]) == 4)
                    Response.Redirect("UserListing.aspx");
                else
                Response.Redirect("ResourcePlanning.aspx");
            }

        #endregion

    }
}
