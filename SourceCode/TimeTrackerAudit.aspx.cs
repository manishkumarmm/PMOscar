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
    public partial class TimeTrackerAudit : System.Web.UI.Page
    {

        #region"Declarations"


        public IList<SqlParameter> parameter;
        private int ResourceId;
        private int ProjectID;
        private string dayfrom;
        private string dayto;
        private int Status;

        #endregion
        #region"Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                ProjectID = Convert.ToInt32(Session["ProjectIdTA"]);
                ResourceId = Convert.ToInt32(Session["ResourceIdTA"]);
                dayfrom = Session["dayfromTA"].ToString();
                dayto = Session["daytoTA"].ToString();
                Status = Convert.ToInt32(Session["Status"]);
            
                    BindGridView();

                 

                }
                      
                      



        }


        #endregion
        private void BindGridView()
        {
            SqlParameter[] parameter = new SqlParameter[6];
            parameter[0] = new SqlParameter("@ResourceID", ResourceId);
            parameter[1] = new SqlParameter("@dayfrom", dayfrom);
            parameter[2] = new SqlParameter("@dayto", dayto);
            parameter[3] = new SqlParameter("@Year", Session["Year"]);
            parameter[4] = new SqlParameter("@Status", Status);
            parameter[5] = new SqlParameter("@ProjectID", ProjectID);

            DataSet dsProjectDetails = DataContract.ExecuteSPDataSet("GetTimeTrackerAudit", parameter);
            gvTimeTrackerAudit.DataSource = dsProjectDetails.Tables[0] as DataTable;
            gvTimeTrackerAudit.DataBind();

        }

    }
}
