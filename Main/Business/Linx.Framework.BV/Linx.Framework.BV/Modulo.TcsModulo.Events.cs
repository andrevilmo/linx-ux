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

namespace Linx.Framework.BV.Modulo
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsModulo
    {
        /// Execute on transaction starting.
        public void OnTransactingChanges(ModuloDomainService context, ChangeOperation changeOperation)
        {
            if (changeOperation == ChangeOperation.Delete)
            {
                foreach (TcsModuloMenu moduloMenu in this.TcsModuloMenuList)
                {
                    context.AddCustomChanges(moduloMenu, null, ChangeOperation.Delete);

                    foreach (TcsTransacaoMenu transacaoMenu in moduloMenu.TcsTransacaoMenuList)
                    {
                        context.AddCustomChanges(transacaoMenu, null, ChangeOperation.Delete);
                    }
                }
            }
        }

        /// Execute after save context changes.
        public static void OnSavedContextChanges(ModuloDomainService context, ChangeSetEntry[] entities)
        {
            if (entities.Where(i => i.Entity is TcsModulo && i.Operation != DomainOperation.None).Count() > 0)
                Utils.RemoveModulesFromCache();
        }
    }
}
