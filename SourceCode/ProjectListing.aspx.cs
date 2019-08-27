
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectListing.cs" company="Naico IT Services Pvt. Ltd">
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
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using PMOscar.Core;


namespace PMOscar
{
    public partial class ProjectListing : System.Web.UI.Page
    {
       
        #region "Declarations"

        private string chkvalueid = "0";
        public IList<SqlParameter> parameter;
        public IList<SqlParameter> parameter1;
        public int projectDeleteId;
            private double totalBillable = 0;
            private double totalBudgeted = 0;
            private double totalRevisedBudgeted = 0;
            private double totalActual = 0; 

        #endregion

        #region"Page Events"

            protected void Page_Load(object sender, EventArgs e)
            {
                Session[chkvalueid] = "0";

                btnAdd.Focus();
                lbMessage.Text = string.Empty;
                Session["ProjectEditId"] = null;
                Session["EditFlag"] = 0;
               
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
                    else if (Convert.ToInt16(Session["UserRoleID"]) == 1)
                    {
                        HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                        if (hcliuser != null)
                            (Page.Master.FindControl("liuser") as HtmlControl).Visible = true;

                        HtmlControl hcauser = Page.Master.FindControl("auser") as HtmlControl;
                        if ( hcauser != null)
                            (Page.Master.FindControl("auser") as HtmlControl).Attributes.Add("class", "");
                    }

                else if (Convert.ToInt16(Session["UserRoleID"]) == 5)
                {
                    Response.Redirect("AccessDenied.aspx");
                }

                else
                    {
                        HtmlControl hcliuser = Page.Master.FindControl("liuser") as HtmlControl;
                        if (hcliuser != null)
                            (Page.Master.FindControl("liuser") as HtmlControl).Visible = false;
                    }
                }
                           
                Session["dtEstimation"] = null;
                if ( !Page.IsPostBack )
                {                 
                   ddlStatus.SelectedIndex = Convert.ToInt32(Session["activestatus"]);
                   DisplayProjectDetails(); // Method to display project details...               
                    projectDeleteId = Convert.ToInt32(Request.QueryString["ProjectDeleteId"]);
                                   
                    if ( projectDeleteId != 0 )
                        DeleteProjectDetails(projectDeleteId); // Method to delete ProjectDetails...
                }
           }

        #endregion

        #region"Methods"
   
