namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// Same block procedure used on Omni (license-server-dll):
    /// 1. GetCustomerLicense — block inactive, discontinued, or financially locked clients
    /// 2. ValidateLicense — block when the usage key is not Ativo
    /// Pure decision logic with no I/O so it can be tested without the license server.
    /// </summary>
    public static class LicenseAccessDecision
    {
        public const byte StatusLicencaTestes = 1;
        public const byte StatusLicencaProducao = 2;
        public const byte StatusLicencaDescontinuada = 3;

        public const byte StatusChaveAtivo = 1;
        public const byte StatusChavePendente = 2;
        public const byte StatusChaveRevogado = 3;
        public const byte StatusChaveNaoAutorizado = 4;

        public static LicenseAccessResult EvaluateCustomer(LicenseAccessSnapshot snapshot)
        {
            if (snapshot == null)
            {
                return LicenseAccessResult.Deny(
                    "CUSTOMER_LICENSE_MISSING",
                    "Acesso bloqueado: cliente sem licença cadastrada para este produto.");
            }

            if (snapshot.InativoCliente)
            {
                return LicenseAccessResult.Deny(
                    "CUSTOMER_INACTIVE",
                    "Acesso bloqueado: cliente inativo no servidor de licenças.");
            }

            if (snapshot.InativoLicenca)
            {
                return LicenseAccessResult.Deny(
                    "CUSTOMER_LICENSE_INACTIVE",
                    "Acesso bloqueado: licença do cliente inativa.");
            }

            if (snapshot.InativoProduto)
            {
                return LicenseAccessResult.Deny(
                    "PRODUCT_INACTIVE",
                    "Acesso bloqueado: produto de licença inativo.");
            }

            if (snapshot.IndicaBloqueioFinanceiroCliente)
            {
                return LicenseAccessResult.Deny(
                    "CUSTOMER_FINANCIAL_BLOCK",
                    "Acesso bloqueado: cliente com bloqueio financeiro.");
            }

            if (snapshot.IndicaBloqueioFinanceiroLicenca)
            {
                return LicenseAccessResult.Deny(
                    "LICENSE_FINANCIAL_BLOCK",
                    "Acesso bloqueado: licença com bloqueio financeiro.");
            }

            if (IsUnusableLicenseStatus(snapshot.LxStatusLicenca))
            {
                return LicenseAccessResult.Deny(
                    "PRODUCT_LICENSE_DISCONTINUED",
                    "Acesso bloqueado: licença do produto descontinuada ou inválida.");
            }

            if (IsUnusableLicenseStatus(snapshot.LxStatusLicencaCliente))
            {
                string statusName = string.IsNullOrEmpty(snapshot.LxStatusLicencaClienteName)
                    ? "inválida"
                    : snapshot.LxStatusLicencaClienteName;
                return LicenseAccessResult.Deny(
                    "CUSTOMER_LICENSE_DISCONTINUED",
                    "Acesso bloqueado: licença do cliente " + statusName + ".");
            }

            if (!HasDeclaredLicenseStatus(snapshot))
            {
                return LicenseAccessResult.Deny(
                    "CUSTOMER_LICENSE_MISSING",
                    "Acesso bloqueado: cliente sem licença cadastrada para este produto.");
            }

            if (snapshot.ControlaQtde && snapshot.QtdeContratada > 0 && snapshot.QtdeEmUso > snapshot.QtdeContratada)
            {
                return LicenseAccessResult.Deny(
                    "LICENSE_QUOTA_EXCEEDED",
                    "Acesso bloqueado: quantidade de licenças contratadas excedida (" +
                    snapshot.QtdeEmUso + "/" + snapshot.QtdeContratada + ").");
            }

            return LicenseAccessResult.Allow();
        }

        public static LicenseAccessResult EvaluateUsageKey(LicenseUsageSnapshot snapshot)
        {
            if (snapshot == null)
            {
                return LicenseAccessResult.Deny(
                    "USAGE_KEY_MISSING",
                    "Acesso bloqueado: não foi possível validar a chave de licença.");
            }

            if (snapshot.LxStatusChave != StatusChaveAtivo)
            {
                string detail = UsageKeyMessage(snapshot);
                return LicenseAccessResult.Deny(
                    UsageKeyReason(snapshot.LxStatusChave),
                    "Acesso bloqueado: " + detail);
            }

            return LicenseAccessResult.Allow();
        }

        private static bool HasDeclaredLicenseStatus(LicenseAccessSnapshot snapshot)
        {
            return IsUsableLicenseStatus(snapshot.LxStatusLicenca)
                || IsUsableLicenseStatus(snapshot.LxStatusLicencaCliente);
        }

        private static bool IsUsableLicenseStatus(byte? status)
        {
            return status == StatusLicencaTestes || status == StatusLicencaProducao;
        }

        private static bool IsUnusableLicenseStatus(byte? status)
        {
            return status.HasValue && status.Value != 0 && !IsUsableLicenseStatus(status);
        }

        private static string UsageKeyReason(byte? status)
        {
            if (status == StatusChavePendente)
                return "USAGE_KEY_PENDING";
            if (status == StatusChaveRevogado)
                return "USAGE_KEY_REVOKED";
            if (status == StatusChaveNaoAutorizado)
                return "USAGE_KEY_UNAUTHORIZED";
            return "USAGE_KEY_INVALID";
        }

        private static string UsageKeyMessage(LicenseUsageSnapshot snapshot)
        {
            if (!string.IsNullOrEmpty(snapshot.Mensagem))
                return snapshot.Mensagem;

            if (snapshot.LxStatusChave == StatusChavePendente)
                return "licença pendente.";
            if (snapshot.LxStatusChave == StatusChaveRevogado)
                return "licença revogada.";
            if (snapshot.LxStatusChave == StatusChaveNaoAutorizado)
                return "licença não autorizada.";
            return "status da licença inválido.";
        }
    }
}
