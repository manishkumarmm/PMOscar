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
    public partial class ProjectAudit : System.Web.UI.Page
    {

        #region"Declarations"


        public IList<SqlParameter> parameter;
        private int EstAudProjectId;

        #endregion
        #region"Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EstAudProjectId = Convert.ToInt32(Session["AudProjectId"]);
                BindGridView();
            }

        }
        #endregion
        private void BindGridView()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@ProjectId", EstAudProjectId);
            parameter[1] = new SqlParameter("@Year", 1);

            DataSet dsProjectDetails = BaseDAL.ExecuteSPDataSet("GetProjectAudit", parameter);
            gvProjectAudit.DataSource = dsProjectDetails.Tables[0] as DataTable;
            gvProjectAudit.DataBind();

        }

    }
}
