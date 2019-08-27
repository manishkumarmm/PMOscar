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
using System.Web.UI.HtmlControls;
using System.Configuration;

public partial class PMOMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = Request.Url.ToString().ToLower();
        if (url != string.Empty)
        {
            if (url.Contains("projectdashboardentry") || url.Contains("projectdashboard"))
            {
                HtmlControl hcadminPD = Page.Master.FindControl("adminPD") as HtmlControl;
                if (hcadminPD != null)
                    (Page.Master.FindControl("adminPD") as HtmlControl).Attributes.Add("class", "active");
            }
            else if (url.Contains("resourceplanning") || url.Contains("projectplanningentry"))
            {
                HtmlControl hcadminPD = Page.Master.FindControl("adminRP") as HtmlControl;
                if (hcadminPD != null)
                    (Page.Master.FindControl("adminRP") as HtmlControl).Attributes.Add("class", "active");
            }
            else if (url.Contains("reports.aspx") || url.Contains("reports"))
            {
                HtmlControl hcadminPD = Page.Master.FindControl("adminR") as HtmlControl;
                if (hcadminPD != null)
                    (Page.Master.FindControl("adminR") as HtmlControl).Attributes.Add("class", "active");
            }
            else if (url.Contains("billingdetails.aspx") || url.Contains("addbillingdetails.aspx") || url.Contains("billingdetailsentry.aspx"))
            {
                HtmlControl hcadminPD = Page.Master.FindControl("adminBD") as HtmlControl;
                if (hcadminPD != null)
                    (Page.Master.FindControl("adminBD") as HtmlControl).Attributes.Add("class", "active");
            }
            else if (url.Contains("project.aspx") || url.Contains("projectlisting"))
            {
                HtmlControl hcadminPD = Page.Master.FindControl("adminPL") as HtmlControl;
                if (hcadminPD != null)
                    (Page.Master.FindControl("adminPL") as HtmlControl).Attributes.Add("class", "active");
            }
            else if (url.Contains("resourcelisting.aspx") || url.Contains("resource.aspx"))
            {
                HtmlControl hcadminPD = Page.Master.FindControl("adminRL") as HtmlControl;
                if (hcadminPD != null)
                    (Page.Master.FindControl("adminRL") as HtmlControl).Attributes.Add("class", "active");
            }          
        }

    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session["UserName"] = null;
        Session.Abandon();
        string PMOscarV1_Url = ConfigurationManager.AppSettings["PMOscarV1_Url"];
        Response.Redirect(PMOscarV1_Url);
    }

    protected void lnkResetpwd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ResetPassword.aspx");
    }
}