        // Method to delete ProjectDetails...
        private void DeleteProjectDetails(int projectDeleteId)
            {
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ProjectId", projectDeleteId));
                parameter.Add(new SqlParameter("@ProjectCode", 1));
                parameter.Add(new SqlParameter("@ProjectName", ""));
                parameter.Add(new SqlParameter("@ShortName", ""));
                parameter.Add(new SqlParameter("@PhaseID", 1));
                parameter.Add(new SqlParameter("@ProjectType", ""));
                parameter.Add(new SqlParameter("@ProjectOwner", 1));
                parameter.Add(new SqlParameter("@ProjectManager", 1));
                parameter.Add(new SqlParameter("@ProjectManagerName", ""));
                parameter.Add(new SqlParameter("@DeliveryDateText", DateTime.Now));
                parameter.Add(new SqlParameter("@RevisedDeliveryDateText", DateTime.Now));
                parameter.Add(new SqlParameter("@ApprvChangeRequest", 1));
                parameter.Add(new SqlParameter("@PMComments", ""));
                parameter.Add(new SqlParameter("@POComments", ""));
                parameter.Add(new SqlParameter("@DeliveryComments", ""));
                parameter.Add(new SqlParameter("@isActive", 1));
                parameter.Add(new SqlParameter("@UpdatedBy", 1));
                parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@CreatedBy", 1));
                parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@OpMode", "DELETE"));
                parameter.Add(new SqlParameter("@status", "Insert"));
                parameter.Add(new SqlParameter("@ProjStartDateText", DateTime.Now));
                parameter.Add(new SqlParameter("@MaintClosingDateText", DateTime.Now));
                parameter.Add(new SqlParameter("@devurl", ""));
                parameter.Add(new SqlParameter("@qaurl", ""));
                parameter.Add(new SqlParameter("@demourl", ""));
                parameter.Add(new SqlParameter("@productionurl", ""));
                parameter.Add(new SqlParameter("@ClientId", 1));
                parameter.Add(new SqlParameter("@ProgramId", 1));
                parameter.Add(new SqlParameter("@Utilization", ""));
                parameter.Add(new SqlParameter("@CostCentreID",1));
                parameter.Add(new SqlParameter("@ShowInDashboard", 1));
                parameter.Add(new SqlParameter("@SVNPath", ""));
                parameter.Add(new SqlParameter("@ProjectDetails", ""));
                parameter.Add(new SqlParameter("@WorkOrderID", 1));
                

                parameter[21].Direction = ParameterDirection.InputOutput;

                try
                {
                    int projectId = PMOscar.BaseDAL.ExecuteSPScalar("ProjectOperations", parameter);
                }
                catch(Exception ex)
                {

                }

            try
            {
                string projname = string.Empty;
                int phaseid = 0;
                int isactive = 0;
                DateTime ProjStartDate = DateTime.Now;
                DateTime MaintClosingDate = DateTime.Now;
                
                string Query = string.Format("select ProjectName,phaseid,isactive,ProjStartDate,MaintClosingDate from Project where projectid ='{0}'", projectDeleteId);
                DataTable project = PMOscar.BaseDAL.ExecuteDataTable(Query);
                foreach (DataRow row in project.Rows)
                {
                    projname = row["ProjectName"].ToString();
                    phaseid = Convert.ToInt16(row["phaseid"]);
                    isactive = Convert.ToInt16(row["isactive"]);
                    ProjStartDate = Convert.ToDateTime(row["ProjStartDate"]);
                    MaintClosingDate = Convert.ToDateTime(row["MaintClosingDate"]);
                }
                
                parameter1 = new List<SqlParameter>();
                parameter1.Add(new SqlParameter("@ProjectName", projname));
                parameter1.Add(new SqlParameter("@ProjectId", projectDeleteId));
                parameter1.Add(new SqlParameter("@StartDate", ProjStartDate));
                parameter1.Add(new SqlParameter("@EndDate", MaintClosingDate));
                parameter1.Add(new SqlParameter("@IsUpdate", 1));
                parameter1.Add(new SqlParameter("@isActive", isactive));
                parameter1.Add(new SqlParameter("@phaseId", phaseid));

                int ProjectId = PMOscar.BaseDAL.ExecuteSPScalar("CreateProjectInTimeTracker", parameter1);
            }
            catch (Exception ex)
            {

            }


            DisplayProjectDetails(); // Method to display project details...
                lbMessage.Text = PMOscar.Core.Constants.AddRole.STATUS;
            }

