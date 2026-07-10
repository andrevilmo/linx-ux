using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Tools
{
    public interface ISecurityHelper
    {
        string GetConnectionString(string connectionName);

        string GetConnectionString(string connectionName, Dictionary<string, string> headers);

        Int64? GetCurrentUserId();

        Int64? GetCurrentUserId(Dictionary<string, string> headers);

        Guid? GetCurrentUserUid();

        Guid? GetCurrentUserUid(Dictionary<string, string> headers);

        Guid? GetCurrentCompanyId();

        Guid? GetCurrentCompanyId(Dictionary<string, string> headers);

        Guid? GetAuthorizationToken();

        Guid? GetAuthorizationToken(Dictionary<string, string> headers);

        string GetTransactionInfo();

        string GetTransactionInfo(Dictionary<string, string> headers);

        string GetCurrentUserName();

        string GetCurrentUserName(Dictionary<string, string> headers);

        string GetCurrentUserAuthenticationName();

        string GetCurrentUserAuthenticationName(Dictionary<string, string> headers);

        int? GetCurrentApplicativeId();

        int? GetCurrentApplicativeId(Dictionary<string, string> headers);

        int? GetApplicativeIdByMediaUse(int usabilityId);

        Guid? GetCurrentApplicationId();

        Guid? GetCurrentApplicationId(Dictionary<string, string> headers);

        Guid? GetCurrentAccessGroupId();

        Guid? GetCurrentAccessGroupId(Dictionary<string, string> headers);

        Guid? GetCurrentEconomicGroupId();

        Guid? GetCurrentEconomicGroupId(Dictionary<string, string> headers);

        int? GetCurrentEnvironmentId();

        int? GetCurrentEnvironmentId(Dictionary<string, string> headers);

        int? GetCurrentIdLinx(string connectionName);

        int? GetCurrentIdLinx(string connectionName, Dictionary<string, string> headers);

        int? GetCurrentIdLinx(string connectionName, int applicativeId, Dictionary<string, string> headers = null);

        int? GetCurrentIdGpecon();

        int? GetCurrentIdGpecon(Dictionary<string, string> headers);

        int? GetCurrentIdLinxEnvironment();

        int? GetCurrentIdLinxEnvironment(Dictionary<string, string> headers);

        bool IsUserMultiGpecon();

        bool IsUserMultiGpecon(Dictionary<string, string> headers);

        string GetCustomSearchById(Int64 uidSearch);

        Dictionary<Int64, string> GetCustomSearchList(string entityName);

        int[] GetCurrentUserBrandInfo(Dictionary<string, string> headers = null);

        int[] GetCurrentUserBrandInfo(string connectionName, Dictionary<string, string> headers = null);

        Dictionary<string, string> GetRelatedEnvironmentInfo();

        Dictionary<string, string> GetRelatedEnvironmentInfo(Dictionary<string, string> headers);

        Dictionary<string, string> GetEnvironmentInfo(int applicativeId, Dictionary<string, string> headers = null);

        bool AddMessage(string titulo, string corpo, List<EntitySearch> filtro, DateTime? dataEnvio, int idLinx, byte lxTipoMensagem);

        int[] GetCurrentUserGpeconInfo(Dictionary<string, string> headers = null);

        #region Audit methods
        bool GetAuditIsEnabled(int? idLinx);

        string[] GetAuditIgnoredTables(int? idLinx);

        string[] GetAuditIgnoredSchemas(int? idLinx);
        #endregion
    }

    public interface ILinx
    {
        int ID_LINX { get; set; }
    }

    public interface IGpecon
    {
        int ID_GPECON { get; set; }
    }
}
