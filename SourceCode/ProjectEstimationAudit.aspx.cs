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
    public partial class ProjectEstimationAudit : System.Web.UI.Page
    {
        #region"Declarations"
        public IList<SqlParameter> parameter;
        private int EstProjectId;
        #endregion
        #region"Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                EstProjectId = Convert.ToInt32(Session["EstAudProjectId"]);
                BindGridView();
            }

        }
        #endregion
        private void BindGridView()
        {
            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@ProjectID", EstProjectId);
            parameter[1] = new SqlParameter("@Year", 1);

            DataSet dsProjectDetails = BaseDAL.ExecuteSPDataSet("GetProjectEstimationAudit", parameter);
            gvProjectEstimationAudit.DataSource = dsProjectDetails.Tables[0] as DataTable;
            gvProjectEstimationAudit.DataBind();

        }


        }
    
}
