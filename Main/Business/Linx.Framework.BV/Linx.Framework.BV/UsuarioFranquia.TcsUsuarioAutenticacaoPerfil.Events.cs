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
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;

namespace Linx.Framework.BV.UsuarioFranquia
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioAutenticacaoPerfil
    {
        public static IEnumerable<TcsUsuarioAutenticacaoPerfil> OnSearchingReplacement(List<EntitySearch> entitySearchList)
        {
            string strEmpresa;
            string strAmbiente;
            Int64 userId = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();

            EntitySearch entitySearch = entitySearchList[0];

            EntitySearchExpression expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdLinx").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);
                int idLinx = Convert.ToInt32(entitySearch.Expressions[fieldPos + 2].Value.ToString());

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

            expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdUsuario").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);
                userId = Convert.ToInt64(entitySearch.Expressions[fieldPos + 2].Value.ToString());
            }

            Dictionary<string, string> headers = new Dictionary<string, string>
                    {
                        {"EconomicGroup", strEmpresa },
                        {"CurrentCompany", strEmpresa },
                        {"Environment", strAmbiente}
                    };

            string serializedEntitySearch = SerializationManager<List<EntitySearch>>.ObjectToString(entitySearchList);
            string repSerializedEntitySearch = serializedEntitySearch;
            repSerializedEntitySearch = EntitySearch.FilterExpressionFields(repSerializedEntitySearch, "TcsUsuarioAutenticacaoPerfil", "TcsUsuarioPerfil", 0, "DescPerfil#DescPerfil", "IdLinxPerfil#IdLinxPerfil", "IdPerfil#IdPerfil", "IdTcsUsuarioPerfil#IdTcsUsuarioPerfil", "IdUsuario#IdUsuario", "NomeUsuario#NomeUsuario", "Inativo#Inativo");


            UsuarioFranquiaDomainService ds = new UsuarioFranquiaDomainService(headers);

            var perfil = ds.GetTcsUsuarioPerfilByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch> { entitySearch }))
                .Where(i => !i.Inativo && i.IdUsuario == userId);

            Linx.Framework.BV.Empresa.EmpresaDomainService serviceContext0 = new Linx.Framework.BV.Empresa.EmpresaDomainService(headers);

            IEnumerable<TcsUsuarioAutenticacaoPerfil> result =
                (
                 from TcsUsuarioPerfil_Rep1 in perfil.ToArray()
                 join TcsEmpresaAutenticacaoP_Rep1 in serviceContext0.GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(EntitySearch.FilterExpressionFields(serializedEntitySearch, "TcsUsuarioAutenticacaoPerfil", "TcsEmpresaAutenticacaoP", 1, "IdLinx#IdLinx", "NomeEmpresa#NomeEmpresa", "UidEmpresa#UidEmpresa")).ToArray() on TcsUsuarioPerfil_Rep1.IdLinxPerfil equals TcsEmpresaAutenticacaoP_Rep1.IdLinx


                 select new TcsUsuarioAutenticacaoPerfil()
                 {

                     DescPerfil = TcsUsuarioPerfil_Rep1.DescPerfil
                 ,
                     IdLinx = TcsEmpresaAutenticacaoP_Rep1.IdLinx
                 ,
                     IdLinxPerfil = TcsUsuarioPerfil_Rep1.IdLinxPerfil
                 ,
                     IdPerfil = TcsUsuarioPerfil_Rep1.IdPerfil
                 ,
                     IdTcsUsuarioPerfil = TcsUsuarioPerfil_Rep1.IdTcsUsuarioPerfil
                 ,
                     IdUsuario = TcsUsuarioPerfil_Rep1.IdUsuario
                 ,
                     NomeEmpresa = TcsEmpresaAutenticacaoP_Rep1.NomeEmpresa
                 ,
                     NomeUsuario = TcsUsuarioPerfil_Rep1.NomeUsuario
                 ,
                     UidEmpresa = TcsEmpresaAutenticacaoP_Rep1.UidEmpresa

                 }
                );

            return result;
        }
    }
}
