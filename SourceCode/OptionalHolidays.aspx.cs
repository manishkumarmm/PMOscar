// <summary>
//  Description  : Optional holiday form
//  Author       : Ann
//  Created Date : 02 Dec 2019
// </summary>

namespace PMOscar
{
    using Core;
    using System;
    using System.Data;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI.HtmlControls;

    public partial class OptionalHolidays : System.Web.UI.Page
    {
        public int userid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            userid = Convert.ToInt16(Session["UserID"]);
            if (Convert.ToInt16(Session["UserRoleID"]) == 4) // Checking role of user by UserRoleID ( 4=Sys Admin)
            {
                #region
                //Only User tab is Visible for Sys admin
                HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                if (hcliuser != null)
                    (Page.Master.FindControl("liuser") as HtmlControl).Visible = true;

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

            else if (Convert.ToInt16(Session["UserRoleID"]) == (int)PMOscar.Utility.EnumTypes.UserRoles.Employees) // Checking role of user by UserRoleID ( 4=Employee)
            {
                #region

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
        [WebMethod]
        public static bool saveOptionalHolidays(string[] oh, string year)
        {
            var userId = HttpContext.Current.Session[Constants.SessionName.USERID];
            string empQuery = string.Format("select EmployeeCode  from [dbo].[User] where UserId='{0}'", userId);
            Object obj = BaseDAL.ExecuteScalar(empQuery);
            var empCode = obj.ToString();

            var query = "Select * From dbo.OhLog Where emp_Code  =" + empCode + " And Year =" + year;
            DataTable dt1 = BaseDAL.ExecuteDataTable(query);
            if (dt1.Rows.Count > 0)
            {
                //already entry for employee
                return false;
            }
            else
            {
                try
                {
                    var OhString = string.Join(",", oh);
                    string query1 = string.Format("insert into [dbo].[OhLog]([Year],[emp_Code],[Holidays]) values('{0}','{1}','{2}')", year, empCode, OhString);
                    int result = BaseDAL.ExecuteNonQuery(query1);
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}