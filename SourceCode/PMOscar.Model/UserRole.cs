// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRole.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : UserRole.cs
// Created Date: 11/05/2012
// Description : Model class for User Role.
// Last Modified Date:
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PMOscar.Model
{
    /// <summary>
    /// Model class for User Role
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// Gets or sets the user role ID.
        /// </summary>
        /// <value>
        /// The user role ID.
        /// </value>
        public int UserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the user role name.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public string RoleName { get; set; }
    }
}
