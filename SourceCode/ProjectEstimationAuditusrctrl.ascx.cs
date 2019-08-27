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
    public partial class ProjectEstimationAuditusrctrl : System.Web.UI.UserControl
    {
        #region"Declarations"
        public IList<SqlParameter> parameter;
        private int EstProjectId;
        #endregion
        #region"Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (!Page.IsPostBack)
            //{

            //    EstProjectId = Convert.ToInt32(Session["EstAudProjectId"]);
            //    BindGridView();
            //}

        }
        #endregion
        public void BindGridView()
        {
            EstProjectId = Convert.ToInt32(Session["EstAudProjectId"]);
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@ProjectID", EstProjectId);
            parameter[1] = new SqlParameter("@Year", 1);
            DataSet dsProjectDetails = BaseDAL.ExecuteSPDataSet("GetProjectEstimationAudit", parameter);

            DataTable dt = AddToBugetRevisionDataTable(dsProjectDetails.Tables[0]);
            gvProjectEstimationAudit.DataSource = dt; //gvProjectEstimationAudit.DataSource = dsProjectDetails.Tables[0] as DataTable;
            gvProjectEstimationAudit.DataBind();
        }

        protected void gvProjectEstimationAudit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Width = new Unit("80px");
                e.Row.Cells[1].Width = new Unit("80px");
                e.Row.Cells[2].Width = new Unit("70px");
                e.Row.Cells[3].Width = new Unit("100px");
                e.Row.Cells[4].Width = new Unit("110px");
                e.Row.Cells[5].Width = new Unit("80px");
                e.Row.Cells[6].Width = new Unit("80px");
                e.Row.Cells[7].Width = new Unit("130px");
            }
        }

        private DataTable AddToBugetRevisionDataTable(DataTable dt)
        {
            DataTable newDataTable = new DataTable();
            newDataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            newDataTable.Columns.Add(new DataColumn("Modified Date", typeof(string)));
            newDataTable.Columns.Add(new DataColumn("Phase", typeof(string)));
            newDataTable.Columns.Add(new DataColumn("Role", typeof(string)));
            newDataTable.Columns.Add(new DataColumn("Type", typeof(string)));
            newDataTable.Columns.Add(new DataColumn("New Value", typeof(string)));
            newDataTable.Columns.Add(new DataColumn("Old Value", typeof(string)));
            newDataTable.Columns.Add(new DataColumn("Comments", typeof(string)));

            foreach (DataRow newrow in dt.Rows)
            {
                DataRow row;

                if (Convert.ToInt32(newrow["New Billable Hours"]) != Convert.ToInt32(newrow["Old Billable Hours"]))
                {
                    row = newDataTable.NewRow();
                    row["Name"] = newrow["Name"].ToString();
                    row["Modified Date"] = newrow["Modified Date"].ToString();
                    row["Phase"] = newrow["Phase"].ToString();
                    row["Role"] = newrow["Role"].ToString();
                    row["Type"] = "Billable Hours";
                    row["New Value"] = newrow["New Billable Hours"].ToString();
                    row["Old Value"] = newrow["Old Billable Hours"].ToString();
                    row["Comments"] = newrow["Comments"].ToString();
                    newDataTable.Rows.Add(row);
                }

                if (Convert.ToInt32(newrow["New Budget Hours"]) != Convert.ToInt32(newrow["Old Budget Hours"]))
                {
                    row = newDataTable.NewRow();
                    row["Name"] = newrow["Name"].ToString();
                    row["Modified Date"] = newrow["Modified Date"].ToString();
                    row["Phase"] = newrow["Phase"].ToString();
                    row["Role"] = newrow["Role"].ToString();
                    row["Type"] = "Budget Hours";
                    row["New Value"] = newrow["New Budget Hours"].ToString();
                    row["Old Value"] = newrow["Old Budget Hours"].ToString();
                    row["Comments"] = newrow["Comments"].ToString();
                    newDataTable.Rows.Add(row);
                }

                if (Convert.ToInt32(newrow["New Revised Budget Hours"]) != Convert.ToInt32(newrow["Old Revised Budget Hours"]))
                {
                    row = newDataTable.NewRow();
                    row["Name"] = newrow["Name"].ToString();
                    row["Modified Date"] = newrow["Modified Date"].ToString();
                    row["Phase"] = newrow["Phase"].ToString();
                    row["Role"] = newrow["Role"].ToString();
                    row["Type"] = "Revised Budget Hours";
                    row["New Value"] = newrow["New Revised Budget Hours"].ToString();
                    row["Old Value"] = newrow["Old Revised Budget Hours"].ToString();
                    row["Comments"] = newrow["Comments"].ToString();
                    newDataTable.Rows.Add(row);
                }
            }

            return newDataTable;
        }
    }    
}
