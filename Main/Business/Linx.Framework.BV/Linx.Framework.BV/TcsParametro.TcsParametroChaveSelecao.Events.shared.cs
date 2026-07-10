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
using System.Xml.Serialization;

namespace Linx.TCS0101.BO.TcsParametro
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Business Events Definition //////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsParametroChaveSelecao
	{
        /// Execute before lookup on client side.
        public EntitySearch BeforeGetLookUpTcsTabelaAutorizacaoBQuery()
        {
            EntitySearch search = new EntitySearch();

            this.TcsParametroValorVariacao.TcsParametro.TcsParametroTabelaSelecaoList.ToList().ForEach(ret =>
            {
                if (search.Expressions.Count () > 0)
                    search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "||"));

                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidTabela"));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, ret.UidTabela));
            });

            if (search.Expressions.Count() == 0)
            {
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidTabela"));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, Guid.Empty));
            }

            return search;
        }
    }
}
