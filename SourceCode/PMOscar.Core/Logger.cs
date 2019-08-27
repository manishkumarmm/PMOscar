// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : Log.cs
// Created Date: 10/04/2012
// Description : This class consist of method to long exceptions.
// Last Modified Date:22/12/2011
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PMOscar.Core
{
    using System;
    using log4net;
    using System.Configuration;

    /// <summary>
    /// This class consist of method to long exceptions.
    /// </summary>
    public static class Logger
    {
        ////File Log Flag (Enabled/Disabled)
        private static readonly string fileLog = ConfigurationManager.AppSettings["fileLog"].Trim().ToLower();

        /// <summary>
        /// To log an exception.
        /// </summary>
        /// <param name="message">Custome error message.</param>
        /// <param name="exception">Inner exception.</param>
        public static void Write(string message, Exception exception)
        {
            if (fileLog == "true")
            {
                //Gettings File log configuration from web.config for log4net
                ILog ifileLog = LogManager.GetLogger("File");
                ifileLog.Error(message, exception);
            }
        }

        /// <summary>
        /// To log an error
        /// </summary>
        /// <param name="message">Custome error message.</param>
        /// <param name="exception">Inner exception.</param>
        public static void Error(string message, Exception exception)
        {
            if (fileLog == "true")
            {
                //Gettings File log configuration from web.config for log4net
                ILog ifileLog = LogManager.GetLogger("File");
                ifileLog.Error(message, exception);
            }
        }

        /// <summary>
        /// To log an information
        /// </summary>
        /// <param name="message">Info</param>
        public static void Info(string message)
        {
            if (fileLog == "true")
            {
                //Gettings File log configuration from web.config for log4net
                ILog ifileLog = LogManager.GetLogger("File");
                ifileLog.Info(message);
            }
        }
    }
}
