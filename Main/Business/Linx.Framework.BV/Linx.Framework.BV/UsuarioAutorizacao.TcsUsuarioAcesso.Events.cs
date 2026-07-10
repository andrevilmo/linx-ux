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
using Linx.Framework.BV.Empresa;

namespace Linx.Framework.BV.UsuarioAutorizacao
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioAcesso
    {
        /// Execute before save context changes.
        public static void OnSavingContextChanges(UsuarioAutorizacao.UsuarioAutorizacaoDomainService context, ChangeSetEntry[] entities)
        {
            //aqui
            ////Verifica se existe aplicativo diferente de 1 (Linx Ux) e 15 (Linx Services) cadastrado mais de uma vez
            //var apps = from result in entities.Where(i => i.Entity is TcsUsuarioAcesso && (((TcsUsuarioAcesso)i.Entity).IdAplicativo != 1 && ((TcsUsuarioAcesso)i.Entity).IdAplicativo != 15) && i.Operation != DomainOperation.Delete).Select(i => i.Entity as TcsUsuarioAcesso)
            //           group result by new { result.UidUsuario, result.IdAplicativo } into groupId
            //           where groupId.Count() > 1
            //           select groupId;

            //if (apps.Count() > 0)
            //{
            //    throw new DomainException(String.Format("Somente os aplicativos {0} e {1} permitem múltiplos acessos.".Translate(), Linx.Framework.Domains.BM.Domains.IdAplicativo.GetValues()["1"], Linx.Framework.Domains.BM.Domains.IdAplicativo.GetValues()["15"]));
            //}
        }

        /// Execute before lookup on server side.
        public static void OnLookUpingLookUpTcsAmbiente(ref IQueryable<LookUpTcsAmbiente> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            EmpresaDomainService ds = new EmpresaDomainService();
            entitySearch.EntityName = "";

            List<int> lstIdLinxAmbiente = new List<int>();

            EntitySearchExpression expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdLinxGpecon").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);
                int idLinxGpecon = Convert.ToInt32(entitySearch.Expressions[fieldPos + 2].Value);

                //remove expressions from list
                Utils.RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);

                lstIdLinxAmbiente = ds.GetTcsEmpresaGpeconNoAssociations().Where(i => i.IdLinxGpecon == idLinxGpecon).Select(i => i.IdLinx).Distinct().ToList();

                if (!lstIdLinxAmbiente.Contains(idLinxGpecon))
                    lstIdLinxAmbiente.Add(idLinxGpecon);
            }

            expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdLinxEmpresa").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);
                Utils.RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);
            }

            Ambiente.AmbienteDomainService dsAmbiente = new Ambiente.AmbienteDomainService();
            searchDefinition = from result in dsAmbiente.GetTcsAmbienteByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch> { entitySearch }))
                               where lstIdLinxAmbiente.Contains(result.IdLinx)
                               select new LookUpTcsAmbiente
                               {
                                   DescricaoAmbiente = result.DescricaoAmbiente,
                                   DescricaoAplicacao = result.DescricaoAplicacao,
                                   EmDesenvolvimento = result.EmDesenvolvimento,
                                   IdLinxEmpresa = result.IdLinx,
                                   IdTcsAmbiente = result.IdTcsAmbiente,
                                   NomeEmpresa = result.NomeEmpresa,
                                   UidAplicacao = result.UidAplicacao,
                                   UidEmpresa = result.UidEmpresa,
                                   Url = result.Url,
                                   DescricaoAplicativo = result.DescricaoAplicativo,
                                   IdTcsAplicativo = result.IdTcsAplicativo
                               };
        }


        /// Execute before lookup on server side.
        public static void OnLookingUpLookUpTcsAmbiente1(ref IQueryable<LookUpTcsAmbiente1> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;

            Ambiente.AmbienteDomainService dsAmbiente = new Ambiente.AmbienteDomainService();
            searchDefinition = dsAmbiente.GetTcsAmbienteByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch })).Where(i => i.IdTcsAplicativo == 2).
                Select(i => new LookUpTcsAmbiente1
                {
                    DescricaoAmbienteRelacionado = i.DescricaoAmbiente,
                    NomeEmpresaAmbienteRelacionado = i.NomeEmpresa,
                    DescricaoAplicacaoAmbienteRelacionado = i.DescricaoAplicacao,
                    IdLinxAmbienteRelacionado = i.IdLinx,
                    IdTcsAmbienteRelacionado = i.IdTcsAmbiente,
                    IdAplicacao = i.IdAplicacao
                });


        }

        public static void OnSavedContextChanges(UsuarioAutorizacaoDomainService context, ChangeSetEntry[] entities)
        {
            //Revoga Licença License Server
            entities.Where(i => i.Entity is TcsUsuarioAcesso && (i.Operation == DomainOperation.Delete)).Select(i => i.Entity as TcsUsuarioAcesso).Select(i => new { i.IdLinxEmpresa, i.IdUsuario, i.TcsUsuarioAutenticacao.UidUsuario }).Distinct().ToList().ForEach(entity =>
            {
                Autorizacao.AutorizacaoDomainService ds = new Autorizacao.AutorizacaoDomainService();
                Autorizacao.UserInfo userInfo = ds.ValidateUserAccess(entity.UidUsuario, true);

                try
                {
                    if (context.GetTcsUsuarioAutenticacaoAcessoPNoAssociations().Where(i => i.IdUsuario == entity.IdUsuario && i.IdLinx == entity.IdLinxEmpresa).Count() == 0)
                    {
                        LicenseControl.Remove(userInfo.NomeAutenticacao, BusinessUserServiceHelper.GetCurrentUserName(), BusinessUserServiceHelper.GetCompanyUid(entity.IdLinxEmpresa));
                    }
                }
                catch { }
            });
        }
    }
}
