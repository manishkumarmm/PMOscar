// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDAL.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : UserDAL.cs
// Created Date: 11/05/2012
// Description : DAL class for User.
// Last Modified Date:
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PMOscar.Model;
using PMOscar.Core;
using System.Data;

namespace PMOscar.DAL
{
    /// <summary>
    /// DAL class for User
    /// </summary>
    public class UserDAL : BaseDAL
    {
        /// <summary>
        /// Checks the user authentication.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public bool CheckUserAuthentication(string userName, string password, out User user, bool gsuite = false)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserName", userName));
            parameters.Add(new SqlParameter("@Password", password));

            var dtUser = new DataTable();
            if (gsuite)
            {
                string query = string.Format("SELECT UserID, FirstName, MiddleName, LastName, UserName,[Password], EmailId, U.UserRoleID,UR.UserRole,IsActive FROM [dbo].[User] U INNER JOIN dbo.UserRole UR ON U.UserRoleID = UR.UserRoleID WHERE UserName = '{0}'", userName);

                dtUser = ExecuteDataTable(query);
            }
            else
            {
                dtUser = ExecuteSPDataTable("CheckUserAuthentication", parameters);
            }


            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                // Check match case of password
                // if (dtUser.Rows[0][Constants.FieldName.User.PASSWORD].ToString().Equals(password))
                {
                    user = new User
                    {
                        UserId = Convert.ToInt32(dtUser.Rows[0][Constants.FieldName.User.USERID]),
                        UserName = dtUser.Rows[0][Constants.FieldName.User.USERNAME].ToString(),
                        Password = dtUser.Rows[0][Constants.FieldName.User.PASSWORD].ToString(),
                        FirstName = dtUser.Rows[0][Constants.FieldName.User.FIRSTNAME].ToString(),
                        MiddleName = dtUser.Rows[0][Constants.FieldName.User.MIDDLENAME].ToString(),
                        LastName = dtUser.Rows[0][Constants.FieldName.User.LASTNAME].ToString(),
                        EmailId = dtUser.Rows[0][Constants.FieldName.User.EMAILID].ToString(),
                        UserRole =
                                       new UserRole
                                       {
                                           UserRoleId = Convert.ToInt32(dtUser.Rows[0][Constants.FieldName.UserRole.USERROLEID]),
                                           RoleName = dtUser.Rows[0][Constants.FieldName.UserRole.USERROLE].ToString()
                                       },
                        IsActive = Convert.ToBoolean(dtUser.Rows[0][Constants.FieldName.User.ISACTIVE])
                    };

                    return true;
                }
            }

            user = null;
            return false;
        }
    }
}
