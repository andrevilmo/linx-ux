using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Tools
{
    public struct ErrorInfo
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public static class ErrorConstants
    {
        /* Autentication/Authorization */
        public static ErrorInfo _ApplicationAccessDenied = new ErrorInfo() { Code = "ERRAUT001", Message = "Acesso não autorizado a aplicação.".Translate() };
        public static ErrorInfo _AccessDenied = new ErrorInfo() { Code = "ERRAUT002", Message = "Acesso não permitido.".Translate() };
        public static ErrorInfo _AuthorizationTokenNotFound = new ErrorInfo() { Code = "ERRAUT003", Message = "Token de autorização não encontrado !".Translate() };
        public static ErrorInfo _AuthorizationTokenExpired = new ErrorInfo() { Code = "ERRAUT004", Message = "Token de autorização expirado !".Translate() };
        public static ErrorInfo _UserNotFound = new ErrorInfo() { Code = "ERRAUT005", Message = "Usuário não encontrado.".Translate() };
        public static ErrorInfo _UserNotActive = new ErrorInfo() { Code = "ERRAUT006", Message = "Usuário inativo.".Translate() };
        public static ErrorInfo _UserLoginExpired = new ErrorInfo() { Code = "ERRAUT007", Message = "Login do usuário expirado.".Translate() };
        public static ErrorInfo _UserBadNameOrPassword = new ErrorInfo() { Code = "ERRAUT008", Message = "Usuário ou senha incorretos".Translate() };
        // Lockout message must stay Portuguese regardless of UI culture (do not Translate).
        public static ErrorInfo _UserLockedOut = new ErrorInfo() { Code = "ERRAUT020", Message = "Usuário bloqueado por excesso de tentativas inválidas de senha. Solicite o desbloqueio ao administrador." };

        /// <summary>Canonical lockout display: "ERRAUT020 - &lt;Portuguese message&gt;".</summary>
        public static string FormatUserLockedOutMessage()
        {
            return string.Format("{0} - {1}", _UserLockedOut.Code, _UserLockedOut.Message);
        }

        /// <summary>True when message is ERRAUT020 or ASP.NET Membership English lockout text.</summary>
        public static bool IsMembershipLockoutMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                return false;
            if (message.StartsWith(_UserLockedOut.Code, StringComparison.OrdinalIgnoreCase))
                return true;
            return message.IndexOf("locked out", StringComparison.OrdinalIgnoreCase) >= 0
                || message.IndexOf("account has been locked", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>Replaces Membership English lockout text with ERRAUT020 Portuguese message.</summary>
        public static string EnsureUserLockedOutMessage(string message)
        {
            return IsMembershipLockoutMessage(message) ? FormatUserLockedOutMessage() : message;
        }

        public static ErrorInfo _PasswordSameAsCurrent = new ErrorInfo() { Code = "ERRAUT009", Message = "A senha deve ser diferente da atual.".Translate() };
        public static ErrorInfo _ChangePasswordError = new ErrorInfo() { Code = "ERRAUT010", Message = "Erro ao alterar a senha do usuário.".Translate() };
        public static ErrorInfo _LoginInvalidParameters = new ErrorInfo() { Code = "ERRAUT011", Message = "Parâmetros de Login inválidos.".Translate() };
        public static ErrorInfo _InvalidEnvironmentInfo = new ErrorInfo() { Code = "ERRAUT012", Message = "Informações de ambiente inválidas.".Translate() };
        public static ErrorInfo _UserHasWindowsAuthentication = new ErrorInfo() { Code = "ERRAUT013", Message = "Usuário utiliza Autenticação do Windows.".Translate() };
        public static ErrorInfo _InvalidCompanyInfo = new ErrorInfo() { Code = "ERRAUT014", Message = "Informações de Empresa inválidas.".Translate() };
        public static ErrorInfo _IdLinxNotFound = new ErrorInfo() { Code = "ERRAUT015", Message = "Não foi possível determinar o Id Linx.".Translate() };
        public static ErrorInfo _CacheInfoNotFound = new ErrorInfo() { Code = "ERRAUT016", Message = "Informações de cache não encontradas. Favor efetuar novo Login.".Translate() };
        public static ErrorInfo _UserHasNoDefaultAccess = new ErrorInfo() { Code = "ERRAUT017", Message = "Usuário não possui acesso padrão informado.".Translate() };
        public static ErrorInfo _InvalidRelatedEnvironmentInfo = new ErrorInfo() { Code = "ERRAUT018", Message = "Informação de Ambiente Relacionado inválida.".Translate() };
        public static ErrorInfo _InvalidIdAplicativeInfo = new ErrorInfo() { Code = "ERRAUT019", Message = "Informação de Aplicativo inválida.".Translate() };
        public static ErrorInfo _ConnectionStringNotFound = new ErrorInfo() { Code = "ERRAUT021", Message = "String de Conexão não encontrada.".Translate() };
    }


    public static class LinxSqlErrors
    {
        public static Exception SqlException(SqlException sqlException)
        {
            return SqlException(sqlException, null);
        }

        public static Exception SqlException(SqlException sqlException, Dictionary<string, string> constraintsInfo)
        {
            string errorMessage = string.Format("{0} - {1}", sqlException.Number, sqlException.Message);
            string table = string.Empty;
            string database = string.Empty;
            string column = string.Empty;
            string constraint = string.Empty;
            string keyValue = string.Empty;
            string command = string.Empty;
            string constraintColumns = null;

            switch (sqlException.Number)
            {
                case 547: //Foreign Key
                    constraint = sqlException.Message.Extract("\"", "\"");
                    database = sqlException.Message.Extract("\"", "\"", 2);
                    table = sqlException.Message.Extract("\"", "\"", 3);
                    column = sqlException.Message.Extract("'", "'");
                    command = sqlException.Message.Extract("The ", " statement ");
                    constraintColumns = GetConstraintColumns(constraintsInfo, constraint);

                    switch (command)
                    {
                        case "DELETE":
                            errorMessage = string.Format("Violação de chave estrangeira: Não é possível excluir registro(s) ainda referenciado(s) na tabela {0} coluna {1} ({2}).", table, constraintColumns.IsNullOrEmpty() ? column : constraintColumns, constraint);
                            break;
                        case "UPDATE":
                            errorMessage = string.Format("Violação de chave estrangeira: Tentativa de atualizar o registro com um valor não existente na tabela {0} coluna {1} ({2}).", table, constraintColumns.IsNullOrEmpty() ? column : constraintColumns, constraint);
                            break;
                        case "INSERT":
                            errorMessage = string.Format("Violação de chave estrangeira: Tentativa de inclusão de registro com um valor não existente na tabela {0} coluna {1} ({2}).", table, constraintColumns.IsNullOrEmpty() ? column : constraintColumns, constraint);
                            break;
                        default:
                            break;
                    }

                    break;

                case 2601: //Alternate Key
                    table = sqlException.Message.Extract("'", "'");
                    constraint = sqlException.Message.Extract("'", "'", 2);
                    keyValue = sqlException.Message.Extract("(", ")");
                    constraintColumns = GetConstraintColumns(constraintsInfo, constraint);
                    errorMessage = string.Format("Violação de chave única: Tentativa de incluir um valor duplicado na tabela {0}{1} com o valor {2}.", table, constraintColumns.IsNullOrEmpty() ? "" : string.Format(" coluna{0} {1}", constraintColumns.Contains(",") ? "s" : "", constraintColumns), keyValue);
                    break;

                case 2627: //Primary key
                    constraint = sqlException.Message.Extract("'", "'");
                    table = sqlException.Message.Extract("'", "'", 2);
                    keyValue = sqlException.Message.Extract("(", ")");
                    constraintColumns = GetConstraintColumns(constraintsInfo, constraint);
                    errorMessage = string.Format("Violação de chave primária: Tentativa de incluir um valor duplicado na tabela {0}{1} com o valor {2}.", table, constraintColumns.IsNullOrEmpty() ? "" : string.Format(" coluna{0} {1}", constraintColumns.Contains(",") ? "s" : "", constraintColumns), keyValue);
                    break;

                default:
                    break;
            }
            return new Exception(errorMessage);
        }

        private static string GetConstraintColumns(Dictionary<string, string> constraintsInfo, string constraintName)
        {
            if (constraintsInfo.IsNull() || constraintsInfo.Count() == 0)
                return null;
            else
                return constraintsInfo.Where(i => i.Key == constraintName).Select(i => i.Value).FirstOrDefault();
        }

    }
}
