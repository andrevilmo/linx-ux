// -----------------------------------------------------------------------
// <copyright file="LinxAuthorizationSearch.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Business.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;
    using System.Text;
    using Linx.Tools;
    using Linx.Data;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity;
    using Linx.Framework.BV;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class LinxAuthorizationSearch 
    {
        //ObjectContext
        public static string GetSecurityFilter(ObjectContext edmContext, string primaryEntity)
        {
            return LinxBusinessAuthorizationSearch.GetSecurityFilter(edmContext, primaryEntity);
        }
        public static string GetEconomicGroupFilter(ObjectContext edmContext, string primaryEntity, string edmPath)
        {
            return GetEconomicGroupFilter(edmContext, primaryEntity, edmPath, "");
        }
        public static string GetEconomicGroupFilter(ObjectContext edmContext, string primaryEntity, string edmPath, string economicGroups)
        {
            return LinxBusinessAuthorizationSearch.GetEconomicGroupFilter(edmContext, primaryEntity, edmPath, economicGroups);
        }

        //DbContext
        public static string GetSecurityFilter(DbContext edmContext, string primaryEntity)
        {
            return LinxBusinessAuthorizationSearch.GetSecurityFilter(edmContext, primaryEntity);
        }
        public static string GetEconomicGroupFilter(DbContext edmContext, string primaryEntity, string edmPath)
        {
            return GetEconomicGroupFilter(edmContext, primaryEntity, edmPath, "");
        }
        public static string GetEconomicGroupFilter(DbContext edmContext, string primaryEntity, string edmPath, string economicGroups)
        {
            return LinxBusinessAuthorizationSearch.GetEconomicGroupFilter(edmContext, primaryEntity, edmPath, economicGroups);
        }
    }
}