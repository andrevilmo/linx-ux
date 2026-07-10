// -----------------------------------------------------------------------
// <copyright file="LinxAuthorizationSearch.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Framework.BV
{
    using Linx.Framework.BV.GrupoEconomico;
    using Linx.Tools;
    using System;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class LinxBusinessAuthorizationSearch
    {

        public static string GetSecurityFilter(System.Data.Entity.Core.Objects.ObjectContext edmContext, string primaryEntity)
        {
            return "true";
        }
        public static string GetSecurityFilter(DbContext edmContext, string primaryEntity)
        {
            return "true";
        }


        public static string GetEconomicGroupFilter(System.Data.Entity.Core.Objects.ObjectContext edmContext, string primaryEntity, string edmPath)
        {
            return GetEconomicGroupFilter(edmContext, primaryEntity, edmPath, "");
        }
        public static string GetEconomicGroupFilter(DbContext edmContext, string primaryEntity, string edmPath)
        {
            return GetEconomicGroupFilter(edmContext, primaryEntity, edmPath, "");
        }


        public static string GetEconomicGroupFilter(System.Data.Entity.Core.Objects.ObjectContext edmContext, string primaryEntity, string edmPath, string economicGroups)
        {
            if (edmPath.IsNullOrEmpty())
                return "true";

            GrupoEconomicoDomainService context = new GrupoEconomicoDomainService();

            Int64? currentUser = BusinessUserServiceHelper.GetCurrentUserId();
            if (currentUser != null)
            {
                if (economicGroups.IsNullOrEmpty())
                {
                    var eGroups = (from u in context.GetTcsUsuarioGpeconNoAssociations()
                                   where u.IdUsuario == currentUser.Value
                                   select u.IdParentGpecon).ToArray();

                    if (eGroups.Length == 0)
                        return "true";

                    //Find End Groups
                    Action<int> getEnds = null;
                    getEnds = (id) =>
                    {
                        //aqui

                        //var detailsGroups = (from u in context.GetTbcGrupoEconomicoNoAssociations()
                        //                     where u.IdGpeconSuperior == id
                        //                     select u.IdGpecon).ToArray();

                        //if (detailsGroups.Length == 0) // End group founded
                        //{
                        //    if (!("," + economicGroups + ",").Contains("," + id.ToString() + ","))
                        //        economicGroups += (economicGroups.IsNullOrEmpty() ? String.Empty : ",") + id.ToString();
                        //}
                        //else
                        //{
                        //    if (primaryEntity == "TBC_GRUPO_ECONOMICO")
                        //        economicGroups += (economicGroups.IsNullOrEmpty() ? String.Empty : ",") + id.ToString();

                        //    foreach (int dId in detailsGroups)
                        //        getEnds(dId);
                        //}
                    };

                    foreach (int id in eGroups)
                        getEnds(id);
                    ///////////////////
                }

                if (!economicGroups.IsNullOrEmpty())
                {
                    string filterResult = String.Empty;
                    string[] paths = edmPath.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string path in paths)
                    {
                        filterResult += (filterResult.IsNullOrEmpty() ? String.Empty : " And ") + path + ".ID_GPECON" + " In {" + economicGroups + "}";
                    }
                    return (filterResult.IsNullOrEmpty() ? "true" : "(" + filterResult + ")");
                }
                else return "true";
            }
            else return "true";
        }
        public static string GetEconomicGroupFilter(DbContext edmContext, string primaryEntity, string edmPath, string economicGroups)
        {
            if (edmPath.IsNullOrEmpty())
                return "true";

            GrupoEconomicoDomainService context = new GrupoEconomicoDomainService();

            Int64? currentUser = BusinessUserServiceHelper.GetCurrentUserId();
            if (currentUser != null)
            {
                if (economicGroups.IsNullOrEmpty())
                {
                    var eGroups = (from u in context.GetTcsUsuarioGpeconNoAssociations()
                                   where u.IdUsuario == currentUser.Value
                                   select u.IdParentGpecon).ToArray();

                    if (eGroups.Length == 0)
                        return "true";

                    //Find End Groups
                    Action<int> getEnds = null;
                    getEnds = (id) =>
                        {
                            //aqui
                            //var detailsGroups = (from u in context.GetTbcGrupoEconomicoNoAssociations()
                            //                     where u.IdGpeconSuperior == id
                            //                     select u.IdGpecon).ToArray();

                            //if (detailsGroups.Length == 0) // End group founded
                            //{
                            //    if (!("," + economicGroups + ",").Contains("," + id.ToString() + ","))
                            //        economicGroups += (economicGroups.IsNullOrEmpty() ? String.Empty : ",") + id.ToString();
                            //}
                            //else
                            //{
                            //    if (primaryEntity == "TBC_GRUPO_ECONOMICO")
                            //        economicGroups += (economicGroups.IsNullOrEmpty() ? String.Empty : ",") + id.ToString();

                            //    foreach (int dId in detailsGroups)
                            //        getEnds(dId);
                            //}
                        };

                    foreach (int id in eGroups)
                        getEnds(id);
                    ///////////////////
                }

                if (!economicGroups.IsNullOrEmpty())
                {
                    string filterResult = String.Empty;
                    string[] paths = edmPath.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string path in paths)
                    {
                        filterResult += (filterResult.IsNullOrEmpty() ? String.Empty : " And ") + path + ".ID_GPECON" + " In {" + economicGroups + "}";
                    }
                    return (filterResult.IsNullOrEmpty() ? "true" : "(" + filterResult + ")");
                }
                else return "true";
            }
            else return "true";
        }
    }
}