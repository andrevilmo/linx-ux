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

namespace Linx.Framework.BV.Usuario
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioRegraModulo
    {
        /// Execute before lookup on server side.
        public static void OnLookUpingLookUpTcsUsuarioRegraModulo(ref IQueryable<LookUpTcsUsuarioRegraModulo> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            searchDefinition =
                from result in Utils.GetLookUpModulo(entitySearch)
                select new LookUpTcsUsuarioRegraModulo()
                {
                    IdModulo = result.IdModulo,
                    DescModulo = result.DescModulo,
                    DescAplicativo = result.DescAplicativo
                };
        }

        /// Execute after save context changes.
        public static void OnSavedContextChanges(UsuarioDomainService context, ChangeSetEntry[] entities)
        {
            entities.Where(i => i.Entity is TcsUsuarioRegraModulo && i.Operation != DomainOperation.None).Select(i => ((TcsUsuarioRegraModulo)i.Entity).TcsUsuario.UidUsuario).Distinct().ToList().ForEach(uidUsuario =>
            {
                Utils.RemoveUserModulesFromCache(uidUsuario, BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault());
            });
        }
    }
}
