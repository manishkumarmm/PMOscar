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

    public partial class TimeTrackerAudit : System.Web.UI.UserControl
    {

        #region"Declarations"


        public IList<SqlParameter> parameter;
        private int ResourceId;
        private int ProjectID;
        private string dayfrom;
        private string dayto;
        private int Status;

        private string  _resourceID;
        private int _projectId;
        private int _year;
        private string _dayFrom;
        private string _dayTo;
        private int _Status;

        #endregion

        public string   SetResourceId
        {
            get { return _resourceID; }
            set {_resourceID = value ;}
        }
        public int SetYear
        {

            get { return _year; }
            set { _year = value; }
        }

        public string SetDayFrom
        {
            get { return _dayFrom; }
            set { _dayFrom = value; }
        }

        public string SetDayTo
        {
            get { return _dayTo; }
            set { _dayTo = value; }

        }
        public int SetStatus
        {
            get { return _Status; }
            set { _Status = value; }

        }
        public int SetProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
        }


        #region"Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //if (!Page.IsPostBack)
            //{
                ProjectID = Convert.ToInt32(Session["ProjectIdTA"]);
                ResourceId = Convert.ToInt32(Session["ResourceIdTA"]);
                dayfrom = Session["dayfromTA"].ToString();
                dayto = Session["daytoTA"].ToString();
                Status = Convert.ToInt32(Session["Status"]);
            
                    BindGridView();
                
          //  }

        }
        #endregion
        public  void BindGridView()
        {
            
            SqlParameter[] parameter = new SqlParameter[6];
            if (ResourceId == 0 && ProjectID ==0)
            {

                parameter[0] = new SqlParameter("@ResourceID", _resourceID == null ? "0" : _resourceID);
                parameter[1] = new SqlParameter("@dayfrom", _dayFrom == null ? "" : _dayFrom);
                parameter[2] = new SqlParameter("@dayto", _dayTo == null ? "" : _dayTo);
                parameter[3] = new SqlParameter("@Year", Session["Year"]);
                parameter[4] = new SqlParameter("@Status", _Status );
                parameter[5] = new SqlParameter("@ProjectID", _projectId );

            }
            else
            //int year = Convert.ToInt16(Session["Year"]);
            {
                parameter[0] = new SqlParameter("@ResourceID", ResourceId);
                parameter[1] = new SqlParameter("@dayfrom", dayfrom);
                parameter[2] = new SqlParameter("@dayto", dayto);
                parameter[3] = new SqlParameter("@Year", Session["Year"]);
                parameter[4] = new SqlParameter("@Status", Status);
                parameter[5] = new SqlParameter("@ProjectID", ProjectID);
            }

            DataSet dsProjectDetails = BaseDAL.ExecuteSPDataSet("GetTimeTrackerAudit", parameter);

            if (dsProjectDetails != null && dsProjectDetails.Tables.Count > 0)
            {
                gvTimeTrackerAudit.DataSource = dsProjectDetails.Tables[0] as DataTable;
                gvTimeTrackerAudit.DataBind();
            }

        }
        protected void gvTimeTrackerAudit_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Width = 300;
            e.Row.Cells[3].Width = 500;
            e.Row.Cells[4].Width = 100;
            e.Row.Cells[5].Width = 100;
            e.Row.Cells[6].Width = 100;
        }




    }
}
