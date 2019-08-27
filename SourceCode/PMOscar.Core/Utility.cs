// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : Utility.cs
// Created Date: 06/01/2012
// Description : Class for declairing application utility methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace PMOscar.Utility
{
    /// <summary>
    /// Class for declairing application utility methods
    /// </summary>
    public class Utility
    {
        public static DateTime CompanyUtiliztionSummartReportStartDate = Convert.ToDateTime(ConfigurationManager.AppSettings["CompanyUtiliztionSummartReport_StartDate"]);
    }
}
