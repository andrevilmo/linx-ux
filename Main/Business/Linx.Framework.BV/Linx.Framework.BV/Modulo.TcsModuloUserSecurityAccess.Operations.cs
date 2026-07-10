using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Entity.Core.Objects;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using Linx;
using Linx.Framework.ControleSistema.BM;
using Linx.Framework.BV.Autorizacao;
using Linx.Framework.Autorizacao.BM;

namespace Linx.Framework.BV.Modulo
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class ModuloDomainService
    {
        [Query(HasSideEffects = true)]
        public IEnumerable<TcsModulo> GetTcsModuloByUserAccess(Guid uidUsuario, Int64 idModuloGrupo, Dictionary<string, string> headers = null)
        {
            List<TcsModuloAccess> allowedModules = new List<TcsModuloAccess>();
            int idTcsAplicativo = BusinessUserServiceHelper.GetCurrentApplicativeId(headers).GetValueOrDefault();
            Int64 idUsuario = BusinessUserServiceHelper.GetCurrentUserId(headers).GetValueOrDefault();
            try
            {
                AutorizacaoDomainService ds = new AutorizacaoDomainService(headers);

                //Validate User (Inativo - Vigência)
                ds.ValidateUserAccess(uidUsuario);

                ModuloAutorizacao.ModuloAutorizacaoDomainService ModuloAutorizacaoDs = new ModuloAutorizacao.ModuloAutorizacaoDomainService(headers);

                //Empresa
                List<Int64> modulosEmpresa = Utils.GetModulosPermitidos(true, headers);

                //TcsPerfil
                Transacao.TransacaoDomainService transacaoDs = new Transacao.TransacaoDomainService(headers);
                List<Int64> tcsPerfil = transacaoDs.GetTcsPerfilUsuario(idUsuario, headers);

                //TcsUsuarioRegraModulo - TcsPerfilRegraModulo
                Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService(headers);
                List<TcsModuloAccess> listaModuloUsuario = (from result in dsUsuario.GetTcsUsuarioRegraModuloNoAssociations().Where(i => i.IdUsuario == idUsuario)
                                                            select new TcsModuloAccess()
                                                            {
                                                                IdModulo = result.IdModulo,
                                                                RegraAcesso = result.LxRegraAcessoModulo,
                                                                Origem = 3
                                                            }).ToList();

                Perfil.PerfilDomainService dsPerfil = new Perfil.PerfilDomainService(headers);
                List<TcsModuloAccess> listaModuloPerfil = (from result in dsPerfil.GetTcsPerfilRegraModuloNoAssociations().Where(i => tcsPerfil.Contains(i.IdPerfil))
                                                           select new TcsModuloAccess()
                                                           {
                                                               IdModulo = result.IdModulo,
                                                               RegraAcesso = result.LxRegraAcessoModulo,
                                                               Origem = 4
                                                           }).ToList();

                List<TcsModuloAccess> listaModulo = (listaModuloUsuario.Union(listaModuloPerfil)).OrderBy(i => i.IdModulo).OrderBy(i => i.Origem).OrderBy(i => i.RegraAcesso).ToList();

                foreach (TcsModuloAccess item in listaModulo)
                {
                    List<TcsModuloAccess> moduloList = allowedModules.Where(i => i.IdModulo == item.IdModulo).ToList();
                    if (moduloList.Count() == 0)
                        allowedModules.Add(new TcsModuloAccess(item.IdModulo, item.RegraAcesso, item.Origem));
                    else
                    {
                        TcsModuloAccess modulo = moduloList.First();

                        if ((modulo.Origem == item.Origem) && (modulo.RegraAcesso < item.RegraAcesso && item.RegraAcesso == 2))
                            modulo.RegraAcesso = item.RegraAcesso;
                    }
                }

                allowedModules = allowedModules.Where(i => i.RegraAcesso != 1).ToList();

                Modulo.ModuloDomainService dsModulo = new ModuloDomainService(headers);

                List<Int64> tcsModulodoGrupo = (dsModulo.GetTcsModuloDoGrupoNoAssociations().Where(i => i.IdGrupoModulo == idModuloGrupo).Select(i => i.IdModulo).ToList());

                //TcsModulo - TcsModuloAutorizacao
                var modulosAut = (from result in ModuloAutorizacaoDs.GetTcsModuloAutorizacaoNoAssociations().Where(i => !i.Inativo && tcsModulodoGrupo.Contains(i.IdModulo))
                                  select new
                                  {
                                      IdModulo = result.IdModulo,
                                      DescModulo = result.DescModulo,
                                      NomeCurto = result.NomeCurto,
                                      Icone = result.Icone,
                                      LxCorFundo = result.LxCorFundo,
                                      NomeTabela = "TCS_MODULO_AUTORIZACAO",
                                      OrdemNavegacao = result.OrdemNavegacao,
                                      IdTcsAplicativo = result.IdTcsAplicativo
                                  }
                                  ).Where(i => modulosEmpresa.Contains(i.IdModulo)).ToList();

                var modulosLocal = (from result in dsModulo.GetTcsModuloNoAssociations().Where(i => !i.Inativo && i.IdTcsAplicativo == idTcsAplicativo && tcsModulodoGrupo.Contains(i.IdModulo))
                                        select new 
                                        {
                                            IdModulo = result.IdModulo,
                                            DescModulo = result.DescModulo,
                                            NomeCurto = result.NomeCurto,
                                            Icone = result.Icone,
                                            LxCorFundo = result.LxCorFundo,
                                            NomeTabela = "TCS_MODULO",
                                            OrdemNavegacao = result.OrdemNavegacao,
                                            IdTcsAplicativo = result.IdTcsAplicativo
                                        }).ToList();

                var modulos = modulosAut.Union(modulosLocal).Distinct().ToList();

                return
                    (from result in modulos
                     join result1 in allowedModules on result.IdModulo equals result1.IdModulo
                     select new TcsModulo()
                     {
                         IdModulo = result.IdModulo,
                         DescModulo = result.DescModulo,
                         NomeCurto = result.NomeCurto,
                         Icone = result.Icone,
                         LxCorFundo = result.LxCorFundo,
                         NomeTabela = result.NomeTabela,
                         IdTcsAplicativo = result.IdTcsAplicativo
                     }).OrderBy(i => i.OrdemNavegacao).ThenBy(i => i.DescModulo);

            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message, oException.InnerException);
            }
        }

        [Query(HasSideEffects = true)]
        public IEnumerable<TcsModuloMenu> GetUserTcsModuloMenu(Int64 idModulo, Dictionary<string, string> headers = null)
        {
            ModuloAutorizacao.ModuloAutorizacaoDomainService ModuloAutorizacaoDs = new ModuloAutorizacao.ModuloAutorizacaoDomainService(headers);
            Modulo.ModuloDomainService dsModulo = new ModuloDomainService(headers);

            return
                (
                    (from result in ModuloAutorizacaoDs.GetTcsModuloMenuAutorizacaoNoAssociations().Where(i => i.IdModulo == idModulo)
                     select new TcsModuloMenu()
                     {
                         DescModuloMenu = result.DescModuloMenu,
                         NomeCurto = result.NomeCurto,
                         DescModuloMenuSuperior = result.DescModuloMenuSuperior,
                         OrdemNavegacao = result.OrdemNavegacao,
                         IdModulo = result.IdModulo,
                         IdModuloMenu = result.IdModuloMenu,
                         IdModuloMenuSuperior = result.IdModuloMenuSuperior,
                         Icone = result.Icone,
                         LxCorFundo = result.LxCorFundo,
                         DescModulo = result.DescModulo,
                         NomeTabela = "TCS_MODULO_MENU_AUTORIZACAO",
                         IdTcsAplicativo = result.IdTcsAplicativo
                     }).ToList().Union
                    (from result in dsModulo.GetTcsModuloMenuNoAssociations().Where(i => i.IdModulo == idModulo)
                     select result).ToList()
                 ).OrderBy(i => i.OrdemNavegacao).ThenBy(i => i.DescModuloMenu);
        }

        [Query(HasSideEffects = false)]
        public IEnumerable<TcsModulo> GetTcsModuloByUserAccessJson(Guid uidUsuario, int idModuloGrupo)
        {
            return GetTcsModuloByUserAccess(uidUsuario, idModuloGrupo);
        }

        [Query(HasSideEffects = false)]
        public IEnumerable<TcsModuloMenu> GetUserTcsModuloMenuJson(Int64 idModulo)
        {
            return GetUserTcsModuloMenu(idModulo);
        }

        [Invoke(HasSideEffects = true)]
        public void CleanUserModulesCache()
        {
            Utils.RemoveModulesFromCache();
        }

        [Ignore()]
        public bool SyncFavorites(List<AppMenu> favorites)
        {
            ModuloDomainService ds = this;
            Guid userUid = BusinessUserServiceHelper.GetCurrentUserUid().GetValueOrDefault();
            Int64 userId = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();

            List<TcsUsuarioFavorito> favoritosList = (from result in ds.GetTcsUsuarioFavoritoNoAssociations().Where(i => i.IdUsuario == userId)
                                                      select result).ToList();

            byte ordem_navegacao = 0;
            int idFavorito = -1;

            foreach (AppMenu favorite in favorites)
            {
                TcsUsuarioFavorito current = null;
                bool isModule = false;

                if (favorite.IdModule == favorite.Id)
                {
                    isModule = true;
                    current = favoritosList.Where(i => i.IdModulo == favorite.IdModule && i.IdTransacao == null && i.IdModuloMenu == null).FirstOrDefault();
                }
                else if (favorite.IsTransaction)
                {
                    current = favoritosList.Where(i => i.IdModulo == favorite.IdModule && i.IdTransacao == favorite.Id && i.IdModuloMenu == null).FirstOrDefault();
                }
                else
                {
                    current = favoritosList.Where(i => i.IdModulo == favorite.IdModule && i.IdTransacao == null && i.IdModuloMenu == favorite.Id).FirstOrDefault();
                }

                if (current.IsNull())
                {
                    current = new TcsUsuarioFavorito()
                    {
                        IdModulo = favorite.IdModule,
                        IdTransacao = (isModule || !favorite.IsTransaction) ? (Int64?)null : favorite.Id,
                        IdModuloMenu = (isModule || favorite.IsTransaction) ? (Int64?)null : favorite.Id,
                        IdUsuario = userId,
                        IdTcsUsuarioFavorito = idFavorito
                    };
                    idFavorito--;
                }
                else
                {
                    favoritosList.Remove(current);
                }

                current.OrdemNavegacao = ordem_navegacao;
                ds.AddCustomChanges(current, null, ChangeOperation.Insert);
                ordem_navegacao++;
            }

            foreach (TcsUsuarioFavorito favorito in favoritosList)
            {
                ds.AddCustomChanges(favorito, null, ChangeOperation.Delete);
            }
            ds.SaveCustomChanges();

            Utils.RemoveUserModulesFromCache(userUid, BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault());

            return true;

        }

        [Ignore()]
        public bool AddUserFavorite(AppMenu favorite)
        {
            ModuloDomainService ds = this;
            Guid userUid = BusinessUserServiceHelper.GetCurrentUserUid().GetValueOrDefault();
            Int64 userId = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();

            byte ordemNavegacao = (from result in ds.GetTcsUsuarioFavoritoNoAssociations().Where(i => i.IdUsuario == userId)
                                   orderby result.OrdemNavegacao descending
                                   select result.OrdemNavegacao).FirstOrDefault();

            if (ordemNavegacao.IsNull())
                ordemNavegacao = 0;
            else
                ordemNavegacao++;

            TcsUsuarioFavorito favorito = new TcsUsuarioFavorito()
            {
                IdTcsUsuarioFavorito = -1,
                IdModulo = favorite.IdModule,
                IdTransacao = (favorite.IdModule == favorite.Id || !favorite.IsTransaction) ? (Int64?)null : favorite.Id,
                IdModuloMenu = (favorite.IdModule == favorite.Id || favorite.IsTransaction) ? (Int64?)null : favorite.Id,
                IdUsuario = userId,
                OrdemNavegacao = ordemNavegacao
            };

            ds.AddCustomChanges(favorito, null, ChangeOperation.Insert);
            ds.SaveCustomChanges();

            Utils.RemoveUserModulesFromCache(userUid, BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault());

            return true;
        }

        [Ignore()]
        public bool DeleteUserFavorite(AppMenu favorite)
        {
            ModuloDomainService ds = this;
            Guid UserUid = BusinessUserServiceHelper.GetCurrentUserUid().GetValueOrDefault();
            Int64 userId = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();

            Int64? idTransacao = favorite.IdModule == favorite.Id || !favorite.IsTransaction ? (Int64?)null : favorite.Id;
            Int64? idModuloMenu = favorite.IdModule == favorite.Id || favorite.IsTransaction ? (Int64?)null : favorite.Id;

            TcsUsuarioFavorito favorito = (from result in ds.GetTcsUsuarioFavoritoNoAssociations().Where(i => i.IdUsuario == userId && i.IdModulo == favorite.IdModule && i.IdTransacao == idTransacao && i.IdModuloMenu == idModuloMenu)
                                           select result).FirstOrDefault();

            if (!favorito.IsNull())
            {
                ds.AddCustomChanges(favorito, null, ChangeOperation.Delete);
                ds.SaveCustomChanges();
            }

            Utils.RemoveUserModulesFromCache(UserUid, BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault());

            return true;
        }

    }

    public class TcsModuloAccess
    {

        public TcsModuloAccess()
        {

        }

        public TcsModuloAccess(Int64 idModulo, int regraAcesso, int origem)
        {
            IdModulo = idModulo;
            RegraAcesso = regraAcesso;
            Origem = origem;

            //1 -> Tcs_Usuario_Regra_Transacao
            //2 -> Tcs_Perfil_Regra_Transacao
            //3 -> Tcs_Usuario_Regra_Modulo
            //4 -> Tcs_perfil_Regra_Modulo

        }
        [Key]
        public Int64 IdModulo { get; set; }
        public int RegraAcesso { get; set; }
        public int Origem { get; set; }
    }
}
