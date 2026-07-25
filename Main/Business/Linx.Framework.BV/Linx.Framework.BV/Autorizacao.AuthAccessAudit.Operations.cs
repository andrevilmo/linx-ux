using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Linx;
using Linx.Data;
using Linx.Tools;

namespace Linx.Framework.BV.Autorizacao
{
    ////////////////////////////////////////////////////////////////////////////
    ////////////////// Auth access audit + sliding-window lockout //////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class AutorizacaoDomainService
    {
        // Local until Linx.Tools.dll (GAC/install) is rebuilt with ErrorConstants._UserLockedOut
        private static readonly ErrorInfo UserLockedOut = new ErrorInfo()
        {
            Code = "ERRAUT021",
            Message = "Usuario bloqueado por excesso de tentativas de login."
        };

        private const string AuthAccessSchemaEnsureSql = @"
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = N'LX_TCS')
    EXEC(N'CREATE SCHEMA [LX_TCS]');";

        private const string AuthAccessTableEnsureSql = @"
IF NOT EXISTS (
    SELECT 1 FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE s.name = N'LX_TCS' AND t.name = N'TCS_LOG_ACESSO_AUTH')
BEGIN
    CREATE TABLE [LX_TCS].[TCS_LOG_ACESSO_AUTH]
    (
        [ID_TCS_LOG_ACESSO_AUTH] INT IDENTITY(1,1) NOT NULL,
        [DATA_HORA] DATETIME NOT NULL,
        [TIPO_EVENTO] CHAR(1) NOT NULL,
        [NOME_USUARIO] NVARCHAR(256) NOT NULL,
        [ID_USUARIO] BIGINT NULL,
        [CODIGO_ERRO] NVARCHAR(20) NULL,
        [DESCRICAO] NVARCHAR(500) NULL,
        [QTD_TENTATIVAS] INT NOT NULL CONSTRAINT [DF_TCS_LOG_ACESSO_AUTH_QTD] DEFAULT ((0)),
        [ENDERECO_IP] NVARCHAR(64) NULL,
        [NOME_MAQUINA] NVARCHAR(128) NULL,
        [CANAL] NVARCHAR(50) NULL,
        [INDICA_CONTA_TENTATIVA] BIT NOT NULL CONSTRAINT [DF_TCS_LOG_ACESSO_AUTH_CONTA] DEFAULT ((0)),
        [INDICA_BLOQUEIO] BIT NOT NULL CONSTRAINT [DF_TCS_LOG_ACESSO_AUTH_BLOQ] DEFAULT ((0)),
        CONSTRAINT [XPK_TCS_LOG_ACESSO_AUTH] PRIMARY KEY CLUSTERED ([ID_TCS_LOG_ACESSO_AUTH] ASC)
    );
END
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_TCS_LOG_ACESSO_AUTH_USUARIO_DATA'
      AND object_id = OBJECT_ID(N'LX_TCS.TCS_LOG_ACESSO_AUTH'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_TCS_LOG_ACESSO_AUTH_USUARIO_DATA]
        ON [LX_TCS].[TCS_LOG_ACESSO_AUTH] ([NOME_USUARIO], [DATA_HORA] DESC);
END";

        private static bool _authAccessTableEnsured;

        private static int AuthMaxInvalidAttempts
        {
            get
            {
                int value;
                if (int.TryParse(ConfigurationManager.AppSettings["AuthAccess.MaxInvalidAttempts"], out value) && value > 0)
                    return value;
                return 5;
            }
        }

        private static int AuthAttemptWindowMinutes
        {
            get
            {
                int value;
                if (int.TryParse(ConfigurationManager.AppSettings["AuthAccess.AttemptWindowMinutes"], out value) && value > 0)
                    return value;
                return 15;
            }
        }

