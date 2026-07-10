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

namespace Linx.Framework.BV.UsuarioFranquia
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioPerfil
    {
        public static void OnLookingUpLookUpTcsPerfil(ref IQueryable<LookUpTcsPerfil> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            string strEmpresa;
            string strAmbiente;
            List<Int64> lstPerfil = new List<long>();

            EntitySearchExpression expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdLinx").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);
                int idLinx = Convert.ToInt32(entitySearch.Expressions[fieldPos + 2].Value.ToString());

                //remove expressions from list
                Utils.RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);

                Ambiente.AmbienteDomainService dsAmbiente = new Ambiente.AmbienteDomainService();
                Ambiente.TcsAmbiente tcsAmbiente = dsAmbiente.GetTcsAmbienteNoAssociations().Where(i => i.IdLinx == idLinx).FirstOrDefault();

                strEmpresa = tcsAmbiente.UidEmpresa.ToString();
                strAmbiente = tcsAmbiente.IdTcsAmbiente.ToString();

            }
            else
            {
                strEmpresa = BusinessUserServiceHelper.GetCurrentCompanyId().ToString();
                strAmbiente = BusinessUserServiceHelper.GetCurrentEnvironmentId().ToString();
            }

            expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdPerfil").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);

                string[] aPerfil = entitySearch.Expressions[fieldPos + 2].Value.ToString().Split(new string[] { ":" }, StringSplitOptions.None);
                foreach (string item in aPerfil)
                {
                    lstPerfil.Add(Convert.ToInt64(item));
                }

                //remove expressions from list
                Utils.RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);

            }

            Dictionary<string, string> headers = new Dictionary<string, string>
                    {
                        {"EconomicGroup", strEmpresa },
                        {"CurrentCompany", strEmpresa },
                        {"Environment", strAmbiente}
                    };

            Int64 userId = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();
            UsuarioFranquiaDomainService ds = new UsuarioFranquiaDomainService(headers) { IsSecure = true };

            searchDefinition = ds.GetTcsUsuarioPerfilByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch> { entitySearch }))
                .Where(i => !i.Inativo && i.IdUsuario == userId && !lstPerfil.Contains(i.IdPerfil)).Select(i => new LookUpTcsPerfil { IdPerfil = i.IdPerfil, DescPerfil = i.DescPerfil }).Distinct();
        }
    }
}
