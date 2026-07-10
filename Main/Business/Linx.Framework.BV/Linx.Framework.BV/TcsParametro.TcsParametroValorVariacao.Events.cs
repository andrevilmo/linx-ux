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

namespace Linx.TCS0101.BO.TcsParametro
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Business Events Definition //////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsParametroValorVariacao
	{
        /// Execute before save context changes.
        public static void OnSavingContextChanges(TcsParametroDomainService context, ChangeSetEntry[] entities)
        {
            //Verifica se todos os valores possuem variação.
            var query = from result in entities.Where(i => i.Entity is TcsParametroValorVariacao && (i.Operation == DomainOperation.Insert || i.Operation == DomainOperation.Update)).Select(i => i.Entity as TcsParametroValorVariacao)
                        where result.TcsParametroChaveSelecaoList.Count() == 0
                        select result;

            if (query.Count() > 0)
            {
                throw new DomainException("É necessário informar a variação para todos os valores.".Translate());
            }
        }
    }
}
