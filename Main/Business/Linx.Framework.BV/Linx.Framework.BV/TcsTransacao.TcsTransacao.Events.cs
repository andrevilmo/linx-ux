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
#if !SILVERLIGHT
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
#endif
using System.Text;
using System.Data.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Objects.DataClasses;
using Linx.Framework.ControleSistema.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;

namespace Linx.TCS0101.BO.TcsTransacao
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
                     UidObjeto = result.UidObjeto,
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

                List<Guid> query = (from result in Utils.GetLookUpObjeto(search)
                             select result.UidObjeto).ToList();

                searchDefinition = searchDefinition.Where(i => query.Contains(i.UidObjeto));
            }
        }
    }
}
