using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Linx.Security;

namespace Linx.Framework.BV.Autorizacao
{
    public class MfaStatusResult
    {
        public string TableOrigin { get; set; }
        public int IdGpecon { get; set; }
        public long IdUserMfa { get; set; }
        public Guid? UidUsuario { get; set; }
        public string NomeAutenticacao { get; set; }
        public string NomeEmpresa { get; set; }
        public bool CompanyMfaEnabled { get; set; }
        public bool UserUtilizaMfa { get; set; }
        public bool UserUtilizaSso { get; set; }
        public bool IndicaUsuarioServico { get; set; }
        public bool AutenticacaoWindows { get; set; }
        public bool Enrolled { get; set; }
        public bool CanRevoke { get; set; }
        public bool RequiresMfa { get; set; }
        public bool MfaLocked { get; set; }
        public string SkipReason { get; set; }
        public bool RememberDeviceEnabled { get; set; }
        public int RememberDeviceDays { get; set; }
    }

    public class MfaEnrollResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string OtpauthUri { get; set; }
        public string AccountLabel { get; set; }
    }

    public class MfaValidateResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Ticket { get; set; }
        public DateTime? TicketExpiresUtc { get; set; }
        public bool MfaLocked { get; set; }
    }

    public class MfaCompanyPolicy
    {
        public int IdGpecon { get; set; }
        public bool IndicaMfaHabilitado { get; set; }
        public bool IndicaDispositivoConfiavel { get; set; }
        public int QtdDiasConfianca { get; set; }
        public bool RowExists { get; set; }
    }

    public class MfaDeviceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string DeviceToken { get; set; }
        public DateTime? ExpiresUtc { get; set; }
    }

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// MFA TOTP (UX / PDV / CLIENTE_CONNECT) //////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class AutorizacaoDomainService
    {
        public const string MfaOriginUx = "UX";
        public const string MfaOriginPdv = "PDV";
        public const string MfaOriginClienteConnect = "CLIENTE_CONNECT";

        private const int MfaTotpMaxAttempts = 5;
        private const int MfaLockMinutes = 15;
        private const int MfaTicketMinutes = 10;
        private static bool _mfaTablesEnsured;
        private static bool _mfaTablesEnsureAttempted;

        private const string MfaEnsureSql = @"
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = N'LX_TCS')
    EXEC(N'CREATE SCHEMA [LX_TCS]');
IF COL_LENGTH(N'LX_TCS.TCS_USUARIO_AUTENTICACAO', N'INDICA_UTILIZA_SSO') IS NULL
    ALTER TABLE [LX_TCS].[TCS_USUARIO_AUTENTICACAO] ADD [INDICA_UTILIZA_SSO] BIT NOT NULL CONSTRAINT [DF_TCS_USUARIO_AUT_SSO] DEFAULT ((0));
IF COL_LENGTH(N'LX_TCS.TCS_USUARIO_AUTENTICACAO', N'INDICA_UTILIZA_MFA') IS NULL
    ALTER TABLE [LX_TCS].[TCS_USUARIO_AUTENTICACAO] ADD [INDICA_UTILIZA_MFA] BIT NULL;
IF NOT EXISTS (SELECT 1 FROM sys.tables t INNER JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE s.name = N'LX_TCS' AND t.name = N'TCS_GPECON_MFA')
BEGIN
    CREATE TABLE [LX_TCS].[TCS_GPECON_MFA] (
        [ID_GPCON] INT NOT NULL,
        [INDICA_MFA_HABILITADO] BIT NOT NULL CONSTRAINT [DF_TCS_GPECON_MFA_HAB] DEFAULT ((1)),
        [INDICA_DISPOSITIVO_CONFIAVEL] BIT NOT NULL CONSTRAINT [DF_TCS_GPECON_MFA_DEV] DEFAULT ((0)),
        [QTD_DIAS_CONFIANCA] INT NOT NULL CONSTRAINT [DF_TCS_GPECON_MFA_DIAS] DEFAULT ((0)),
        [CREATED_AT] DATETIME NOT NULL CONSTRAINT [DF_TCS_GPECON_MFA_CRT] DEFAULT (GETDATE()),
        [UPDATED_AT] DATETIME NOT NULL CONSTRAINT [DF_TCS_GPECON_MFA_UPD] DEFAULT (GETDATE()),
        CONSTRAINT [XPK_TCS_GPECON_MFA] PRIMARY KEY CLUSTERED ([ID_GPCON] ASC));
