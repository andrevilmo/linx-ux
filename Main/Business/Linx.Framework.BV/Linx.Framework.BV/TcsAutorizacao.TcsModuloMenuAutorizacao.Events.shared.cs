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

namespace Linx.TCS0101.BO.TcsAutorizacao
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsModuloMenuAutorizacao
    {
        /// Execute before lookup on client side.
        public EntitySearch BeforeGetLookUpModuloMenuSuperiorQuery()
        {
            EntitySearch search = new EntitySearch();

            if (!this.TcsModuloAutorizacao.UidModulo.IsNullOrEmpty())
            {
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "UidModulo"));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.TcsModuloAutorizacao.UidModulo));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "DescModuloMenuSuperior"));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "!="));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.DescModuloMenu));
            }
            return search;
        }
    }
}
