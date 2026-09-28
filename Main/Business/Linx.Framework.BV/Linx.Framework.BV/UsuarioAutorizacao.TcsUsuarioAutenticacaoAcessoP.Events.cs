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

            EntitySearch userFilter = new EntitySearch();
            userFilter.EntityName = string.Empty;
            userFilter.Expressions.Add(new EntitySearchExpression("Field", "IdUsuario"));
            userFilter.Expressions.Add(new EntitySearchExpression("Operator", "=="));
            userFilter.Expressions.Add(new EntitySearchExpression("Value", userId));

            List<EntitySearch> searchList = new List<EntitySearch>();
            searchList.Add(userFilter);

            List<LookUpTcsAmbiente2> result = new List<LookUpTcsAmbiente2>();
            foreach (TcsUsuarioAutenticacaoAcessoP row in ds.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(searchList)))
            {
                LookUpTcsAmbiente2 item = new LookUpTcsAmbiente2();
                item.DescricaoAmbiente = row.DescricaoAmbiente;
                item.DescricaoAplicativo = row.DescricaoAplicativo;
                item.NomeEmpresa = row.NomeEmpresa;
                item.DescricaoAplicacao = row.DescricaoAplicacao;
                item.IdTcsAmbiente = row.IdTcsAmbiente;
                item.IdAplicacao = row.IdAplicacao;
                item.IdTcsAplicativo = row.IdTcsAplicativo;
                item.IdLinx = row.IdLinx;
                result.Add(item);
            }

            searchDefinition = result.AsQueryable();
        }

        /// <summary>
        /// Cadastro Usuario Local must list environments of the user being edited.
        /// When the client sends IdUsuario in the lookup filter, use that id; otherwise keep the previous session-user fallback.
        /// Implemented without lambdas so the method can be published onto an existing Service assembly.
        /// </summary>
        public static Int64 ResolveLookUpTcsAmbiente2UserId(EntitySearch entitySearch)
        {
            Int64 userId = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();
            if (entitySearch == null || entitySearch.Expressions == null)
                return userId;

            EntitySearchExpression expression = null;
            int fieldPos = -1;
            for (int i = 0; i < entitySearch.Expressions.Count; i++)
            {
                EntitySearchExpression candidate = entitySearch.Expressions[i];
                if (candidate != null
                    && candidate.Name == "Field"
                    && candidate.Value != null
                    && candidate.Value.ToString() == "IdUsuario")
                {
                    expression = candidate;
                    fieldPos = i;
                    break;
                }
            }

            if (expression.IsNull() || fieldPos < 0)
                return userId;

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
