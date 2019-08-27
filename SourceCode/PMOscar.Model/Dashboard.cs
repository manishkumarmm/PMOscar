// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : Dashboard.cs
// Created Date: 12/04/2012
// Description : Model class for Dashboard.
// Last Modified Date:
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMOscar.Model
{
    /// <summary>
    /// Model class for Dashboard
    /// </summary>
    public class Dashboard
    {
        /// <summary>
        /// Gets or sets the dashboard id.
        /// </summary>
        /// <value>
        /// The dashboard id.
        /// </value>
        public int DashboardId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public string FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public string ToDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the period.
        /// </summary>
        /// <value>
        /// The type of the period.
        /// </value>
        public string PeriodType { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }
        public DateTime toSort { get; set; }

    }
}
