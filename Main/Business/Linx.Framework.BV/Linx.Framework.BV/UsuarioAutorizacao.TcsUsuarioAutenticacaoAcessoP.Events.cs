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
            if (entitySearch == null)
                entitySearch = new EntitySearch();

            Int64 userId = LookUpTcsAmbiente2Helper.ExtractEditedUserId(entitySearch);

            UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacaoDomainService();
            List<int> assignedEnvironments = LookUpTcsAmbiente2Helper.GetAssignedEnvironmentIds(ds, userId);

            Ambiente.AmbienteDomainService dsAmbiente = new Ambiente.AmbienteDomainService();
            IQueryable<Ambiente.TcsAmbiente> environments = dsAmbiente.GetTcsAmbienteByEntitySearchNoAssociations(
                SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }));

            if (assignedEnvironments.Count > 0)
                environments = environments.Where(i => !assignedEnvironments.Contains(i.IdTcsAmbiente));

            searchDefinition = environments.Select(i => new LookUpTcsAmbiente2
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

    public static class LookUpTcsAmbiente2Helper
    {
        public static Int64 ExtractEditedUserId(EntitySearch entitySearch)
        {
            if (entitySearch == null)
                return 0;

            entitySearch.EntityName = string.Empty;

            EntitySearchExpression expression = entitySearch.Expressions
                .Where(i => i.Name == "Field" && i.Value != null && i.Value.ToString() == "IdUsuario")
                .FirstOrDefault();

            if (expression.IsNull())
                return 0;

            int fieldPos = entitySearch.Expressions.IndexOf(expression);
            Int64 userId = Convert.ToInt64(entitySearch.Expressions[fieldPos + 2].Value.ToString());
            Utils.RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);
            return userId;
        }

        public static List<int> GetAssignedEnvironmentIds(UsuarioAutorizacaoDomainService ds, Int64 userId)
        {
            if (ds == null || userId == 0)
                return new List<int>();

            return ds.GetTcsUsuarioAutenticacaoAcessoPNoAssociations()
                .Where(i => i.IdUsuario == userId)
                .Select(i => i.IdTcsAmbiente)
                .Distinct()
                .ToList();
        }

        public static List<int> GetUnassignedEnvironmentIds(IEnumerable<int> allEnvironmentIds, IEnumerable<int> assignedEnvironmentIds)
        {
            HashSet<int> assigned = new HashSet<int>(assignedEnvironmentIds ?? Enumerable.Empty<int>());
            return (allEnvironmentIds ?? Enumerable.Empty<int>())
                .Where(id => !assigned.Contains(id))
                .ToList();
        }
    }
}
