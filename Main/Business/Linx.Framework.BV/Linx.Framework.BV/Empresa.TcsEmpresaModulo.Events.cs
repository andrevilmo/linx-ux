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
using Linx.Framework.Autorizacao.BM;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;

namespace Linx.Framework.BV.Empresa
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsEmpresaModulo
    {
        /// Execute on transaction context ending.
        public static void OnTransactedContextChanges(EmpresaDomainService context, ChangeSetEntry[] entities)
        {
            TcsEmpresaModulo empresaModulo = entities.Where(i => i.Entity is TcsEmpresaModulo && i.Operation != DomainOperation.None).Select(i => i.Entity as TcsEmpresaModulo).FirstOrDefault();
            if (!empresaModulo.IsNullOrEmpty())
            {
                Ambiente.AmbienteDomainService dsAmbiente = new Ambiente.AmbienteDomainService();
                dsAmbiente.GetTcsAmbienteNoAssociations().Where(i => i.IdLinx == empresaModulo.IdLinx).Select(i => i.IdTcsAmbiente).ToList().Foreach(idAmbiente =>
                    {
                        Utils.RemoveModulesFromCache(idAmbiente);
                    });
            }
        }
    }
}
