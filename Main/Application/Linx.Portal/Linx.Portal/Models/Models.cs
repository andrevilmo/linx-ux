using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linx.Portal.Models
{
    public class LogOnModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool RecoverPassword { get; set; }
        public bool ShowEnvironments { get; set; }
    }

    public class LoggedUser
    {
        public string DescricaoAmbiente { get; set; }
        public string DescricaoAplicacao { get; set; }
        //public string DescricaoGrupo { get; set; }
        public int IdTcsAmbiente { get; set; }
        public bool IndicaAdministrador { get; set; }
        public string NomeEmpresa { get; set; }
        public string GrupoEconomico { get; set; }
        public Guid UidAplicacao { get; set; }
        public Guid UidGrupoEconomico { get; set; }
        public Guid UidEmpresa { get; set; }
        //public string UidGrupoAcesso { get; set; }
        public Guid UidUsuario { get; set; }
        public string Url { get; set; }
        public string NomeUsuario { get; set; }
        public int IdLinxGpecon { get; set; }
        public bool IndicaAcessoPadrao { get; set; }

        public string DescricaoAplicativo { get; set; }
        public int IdTcsAplicativo { get; set; }

        public Guid? UidUsuarioSuporte { get; set; }
        public string UsuarioSuporte { get; set; }
        public string NomeAutenticacao { get; set; }

        public string UrlWorkArea { get; set; }
    }

    public class UserInfo
    {
        public int IdUsuario { get; set; }
        public Guid UidUsuario { get; set; }
        public string NomeAutenticacao { get; set; }
        public string NomeCurtoUsuario { get; set; }
        public string NomeUsuario { get; set; }
    }

    [Serializable]
    public class MfaPendingRedirect
    {
        public string Url { get; set; }
        public Guid UidEmpresa { get; set; }
        public Guid UidGrupoEconomico { get; set; }
        public Guid UidUsuario { get; set; }
        public Guid UidAplicacao { get; set; }
        public int IdAmbiente { get; set; }
        public string Formulario { get; set; }
        public string NomeEmpresa { get; set; }
        public string GrupoEconomico { get; set; }
        public int IdLinxGpecon { get; set; }
        public string UsuarioAutenticacao { get; set; }
        public bool SupportMode { get; set; }
        public string UrlWorkArea { get; set; }
        public string Ticket { get; set; }
    }

    public class PortalMfaStatus
    {
        public string TableOrigin { get; set; }
        public int IdGpecon { get; set; }
        public long IdUserMfa { get; set; }
        public Guid? UidUsuario { get; set; }
        public string NomeAutenticacao { get; set; }
        public string NomeEmpresa { get; set; }
        public bool CompanyMfaEnabled { get; set; }
        public bool UserUtilizaMfa { get; set; }
        public bool Enrolled { get; set; }
        public bool RequiresMfa { get; set; }
        public bool MfaLocked { get; set; }
        public string SkipReason { get; set; }
    }

    public class PortalMfaEnroll
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string OtpauthUri { get; set; }
        public string AccountLabel { get; set; }
        public string QrCodePngBase64 { get; set; }
    }

    public class PortalMfaValidate
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Ticket { get; set; }
        public DateTime? TicketExpiresUtc { get; set; }
        public bool MfaLocked { get; set; }
    }
}