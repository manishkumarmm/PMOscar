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
    public partial class ActualHoursuctrl : System.Web.UI.UserControl
    {

        #region"Declarations"


        public IList<SqlParameter> parameter;

        private int ActProjectId;
        private int ActPhaseId;
        private int ActEstRoleId;


        #endregion
        #region"Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
          
            //if (!Page.IsPostBack)
            //{
                //ActProjectId = Convert.ToInt32(Session["ActProjectId"]);
                //ActPhaseId = Convert.ToInt32(Session["ActPhaseId"]);
                //ActEstRoleId = Convert.ToInt32(Session["ActEstRoleId"]);

                //BindGridView();
            //}

        }
        #endregion
        public void BindGridView()
        {
            ActProjectId = Convert.ToInt32(Session["ActProjectId"]);
            ActPhaseId = Convert.ToInt32(Session["ActPhaseId"]);
            ActEstRoleId = Convert.ToInt32(Session["ActEstRoleId"]);

            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@ProjectId", ActProjectId);
            parameter[1] = new SqlParameter("@PhaseID", ActPhaseId);
            parameter[2] = new SqlParameter("@EstimationRoleID", ActEstRoleId);
            

            DataSet dsProjectDetails = BaseDAL.ExecuteSPDataSet("GetActualHoursHistory", parameter);
            gvProjectAudit.DataSource = dsProjectDetails.Tables[0] as DataTable;
            gvProjectAudit.DataBind();

            //Session["ActProjectId"] = 0;
            //Session["ActPhaseId"] = 0;
            //Session["ActEstRoleId"] = 0;

        }

    }
}
