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
using Linx.Framework.ControleSistema.BM;
using Linx.Business.Tools;
using System.ServiceModel.DomainServices.Server;
using Linx.Framework.BV.Modulo;
using Linx.Framework.BV.Domains;

namespace Linx.Framework.Custom.BV.PerfilFranquia
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsPerfilRegraModulo
    {
        public static void OnLookingUpLookUpTcsPerfilRegraModulo(ref IQueryable<LookUpTcsPerfilRegraModulo> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;
            string serializedString = SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch });
            Int64 idUsuario = UserServiceHelper.GetCurrentUserId().GetValueOrDefault();

            Linx.Framework.BV.ModuloAutorizacao.ModuloAutorizacaoDomainService dsModuloAut = new Framework.BV.ModuloAutorizacao.ModuloAutorizacaoDomainService();
            Linx.Framework.BV.Modulo.ModuloDomainService dsModulo = new Framework.BV.Modulo.ModuloDomainService();

            var modulos =
                dsModuloAut.GetTcsModuloAutorizacaoByEntitySearchNoAssociations(serializedString).Select(i => new { i.IdModulo, i.DescModulo, i.IdTcsAplicativo, i.DescricaoAplicativo, Origem = "Portal" }).ToList().Union(
                dsModulo.GetTcsModuloByEntitySearchNoAssociations(serializedString).Where(i => i.IdTcsAplicativo == 1).Select(i => new { i.IdModulo, i.DescModulo, i.IdTcsAplicativo, DescricaoAplicativo = "", Origem = "Local" }).ToList()).ToList();

            List<long> modulosFiltro = modulos.Select(i => i.IdModulo).Distinct().ToList();

            PerfilFranquiaDomainService ds = new PerfilFranquiaDomainService();
            List<Int64> perfis = ds.GetPerfilList(idUsuario);
            Linx.Framework.BV.Usuario.UsuarioDomainService dsUsuario = new Framework.BV.Usuario.UsuarioDomainService();

            var modulosUsuario = dsUsuario.GetTcsUsuarioRegraModuloNoAssociations().Where(i => i.IdUsuario == idUsuario && modulosFiltro.Contains(i.IdModulo)).Select(i => new { i.IdModulo, i.LxRegraAcessoModulo, Origem = 0 }).ToList();
            var modulosPerfil = ds.GetTcsPerfilRegraModuloNoAssociations().Where(i => perfis.Contains(i.IdPerfil) && modulosFiltro.Contains(i.IdModulo)).Select(i => new { i.IdModulo, i.LxRegraAcessoModulo, Origem = 1 }).ToList();
            var userModulos = (modulosUsuario.Union(modulosPerfil)).OrderBy(i => i.IdModulo).OrderBy(i => i.Origem).OrderBy(i => i.LxRegraAcessoModulo).ToList();

            List<TcsModuloAccess> allowedModules = new List<TcsModuloAccess>();

            foreach (var item in userModulos)
            {
                TcsModuloAccess modulo = allowedModules.Where(i => i.IdModulo == item.IdModulo).FirstOrDefault();

                if (modulo.IsNull())
                {
                    allowedModules.Add(new TcsModuloAccess() { IdModulo = item.IdModulo, Origem = item.Origem, RegraAcesso = item.LxRegraAcessoModulo });
                }
                else
                {
                    if ((modulo.Origem == item.Origem) && (modulo.RegraAcesso < item.LxRegraAcessoModulo && item.LxRegraAcessoModulo == 2))
                        modulo.RegraAcesso = item.LxRegraAcessoModulo;
                }
            }

            allowedModules = allowedModules.Where(i => i.RegraAcesso != 1).ToList();

            searchDefinition = (from result in allowedModules
                                join result1 in modulos on result.IdModulo equals result1.IdModulo
                                select new LookUpTcsPerfilRegraModulo
                                {
                                    IdModulo = result.IdModulo,
                                    DescModulo = result1.DescModulo,
                                    DescAplicativo = DescricaoAplicativo(result1.IdTcsAplicativo, result1.DescricaoAplicativo),
                                    Origem = result1.Origem
                                }).AsQueryable();
        }

        private static string DescricaoAplicativo(int idTcsAplicativo, string descAplicativo)
        {
            if (!descAplicativo.IsNullOrEmpty())
            {
                return descAplicativo;
            }
            else
            {
                return Linx.Framework.BV.Utils.GetDescAplicativo(idTcsAplicativo);
            }
        }

        public static void OnSavedContextChanges(PerfilFranquiaDomainService context, ChangeSetEntry[] entities)
        {
            if (entities.Where(i => i.Entity is TcsPerfilRegraModulo && i.Operation != DomainOperation.None).Count() > 0)
                Framework.BV.Utils.RemoveModulesFromCache();

        }

        public static void OnLookingUpLookUpLxRegraAcessoModulo(ref IQueryable<LookUpLxRegraAcessoModulo> searchDefinition, string propertyName, EntitySearch entitySearch)
        {
            searchDefinition = Utils.GetRegraAcessoModulo(entitySearch).Select(i => new LookUpLxRegraAcessoModulo() { LxRegraAcessoModulo = i.RegraAcesso, LxRegraAcessoModuloName = i.RegraAcessoName }).AsQueryable();
        }

    }
}
