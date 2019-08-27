using System;
using PMOscar.Core;
using PMOscar.Model;
using PMOscar.DAL;

public partial class Default : System.Web.UI.Page
{
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        txtUserName.Focus();

        try
        {
            string Email = Request.QueryString["Email"];

            if (Email != null)
            {
                string Password = PMOscar.BaseDAL.DecryptString(Request.QueryString["Password"].Replace(" ", "+"));
                string returnUrl = Request.QueryString["returnUrl"];
                var user = new User();
                var objUserDAL = new UserDAL();

                if (objUserDAL.CheckUserAuthentication(Email, PMOscar.BaseDAL.EncryptText(Password), out user))
                {
                    if (!user.IsActive)
                    {
                        lblmsg.Text = "This user account has been disabled!";
                    }
                    else
                    {
                        Session[Constants.SessionName.USERNAME] = user.UserName;
                        Session[Constants.SessionName.ENCRYPTEDPASSWORD] = PMOscar.BaseDAL.EncryptString(Password);
                        Session[Constants.SessionName.USERID] = user.UserId;
                        Session[Constants.SessionName.USERROLEID] = user.UserRole.UserRoleId;
                        lblmsg.Text = "";
                        Response.Redirect(returnUrl);
                    }

                }
                else
                {
                    lblmsg.Text = "Invalid UserName/Password";
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex.Message, ex);
        }
    }

    #endregion

    #region Control Events

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            var user = new User();
            var objUserDAL = new UserDAL();

            if (objUserDAL.CheckUserAuthentication(txtUserName.Text, PMOscar.BaseDAL.EncryptText(txtPassword.Text), out user))
            {
                if(!user.IsActive)
                {
                    lblmsg.Text = "This user account has been disabled!";
                }
                else
                {
                    Session[Constants.SessionName.USERNAME] = user.UserName;
                    Session[Constants.SessionName.ENCRYPTEDPASSWORD] = PMOscar.BaseDAL.EncryptString(txtPassword.Text);
                    Session[Constants.SessionName.USERID] = user.UserId;
                    Session[Constants.SessionName.USERROLEID] = user.UserRole.UserRoleId;
                    lblmsg.Text = "";
                    if(Convert.ToInt32(Session[Constants.SessionName.USERROLEID])==4)
                        Response.Redirect("UserListing.aspx");
                    else
                    Response.Redirect("ResourcePlanning.aspx");
                }              
            }
            else
            {
                lblmsg.Text = "Invalid UserName/Password";
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex.Message, ex);
        }
    }

    #endregion
}
