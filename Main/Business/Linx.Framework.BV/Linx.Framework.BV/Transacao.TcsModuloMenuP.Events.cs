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
    public partial class TcsModuloMenuP
    {
        /// Replace the automatic search method.
        public static IQueryable<TcsModuloMenuP> OnSearchingReplacement(ControleSistemaContext context, string dynQuery, List<ObjectParameter> parameters, List<EntitySearch> entitySearchList)
        {
            string tcsModuloWhere = "it.DESC_MODULO LIKE '%'";
            string tcsModuloMenuWhere = "it.DESC_MODULO_MENU LIKE '%'";

            foreach (EntitySearch entity in entitySearchList)
            {
                EntitySearchExpression expression = entity.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "DescModulo").FirstOrDefault();

                if (!expression.IsNull())
                {
                    int fieldPosition = entity.Expressions.IndexOf(expression);
                    tcsModuloWhere = string.Format("it.DESC_MODULO {0} '{1}'", entity.Expressions[fieldPosition + 1].Value, entity.Expressions[fieldPosition + 2].Value);
                }

                expression = entity.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "DescModuloMenu").FirstOrDefault();

                if (!expression.IsNull())
                {
                    int fieldPosition = entity.Expressions.IndexOf(expression);
                    tcsModuloMenuWhere = string.Format("it.DESC_MODULO_MENU {0} '{1}'", entity.Expressions[fieldPosition + 1].Value, entity.Expressions[fieldPosition + 2].Value);
                }

            }

            IQueryable<TcsModuloMenuP> result =
                (from entity1 in context.TCS_MODULO.Where(tcsModuloWhere)
                 join entity0 in context.TCS_MODULO_MENU.Where(tcsModuloMenuWhere) on entity1.ID_MODULO equals entity0.ID_MODULO
                 select new TcsModuloMenuP()
                 {
                     DescModuloMenu = entity0.DESC_MODULO_MENU,
                     IdModulo = entity0.ID_MODULO,
                     IdModuloMenu = entity0.ID_MODULO_MENU,
                     DescModulo = entity1.DESC_MODULO,
                     IdTcsAplicativo = entity1.ID_TCS_APLICATIVO
                 }
                );


            return result;
        }
    }
}
