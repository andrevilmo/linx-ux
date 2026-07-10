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
    //var serviceHelper = Linx.Tools.ImplementationHelper<ISecurityHelper>.GetInstance("SecurityHelper", "Linx.Business.Tools");
    [Export(typeof(ISecurityHelper))]
    [ExportMetadata("ImplementationName", "SecurityHelper")]
    public class SecurityHelper : ISecurityHelper
    {
        public string GetConnectionString(string connectionName)
        {
            return GetConnectionString(connectionName, null);
        }

        public string GetConnectionString(string connectionName, Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return "Name=" + connectionName;
            else
                return BusinessCacheAccessHelper.GetConnectionString(connectionName, headers);
        }

        public int? GetApplicativeIdByMediaUse(int usabilityId)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.ApplicativeId;
            else
                return UserServiceHelper.GetApplicativeIdByMediaUse(usabilityId);
        }

        public Guid? GetAuthorizationToken(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.AuthorizationToken;
            else
                return UserServiceHelper.GetAuthorizationToken(headers);
        }

        public Guid? GetAuthorizationToken()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.AuthorizationToken;
            else
                return UserServiceHelper.GetAuthorizationToken();
        }

        public Guid? GetCurrentAccessGroupId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.AccessGroup;
            else
                return UserServiceHelper.GetCurrentAccessGroupId(headers);
        }

        public Guid? GetCurrentAccessGroupId()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.AccessGroup;
            else
                return UserServiceHelper.GetCurrentAccessGroupId();
        }

        public Guid? GetCurrentApplicationId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.ApplicationId;
            else
                return UserServiceHelper.GetCurrentApplicationId(headers);
        }

        public Guid? GetCurrentApplicationId()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.ApplicationId;
            else
                return UserServiceHelper.GetCurrentApplicationId();
        }

        public int? GetCurrentApplicativeId()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.ApplicativeId;
            else
                return GetCurrentApplicativeId(null);
        }

        public int? GetCurrentApplicativeId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.ApplicativeId;
            else
                return UserServiceHelper.GetCurrentApplicativeId(headers);
        }

        public Guid? GetCurrentCompanyId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.CurrentCompany;
            else
                return UserServiceHelper.GetCurrentCompanyId(headers);
        }

        public Guid? GetCurrentCompanyId()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.CurrentCompany;
            else
                return UserServiceHelper.GetCurrentCompanyId();
        }

        public Guid? GetCurrentEconomicGroupId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.EconomicGroup;
            else
                return UserServiceHelper.GetCurrentEconomicGroupId(headers);
        }

        public Guid? GetCurrentEconomicGroupId()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.EconomicGroup;
            else
                return UserServiceHelper.GetCurrentEconomicGroupId();
        }

        public int? GetCurrentEnvironmentId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.Environment;
            else
                return UserServiceHelper.GetCurrentEnvironmentId(headers);
        }

        public int? GetCurrentEnvironmentId()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.Environment;
            else
                return UserServiceHelper.GetCurrentEnvironmentId();
        }

        public int? GetCurrentIdGpecon(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.IdGpecon;
            else
                return UserServiceHelper.GetCurrentIdGpecon(headers);
        }

        public int? GetCurrentIdGpecon()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.IdGpecon;
            else
                return UserServiceHelper.GetCurrentIdGpecon();
        }

        public int? GetCurrentIdLinx(string connectionName, Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.IdLinx;
            else
                return UserServiceHelper.GetCurrentIdLinx(connectionName, headers);
        }

        public int? GetCurrentIdLinx(string connectionName)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.IdLinx;
            else
                return UserServiceHelper.GetCurrentIdLinx(connectionName);
        }

        public int? GetCurrentIdLinx(string connectionName, int applicativeId, Dictionary<string, string> headers = null)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.IdLinx;
            else
                return UserServiceHelper.GetCurrentIdLinx(connectionName, applicativeId, headers);
        }

        public int? GetCurrentIdLinxEnvironment()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.IdLinx;
            else
                return UserServiceHelper.GetCurrentIdLinxEnvironment();
        }

        public int? GetCurrentIdLinxEnvironment(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.IdLinx;
            else
                return UserServiceHelper.GetCurrentIdLinxEnvironment(headers);
        }

        public string GetCurrentUserAuthenticationName(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.CurrentUserName;
            else
                return UserServiceHelper.GetCurrentUserAuthenticationName(headers);
        }

        public string GetCurrentUserAuthenticationName()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.CurrentUserName;
            else
                return UserServiceHelper.GetCurrentUserAuthenticationName();
        }

        public Guid? GetCurrentUserUid(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.CurrentUser;
            else
                return UserServiceHelper.GetCurrentUserUid(headers);
        }

        public Guid? GetCurrentUserUid()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.CurrentUser;
            else
                return UserServiceHelper.GetCurrentUserUid();
        }

        public Int64? GetCurrentUserId(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.CurrentUserId;
            else
                return UserServiceHelper.GetCurrentUserId(headers);
        }

        public Int64? GetCurrentUserId()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.CurrentUserId;
            else
                return UserServiceHelper.GetCurrentUserId();
        }

        public string GetCurrentUserName(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.CurrentUserName;
            else
                return UserServiceHelper.GetCurrentUserName(headers);
        }

        public string GetCurrentUserName()
        {
            if (LocalServiceBus.Enabled)
                return LocalServiceBus.CurrentUserName;
            else
                return UserServiceHelper.GetCurrentUserName();
        }

        public string GetTransactionInfo(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return null;
            else
                return UserServiceHelper.GetTransactionInfo(headers);
        }

        public string GetTransactionInfo()
        {
            if (LocalServiceBus.Enabled)
                return null;
            else
                return UserServiceHelper.GetTransactionInfo();
        }

        public bool IsUserMultiGpecon()
        {
            if (LocalServiceBus.Enabled)
                return Linx.Tools.LocalServiceBus.IsUserMultiGpecon;
            else
                return UserServiceHelper.IsUserMultiGpecon();
        }

        public bool IsUserMultiGpecon(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return Linx.Tools.LocalServiceBus.IsUserMultiGpecon;
            else
                return UserServiceHelper.IsUserMultiGpecon(headers);
        }

        public string GetCustomSearchById(Int64 idSearch)
        {
            if (LocalServiceBus.Enabled)
                return null;
            else
                return UserServiceHelper.GetCustomSearchById(idSearch);
        }

        public Dictionary<Int64, string> GetCustomSearchList(string entityName)
        {
            if (LocalServiceBus.Enabled)
                return null;
            else
                return UserServiceHelper.GetCustomSearchList(entityName);
        }

        public int[] GetCurrentUserBrandInfo(Dictionary<string, string> headers = null)
        {
            if (LocalServiceBus.Enabled)
                return new int[0];
            else
                return UserServiceHelper.GetCurrentUserBrandInfo(headers);
        }

        public int[] GetCurrentUserBrandInfo(string connectionName, Dictionary<string, string> headers = null)
        {
            if (LocalServiceBus.Enabled)
                return new int[0];
            else
                return UserServiceHelper.GetCurrentUserBrandInfo(connectionName, headers);
        }

        public Dictionary<string, string> GetRelatedEnvironmentInfo()
        {
            if (LocalServiceBus.Enabled)
                return new Dictionary<string, string>();
            else
                return UserServiceHelper.GetRelatedEnvironmentInfo();
        }

        public Dictionary<string, string> GetRelatedEnvironmentInfo(Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled)
                return new Dictionary<string, string>();
            else
                return UserServiceHelper.GetRelatedEnvironmentInfo(headers);
        }

        public Dictionary<string, string> GetEnvironmentInfo(int applicativeId, Dictionary<string, string> headers = null)
        {
            if (LocalServiceBus.Enabled)
            {
                Dictionary<string, string> info = new Dictionary<string, string>();
                info.Add("Application", GetCurrentApplicationId().GetValueOrDefault().ToString());
                info.Add("CurrentCompany", GetCurrentCompanyId().GetValueOrDefault().ToString());
                info.Add("Environment", GetCurrentEnvironmentId().GetValueOrDefault().ToString());
                info.Add("CurrentUser", GetCurrentUserUid().GetValueOrDefault().ToString());
                info.Add("EconomicGroup", GetCurrentEconomicGroupId().GetValueOrDefault().ToString());
                return info;
            }
            else
                return UserServiceHelper.GetEnvironmentInfo(applicativeId, headers);
        }
        public bool AddMessage(string titulo, string corpo, List<EntitySearch> filtro, DateTime? dataEnvio, int idLinx, byte lxTipoMensagem)
        {
            return UserServiceHelper.AddMessage(titulo, corpo, filtro, dataEnvio, idLinx, lxTipoMensagem);
        }

        public int[] GetCurrentUserGpeconInfo(Dictionary<string, string> headers = null)
        {
            if (LocalServiceBus.Enabled)
                return new int[] { LocalServiceBus.IdGpecon };
            else
                return UserServiceHelper.GetCurrentUserGpeconInfo(headers);
        }

        public bool GetAuditIsEnabled(int? idLinx)
        {
            return BusinessUserServiceHelper.GetAuditIsEnabled(idLinx);
        }

        public string[] GetAuditIgnoredTables(int? idLinx)
        {
            return BusinessUserServiceHelper.GetAuditIgnoredTables(idLinx);
        }

        public string[] GetAuditIgnoredSchemas(int? idLinx)
        {
            return BusinessUserServiceHelper.GetAuditIgnoredSchemas(idLinx);
        }
    }

}
