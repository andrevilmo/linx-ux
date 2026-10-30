using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.ServiceModel.DomainServices.Server;
using System.Web.Security;
using Linx.Tools;

namespace Linx.Framework.BV.Autorizacao
{
    ////////////////////////////////////////////////////////////////////////////
    ////////////////// Membership password history (reuse check) ///////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class AutorizacaoDomainService
    {
        private const string PasswordHistorySchemaEnsureSql = @"
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = N'LX_TCS')
    EXEC(N'CREATE SCHEMA [LX_TCS]');";

        private const string PasswordHistoryTableEnsureSql = @"
IF NOT EXISTS (
    SELECT 1 FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE s.name = N'LX_TCS' AND t.name = N'TCS_HISTORICO_SENHA_USUARIO')
BEGIN
    CREATE TABLE [LX_TCS].[TCS_HISTORICO_SENHA_USUARIO]
    (
        [ID_TCS_HISTORICO_SENHA_USUARIO] INT IDENTITY(1,1) NOT NULL,
        [NOME_USUARIO] NVARCHAR(256) NOT NULL,
        [PASSWORD_HASH] NVARCHAR(128) NOT NULL,
        [PASSWORD_SALT] NVARCHAR(128) NOT NULL,
        [PASSWORD_FORMAT] INT NOT NULL CONSTRAINT [DF_TCS_HISTORICO_SENHA_FORMAT] DEFAULT ((1)),
        [DATA_ALTERACAO] DATETIME NOT NULL CONSTRAINT [DF_TCS_HISTORICO_SENHA_DATA] DEFAULT (GETDATE()),
        CONSTRAINT [XPK_TCS_HISTORICO_SENHA_USUARIO] PRIMARY KEY CLUSTERED ([ID_TCS_HISTORICO_SENHA_USUARIO] ASC)
    );
END
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_TCS_HISTORICO_SENHA_USUARIO_NOME_DATA'
      AND object_id = OBJECT_ID(N'LX_TCS.TCS_HISTORICO_SENHA_USUARIO'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_TCS_HISTORICO_SENHA_USUARIO_NOME_DATA]
        ON [LX_TCS].[TCS_HISTORICO_SENHA_USUARIO] ([NOME_USUARIO], [DATA_ALTERACAO] DESC);
END";

        private static bool _passwordHistoryTableEnsured;

        private static int PasswordHistoryCount
        {
            get
            {
                int value;
                if (int.TryParse(ConfigurationManager.AppSettings["Password.HistoryCount"], out value) && value > 0)
                    return value;
                return 5;
            }
        }

        private sealed class MembershipPasswordRecord
        {
            public string PasswordHash;
            public string PasswordSalt;
            public int PasswordFormat;
        }

        /// <summary>
        /// Rejects <paramref name="newPassword"/> when it matches the current Membership hash
        /// or any of the last N archived hashes, then archives the current hash for future checks.
        /// Call immediately before Membership ChangePassword / ResetPassword.
        /// </summary>
        /// <param name="enforceHistory">
        /// When false (system-generated passwords), only archives the current hash.
        /// </param>
        private void ValidateAndArchivePasswordHistory(string userName, string newPassword, bool enforceHistory)
        {
            string normalized = NormalizeUserName(userName);
            if (string.IsNullOrEmpty(normalized))
                return;

            EnsurePasswordHistoryTable();

            MembershipPasswordRecord current = TryGetMembershipPassword(normalized);

            if (enforceHistory && !string.IsNullOrEmpty(newPassword))
            {
                if (current != null && PasswordMatchesRecord(newPassword, current))
                {
                    throw new DomainException(string.Format(
                        "{0} - {1}",
                        ErrorConstants._PasswordSameAsCurrent.Code,
                        ErrorConstants._PasswordSameAsCurrent.Message));
                }

                if (PasswordMatchesHistory(normalized, newPassword))
                {
                    throw new DomainException(string.Format(
                        "{0} - {1}",
                        ErrorConstants._PasswordRecentlyUsed.Code,
                        ErrorConstants._PasswordRecentlyUsed.Message));
                }
            }

            // Archive is best-effort so a history insert failure does not block the change
            // after the reuse check already passed.
            try
            {
                if (current != null)
                {
                    ArchiveMembershipPassword(normalized, current);
                    PrunePasswordHistory(normalized);
                }
            }
            catch
            {
            }
        }

        private void EnsurePasswordHistoryTable()
        {
            if (_passwordHistoryTableEnsured)
                return;

            this.DbContext.Database.ExecuteSqlCommand(PasswordHistorySchemaEnsureSql);
            this.DbContext.Database.ExecuteSqlCommand(PasswordHistoryTableEnsureSql);
            _passwordHistoryTableEnsured = true;
        }

        private static string MembershipConnectionString
        {
            get
            {
                ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings["FrameworkAutorizacao"];
                return cs != null ? cs.ConnectionString : null;
            }
        }

        private static MembershipPasswordRecord TryGetMembershipPassword(string userName)
        {
            string connectionString = MembershipConnectionString;
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(userName))
                return null;

            string applicationName = Membership.ApplicationName ?? "/";

