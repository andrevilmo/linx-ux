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

namespace Linx.Framework.BV.UsuarioAutorizacao
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioAutenticacaoAcessoP
    {
        public static void OnLookingUpLookUpTcsAmbiente2(ref IQueryable<LookUpTcsAmbiente2> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            Int64 userId = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();
            UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacaoDomainService();
            //List<int> environments = ds.GetTcsUsuarioAutenticacaoAcessoPNoAssociations().Where(i => i.IdUsuario == userId).Select(i => i.IdTcsAmbiente).ToList();
            //searchDefinition = searchDefinition.Where(i => environments.Contains(i.IdTcsAmbiente));

            searchDefinition = ds.GetTcsUsuarioAutenticacaoAcessoPNoAssociations().Where(i => i.IdUsuario == userId).Select(i => new LookUpTcsAmbiente2
            {
                DescricaoAmbiente = i.DescricaoAmbiente,
                DescricaoAplicativo = i.DescricaoAplicativo,
                NomeEmpresa = i.NomeEmpresa,
                DescricaoAplicacao = i.DescricaoAplicacao,
                IdTcsAmbiente = i.IdTcsAmbiente,
                IdAplicacao = i.IdAplicacao,
                IdTcsAplicativo = i.IdTcsAplicativo,
                IdLinx = i.IdLinx
            });
        }

        public static void OnLookingUpLookUpTcsAmbiente2Relacionado(ref IQueryable<LookUpTcsAmbiente2Relacionado> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;
            UsuarioAutorizacaoDomainService ds = new UsuarioAutorizacaoDomainService();
            searchDefinition = ds.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch })).Where(i => i.IdTcsAplicativo == 2).
                Select(i => new LookUpTcsAmbiente2Relacionado
            {
                DescricaoAmbienteRelacionado = i.DescricaoAmbiente,
                DescricaoAplicativo = i.DescricaoAplicativo,
                DescricaoAplicacao = i.DescricaoAplicacao,
                IdTcsAmbienteRelacionado = i.IdTcsAmbiente,
            }).Distinct();

        }
    }
}
