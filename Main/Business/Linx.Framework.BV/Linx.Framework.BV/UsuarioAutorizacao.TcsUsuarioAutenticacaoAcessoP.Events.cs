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

namespace Linx.Framework.BV.UsuarioAutorizacao
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioAutenticacaoAcessoP
    {
        public static void OnLookingUpLookUpTcsAmbiente2(ref IQueryable<LookUpTcsAmbiente2> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            Int64 userId = ResolveLookUpTcsAmbiente2UserId(entitySearch);
            UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacaoDomainService();
            //List<int> environments = ds.GetTcsUsuarioAutenticacaoAcessoPNoAssociations().Where(i => i.IdUsuario == userId).Select(i => i.IdTcsAmbiente).ToList();
            //searchDefinition = searchDefinition.Where(i => environments.Contains(i.IdTcsAmbiente));

            searchDefinition = ds.GetTcsUsuarioAutenticacaoAcessoPNoAssociations().Where(i => i.IdUsuario == userId).Select(i => new LookUpTcsAmbiente2
            {
                DescricaoAmbiente = i.DescricaoAmbiente,
                DescricaoAplicativo = i.DescricaoAplicativo,
                NomeEmpresa = i.NomeEmpresa,
                DescricaoAplicacao = i.DescricaoAplicacao,
                IdTcsAmbiente = i.IdTcsAmbiente,
                IdAplicacao = i.IdAplicacao,
                IdTcsAplicativo = i.IdTcsAplicativo,
                IdLinx = i.IdLinx
            });
        }

        /// <summary>
        /// Cadastro Usuario Local must list environments of the user being edited.
        /// When the client sends IdUsuario in the lookup filter, use that id; otherwise keep the previous session-user fallback.
        /// </summary>
        public static Int64 ResolveLookUpTcsAmbiente2UserId(EntitySearch entitySearch)
        {
            Int64 userId = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();
            if (entitySearch == null || entitySearch.Expressions == null)
                return userId;

            EntitySearchExpression expression = entitySearch.Expressions.FirstOrDefault(i => i.Name == "Field" && i.Value != null && i.Value.ToString() == "IdUsuario");
            if (expression.IsNull())
                return userId;

            int fieldPos = entitySearch.Expressions.IndexOf(expression);
            if ((fieldPos + 2) < entitySearch.Expressions.Count
                && entitySearch.Expressions[fieldPos + 1].Name == "Operator"
                && entitySearch.Expressions[fieldPos + 2].Name == "Value"
                && entitySearch.Expressions[fieldPos + 2].Value != null)
            {
                userId = Convert.ToInt64(entitySearch.Expressions[fieldPos + 2].Value);
                Utils.RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);
            }

            return userId;
        }

        public static void OnLookingUpLookUpTcsAmbiente2Relacionado(ref IQueryable<LookUpTcsAmbiente2Relacionado> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;
            UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacaoDomainService();
            searchDefinition = ds.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch })).Where(i => i.IdTcsAplicativo == 2).
                Select(i => new LookUpTcsAmbiente2Relacionado
            {
                DescricaoAmbienteRelacionado = i.DescricaoAmbiente,
                DescricaoAplicativo = i.DescricaoAplicativo,
                DescricaoAplicacao = i.DescricaoAplicacao,
                IdTcsAmbienteRelacionado = i.IdTcsAmbiente,
            }).Distinct();

        }
    }
}
