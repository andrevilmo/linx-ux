using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Linx.Tools;
using Linx.Framework.BV;
using System.ComponentModel.Composition;

namespace Linx.Business.Tools
{
    public static class UserServiceHelper
    {
        public static Guid? GetCurrentUserUid()
        {
            return BusinessUserServiceHelper.GetCurrentUserUid();
        }

        public static Guid? GetCurrentUserUid(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentUserUid(headers);
        }

        public static Int64? GetCurrentUserId()
        {
            return BusinessUserServiceHelper.GetCurrentUserId();
        }

        public static Int64? GetCurrentUserId(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentUserId(headers);
        }

        public static bool IsUserMultiGpecon()
        {
            return IsUserMultiGpecon(null);
        }

        public static bool IsUserMultiGpecon(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.IsUserMultiGpecon(headers);
        }

        public static bool IsUserAdministrator()
        {
            return IsUserAdministrator(null);
        }

        public static bool IsUserAdministrator(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.IsUserAdministrator(headers);
        }

        public static Guid? GetCurrentCompanyId()
        {
            return BusinessUserServiceHelper.GetCurrentCompanyId();
        }

        public static Guid? GetCurrentCompanyId(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentCompanyId(headers);
        }

        public static Guid? GetAuthorizationToken()
        {
            return BusinessUserServiceHelper.GetAuthorizationToken();
        }

        public static Guid? GetAuthorizationToken(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetAuthorizationToken(headers);
        }

        public static string GetTransactionInfo()
        {
            return BusinessUserServiceHelper.GetTransactionInfo();
        }

        public static string GetTransactionInfo(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetTransactionInfo(headers);
        }

        public static string GetCurrentUserName()
        {
            return BusinessUserServiceHelper.GetCurrentUserName();
        }

        public static string GetCurrentUserName(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentUserName(headers);
        }

        public static string GetCurrentUserAuthenticationName()
        {
            return BusinessUserServiceHelper.GetCurrentUserAuthenticationName();
        }

        public static string GetCurrentUserAuthenticationName(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentUserAuthenticationName(headers);
        }

        public static int? GetCurrentApplicativeId()
        {
            return GetCurrentApplicativeId(null);
        }

        public static int? GetCurrentApplicativeId(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentApplicativeId(headers);
        }

        public static int? GetApplicativeIdByMediaUse(int usabilityId)
        {
            return BusinessUserServiceHelper.GetApplicativeIdByMediaUse(usabilityId);
        }

        public static Guid? GetCurrentApplicationId()
        {
            return BusinessUserServiceHelper.GetCurrentApplicationId();
        }

        public static Guid? GetCurrentApplicationId(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentApplicationId(headers);
        }

        public static Guid? GetCurrentAccessGroupId()
        {
            return BusinessUserServiceHelper.GetCurrentAccessGroupId();
        }

        public static Guid? GetCurrentAccessGroupId(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentAccessGroupId(headers);
        }

        public static Guid? GetCurrentEconomicGroupId()
        {
            return BusinessUserServiceHelper.GetCurrentEconomicGroupId();
        }

        public static Guid? GetCurrentEconomicGroupId(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentEconomicGroupId(headers);
        }

        public static int? GetCurrentEnvironmentId()
        {
            return BusinessUserServiceHelper.GetCurrentEnvironmentId();
        }

        public static int? GetCurrentEnvironmentId(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentEnvironmentId(headers);
        }

        public static int? GetCurrentIdLinx(string connectionName)
        {
            return BusinessUserServiceHelper.GetCurrentIdLinx(connectionName);
        }

        public static int? GetCurrentIdLinx(string connectionName, Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentIdLinx(connectionName, headers);
        }

        public static int? GetCurrentIdLinx(string connectionName, int applicativeId, Dictionary<string, string> headers = null)
        {
            return BusinessUserServiceHelper.GetCurrentIdLinx(connectionName, applicativeId, headers);
        }

        public static int? GetCurrentIdGpecon()
        {
            return BusinessUserServiceHelper.GetCurrentIdGpecon();
        }

        public static int? GetCurrentIdGpecon(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentIdGpecon(headers);
        }

        public static int? GetCurrentIdLinxEnvironment()
        {
            return BusinessUserServiceHelper.GetCurrentIdLinxEnvironment();
        }

        public static int? GetCurrentIdLinxEnvironment(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetCurrentIdLinxEnvironment(headers);
        }

        public static string GetCustomSearchById(Int64 uidSearch)
        {
            return BusinessUserServiceHelper.GetCustomSearchById(uidSearch);
        }

        public static Dictionary<Int64, string> GetCustomSearchList(string entityName)
        {
            return BusinessUserServiceHelper.GetCustomSearchList(entityName);
        }

        public static int[] GetCurrentUserBrandInfo(Dictionary<string, string> headers = null)
        {
            return BusinessUserServiceHelper.GetCurrentUserBrandInfo(headers);
        }
        public static int[] GetCurrentUserBrandInfo(string connectionName, Dictionary<string, string> headers = null)
        {
            return BusinessUserServiceHelper.GetCurrentUserBrandInfo(connectionName, headers);
        }

        public static Dictionary<string, string> GetRelatedEnvironmentInfo()
        {
            return BusinessUserServiceHelper.GetRelatedEnvironmentInfo();
        }

        public static Dictionary<string, string> GetRelatedEnvironmentInfo(Dictionary<string, string> headers)
        {
            return BusinessUserServiceHelper.GetRelatedEnvironmentInfo(headers);
        }

        public static Dictionary<string, string> GetEnvironmentInfo(int applicativeId, Dictionary<string, string> headers = null)
        {
            return BusinessUserServiceHelper.GetEnvironmentInfo(applicativeId, headers);
        }

        public static bool AddMessage(string titulo, string corpo, List<EntitySearch> filtro, DateTime? dataEnvio, int idLinx, byte lxTipoMensagem)
        {
            return BusinessUserServiceHelper.AddMessage(titulo, corpo, filtro, dataEnvio, idLinx, lxTipoMensagem);
        }

        public static int[] GetCurrentUserGpeconInfo(Dictionary<string, string> headers = null)
        {
            return BusinessUserServiceHelper.GetCurrentUserGpeconInfo(headers);
        }

        public static Guid AddEntySearchToCache(string serializedEntitySearch, string jEntitySearch)
        {
            return BusinessUserServiceHelper.AddEntySearchToCache(serializedEntitySearch, jEntitySearch);
        }

        public static string[] GetEntitySearchFromCache(Guid entitySearchUid)
        {
            return BusinessUserServiceHelper.GetEntitySearchFromCache(entitySearchUid);
        }
    }
}
