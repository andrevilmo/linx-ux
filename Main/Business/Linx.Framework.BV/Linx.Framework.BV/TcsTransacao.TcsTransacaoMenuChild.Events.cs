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

namespace Linx.TCS0101.BO.TcsTransacao
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Business Events Definition //////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsTransacaoMenuChild
	{
        /// Execute before lookup on server side.
        public static void OnLookUpingLookUpTcsTransacaoMenuChildTcsModuloMenu(ref IQueryable<LookUpTcsTransacaoMenuChildTcsModuloMenu> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            searchDefinition =
                (from result in Utils.GetLookUpModuloMenu(entitySearch)
                 select new LookUpTcsTransacaoMenuChildTcsModuloMenu()
                 {
                     UidModulo = result.UidModulo,
                     UidModuloMenu = result.UidModuloMenu,
                     DescModulo = result.DescModulo,
                     DescModuloMenu = result.DescModuloMenu
                 });
        }
    }
}
