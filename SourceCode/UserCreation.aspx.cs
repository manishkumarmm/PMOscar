
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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace PMOscar
{
    public partial class UserCreation : System.Web.UI.Page
    {       
        #region"Declaration"
       
        public IList<SqlParameter> parameter;

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
                }
            }
             
            txtFirstName.Focus();

            if ( !IsPostBack )
                BindDropDownRole(); // Method to bind the roles in the DropDownList

            txtFirstName.Attributes.Add("onkeypress", "return CheckTextValue(event,this);"); 
        }

        #endregion

        #region"Control Events"  
     
        // Storing the resource details in to variables... 
        protected virtual void Submit(object sender, System.EventArgs e)
        {
            var firstName = txtFirstName.Text.Trim();
            var lastName = LastName.Text.Trim();
            var userName = txtUserName.Text.Trim();
            var password = Password.Value;
            var emailid = string.Empty;
            var middlename = string.Empty;
            int roleid = Convert.ToInt16(ddlRole.SelectedItem.Value);
            var empcode = txtEmpCode.Text.Trim();
            string status = InsertUser(firstName, lastName, userName, password, emailid, middlename, roleid, empcode);

            if (status.Equals("t"))
            {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('User has created successfully.');window.location ='UserListing.aspx';",
                    true);
            }
              
            
            else if(status.Equals("i"))            
               lblError.Text = "UserName already exists.";

            else if (status.Equals("e"))
                lblError.Text = "Employee code already exists.";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserListing.aspx");
        } 
   
        #endregion

        #region"Methods"

        // Method to bind the roles in the DropDownList
        private void BindDropDownRole()
        {
            parameter = new List<SqlParameter>();          
            DataTable dtUserRole = BaseDAL.ExecuteSPDataTable("GetUserRole", parameter);
            if (dtUserRole.Rows.Count > 0)
            {
                ddlRole.DataSource = dtUserRole;
                ddlRole.DataTextField = "UserRole";
                ddlRole.DataValueField = "UserRoleID";
                ddlRole.DataBind();
                ddlRole.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        // Method to add the user details
        private string InsertUser(string FirstName, string LastName, string UserName, string Password, string EmailId,string MiddleName,int roleID, string EmpCode)
        {
            int radioButtonStatus = 0;

            if ( RdActive.Checked == true )
               radioButtonStatus = 1;
            
            else
              radioButtonStatus = 0;            

            string status = "I";

            try
            {
                FirstName = FirstName.Replace("<script", "[script").Replace("</script>", "[/script]");
                LastName = LastName.Replace("<script", "[script").Replace("</script>", "[/script]");
                UserName = UserName.Replace("<script", "[script").Replace("</script>", "[/script]");
                Password=PMOscar.BaseDAL.EncryptText(Password);
                EmailId = EmailId.Replace("<script", "[script").Replace("</script>", "[/script]");
                MiddleName = MiddleName.Replace("<script", "[script").Replace("</script>", "[/script]");

                SqlParameter[] parameters = new SqlParameter[15];
                parameters[0] = new SqlParameter("@FirstName", FirstName);
                parameters[1] = new SqlParameter("@LastName", LastName);
                parameters[2] = new SqlParameter("@UserName", UserName);
                parameters[3] = new SqlParameter("@Password", Password);
                parameters[4] = new SqlParameter("@EmailId", EmailId);
                parameters[5] = new SqlParameter("@Status", status);           
                parameters[6] = new SqlParameter("@UpdatedBy", Session["UserID"]);
                parameters[7] = new SqlParameter("@UpdatedDate", DateTime.Now);
                parameters[8] = new SqlParameter("@CreatedBy", Session["UserID"]);
                parameters[9] = new SqlParameter("@Createddate", DateTime.Now);
                parameters[10] = new SqlParameter("@middleName", MiddleName);
                parameters[11] = new SqlParameter("@userID", 1);
                parameters[12] = new SqlParameter("@IsActive", radioButtonStatus);
                parameters[13] = new SqlParameter("@UserRoleID", roleID);
                parameters[14] = new SqlParameter("@EmployeeCode",EmpCode);
                parameters[5].Direction = ParameterDirection.InputOutput;
                BaseDAL.ExecuteSPNonQuery("[UserOperations]", parameters);
                status = parameters[5].Value.ToString();
                return status;
            }           
            catch (Exception ex)
            {
                return status;
                throw ex;
            }
        }

        #endregion
    }
}
