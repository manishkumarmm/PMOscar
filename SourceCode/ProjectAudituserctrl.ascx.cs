

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
    public partial class ProjectAudituserctrl : System.Web.UI.UserControl
    {

        #region"Declarations"


        public IList<SqlParameter> parameter;
        private int EstAudProjectId;

        #endregion
        #region"Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
          

        }
        #endregion
        public void BindGridView()

        {
            
            EstAudProjectId = Convert.ToInt32(Session["AudProjectId"]);
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@ProjectId", EstAudProjectId);
            parameter[1] = new SqlParameter("@Year", 1);
            DataTable dt = PMOscar.BaseDAL.ExecuteDataTable("SELECT DeliveryDate,RevisedDeliveryDate FROM Project where ProjectId =" + EstAudProjectId + " ");
            DataSet dsProjectDetails = BaseDAL.ExecuteSPDataSet("GetProjectAudit", parameter);
            gvProjectAudit.DataSource = dsProjectDetails.Tables[0] as DataTable;
            gvProjectAudit.DataBind();

        }
        protected void gvProjectAudit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[10].ToolTip = e.Row.Cells[10].Text.Trim();
            e.Row.Cells[10].Text = e.Row.Cells[10].Text.ToString().Length > 49 ? e.Row.Cells[10].Text.Substring(0, 50) + "..." : e.Row.Cells[10].Text == "&nbsp;" ? "" : e.Row.Cells[10].Text;

            e.Row.Cells[11].ToolTip = e.Row.Cells[11].Text.Trim();
            e.Row.Cells[11].Text = e.Row.Cells[11].Text.ToString().Length > 49 ? e.Row.Cells[11].Text.Substring(0, 50) + "..." : e.Row.Cells[11].Text == "&nbsp;" ? "" : e.Row.Cells[11].Text;

        }

    }
}
