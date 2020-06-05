
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateDashboard.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
//  Description  : Creation of the ProjectDashboard.
//  Author       : Anju Alias
//  Created Date : 14 April 2011
//  Modified By  : Muralikrishnan
//  Modified Date:   
// </summary>
// -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PMOscar
{
     public partial class CreateDashboard : System.Web.UI.Page
    {

        #region"Declaration"

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
            int fromMonth = 0;
            int toMonth = 0;
            int fromYear = 0;
            int toYear = 0;
            int month = 0;          
            string fromName = string.Empty;
            string toName = string.Empty;        
            DataTable dtDashboard = new DataTable();
            DataTable dtEstimation = new DataTable();
       
#endregion

        #region"Page Events"

            protected void Page_Load(object sender, EventArgs e)
        {
            if ( !IsPostBack )
            {
                FillYears(); // To fill the years in the DropDownList
               
                string year = DateTime.Now.Year.ToString();
                ddlYear.SelectedValue = year;
                string month = DateTime.Now.Month.ToString();
                ddlMonth.SelectedValue = month;

                GetWeek(); // Method for populating fromweek and week dropdownlist...

                int weekIndex = ddlWeek.SelectedIndex;

                if ( weekIndex == 0 && ddlMonth.SelectedItem.Text!="January" )
                {
                    ddlMonth.SelectedIndex = ddlMonth.SelectedIndex - 1;
                    ddlWeek.SelectedValue = ddlWeek.Items[ddlWeek.Items.Count - 1].Value;
                }

                else if ( weekIndex == 0 && ddlMonth.SelectedItem.Text == "January" )
                {
                    ddlMonth.SelectedValue =ddlMonth.Items[ddlMonth.Items.Count - 1].Value;
                    ddlYear.SelectedIndex = ddlYear.SelectedIndex - 1;
                    ddlWeek.SelectedValue = ddlWeek.Items[ddlWeek.Items.Count - 1].Value;
                }

                else
                   ddlWeek.SelectedIndex = weekIndex - 1;
                
                pnlWeeklySearch.Visible = true;
                pnlMultiWeelySearch.Visible = false;
            }

            lblMsg.Text = string.Empty;
            btnClose.Attributes.Add("OnClick", "opener.location.reload();self.close();return true;");
        }

        #endregion

        #region"Methods"

            // Method for Populating Year Combobox...
            private void FillYears()
        {
            for (int yearCount = DateTime.Now.Year - 4; yearCount <= DateTime.Now.Year + 1; yearCount++)
            {
                ListItem lstYears = new ListItem();
                lstYears.Value = yearCount.ToString();
                lstYears.Text = yearCount.ToString();
                if (ddlProjectPeriod.SelectedItem.Text == "Weekly")
                {
                    ddlYear.Items.Add(lstYears);
                    ddlYear.DataBind();
                }
                else
                {
                    ddlFromYear.Items.Add(lstYears);
                    ddlFromYear.DataBind();
                    ddlToYear.Items.Add(lstYears);
                    ddlToYear.DataBind();
                }
            }
        }

            // Method for getting dates and months...
            private void setPeriod()
        {
            int dedFactor = 0;
            month = ddlWeek.SelectedIndex == 0 && Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) > Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - (ddlWeek.SelectedItem.ToString().Length > 3 ? 3 : 2))) ? Convert.ToInt16(ddlMonth.SelectedIndex) : (ddlWeek.SelectedIndex != 0 &&
                                                   Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) >
                                                   Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1,
                                                   ddlWeek.SelectedItem.ToString().Length - (ddlWeek.SelectedItem.ToString().Length > 3 ? 3 : 2))) ? Convert.ToInt16(ddlMonth.SelectedIndex + 2) :
                                                   Convert.ToInt16(ddlMonth.SelectedIndex + 1));

            if (ddlWeek.SelectedItem.ToString().Length > 4)
                dedFactor = 3;
            
            else if (ddlWeek.SelectedItem.ToString().Length == 3)           
                dedFactor = 2;
            
            else            
                dedFactor = ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().Length - 3, 1).ToString() == "-" ? 2 : 3;
           

            if (ddlWeek.SelectedIndex == 0 && Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) > Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - dedFactor)))
            {
                fromMonth = Convert.ToInt16(ddlMonth.SelectedIndex) == 0 ? 12 : Convert.ToInt16(ddlMonth.SelectedIndex);
                toMonth = ddlMonth.SelectedIndex == 11 ? (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) : (ddlMonth.SelectedIndex == 11 ? (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) : (Convert.ToInt16(ddlMonth.SelectedIndex) + 1));
                fromYear = Convert.ToInt16(ddlMonth.SelectedIndex) == 0 ? (Convert.ToInt16(ddlYear.SelectedValue) - 1) : (Convert.ToInt16(ddlMonth.SelectedIndex) == 11 ? (Convert.ToInt16(ddlYear.SelectedValue)) : Convert.ToInt16(ddlYear.SelectedValue));
                toYear = Convert.ToInt16(ddlYear.SelectedValue);             

                fromName = Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) + "/" + fromMonth + "/" + fromYear;
                toName = Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - dedFactor)) + "/" + toMonth + "/" + toYear;
      
                dayFrom = fromYear + "-" + fromMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-")));
                dayTo = toYear + "-" + toMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - dedFactor));
            }

            else if (ddlWeek.SelectedIndex != 0 && Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) >
                                                   Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1,
                                                   ddlWeek.SelectedItem.ToString().Length - dedFactor)))
            {

                fromMonth = ddlMonth.SelectedIndex == 11 ? (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) : (Convert.ToInt16(ddlMonth.SelectedIndex) + 1);
                toMonth = ddlMonth.SelectedIndex == 11 ? 1 : (Convert.ToInt16(ddlMonth.SelectedIndex) + 2);
                fromYear = Convert.ToInt16(ddlMonth.SelectedIndex) == 0 ? (Convert.ToInt16(ddlYear.SelectedValue)) : (Convert.ToInt16(ddlMonth.SelectedIndex) == 11 ? Convert.ToInt16(ddlYear.SelectedValue) : Convert.ToInt16(ddlYear.SelectedValue));
                toYear = Convert.ToInt16(ddlMonth.SelectedIndex) == 11 ? Convert.ToInt16(ddlYear.SelectedValue) + 1 : Convert.ToInt16(ddlYear.SelectedValue);
                            
                fromName = Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) + "/" + fromMonth + "/" + fromYear;
                toName = Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - dedFactor)) + "/" + toMonth + "/" + toYear;
                
                dayFrom = fromYear + "-" + fromMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-")));
                dayTo = toYear + "-" + toMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - dedFactor));
            }

            else
            {
                fromMonth = ddlMonth.SelectedIndex == 11 ? 1 : (Convert.ToInt16(ddlMonth.SelectedIndex) + 1);
                toMonth = ddlMonth.SelectedIndex == 11 ? 1 : (Convert.ToInt16(ddlMonth.SelectedIndex) + 1);
                fromYear = Convert.ToInt16(ddlYear.SelectedValue);
                toYear = Convert.ToInt16(ddlYear.SelectedValue);

                fromName = Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-"))) + "/" + (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) + "/" + fromYear;
                toName = Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - dedFactor)) + "/" + (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) + "/" + toYear;
     
                dayFrom = fromYear + "-" + (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-")));
                dayTo = fromYear + "-" + (Convert.ToInt16(ddlMonth.SelectedIndex) + 1) + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(ddlWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlWeek.SelectedItem.ToString().Length - dedFactor));
             }
        }

            // Method for getting dates and names...
            private void setMultiWeeklyPeriod()
        {
            int dedFactor = 0;
            int todedFactor = 0;

            if ( ddlFromWeek.SelectedItem.ToString().Length > 4 )
                dedFactor = 3;            
            else if ( ddlFromWeek.SelectedItem.ToString().Length == 3 )
                dedFactor = 2;            
            else           
                dedFactor = ddlFromWeek.SelectedItem.ToString().Substring(ddlFromWeek.SelectedItem.ToString().Length - 3, 1).ToString() == "-" ? 2 : 3;
            
            if ( ddlToWeek.SelectedItem.ToString().Length > 4 )
                todedFactor = 3;            
            else if ( ddlToWeek.SelectedItem.ToString().Length == 3 )
                todedFactor = 2;           
            else           
                todedFactor = ddlToWeek.SelectedItem.ToString().Substring(ddlToWeek.SelectedItem.ToString().Length - 3, 1).ToString() == "-" ? 2 : 3;
           
            if ( ddlFromWeek.SelectedIndex == 0 && Convert.ToInt16(ddlFromWeek.SelectedItem.ToString().Substring(0, ddlFromWeek.SelectedItem.ToString().IndexOf("-"))) > Convert.ToInt16(ddlFromWeek.SelectedItem.ToString().Substring(ddlFromWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlFromWeek.SelectedItem.ToString().Length - dedFactor)) )
            {
                fromMonth = Convert.ToInt16(ddlfromMonth.SelectedIndex) == 0 ? 12 : Convert.ToInt16(ddlfromMonth.SelectedIndex);
                fromYear = Convert.ToInt16(ddlfromMonth.SelectedIndex) == 0 ? (Convert.ToInt16(ddlFromYear.SelectedValue) - 1) : (Convert.ToInt16(ddlfromMonth.SelectedIndex) == 11 ? (Convert.ToInt16(ddlFromYear.SelectedValue)) : Convert.ToInt16(ddlFromYear.SelectedValue));
            }
            else if ( ddlFromWeek.SelectedIndex != 0 && Convert.ToInt16(ddlFromWeek.SelectedItem.ToString().Substring(0, ddlFromWeek.SelectedItem.ToString().IndexOf("-"))) >
                                                   Convert.ToInt16(ddlFromWeek.SelectedItem.ToString().Substring(ddlFromWeek.SelectedItem.ToString().IndexOf("-") + 1,
                                                   ddlFromWeek.SelectedItem.ToString().Length - dedFactor)) )
            {
                fromMonth = ddlfromMonth.SelectedIndex == 11 ? (Convert.ToInt16(ddlfromMonth.SelectedIndex) + 1) : (Convert.ToInt16(ddlfromMonth.SelectedIndex) + 1);
                fromYear = Convert.ToInt16(ddlFromYear.SelectedIndex) == 0 ? (Convert.ToInt16(ddlFromYear.SelectedValue)) : (Convert.ToInt16(ddlfromMonth.SelectedIndex) == 11 ? Convert.ToInt16(ddlFromYear.SelectedValue) : Convert.ToInt16(ddlFromYear.SelectedValue));

            }
            else if ( ddlFromWeek.SelectedIndex != 0 && Convert.ToInt16(ddlFromWeek.SelectedItem.ToString().Substring(0, ddlFromWeek.SelectedItem.ToString().IndexOf("-"))) <
                                                   Convert.ToInt16(ddlFromWeek.SelectedItem.ToString().Substring(ddlFromWeek.SelectedItem.ToString().IndexOf("-") + 1,
                                                   ddlFromWeek.SelectedItem.ToString().Length - dedFactor)) )
            {
                fromMonth = Convert.ToInt16(ddlfromMonth.SelectedIndex) + 1;
                fromYear = Convert.ToInt16 (ddlFromYear.SelectedValue);
            }

            else
            {
                fromMonth = ddlfromMonth.SelectedIndex == 11 ? 1 : (Convert.ToInt16(ddlfromMonth.SelectedIndex) + 1);
                fromYear = Convert.ToInt16(ddlFromYear.SelectedValue);
            }

            if ( ddlToWeek.SelectedIndex == 0 && Convert.ToInt16(ddlToWeek.SelectedItem.ToString().Substring(0, ddlToWeek.SelectedItem.ToString().IndexOf("-"))) > Convert.ToInt16(ddlToWeek.SelectedItem.ToString().Substring(ddlToWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlToWeek.SelectedItem.ToString().Length - todedFactor)) )
            {
                toMonth = ddlToMonth.SelectedIndex == 11 ? (Convert.ToInt16(ddlToMonth.SelectedIndex) + 1) : (ddlToMonth.SelectedIndex == 11 ? (Convert.ToInt16(ddlToMonth.SelectedIndex) + 1) : (Convert.ToInt16(ddlToMonth.SelectedIndex) + 1));
                toYear = Convert.ToInt16(ddlToYear.SelectedValue);

            }
            else if ( ddlToWeek.SelectedIndex != 0 && Convert.ToInt16(ddlToWeek.SelectedItem.ToString().Substring(0, ddlToWeek.SelectedItem.ToString().IndexOf("-"))) >
                                                   Convert.ToInt16(ddlToWeek.SelectedItem.ToString().Substring(ddlToWeek.SelectedItem.ToString().IndexOf("-") + 1,
                                                   ddlToWeek.SelectedItem.ToString().Length - todedFactor)) )
            {
                toMonth = ddlToMonth.SelectedIndex == 11 ? 1 : (Convert.ToInt16(ddlToMonth.SelectedIndex) + 2);
                toYear = Convert.ToInt16(ddlToMonth.SelectedIndex) == 11 ? Convert.ToInt16(ddlToYear.SelectedValue) + 1 : Convert.ToInt16(ddlToYear.SelectedValue);
            }

            else if ( ddlToWeek.SelectedIndex != 0 && Convert.ToInt16(ddlToWeek.SelectedItem.ToString().Substring(0, ddlToWeek.SelectedItem.ToString().IndexOf("-"))) <
                                                   Convert.ToInt16(ddlToWeek.SelectedItem.ToString().Substring(ddlToWeek.SelectedItem.ToString().IndexOf("-") + 1,
                                                   ddlToWeek.SelectedItem.ToString().Length - todedFactor)) )
            {
                toMonth = (Convert.ToInt16(ddlToMonth.SelectedIndex) + 1);
                toYear = Convert.ToInt16(ddlToYear.SelectedValue);
            }

            else
            {
                toMonth = ddlToMonth.SelectedIndex == 11 ? 1 : (Convert.ToInt16(ddlToMonth.SelectedIndex) + 1);
                toYear = Convert.ToInt16(ddlToYear.SelectedValue);
            }

            fromName = Convert.ToInt16(ddlFromWeek.SelectedItem.ToString().Substring(0, ddlFromWeek.SelectedItem.ToString().IndexOf("-"))) + "/" + fromMonth + "/" + fromYear;
            toName = Convert.ToInt16(ddlToWeek.SelectedItem.ToString().Substring(ddlToWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlToWeek.SelectedItem.ToString().Length - todedFactor)) + "/" + toMonth + "/" + toYear;

            dayFrom = fromYear + "-" + fromMonth + "-" + Convert.ToInt16(ddlFromWeek.SelectedItem.ToString().Substring(0, ddlFromWeek.SelectedItem.ToString().IndexOf("-")));
            dayTo = toYear + "-" + toMonth + "-" + Convert.ToInt16(ddlToWeek.SelectedItem.ToString().Substring(ddlToWeek.SelectedItem.ToString().IndexOf("-") + 1, ddlToWeek.SelectedItem.ToString().Length - todedFactor));                 
        }

            // Method for populating fromweek and week dropdownlist...
            private void GetWeek()
        { 
            ddlWeek.Items.Clear();
            ddlFromWeek.Items.Clear();

            int currentMonthStartingDay = 1;
            int previousMonth = 1;

            currentYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            currentMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());

            if (ddlProjectPeriod.SelectedItem.Text == "Weekly")
            {
                currentYear = Convert.ToInt32(ddlYear.SelectedValue.ToString());
                currentMonth = Convert.ToInt32(ddlMonth.SelectedValue.ToString());
            }
            else
            {
                currentYear = Convert.ToInt32(ddlFromYear.SelectedValue.ToString());
                currentMonth = Convert.ToInt32(ddlfromMonth.SelectedValue.ToString());
            }

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

            previousMonthWeekText = GetFirstWeekText();    // To get the start day and end day for the first week text in the dropdownlist... 

            ListItem lstFirstWeekText = new ListItem();
            lstFirstWeekText.Text = previousMonthWeekText;
          
            if (ddlProjectPeriod.SelectedItem.Text == "Weekly")
            {
                ddlWeek.Items.Add(lstFirstWeekText);
                ddlWeek.DataBind();
            }
            else
            {
                ddlFromWeek.Items.Add(lstFirstWeekText);
                ddlFromWeek.DataBind();
            }


            if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Day <= endDay && DateTime.Now.Date.Month == ddlMonth.SelectedIndex + 1)
            {
                // Modified  ddlWeek.SelectedIndex = 0 as a quick fix to resolve server error while choosing May from Month dropdown
                // NOTE that there is no previous month period is listing as the first item while choosing May from Month dropdown.
                //ddlWeek.SelectedIndex = 1;
                ddlWeek.SelectedIndex = 0;
            }

            currentMonthDays = GetMonthDays(currentMonth, currentYear); // To get the number of days in the current month.
            GetMiddleWeekTextFromWeek();  // To get the start day and end day for the middle week texts in the from week dropdownlist...

            DateTime currentMonthLastDayDate = new DateTime(currentYear, currentMonth, currentMonthDays);
            currentMonthLastDay = Convert.ToString(currentMonthLastDayDate.DayOfWeek.ToString());
            DateTime nextMonthDayDate = new DateTime(nextYear, nextMonth, nextMonthStartingDay);
            nextMonthDay = Convert.ToString(nextMonthDayDate.DayOfWeek.ToString());

            currentMonthDayNo = 1;
            previousMonthDayNo = currentMonthDays;

            previousMonthWeekText = GetLastWeekText(); // To get the start day and end day for the last week text in the dropdownlist...
            if (status == 0)
            {
                previousMonthWeekText = startDay.ToString() + '-' + endDay.ToString();
                ListItem lstLastWeekText = new ListItem();
                lstLastWeekText.Text = previousMonthWeekText;

                if (ddlProjectPeriod.SelectedItem.Text == "Weekly")
                {

                    ddlWeek.Items.Add(lstLastWeekText);
                    if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Month == ddlMonth.SelectedIndex + 1)
                        lstLastWeekText.Selected = true;
                    ddlWeek.DataBind();
                }
                else
                {
                    ddlFromWeek.Items.Add(lstLastWeekText);
                    if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Month == ddlMonth.SelectedIndex + 1)
                    {
                        lstLastWeekText.Selected = true;
                    }

                    ddlFromWeek.DataBind();
                }
            }
        }

            // Method for populating toweek dropdownlist...
            private void GetToPeriodWeek()
        {          
            ddlToWeek.Items.Clear();
            int currentMonthStartingDay = 1;
            int previousMonth = 1;

            currentYear = Convert.ToInt32(ddlToYear.SelectedValue.ToString());
            currentMonth = Convert.ToInt32(ddlToMonth.SelectedValue.ToString());

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

            previousMonthWeekText = GetFirstWeekText();    // To get the start day and end day for the first week text in the dropdownlist... 

            ListItem lstFirstWeekText = new ListItem();
            lstFirstWeekText.Text = previousMonthWeekText;
            ddlToWeek.Items.Add(lstFirstWeekText);
            ddlToWeek.DataBind();
             

            if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Day <= endDay && DateTime.Now.Date.Month == ddlMonth.SelectedIndex + 1)
            {
                // Modified  ddlWeek.SelectedIndex = 0 as a quick fix to resolve server error while choosing May from Month dropdown
                // NOTE that there is no previous month period is listing as the first item while choosing May from Month dropdown.
                //ddlWeek.SelectedIndex = 1;
                ddlToWeek.SelectedIndex = 0;
            }

            currentMonthDays = GetMonthDays(currentMonth, currentYear); // To get the number of days in the current month.
            GetMiddleWeekTextToWeek();  // To get the start day and end day for the middle week texts in the dropdownlist...

            DateTime currentMonthLastDayDate = new DateTime(currentYear, currentMonth, currentMonthDays);
            currentMonthLastDay = Convert.ToString(currentMonthLastDayDate.DayOfWeek.ToString());
            DateTime nextMonthDayDate = new DateTime(nextYear, nextMonth, nextMonthStartingDay);
            nextMonthDay = Convert.ToString(nextMonthDayDate.DayOfWeek.ToString());

            currentMonthDayNo = 1;
            previousMonthDayNo = currentMonthDays;

           previousMonthWeekText =  GetLastWeekText(); // To get the start day and end day for the last week text in the dropdownlist...
           if (status == 0)
           {
               previousMonthWeekText = startDay.ToString() + '-' + endDay.ToString();
               ListItem lstLastWeekText = new ListItem();
               lstLastWeekText.Text = previousMonthWeekText;
               ddlToWeek.Items.Add(lstLastWeekText);
               
               if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Month == ddlToMonth.SelectedIndex + 1)
                   lstLastWeekText.Selected = true;
               
               ddlToWeek.DataBind();
           }
        }              

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
            private string GetFirstWeekText()
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
            //ListItem lstFirstWeekText = new ListItem();
            //lstFirstWeekText.Text = previousMonthWeekText;
            ////ddlWeek.Items.Add(lstFirstWeekText);
            ////ddlWeek.DataBind();

            return previousMonthWeekText;
        }

            // To get the start day and end day for the middle week texts in the toweek dropdownlist...
            private void GetMiddleWeekTextToWeek()
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
                ddlToWeek.Items.Add(lstMiddleWeekTexts);
                ddlToWeek.DataBind();
                if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Day <= endDay && DateTime.Now.Date.Month == ddlMonth.SelectedIndex + 1)
                    ddlToWeek.SelectedIndex = middleWeekTextsCount + 1;
                 
            }
        }

            // To get the start day and end day for the middle week texts in the fromweek dropdownlist...
            private void GetMiddleWeekTextFromWeek()
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
                if (ddlProjectPeriod.SelectedItem.Text == "Weekly")
                {
                    ddlWeek.Items.Add(lstMiddleWeekTexts);
                    ddlWeek.DataBind();
                }
                else
                {
                    ddlFromWeek.Items.Add(lstMiddleWeekTexts);
                    ddlFromWeek.DataBind();
                }
               
                if (DateTime.Now.Date.Day >= startDay && DateTime.Now.Date.Day <= endDay && DateTime.Now.Date.Month == ddlMonth.SelectedIndex + 1)
                {                   
                    if (ddlProjectPeriod.SelectedItem.Text == "Weekly")
                        ddlWeek.SelectedIndex = middleWeekTextsCount + 1;
                    else
                        ddlFromWeek.SelectedIndex = middleWeekTextsCount + 1;                   
                }
            }
        }

            // To get the start day and end day for the last week text in the dropdownlist...
            private string  GetLastWeekText()
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

            return previousMonthWeekText;
        }

            // Method to insert project details in the dashboard...
        private void insertDashboardDetails()
        {
            string uptoDate = string.Empty;
            if ( ddlProjectPeriod.SelectedItem.Text == "Weekly" )
            {
                setPeriod();  // Method for getting dates and months...
                uptoDate = fromYear + "-" + fromMonth + "-" + Convert.ToInt16(ddlWeek.SelectedItem.ToString().Substring(0, ddlWeek.SelectedItem.ToString().IndexOf("-")));
            }
            else
            {
                setMultiWeeklyPeriod(); // Method for getting dates and names...
                uptoDate = fromYear + "-" + fromMonth + "-" + Convert.ToInt16(ddlFromWeek.SelectedItem.ToString().Substring(0, ddlFromWeek.SelectedItem.ToString().IndexOf("-")));
            }

            DataTable dt1 = PMOscar.BaseDAL.ExecuteDataTable("Select * from Dashboard where Name='" + fromName + "-" + toName+"'");
            if (dt1.Rows.Count > 0)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Dashboard has already been created for this period.";
            }
            else
            {
                List<SqlParameter> parameter = new List<SqlParameter>();
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@Name", fromName + "-" + toName));
                parameter.Add(new SqlParameter("@FromDate", dayFrom));
                parameter.Add(new SqlParameter("@ToDate", dayTo));
                parameter.Add(new SqlParameter("@PeriodType", ddlProjectPeriod.SelectedItem.Text == "Weekly" ? "W" : "M"));
                parameter.Add(new SqlParameter("@Status", "I")); //I:-Inprogress,F:-Finalized
                SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
                returnValue.Direction = ParameterDirection.ReturnValue;
                parameter.Add(returnValue);
                PMOscar.BaseDAL.ExecuteSPNonQuery("DashboardOperations", parameter);
                int dashboardID = Convert.ToInt16(returnValue.Value);
                DataTable dt = PMOscar.BaseDAL.ExecuteDataTable("Select Status from Dashboard where DashboardID=" + dashboardID);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Status"].ToString() == "F")
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Dashboard for the selected period has already been finalized.";
                    }
                    else
                    {
                        PMOscar.BaseDAL.ExecuteNonQuery("Delete from dbo.ProjectDashboardEstimation where DashboardID=" + dashboardID);
                        dtDashboard = new DataTable();
                        parameter = new List<SqlParameter>();
                        parameter.Add(new SqlParameter("@dayfrom", dayFrom));
                        parameter.Add(new SqlParameter("@dayto", dayTo));
                        parameter.Add(new SqlParameter("@uptodate", uptoDate));
                        parameter.Add(new SqlParameter("@tabType", "dashboard"));
                        parameter.Add(new SqlParameter("@Status", "Create"));
                        parameter.Add(new SqlParameter("@DashboardID", dashboardID));
                        parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", true));
                        dtDashboard = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];

                        dtEstimation = new DataTable();
                        parameter = new List<SqlParameter>();
                        parameter.Add(new SqlParameter("@dayfrom", dayFrom));
                        parameter.Add(new SqlParameter("@dayto", dayTo));
                        parameter.Add(new SqlParameter("@uptodate", uptoDate));
                        parameter.Add(new SqlParameter("@tabType", "Estimation"));
                        parameter.Add(new SqlParameter("@Status", "Create"));
                        parameter.Add(new SqlParameter("@DashboardID", "1"));
                        parameter.Add(new SqlParameter("@IsIncludeProposalAndVAS", true));
                        dtEstimation = PMOscar.BaseDAL.ExecuteSPDataSet("[getProjectDashboardDetails]", parameter).Tables[0];

                        for (int insertPjctDtsCount = 0; insertPjctDtsCount < dtDashboard.Rows.Count; insertPjctDtsCount++)
                        {
                            parameter = new List<SqlParameter>();
                            parameter.Add(new SqlParameter("@ProjectID", dtDashboard.Rows[insertPjctDtsCount]["ProjectID"]));
                            parameter.Add(new SqlParameter("@PhaseID", dtDashboard.Rows[insertPjctDtsCount]["PhaseID"]));
                            parameter.Add(new SqlParameter("@ClientStatus", 3));
                            parameter.Add(new SqlParameter("@TimelineStatus", 3));
                            parameter.Add(new SqlParameter("@BudgetStatus", 3));
                            parameter.Add(new SqlParameter("@EscalateStatus", 3));
                            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
                            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                            parameter.Add(new SqlParameter("@Status", "I"));
                            parameter.Add(new SqlParameter("@DashboardID", dashboardID));
                            parameter.Add(new SqlParameter("@ProjectName", dtDashboard.Rows[insertPjctDtsCount]["ProjectName"]));
                            parameter.Add(new SqlParameter("@ShortName", dtDashboard.Rows[insertPjctDtsCount]["ShortName"]));
                            parameter.Add(new SqlParameter("@ProjectType", dtDashboard.Rows[insertPjctDtsCount]["ProjectType"]));
                            parameter.Add(new SqlParameter("@ProjectOwner", dtDashboard.Rows[insertPjctDtsCount]["ProjectOwnerID"]));
                            parameter.Add(new SqlParameter("@ProjectManager", dtDashboard.Rows[insertPjctDtsCount]["ProjectManagerID"]));
                            parameter.Add(new SqlParameter("@DeliveryDate", dtDashboard.Rows[insertPjctDtsCount]["DeliveryDate"]));
                            parameter.Add(new SqlParameter("@RevisedDeliveryDate", dtDashboard.Rows[insertPjctDtsCount]["RevisedDeliveryDate"]));
                            parameter.Add(new SqlParameter("@PMComments", dtDashboard.Rows[insertPjctDtsCount]["PMComments"]));
                            parameter.Add(new SqlParameter("@POComments", dtDashboard.Rows[insertPjctDtsCount]["POComments"]));
                            parameter.Add(new SqlParameter("@DeliveryComments", dtDashboard.Rows[insertPjctDtsCount]["DeliveryComments"]));
                            parameter.Add(new SqlParameter("@Comments", ""));
                            parameter.Add(new SqlParameter("@isActive", 1));
                            parameter.Add(new SqlParameter("@Utilization", dtDashboard.Rows[insertPjctDtsCount]["Utilization"]));
                            int projectDashboardID = PMOscar.BaseDAL.ExecuteSPScalar("[ProjectDashboardOperations]", parameter);
                            BaseDAL.insertProjectActivityStatus(projectDashboardID, 3, 3, 3, 3, Session["UserID"], Session["UserID"], Convert.ToInt32(dtDashboard.Rows[insertPjctDtsCount]["ProjectID"]), dashboardID);
                        }

                        for (int insertPjctEstimationDtsCount = 0; insertPjctEstimationDtsCount < dtEstimation.Rows.Count; insertPjctEstimationDtsCount++)
                        {
                            parameter = new List<SqlParameter>();
                            parameter.Add(new SqlParameter("@DashboardID", dashboardID));
                            parameter.Add(new SqlParameter("@ProjectID", dtEstimation.Rows[insertPjctEstimationDtsCount]["ProjectID"]));
                            parameter.Add(new SqlParameter("@PhaseID", dtEstimation.Rows[insertPjctEstimationDtsCount]["PhaseID"]));
                            parameter.Add(new SqlParameter("@EstimationRoleID", dtEstimation.Rows[insertPjctEstimationDtsCount]["EstimationRoleID"]));
                            parameter.Add(new SqlParameter("@BillableHours", dtEstimation.Rows[insertPjctEstimationDtsCount]["BillableHours"]));
                            parameter.Add(new SqlParameter("@BudgetHours", dtEstimation.Rows[insertPjctEstimationDtsCount]["BudgetHours"]));
                            parameter.Add(new SqlParameter("@RevisedBudgetHours", dtEstimation.Rows[insertPjctEstimationDtsCount]["RevisedBudgetHours"]));
                            parameter.Add(new SqlParameter("@ActualHrs", dtEstimation.Rows[insertPjctEstimationDtsCount]["ActualHours"]));
                            parameter.Add(new SqlParameter("@PeriodEstimetedHrs", dtEstimation.Rows[insertPjctEstimationDtsCount]["periodEstimatedHours"]));
                            parameter.Add(new SqlParameter("@PeriodActualHrs", dtEstimation.Rows[insertPjctEstimationDtsCount]["PeriodActualHours"]));
                            parameter.Add(new SqlParameter("@PeriodEstimetedHrsAdj", Convert.ToDouble(dtEstimation.Rows[insertPjctEstimationDtsCount]["periodEstimatedHours"])));
                            parameter.Add(new SqlParameter("@PeriodActualHrsAdj", Convert.ToDouble(dtEstimation.Rows[insertPjctEstimationDtsCount]["PeriodActualHours"])));
                            parameter.Add(new SqlParameter("@EstRoleName", dtEstimation.Rows[insertPjctEstimationDtsCount]["EstRoleName"]));
                            parameter.Add(new SqlParameter("@EstRoleShrtName", dtEstimation.Rows[insertPjctEstimationDtsCount]["EstRoleshortName"]));
                            parameter.Add(new SqlParameter("@RoleName", dtEstimation.Rows[insertPjctEstimationDtsCount]["Role"]));
                            parameter.Add(new SqlParameter("@RoleShrtName", dtEstimation.Rows[insertPjctEstimationDtsCount]["ShortName"]));
                            parameter.Add(new SqlParameter("@ActualHrsCorrected", Convert.ToDouble(dtEstimation.Rows[insertPjctEstimationDtsCount]["ActualHours"])));
                            parameter.Add(new SqlParameter("@RevisedComments", dtEstimation.Rows[insertPjctEstimationDtsCount]["RevisedComments"]));
                            PMOscar.BaseDAL.ExecuteSPNonQuery("[ProjectDashboardEstimationOperations]", parameter);
                        }

                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = PMOscar.Core.Constants.AddRole.CREATEDDASHBOARD;
                    }
                }
                dt.Dispose();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Dashboard has created successfully.')", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.reload(true);self.close();</script>");
            }
      }
        #endregion

        #region"Control Events"

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( Convert.ToInt32(ddlYear.SelectedItem.Value) != DateTime.Now.Year )
                ddlMonth.SelectedIndex = 0;           
            else            
                ddlMonth.SelectedIndex = DateTime.Now.Date.Month - 1;
            
            pnlWeeklySearch.Visible = true;
            pnlMultiWeelySearch.Visible = false;
            GetWeek();  // Method for populating fromweek and week dropdownlist...         
         }

            protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlWeeklySearch.Visible = true;
            pnlMultiWeelySearch.Visible = false;
            GetWeek(); // Method for populating fromweek and week dropdownlist...
        }

            protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlWeeklySearch.Visible = true;
            pnlMultiWeelySearch.Visible = false; 
        }

            protected void btnCreate_Click(object sender, EventArgs e)
        {
            insertDashboardDetails();   // Method to insert project details in the dashboard...
        }

            protected void ddlProjectPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( ddlProjectPeriod.SelectedItem.Text == "Weekly" )
            {
                pnlWeeklySearch.Visible = true;
                pnlMultiWeelySearch.Visible = false;

                FillYears(); // Method for Populating Year Combobox...

                string year = DateTime.Now.Year.ToString();
                ddlYear.SelectedValue = year;
                string month = DateTime.Now.Month.ToString();
                ddlMonth.SelectedValue = month;

                GetWeek();  // Method for populating fromweek and week dropdownlist...
            }

            else if ( ddlProjectPeriod.SelectedItem.Text == "Multi-Week" )
            {
                pnlWeeklySearch.Visible = false;
                pnlMultiWeelySearch.Visible = true;

                FillYears(); // Method for Populating Year Combobox...

                string year = DateTime.Now.Year.ToString();
                ddlFromYear.SelectedValue = year;
                ddlToYear.SelectedValue = year;
                string month = DateTime.Now.Month.ToString();
                ddlfromMonth.SelectedValue = month;
                ddlToMonth.SelectedValue = month;

                GetWeek(); // Method for populating fromweek and week dropdownlist...
                GetToPeriodWeek();  // Method for populating toweek dropdownlist...
            }
        }

            protected void ddlFromYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlWeeklySearch.Visible = false;
            pnlMultiWeelySearch.Visible = true;

            GetWeek(); // Method for populating fromweek and week dropdownlist...
            GetToPeriodWeek();  // Method for populating toweek dropdownlist...

            lblMsg.Text = string.Empty;          
        }

            protected void ddlFromMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlWeeklySearch.Visible = false;
            pnlMultiWeelySearch.Visible = true;

            GetWeek(); // Method for populating fromweek and week dropdownlist...
            GetToPeriodWeek();  // Method for populating toweek dropdownlist...

            lblMsg.Text = string.Empty;
        }

            protected void ddlFromWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlWeeklySearch.Visible = false;
            pnlMultiWeelySearch.Visible = true;
            lblMsg.Text = string.Empty;
        }

            protected void ddlToYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlWeeklySearch.Visible = false;
            pnlMultiWeelySearch.Visible = true;

            GetWeek(); // Method for populating fromweek and week dropdownlist...
            GetToPeriodWeek();  // Method for populating toweek dropdownlist...

            lblMsg.Text = string.Empty;
        }

            protected void ddlToMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlWeeklySearch.Visible = false;
            pnlMultiWeelySearch.Visible = true;

            GetWeek(); // Method for populating fromweek and week dropdownlist...
            GetToPeriodWeek();  // Method for populating toweek dropdownlist...

            lblMsg.Text = string.Empty;
        }

            protected void ddlToWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlWeeklySearch.Visible = false;
            pnlMultiWeelySearch.Visible = true;
            lblMsg.Text = string.Empty;
        }

            protected void btnMultiweekCreate_Click(object sender, EventArgs e)
        {
            if ( Convert.ToInt16(ddlFromYear.SelectedValue) > Convert.ToInt16(ddlToYear.SelectedValue) )
                lblMsg.Text = "From period must be less than to period.";
            else if ( Convert.ToInt16(ddlFromYear.SelectedValue) == Convert.ToInt16(ddlToYear.SelectedValue) && Convert.ToInt16(ddlfromMonth.SelectedValue) > Convert.ToInt16(ddlToMonth.SelectedValue) )
               lblMsg.Text = "From period must be less than to period.";            
            else if ( Convert.ToInt16(ddlFromYear.SelectedValue) == Convert.ToInt16(ddlToYear.SelectedValue) &&  Convert.ToInt16(ddlfromMonth.SelectedValue)== Convert.ToInt16(ddlToMonth.SelectedValue) && Convert.ToInt16(ddlFromWeek.SelectedIndex) > Convert.ToInt16(ddlToWeek.SelectedIndex) )
               lblMsg.Text = "From period must be less than to period.";            
            else
                insertDashboardDetails();  // Method to insert project details in the dashboard...
        }

            protected void btnClose_Click(object sender, EventArgs e)
        {
           ScriptManager.RegisterClientScriptBlock(btnClose, btnClose.GetType(), "KEY", "window.opener = top;window.self.close(); opener.location.reload();window.top.location.href=window.top.location.href;", true);
        }

        #endregion

    }
}