END
IF NOT EXISTS (SELECT 1 FROM sys.tables t INNER JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE s.name = N'LX_TCS' AND t.name = N'TCS_USUARIO_MFA')
BEGIN
    CREATE TABLE [LX_TCS].[TCS_USUARIO_MFA] (
        [TABLE_ORIGIN] VARCHAR(32) NOT NULL,
        [ID_GPCON] INT NOT NULL,
        [ID_USER_MFA] BIGINT NOT NULL,
        [ATIVO] BIT NOT NULL CONSTRAINT [DF_TCS_USUARIO_MFA_ATIVO] DEFAULT ((0)),
        [ACCESS_SECRET] NVARCHAR(512) NULL,
        [QTD_TENTATIVAS_TOTP] INT NOT NULL CONSTRAINT [DF_TCS_USUARIO_MFA_QTD] DEFAULT ((0)),
        [DATA_BLOQUEIO_ATE] DATETIME NULL,
        [CREATED_AT] DATETIME NOT NULL CONSTRAINT [DF_TCS_USUARIO_MFA_CRT] DEFAULT (GETDATE()),
        [UPDATED_AT] DATETIME NOT NULL CONSTRAINT [DF_TCS_USUARIO_MFA_UPD] DEFAULT (GETDATE()),
        CONSTRAINT [XPK_TCS_USUARIO_MFA] PRIMARY KEY CLUSTERED ([TABLE_ORIGIN] ASC, [ID_GPCON] ASC, [ID_USER_MFA] ASC));
END
IF NOT EXISTS (SELECT 1 FROM sys.tables t INNER JOIN sys.schemas s ON t.schema_id = s.schema_id WHERE s.name = N'LX_TCS' AND t.name = N'TCS_USUARIO_MFA_DISPOSITIVO')
BEGIN
    CREATE TABLE [LX_TCS].[TCS_USUARIO_MFA_DISPOSITIVO] (
        [ID_DISPOSITIVO] BIGINT IDENTITY(1,1) NOT NULL,
        [TABLE_ORIGIN] VARCHAR(32) NOT NULL,
        [ID_GPCON] INT NOT NULL,
        [ID_USER_MFA] BIGINT NOT NULL,
        [TOKEN_HASH] NVARCHAR(128) NOT NULL,
        [USER_AGENT] NVARCHAR(256) NULL,
        [DATA_EXPIRACAO] DATETIME NOT NULL,
        [CREATED_AT] DATETIME NOT NULL CONSTRAINT [DF_TCS_USUARIO_MFA_DEV_CRT] DEFAULT (GETDATE()),
        [UPDATED_AT] DATETIME NOT NULL CONSTRAINT [DF_TCS_USUARIO_MFA_DEV_UPD] DEFAULT (GETDATE()),
        CONSTRAINT [XPK_TCS_USUARIO_MFA_DISPOSITIVO] PRIMARY KEY CLUSTERED ([ID_DISPOSITIVO] ASC));
    CREATE NONCLUSTERED INDEX [IX_TCS_USUARIO_MFA_DISPOSITIVO_KEY]
        ON [LX_TCS].[TCS_USUARIO_MFA_DISPOSITIVO] ([TABLE_ORIGIN], [ID_GPCON], [ID_USER_MFA], [TOKEN_HASH]);
