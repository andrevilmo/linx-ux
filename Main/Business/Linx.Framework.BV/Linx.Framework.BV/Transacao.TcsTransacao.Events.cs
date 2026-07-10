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
using Linx.Framework.ControleSistema.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;

namespace Linx.Framework.BV.Transacao
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsTransacao
    {
        /// Execute before lookup on server side.
        public static void OnLookUpingLookUpTcsObjetoTransacao(ref IQueryable<LookUpTcsObjetoTransacao> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            searchDefinition =
                (from result in Utils.GetLookUpObjeto(entitySearch)
                 select new LookUpTcsObjetoTransacao()
                 {
                     IdObjeto = result.IdObjeto,
                     DescObjeto = result.DescObjeto,
                     ClasseNome = result.ClasseNome
                 });
        }

        /// Execute before search data.
        public static void OnSearching(ref IQueryable<TcsTransacao> searchDefinition, bool noAssociations, List<EntitySearch> searchList)
        {
            if (searchList.IsNull())
                return;

            EntitySearch entity = searchList.Where(i => i.EntityName == "TcsTransacao").FirstOrDefault();

            if (!entity.IsNull())
            {
                EntitySearchExpression expression = entity.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "DescObjeto").FirstOrDefault();

                if (expression.IsNull())
                    return;

                int fieldPosition = entity.Expressions.IndexOf(expression);

                EntitySearch search = new EntitySearch();
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "DescObjeto"));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, entity.Expressions[fieldPosition + 1].Value));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, entity.Expressions[fieldPosition + 2].Value));

                List<Int64> query = (from result in Utils.GetLookUpObjeto(search)
                                    select result.IdObjeto).ToList();

                searchDefinition = searchDefinition.Where(i => query.Contains(i.IdObjeto));
            }
        }

        /// Execute after save context changes.
        public static void OnSavedContextChanges(TransacaoDomainService context, ChangeSetEntry[] entities)
        {
            if (entities.Where(i => i.Entity is TcsTransacao && i.Operation != DomainOperation.None).Count() > 0)
                Utils.RemoveModulesFromCache();
        }
    }
}
