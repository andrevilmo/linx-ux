using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Linx.Data;
using System.Text;
using System.Data.Entity.Core.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Reflection;
using Linx.Framework.Autorizacao.BM;
using System.ServiceModel.DomainServices.Server;

namespace Linx.Framework.BV.UsuarioAutorizacao
{
    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsUsuarioGpecon
    {
        public static void OnLookingUpLookUpTcsEmpresaAutenticacao(ref IQueryable<LookUpTcsEmpresaAutenticacao> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            Empresa.EmpresaDomainService ds = new Empresa.EmpresaDomainService();
            entitySearch.EntityName = "";

            searchDefinition = from result in ds.GetTcsEmpresaGpeconByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch> { entitySearch }))
                               select new LookUpTcsEmpresaAutenticacao
                               {
                                   IdLinx = result.IdLinxGpecon,
                                   NomeEmpresa = result.GrupoEconomico
                               };
        }

        public static void OnSavedContextChanges(UsuarioAutorizacaoDomainService context, ChangeSetEntry[] entities)
        {
            entities.Where(i => i.Entity is TcsUsuarioGpecon && i.Operation != DomainOperation.None).Select(i => ((TcsUsuarioGpecon)i.Entity).TcsUsuarioAutenticacao.UidUsuario).Distinct().ToList().ForEach(uidUsuario =>
            {
                Utils.RemoveGpeconInfoFromCache(uidUsuario);
            });

        }
    }
}
