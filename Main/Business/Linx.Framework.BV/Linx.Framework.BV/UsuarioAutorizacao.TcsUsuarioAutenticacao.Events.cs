using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using Linx;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
using System.Text;
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Entity.Core.Objects.DataClasses;
using Linx.Framework.Autorizacao.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using System.Web.Security;
using Linx.Framework.BV.Usuario;

namespace Linx.Framework.BV.UsuarioAutorizacao
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioAutenticacao
    {
        /// Execute on transaction context ending.
        public static void OnTransactedContextChanges(UsuarioAutorizacao.UsuarioAutorizacaoDomainService context, ChangeSetEntry[] entities)
        {
            #region : AspnetUser

            //AspNetUser
            entities.Where(i => i.Entity is TcsUsuarioAutenticacao && i.Operation != DomainOperation.None).ToList().ForEach(entity =>
            {
                TcsUsuarioAutenticacao usuarioAutenticacao = entity.Entity as TcsUsuarioAutenticacao;
                bool userExists = !usuarioAutenticacao.AutenticacaoWindows && Membership.GetUser(usuarioAutenticacao.NomeAutenticacao) != null;

                //Delete
                if (entity.Operation == DomainOperation.Delete)
                {
                    //TcsUsuarioAcesso == 0 && AspNetUser exists.
                    //Delete AspNetUser
                    if (context.GetTcsUsuarioAcesso().Where(i => i.IdUsuario == usuarioAutenticacao.IdUsuario).Count() == 0 && userExists)
                    {
                        if (!Membership.DeleteUser(usuarioAutenticacao.NomeAutenticacao))
                            throw new DomainException("Erro ao excluir Usuário ASP Net Security".Translate());
                    }
                }
                else if (entity.Operation == DomainOperation.Insert)
                {
                    //Add AspNetUser
                    if (!userExists && !usuarioAutenticacao.AutenticacaoWindows)
                        usuarioAutenticacao.AddAspNetUser();
                }
            });

            #endregion
        }

        /// Execute on transaction starting.
        public void OnTransactingChanges(UsuarioAutorizacao.UsuarioAutorizacaoDomainService context, ChangeOperation changeOperation)
        {
            if (changeOperation == ChangeOperation.Delete)
            {
                context.GetTcsUsuarioAcessoAmbienteNoAssociations().Where(i => i.IdUsuario == this.IdUsuario && i.EmDesenvolvimento == false).Select(i => new { i.IdTcsAmbiente, i.UidEmpresa }).Distinct().ToList().ForEach(info =>
                {
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Environment", info.IdTcsAmbiente.ToString());
                    headers.Add("EconomicGroup", info.UidEmpresa.ToString());
                    headers.Add("CurrentCompany", info.UidEmpresa.ToString());
                    headers.Add("CurrentUser", BusinessUserServiceHelper.GetCurrentUserUid().ToString());
                    UsuarioDomainService usuarioCtx = new UsuarioDomainService(headers);
                    TcsUsuarioAcessoLocal tcsUsuario = usuarioCtx.GetTcsUsuarioAcessoLocalNoAssociations().Where(i => i.IdUsuario == this.IdUsuario).FirstOrDefault();

                    if (!tcsUsuario.IsNull())
                    {
                        try
                        {
                            usuarioCtx.AddCustomChanges(tcsUsuario, null, ChangeOperation.Delete);
                            usuarioCtx.SaveCustomChanges();
                        }
                        catch
                        {
                            throw new DomainException("Usuário possui movimentações e não pode ser excluído, deve ser inativado.".Translate());
                        }
                    }
                });
            }
        }

        /// Execute on transaction ending.
        public void OnTransactedChanges(UsuarioAutorizacao.UsuarioAutorizacaoDomainService context, ChangeOperation changeOperation)
        {
            if (changeOperation == ChangeOperation.Insert || changeOperation == ChangeOperation.Update)
            {
                var query = (from result in context.GetTcsUsuarioAcessoAmbienteNoAssociations().Where(i => i.IdUsuario == this.IdUsuario && i.EmDesenvolvimento == false)
                             select new { IdTcsAmbiente = result.IdTcsAmbiente, UidEmpresa = result.UidEmpresa }).Distinct().ToList();

                query.ForEach(info =>
                {
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("Environment", info.IdTcsAmbiente.ToString());
                    headers.Add("EconomicGroup", info.UidEmpresa.ToString());
                    headers.Add("CurrentCompany", info.UidEmpresa.ToString());
                    headers.Add("CurrentUser", BusinessUserServiceHelper.GetCurrentUserUid().ToString());
                    UsuarioDomainService usuarioCtx = new UsuarioDomainService(headers);
                    TcsUsuario changed = new TcsUsuario()
                    {
                        IdUsuario = this.IdUsuario,
                        NomeUsuario = this.NomeUsuario,
                        LxPfjFisicaJuridica = this.LxPfjFisicaJuridica,
                        CnpjCpf = this.CnpjCpf,
                        InscrEstadualRg = this.InscrEstadualRg,
                        LxTipoLogradouro = this.LxTipoLogradouro,
                        Logradouro = this.Logradouro,
                        Numero = this.Numero,
                        Complemento = this.Complemento,
                        Bairro = this.Bairro,
                        Municipio = this.Municipio,
                        Uf = this.Uf,
                        Cep = this.Cep,
                        ObsEndereco = this.ObsEndereco,
                        Email = this.Email,
                        FoneCelular = this.FoneCelular,
                        FoneFixo = this.FoneFixo,
                        Ramal = this.Ramal,
                        DataCadastro = this.DataCadastro,
                        DataAlteracao = this.DataAlteracao,
                        IdLinx = this.IdLinx,
                        UidUsuario = this.UidUsuario
                    };
                    usuarioCtx.AddCustomChanges(changed, null, ChangeOperation.Insert);
                    usuarioCtx.SaveCustomChanges();
                });
            }
        }

        /// Execute after save context changes.
        public static void OnSavedContextChanges(UsuarioAutorizacaoDomainService context, ChangeSetEntry[] entities)
        {
            //Remove UserInfo do cache.
            //Revoga Licença License Server
            entities.Where(i => i.Entity is TcsUsuarioAutenticacao && (i.Operation == DomainOperation.Delete || i.Operation == DomainOperation.Update)).ToList().ForEach(entity =>
            {
                TcsUsuarioAutenticacao originalEntity = entity.OriginalEntity as TcsUsuarioAutenticacao;
                TcsUsuarioAutenticacao changedEntity = entity.Entity as TcsUsuarioAutenticacao;

                if (entity.Operation == DomainOperation.Delete || (!originalEntity.IsNull() && (originalEntity.VigenciaFinal != changedEntity.VigenciaFinal || originalEntity.VigenciaFinal != changedEntity.VigenciaFinal || originalEntity.Inativo != changedEntity.Inativo)))
                    Utils.RemoveUserInfoFromCache(changedEntity.UidUsuario);

                //Se delete do usuário ou inativação
                if (entity.Operation == DomainOperation.Delete || (!originalEntity.IsNull() && (originalEntity.Inativo != changedEntity.Inativo && changedEntity.Inativo)))
                {
                    Autorizacao.AutorizacaoDomainService ds = new Autorizacao.AutorizacaoDomainService();
                    Autorizacao.UserInfo userInfo = ds.ValidateUserAccess(changedEntity.UidUsuario, true);

                    var acessos = (from result in context.GetTcsUsuarioAutenticacaoAcessoPNoAssociations().Where(i => i.IdUsuario == changedEntity.IdUsuario)
                                  select result.IdLinx).Distinct().ToList();

                    acessos.ForEach(idLinx =>
                    {
                        try
                        {
                            LicenseControl.Remove(changedEntity.NomeAutenticacao, BusinessUserServiceHelper.GetCurrentUserName(), BusinessUserServiceHelper.GetCompanyUid(idLinx));
                        }
                        catch { }
                    });
                }
            });
        }

        /// Execute before save changes.
        public void OnSavingChanges(UsuarioAutorizacaoDomainService context, ChangeOperation changeOperation)
        {
            if (changeOperation == ChangeOperation.Insert && this.UidUsuario.IsNullOrEmpty())
            {
                this.UidUsuario = Guid.NewGuid();
            }

            if (changeOperation == ChangeOperation.Update)
                this.DataAlteracao = DateTime.Now;

        }
    }
}
