using System;
using PMOscar.Core;
using PMOscar.Model;
using PMOscar.DAL;
using System.Net;
using System.Web;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.IO;

public partial class Default : System.Web.UI.Page
{
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        txtUserName.Focus();

        try
        {
            string logOut = Request.QueryString["action"];
            if (logOut != null && logOut != "")
            {
                Session["UserName"] = null;
                Session.Abandon();
            }
            else
            {
                string encryptString = Request.QueryString["data"];
                string Email = Request.QueryString["Email"];
                var json_serializer = new JavaScriptSerializer();
                bool gsuite = false;
                if (encryptString != null && encryptString != "")
                {
                    gsuite = true;
                    var request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["gSuiteKey"]);
                    var response = (HttpWebResponse)request.GetResponse();
                    var dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);

                    // Read the content fully up to the end.
                    string secretKey = reader.ReadToEnd();

                    TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
                    byte[] byteBuff;
                    string iv = Request.QueryString["iv"];
                    desCryptoProvider.Key = Encoding.UTF8.GetBytes(secretKey);
                    desCryptoProvider.IV = UTF8Encoding.UTF8.GetBytes(iv);
                    byteBuff = Convert.FromBase64String(encryptString);

                    string plaintext = Encoding.UTF8.GetString(desCryptoProvider.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));

                    var gsuiteData = (IDictionary<string, object>)json_serializer.DeserializeObject(plaintext);
                    Email = gsuiteData["email"].ToString();
                }
                if (Email != null)
                {
                    string Password = "";
                    if (!gsuite)
                    {
                        Password = PMOscar.BaseDAL.DecryptString(Request.QueryString["Password"].Replace(" ", "+"));
                    }
                    string returnUrl = Request.QueryString["returnUrl"];
                    var user = new User();
                    var objUserDAL = new UserDAL();

                    if (objUserDAL.CheckUserAuthentication(Email, PMOscar.BaseDAL.EncryptText(Password), out user, gsuite))
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
                            if (gsuite)
                            {
                                if (Convert.ToInt32(Session[Constants.SessionName.USERROLEID]) == 4)
                                    Response.Redirect("UserListing.aspx", false);
                                else
                                    Response.Redirect("ResourcePlanning.aspx", false);
                            }
                            else Response.Redirect(returnUrl);
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Invalid UserName/Password";
                    }
                }
                else
                {
                    var application = ConfigurationManager.AppSettings["gSuiteSsoRequest"];
                    Response.Redirect(application, false);
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
