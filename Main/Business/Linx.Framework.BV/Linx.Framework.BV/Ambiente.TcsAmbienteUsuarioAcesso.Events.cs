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
using Linx.Framework.BV.Autorizacao;
using Linx.Framework.BV.Usuario;
using Linx.Framework.BV.UsuarioAutorizacao;

namespace Linx.Framework.BV.Ambiente
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsAmbienteUsuarioAcesso
    {
        /// Execute before lookup on server side.
        public static void OnLookUpingLookUpTcsUsuarioAutenticacao(ref IQueryable<LookUpTcsUsuarioAutenticacao> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            Empresa.EmpresaDomainService ds = new Empresa.EmpresaDomainService();
            entitySearch.EntityName = "";

            List<int> lstIdLinx = new List<int>();

            EntitySearchExpression expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdLinxGpecon").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);
                int idLinxEnvironment = Convert.ToInt32(entitySearch.Expressions[fieldPos + 2].Value);

                //remove expressions from list
                Utils.RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);

                lstIdLinx = ds.GetTcsEmpresaGpeconNoAssociations().Where(i => i.IdLinx == idLinxEnvironment).Select(i => i.IdLinxGpecon).Distinct().ToList();

                if (!lstIdLinx.Contains(idLinxEnvironment))
                    lstIdLinx.Add(idLinxEnvironment);
            }

            UsuarioAutorizacao.UsuarioAutorizacaoDomainService dsUsuario = new UsuarioAutorizacaoDomainService();
            searchDefinition = from result in dsUsuario.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch> { entitySearch }))
                               where lstIdLinx.Contains(result.IdLinx)
                               select new LookUpTcsUsuarioAutenticacao
                               {
                                   IdLinx = result.IdLinx,
                                   NomeAutenticacao = result.NomeAutenticacao,
                                   NomeEmpresa = result.NomeEmpresa,
                                   NomeUsuario = result.NomeUsuario,
                                   IdUsuario = result.IdUsuario,
                                   UidUsuario = result.UidUsuario
                               };

        }

        /// Execute on transaction ending.
        public void OnTransactedChanges(AmbienteDomainService context, ChangeOperation changeOperation)
        {
            if (changeOperation == ChangeOperation.Insert || changeOperation == ChangeOperation.Update)
            {
                UsuarioAutorizacao.UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacao.UsuarioAutorizacaoDomainService();
                TcsUsuarioAutenticacao userInfo = ds.GetTcsUsuarioAutenticacaoNoAssociations().Where(i => i.IdUsuario == this.IdUsuario).FirstOrDefault();

                if (userInfo.UidUsuario.IsNull())
                    return;

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Environment", this.IdTcsAmbiente.ToString());
                headers.Add("EconomicGroup", this.TcsAmbiente.UidEmpresa.ToString());
                headers.Add("CurrentCompany", this.TcsAmbiente.UidEmpresa.ToString());
                headers.Add("CurrentUser", BusinessUserServiceHelper.GetCurrentUserUid().ToString());


                UsuarioDomainService usuarioCtx = new UsuarioDomainService(headers);
                TcsUsuario changed = new TcsUsuario()
                {
                    IdUsuario = this.IdUsuario,
                    UidUsuario = this.UidUsuario,
                    NomeUsuario = this.NomeUsuario,
                    LxPfjFisicaJuridica = userInfo.LxPfjFisicaJuridica,
                    CnpjCpf = userInfo.CnpjCpf,
                    InscrEstadualRg = userInfo.InscrEstadualRg,
                    LxTipoLogradouro = userInfo.LxTipoLogradouro,
                    Logradouro = userInfo.Logradouro,
                    Numero = userInfo.Numero,
                    Complemento = userInfo.Complemento,
                    Bairro = userInfo.Bairro,
                    Municipio = userInfo.Municipio,
                    Uf = userInfo.Uf,
                    Cep = userInfo.Cep,
                    ObsEndereco = userInfo.ObsEndereco,
                    Email = userInfo.Email,
                    FoneCelular = userInfo.FoneCelular,
                    FoneFixo = userInfo.FoneFixo,
                    Ramal = userInfo.Ramal,
                    DataCadastro = userInfo.DataCadastro,
                    DataAlteracao = userInfo.DataAlteracao,
                    IdLinx = userInfo.IdLinx
                };
                usuarioCtx.AddCustomChanges(changed, null, ChangeOperation.Insert);
                usuarioCtx.SaveCustomChanges();
            }
        }

        /// Execute before lookup on server side.
        public static void OnLookingUpLookUpTcsAmbienteAdministrativo(ref IQueryable<LookUpTcsAmbienteAdministrativo> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            searchDefinition = (from result in Utils.GetLookupTcsAmbienteRelacionado(entitySearch)
                                select new LookUpTcsAmbienteAdministrativo
                                {
                                    IdTcsAmbienteRelacionado = result.IdTcsAmbienteRelacionado,
                                    IdLinxAmbienteRelacionado = result.IdTcsAmbienteRelacionado,
                                    DescricaoAmbienteRelacionado = result.DescricaoAmbienteRelacionado,
                                    DescricaoAplicacaoAmbienteRelacionado = result.DescricaoAplicacaoAmbienteRelacionado,
                                    NomeEmpresaAmbienteRelacionado = result.NomeEmpresaAmbienteRelacionado
                                });
        }

        public static void OnSavedContextChanges(AmbienteDomainService context, ChangeSetEntry[] entities)
        {
            //Revoga Licença License Server
            entities.Where(i => i.Entity is TcsAmbienteUsuarioAcesso && (i.Operation == DomainOperation.Delete)).Select(i => i.Entity as TcsAmbienteUsuarioAcesso).Select(i => new { i.TcsAmbiente.IdLinx, i.IdUsuario, i.UidUsuario }).Distinct().ToList().ForEach(entity =>
            {
                Autorizacao.AutorizacaoDomainService ds = new Autorizacao.AutorizacaoDomainService();
                Autorizacao.UserInfo userInfo = ds.ValidateUserAccess(entity.UidUsuario, true);

                try
                {
                    UsuarioAutorizacaoDomainService dsUsuario = new UsuarioAutorizacaoDomainService();
                    if (dsUsuario.GetTcsUsuarioAutenticacaoAcessoPNoAssociations().Where(i => i.IdUsuario == entity.IdUsuario && i.IdLinx == entity.IdLinx).Count() == 0)
                    {
                        LicenseControl.Remove(userInfo.NomeAutenticacao, BusinessUserServiceHelper.GetCurrentUserName(), BusinessUserServiceHelper.GetCompanyUid(entity.IdLinx));
                    }
                }
                catch { }
            });
        }
    }
}
