// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectDashboardEntry.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
//  Description  : Lists the ProjectDashboard Details.
//  Author       : Anju Alias
//  Created Date : 21 April 2011
//  Modified By  : Muralikrishnan
//  Modified Date: 25 Jan 2012  
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

namespace PMOscar
{
    public partial class ProjectDashboard : System.Web.UI.Page
    {

    #region"Declaration"

        private  IList<SqlParameter> parameter;
        private int ProjectDashBoardEditId = 0;        
        int year;
        int month;
        int weekindex;
        int projectId = 0;
        string projectName = string.Empty;
       
    #endregion

    #region"Page Events"

        protected void Page_Load(object sender, EventArgs e)
        {  
            ProjectDashBoardEditId = Request.QueryString["ProjectDashEditId"].ToString() != " " ? Convert.ToInt32(Request.QueryString["ProjectDashEditId"]) : 0;
            projectId = Request.QueryString["projectID"] != null ? Convert.ToInt32(Request.QueryString["projectID"]) : 0;
            projectName = Request.QueryString["projectName"] != null ? Request.QueryString["projectName"] : "";

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
                    Response.Redirect("AccessDenied.aspx");
                }
                else if (Convert.ToInt16(Session["UserRoleID"]) == 1) // Checking role of user by UserRoleID ( 1 = ProjectOwner , 2 = ProjectManager )
                {
                    HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                    if ( hcliuser != null )
                        (Page.Master.FindControl("liuser") as HtmlControl).Visible = true;

                    HtmlControl hcauser = Page.Master.FindControl("auser") as HtmlControl;
                    if ( hcauser != null )
                        (Page.Master.FindControl("auser") as HtmlControl).Attributes.Add("class", "");
                }
                else
                {
                    HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                    if (hcliuser != null)
                    (Page.Master.FindControl("liuser") as HtmlControl).Visible = false;
                }
            }           

            if ( !Page.IsPostBack )
            {             
                DataTable dtDataSession = (DataTable)Session["dtDatas1"];
                if ( dtDataSession!=null && dtDataSession.Rows.Count > 0 )
                {
                    Session["dayfrom"] = dtDataSession.Rows[0].ItemArray[0];
                    Session["dayto"] = dtDataSession.Rows[0].ItemArray[1];
                    Session["dashboardID"]= Convert.ToInt64(dtDataSession.Rows[0].ItemArray[2]);
                    Session["status"] = dtDataSession.Rows[0].ItemArray[3];
                    Session["ProjectName"]=projectName!=null?projectName:"";
                    lblheading.Text = "Project:" + ' ' + Session["ProjectName"];
                    Session["dayfrom"] = Convert.ToDateTime(Session["dayfrom"]).ToString("dd/MM/yyyy");
                    Session["dayto"] = Convert.ToDateTime(Session["dayto"]).ToString("dd/MM/yyyy");
                    lblDates.Text = "From:" + ' ' + Session["dayfrom"].ToString()+ ' ' + '-' + ' ' + "To:" + ' ' + Session["dayto"].ToString();  
                }

             if ( ProjectDashBoardEditId != 0 )
                 GetDashBoardDetails();  // Method to get dashboard entries...

             lblMsg.Text = string.Empty;            
            }

