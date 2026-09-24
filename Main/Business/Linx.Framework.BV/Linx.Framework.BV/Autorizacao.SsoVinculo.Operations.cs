using System;
using System.Data.SqlClient;
using System.Linq;
using Linx.Tools;

namespace Linx.Framework.BV.Autorizacao
{
    public class PortalSsoVinculoResult
    {
        public bool Success { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public bool HasVinculo { get; set; }
        public bool CanRevoke { get; set; }
        public string NomeAutenticacao { get; set; }
        public long? IdUsuario { get; set; }
        public string AzureOid { get; set; }
        public string AzureUpn { get; set; }
        public DateTime? DataVinculo { get; set; }
        public DateTime? DataUltimoLogin { get; set; }
    }

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// PORTAL SSO VÍNCULO (Azure OID ↔ Linx) //////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class AutorizacaoDomainService
    {
        public const string SsoVinculoCodeFirst = "SSOI-FIRST";
        public const string SsoVinculoCodeLink = "SSOI-LINK";
        public const string SsoVinculoCodeLinkFail = "SSOF-LINK";
        public const string SsoVinculoCodeRevoke = "SSOI-REV";
        public const string SsoVinculoCodeRevokeFail = "SSOF-REV";
        public const string SsoVinculoCodeBindFail = "SSOF-BIND";

        private static bool _ssoVinculoTableEnsured;
        private static bool _ssoVinculoTableEnsureAttempted;

        private const string SsoVinculoEnsureSql = @"
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = N'LX_TCS')
    EXEC(N'CREATE SCHEMA [LX_TCS]');
IF NOT EXISTS (
    SELECT 1 FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE s.name = N'LX_TCS' AND t.name = N'TCS_USUARIO_SSO_VINCULO')
BEGIN
    CREATE TABLE [LX_TCS].[TCS_USUARIO_SSO_VINCULO]
    (
        [ID_USUARIO] BIGINT NOT NULL,
        [NOME_AUTENTICACAO] NVARCHAR(256) NOT NULL,
        [AZURE_OID] NVARCHAR(64) NOT NULL,
        [AZURE_UPN] NVARCHAR(256) NOT NULL,
        [DATA_VINCULO] DATETIME NOT NULL CONSTRAINT [DF_TCS_USUARIO_SSO_VINCULO_VINC] DEFAULT (GETDATE()),
        [DATA_ULTIMO_LOGIN] DATETIME NOT NULL CONSTRAINT [DF_TCS_USUARIO_SSO_VINCULO_ULT] DEFAULT (GETDATE()),
        CONSTRAINT [XPK_TCS_USUARIO_SSO_VINCULO] PRIMARY KEY CLUSTERED ([ID_USUARIO] ASC)
    );
    CREATE UNIQUE NONCLUSTERED INDEX [UX_TCS_USUARIO_SSO_VINCULO_OID]
        ON [LX_TCS].[TCS_USUARIO_SSO_VINCULO] ([AZURE_OID]);
END
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'UX_TCS_USUARIO_SSO_VINCULO_OID'
      AND object_id = OBJECT_ID(N'LX_TCS.TCS_USUARIO_SSO_VINCULO'))
    AND OBJECT_ID(N'LX_TCS.TCS_USUARIO_SSO_VINCULO', N'U') IS NOT NULL
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_TCS_USUARIO_SSO_VINCULO_OID]
        ON [LX_TCS].[TCS_USUARIO_SSO_VINCULO] ([AZURE_OID]);
