using Newtonsoft.Json;

namespace Linx.Framework.BV.LicenseServer
{
    public sealed class LicenseValidationRequest
    {
        [JsonProperty("idLicenca")]
        public long IdLicenca { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("chave")]
        public string Chave { get; set; }

        [JsonProperty("usuario")]
        public string Usuario { get; set; }

        [JsonProperty("terminal")]
        public string Terminal { get; set; }

        [JsonProperty("versao")]
        public string Versao { get; set; }
    }

    public sealed class LicenseValidationResult
    {
        [JsonProperty("licenca")]
        public LicenseInfo Licenca { get; set; }

        [JsonIgnore]
        public bool IsActive
        {
            get { return Licenca != null && Licenca.LxStatusChave == LicenseAccessDecision.StatusChaveAtivo; }
        }
    }

    public sealed class LicenseInfo
    {
        [JsonProperty("idLicencaUso")]
        public long IdLicencaUso { get; set; }

        [JsonProperty("lxStatusChave")]
        public int LxStatusChave { get; set; }

        [JsonProperty("periodicidade")]
        public int Periodicidade { get; set; }

        [JsonProperty("diasOffline")]
        public int DiasOffline { get; set; }

        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("origemBloqueio")]
        public string OrigemBloqueio { get; set; }

        [JsonProperty("quantidadeContratada")]
        public int QuantidadeContratada { get; set; }

        [JsonProperty("quantidadeEmUso")]
        public int QuantidadeEmUso { get; set; }

        [JsonProperty("lxStatusChaveName")]
        public string LxStatusChaveName { get; set; }

        [JsonIgnore]
        public string BlockOrigin
        {
            get { return string.IsNullOrWhiteSpace(OrigemBloqueio) ? string.Empty : OrigemBloqueio.Trim().ToUpperInvariant(); }
        }
    }

    internal sealed class AuthenticationResult
    {
        [JsonProperty("data")]
        public AuthenticationData Data { get; set; }
    }

    internal sealed class AuthenticationData
    {
        [JsonProperty("api_token")]
        public string ApiToken { get; set; }

        [JsonProperty("expires_in_hours")]
        public double ExpiresInHours { get; set; }
    }
}
