using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace PMOscar
{
    public partial class ResourceClone : System.Web.UI.Page
    {
        # region"Declarations"

        string fromDate = string.Empty;
        string toDate = string.Empty;
        int fromMonth = 0;
        int toMonth = 0;
        int fromYear = 0;
        int toYear = 0;       
        int month = 0;
        public string previousMonthWeekText = string.Empty;
        public string currentMonthWeekText = string.Empty;
        public string previousMonthDay = string.Empty;
        public string currentMonthDay = string.Empty;
        public string currentMonthLastDay = string.Empty;
        public string nextMonthDay = string.Empty;
        public int startDay = 0, endDay = 0, status = 0;
        public int currentMonthDayNo = 1;
        public int previousMonthDayNo = 0;
        public int nextMonth = 0;
        public int nextYear = 0;
        public int nextMonthStartingDay = 1;
        public int previousMonthDays = 0;
        public int currentMonthDays = 0;
        public int previousYear = 2011;
        public int currentYear = 0;
        public int currentMonth = 0;
        string dayFrom = string.Empty;
        string dayTo = string.Empty;

        #endregion

        #region"Page Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["dtpopup"] == null)
            {               
                btnClone.Enabled = false;
                ddlMonth.Enabled = false;
                ddlWeek.Enabled = false;
                ddlYear.Enabled = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "window.open('', '_self', ''); window.opener.location = '/Default.aspx'; window.close();", true);
            }

            if ( !IsPostBack )
            {
                FillYears(); // Method to fill year in the DropDownList...

                if ( Session["dtpopup"] != null )
                {
                    DataTable dtSession = (DataTable)Session["dtpopup"];
                    if ( dtSession.Rows.Count > 0 )
                    {
                        Session["fromdate"] = dtSession.Rows[0].ItemArray[2].ToString();
                        Session["todate"] = dtSession.Rows[0].ItemArray[3].ToString();
                        string year = DateTime.Now.Year.ToString();
                        ddlYear.SelectedValue = year;
                        string month = DateTime.Now.Month.ToString();
                        ddlMonth.SelectedValue = month;

                        GetWeek();   // Method to populate the weeks in selected Month and Year...
                    }
                    dtSession.Dispose();
                }
                lblErrormsg.Text = string.Empty;  
            }
        }

        #endregion

        #region"Methods"

        #region"Week Development Methods"

        // To get the number of days in the specified year and month...
        private int GetMonthDays(int monthId, int yearId)
        {
            int monthDays = 0;
            switch (Convert.ToInt32(monthId))
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    monthDays = 31;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    monthDays = 30;
                    break;
                case 2:
                    if (((Convert.ToInt32(yearId) % 4 == 0 & Convert.ToInt32(yearId) % 100 != 0) | (Convert.ToInt32(yearId) % 400 == 0)))
                    {
                        monthDays = 29;
                    }
                    else
                    {
                        monthDays = 28;
                    }
                    break;
            }
            return monthDays;
        }

        // To get the start day and end day for the first week text in the dropdownlist... 
        private void GetFirstWeekText()
        {
            if (currentMonth == 12)
            {
                nextYear = currentYear + 1;
                nextMonth = 1;
            }
            else
            {
                previousYear = currentYear;
                nextMonth = currentMonth + 1;
                nextYear = currentYear;
            }
            if (previousMonthDay == "Sunday")
                startDay = 1;

            if (currentMonthDay == "Sunday")
                endDay = currentMonthDayNo + 7;

            if (previousMonthDay == "Monday")
                startDay = previousMonthDayNo;

            if (currentMonthDay == "Monday")
                endDay = currentMonthDayNo + 6;

            if (previousMonthDay == "Tuesday")
                startDay = previousMonthDayNo - 1;

            if (currentMonthDay == "Tuesday")
                endDay = currentMonthDayNo + 5;

            if (previousMonthDay == "Wednesday")
                startDay = previousMonthDayNo - 2;

            if (currentMonthDay == "Wednesday")
                endDay = currentMonthDayNo + 4;

            if (previousMonthDay == "Thursday")
                startDay = previousMonthDayNo - 3;

            if (currentMonthDay == "Thursday")
                endDay = currentMonthDayNo + 3;

            if (previousMonthDay == "Friday")
                startDay = previousMonthDayNo - 4;

            if (currentMonthDay == "Friday")
                endDay = currentMonthDayNo + 2;

            if (previousMonthDay == "Saturday")
                startDay = currentMonthDayNo + 1;

            status = 0;

            if (currentMonthDay == "Saturday")
                endDay = currentMonthDayNo + 1;

            previousMonthWeekText = startDay.ToString() + '-' + endDay.ToString();
            ListItem lstFirstWeekText = new ListItem();
            lstFirstWeekText.Text = previousMonthWeekText;
            ddlWeek.Items.Add(lstFirstWeekText);
            ddlWeek.DataBind();
        }

        // To get the start day and end day for the middle week texts in the dropdownlist...
        private void GetMiddleWeekText()
        {
            for (int middleWeekTextsCount = 0; middleWeekTextsCount <= 5; middleWeekTextsCount++)
            {
                startDay = endDay + 1;
                endDay = startDay + 6;

                if (endDay > currentMonthDays)
                    break;

                currentMonthWeekText = startDay.ToString() + '-' + endDay.ToString();
                ListItem lstMiddleWeekTexts = new ListItem();
                lstMiddleWeekTexts.Text = currentMonthWeekText;
                ddlWeek.Items.Add(lstMiddleWeekTexts);
                ddlWeek.DataBind();
                if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Day <= endDay && DateTime.Now.Date.Month == ddlMonth.SelectedIndex + 1)
                    ddlWeek.SelectedIndex = middleWeekTextsCount + 1;
            }
        }

        // To get the start day and end day for the last week text in the dropdownlist...
        private void GetLastWeekText()
        {
            status = 0;

            if (currentMonthLastDay == "Sunday")
            {
                startDay = 1;
                status = 1;
            }

            if (nextMonthDay == "Sunday")
                endDay = 1;

            if (currentMonthLastDay == "Monday")
                startDay = previousMonthDayNo;

            if (nextMonthDay == "Monday")
                endDay = currentMonthDayNo + 6;

            if (currentMonthLastDay == "Tuesday")
                startDay = previousMonthDayNo - 1;

            if (nextMonthDay == "Tuesday")
                endDay = currentMonthDayNo + 5;

            if (currentMonthLastDay == "Wednesday")
                startDay = previousMonthDayNo - 2;

            if (nextMonthDay == "Wednesday")
                endDay = currentMonthDayNo + 4;

            if (currentMonthLastDay == "Thursday")
                startDay = previousMonthDayNo - 3;

            if (nextMonthDay == "Thursday")
                endDay = currentMonthDayNo + 3;

            if (currentMonthLastDay == "Friday")
                startDay = previousMonthDayNo - 4;

            if (nextMonthDay == "Friday")
                endDay = currentMonthDayNo + 2;

            if (currentMonthLastDay == "Saturday")
                startDay = previousMonthDayNo - 5;

            if (nextMonthDay == "Saturday")
                endDay = currentMonthDayNo + 1;

            if (status == 0)
            {
                previousMonthWeekText = startDay.ToString() + '-' + endDay.ToString();
                ListItem lstLastWeekText = new ListItem();
                lstLastWeekText.Text = previousMonthWeekText;
                ddlWeek.Items.Add(lstLastWeekText);
                if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Month == ddlMonth.SelectedIndex + 1)
                {
                    lstLastWeekText.Selected = true;
                }
                ddlWeek.DataBind();
            }
        }

        // Method to populate the weeks in selected Month and Year...
        private void GetWeek()
        {
           
            ddlWeek.Items.Clear();
            int currentMonthStartingDay = 1;
            int previousMonth = 1;

            currentYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            currentMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());

            if (currentMonth == 1)
            {
                previousYear = currentYear - 1;
                previousMonth = 12;
            }
            else
            {
                previousYear = currentYear;
                previousMonth = currentMonth - 1;
            }

            previousMonthDays = GetMonthDays(previousMonth, previousYear); // To get the number of days in the previous month.

            DateTime previousMonthDate = new DateTime(previousYear, previousMonth, previousMonthDays);
            previousMonthDay = Convert.ToString(previousMonthDate.DayOfWeek.ToString());
            DateTime currentMonthDate = new DateTime(currentYear, currentMonth, currentMonthStartingDay);
            currentMonthDay = Convert.ToString(currentMonthDate.DayOfWeek.ToString());
            previousMonthDayNo = previousMonthDays;

            GetFirstWeekText();    // To get the start day and end day for the first week text in the dropdownlist...            

            if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Day <= endDay && DateTime.Now.Date.Month == ddlMonth.SelectedIndex + 1)
            {
                // Modified  ddlWeek.SelectedIndex = 0 as a quick fix to resolve server error while choosing May from Month dropdown
                // NOTE that there is no previous month period is listing as the first item while choosing May from Month dropdown.
                //ddlWeek.SelectedIndex = 1;
                ddlWeek.SelectedIndex = 0;
            }

            currentMonthDays = GetMonthDays(currentMonth, currentYear); // To get the number of days in the current month.
            GetMiddleWeekText();  // To get the start day and end day for the middle week texts in the dropdownlist...

            DateTime currentMonthLastDayDate = new DateTime(currentYear, currentMonth, currentMonthDays);
            currentMonthLastDay = Convert.ToString(currentMonthLastDayDate.DayOfWeek.ToString());
            DateTime nextMonthDayDate = new DateTime(nextYear, nextMonth, nextMonthStartingDay);
            nextMonthDay = Convert.ToString(nextMonthDayDate.DayOfWeek.ToString());

            currentMonthDayNo = 1;
            previousMonthDayNo = currentMonthDays;

            GetLastWeekText(); // To get the start day and end day for the last week text in the dropdownlist...
        }

        #endregion

        // Method to fill year in the DropDownList...
        private void FillYears()
        {
            for (int yearCount = DateTime.Now.Year - 4; yearCount <= DateTime.Now.Year + 1; yearCount++)
            {
                ListItem ltYear = new ListItem();
                ltYear.Value = yearCount.ToString();
                ltYear.Text = yearCount.ToString();
                ddlYear.Items.Add(ltYear);
                ddlYear.DataBind();
            }
        }

        // Method for getting dates and months...
        private void SetPeriod()
        {
            int dedFactor = 0;

            month = ddlWeek.SelectedIndex == 0 && Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0,
            ddlWeek.SelectedItem.ToString().IndexOf("-"))) > Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1,
            ddlWeek.SelectedItem.ToString().Length - (ddlWeek.SelectedItem.ToString().Length > 3 ? 3 : 2))) ? Convert.ToInt16(ddlMonth.SelectedIndex) : (ddlWeek.SelectedIndex != 0 &&
            Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) >
            Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1,
            ddlWeek.SelectedItem.ToString().Length - (ddlWeek.SelectedItem.ToString().Length > 3 ? 3 : 2))) ? Convert.ToInt16(ddlMonth.SelectedIndex + 2) :
            Convert.ToInt16(ddlMonth.SelectedIndex + 1));

            if ( ddlWeek.SelectedItem.ToString().Length > 4 )            
                dedFactor = 3;            
            else if (ddlWeek.SelectedItem.ToString().Length == 3)            
                dedFactor = 2;            
            else            
               dedFactor = ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().Length - 3, 1).ToString() == "-" ? 2 : 3;
            

            if ( ddlWeek.SelectedIndex == 0 && Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) > Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - dedFactor)) )
            {
                fromMonth = Convert.ToInt16(ddlMonth.SelectedIndex) == 0 ? 12 : Convert.ToInt16(ddlMonth.SelectedIndex);
                toMonth = ddlMonth.SelectedIndex == 11 ? (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) : (ddlMonth.SelectedIndex == 11 ? (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) : (Convert.ToInt16(ddlMonth.SelectedIndex) + 1));
                fromYear = Convert.ToInt16(ddlMonth.SelectedIndex) == 0 ? (Convert.ToInt16(ddlYear.SelectedValue) - 1) : (Convert.ToInt16(ddlMonth.SelectedIndex) == 11 ? (Convert.ToInt16(ddlYear.SelectedValue)) : Convert.ToInt16(ddlYear.SelectedValue));
                toYear = Convert.ToInt16(ddlYear.SelectedValue);
                dayFrom = fromYear + "-" + fromMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-")));
                dayTo = toYear + "-" + toMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - dedFactor));
            }
            else if ( ddlWeek.SelectedIndex != 0 && Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) >
                                                   Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1,
                                                   ddlWeek.SelectedItem.ToString().Length - dedFactor)) )
            {

                fromMonth = ddlMonth.SelectedIndex == 11 ? (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) : (Convert.ToInt16(ddlMonth.SelectedIndex) + 1);
                toMonth = ddlMonth.SelectedIndex == 11 ? 1 : (Convert.ToInt16(ddlMonth.SelectedIndex) + 2);
                fromYear = Convert.ToInt16(ddlMonth.SelectedIndex) == 0 ? (Convert.ToInt16(ddlYear.SelectedValue)) : (Convert.ToInt16(ddlMonth.SelectedIndex) == 11 ? Convert.ToInt16(ddlYear.SelectedValue) : Convert.ToInt16(ddlYear.SelectedValue));
                toYear = Convert.ToInt16(ddlMonth.SelectedIndex) == 11 ? Convert.ToInt16(ddlYear.SelectedValue) + 1 : Convert.ToInt16(ddlYear.SelectedValue);
                dayFrom = fromYear + "-" + fromMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-")));
                dayTo = toYear + "-" + toMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - dedFactor));
            }

            else
            {
                fromMonth = ddlMonth.SelectedIndex == 11 ? 1 : (Convert.ToInt16(ddlMonth.SelectedIndex) + 1);
                toMonth = ddlMonth.SelectedIndex == 11 ? 1 : (Convert.ToInt16(ddlMonth.SelectedIndex) + 1);
                fromYear = Convert.ToInt16(ddlYear.SelectedValue);
                toYear = Convert.ToInt16(ddlYear.SelectedValue);
                dayFrom = fromYear + "-" + (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-")));
                dayTo = fromYear + "-" + (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - dedFactor));
            }
            Session["dayfrom"] = dayFrom;
            Session["dayto"] = dayTo;
        }

        #endregion

        #region"Control Events"

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetWeek();  // Method to populate the weeks in selected Month and Year...
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetWeek();  // Method to populate the weeks in selected Month and Year...
        }

        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPeriod();  // Method for getting dates and months...
        }

        protected void btnClone_Click(object sender, EventArgs e)
        {
            SetPeriod();  // Method for getting dates and months...

            fromDate = Session["fromdate"].ToString() != "" ? Session["fromdate"].ToString() : "";
            toDate = Session["todate"].ToString() != "" ? Session["todate"].ToString() : "";
            dayFrom = Session["dayfrom"].ToString() != "" ? Session["dayfrom"].ToString() : "";
            dayTo = Session["dayto"].ToString() != "" ? Session["dayto"].ToString() : "";

            DataTable dtDates = new DataTable();
            dtDates.Columns.Add("Duplication");
            dtDates.Columns.Add("year");
            dtDates.Columns.Add("month");
            dtDates.Columns.Add("weekindex");

            DataTable dtTimeTracker = PMOscar.BaseDAL.ExecuteDataTable("Select* From TimeTracker where FromDate>='" + dayFrom + "' and ToDate<='" + dayTo + "'");

            if ( dtTimeTracker.Rows.Count > 0 )
            {
                DataRow dr = dtDates.NewRow();
                dr["Duplication"] = "Y";
                dr["year"] = ddlYear.SelectedValue;
                dr["month"] = ddlMonth.SelectedValue;
                dr["weekindex"] = ddlWeek.SelectedIndex;
                dtDates.Rows.Add(dr);
                Session["dtClonevalue"] = dtDates;
                lblErrormsg.Text = "Data is already there for this Week!!";
                return;
            }
            else
            {
                IList<SqlParameter> parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@fromdate", fromDate));
                parameter.Add(new SqlParameter("@todate", toDate));
                parameter.Add(new SqlParameter("@clonefromdate", dayFrom));
                parameter.Add(new SqlParameter("@clonetodate", dayTo));
                PMOscar.BaseDAL.ExecuteSPNonQuery("[CloneResourcePlanningDetails]", parameter);

                DataRow dr = dtDates.NewRow();
                dr["Duplication"] = "N";
                dr["year"] = ddlYear.SelectedValue;
                dr["month"] = ddlMonth.SelectedValue;
                dr["weekindex"] = ddlWeek.SelectedIndex;
                dtDates.Rows.Add(dr);
                Session["dtClonevalue"] = dtDates;
                dtDates.Dispose();
                lblErrormsg.Text = "Data is cloned successfully!!";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Key", "window.opener.location.reload();window.history.back();self.close();", true);

            }
            dtTimeTracker.Dispose();
        }  

        #endregion
    }
}