        private static bool IsCountableAuthFailure(string errorCode)
        {
            return string.Equals(errorCode, ErrorConstants._UserNotFound.Code, StringComparison.OrdinalIgnoreCase)
                || string.Equals(errorCode, ErrorConstants._UserBadNameOrPassword.Code, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Throws ERRAUT021 when the user is inside an active lockout window.
        /// </summary>
        public void EnsureUserNotLocked(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return;

            try
            {
                EnsureAuthAccessTable();
                string normalized = NormalizeUserName(userName);
                DateTime? lockTime = GetLastLockTimeAfterSuccess(normalized);
                if (!lockTime.HasValue)
                    return;

                if (DateTime.Now < lockTime.Value.AddMinutes(AuthAttemptWindowMinutes))
                {
                    string description = string.Format("{0} - {1}", UserLockedOut.Code, UserLockedOut.Message);
                    InsertAuthAccessEvent(
                        tipoEvento: 'F',
                        userName: normalized,
                        idUsuario: ResolveUserId(normalized),
                        codigoErro: UserLockedOut.Code,
                        descricao: description,
                        qtdTentativas: GetCurrentAttemptCount(normalized),
                        contaTentativa: false,
                        indicaBloqueio: true);

                    throw new Exception(description);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != null && ex.Message.StartsWith("ERRAUT021", StringComparison.OrdinalIgnoreCase))
                    throw;
                // Best-effort: never block auth solely because audit/lock storage failed.
            }
        }

        public void LogAuthAccessFailure(string userName, ErrorInfo error, string canal = null)
        {
            LogAuthAccessFailure(userName, error.Code, error.Message, canal, null);
        }

        public void LogAuthAccessFailure(string userName, string errorCode, string errorMessage, string canal = null, bool? countsTowardLockout = null)
        {
            try
            {
                EnsureAuthAccessTable();
                string normalized = NormalizeUserName(userName);
                if (string.IsNullOrEmpty(normalized))
                    normalized = "(unknown)";

                bool counts = countsTowardLockout.HasValue
                    ? countsTowardLockout.Value
                    : IsCountableAuthFailure(errorCode);
                int attemptCount = 0;
                bool locked = false;

                if (counts)
                {
                    attemptCount = GetCurrentAttemptCount(normalized) + 1;
                    if (attemptCount >= AuthMaxInvalidAttempts)
                        locked = true;
                }
                else
                {
                    attemptCount = GetCurrentAttemptCount(normalized);
                }

                string description = string.IsNullOrEmpty(errorCode)
                    ? (errorMessage ?? string.Empty)
                    : string.Format("{0} - {1}", errorCode, errorMessage);

                if (locked)
                    description = string.Format("{0} | {1} - {2}", description, UserLockedOut.Code, UserLockedOut.Message);

                InsertAuthAccessEvent(
                    tipoEvento: 'F',
                    userName: normalized,
                    idUsuario: ResolveUserId(normalized),
                    codigoErro: errorCode,
                    descricao: Truncate(description, 500),
                    qtdTentativas: attemptCount,
                    contaTentativa: counts,
                    indicaBloqueio: locked,
                    canal: canal);

                if (locked)
                    throw new Exception(string.Format("{0} - {1}", UserLockedOut.Code, UserLockedOut.Message));
            }
            catch (Exception ex)
            {
                if (ex.Message != null && ex.Message.StartsWith("ERRAUT021", StringComparison.OrdinalIgnoreCase))
                    throw;
                // Best-effort audit.
            }
        }

        public void LogAuthAccessSuccess(string userName, string canal = null)
        {
            try
            {
                EnsureAuthAccessTable();
                string normalized = NormalizeUserName(userName);
                if (string.IsNullOrEmpty(normalized))
                    return;

                InsertAuthAccessEvent(
                    tipoEvento: 'S',
                    userName: normalized,
                    idUsuario: ResolveUserId(normalized),
                    codigoErro: null,
                    descricao: "Login efetuado",
                    qtdTentativas: 0,
                    contaTentativa: false,
                    indicaBloqueio: false,
                    canal: canal);
            }
            catch
            {
                // Best-effort audit.
            }
        }

        private void EnsureAuthAccessTable()
        {
            if (_authAccessTableEnsured)
                return;

            this.DbContext.Database.ExecuteSqlCommand(AuthAccessSchemaEnsureSql);
            this.DbContext.Database.ExecuteSqlCommand(AuthAccessTableEnsureSql);
            _authAccessTableEnsured = true;
        }

        private static string NormalizeUserName(string userName)
        {
            return (userName ?? string.Empty).Trim().ToUpperInvariant();
        }

        private long? ResolveUserId(string normalizedUserName)
        {
            try
            {
                return this.DbContext.TCS_USUARIO_AUTENTICACAO
                    .Where(u => u.NOME_AUTENTICACAO.ToUpper() == normalizedUserName)
                    .Select(u => (long?)u.ID_USUARIO)
                    .FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        private DateTime? GetLastLockTimeAfterSuccess(string normalizedUserName)
        {
            const string sql = @"
SELECT TOP 1 L.DATA_HORA
FROM [LX_TCS].[TCS_LOG_ACESSO_AUTH] L
WHERE L.NOME_USUARIO = @user
  AND L.INDICA_BLOQUEIO = 1
  AND L.DATA_HORA > ISNULL((
        SELECT MAX(S.DATA_HORA)
        FROM [LX_TCS].[TCS_LOG_ACESSO_AUTH] S
        WHERE S.NOME_USUARIO = @user AND S.TIPO_EVENTO = 'S'
      ), '19000101')
ORDER BY L.DATA_HORA DESC";

            var rows = this.DbContext.Database.SqlQuery<DateTime>(sql,
                new SqlParameter("@user", normalizedUserName)).ToList();
            if (rows == null || rows.Count == 0)
                return null;
            return rows[0];
        }

        private int GetCurrentAttemptCount(string normalizedUserName)
        {
            DateTime windowStart = DateTime.Now.AddMinutes(-AuthAttemptWindowMinutes);

            const string sql = @"
SELECT COUNT(1)
FROM [LX_TCS].[TCS_LOG_ACESSO_AUTH] F
WHERE F.NOME_USUARIO = @user
  AND F.TIPO_EVENTO = 'F'
  AND F.INDICA_CONTA_TENTATIVA = 1
  AND F.DATA_HORA >= @windowStart
  AND F.DATA_HORA > ISNULL((
        SELECT MAX(S.DATA_HORA)
        FROM [LX_TCS].[TCS_LOG_ACESSO_AUTH] S
        WHERE S.NOME_USUARIO = @user AND S.TIPO_EVENTO = 'S'
      ), '19000101')";

            return this.DbContext.Database.SqlQuery<int>(sql,
                new SqlParameter("@user", normalizedUserName),
                new SqlParameter("@windowStart", windowStart)).FirstOrDefault();
        }

        private void InsertAuthAccessEvent(
            char tipoEvento,
            string userName,
            long? idUsuario,
            string codigoErro,
            string descricao,
            int qtdTentativas,
            bool contaTentativa,
            bool indicaBloqueio,
            string canal = null)
        {
            string ip;
            string machine;
            string resolvedCanal;
            ResolveRequestContext(out ip, out machine, out resolvedCanal);
            if (!string.IsNullOrEmpty(canal))
                resolvedCanal = canal;

            const string sql = @"
INSERT INTO [LX_TCS].[TCS_LOG_ACESSO_AUTH]
(
    [DATA_HORA], [TIPO_EVENTO], [NOME_USUARIO], [ID_USUARIO], [CODIGO_ERRO], [DESCRICAO],
    [QTD_TENTATIVAS], [ENDERECO_IP], [NOME_MAQUINA], [CANAL], [INDICA_CONTA_TENTATIVA], [INDICA_BLOQUEIO]
)
VALUES
(
    @dataHora, @tipo, @user, @idUsuario, @codigo, @descricao,
    @qtd, @ip, @machine, @canal, @conta, @bloqueio
)";

            this.DbContext.Database.ExecuteSqlCommand(sql,
                new SqlParameter("@dataHora", DateTime.Now),
                new SqlParameter("@tipo", tipoEvento.ToString()),
                new SqlParameter("@user", userName ?? string.Empty),
                new SqlParameter("@idUsuario", (object)idUsuario ?? DBNull.Value),
                new SqlParameter("@codigo", (object)codigoErro ?? DBNull.Value),
                new SqlParameter("@descricao", (object)descricao ?? DBNull.Value),
                new SqlParameter("@qtd", qtdTentativas),
                new SqlParameter("@ip", (object)ip ?? DBNull.Value),
                new SqlParameter("@machine", (object)machine ?? DBNull.Value),
                new SqlParameter("@canal", (object)resolvedCanal ?? DBNull.Value),
                new SqlParameter("@conta", contaTentativa),
                new SqlParameter("@bloqueio", indicaBloqueio));
        }

        private static void ResolveRequestContext(out string ip, out string machine, out string canal)
        {
            ip = null;
            machine = null;
            canal = "Service";

            try
            {
                var http = HttpContext.Current;
                if (http == null || http.Request == null)
                    return;

                var request = http.Request;
                ip = FirstNonEmpty(
                    request.Headers["X-Client-IP"],
                    request.Headers["X-Forwarded-For"],
                    request.UserHostAddress);

                if (!string.IsNullOrEmpty(ip) && ip.Contains(","))
                    ip = ip.Split(',')[0].Trim();

                machine = FirstNonEmpty(request.Headers["X-Client-Machine"]);
                canal = FirstNonEmpty(request.Headers["X-Auth-Channel"], "Service");
            }
            catch
            {
            }
        }

        private static string FirstNonEmpty(params string[] values)
        {
            if (values == null)
                return null;
            foreach (var value in values)
            {
                if (!string.IsNullOrWhiteSpace(value))
                    return value.Trim();
            }
            return null;
        }

        private static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
                return value;
            return value.Substring(0, maxLength);
        }
    }
}
