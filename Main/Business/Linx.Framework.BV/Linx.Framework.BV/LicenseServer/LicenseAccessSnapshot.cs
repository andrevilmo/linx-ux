namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// Customer license snapshot returned by GetCustomerLicense.
    /// Fields mirror Linx.LicenseServer.BV.Licenciamento customer license view.
    /// </summary>
    public sealed class LicenseAccessSnapshot
    {
        public byte? LxStatusLicenca { get; set; }
        public byte? LxStatusLicencaCliente { get; set; }
        public string LxStatusLicencaClienteName { get; set; }
        public bool InativoCliente { get; set; }
        public bool InativoLicenca { get; set; }
        public bool InativoProduto { get; set; }
        public bool IndicaBloqueioFinanceiroCliente { get; set; }
        public bool IndicaBloqueioFinanceiroLicenca { get; set; }
        public int QtdeContratada { get; set; }
        public int QtdeEmUso { get; set; }
        public bool ControlaQtde { get; set; }
    }

    /// <summary>
    /// Per-user usage key returned by ValidateLicense.
    /// </summary>
    public sealed class LicenseUsageSnapshot
    {
        public byte? LxStatusChave { get; set; }
        public string Mensagem { get; set; }
    }
}
