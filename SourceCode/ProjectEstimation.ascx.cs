using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;

namespace PMOscar
{

    public partial class ProjectEstimation : System.Web.UI.UserControl 
    {

        #region"Declarations"
        private int ProjectEditId = 0;
        private static int ProjectId = 0;      
        private DataTable dtEstimation = new DataTable();
        private long editid = 0;
        public IList<SqlParameter> parameter;
        #endregion

        #region"Page Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtBudHours.Attributes.Add("onkeypress", "return CheckNemericValue(event,this);");
                txtRevBudHours.Attributes.Add("onkeypress", "return CheckNemericValue(event,this);");
                txtBillableHrs.Attributes.Add("onkeypress", "return CheckNemericValue(event,this);");
                btnEstSave.Text = "Save";
                BindComboDetails();
                ClearEstimationDetails();
            }            
        }

        protected void btnEstCancel_Click(object sender, EventArgs e)
        {
            ClearEstimationDetails();
            ddlPhase.Focus();           
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BindEstimationDT(); //..Checking
            bool hasError = false;
            // EditFlag = 1;
            //editClickFlag = 1;
            bool isComment = true;
            // Session["EditFlag"] = EditFlag;
            // Session["EditClickFlag"] = editClickFlag;

            if (txtEstID.Text != "")
            {
                if (string.IsNullOrEmpty(txtComments.Text.ToString()))
                {
                    lblComments.Text = "Please Enter a Comment.";
                    isComment = false;
                    hasError = true;
                }
            }

            if (txtRevBudHours.Text == "" || txtRevBudHours.Text == "0")
            {
                txtRevBudHours.Text = txtBudHours.Text;
            }

            if (ddlPhase.SelectedItem.Text == "Select")
            {
                lblValPhase.Text = "Select Phase";
                hasError = true;
            }

            if (ddlRole.SelectedItem.Text == "Select")
            {
                lblValRole.Text = "Select Role";
                hasError = true;
            }

            if (hasError)
            {
                Page.RegisterStartupScript("myScript", "<script language='JavaScript'>AddEstimationPopUp();</script>");
                return;
            }

            if (ddlPhase.SelectedItem.Text != "Select" && ddlRole.SelectedItem.Text != "Select" && isComment)
            {
                dtEstimation = Session["dtEstimation"] != null ? (DataTable)Session["dtEstimation"] : dtEstimation;

                if (dtEstimation.Rows.Count > 0)
                {
                    DataRow[] dr11;

                    if (txtEstID.Text != "")
                    {
                        editid = Convert.ToInt64(txtEstID.Text);
                    }

                    if (editid != 0)
                    {
                        dr11 = dtEstimation.Select("PhaseID=" + ddlPhase.SelectedItem.Value + " And   RoleId=" + ddlRole.SelectedItem.Value + "  and ProjectEstimationID <>" + editid);
                    }
                    else
                    {
                        dr11 = dtEstimation.Select("PhaseID=" + ddlPhase.SelectedItem.Value + " And   RoleId=" + ddlRole.SelectedItem.Value);
                    }

                    lblesterror.Text = "";

                    if (dr11.Length > 0)
                    {
                        lblesterror.Text = "Same phase and role cannot be repeatedly added for the same project";
                        TextBox1.Focus();
                        return;
                    }

                    insertEstimationDetails();                  
                    ClearEstimationDetails();
                }

                else
                {
                    insertEstimationDetails();
                    ClearEstimationDetails();
                }
            }

            TextBox1.Focus();
            ddlPhase.Focus();
            
            Response.Redirect(Request.RawUrl);

        }

        #endregion

        private void ClearEstimationDetails()
        {
            ddlPhase.SelectedIndex = 0;
            ddlRole.SelectedIndex = 0;
            lblValPhase.Text = "";
            lblValRole.Text = "";
            txtBudHours.Text = "";
            txtBillableHrs.Text = "";
            txtBudHours.Text = "";
            txtRevBudHours.Text = "";
            txtEstID.Text = "";
            lblComments.Text = "";
            txtComments.Text = "";
        }

        private void BindEstimationDT()
        {
            //totalActualHoursOriginal = 0;
            //totalActualHrs = 0;
            //totalBillable = 0;
            //totalBudgeted = 0;
            //totalRevisedBudgeted = 0;

            if (ProjectEditId == 0)
            {
                if (Request.QueryString["ProjectEditId"] != null)
                {
                    ProjectEditId = Convert.ToInt32(Request.QueryString["ProjectEditId"]);
                }
                else
                {
                    ProjectEditId = Convert.ToInt16(Session["ProjectEditId"]);//CHK123
                    // ProjectEditId = Convert.ToInt16(ViewState["ProjectEditId"]);//CHK124
                }
            }

            string Query = "Select A.ProjectID,A.PhaseID,B.Phase,A.BillableHours,A.BudgetHours,A.RevisedBudgetHours,RowNumber = ROW_NUMBER(" +
            " ) OVER 	(ORDER BY A.ProjectEstimationID ASC),C.RoleName as Role,A.EstimationRoleID as RoleID,round(isnull(T.ActualHours,0),0)ActualHours1," +
            " round(isnull(D.ActualHours,0),0)ActualHours," +
            " A.ProjectEstimationID" +
            " from 	dbo.ProjectEstimation A inner join Phase B on A.PhaseID=B.PhaseID" +
            " inner join EstimationRole C on  C.EstimationRoleID=A.EstimationRoleID 	left join (   SELECT  CASE P.ProjectType" +
            " WHEN  'F'  THEN round(isnull(sum(A.ActualHours* B.EstimationPercentage),0),0) ELSE round(isnull(sum(A.ActualHours),0),0)" +
            " END ActualHours,A.PhaseID,A.ProjectId,b.EstimationRoleID from TimeTracker A inner join Role B on A.RoleID=B.RoleID" +
            " inner join Project P on P.ProjectId = A.ProjectId  group by b.EstimationRoleID,A.phaseID,A.ProjectId, P.ProjectType" +
            ") as D on   A.PhaseID=D.PhaseID and A.ProjectId=D.ProjectId and D.EstimationRoleID=A.EstimationRoleID " +
            " left join (   SELECT   round(isnull(sum(A.ActualHours),0),0)ActualHours,A.PhaseID,A.ProjectId,b.EstimationRoleID" +
            " from TimeTracker A inner join Role B on A.RoleID=B.RoleID" +
            " inner join Project P on P.ProjectId = A.ProjectId  group by b.EstimationRoleID,A.phaseID,A.ProjectId, P.ProjectType" +
            " ) as T on   A.PhaseID=T.PhaseID and A.ProjectId=T.ProjectId and T.EstimationRoleID=A.EstimationRoleID" +
            " where A.ProjectID= " + ProjectEditId + " order by A.PhaseID";  // To Incluse VAS and Proposal

            dtEstimation = PMOscar.BaseDAL.ExecuteDataTable(Query);
            Session["dtEstimation"] = dtEstimation;
        }

        private void insertEstimationDetails()
        {
            if (ProjectEditId == 0)
            {
                ProjectEditId = Convert.ToInt16(Session["ProjectEditId"]);
            }

            if (txtBillableHrs.Text == "")
                txtBillableHrs.Text = "0";
            if (txtBudHours.Text == "")
                txtBudHours.Text = "0";
            if (txtRevBudHours.Text == "")
                txtRevBudHours.Text = "0";

            if (editid != 0)
            {
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ProjectId", ProjectEditId));
                parameter.Add(new SqlParameter("@PhaseID", Convert.ToInt64(ddlPhase.SelectedItem.Value)));
                parameter.Add(new SqlParameter("@RoleID", Convert.ToInt64(ddlRole.SelectedItem.Value)));
                parameter.Add(new SqlParameter("@BillableHours", Convert.ToInt64(txtBillableHrs.Text)));
                parameter.Add(new SqlParameter("@BudgetHours", Convert.ToInt64(txtBudHours.Text)));
                parameter.Add(new SqlParameter("@RevisedBudgetHours ", Convert.ToInt64(txtRevBudHours.Text)));
                parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@status", "U"));
                parameter.Add(new SqlParameter("@ProjectEstimationID", editid));
                parameter.Add(new SqlParameter("@Comments", txtComments.Text.Trim()));
                try
                {
                    int ProjectId = PMOscar.BaseDAL.ExecuteSPScalar("[ProjectEstimationOperations]", parameter);
                }
                catch
                {
                }
            }
            else
            {
                parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@ProjectId", ProjectEditId));
                parameter.Add(new SqlParameter("@PhaseID", Convert.ToInt64(ddlPhase.SelectedItem.Value)));
                parameter.Add(new SqlParameter("@RoleID", Convert.ToInt64(ddlRole.SelectedItem.Value)));
                parameter.Add(new SqlParameter("@BillableHours", Convert.ToInt64(txtBillableHrs.Text == "" ? "0" : txtBillableHrs.Text)));
                parameter.Add(new SqlParameter("@BudgetHours", Convert.ToInt64(txtBudHours.Text == "" ? "0" : txtBudHours.Text)));
                parameter.Add(new SqlParameter("@RevisedBudgetHours ", Convert.ToInt64(txtRevBudHours.Text == "" ? "0" : txtRevBudHours.Text)));
                parameter.Add(new SqlParameter("@CreatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@UpdatedBy", Session["UserID"]));
                parameter.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
                parameter.Add(new SqlParameter("@status", "I"));
                parameter.Add(new SqlParameter("@ProjectEstimationID", 1));
                if (!string.IsNullOrEmpty(txtComments.Text.ToString()))
                {
                    parameter.Add(new SqlParameter("@Comments", txtComments.Text.Trim()));
                }
                try
                {
                    int ProjectId = PMOscar.BaseDAL.ExecuteSPScalar("[ProjectEstimationOperations]", parameter);
                }
                catch
                {
                }
            }
        }

        private void BindComboDetails()
        {
            parameter = new List<SqlParameter>();
            DataTable dt = BaseDAL.ExecuteSPPhaseDataTable("GetPhase", parameter);
            ddlPhase.DataSource = dt;
            ddlPhase.DataTextField = "Phase";
            ddlPhase.DataValueField = "PhaseID";
            ddlPhase.DataBind();
            ddlPhase.Items.Insert(0, new ListItem("Select", "0"));           

            dt = PMOscar.BaseDAL.ExecuteDataTable("Select EstimationRoleID,RoleName from EstimationRole ");
            ddlRole.DataSource = dt;
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "EstimationRoleID";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("Select", "0"));
            dt.Dispose();
        }
    }
}
