using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linx.Internet.Application.Models
{
    [Serializable]
    public class FormAccess
    {
        public bool AcessoTotal { get; set; }
        public bool Incluir { get; set; }
        public bool PesquisaEspecial { get; set; }
        public bool Excluir{ get; set; }
        public bool Alterar { get; set; } 
        public bool Layout{ get; set; }
        public bool Imprimir{ get; set; }
        public bool Pesquisar{ get; set; }
        public bool Exportar { get; set; }
    }
    
    [Serializable]
    public class LoginInfo
    {
        public Guid UidUsuario { get; set; }
        public Int64 IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string NomeCurtoUsuario { get; set; }
        public string UsuarioAutenticacao { get; set; }
        public bool AutenticacaoWindows { get; set; }
        public DateTime DataExpiracaoSenha { get; set; }
        public Guid UidGrupoEconomico { get; set; }
        public string DescricaoGrupoEconomico { get; set; }
        public int IdLinxGrupoEconomico { get; set; }
        public int IdTcsAmbienteDefault { get; set; }
        public string CacheKey { get; set; }
        public bool IsSupportMode { get; set; }
        public List<AmbienteInfo> Ambientes { get; set; }
        public string UrlWorkArea { get; set; }
        public List<GpeconInfo> GruposEconomicos { get; set; }
        public string Info { get; set; }
    }

    [Serializable]
    public class AmbienteInfo
    {
        public int IdTcsAmbiente { get; set; }
        public string DescricaoAmbiente { get; set; }
        public int IdTcsAplicativo { get; set; }
        public string DescricaoAplicativo { get; set; }
        public string UrlAplicativo { get; set; }
        public Guid Token { get; set; }
        public Guid UidAplicacao { get; set; }
        public Guid UidEmpresa { get; set; }
        public string DescricaoEmpresa { get; set; }
        public bool IndicaAdministrador { get; set; }
        public List<ParametroInfo> Parametros { get; set; }
        public string UrlServiceBus { get; set; }
        public bool IndicaMultiGpecon { get; set; }
    }

    [Serializable]
    public class GpeconInfo
    {
        public int IdGpecon { get; set; }
        public string Descricao { get; set; }
    }

    [Serializable]
    public class ParametroInfo
    {
        public string TituloParametro { get; set; }
        public string ValorParametro { get; set; }
    }

    [Serializable]
    public class LoggedUser
    {
        public string DescricaoAplicacao { get; set; }
        public string DescricaoAplicativo { get; set; }
        public string GrupoEconomico { get; set; }
        public int IdLinxGpecon { get; set; }
        public int IdTcsAmbiente { get; set; }
        public int IdTcsAplicativo { get; set; }
        public bool IndicaAcessoPadrao { get; set; }
        public bool IndicaAdministrador { get; set; }
        public string NomeEmpresa { get; set; }
        public Guid UidAplicacao { get; set; }
        public Guid UidEmpresa { get; set; }
        public Guid UidGrupoEconomico { get; set; }
        public Guid UidUsuario { get; set; }
        public string Url { get; set; }
        public string UrlWorkArea { get; set; }
        public string NomeAutenticacao { get; set; }
        public string NomeUsuario { get; set; }
    }
}