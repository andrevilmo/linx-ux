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
using System.Xml.Serialization;

namespace Linx.Framework.BV.ModuloAutorizacao
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

            if (!this.TcsModuloAutorizacao.IdModulo.IsNullOrEmpty())
            {
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "IdModulo"));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.TcsModuloAutorizacao.IdModulo));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "DescModuloMenuSuperior"));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "!="));
                search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, this.DescModuloMenu));
            }
            return search;
        }
    }
}