            // Method to display project details...
            protected void DisplayProjectDetails()
            {
               
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ProjectId", projectDeleteId));

                if (ddlStatus.SelectedItem.Text == "Active")
                {
                    parameter.Add(new SqlParameter("@ProjectCode", 1));
                    parameter.Add(new SqlParameter("@ProjectManagerName", ""));
                }

                else if (ddlStatus.SelectedItem.Text == "Inactive")
                {
                    parameter.Add(new SqlParameter("@ProjectCode", 2));
                    parameter.Add(new SqlParameter("@ProjectManagerName",""));
                    
                }
                //Option to display only the projects of current user
                else if (ddlStatus.SelectedItem.Text == "My Projects")
                {
                    var username = Session["UserName"] as string;
                    DataTable dtprojectmanager = PMOscar.BaseDAL.ExecuteDataTable(" select FirstName from [dbo].[User] where UserName='" + username + "'");
                    string projectManager = dtprojectmanager.Rows[0]["FirstName"].ToString();
                    parameter.Add(new SqlParameter("@ProjectCode", 3));
                    parameter.Add(new SqlParameter("@ProjectManagerName", projectManager));
                }

                else
                {
                    parameter.Add(new SqlParameter("@ProjectCode", 4));
                    parameter.Add(new SqlParameter("@ProjectManagerName", ""));
                }
                
                parameter.Add(new SqlParameter("@ProjectName", ""));
                parameter.Add(new SqlParameter("@ShortName", ""));
                parameter.Add(new SqlParameter("@PhaseID", 1));
                parameter.Add(new SqlParameter("@ProjectType", ""));
                parameter.Add(new SqlParameter("@ProjectOwner", 1));
                parameter.Add(new SqlParameter("@ProjectManager", 1));
                parameter.Add(new SqlParameter("@DeliveryDateText", DateTime.Now));
                parameter.Add(new SqlParameter("@RevisedDeliveryDateText", DateTime.Now));
                parameter.Add(new SqlParameter("@ApprvChangeRequest", 1));
                parameter.Add(new SqlParameter("@PMComments", ""));
                parameter.Add(new SqlParameter("@POComments",""));
                parameter.Add(new SqlParameter("@DeliveryComments", ""));
                parameter.Add(new SqlParameter("@isActive", 1));
                parameter.Add(new SqlParameter("@UpdatedBy", 1));
                parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@CreatedBy", 1));
                parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@OpMode", "SELECT"));
                parameter.Add(new SqlParameter("@status", "Insert"));
                parameter.Add(new SqlParameter("@ProjStartDateText", DateTime.Now));
                parameter.Add(new SqlParameter("@MaintClosingDateText", DateTime.Now));
                parameter.Add(new SqlParameter("@devurl", ""));
                parameter.Add(new SqlParameter("@qaurl", ""));
                parameter.Add(new SqlParameter("@demourl", ""));
                parameter.Add(new SqlParameter("@productionurl", ""));
                parameter.Add(new SqlParameter("@ClientId", 1));
                parameter.Add(new SqlParameter("@ProgramId", 1));
                parameter.Add(new SqlParameter("@Utilization", ""));
                parameter.Add(new SqlParameter("@CostCentreID", ""));
                parameter.Add(new SqlParameter("@ShowInDashboard", 1));
                parameter.Add(new SqlParameter("@SVNPath", string.Empty));
                parameter.Add(new SqlParameter("@ProjectDetails", string.Empty));
                parameter.Add(new SqlParameter("@WorkOrderID", 1));
                parameter[21].Direction = ParameterDirection.InputOutput;
                DataSet dsProjectDetails = PMOscar.BaseDAL.ExecuteSPDataSet("ProjectOperations", parameter);

            //  if (dsProjectDetails.Tables[0].Rows.Count > 0)
            //{
            dgProjectDetails.DataSource = dsProjectDetails.Tables[0];
                    Session["Grview"] = dsProjectDetails.Tables[0];
                    dgProjectDetails.DataBind();
                    dgProjectDetails.Columns[1].Visible = false;
              // }

                for (int projectsCount = 0; projectsCount < dsProjectDetails.Tables[0].Rows.Count; projectsCount++)
                {
                    dgProjectDetails.Rows[projectsCount].Cells[0].Text = (projectsCount + 1).ToString();
                }

                dsProjectDetails.Dispose();
            }

            // Method to sort projectdetails...
            private string GetSortDirection(string column)
            {
                // By default, set the sort direction to ascending.
                string sortDirection = "ASC";

                // Retrieve the last column that was sorted.
                string sortExpression = ViewState["SortExpression"] as string;

                if (sortExpression != null)
                {
                    // Check if the same column is being sorted.
                    // Otherwise, the default value can be returned.
                    if (sortExpression == column)
                    {
                        string lastDirection = ViewState["SortDirection"] as string;
                        if ((lastDirection != null) && (lastDirection == "ASC"))
                        {
                            sortDirection = "DESC";
                        }
                    }
                }

                // Save new values in ViewState.
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = column;

                return sortDirection;
            }

        #endregion 

        #region "ControlEvents"

            protected void btnAdd_Click(object sender, System.EventArgs e)
            {
                Session["ProjectEditId"] = null;
                Session["dtEstimation"] = null;
                Session["AudProjectId"] = null;
                Response.Redirect("Project.aspx?mode=Add");
            }

