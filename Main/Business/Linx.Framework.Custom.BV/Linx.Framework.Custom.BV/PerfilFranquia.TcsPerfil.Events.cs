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

namespace Linx.Framework.Custom.BV.PerfilFranquia
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Business Events Definition //////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsPerfil
    {
        public static void OnSearching(ref IQueryable<TcsPerfil> searchDefinition, bool noAssociations, List<EntitySearch> searchList)
        {
            Int32 idGpecon = UserServiceHelper.GetCurrentIdGpecon().GetValueOrDefault();
            searchDefinition = searchDefinition.Where(i => i.IdGpeconFranquia == idGpecon);
        }

        public void OnSavingChanges(PerfilFranquiaDomainService context, ChangeOperation changeOperation)
        {
            if (IdGpeconFranquia.IsNullOrEmpty())
            {
                IdGpeconFranquia = UserServiceHelper.GetCurrentIdGpecon();
            }
        }

        public void OnTransactedChanges(PerfilFranquiaDomainService context, ChangeOperation changeOperation)
        {
            try
            {
                if (changeOperation != ChangeOperation.None && IdPerfilOrigem.IsNullOrEmpty() && UserServiceHelper.GetCurrentApplicativeId().GetValueOrDefault() == 3)
                {
                    Dictionary<string, string> headers = UserServiceHelper.GetRelatedEnvironmentInfo();

                    if (headers.IsNullOrEmpty() || headers.Count() == 0)
                    {
                        return;
                    }

                    int idLinxOperacional = UserServiceHelper.GetCurrentIdLinxEnvironment().GetValueOrDefault();
                    int idLinxAdministrativo = UserServiceHelper.GetCurrentIdLinxEnvironment(headers).GetValueOrDefault();

                    if (idLinxOperacional == idLinxAdministrativo)
                    {
                        return;
                    }

                    PerfilFranquiaDomainService ds = new PerfilFranquiaDomainService(headers) { IsSecure = true };
                    TcsPerfil perfilOrigem = context.GetTcsPerfil().Where(i => i.IdPerfil == IdPerfil).FirstOrDefault();
                    TcsPerfil perfil = ds.GetTcsPerfil().Where(i => i.IdPerfilOrigem == IdPerfil).FirstOrDefault();

                    List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
                    int entityCount = 0;

                    if (changeOperation == ChangeOperation.Delete)
                    {
                        if (perfil.IsNull())
                        {
                            return;
                        }
                        changeSetEntries.Add(new ChangeSetEntry(entityCount, perfil, null, DomainOperation.Delete));
                        entityCount++;
                    }
                    else
                    {
                        if (perfil.IsNull())
                        {
                            perfil = new TcsPerfil();
                        }

                        TcsPerfil perfilAdd = new TcsPerfil() { IdPerfil = perfil.IdPerfil, IdPerfilOrigem = IdPerfil, DescPerfil = DescPerfil, Inativo = Inativo, IdGpeconFranquia = IdGpeconFranquia };
                        changeSetEntries.Add(new ChangeSetEntry(entityCount, perfilAdd, null, DomainOperation.Insert));
                        entityCount++;

                        //TcsPerfilUsuario
                        //Usuários para remoção
                        List<long> usuarios = perfilOrigem.TcsUsuarioPerfilList.Select(i => i.IdUsuario).ToList();
                        perfil.TcsUsuarioPerfilList.Where(i => !usuarios.Contains(i.IdUsuario)).Foreach(item =>
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, item, null, DomainOperation.Delete));
                            entityCount++;
                        });

                        //Usuário para adição
                        usuarios = perfil.TcsUsuarioPerfilList.Select(i => i.IdUsuario).ToList();
                        perfilOrigem.TcsUsuarioPerfilList.Where(i => !usuarios.Contains(i.IdUsuario)).Select(i => i.IdUsuario).ToList().ForEach(id =>
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsUsuarioPerfil() { IdPerfil = perfilAdd.IdPerfil, IdUsuario = id }, null, DomainOperation.Insert));
                            entityCount++;
                        });

                        //TcsPerfilBandeiraRede
                        //Bandeiras para remoção
                        List<int> bandeiras = perfilOrigem.TcsPerfilBandeiraRedeList.Select(i => i.IdBandeiraR).ToList();
                        perfil.TcsPerfilBandeiraRedeList.Where(i => !bandeiras.Contains(i.IdBandeiraR)).Foreach(item =>
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, item, null, DomainOperation.Delete));
                            entityCount++;
                        });

                        //Bandeiras para adição
                        bandeiras = perfil.TcsPerfilBandeiraRedeList.Select(i => i.IdBandeiraR).ToList();
                        perfilOrigem.TcsPerfilBandeiraRedeList.Where(i => !bandeiras.Contains(i.IdBandeiraR)).Select(i => i.IdBandeiraR).Foreach(id =>
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsPerfilBandeiraRede() { IdPerfil = perfilAdd.IdPerfil, IdBandeiraR = id }, null, DomainOperation.Insert));
                            entityCount++;
                        });

                        //TcsPerfilFilial
                        //Filiais para remoção
                        List<int> filiais = perfilOrigem.TcsPerfilFilialList.Select(i => i.IdFilialPfj).ToList();
                        perfil.TcsPerfilFilialList.Where(i => !filiais.Contains(i.IdFilialPfj)).Foreach(item =>
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, item, null, DomainOperation.Delete));
                            entityCount++;
                        });

                        //Filiais para adição
                        filiais = perfil.TcsPerfilFilialList.Select(i => i.IdFilialPfj).ToList();
                        perfilOrigem.TcsPerfilFilialList.Where(i => !filiais.Contains(i.IdFilialPfj)).Select(i => i.IdFilialPfj).Foreach(id =>
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsPerfilFilial() { IdPerfil = perfilAdd.IdPerfil, IdFilialPfj = id }, null, DomainOperation.Insert));
                            entityCount++;
                        });

                        //TcsPerfilRegraModulo
                        //Módulos para remoção
                        List<string> modulos = perfilOrigem.TcsPerfilRegraModuloList.Where(i => i.Origem == "Portal").Select(i => i.IdModulo.ToString() + "||" + i.LxRegraAcessoModulo.ToString()).ToList();
                        perfil.TcsPerfilRegraModuloList.Where(i => !modulos.Contains(i.IdModulo.ToString() + "||" + i.LxRegraAcessoModulo.ToString())).Foreach(item =>
                         {
                             changeSetEntries.Add(new ChangeSetEntry(entityCount, item, null, DomainOperation.Delete));
                             entityCount++;
                         });

                        //Módulos para adição
                        modulos = perfil.TcsPerfilRegraModuloList.Select(i => i.IdModulo.ToString() + "||" + i.LxRegraAcessoModulo.ToString()).ToList();
                        perfilOrigem.TcsPerfilRegraModuloList.Where(i => i.Origem == "Portal" && !modulos.Contains(i.IdModulo.ToString() + "||" + i.LxRegraAcessoModulo.ToString())).Foreach(item =>
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsPerfilRegraModulo() { IdPerfil = perfilAdd.IdPerfil, IdModulo = item.IdModulo, LxRegraAcessoModulo = item.LxRegraAcessoModulo }, null, DomainOperation.Insert));
                            entityCount++;
                        });

                        //TcsPerfilRegraTransacao
                        //Transações para remoção
                        List<string> transacoes = perfilOrigem.TcsPerfilRegraTransacaoList.Where(i => i.Origem == "Portal").Select(i => i.IdTransacao.ToString() + "||" + i.LxRegraAcessoTransacao.ToString()).ToList();
                        perfil.TcsPerfilRegraTransacaoList.Where(i => !transacoes.Contains(i.IdTransacao.ToString() + "||" + i.LxRegraAcessoTransacao.ToString())).Foreach(item =>
                        {
                            changeSetEntries.Add(new ChangeSetEntry(entityCount, item, null, DomainOperation.Delete));
                            entityCount++;
                        });

                        //Transações para adição
                        transacoes = perfil.TcsPerfilRegraTransacaoList.Select(i => i.IdTransacao.ToString() + "||" + i.LxRegraAcessoTransacao.ToString()).ToList();
                        perfilOrigem.TcsPerfilRegraTransacaoList.Where(i => i.Origem == "Portal" && !transacoes.Contains(i.IdTransacao.ToString() + "||" + i.LxRegraAcessoTransacao.ToString())).Foreach(item =>
                          {
                              changeSetEntries.Add(new ChangeSetEntry(entityCount, new TcsPerfilRegraTransacao() { IdPerfil = perfilAdd.IdPerfil, IdTransacao = item.IdTransacao, LxRegraAcessoTransacao = item.LxRegraAcessoTransacao }, null, DomainOperation.Insert));
                              entityCount++;
                          });
                    }
                    ds.SaveEntities(changeSetEntries);
                }
            }
            catch (Exception oException)
            {
                throw oException;
            }
        }
    }
}
