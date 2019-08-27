// ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Resource.cs" company="Naico IT Services Pvt. Ltd">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
//  Description  : Add an new role to dropdown list.
//  Author       : Haritha ES
//  Created Date : 19 April 2018
//  Modified By  : Haritha ES
//  Modified Date:   
// </summary>
// -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PMOscar.Core;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PMOscar
{
    public partial class AddRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblmsg.Text = string.Empty;
            string query = string.Format("Select Role from [dbo].[Role] where [Role] IN ('{0}') order by Role",txtRole.Text);
            DataTable resourceRole = PMOscar.BaseDAL.ExecuteDataTable(query);

            if (txtEstpercent.Text != string.Empty)
            {
                if (decimal.Parse(txtEstpercent.Text) > Convert.ToDecimal(PMOscar.Utility.EnumTypes.Estimation.EstimationPercentage))
                {
                    lblEstimation.Visible = true;
                    lblEstimation.Text = PMOscar.Core.Constants.AddRole.ESTIMATIONPERCENT;
                    return;
                }
            }

            if (resourceRole.Rows.Count > 0)
            {
                lblmsg.Text = Constants.AddRole.DUPLICATE;
                lblEstimation.Visible = false;
            }
           
            else 
            {
                lblEstimation.Visible = false;
                string query1 = string.Format("insert into [dbo].[EstimationRole](RoleName,ShortName) values('{0}','{1}')", txtRole.Text, txtShortName.Text);
                string query2 = string.Format("select MAX(EstimationRoleID) from EstimationRole");

                try
                {
                    int result1 = PMOscar.BaseDAL.ExecuteNonQuery(query1);
                    object Estimation = PMOscar.BaseDAL.ExecuteScalar(query2);
                    string query3 = string.Format("insert into  Role (Role,ShortName, Desription,EstimationPercentage,EstimationRoleID) values('{0}','{1}','{2}',{3},{4})", txtRole.Text, txtShortName.Text, txtDescription.Text, txtEstpercent.Text,Estimation);
                    int result = PMOscar.BaseDAL.ExecuteNonQuery(query3);
                    lblmsg.Text = Constants.AddRole.SAVE;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Key", "window.opener.location.reload();window.history.back();self.close();", true);
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }
    }
}