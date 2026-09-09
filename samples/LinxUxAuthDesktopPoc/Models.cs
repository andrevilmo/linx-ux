using System;
using System.Collections.Generic;

namespace Linx.Ux.AuthDesktopPoc
{
    public sealed class AmbienteAcesso
    {
        public int IdTcsAmbiente { get; set; }
        public string DescricaoAmbiente { get; set; }
        public Guid UidAplicacao { get; set; }
        public Guid UidEmpresa { get; set; }
        public Guid UidGrupoEconomico { get; set; }
        public Guid UidUsuario { get; set; }
        public string NomeEmpresa { get; set; }
        public string GrupoEconomico { get; set; }
        public int IdLinxGpecon { get; set; }
        public bool IndicaAcessoPadrao { get; set; }
        public string Url { get; set; }
        public string NomeAutenticacao { get; set; }
    }

    public sealed class MfaStatusDto
    {
        public bool RequiresMfa { get; set; }
        public bool Enrolled { get; set; }
        public bool MfaLocked { get; set; }
        public string SkipReason { get; set; }
        public Guid? UidUsuario { get; set; }
        public int IdGpecon { get; set; }
        public string NomeAutenticacao { get; set; }
    }

    public sealed class MfaEnrollDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string OtpauthUri { get; set; }
        public string AccountLabel { get; set; }
        public string QrCodePngBase64 { get; set; }
    }

    public sealed class MfaTicketDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Ticket { get; set; }
        public DateTime? TicketExpiresUtc { get; set; }
        public bool MfaLocked { get; set; }
    }

    public sealed class LoginInfoDto
    {
        public Guid UidUsuario { get; set; }
        public long IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string NomeCurtoUsuario { get; set; }
        public int IdLinxGrupoEconomico { get; set; }
        public List<AmbienteTokenDto> Ambientes { get; set; }
    }

    public sealed class AmbienteTokenDto
    {
        public int IdTcsAmbiente { get; set; }
        public Guid Token { get; set; }
        public Guid UidAplicacao { get; set; }
        public Guid UidEmpresa { get; set; }
        public string UrlServiceBus { get; set; }
    }
}
