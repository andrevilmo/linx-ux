namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// Result of the Omni-style license block decision.
    /// Status codes match Linx.LicenseServer domains:
    /// STATUS_LICENCA: 1=Testes, 2=Produção, 3=Descontinuada
    /// STATUS_CHAVE: 1=Ativo, 2=Pendente, 3=Revogado, 4=Não Autorizado
    /// </summary>
    public sealed class LicenseAccessResult
    {
        public bool Allowed { get; private set; }
        public string ReasonCode { get; private set; }
        public string Message { get; private set; }

        private LicenseAccessResult(bool allowed, string reasonCode, string message)
        {
            Allowed = allowed;
            ReasonCode = reasonCode;
            Message = message;
        }

        public static LicenseAccessResult Allow()
        {
            return new LicenseAccessResult(true, "ALLOWED", "Licença Ativa.");
        }

        public static LicenseAccessResult Deny(string reasonCode, string message)
        {
            return new LicenseAccessResult(false, reasonCode, message);
        }
    }
}