            const string sql = @"
SELECT TOP (1) m.[Password], m.[PasswordSalt], m.[PasswordFormat]
FROM dbo.aspnet_Membership m
INNER JOIN dbo.aspnet_Users u ON m.UserId = u.UserId
INNER JOIN dbo.aspnet_Applications a ON u.ApplicationId = a.ApplicationId
WHERE a.LoweredApplicationName = LOWER(@ApplicationName)
  AND u.LoweredUserName = LOWER(@UserName);";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@ApplicationName", SqlDbType.NVarChar, 256).Value = applicationName;
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 256).Value = userName;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!reader.Read())
                        return null;

                    return new MembershipPasswordRecord
                    {
                        PasswordHash = reader.IsDBNull(0) ? null : reader.GetString(0),
                        PasswordSalt = reader.IsDBNull(1) ? null : reader.GetString(1),
                        PasswordFormat = reader.IsDBNull(2) ? 1 : reader.GetInt32(2)
                    };
                }
            }
        }

        private static bool PasswordMatchesRecord(string plainPassword, MembershipPasswordRecord record)
        {
            if (record == null || string.IsNullOrEmpty(record.PasswordHash) || string.IsNullOrEmpty(plainPassword))
                return false;

            // Clear text
            if (record.PasswordFormat == 0)
                return string.Equals(plainPassword, record.PasswordHash, StringComparison.Ordinal);

            // Encrypted: cannot re-encode safely here; skip (current-password check already done by caller when applicable).
            if (record.PasswordFormat != 1)
                return false;

            if (string.IsNullOrEmpty(record.PasswordSalt))
                return false;

            string encoded = EncodeMembershipHashedPassword(plainPassword, record.PasswordSalt);
            return string.Equals(encoded, record.PasswordHash, StringComparison.Ordinal);
        }

        private bool PasswordMatchesHistory(string normalizedUserName, string plainPassword)
        {
            string connectionString = MembershipConnectionString;
            if (string.IsNullOrEmpty(connectionString))
                return false;

            const string sql = @"
SELECT TOP (@Take) [PASSWORD_HASH], [PASSWORD_SALT], [PASSWORD_FORMAT]
FROM [LX_TCS].[TCS_HISTORICO_SENHA_USUARIO]
WHERE [NOME_USUARIO] = @UserName
ORDER BY [DATA_ALTERACAO] DESC, [ID_TCS_HISTORICO_SENHA_USUARIO] DESC;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Take", SqlDbType.Int).Value = PasswordHistoryCount;
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 256).Value = normalizedUserName;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var record = new MembershipPasswordRecord
                        {
                            PasswordHash = reader.IsDBNull(0) ? null : reader.GetString(0),
                            PasswordSalt = reader.IsDBNull(1) ? null : reader.GetString(1),
                            PasswordFormat = reader.IsDBNull(2) ? 1 : reader.GetInt32(2)
                        };
                        if (PasswordMatchesRecord(plainPassword, record))
                            return true;
                    }
                }
            }

            return false;
        }

        private static void ArchiveMembershipPassword(string normalizedUserName, MembershipPasswordRecord current)
        {
            if (current == null || string.IsNullOrEmpty(current.PasswordHash) || string.IsNullOrEmpty(current.PasswordSalt))
                return;

            string connectionString = MembershipConnectionString;
            if (string.IsNullOrEmpty(connectionString))
                return;

            const string sql = @"
INSERT INTO [LX_TCS].[TCS_HISTORICO_SENHA_USUARIO]
    ([NOME_USUARIO], [PASSWORD_HASH], [PASSWORD_SALT], [PASSWORD_FORMAT], [DATA_ALTERACAO])
VALUES
    (@UserName, @Hash, @Salt, @Format, GETDATE());";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 256).Value = normalizedUserName;
                cmd.Parameters.Add("@Hash", SqlDbType.NVarChar, 128).Value = TruncateNvarchar(current.PasswordHash, 128);
                cmd.Parameters.Add("@Salt", SqlDbType.NVarChar, 128).Value = TruncateNvarchar(current.PasswordSalt, 128);
                cmd.Parameters.Add("@Format", SqlDbType.Int).Value = current.PasswordFormat;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void PrunePasswordHistory(string normalizedUserName)
        {
            string connectionString = MembershipConnectionString;
            if (string.IsNullOrEmpty(connectionString))
                return;

            // Keep the most recent N rows; delete older ones.
            const string sql = @"
;WITH ranked AS (
    SELECT [ID_TCS_HISTORICO_SENHA_USUARIO],
           ROW_NUMBER() OVER (ORDER BY [DATA_ALTERACAO] DESC, [ID_TCS_HISTORICO_SENHA_USUARIO] DESC) AS rn
    FROM [LX_TCS].[TCS_HISTORICO_SENHA_USUARIO]
    WHERE [NOME_USUARIO] = @UserName
)
DELETE FROM ranked WHERE rn > @Keep;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 256).Value = normalizedUserName;
                cmd.Parameters.Add("@Keep", SqlDbType.Int).Value = PasswordHistoryCount;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Mirrors ASP.NET Membership EncodePassword for passwordFormat=Hashed
        /// under Framework20 compatibility (default): SHA1(salt_bytes + Unicode(password)).
        /// </summary>
        private static string EncodeMembershipHashedPassword(string password, string saltBase64)
        {
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(saltBase64);
            byte[] all = new byte[saltBytes.Length + passwordBytes.Length];
            Buffer.BlockCopy(saltBytes, 0, all, 0, saltBytes.Length);
            Buffer.BlockCopy(passwordBytes, 0, all, saltBytes.Length, passwordBytes.Length);

            using (SHA1 sha1 = SHA1.Create())
                return Convert.ToBase64String(sha1.ComputeHash(all));
        }

        private static string TruncateNvarchar(string value, int maxLen)
        {
            if (string.IsNullOrEmpty(value) || value.Length <= maxLen)
                return value ?? string.Empty;
            return value.Substring(0, maxLen);
        }
    }
}
