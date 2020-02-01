using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MapTest.Models
{
    public enum CommonFilters
    {
        /// <summary>
        /// По всем федеральным проектам
        /// </summary>
        [Description("По всем федеральным проектам")]
        ActiveFederalProjects = 1,

        /// <summary>
        /// По выбранным проектам
        /// </summary>
        [Description("По выбранным проектам")]
        SelectedFederalProjects = 2,

        /// <summary>
        /// По завершенным федеральным проектам
        /// </summary>
        [Description("По завершенным федеральным проектам")]
        CompletedFederalProjects = 3,
    }
}