END";

        public static string NormalizeMfaOrigin(string tableOrigin)
        {
            if (string.IsNullOrWhiteSpace(tableOrigin))
                return MfaOriginUx;
            string raw = tableOrigin.Trim().ToUpperInvariant();
            if (raw == "TCS_USUARIO_AUTENTICACAO" || raw == "UX")
                return MfaOriginUx;
            if (raw == "LJV_VENDEDOR" || raw == "PDV")
                return MfaOriginPdv;
            if (raw == "CLIENTE_CONNECT")
                return MfaOriginClienteConnect;
            throw new Exception("tableOrigin inválido. Use UX, PDV ou CLIENTE_CONNECT.");
        }

        private void EnsureMfaTables()
        {
            if (_mfaTablesEnsured || _mfaTablesEnsureAttempted)
                return;
            _mfaTablesEnsureAttempted = true;
            try
            {
                this.DbContext.Database.ExecuteSqlCommand(MfaEnsureSql);
                _mfaTablesEnsured = true;
            }
            catch
            {
                // SQL login may lack ALTER. SELECT paths still run;
                // missing objects surface as a visible Portal error instead of a silent loop.
            }
        }

        public MfaStatusResult GetMfaStatus(string tableOrigin, int idGpecon, long idUserMfa, Guid? uidUsuario)
        {
            EnsureMfaTables();
            string origin = NormalizeMfaOrigin(tableOrigin);
            MfaStatusResult result = new MfaStatusResult
            {
                TableOrigin = origin,
                IdGpecon = idGpecon,
                IdUserMfa = idUserMfa,
                UidUsuario = uidUsuario,
                UserUtilizaMfa = true,
                CompanyMfaEnabled = true
            };

            LoadUxUser(origin, uidUsuario, result);
            if (result.IdUserMfa <= 0)
                result.IdUserMfa = idUserMfa;
            if (result.IdGpecon <= 0)
                result.IdGpecon = idGpecon;

            MfaCompanyPolicy policy = GetMfaCompanyPolicy(result.IdGpecon);
            result.CompanyMfaEnabled = policy.IndicaMfaHabilitado;
            result.RememberDeviceEnabled = policy.IndicaDispositivoConfiavel;
            result.RememberDeviceDays = policy.QtdDiasConfianca;

            bool enrolled = false;
            bool locked = false;
            if (result.IdUserMfa > 0)
            {
                using (SqlConnection conn = CreateMfaConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT ATIVO, ACCESS_SECRET, DATA_BLOQUEIO_ATE
FROM [LX_TCS].[TCS_USUARIO_MFA]
WHERE TABLE_ORIGIN = @o AND ID_GPCON = @g AND ID_USER_MFA = @u";
                    cmd.Parameters.AddWithValue("@o", origin);
                    cmd.Parameters.AddWithValue("@g", result.IdGpecon);
                    cmd.Parameters.AddWithValue("@u", result.IdUserMfa);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool ativo = !reader.IsDBNull(0) && Convert.ToBoolean(reader.GetValue(0));
                            string secret = reader.IsDBNull(1) ? null : reader.GetString(1);
                            DateTime? lockUntil = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2);
                            enrolled = ativo && !string.IsNullOrWhiteSpace(secret);
                            locked = lockUntil.HasValue && lockUntil.Value > DateTime.Now;
                        }
                    }
                }
            }

            result.Enrolled = enrolled;
            result.CanRevoke = enrolled;
            result.MfaLocked = locked;

            if (result.IndicaUsuarioServico)
            {
                result.RequiresMfa = false;
                result.SkipReason = "INDICA_USUARIO_SERVICO";
            }
            else if (result.AutenticacaoWindows)
            {
                result.RequiresMfa = false;
                result.SkipReason = "AUTENTICACAO_WINDOWS";
            }
            else if (!result.CompanyMfaEnabled)
            {
                result.RequiresMfa = false;
                result.SkipReason = "COMPANY_MFA_OFF";
            }
            else if (!result.UserUtilizaMfa)
            {
                result.RequiresMfa = false;
                result.SkipReason = "USER_MFA_OFF";
            }
            else
            {
                result.RequiresMfa = true;
                result.SkipReason = null;
            }

            return result;
        }

        public MfaEnrollResult BeginMfaEnrollment(string tableOrigin, int idGpecon, long idUserMfa, Guid? uidUsuario)
        {
            MfaStatusResult status = GetMfaStatus(tableOrigin, idGpecon, idUserMfa, uidUsuario);
            if (!status.RequiresMfa)
                return new MfaEnrollResult { Success = false, Message = status.SkipReason ?? "MFA não é exigido." };
            if (status.IdUserMfa <= 0)
                return new MfaEnrollResult { Success = false, Message = "idUserMfa é obrigatório." };
            if (status.MfaLocked)
                return new MfaEnrollResult { Success = false, Message = "MFA bloqueado por excesso de tentativas." };
            if (status.Enrolled)
                return new MfaEnrollResult { Success = false, Message = "MFA já cadastrado. Informe o código do autenticador." };

            string rawSecret = null;
            string existingEncrypted = GetMfaAccessSecret(status.TableOrigin, status.IdGpecon, status.IdUserMfa);
            if (!string.IsNullOrWhiteSpace(existingEncrypted))
            {
                try
                {
                    rawSecret = DecryptMfaSecret(existingEncrypted);
                }
                catch
                {
                    rawSecret = null;
                }
            }

            if (string.IsNullOrWhiteSpace(rawSecret))
            {
                rawSecret = MfaTotp.GenerateSecret();
                string encrypted = EncryptMfaSecret(rawSecret);
                UpsertMfaSecret(status.TableOrigin, status.IdGpecon, status.IdUserMfa, encrypted, false);
                LogMfaAudit(status.NomeAutenticacao, "E", "Início de cadastro MFA", "MFA", status.IdUserMfa, status.IdGpecon);
            }

            string company = string.IsNullOrWhiteSpace(status.NomeEmpresa) ? "Linx" : status.NomeEmpresa;
            string login = string.IsNullOrWhiteSpace(status.NomeAutenticacao) ? status.IdUserMfa.ToString(CultureInfo.InvariantCulture) : status.NomeAutenticacao;
            string label = Uri.EscapeDataString(company + ":" + login);
            string issuer = Uri.EscapeDataString(company);
            string uri = string.Format("otpauth://totp/{0}?secret={1}&issuer={2}&digits=6&period=30", label, rawSecret, issuer);
            return new MfaEnrollResult { Success = true, OtpauthUri = uri, AccountLabel = company + " + " + login };
        }

        public MfaValidateResult ConfirmMfaEnrollment(string tableOrigin, int idGpecon, long idUserMfa, Guid? uidUsuario, string code)
        {
            return FinishTotp(tableOrigin, idGpecon, idUserMfa, uidUsuario, code, true, "MFA");
        }

        public MfaValidateResult ValidateMfaCode(string tableOrigin, int idGpecon, long idUserMfa, Guid? uidUsuario, string code, string canal)
        {
            return FinishTotp(tableOrigin, idGpecon, idUserMfa, uidUsuario, code, false, string.IsNullOrEmpty(canal) ? "MFA" : canal);
        }

        public MfaValidateResult RevokeMfaSecret(string tableOrigin, int idGpecon, long idUserMfa, Guid? uidUsuario)
        {
            MfaStatusResult status = GetMfaStatus(tableOrigin, idGpecon, idUserMfa, uidUsuario);
            if (status.IdUserMfa <= 0)
                return new MfaValidateResult { Success = false, Message = "Usuário MFA não informado." };

            this.DbContext.Database.ExecuteSqlCommand(
                @"UPDATE [LX_TCS].[TCS_USUARIO_MFA]
SET ATIVO = 0, ACCESS_SECRET = NULL, QTD_TENTATIVAS_TOTP = 0, DATA_BLOQUEIO_ATE = NULL, UPDATED_AT = GETDATE()
WHERE TABLE_ORIGIN = @o AND ID_GPCON = @g AND ID_USER_MFA = @u",
                new SqlParameter("@o", status.TableOrigin),
                new SqlParameter("@g", status.IdGpecon),
                new SqlParameter("@u", status.IdUserMfa));

            this.DbContext.Database.ExecuteSqlCommand(
                @"DELETE FROM [LX_TCS].[TCS_USUARIO_MFA_DISPOSITIVO]
WHERE TABLE_ORIGIN = @o AND ID_GPCON = @g AND ID_USER_MFA = @u",
                new SqlParameter("@o", status.TableOrigin),
                new SqlParameter("@g", status.IdGpecon),
                new SqlParameter("@u", status.IdUserMfa));

            LogMfaAudit(status.NomeAutenticacao, "R", "Revogação de ACCESS_SECRET MFA", "MFA-Admin", status.IdUserMfa, status.IdGpecon);
            return new MfaValidateResult { Success = true, Message = "MFA revogado. O usuário cadastrará um novo QR no próximo acesso." };
        }

        public MfaCompanyPolicy GetMfaCompanyPolicy(int idGpecon)
        {
            EnsureMfaTables();
            MfaCompanyPolicy policy = new MfaCompanyPolicy
            {
                IdGpecon = idGpecon,
                IndicaMfaHabilitado = true,
                IndicaDispositivoConfiavel = false,
                QtdDiasConfianca = 0,
                RowExists = false
            };

            using (SqlConnection conn = CreateMfaConnection())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT INDICA_MFA_HABILITADO, INDICA_DISPOSITIVO_CONFIAVEL, QTD_DIAS_CONFIANCA
FROM [LX_TCS].[TCS_GPECON_MFA] WHERE ID_GPCON = @g";
                    cmd.Parameters.AddWithValue("@g", idGpecon);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            policy.RowExists = true;
                            policy.IndicaMfaHabilitado = Convert.ToBoolean(reader.GetValue(0));
                            policy.IndicaDispositivoConfiavel = Convert.ToBoolean(reader.GetValue(1));
                            policy.QtdDiasConfianca = Convert.ToInt32(reader.GetValue(2));
                        }
                    }
                }
            }
            return policy;
        }

        public MfaCompanyPolicy SetMfaCompanyPolicy(int idGpecon, bool indicaMfaHabilitado, bool indicaDispositivoConfiavel, int qtdDiasConfianca)
        {
            EnsureMfaTables();
            this.DbContext.Database.ExecuteSqlCommand(
                @"IF EXISTS (SELECT 1 FROM [LX_TCS].[TCS_GPECON_MFA] WHERE ID_GPCON = @g)
    UPDATE [LX_TCS].[TCS_GPECON_MFA]
    SET INDICA_MFA_HABILITADO = @h, INDICA_DISPOSITIVO_CONFIAVEL = @d, QTD_DIAS_CONFIANCA = @n, UPDATED_AT = GETDATE()
    WHERE ID_GPCON = @g
ELSE
    INSERT INTO [LX_TCS].[TCS_GPECON_MFA] (ID_GPCON, INDICA_MFA_HABILITADO, INDICA_DISPOSITIVO_CONFIAVEL, QTD_DIAS_CONFIANCA, CREATED_AT, UPDATED_AT)
    VALUES (@g, @h, @d, @n, GETDATE(), GETDATE())",
                new SqlParameter("@g", idGpecon),
                new SqlParameter("@h", indicaMfaHabilitado),
                new SqlParameter("@d", indicaDispositivoConfiavel),
                new SqlParameter("@n", qtdDiasConfianca < 0 ? 0 : qtdDiasConfianca));

            LogMfaAudit(null, "R", string.Format("Política MFA GPECON {0}: habilitado={1}", idGpecon, indicaMfaHabilitado), "MFA-Admin", null, idGpecon);
            return GetMfaCompanyPolicy(idGpecon);
        }

        public MfaStatusResult SetUserMfaFlags(Guid uidUsuario, bool? utilizaSso, bool? utilizaMfa)
        {
            EnsureMfaTables();
            if (utilizaSso.HasValue)
            {
                this.DbContext.Database.ExecuteSqlCommand(
                    @"UPDATE [LX_TCS].[TCS_USUARIO_AUTENTICACAO] SET INDICA_UTILIZA_SSO = @s WHERE UID_USUARIO = @u",
                    new SqlParameter("@s", utilizaSso.Value),
                    new SqlParameter("@u", uidUsuario));
            }
            if (utilizaMfa.HasValue)
            {
                this.DbContext.Database.ExecuteSqlCommand(
                    @"UPDATE [LX_TCS].[TCS_USUARIO_AUTENTICACAO] SET INDICA_UTILIZA_MFA = @m WHERE UID_USUARIO = @u",
                    new SqlParameter("@m", utilizaMfa.Value),
                    new SqlParameter("@u", uidUsuario));
            }
            return GetMfaStatus(MfaOriginUx, 0, 0, uidUsuario);
        }

        public MfaDeviceResult LinkMfaDevice(string tableOrigin, int idGpecon, long idUserMfa, Guid? uidUsuario, string userAgent)
        {
            MfaStatusResult status = GetMfaStatus(tableOrigin, idGpecon, idUserMfa, uidUsuario);
            if (!status.RequiresMfa || !status.Enrolled)
                return new MfaDeviceResult { Success = false, Message = "Dispositivo confiável só após MFA ativo." };
            if (!status.RememberDeviceEnabled || status.RememberDeviceDays <= 0)
                return new MfaDeviceResult { Success = false, Message = "Dispositivo confiável desligado para esta empresa." };

            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            string hash = Sha256Hex(token);
            DateTime expires = DateTime.Now.AddDays(status.RememberDeviceDays);
            this.DbContext.Database.ExecuteSqlCommand(
                @"INSERT INTO [LX_TCS].[TCS_USUARIO_MFA_DISPOSITIVO]
(TABLE_ORIGIN, ID_GPCON, ID_USER_MFA, TOKEN_HASH, USER_AGENT, DATA_EXPIRACAO, CREATED_AT, UPDATED_AT)
VALUES (@o, @g, @u, @h, @a, @e, GETDATE(), GETDATE())",
                new SqlParameter("@o", status.TableOrigin),
                new SqlParameter("@g", status.IdGpecon),
                new SqlParameter("@u", status.IdUserMfa),
                new SqlParameter("@h", hash),
                new SqlParameter("@a", (object)userAgent ?? DBNull.Value),
                new SqlParameter("@e", expires));

            LogMfaAudit(status.NomeAutenticacao, "M", "Dispositivo MFA vinculado", "MFA", status.IdUserMfa, status.IdGpecon);
            return new MfaDeviceResult { Success = true, DeviceToken = token, ExpiresUtc = expires.ToUniversalTime() };
        }

        public bool CheckMfaDevice(string tableOrigin, int idGpecon, long idUserMfa, Guid? uidUsuario, string deviceToken)
        {
            MfaStatusResult status = GetMfaStatus(tableOrigin, idGpecon, idUserMfa, uidUsuario);
            if (!status.RememberDeviceEnabled || status.RememberDeviceDays <= 0 || string.IsNullOrWhiteSpace(deviceToken))
                return false;
            string hash = Sha256Hex(deviceToken);
            int count = 0;
            using (SqlConnection conn = CreateMfaConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT COUNT(1) FROM [LX_TCS].[TCS_USUARIO_MFA_DISPOSITIVO]
WHERE TABLE_ORIGIN = @o AND ID_GPCON = @g AND ID_USER_MFA = @u AND TOKEN_HASH = @h AND DATA_EXPIRACAO > GETDATE()";
                cmd.Parameters.AddWithValue("@o", status.TableOrigin);
                cmd.Parameters.AddWithValue("@g", status.IdGpecon);
                cmd.Parameters.AddWithValue("@u", status.IdUserMfa);
                cmd.Parameters.AddWithValue("@h", hash);
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return count > 0;
        }

        public MfaValidateResult ValidateMfaTicket(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
                return new MfaValidateResult { Success = false, Message = "Ticket MFA ausente." };
            try
            {
                Cryptography crypto = new Cryptography();
                crypto.UseSeed = false;
                string plain = crypto.Decrypt(ticket);
                string[] parts = plain.Split(new[] { "||" }, StringSplitOptions.None);
                if (parts.Length < 5 || parts[0] != "MFA")
                    return new MfaValidateResult { Success = false, Message = "Ticket MFA inválido." };

                long expUnix;
                if (!TryReadTicketExpiryUnix(parts[4], out expUnix))
                    return new MfaValidateResult { Success = false, Message = "Ticket MFA inválido." };
                if (UnixTimeSeconds() > expUnix)
                    return new MfaValidateResult { Success = false, Message = "Ticket MFA expirado." };

                DateTime expUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expUnix);
                return new MfaValidateResult { Success = true, Ticket = ticket, TicketExpiresUtc = expUtc };
            }
            catch
            {
                return new MfaValidateResult { Success = false, Message = "Ticket MFA inválido." };
            }
        }

        public MfaValidateResult IssueMfaSkipTicket(string tableOrigin, int idGpecon, long idUserMfa, Guid? uidUsuario, string reason)
        {
            MfaStatusResult status = GetMfaStatus(tableOrigin, idGpecon, idUserMfa, uidUsuario);
            return IssueTicket(status, reason);
        }

        private MfaValidateResult FinishTotp(string tableOrigin, int idGpecon, long idUserMfa, Guid? uidUsuario, string code, bool enrollConfirm, string canal)
        {
            MfaStatusResult status = GetMfaStatus(tableOrigin, idGpecon, idUserMfa, uidUsuario);
            if (!status.RequiresMfa)
                return IssueTicket(status, status.SkipReason);
            if (status.MfaLocked)
                return new MfaValidateResult { Success = false, MfaLocked = true, Message = "MFA bloqueado por excesso de tentativas." };

            string encrypted = null;
            int attempts = 0;
            using (SqlConnection conn = CreateMfaConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT ACCESS_SECRET, ATIVO, QTD_TENTATIVAS_TOTP FROM [LX_TCS].[TCS_USUARIO_MFA]
WHERE TABLE_ORIGIN = @o AND ID_GPCON = @g AND ID_USER_MFA = @u";
                cmd.Parameters.AddWithValue("@o", status.TableOrigin);
                cmd.Parameters.AddWithValue("@g", status.IdGpecon);
                cmd.Parameters.AddWithValue("@u", status.IdUserMfa);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        encrypted = reader.IsDBNull(0) ? null : reader.GetString(0);
                        attempts = reader.IsDBNull(2) ? 0 : Convert.ToInt32(reader.GetValue(2));
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(encrypted))
                return new MfaValidateResult { Success = false, Message = "Cadastre o autenticador (QR Code) antes de validar." };

            string secret = DecryptMfaSecret(encrypted);
            bool ok = MfaTotp.Verify(secret, code, 1);
            if (!ok)
            {
                attempts++;
                DateTime? lockUntil = null;
                if (attempts >= MfaTotpMaxAttempts)
                    lockUntil = DateTime.Now.AddMinutes(MfaLockMinutes);
                this.DbContext.Database.ExecuteSqlCommand(
                    @"UPDATE [LX_TCS].[TCS_USUARIO_MFA]
SET QTD_TENTATIVAS_TOTP = @q, DATA_BLOQUEIO_ATE = @b, UPDATED_AT = GETDATE()
WHERE TABLE_ORIGIN = @o AND ID_GPCON = @g AND ID_USER_MFA = @u",
                    new SqlParameter("@q", attempts),
                    new SqlParameter("@b", (object)lockUntil ?? DBNull.Value),
                    new SqlParameter("@o", status.TableOrigin),
                    new SqlParameter("@g", status.IdGpecon),
                    new SqlParameter("@u", status.IdUserMfa));

                LogMfaAudit(status.NomeAutenticacao, "M", "Falha TOTP MFA", canal, status.IdUserMfa, status.IdGpecon);
                return new MfaValidateResult
                {
                    Success = false,
                    MfaLocked = lockUntil.HasValue,
                    Message = lockUntil.HasValue
                        ? "MFA bloqueado por excesso de tentativas."
                        : "Código MFA inválido."
                };
            }

            this.DbContext.Database.ExecuteSqlCommand(
                @"UPDATE [LX_TCS].[TCS_USUARIO_MFA]
SET ATIVO = 1, QTD_TENTATIVAS_TOTP = 0, DATA_BLOQUEIO_ATE = NULL, UPDATED_AT = GETDATE()
WHERE TABLE_ORIGIN = @o AND ID_GPCON = @g AND ID_USER_MFA = @u",
                new SqlParameter("@o", status.TableOrigin),
                new SqlParameter("@g", status.IdGpecon),
                new SqlParameter("@u", status.IdUserMfa));

            LogMfaAudit(status.NomeAutenticacao, enrollConfirm ? "E" : "M",
                enrollConfirm ? "Confirmação de cadastro MFA" : "TOTP MFA válido",
                canal, status.IdUserMfa, status.IdGpecon);
            return IssueTicket(status, enrollConfirm ? "ENROLL_OK" : "OK");
        }

        private MfaValidateResult IssueTicket(MfaStatusResult status, string reason)
        {
            long expUnix = UnixTimeSeconds() + (MfaTicketMinutes * 60);
            string payload = string.Format(CultureInfo.InvariantCulture,
                "MFA||{0}||{1}||{2}||{3}||{4}",
                status.TableOrigin, status.IdGpecon, status.IdUserMfa, expUnix, reason ?? "");
            Cryptography crypto = new Cryptography();
            crypto.UseSeed = false;
            string ticket = crypto.Encrypt(payload);
            DateTime expUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expUnix);
            return new MfaValidateResult { Success = true, Ticket = ticket, TicketExpiresUtc = expUtc, Message = reason };
        }

        private static bool TryReadTicketExpiryUnix(string raw, out long expUnix)
        {
            expUnix = 0;
            if (string.IsNullOrWhiteSpace(raw))
                return false;
            if (long.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out expUnix) && expUnix > 100000)
                return true;
            DateTime parsed;
            if (!DateTime.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out parsed))
                return false;
            if (parsed.Kind == DateTimeKind.Local)
                parsed = parsed.ToUniversalTime();
            else if (parsed.Kind == DateTimeKind.Unspecified)
                parsed = DateTime.SpecifyKind(parsed, DateTimeKind.Utc);
            expUnix = (long)(parsed - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            return true;
        }

        private void LoadUxUser(string origin, Guid? uidUsuario, MfaStatusResult result)
        {
            if (origin != MfaOriginUx && origin != MfaOriginClienteConnect)
                return;

            using (SqlConnection conn = CreateMfaConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                if (uidUsuario.HasValue && uidUsuario.Value != Guid.Empty)
                {
                    cmd.CommandText = @"SELECT ID_USUARIO, UID_USUARIO, NOME_AUTENTICACAO, ID_LINX_GPECON,
ISNULL(INDICA_USUARIO_SERVICO,0), ISNULL(AUTENTICACAO_WINDOWS,0),
ISNULL(INDICA_UTILIZA_SSO,0), INDICA_UTILIZA_MFA
FROM [LX_TCS].[TCS_USUARIO_AUTENTICACAO] WHERE UID_USUARIO = @uid";
                    cmd.Parameters.AddWithValue("@uid", uidUsuario.Value);
                }
                else if (result.IdUserMfa > 0)
                {
                    cmd.CommandText = @"SELECT ID_USUARIO, UID_USUARIO, NOME_AUTENTICACAO, ID_LINX_GPECON,
ISNULL(INDICA_USUARIO_SERVICO,0), ISNULL(AUTENTICACAO_WINDOWS,0),
ISNULL(INDICA_UTILIZA_SSO,0), INDICA_UTILIZA_MFA
FROM [LX_TCS].[TCS_USUARIO_AUTENTICACAO] WHERE ID_USUARIO = @id";
                    cmd.Parameters.AddWithValue("@id", result.IdUserMfa);
                }
                else
                    return;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        return;
                    result.IdUserMfa = Convert.ToInt64(reader.GetValue(0));
                    result.UidUsuario = reader.IsDBNull(1) ? result.UidUsuario : reader.GetGuid(1);
                    result.NomeAutenticacao = reader.IsDBNull(2) ? null : reader.GetString(2);
                    if (result.IdGpecon <= 0 && !reader.IsDBNull(3))
                        result.IdGpecon = Convert.ToInt32(reader.GetValue(3));
                    result.IndicaUsuarioServico = Convert.ToBoolean(reader.GetValue(4));
                    result.AutenticacaoWindows = Convert.ToBoolean(reader.GetValue(5));
                    result.UserUtilizaSso = Convert.ToBoolean(reader.GetValue(6));
                    result.UserUtilizaMfa = reader.IsDBNull(7) ? true : Convert.ToBoolean(reader.GetValue(7));
                }

                if (result.IdGpecon > 0)
                {
                    using (SqlCommand cmdEmp = conn.CreateCommand())
                    {
                        cmdEmp.CommandText = "SELECT NOME_EMPRESA FROM [LX_TCS].[TCS_EMPRESA_AUTENTICACAO] WHERE ID_LINX = @g";
                        cmdEmp.Parameters.AddWithValue("@g", result.IdGpecon);
                        object name = cmdEmp.ExecuteScalar();
                        if (name != null && name != DBNull.Value)
                            result.NomeEmpresa = Convert.ToString(name);
                    }
                }
            }
        }

        private string GetMfaAccessSecret(string origin, int idGpecon, long idUserMfa)
        {
            using (SqlConnection conn = CreateMfaConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT ACCESS_SECRET FROM [LX_TCS].[TCS_USUARIO_MFA]
WHERE TABLE_ORIGIN = @o AND ID_GPCON = @g AND ID_USER_MFA = @u";
                cmd.Parameters.AddWithValue("@o", origin);
                cmd.Parameters.AddWithValue("@g", idGpecon);
                cmd.Parameters.AddWithValue("@u", idUserMfa);
                object value = cmd.ExecuteScalar();
                if (value == null || value == DBNull.Value)
                    return null;
                return Convert.ToString(value);
            }
        }

        private void UpsertMfaSecret(string origin, int idGpecon, long idUserMfa, string encryptedSecret, bool ativo)
        {
            this.DbContext.Database.ExecuteSqlCommand(
                @"IF EXISTS (SELECT 1 FROM [LX_TCS].[TCS_USUARIO_MFA] WHERE TABLE_ORIGIN = @o AND ID_GPCON = @g AND ID_USER_MFA = @u)
    UPDATE [LX_TCS].[TCS_USUARIO_MFA]
    SET ACCESS_SECRET = @s, ATIVO = @a, QTD_TENTATIVAS_TOTP = 0, DATA_BLOQUEIO_ATE = NULL, UPDATED_AT = GETDATE()
    WHERE TABLE_ORIGIN = @o AND ID_GPCON = @g AND ID_USER_MFA = @u
ELSE
    INSERT INTO [LX_TCS].[TCS_USUARIO_MFA]
    (TABLE_ORIGIN, ID_GPCON, ID_USER_MFA, ATIVO, ACCESS_SECRET, QTD_TENTATIVAS_TOTP, CREATED_AT, UPDATED_AT)
    VALUES (@o, @g, @u, @a, @s, 0, GETDATE(), GETDATE())",
                new SqlParameter("@o", origin),
                new SqlParameter("@g", idGpecon),
                new SqlParameter("@u", idUserMfa),
                new SqlParameter("@s", encryptedSecret),
                new SqlParameter("@a", ativo));
        }

        private SqlConnection CreateMfaConnection()
        {
            // Do not use Database.Connection.ConnectionString after EF has opened it:
            // Persist Security Info=false strips Password, so a new SqlConnection fails
            // while EF queries on the same DbContext still work (login + environment list).
            string cs = null;
            ConnectionStringSettings named = ConfigurationManager.ConnectionStrings["FrameworkAutorizacao"];
            if (named != null && !string.IsNullOrWhiteSpace(named.ConnectionString))
                cs = named.ConnectionString;
            if (string.IsNullOrWhiteSpace(cs))
                cs = this.DbContext.Database.Connection.ConnectionString;
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            return conn;
        }

        private static string EncryptMfaSecret(string raw)
        {
            Cryptography crypto = new Cryptography();
            crypto.UseSeed = false;
            return crypto.Encrypt(raw);
        }

        private static string DecryptMfaSecret(string encrypted)
        {
            Cryptography crypto = new Cryptography();
            crypto.UseSeed = false;
            return crypto.Decrypt(encrypted);
        }

        private void LogMfaAudit(string userName, string tipoEvento, string descricao, string canal, long? idUsuario, int? idGpecon)
        {
            try
            {
                EnsureAuthAccessTable();
                InsertAuthAccessEvent(
                    tipoEvento: tipoEvento[0],
                    userName: string.IsNullOrEmpty(userName) ? ("GPECON:" + (idGpecon.HasValue ? idGpecon.Value.ToString() : "0")) : userName,
                    idUsuario: idUsuario,
                    codigoErro: null,
                    descricao: descricao,
                    qtdTentativas: 0,
                    contaTentativa: false,
                    indicaBloqueio: false,
                    canal: canal);
            }
            catch
            {
            }
        }

        private static string Sha256Hex(string value)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(value ?? ""));
                StringBuilder sb = new StringBuilder(hash.Length * 2);
                for (int i = 0; i < hash.Length; i++)
                    sb.Append(hash[i].ToString("x2", CultureInfo.InvariantCulture));
                return sb.ToString();
            }
        }
    }

    internal static class MfaTotp
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        public static string GenerateSecret()
        {
            byte[] bytes = new byte[20];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                rng.GetBytes(bytes);
            return ToBase32(bytes);
        }

        public static bool Verify(string base32Secret, string code, int window)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;
            string digits = code.Trim();
            if (digits.Length != 6)
                return false;
            long timestep = UnixTimeSeconds() / 30;
            for (int i = -window; i <= window; i++)
            {
                if (Compute(base32Secret, timestep + i) == digits)
                    return true;
            }
            return false;
        }

        public static string Compute(string base32Secret, long timestep)
        {
            byte[] key = FromBase32(base32Secret);
            byte[] data = BitConverter.GetBytes(timestep);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(data);
            using (HMACSHA1 hmac = new HMACSHA1(key))
            {
                byte[] hash = hmac.ComputeHash(data);
                int offset = hash[hash.Length - 1] & 0x0F;
                int binary = ((hash[offset] & 0x7F) << 24)
                    | ((hash[offset + 1] & 0xFF) << 16)
                    | ((hash[offset + 2] & 0xFF) << 8)
                    | (hash[offset + 3] & 0xFF);
                int otp = binary % 1000000;
                return otp.ToString("000000", CultureInfo.InvariantCulture);
            }
        }

        private static long UnixTimeSeconds()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        private static string ToBase32(byte[] data)
        {
            StringBuilder sb = new StringBuilder((data.Length * 8 + 4) / 5);
            int buffer = 0;
            int bits = 0;
            for (int i = 0; i < data.Length; i++)
            {
                buffer = (buffer << 8) | data[i];
                bits += 8;
                while (bits >= 5)
                {
                    bits -= 5;
                    sb.Append(Alphabet[(buffer >> bits) & 31]);
                }
            }
            if (bits > 0)
                sb.Append(Alphabet[(buffer << (5 - bits)) & 31]);
            return sb.ToString();
        }

        private static byte[] FromBase32(string input)
        {
            string s = (input ?? "").Trim().TrimEnd('=').ToUpperInvariant();
            int buffer = 0;
            int bits = 0;
            System.Collections.Generic.List<byte> bytes = new System.Collections.Generic.List<byte>(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                int val = Alphabet.IndexOf(s[i]);
                if (val < 0)
                    continue;
                buffer = (buffer << 5) | val;
                bits += 5;
                if (bits >= 8)
                {
                    bits -= 8;
                    bytes.Add((byte)((buffer >> bits) & 0xFF));
                }
            }
            return bytes.ToArray();
        }
    }
}