            protected void Grview_RowDataBound(object sender, GridViewRowEventArgs e)
            {
               
                if ( e.Row.RowType == DataControlRowType.DataRow )
                {
                    e.Row.Cells[10].Width = 60;
                    e.Row.Cells[11].Width = 60;
                    e.Row.Cells[12].Width = 60;
                    e.Row.Cells[13].Width = 60;

                    string[] words = e.Row.Cells[12].Text.Split('.');
                    e.Row.Cells[12].Text = words[0];
                string value = e.Row.Cells[14].Text;
                if (value == Constants.Active)
                {
                    ((LinkButton)e.Row.FindControl("aProjectDelete")).Text = "Deactivate";

                }
                else if (value == Constants.InActive)
                {
                    ((LinkButton)e.Row.FindControl("aProjectDelete")).Text = "Activate";

                }

            }

                if ( e.Row.RowType == DataControlRowType.Header ) // Applying color to the header cells...
                {
                    e.Row.Cells[10].Width = 60;
                    e.Row.Cells[11].Width = 60;
                    e.Row.Cells[12].Width = 60;
                    e.Row.Cells[13].Width = 60;

                    e.Row.Cells[1].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[10].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[12].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[13].ForeColor = System.Drawing.Color.FromName("#fcff00");
                    e.Row.Cells[14].ForeColor = System.Drawing.Color.FromName("#fcff00");
                }

                if ( e.Row.RowType == DataControlRowType.DataRow )
                {
                    if ( e.Row.DataItem != null )
                    {
                        if ( e.Row.Cells[9].Text == "&nbsp;" )
                            e.Row.Cells[9].Text = "";
                        if ( e.Row.Cells[10].Text == "&nbsp;" )
                            e.Row.Cells[10].Text = "";
                        if ( e.Row.Cells[11].Text == "&nbsp;" )
                            e.Row.Cells[11].Text = "";
                        if (e.Row.Cells[12].Text == "&nbsp;")
                            e.Row.Cells[12].Text = "";
                        if (e.Row.Cells[13].Text == "&nbsp;")
                            e.Row.Cells[13].Text = "";

                        totalBillable += Convert.ToDouble(e.Row.Cells[10].Text.ToString() == "" ? "0" : e.Row.Cells[10].Text.ToString());
                        totalBudgeted += Convert.ToDouble(e.Row.Cells[11].Text.ToString() == "" ? "0" : e.Row.Cells[11].Text.ToString());
                        totalRevisedBudgeted += Convert.ToDouble(e.Row.Cells[12].Text.ToString() == "" ? "0" : e.Row.Cells[12].Text.ToString());
                        totalActual += Convert.ToDouble(e.Row.Cells[13].Text.ToString() == "" ? "0" : e.Row.Cells[13].Text.ToString());
                    }
                }

                if (e.Row.RowType == DataControlRowType.Footer)    // Applying blank spaces if the values are zero
                {
                    e.Row.Cells[10].Width = 60;
                    e.Row.Cells[11].Width = 60;
                    e.Row.Cells[12].Width = 60;
                    e.Row.Cells[13].Width = 60;

                    if (totalBillable == 0)
                        e.Row.Cells[10].Text = "";
                    else
                        e.Row.Cells[10].Text = totalBillable.ToString();

                    if (totalBudgeted == 0)
                        e.Row.Cells[11].Text = "";
                    else
                        e.Row.Cells[11].Text = totalBudgeted.ToString();

                    if (totalRevisedBudgeted == 0)
                        e.Row.Cells[12].Text = "";
                    else
                        e.Row.Cells[12].Text = totalRevisedBudgeted.ToString();

                    if (totalActual == 0)
                        e.Row.Cells[13].Text = "";
                    else
                        e.Row.Cells[13].Text = totalActual.ToString();

                    e.Row.Cells[2].Text = "Total";
                    e.Row.Cells[2].Attributes.Add("style", "text-align: center;");
                }
            }          

            protected void Grview_Sorting(object sender, GridViewSortEventArgs e)
            {
                DataTable dtSortProjectDetails = (DataTable)Session["Grview"];

                if (dtSortProjectDetails != null)
                {
                    dtSortProjectDetails.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                    dgProjectDetails.DataSource = dtSortProjectDetails;
                    dgProjectDetails.DataBind();
                }
            }

            protected void Grview_RowCreated(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)            
                    e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();            
            }    

            protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
            {
                Session["activestatus"] = ddlStatus.SelectedIndex.ToString();
                DisplayProjectDetails();  // Method to display project details...
            }


            public void OnConfirm(object sender, EventArgs e)
            {
                int ProjectDeleteId = 0;
                string confirmvalue = Request.Form["confirm_value"];
                string projectId = Request.Form["projectId"];
                if (projectId != null)
                    ProjectDeleteId = Convert.ToInt16(projectId);

                if (confirmvalue == "Yes")
                {
                    DeleteProjectDetails(ProjectDeleteId);
                }
                else
                {
                    DataTable dtSortProjectDetails = (DataTable)Session["Grview"];
                    if (dtSortProjectDetails != null)
                    {
                        dgProjectDetails.DataSource = dtSortProjectDetails;
                        dgProjectDetails.DataBind();
                    }
                }
            }

        #endregion
    }
}
