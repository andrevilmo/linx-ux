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
        // Align with ErrorConstants._UserLockedOut (ERRAUT020); keep Portuguese (no Translate).
        private static readonly ErrorInfo UserLockedOut = new ErrorInfo()
        {
            Code = "ERRAUT020",
            Message = "Usuário bloqueado por excesso de tentativas inválidas de senha. Solicite o desbloqueio ao administrador."
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
        [TIPO_EVENTO] CHAR(1) NOT NULL, -- S = success, F = failure, U = unlock, P = password change
        [NOME_USUARIO] NVARCHAR(256) NOT NULL,
        [ID_USUARIO] BIGINT NULL,
        [ID_LINX] INT NULL,
        [CODIGO_ERRO] NVARCHAR(20) NULL,
        [DESCRICAO] NVARCHAR(500) NULL,
        [QTD_TENTATIVAS] INT NOT NULL CONSTRAINT [DF_TCS_LOG_ACESSO_AUTH_QTD] DEFAULT ((0)),
        [ENDERECO_IP] NVARCHAR(64) NULL,
        [NOME_MAQUINA] NVARCHAR(128) NULL,
        [CANAL] NVARCHAR(50) NULL,
        [INDICA_CONTA_TENTATIVA] BIT NOT NULL CONSTRAINT [DF_TCS_LOG_ACESSO_AUTH_CONTA] DEFAULT ((0)),
        [INDICA_BLOQUEIO] BIT NOT NULL CONSTRAINT [DF_TCS_LOG_ACESSO_AUTH_BLOQ] DEFAULT ((0)),
        [INDICA_USUARIO_SERVICO] BIT NOT NULL CONSTRAINT [DF_TCS_LOG_ACESSO_AUTH_INDICA_USUARIO_SERVICO] DEFAULT ((0)),
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

        private const string AuthAccessColumnEnsureSql = @"
IF COL_LENGTH(N'LX_TCS.TCS_LOG_ACESSO_AUTH', N'INDICA_USUARIO_SERVICO') IS NULL
BEGIN
    ALTER TABLE [LX_TCS].[TCS_LOG_ACESSO_AUTH]
        ADD [INDICA_USUARIO_SERVICO] BIT NOT NULL
            CONSTRAINT [DF_TCS_LOG_ACESSO_AUTH_INDICA_USUARIO_SERVICO] DEFAULT ((0));
END
IF COL_LENGTH(N'LX_TCS.TCS_LOG_ACESSO_AUTH', N'ID_LINX') IS NULL
BEGIN
    ALTER TABLE [LX_TCS].[TCS_LOG_ACESSO_AUTH]
        ADD [ID_LINX] INT NULL;
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
        /// Returns how many invalid login attempts remain before lockout (0 = at or over the limit).
        /// </summary>
        public int GetRemainingAuthAttempts(string userName)
        {
            try
            {
                EnsureAuthAccessTable();
                string normalized = NormalizeUserName(userName);
                if (string.IsNullOrEmpty(normalized))
                    return AuthMaxInvalidAttempts;

                int current = GetCurrentAttemptCount(normalized);
                int remaining = AuthMaxInvalidAttempts - current;
                return remaining < 0 ? 0 : remaining;
            }
            catch
            {
                return AuthMaxInvalidAttempts;
            }
        }

        /// <summary>
        /// Builds the login failure message, appending remaining attempts for countable failures.
        /// </summary>
        public string FormatCountableAuthFailureMessage(string userName, string errorCode, string errorMessage)
        {
            string baseMessage = string.IsNullOrEmpty(errorCode)
                ? (errorMessage ?? string.Empty)
                : string.Format("{0} - {1}", errorCode, errorMessage);

            if (!IsCountableAuthFailure(errorCode))
                return baseMessage;

            int remaining = GetRemainingAuthAttempts(userName);
            if (remaining <= 0)
                return ErrorConstants.FormatUserLockedOutMessage();

            if (remaining == 1)
                return string.Format("{0}. Ainda falta 1 tentativa.", baseMessage);

            return string.Format("{0}. Ainda faltam {1} tentativas.", baseMessage, remaining);
        }

        /// <summary>
        /// True when TCS_LOG_ACESSO_AUTH has an active sliding-window lockout for the user
        /// (independent of ASP.NET Membership IsLockedOut).
        /// </summary>
        public bool IsAuthAccessLocked(string userName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                    return false;

                EnsureAuthAccessTable();
                string normalized = NormalizeUserName(userName);
                DateTime? lockTime = GetLastLockTimeAfterSuccess(normalized);
                if (!lockTime.HasValue)
                    return false;

                return DateTime.Now < lockTime.Value.AddMinutes(AuthAttemptWindowMinutes);
            }
            catch
            {
                return false;
            }
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
                    string description = ErrorConstants.FormatUserLockedOutMessage();
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
                if (ErrorConstants.IsMembershipLockoutMessage(ex.Message))
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
                    description = string.Format("{0} | {1}", description, ErrorConstants.FormatUserLockedOutMessage());

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
                    throw new Exception(ErrorConstants.FormatUserLockedOutMessage());
            }
            catch (Exception ex)
            {
                if (ErrorConstants.IsMembershipLockoutMessage(ex.Message))
                    throw new Exception(ErrorConstants.FormatUserLockedOutMessage());
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

        /// <summary>
        /// Logs self-service password change (tela Alteração de senha) to TCS_LOG_ACESSO_AUTH (TIPO_EVENTO = P).
        /// Does not reset the sliding-window lockout (only S/U do).
        /// </summary>
        /// <param name="userName">Authentication name of the user who changed their password.</param>
        /// <param name="canal">Optional channel override (default resolved from request; prefer "Alteração de senha").</param>
        public void LogAuthAccessPasswordChange(string userName, string canal = null)
        {
            try
            {
                EnsureAuthAccessTable();
                string normalized = NormalizeUserName(userName);
                if (string.IsNullOrEmpty(normalized))
                    return;

                InsertAuthAccessEvent(
                    tipoEvento: 'P',
                    userName: normalized,
                    idUsuario: ResolveUserId(normalized),
                    codigoErro: null,
                    descricao: "Alteração de senha pelo próprio usuário",
                    qtdTentativas: 0,
                    contaTentativa: false,
                    indicaBloqueio: false,
                    canal: string.IsNullOrEmpty(canal) ? "Alteração de senha" : canal);
            }
            catch
            {
                // Best-effort audit.
            }
        }

        /// <summary>
        /// Logs Membership unlock to TCS_LOG_ACESSO_AUTH (TIPO_EVENTO = U).
        /// NOME_USUARIO is the unlocked account; DESCRICAO records who performed the unlock.
        /// Treats unlock like success for sliding-window lockout reset.
        /// </summary>
        /// <param name="userName">Authentication name of the unlocked user.</param>
        /// <param name="unlockedByUserName">Who performed the unlock (admin or the user themselves).</param>
        /// <param name="canal">Optional channel override.</param>
        /// <param name="reason">Optional reason suffix, e.g. "redefinição de senha".</param>
        public void LogAuthAccessUnlock(string userName, string unlockedByUserName, string canal = null, string reason = null)
        {
            try
            {
                EnsureAuthAccessTable();
                string normalized = NormalizeUserName(userName);
                if (string.IsNullOrEmpty(normalized))
                    return;

                string by = NormalizeUserName(unlockedByUserName);
                if (string.IsNullOrEmpty(by))
                    by = normalized;

                string descricao = string.Format("Usuário desbloqueado por {0}", by);
                if (!string.IsNullOrWhiteSpace(reason))
                    descricao = string.Format("{0} ({1})", descricao, reason.Trim());

                if (descricao.Length > 500)
                    descricao = descricao.Substring(0, 500);

                InsertAuthAccessEvent(
                    tipoEvento: 'U',
                    userName: normalized,
                    idUsuario: ResolveUserId(normalized),
                    codigoErro: null,
                    descricao: descricao,
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
            this.DbContext.Database.ExecuteSqlCommand(AuthAccessColumnEnsureSql);
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

        /// <summary>
        /// Prefer the authenticated session IdLinx (LocalServiceBus / CurrentUser headers);
        /// fall back to the user's ID_LINX_GPECON when available.
        /// </summary>
        private int? ResolveIdLinx(string normalizedUserName)
        {
            try
            {
                if (LocalServiceBus.IdLinx > 0)
                    return LocalServiceBus.IdLinx;
            }
            catch
            {
            }

            try
            {
                return this.DbContext.TCS_USUARIO_AUTENTICACAO
                    .Where(u => u.NOME_AUTENTICACAO.ToUpper() == normalizedUserName)
                    .Select(u => (int?)u.ID_LINX_GPECON)
                    .FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        private bool ResolveIndicaUsuarioServico(string normalizedUserName)
        {
            try
            {
                return this.DbContext.TCS_USUARIO_AUTENTICACAO
                    .Where(u => u.NOME_AUTENTICACAO.ToUpper() == normalizedUserName)
                    .Select(u => (bool?)u.INDICA_USUARIO_SERVICO)
                    .FirstOrDefault() ?? false;
            }
            catch
            {
                return false;
            }
        }

        private DateTime? GetLastLockTimeAfterSuccess(string normalizedUserName)
        {
            // 'S' = login success, 'U' = unlock — both clear the active lockout window.
            const string sql = @"
SELECT TOP 1 L.DATA_HORA
FROM [LX_TCS].[TCS_LOG_ACESSO_AUTH] L
WHERE L.NOME_USUARIO = @user
  AND L.INDICA_BLOQUEIO = 1
  AND L.DATA_HORA > ISNULL((
        SELECT MAX(S.DATA_HORA)
        FROM [LX_TCS].[TCS_LOG_ACESSO_AUTH] S
        WHERE S.NOME_USUARIO = @user AND S.TIPO_EVENTO IN ('S', 'U')
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
        WHERE S.NOME_USUARIO = @user AND S.TIPO_EVENTO IN ('S', 'U')
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

            bool indicaUsuarioServico = ResolveIndicaUsuarioServico(userName);
            int? idLinx = ResolveIdLinx(userName);

            const string sql = @"
INSERT INTO [LX_TCS].[TCS_LOG_ACESSO_AUTH]
(
    [DATA_HORA], [TIPO_EVENTO], [NOME_USUARIO], [ID_USUARIO], [ID_LINX], [CODIGO_ERRO], [DESCRICAO],
    [QTD_TENTATIVAS], [ENDERECO_IP], [NOME_MAQUINA], [CANAL], [INDICA_CONTA_TENTATIVA], [INDICA_BLOQUEIO],
    [INDICA_USUARIO_SERVICO]
)
VALUES
(
    @dataHora, @tipo, @user, @idUsuario, @idLinx, @codigo, @descricao,
    @qtd, @ip, @machine, @canal, @conta, @bloqueio,
    @indicaUsuarioServico
)";

            this.DbContext.Database.ExecuteSqlCommand(sql,
                new SqlParameter("@dataHora", DateTime.Now),
                new SqlParameter("@tipo", tipoEvento.ToString()),
                new SqlParameter("@user", userName ?? string.Empty),
                new SqlParameter("@idUsuario", (object)idUsuario ?? DBNull.Value),
                new SqlParameter("@idLinx", (object)idLinx ?? DBNull.Value),
                new SqlParameter("@codigo", (object)codigoErro ?? DBNull.Value),
                new SqlParameter("@descricao", (object)descricao ?? DBNull.Value),
                new SqlParameter("@qtd", qtdTentativas),
                new SqlParameter("@ip", (object)ip ?? DBNull.Value),
                new SqlParameter("@machine", (object)machine ?? DBNull.Value),
                new SqlParameter("@canal", (object)resolvedCanal ?? DBNull.Value),
                new SqlParameter("@conta", contaTentativa),
                new SqlParameter("@bloqueio", indicaBloqueio),
                new SqlParameter("@indicaUsuarioServico", indicaUsuarioServico));
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