            GetFinaliseddashBoard(); // Method to find the Finalised DashBoard ...
        }

    #endregion

    #region "Control Events" 

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if ( ProjectDashBoardEditId != 0 )
            {
                UpdateDashBoardEntries();  // Method to update dashboard entries...
                ScriptManager.RegisterStartupScript(this, this.GetType(),"alert",
                "alert('Data saved successfully!!');window.location ='ProjectDashboard.aspx';",
                true);
            }
            else
            Response.Redirect("ProjectDashboard.aspx");
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            year = Convert.ToInt32(Session["Year"]);
            month = Convert.ToInt32(Session["Month"]);
            weekindex = Convert.ToInt16(Session["WeekIndex"]);

            DataTable dtColumns = new DataTable();
            dtColumns.Columns.Add("fromperiod");
            dtColumns.Columns.Add("toperiod");
            dtColumns.Columns.Add("dashboardid");
            dtColumns.Columns.Add("status");

            DataRow drRows = dtColumns.NewRow();
            drRows["fromperiod"] = Session["dayfrom"];
            drRows["toperiod"] = Session["dayto"];
            drRows["dashboardid"] = Session["dashboardID"];
            drRows["status"] = Session["status"];

            dtColumns.Rows.Add(drRows);

            Session["dtDatas1"] = dtColumns;
            dtColumns.Dispose();
            Response.Redirect("ProjectDashboard.aspx");
        }

    #endregion

    #region"Methods"  

        // Method to find the Finalised DashBoard ...
        private void GetFinaliseddashBoard()
        {
            int dashBoardDataEntryId = Convert.ToInt32(Request.QueryString["ProjectDashboardId"]);
            string status = string.Empty;
            DataTable dsDashBoard = PMOscar.BaseDAL.ExecuteDataTable("Select Status from DashBoard Where dashBoardId = '" + dashBoardDataEntryId + "' And Status = 'F'");

            if (dsDashBoard.Rows.Count > 0)
            {
                status = dsDashBoard.Rows[0].ItemArray[0].ToString();
                btnSave.Enabled = false;
            }
        }
        
        // Method to update dashboard entries...
        private void UpdateDashBoardEntries()
        {
            int clientStatus = RBCR.Checked == true ? 1 : (RBCY.Checked == true ? 2 : 3);
            int timeLineStatus = RBTR.Checked == true ? 1 : (RBTY.Checked == true ? 2 : 3);
            int budgetStatus = RBBR.Checked == true ? 1 : (RBBY.Checked == true ? 2 : 3);
            int escalateStatus = RBER.Checked == true ? 1 : (RBEY.Checked == true ? 2 : 3);

            Session["value"] = Request.Url.AbsoluteUri +"&budgetStatus=" + budgetStatus ; 

            parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProjectID", projectId));
            parameter.Add(new SqlParameter("@PhaseID", 1));
            parameter.Add(new SqlParameter("@ClientStatus", clientStatus));
            parameter.Add(new SqlParameter("@TimelineStatus", timeLineStatus));
            parameter.Add(new SqlParameter("@BudgetStatus", budgetStatus));
            parameter.Add(new SqlParameter("@EscalateStatus", escalateStatus));            
            parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
            parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
            parameter.Add(new SqlParameter("@Status", "U"));
            parameter.Add(new SqlParameter("@DashboardID", ProjectDashBoardEditId));
            parameter.Add(new SqlParameter("@ProjectName", ""));
            parameter.Add(new SqlParameter("@ShortName", ""));
            parameter.Add(new SqlParameter("@ProjectType", ""));
            parameter.Add(new SqlParameter("@ProjectOwner", 1));
            parameter.Add(new SqlParameter("@ProjectManager",1));
            parameter.Add(new SqlParameter("@DeliveryDate", DateTime.Now.Date));
            parameter.Add(new SqlParameter("@RevisedDeliveryDate", DateTime.Now.Date));
            parameter.Add(new SqlParameter("@PMComments", ""));
            parameter.Add(new SqlParameter("@DeliveryComments", ""));
            parameter.Add(new SqlParameter("@POComments", ""));         
            parameter.Add(new SqlParameter("@isActive", 1));
            parameter.Add(new SqlParameter("@Comments", Server.HtmlEncode(txtWeeklyComments.Text.Trim())));          
           
            // Return value as parameter
            SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;
            parameter.Add(returnValue);
            try
            {
                string query1 = string.Format("update [dbo].[ProjectActivityStatus] set ClientStatus={0}, TimelineStatus={1}, BudgetStatus={2}, EscalateStatus={3},Comments='{4}' where ProjectDashboardID={5}", clientStatus, timeLineStatus, budgetStatus, escalateStatus, Server.HtmlEncode(txtWeeklyComments.Text.Trim()), ProjectDashBoardEditId);
                int result1 = PMOscar.BaseDAL.ExecuteNonQuery(query1);
                ProjectDashBoardEditId = Convert.ToInt16(returnValue.Value);
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        // Method to get dashboard entries...
        private void GetDashBoardDetails()
        {
            var queryDashboard = BaseDAL.getProjectDashboard("ProjectDashboardID", ProjectDashBoardEditId);
            DataSet dsDashBoard = BaseDAL.ExecuteDataSet(queryDashboard);
            DataSet dsBudget = BaseDAL.ExecuteDataSet("SELECT ProjectID,sum(ISNULL(BudgetHours,0)) BudgetHours,sum(ISNULL(RevisedBudgetHours,0)) RevisedBudgetHours,sum(ISNULL(ActualHrs,0)) ActualHrs FROM	(SELECT dt1.ProjectID,dt1.PhaseID,sum(dt1.BudgetHours) BudgetHours,sum(dt1.RevisedBudgetHours) RevisedBudgetHours,sum(dt1.ActualHrs) ActualHrs FROM(SELECT  projest.EstimationRoleID,projest.PhaseID,pro.ProjectID ,avg(ISNULL(projest.BudgetHours,0)) BudgetHours,avg(ISNULL(projest.RevisedBudgetHours,0)) RevisedBudgetHours,sum(ISNULL(projest.ActualHrs,0)) ActualHrs FROM ProjectDashBoardEstimation projest right join project  pro  on  projest.ProjectID=pro.ProjectID GROUP BY projest.EstimationRoleID,projest.PhaseID,pro.ProjectID) AS dt1 GROUP BY dt1.PhaseID,dt1.ProjectID ) AS d1 GROUP BY d1.projectID");
            if (dsDashBoard.Tables[0].Rows.Count > 0)
            {
                DataSet dsDash = BaseDAL.ExecuteDataSet("SELECT  DashboardID,Name,FromDate,ToDate,PeriodType,Status From Dashboard where DashboardID=" + dsDashBoard.Tables[0].Rows[dsDashBoard.Tables[0].Rows.Count - 1].ItemArray[23] + "");
                if (dsDashBoard.Tables[0].Rows.Count > 0)
                {
                    if (dsDashBoard.Tables[0].Rows[0].ItemArray[3].ToString() == "3")
                    {
                        RBCG.Checked = true;
                        RBCY.Checked = false;
                        RBCR.Checked = false;
                    }
                    else if (dsDashBoard.Tables[0].Rows[0].ItemArray[3].ToString() == "1")
                    {
                        RBCG.Checked = false;
                        RBCY.Checked = false;
                        RBCR.Checked = true;
                    }
                    else
                    {
                        RBCG.Checked = false;
                        RBCY.Checked = true;
                        RBCR.Checked = false;
                    }

                    if (dsDashBoard.Tables[0].Rows[0].ItemArray[4].ToString() == "3")
                    {
                        RBTG.Checked = true;
                        RBTR.Checked = false;
                        RBTY.Checked = false;
                    }
                    else if (dsDashBoard.Tables[0].Rows[0].ItemArray[4].ToString() == "1")
                    {
                        RBTG.Checked = false;
                        RBTR.Checked = true;
                        RBTY.Checked = false;
                    }
                    else
                    {
                        RBTG.Checked = false;
                        RBTR.Checked = false;
                        RBTY.Checked = true;
                    }
                    //for (int i = 0; i < dsBudget.Tables[0].Rows.Count; i++)
                    //{
                    //if (dsDash.Tables[0].Rows[0].ItemArray[5].Equals("I"))
                    //{
                    //    if (dsDashBoard.Tables[0].Rows[0].ItemArray[1].ToString() == dsBudget.Tables[0].Rows[i].ItemArray[0].ToString())
                    //    {
                    //        if (Convert.ToInt32(dsBudget.Tables[0].Rows[i].ItemArray[3]) > Convert.ToInt32(dsBudget.Tables[0].Rows[i].ItemArray[1].ToString()))
                    //        {

                    //            RBBG.Checked = false;
                    //            RBBR.Checked = true;
                    //            RBBY.Checked = false;
                    //        }
                    //        else
                    //        {
                    //            if (dsDashBoard.Tables[0].Rows[0].ItemArray[5].ToString() == "3")
                    //            {
                    //                RBBG.Checked = true;
                    //                RBBR.Checked = false;
                    //                RBBY.Checked = false;
                    //            }
                    //            else if (dsDashBoard.Tables[0].Rows[0].ItemArray[5].ToString() == "1")
                    //            {
                    //                RBBG.Checked = false;
                    //                RBBR.Checked = true;
                    //                RBBY.Checked = false;
                    //            }
                    //            else if (dsDashBoard.Tables[0].Rows[0].ItemArray[5].ToString() == "2")
                    //            {
                    //                RBBG.Checked = false;
                    //                RBBR.Checked = false;
                    //                RBBY.Checked = true;
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    if (dsDashBoard.Tables[0].Rows[0].ItemArray[5].ToString() == "3")
                    {
                        RBBG.Checked = true;
                        RBBR.Checked = false;
                        RBBY.Checked = false;
                    }
                    else if (dsDashBoard.Tables[0].Rows[0].ItemArray[5].ToString() == "1")
                    {
                        RBBG.Checked = false;
                        RBBR.Checked = true;
                        RBBY.Checked = false;
                    }
                    else if (dsDashBoard.Tables[0].Rows[0].ItemArray[5].ToString() == "2")
                    {
                        RBBG.Checked = false;
                        RBBR.Checked = false;
                        RBBY.Checked = true;
                    }
                    // }

                    //} 
                    if (dsDashBoard.Tables[0].Rows[0].ItemArray[6].ToString() == "3")
                    {
                        RBEG.Checked = true;
                        RBER.Checked = false;
                        RBEY.Checked = false;
                    }
                    else if (dsDashBoard.Tables[0].Rows[0].ItemArray[6].ToString() == "1")
                    {
                        RBEG.Checked = false;
                        RBER.Checked = true;
                        RBEY.Checked = false;
                    }
                    else
                    {
                        RBEG.Checked = false;
                        RBER.Checked = false;
                        RBEY.Checked = true;
                    }

                    txtWeeklyComments.Text = Server.HtmlDecode(dsDashBoard.Tables[0].Rows[0]["Comments"].ToString());
                }
            }
        }
     
    #endregion  

    }
}
