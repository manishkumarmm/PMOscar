// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reports.cs" company="Naico IT Services Pvt. Ltd.">
//   This File is owned by Naico IT Services Pvt. Ltd. 
// </copyright>
// <summary>
// File Name   : Project.cs
// Created Date: 12/04/2012
// Description : Model class for Project.
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
    /// Model class for Project.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>
        /// The project id.
        /// </value>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>
        /// The name of the project.
        /// </value>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        /// <value>
        /// The short name.
        /// </value>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the project code.
        /// </summary>
        /// <value>
        /// The project code.
        /// </value>
        public string ProjectCode { get; set; }

        /// <summary>
        /// Gets or sets the project owner.
        /// </summary>
        /// <value>
        /// The project owner.
        /// </value>
        public string ProjectOwner { get; set; }

        /// <summary>
        /// Gets or sets the project manager.
        /// </summary>
        /// <value>
        /// The project manager.
        /// </value>
        public string ProjectManager { get; set; }

        /// <summary>
        /// Gets or sets the program id.
        /// </summary>
        /// <value>
        /// The program id.
        /// </value>
        public int ProgramId { get; set; }

        /// <summary>
        /// Gets or sets the name of the program.
        /// </summary>
        /// <value>
        /// The name of the program.
        /// </value>
        public string ProgramName { get; set; }

        /// <summary>
        /// Gets or sets the current phase.
        /// </summary>
        /// <value>
        /// The current phase.
        /// </value>
        public string CurrentPhase { get; set; }


    }
}
