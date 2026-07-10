using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Linx.Framework.BV.UsuarioAutorizacao;
using System.ServiceModel.DomainServices.Server;
using System.Web;
using System.Web.Security;



namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkUsuarioAutorizacaoController
    {
        [Route("PortalUserAccess"), System.Web.Http.HttpPost()]
        public List<UsuarioAcesso> PortalUserAccess(RequisicaoAcesso requisicaoAcesso)
        {
            try
            {
                List<UsuarioAcesso> usuarioAcesso = new List<UsuarioAcesso>();
                Linx.Security.Cryptography crypto = new Linx.Security.Cryptography();

                var parametros = crypto.Decrypt(requisicaoAcesso.Parametros);

                if (parametros.IsNullOrEmpty())
                {
                    usuarioAcesso = (from result in this.repository.Context.GetTcsUsuarioAcessoAmbienteNoAssociations().Where(i => i.NomeAutenticacao == requisicaoAcesso.NomeAutenticacao && i.EmDesenvolvimento == requisicaoAcesso.AcessoLocal)
                                     select new UsuarioAcesso
                                     {
                                         IdTcsAmbiente = result.IdTcsAmbiente,
                                         DescricaoAmbiente = result.DescricaoAmbiente,
                                         UidAplicacao = result.UidAplicacao,
                                         DescricaoAplicacao = result.DescricaoAplicacao,
                                         UidEmpresa = result.UidEmpresa,
                                         NomeEmpresa = result.NomeEmpresa,
                                         UidGrupoEconomico = result.UidGrupoEconomico,
                                         GrupoEconomico = result.GrupoEconomico,
                                         UidUsuario = result.UidUsuario,
                                         NomeUsuario = result.NomeUsuario,
                                         IdTcsAplicativo = result.IdTcsAplicativo,
                                         DescricaoAplicativo = result.DescricaoAplicativo,
                                         Url = result.Url,
                                         IdLinxGpecon = result.IdLinxGpecon,
                                         IndicaAdministrador = result.IndicaAdministrador,
                                         IndicaAcessoPadrao = result.IndicaAcessoPadrao,
                                         UrlWorkArea = result.UrlWorkArea
                                     }).ToList();
                }
                else
                {
                    string[] decryptedLines = parametros.Split(new string[] { "||" }, StringSplitOptions.None);

                    if (decryptedLines.Count() != 3)
                        throw new Exception("Parâmetros inválidos.");

                    int idTcsSuporteAcesso = Convert.ToInt32(decryptedLines[0]);
                    string nomeAutenticacaoAcesso = decryptedLines[1];
                    DateTime dataCadastro = DateTime.Parse(decryptedLines[2]);

                    var usuario = (from result in this.repository.Context.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.NomeAutenticacao == requisicaoAcesso.NomeAutenticacao)
                                   select new { IdUsuario = result.IdUsuario, UidUsuario = result.UidUsuario, NomeUsuario = result.NomeUsuario, IndicaAcessoSuporte = result.IndicaAcessoSuporte }).FirstOrDefault();

                    if (!usuario.IndicaAcessoSuporte)
                        throw new Exception("Usuário sem permissão para acesso de Suporte.");

                    TcsSuporteAcessoLog acessoSuporte = (from result in this.repository.Context.GetTcsSuporteAcessoLogNoAssociations().Where(i => i.IdTcsSuporteAcessoLog == idTcsSuporteAcesso && i.DataAcesso == null
                                                && i.NomeAutenticacaoAcesso == nomeAutenticacaoAcesso && i.DataCadastro == dataCadastro && !i.AcessoExpirado)
                                         select result).FirstOrDefault();

                    if (acessoSuporte.IsNull())
                        throw new Exception("Acesso expirado.");

                    TcsSuporteAcessoLog acessoSuporteOld = new TcsSuporteAcessoLog();
                    acessoSuporteOld.CopyInstanceFrom(acessoSuporte);
                    acessoSuporte.IdUsuarioSuporte = usuario.IdUsuario;
                    acessoSuporte.DataAcesso = DateTime.Now;

                    this.repository.Context.AddCustomChanges(acessoSuporte, acessoSuporteOld, ChangeOperation.Update);
                    this.repository.Context.SaveCustomChanges();

                    usuarioAcesso = (from result in this.repository.Context.GetTcsUsuarioAcessoAmbienteNoAssociations().Where(i => i.IdTcsUsuarioAcesso == acessoSuporte.IdTcsUsuarioAcesso)
                                     select new UsuarioAcesso
                                     {
                                         IdTcsAmbiente = result.IdTcsAmbiente,
                                         DescricaoAmbiente = result.DescricaoAmbiente,
                                         UidAplicacao = result.UidAplicacao,
                                         DescricaoAplicacao = result.DescricaoAplicacao,
                                         UidEmpresa = result.UidEmpresa,
                                         NomeEmpresa = result.NomeEmpresa,
                                         UidGrupoEconomico = result.UidGrupoEconomico,
                                         GrupoEconomico = result.GrupoEconomico,
                                         UidUsuario = result.UidUsuario,
                                         NomeUsuario = result.NomeUsuario,
                                         IdTcsAplicativo = result.IdTcsAplicativo,
                                         DescricaoAplicativo = result.DescricaoAplicativo,
                                         Url = result.Url,
                                         IdLinxGpecon = result.IdLinxGpecon,
                                         IndicaAdministrador = result.IndicaAdministrador,
                                         UidUsuarioSuporte = usuario.UidUsuario,
                                         UsuarioSuporte = usuario.NomeUsuario,
                                         NomeAutenticacao = result.NomeAutenticacao,
                                         IndicaAcessoPadrao = false,
                                         UrlWorkArea = result.UrlWorkArea
                                     }).ToList();
                }

                return usuarioAcesso;
            }
            catch (Exception oException)
            {
                throw oException;
            }
        }

        [Route("SupportRequest"), System.Web.Http.HttpPost()]
        public string SupportRequest(RequisicaoSuporte requisicaoSuporte)
        {
            Int64 idUsuario = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();
            string nomeAutenticacao = BusinessUserServiceHelper.GetCurrentUserAuthenticationName();
            int idTcsAmbiente;
            Guid uidUsuario;

            if (requisicaoSuporte.UidUsuario.IsNullOrEmpty())
            {
                uidUsuario = BusinessUserServiceHelper.GetCurrentUserUid().GetValueOrDefault();
                idTcsAmbiente = BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault();
            }
            else
            {
                uidUsuario = requisicaoSuporte.UidUsuario.GetValueOrDefault();
                idTcsAmbiente = requisicaoSuporte.IdTcsAmbiente.GetValueOrDefault();
            }

            var acesso = (from result in this.repository.Context.GetTcsUsuarioAcessoAmbienteNoAssociations().Where(i => i.UidUsuario == uidUsuario && i.IdTcsAmbiente == idTcsAmbiente)
                          select result.IdTcsUsuarioAcesso).FirstOrDefault();

            if (acesso.IsNullOrEmpty())
                throw new Exception("Acesso não encontrado.");

            TcsSuporteAcessoLog suporteAcesso = new TcsSuporteAcessoLog() { IdTcsUsuarioAcesso = acesso, IdUsuarioAcesso = idUsuario, DataCadastro = DateTime.Parse(DateTime.Now.ToString()) };
            this.repository.Context.AddCustomChanges(suporteAcesso, null, ChangeOperation.Insert);
            this.repository.Context.SaveCustomChanges();
            suporteAcesso.RefreshKeys();

            Linx.Security.Cryptography crypto = new Linx.Security.Cryptography();
            return string.Format("{0}{1}?supportMode={2}", requisicaoSuporte.UrlPortal, requisicaoSuporte.UrlPortal.Right(1) == "/" ? "" : "/", HttpUtility.UrlEncode(crypto.Encrypt(string.Format("{0}||{1}||{2}", suporteAcesso.IdTcsSuporteAcessoLog, nomeAutenticacao, suporteAcesso.DataCadastro))));
        }

        [Route("CheckLoginAvailability")]
        [HttpGet()]
        public bool CheckLoginAvailability(string loginName)
        {
            return repository.Context.CheckLoginAvailability(loginName);
        }

        [Route("GetAvailableLogins")]
        [HttpGet()]
        public string[] GetAvailableLogins(string userName, string companyName)
        {
            return repository.Context.GetAvailableLogins(userName, companyName);
        }
    }
}
