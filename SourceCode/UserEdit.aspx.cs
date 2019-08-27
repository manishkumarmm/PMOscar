
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="UserEdit.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// Description  : This pages is used to edit user details.
//  Author       : Muralikrishnan
//  Created Date : 21 February 2011
//  Modified By  : Muralikrishnan
//  Modified Date:   
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
    public partial class UserEdit : System.Web.UI.Page
    {
        #region"Declaration"  

        public IList<SqlParameter> parameter;
        private  int userEditId = 0;

        #endregion

        #region"PageEvents"

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
                        else if (Convert.ToInt16(Session["UserRoleID"]) == 1)
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
                    FirstName.Focus();

                    if ( !Page.IsPostBack )
                    {
                        BindDropDownRole(); // Method to bind the roles in the DropDownList
                        ShowUser();  // Method to show the user details            
                    }

                    FirstName.Attributes.Add("onkeypress", "return CheckTextValue(event,this);"); 
            }

        #endregion

        #region"Methods"

            // Method to bind the roles in the DropDownList
            private void BindDropDownRole()
            {
                parameter = new List<SqlParameter>();                
                DataTable dt = BaseDAL.ExecuteSPDataTable("GetUserRole", parameter);
                ddlRole.DataSource = dt;
                ddlRole.DataTextField = "UserRole";
                ddlRole.DataValueField = "UserRoleID";
                ddlRole.DataBind();
                ddlRole.Items.Insert(0, new ListItem("Select", "0"));
            }

            // Method to show the user details
            private void ShowUser() 
            {           
                userEditId = Convert.ToInt32(Request.QueryString["UserEditId"]);
              
                string strQuery = "Select FirstName,UserName,Password,UserRoleID,IsActive,LastName,EmployeeCode from [User] where [UserId]='" + userEditId + "'";
                DataSet user = PMOscar.BaseDAL.ExecuteDataSet(strQuery);

                FirstName.Text = user.Tables[0].Rows[0].ItemArray[0].ToString();  //firstname as per db order        
                UserName.Text = user.Tables[0].Rows[0].ItemArray[1].ToString();
               
                Password.Attributes.Add("value",PMOscar.BaseDAL.DecryptText(user.Tables[0].Rows[0].ItemArray[2].ToString()));
                LastName.Text = user.Tables[0].Rows[0].ItemArray[5].ToString();
                txtEmployeecode.Text = user.Tables[0].Rows[0].ItemArray[6].ToString();

            ddlRole.Text = user.Tables[0].Rows[0].ItemArray[3].ToString();
                
                if ( user.Tables[0].Rows[0].ItemArray[4].ToString() == "True" )            
                    rdActive.Checked = true;
               
                else                
                 rdInActive.Checked = true;         
            }

            // Method to update the user details
            private string UpdateUser(string FirstName, string LastName, string UserName, string EmailId, string pwd, string middlename, string employeecode)
            {
                  userEditId = Convert.ToInt32(Request.QueryString["UserEditId"]);

                  int radioButtonStatus = 0;

                  if ( rdActive.Checked == true )
                      radioButtonStatus = 1;
                  
                  else              
                      radioButtonStatus = 0;             

                  string status = "U";

                  try
                  {
                      FirstName = FirstName.Replace("<script", "[script").Replace("</script>", "[/script]");
                      LastName = LastName.Replace("<script", "[script").Replace("</script>", "[/script]");
                      UserName = UserName.Replace("<script", "[script").Replace("</script>", "[/script]");
                      pwd = PMOscar.BaseDAL.EncryptText(pwd);
                      EmailId = EmailId.Replace("<script", "[script").Replace("</script>", "[/script]");
                      middlename = middlename.Replace("<script", "[script").Replace("</script>", "[/script]");
                      employeecode = employeecode.Replace("<script", "[script").Replace("</script>", "[/script]");

                SqlParameter[] parameters = new SqlParameter[15];
                      parameters[0] = new SqlParameter("@FirstName", FirstName);
                      parameters[1] = new SqlParameter("@LastName", LastName);
                      parameters[2] = new SqlParameter("@UserName", UserName);
                      parameters[3] = new SqlParameter("@Password", pwd);
                      parameters[4] = new SqlParameter("@EmailId", EmailId);
                      parameters[5] = new SqlParameter("@Status", status);
                      parameters[6] = new SqlParameter("@CreatedBy", Session["UserID"]);
                      parameters[7] = new SqlParameter("@Createddate", DateTime.Now);
                      parameters[8] = new SqlParameter("@UpdatedBy", Session["UserID"]);
                      parameters[9] = new SqlParameter("@UpdatedDate", DateTime.Now);
                      parameters[10] = new SqlParameter("@middleName", middlename);
                      parameters[11] = new SqlParameter("@userID", userEditId);
                      parameters[12] = new SqlParameter("@IsActive", radioButtonStatus);
                      parameters[13] = new SqlParameter("@UserRoleID", Convert.ToInt32(ddlRole.SelectedValue.ToString()));
                      if(employeecode!=null)
                      {
                        parameters[14] = new SqlParameter("@EmployeeCode", employeecode);
                      }
                      else
                      {
                        parameters[14] = new SqlParameter("@EmployeeCode", "");
                      }
                      
                      parameters[5].Direction = ParameterDirection.InputOutput;
                      BaseDAL.ExecuteSPNonQuery("[UserOperations]", parameters);
                      status = parameters[5].Value.ToString();
                      return status;
                  }

                  catch (Exception ex)
                  {
                      //Log.Write("Error in user Creation:" + Environment.NewLine + ex.StackTrace + ex.InnerException, ex);
                      return status;
                  }
            }

        #endregion

        #region"Control Events"    
    
            protected virtual void Submit(object sender, System.EventArgs e)
        {
            var firstName = FirstName.Text.Trim();
            var lastName = LastName.Text.Trim();
            var userName = UserName.Text.Trim();
            var pwd = Password.Text.Trim();
            var employeeCode = txtEmployeecode.Text.Trim();
            var emailid = string.Empty;
            var middleName = string.Empty;

            string status = UpdateUser(firstName, lastName, userName, emailid, pwd, middleName,employeeCode);

            if (status.Equals("t"))
            {
                //successfully Updated
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('User has updated successfully.');window.location ='UserListing.aspx';",
                    true);
            }
                

            else if (status.Equals("i"))
                lblError.Text = "UserName already exists.";
            else if (status.Equals("e"))
                lblError.Text = PMOscar.Core.Constants.AddRole.DUPLICATEEMPLOYEECODE;
        }

            protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserListing.aspx");
        } 
    
        #endregion       
    }
}