END";

        private sealed class SsoVinculoUser
        {
            public long IdUsuario { get; set; }
            public string NomeAutenticacao { get; set; }
        }

        private sealed class SsoVinculoRow
        {
            public long IdUsuario { get; set; }
            public string NomeAutenticacao { get; set; }
            public string AzureOid { get; set; }
            public string AzureUpn { get; set; }
            public DateTime DataVinculo { get; set; }
            public DateTime DataUltimoLogin { get; set; }
        }

        private void EnsureSsoVinculoTable()
        {
            if (_ssoVinculoTableEnsured || _ssoVinculoTableEnsureAttempted)
                return;
            _ssoVinculoTableEnsureAttempted = true;
            try
            {
                this.DbContext.Database.ExecuteSqlCommand(SsoVinculoEnsureSql);
                _ssoVinculoTableEnsured = true;
            }
            catch
            {
                // SQL login may lack ALTER. SELECT/INSERT then surface as a visible error.
            }
        }

        public PortalSsoVinculoResult CheckPortalSsoVinculo(string userName, Guid? uidUsuario)
        {
            try
            {
                EnsureSsoVinculoTable();
                SsoVinculoUser user = ResolveSsoVinculoUser(userName, uidUsuario);
                if (user == null)
                {
                    return new PortalSsoVinculoResult
                    {
                        Success = true,
                        Code = SsoVinculoCodeLink,
                        Message = "Usuário não encontrado.",
                        HasVinculo = false,
                        CanRevoke = false,
                        NomeAutenticacao = userName
                    };
                }

                SsoVinculoRow row = LoadVinculoByUserId(user.IdUsuario);
                return ToCheckResult(user, row);
            }
            catch (Exception ex)
            {
                return new PortalSsoVinculoResult
                {
                    Success = true,
                    Code = SsoVinculoCodeLink,
                    Message = ex.Message,
                    HasVinculo = false,
                    CanRevoke = false,
                    NomeAutenticacao = userName
                };
            }
        }

        public PortalSsoVinculoResult BindPortalSsoVinculo(string userName, string azureOid, string azureUpn)
        {
            EnsureSsoVinculoTable();
            string localLogin = (userName ?? string.Empty).Trim();
            string oid = NormalizeAzureOid(azureOid);
            string upn = (azureUpn ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(localLogin))
                return BindFail(localLogin, oid, upn, null, "login local vazio.");
            if (string.IsNullOrEmpty(oid))
                return BindFail(localLogin, oid, upn, null, "Azure OID vazio.");

            SsoVinculoUser user;
            try
            {
                user = ResolveSsoVinculoUser(localLogin, null);
            }
            catch (Exception resolveEx)
            {
                return BindFail(localLogin, oid, upn, null, resolveEx.Message);
            }
            if (user == null)
                return BindFail(localLogin, oid, upn, null, "usuário sem cadastro local.");

            try
            {
                SsoVinculoRow existing = LoadVinculoByUserId(user.IdUsuario);
                SsoVinculoRow byOid = LoadVinculoByOid(oid);

                if (existing == null)
                {
                    if (byOid != null && byOid.IdUsuario != user.IdUsuario)
                    {
                        return BindFail(user.NomeAutenticacao, oid, upn, byOid,
                            "OID já vinculado a outro usuário Linx (" + byOid.NomeAutenticacao + ").");
                    }

                    InsertVinculo(user, oid, upn);
                    SsoVinculoRow created = LoadVinculoByUserId(user.IdUsuario);
                    string descricao = FormatVinculoDescricao("FIRST", user.NomeAutenticacao, oid, upn, created, null);
                    LogAuthAccessSsoProcess(user.NomeAutenticacao, false, SsoVinculoCodeFirst, descricao);
                    return ToBindSuccess(SsoVinculoCodeFirst, "Primeiro vínculo SSO gravado.", user, created);
                }

                if (!AzureOidEquals(existing.AzureOid, oid))
                {
                    string descricao = FormatMismatchDescricao(user.NomeAutenticacao, existing, oid, upn);
                    LogAuthAccessSsoProcess(user.NomeAutenticacao, true, SsoVinculoCodeLinkFail, descricao);
                    PortalSsoVinculoResult mismatch = ToCheckResult(user, existing);
                    mismatch.Success = false;
                    mismatch.Code = SsoVinculoCodeLinkFail;
                    mismatch.Message = "Conta Microsoft diferente da vinculada a este usuário. Use a conta Azure já associada ou peça para revogar o SSO no cadastro.";
                    return mismatch;
                }

                DateTime previousLast = existing.DataUltimoLogin;
                UpdateVinculoLastLogin(user, oid, upn);
                SsoVinculoRow updated = LoadVinculoByUserId(user.IdUsuario);
                string linkDescricao = FormatVinculoDescricao("LINK", user.NomeAutenticacao, oid, upn, updated, previousLast);
                LogAuthAccessSsoProcess(user.NomeAutenticacao, false, SsoVinculoCodeLink, linkDescricao);
                return ToBindSuccess(SsoVinculoCodeLink, "Vínculo SSO confirmado.", user, updated);
            }
            catch (SqlException sqlEx)
            {
                return BindFail(user.NomeAutenticacao, oid, upn, null, "erro SQL: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                return BindFail(user.NomeAutenticacao, oid, upn, null, ex.Message);
            }
        }

        public PortalSsoVinculoResult RevokePortalSsoVinculo(string userName, Guid? uidUsuario, string revokedByUserName)
        {
            EnsureSsoVinculoTable();
            SsoVinculoUser user = ResolveSsoVinculoUser(userName, uidUsuario);
            string revokedBy = ResolveRevokedBy(revokedByUserName);
            if (user == null)
            {
                string unknown = string.IsNullOrWhiteSpace(userName) ? "(UNKNOWN)" : userName.Trim();
                LogAuthAccessSsoProcess(unknown, true, SsoVinculoCodeRevokeFail,
                    "REV: local=" + unknown + " reason=usuário não encontrado by=" + revokedBy);
                return new PortalSsoVinculoResult
                {
                    Success = false,
                    Code = SsoVinculoCodeRevokeFail,
                    Message = "Usuário não encontrado para revogar SSO.",
                    NomeAutenticacao = unknown
                };
            }

            SsoVinculoRow row = LoadVinculoByUserId(user.IdUsuario);
            if (row == null)
            {
                LogAuthAccessSsoProcess(user.NomeAutenticacao, true, SsoVinculoCodeRevokeFail,
                    "REV: local=" + user.NomeAutenticacao + " reason=sem vínculo by=" + revokedBy);
                return new PortalSsoVinculoResult
                {
                    Success = false,
                    Code = SsoVinculoCodeRevokeFail,
                    Message = "Não há vínculo SSO para revogar.",
                    NomeAutenticacao = user.NomeAutenticacao,
                    IdUsuario = user.IdUsuario,
                    HasVinculo = false,
                    CanRevoke = false
                };
            }

            try
            {
                this.DbContext.Database.ExecuteSqlCommand(
                    "DELETE FROM [LX_TCS].[TCS_USUARIO_SSO_VINCULO] WHERE [ID_USUARIO] = @id",
                    new SqlParameter("@id", user.IdUsuario));

                LogAuthAccessSsoProcess(user.NomeAutenticacao, false, SsoVinculoCodeRevoke,
                    "REV: local=" + user.NomeAutenticacao
                    + " azure_oid=" + row.AzureOid
                    + " azure_upn=" + row.AzureUpn
                    + " by=" + revokedBy);

                return new PortalSsoVinculoResult
                {
                    Success = true,
                    Code = SsoVinculoCodeRevoke,
                    Message = "SSO revogado. No próximo login Microsoft o usuário vinculará a conta de novo.",
                    HasVinculo = false,
                    CanRevoke = false,
                    NomeAutenticacao = user.NomeAutenticacao,
                    IdUsuario = user.IdUsuario,
                    AzureOid = row.AzureOid,
                    AzureUpn = row.AzureUpn,
                    DataVinculo = row.DataVinculo,
                    DataUltimoLogin = row.DataUltimoLogin
                };
            }
            catch (Exception ex)
            {
                LogAuthAccessSsoProcess(user.NomeAutenticacao, true, SsoVinculoCodeRevokeFail,
                    "REV: local=" + user.NomeAutenticacao + " reason=" + ex.Message + " by=" + revokedBy);
                return new PortalSsoVinculoResult
                {
                    Success = false,
                    Code = SsoVinculoCodeRevokeFail,
                    Message = "Falha ao revogar SSO: " + ex.Message,
                    HasVinculo = true,
                    CanRevoke = true,
                    NomeAutenticacao = user.NomeAutenticacao,
                    IdUsuario = user.IdUsuario,
                    AzureOid = row.AzureOid,
                    AzureUpn = row.AzureUpn
                };
            }
        }

        private PortalSsoVinculoResult BindFail(string localLogin, string oid, string upn, SsoVinculoRow other, string reason)
        {
            string descricao = "BIND: local=" + (localLogin ?? "(UNKNOWN)")
                + " azure_oid=" + (oid ?? "(none)")
                + " azure_upn=" + (upn ?? "(none)")
                + " reason=" + (reason ?? "falha");
            if (other != null)
                descricao += " other_user=" + other.NomeAutenticacao;
            LogAuthAccessSsoProcess(string.IsNullOrEmpty(localLogin) ? "(UNKNOWN)" : localLogin, true, SsoVinculoCodeBindFail, descricao);
            return new PortalSsoVinculoResult
            {
                Success = false,
                Code = SsoVinculoCodeBindFail,
                Message = string.IsNullOrEmpty(reason)
                    ? "Não foi possível vincular a conta Microsoft."
                    : reason,
                NomeAutenticacao = localLogin,
                AzureOid = oid,
                AzureUpn = upn
            };
        }

        private static PortalSsoVinculoResult ToBindSuccess(string code, string message, SsoVinculoUser user, SsoVinculoRow row)
        {
            PortalSsoVinculoResult result = ToCheckResult(user, row);
            result.Success = true;
            result.Code = code;
            result.Message = message;
            return result;
        }

        private static PortalSsoVinculoResult ToCheckResult(SsoVinculoUser user, SsoVinculoRow row)
        {
            bool has = row != null;
            return new PortalSsoVinculoResult
            {
                Success = true,
                Code = has ? SsoVinculoCodeLink : SsoVinculoCodeFirst,
                Message = has ? "Vínculo SSO encontrado." : "Usuário sem vínculo SSO.",
                HasVinculo = has,
                CanRevoke = has,
                NomeAutenticacao = user != null ? user.NomeAutenticacao : null,
                IdUsuario = user != null ? (long?)user.IdUsuario : null,
                AzureOid = has ? row.AzureOid : null,
                AzureUpn = has ? row.AzureUpn : null,
                DataVinculo = has ? (DateTime?)row.DataVinculo : null,
                DataUltimoLogin = has ? (DateTime?)row.DataUltimoLogin : null
            };
        }

        private SsoVinculoUser ResolveSsoVinculoUser(string userName, Guid? uidUsuario)
        {
            // Same EF connection as login / GetPortalLoginOptions. A second SqlConnection
            // (CreateMfaConnection) times out on the AWS host.
            if (uidUsuario.HasValue && uidUsuario.Value != Guid.Empty)
            {
                return this.DbContext.Database.SqlQuery<SsoVinculoUser>(
                    @"SELECT ID_USUARIO AS IdUsuario, NOME_AUTENTICACAO AS NomeAutenticacao
FROM [LX_TCS].[TCS_USUARIO_AUTENTICACAO]
WHERE UID_USUARIO = @uid",
                    new SqlParameter("@uid", uidUsuario.Value)).FirstOrDefault();
            }
            if (string.IsNullOrWhiteSpace(userName))
                return null;
            return this.DbContext.Database.SqlQuery<SsoVinculoUser>(
                @"SELECT ID_USUARIO AS IdUsuario, NOME_AUTENTICACAO AS NomeAutenticacao
FROM [LX_TCS].[TCS_USUARIO_AUTENTICACAO]
WHERE UPPER(LTRIM(RTRIM(NOME_AUTENTICACAO))) = UPPER(LTRIM(RTRIM(@n)))",
                new SqlParameter("@n", userName.Trim())).FirstOrDefault();
        }

        private SsoVinculoRow LoadVinculoByUserId(long idUsuario)
        {
            return this.DbContext.Database.SqlQuery<SsoVinculoRow>(
                @"SELECT ID_USUARIO AS IdUsuario, NOME_AUTENTICACAO AS NomeAutenticacao,
AZURE_OID AS AzureOid, AZURE_UPN AS AzureUpn,
DATA_VINCULO AS DataVinculo, DATA_ULTIMO_LOGIN AS DataUltimoLogin
FROM [LX_TCS].[TCS_USUARIO_SSO_VINCULO]
WHERE ID_USUARIO = @id",
                new SqlParameter("@id", idUsuario)).FirstOrDefault();
        }

        private SsoVinculoRow LoadVinculoByOid(string azureOid)
        {
            return this.DbContext.Database.SqlQuery<SsoVinculoRow>(
                @"SELECT ID_USUARIO AS IdUsuario, NOME_AUTENTICACAO AS NomeAutenticacao,
AZURE_OID AS AzureOid, AZURE_UPN AS AzureUpn,
DATA_VINCULO AS DataVinculo, DATA_ULTIMO_LOGIN AS DataUltimoLogin
FROM [LX_TCS].[TCS_USUARIO_SSO_VINCULO]
WHERE UPPER(AZURE_OID) = UPPER(@oid)",
                new SqlParameter("@oid", azureOid)).FirstOrDefault();
        }

        private void InsertVinculo(SsoVinculoUser user, string azureOid, string azureUpn)
        {
            this.DbContext.Database.ExecuteSqlCommand(
                @"INSERT INTO [LX_TCS].[TCS_USUARIO_SSO_VINCULO]
([ID_USUARIO], [NOME_AUTENTICACAO], [AZURE_OID], [AZURE_UPN], [DATA_VINCULO], [DATA_ULTIMO_LOGIN])
VALUES (@id, @nome, @oid, @upn, GETDATE(), GETDATE())",
                new SqlParameter("@id", user.IdUsuario),
                new SqlParameter("@nome", user.NomeAutenticacao ?? string.Empty),
                new SqlParameter("@oid", azureOid),
                new SqlParameter("@upn", azureUpn ?? string.Empty));
        }

        private void UpdateVinculoLastLogin(SsoVinculoUser user, string azureOid, string azureUpn)
        {
            this.DbContext.Database.ExecuteSqlCommand(
                @"UPDATE [LX_TCS].[TCS_USUARIO_SSO_VINCULO]
SET [NOME_AUTENTICACAO] = @nome,
    [AZURE_OID] = @oid,
    [AZURE_UPN] = @upn,
    [DATA_ULTIMO_LOGIN] = GETDATE()
WHERE [ID_USUARIO] = @id",
                new SqlParameter("@id", user.IdUsuario),
                new SqlParameter("@nome", user.NomeAutenticacao ?? string.Empty),
                new SqlParameter("@oid", azureOid),
                new SqlParameter("@upn", azureUpn ?? string.Empty));
        }

        private static string ResolveRevokedBy(string revokedByUserName)
        {
            if (!string.IsNullOrWhiteSpace(revokedByUserName))
                return revokedByUserName.Trim();
            try
            {
                if (!string.IsNullOrWhiteSpace(LocalServiceBus.CurrentUserName))
                    return LocalServiceBus.CurrentUserName;
            }
            catch
            {
            }
            return "cadastro";
        }

        private static string NormalizeAzureOid(string azureOid)
        {
            if (string.IsNullOrWhiteSpace(azureOid))
                return null;
            return azureOid.Trim();
        }

        private static bool AzureOidEquals(string left, string right)
        {
            return string.Equals(left, right, StringComparison.OrdinalIgnoreCase);
        }

        private static string FormatVinculoDescricao(
            string step,
            string localLogin,
            string oid,
            string upn,
            SsoVinculoRow row,
            DateTime? previousLastLogin)
        {
            string last = row != null ? FormatTs(row.DataUltimoLogin) : "";
            string vinculo = row != null ? FormatTs(row.DataVinculo) : "";
            string prev = previousLastLogin.HasValue ? FormatTs(previousLastLogin.Value) : "";
            return step + ": local=" + localLogin
                + " azure_oid=" + oid
                + " azure_upn=" + upn
                + " data_vinculo=" + vinculo
                + (string.IsNullOrEmpty(prev) ? "" : " last_login_prev=" + prev)
                + " last_login=" + last;
        }

        private static string FormatMismatchDescricao(string localLogin, SsoVinculoRow expected, string receivedOid, string receivedUpn)
        {
            return "LINK: local=" + localLogin
                + " expected_oid=" + (expected != null ? expected.AzureOid : "")
                + " expected_upn=" + (expected != null ? expected.AzureUpn : "")
                + " received_oid=" + receivedOid
                + " received_upn=" + receivedUpn
                + " last_login=" + (expected != null ? FormatTs(expected.DataUltimoLogin) : "")
                + " mismatch";
        }

        private static string FormatTs(DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
