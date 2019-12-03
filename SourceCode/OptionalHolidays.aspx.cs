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

    public partial class OptionalHolidays : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {

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