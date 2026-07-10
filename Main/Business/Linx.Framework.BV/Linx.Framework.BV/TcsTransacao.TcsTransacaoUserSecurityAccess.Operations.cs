using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Objects;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ComponentModel.DataAnnotations;
using Linx;
using Linx.Framework.ControleSistema.BM;
using System.ServiceModel.DomainServices.Server;
using Linx.TCS0101.BO.TcsModulo;
using Linx.TCS0101.BO.TcsAutorizacao;
using Linx.Framework.Autorizacao.BM;

namespace Linx.TCS0101.BO.TcsTransacao
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsTransacaoDomainService
    {
        [Query(HasSideEffects = true)]
        public IEnumerable<TcsTransacaoSecurity> GetTcsTransacaoByUserAccess(Guid uidUsuario, Guid uidModuloMenu)
        {
            try
            {
                TcsTransacaoSecurity modulePermissions = new TcsTransacaoSecurity();

                TcsAutorizacao.TcsAutorizacaoDomainService ds = new TcsAutorizacao.TcsAutorizacaoDomainService();
                TCS_USUARIO_AUTENTICACAO usuario = ds.GetUser(uidUsuario);

                if (usuario.IsNull())
                    return new List<TcsTransacaoSecurity>();

                //TcsModuloAutorizacao - TcsModulo
                Guid uidModulo =
                    (
                        (from result in ds.GetTcsModuloMenuAutorizacaoNoAssociations().Where(i => i.UidModuloMenu == uidModuloMenu && !i.InativoModulo)
                         select result.UidModulo).ToList().Union
                        (from result in this.DbContext.TCS_MODULO_MENU
                         join result1 in this.DbContext.TCS_MODULO on result.UID_MODULO equals result1.UID_MODULO
                         where result.UID_MODULO_MENU == uidModuloMenu && !result1.INATIVO
                         select result.UID_MODULO).ToList()
                     ).FirstOrDefault();

                if (uidModulo.IsNull())
                    return new List<TcsTransacaoSecurity>();

                IQueryable<Int32> tcsPerfil = this.GetTcsPerfilUsuario(uidUsuario);

                //TcsUsuarioRegraModulo - TcsPerfilRegraModulo
                IQueryable<TcsModulo.TcsModuloAccess> listaModulo =
                    (
                        (from result in this.DbContext.TCS_USUARIO_REGRA_MODULO
                         where result.TCS_USUARIO.UID_USUARIO == uidUsuario && result.UID_MODULO == uidModulo
                         select new TcsModulo.TcsModuloAccess()
                         {
                             UidModulo = result.UID_MODULO,
                             RegraAcesso = result.LX_REGRA_ACESSO_MODULO,
                             Origem = 3
                         }
                         ).Union
                        (from result in this.DbContext.TCS_PERFIL_REGRA_MODULO
                         join result1 in tcsPerfil on result.TCS_PERFIL.ID_PERFIL equals result1
                         where result.UID_MODULO == uidModulo
                         select new TcsModulo.TcsModuloAccess()
                        {
                            UidModulo = result.UID_MODULO,
                            RegraAcesso = result.LX_REGRA_ACESSO_MODULO,
                            Origem = 4
                        }
                        )
                    ).OrderBy(i => i.UidModulo).ThenByDescending(i => i.Origem).ThenByDescending(i => i.RegraAcesso);

                TcsModulo.TcsModuloAccess previousItem = new TcsModulo.TcsModuloAccess();

                foreach (TcsModulo.TcsModuloAccess item in listaModulo)
                {
                    ValidateAccess(modulePermissions, item.Origem, item.RegraAcesso, previousItem.RegraAcesso);
                    previousItem = item;
                }

                //TcsTransacao - TcsTransacaoAutorizacao
                int[] lxTipoTransacaoPermitido = new int[] { 1, 2 };

                var tcsTransacaoMenu =
                    (from result in this.GetTcsTransacaoMenuNoAssociations().Where(i => !i.Inativo && i.UidModuloMenu == uidModuloMenu)
                     select new
                     {
                         UidTransacao = result.UidTransacao,
                         OrdemNavegacao = result.OrdemNavegacao
                     }).ToList();

                List<Guid> transacoes =
                    (from result in tcsTransacaoMenu
                     select result.UidTransacao).ToList();

                var tcsTransacaoLocal =
                    (from result in this.GetTcsTransacaoNoAssociations().Where(i => !i.Inativo && transacoes.Contains(i.UidTransacao) && lxTipoTransacaoPermitido.Contains(i.LxTipoTransacao))
                     select new
                     {
                         UidTransacao = result.UidTransacao,
                         CodTransacao = result.CodTransacao,
                         DescTransacao = result.DescTransacao,
                         ClasseNome = result.ClasseNome,
                         LxTipoTransacao = result.LxTipoTransacao,
                         UidObjeto = result.UidObjeto,
                         DescObjeto = result.DescObjeto,
                         ClasseNomeObjeto = result.ClasseNomeObjeto
                     }).ToList();

                var tcsTransacaoAutorizacao =
                    (from result in ds.GetTcsTransacaoAutorizacaoNoAssociations().Where(i => !i.Inativo && transacoes.Contains(i.UidTransacao) && lxTipoTransacaoPermitido.Contains(i.LxTipoTransacao))
                     select new
                     {
                         UidTransacao = result.UidTransacao,
                         CodTransacao = result.CodTransacao,
                         DescTransacao = result.DescTransacao,
                         ClasseNome = result.ClasseNome,
                         LxTipoTransacao = result.LxTipoTransacao,
                         UidObjeto = result.UidObjeto,
                         DescObjeto = result.DescObjeto,
                         ClasseNomeObjeto = result.ObjetoClasseNome
                     }).ToList();

                List<TcsTransacaoSecurity> transacoesLista =
                    (
                     (from result in tcsTransacaoLocal
                      join result1 in tcsTransacaoMenu on result.UidTransacao equals result1.UidTransacao
                      select new TcsTransacaoSecurity()
                      {
                          UidTransacao = result.UidTransacao,
                          CodTransacao = result.CodTransacao,
                          DescTransacao = result.DescTransacao,
                          ClasseNome = result.ClasseNome,
                          LxTipoTransacao = result.LxTipoTransacao,
                          UidObjeto = result.UidObjeto,
                          DescObjeto = result.DescObjeto,
                          ClasseNomeObjeto = result.ClasseNomeObjeto,
                          OrdemNavegacao = result1.OrdemNavegacao,
                          AcessoBloqueado = modulePermissions.AcessoBloqueado,
                          AcessoTotal = modulePermissions.AcessoTotal,
                          Pesquisar = modulePermissions.Pesquisar,
                          Incluir = modulePermissions.Incluir,
                          Alterar = modulePermissions.Alterar,
                          Excluir = modulePermissions.Excluir,
                          PesquisaEspecial = modulePermissions.PesquisaEspecial,
                          Imprimir = modulePermissions.Imprimir,
                          Exportar = modulePermissions.Exportar,
                          CriarRelatorio = modulePermissions.CriarRelatorio,
                          CriarPesquisa = modulePermissions.CriarPesquisa,
                          Layout = modulePermissions.Layout,
                          Origem = modulePermissions.Origem
                      }).ToList().Union
                     (from result in tcsTransacaoAutorizacao
                      join result1 in tcsTransacaoMenu on result.UidTransacao equals result1.UidTransacao
                      select new TcsTransacaoSecurity()
                      {
                          UidTransacao = result.UidTransacao,
                          CodTransacao = result.CodTransacao,
                          DescTransacao = result.DescTransacao,
                          ClasseNome = result.ClasseNome,
                          LxTipoTransacao = result.LxTipoTransacao,
                          UidObjeto = result.UidObjeto,
                          DescObjeto = result.DescObjeto,
                          ClasseNomeObjeto = result.ClasseNomeObjeto,
                          OrdemNavegacao = result1.OrdemNavegacao,
                          AcessoBloqueado = modulePermissions.AcessoBloqueado,
                          AcessoTotal = modulePermissions.AcessoTotal,
                          Pesquisar = modulePermissions.Pesquisar,
                          Incluir = modulePermissions.Incluir,
                          Alterar = modulePermissions.Alterar,
                          Excluir = modulePermissions.Excluir,
                          PesquisaEspecial = modulePermissions.PesquisaEspecial,
                          Imprimir = modulePermissions.Imprimir,
                          Exportar = modulePermissions.Exportar,
                          CriarRelatorio = modulePermissions.CriarRelatorio,
                          CriarPesquisa = modulePermissions.CriarPesquisa,
                          Layout = modulePermissions.Layout,
                          Origem = modulePermissions.Origem
                      }).ToList().Union
                        (from result in ds.GetTcsTransacaoMenuAutorizacao().Where(i => i.UidModuloMenu == uidModuloMenu && i.UidModulo == uidModulo && !i.InativoTransacao && lxTipoTransacaoPermitido.Contains(i.LxTipoTransacao))
                         select new TcsTransacaoSecurity()
                         {
                             UidTransacao = result.UidTransacao,
                             CodTransacao = result.CodTransacao,
                             DescTransacao = result.DescTransacao,
                             ClasseNome = result.ClasseNomeTransacao,
                             LxTipoTransacao = result.LxTipoTransacao,
                             UidObjeto = result.UidObjeto,
                             DescObjeto = result.DescObjeto,
                             ClasseNomeObjeto = result.ClasseNomeObjeto,
                             OrdemNavegacao = result.OrdemNavegacao,
                             AcessoBloqueado = modulePermissions.AcessoBloqueado,
                             AcessoTotal = modulePermissions.AcessoTotal,
                             Pesquisar = modulePermissions.Pesquisar,
                             Incluir = modulePermissions.Incluir,
                             Alterar = modulePermissions.Alterar,
                             Excluir = modulePermissions.Excluir,
                             PesquisaEspecial = modulePermissions.PesquisaEspecial,
                             Imprimir = modulePermissions.Imprimir,
                             Exportar = modulePermissions.Exportar,
                             CriarRelatorio = modulePermissions.CriarRelatorio,
                             CriarPesquisa = modulePermissions.CriarPesquisa,
                             Layout = modulePermissions.Layout,
                             Origem = modulePermissions.Origem
                         }).ToList()
                    );

                transacoes =
                    (from result in transacoesLista
                     select result.UidTransacao).Distinct().ToList();

                //TcsUsuarioRegraTransacao - TcsPerfilRegraTransacao
                var transacaoRegraLista =
                    (
                        (from result in this.DbContext.TCS_USUARIO_REGRA_TRANSACAO
                         join result1 in transacoes on result.UID_TRANSACAO equals result1
                         where result.UID_USUARIO == uidUsuario
                         select new TcsTransacaoAccess()
                        {
                            UidTransacao = result.UID_TRANSACAO,
                            RegraAcessoTransacao = result.LX_REGRA_ACESSO_TRANSACAO,
                            Origem = 1
                        }
                         ).ToList().Union
                        (from result in this.DbContext.TCS_PERFIL_REGRA_TRANSACAO
                         join result1 in tcsPerfil on result.TCS_PERFIL.ID_PERFIL equals result1
                         join result2 in transacoes on result.UID_TRANSACAO equals result2
                         select new TcsTransacaoAccess()
                         {
                             UidTransacao = result.UID_TRANSACAO,
                             RegraAcessoTransacao = result.LX_REGRA_ACESSO_TRANSACAO,
                             Origem = 2
                         }).ToList()
                     ).OrderBy(i => i.UidTransacao).ThenByDescending(i => i.Origem).ThenByDescending(i => i.RegraAcessoTransacao);

                TcsTransacaoAccess previousTransactionItem = new TcsTransacaoAccess();

                foreach (TcsTransacaoAccess transactionItem in transacaoRegraLista)
                {
                    List<TcsTransacaoSecurity> transacoesListaAux = transacoesLista.Where(i => i.UidTransacao == transactionItem.UidTransacao).ToList();

                    if (previousTransactionItem.UidTransacao != transactionItem.UidTransacao)
                        previousTransactionItem = transactionItem;

                    if (transacoesListaAux.Count() > 0)
                    {
                        TcsTransacaoSecurity transacao = transacoesListaAux.First();
                        ValidateAccess(transacao, transactionItem.Origem, transactionItem.RegraAcessoTransacao, previousTransactionItem.RegraAcessoTransacao);
                    }
                    previousTransactionItem = transactionItem;
                }

                return
                    (from result in transacoesLista
                     where !result.AcessoBloqueado
                     select result).OrderBy(i => i.OrdemNavegacao).ThenBy(i => i.DescTransacao).ToList();
            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message);
            }
        }

        [Query(HasSideEffects = true)]
        public IEnumerable<TcsTransacaoSecurity> GetBoAccess(Guid uidUsuario, string boName, string transaction)
        {
            TcsModulo.TcsModuloAccess previousItem = new TcsModulo.TcsModuloAccess();
            TcsTransacaoSecurity modulePermissions = new TcsTransacaoSecurity();
            List<TcsModuloAccess> allowedModules = new List<TcsModuloAccess>();
            TcsTransacaoAccess previousTransactionItem = new TcsTransacaoAccess();
            List<TcsTransacaoSecurity> transacaoSecurity = new List<TcsTransacaoSecurity>();

            try
            {
                TcsAutorizacao.TcsAutorizacaoDomainService ds = new TcsAutorizacao.TcsAutorizacaoDomainService();
                TCS_USUARIO_AUTENTICACAO usuario = ds.GetUser(uidUsuario);

                if (usuario.IsNull())
                    return transacaoSecurity;

                //TcsTransacao - TcsTransacaoAutorizacao
                List<Guid> transacoes = this.GetTcsTransacaoAcesso(transaction, boName);

                if (transacoes.IsNull())
                    return transacaoSecurity;

                //TcsPerfil
                IQueryable<Int32> tcsPerfil = this.GetTcsPerfilUsuario(uidUsuario);

                //TcsModulo - TcsModuloAutorizacao
                List<Guid> tcsTransacaoMenu =
                    (from result in this.DbContext.TCS_TRANSACAO_MENU
                     where !result.INATIVO && transacoes.Contains(result.UID_TRANSACAO)
                     select result.UID_MODULO_MENU).Distinct().ToList();

                List<Guid> tcsModuloMenu =
                    ((from result in this.DbContext.TCS_MODULO_MENU
                      where tcsTransacaoMenu.Contains(result.UID_MODULO_MENU)
                      select result.UID_MODULO).ToList().Union
                    (from result in ds.GetTcsModuloMenuAutorizacaoNoAssociations().Where(i => tcsTransacaoMenu.Contains(i.UidModuloMenu))
                     select result.UidModulo).ToList());

                List<Guid> modulos =
                    ((
                    (from result in this.DbContext.TCS_MODULO
                     where !result.INATIVO && tcsModuloMenu.Contains(result.UID_MODULO)
                     select result.UID_MODULO
                    ).ToList().Union
                    (from result in ds.GetTcsModuloAutorizacaoNoAssociations().Where(i => !i.Inativo && tcsModuloMenu.Contains(i.UidModulo))
                     select result.UidModulo
                    ).ToList().Union
                     (from result in ds.GetTcsModuloMenuAutorizacaoNoAssociations().Where(i => tcsTransacaoMenu.Contains(i.UidModulo) && !i.InativoModulo)
                      select result.UidModulo).ToList()).Union
                    (from result in ds.GetTcsTransacaoMenuAutorizacaoNoAssociations().Where(i => transacoes.Contains(i.UidTransacao) && !i.InativoModulo && !i.Inativo)
                     select result.UidModulo).ToList());

                //TcsUsuarioRegraModulo - TcsPerfilRegraModulo
                IQueryable<TcsModuloAccess> listaModulo =
                    ((from result in this.DbContext.TCS_USUARIO_REGRA_MODULO
                      where modulos.Contains(result.UID_MODULO) && result.TCS_USUARIO.UID_USUARIO == uidUsuario
                      select new TcsModuloAccess() { UidModulo = Guid.Empty, RegraAcesso = result.LX_REGRA_ACESSO_MODULO, Origem = 3 }
                     ).Union
                     (from result in this.DbContext.TCS_PERFIL_REGRA_MODULO
                      where tcsPerfil.Contains(result.TCS_PERFIL.ID_PERFIL) && modulos.Contains(result.UID_MODULO)
                      select new TcsModuloAccess() { UidModulo = Guid.Empty, RegraAcesso = result.LX_REGRA_ACESSO_MODULO, Origem = 4 }
                     )).OrderByDescending(i => i.Origem).OrderByDescending(i => i.RegraAcesso);

                foreach (TcsModuloAccess item in listaModulo)
                {
                    ValidateAccess(modulePermissions, item.Origem, item.RegraAcesso, previousItem.RegraAcesso);
                    previousItem = item;
                }

                List<TcsTransacaoSecurity> transacoesLista =
                    (from result in transacoes
                     select new TcsTransacaoSecurity()
                     {
                         UidTransacao = Guid.Empty,
                         AcessoBloqueado = modulePermissions.AcessoBloqueado,
                         AcessoTotal = modulePermissions.AcessoTotal,
                         Pesquisar = modulePermissions.Pesquisar,
                         Incluir = modulePermissions.Incluir,
                         Alterar = modulePermissions.Alterar,
                         Excluir = modulePermissions.Excluir,
                         PesquisaEspecial = modulePermissions.PesquisaEspecial,
                         Imprimir = modulePermissions.Imprimir,
                         Exportar = modulePermissions.Exportar,
                         CriarRelatorio = modulePermissions.CriarRelatorio,
                         CriarPesquisa = modulePermissions.CriarPesquisa,
                         Layout = modulePermissions.Layout,
                         Origem = modulePermissions.Origem
                     }
                    ).Distinct().ToList();

                //TcsUsuarioRegraTransacao - TcsPerfilRegraTransacao
                IQueryable<TcsTransacaoAccess> listaTransacao =
                    ((from result in this.DbContext.TCS_USUARIO_REGRA_TRANSACAO
                      where transacoes.Contains(result.UID_TRANSACAO) && result.TCS_USUARIO.UID_USUARIO == uidUsuario
                      select new TcsTransacaoAccess() { UidTransacao = Guid.Empty, RegraAcessoTransacao = result.LX_REGRA_ACESSO_TRANSACAO, Origem = 1 }
                    ).Union
                    (from result in this.DbContext.TCS_PERFIL_REGRA_TRANSACAO
                     where transacoes.Contains(result.UID_TRANSACAO) && tcsPerfil.Contains(result.TCS_PERFIL.ID_PERFIL)
                     select new TcsTransacaoAccess() { UidTransacao = Guid.Empty, RegraAcessoTransacao = result.LX_REGRA_ACESSO_TRANSACAO, Origem = 2 }
                    )).OrderBy(i => i.UidTransacao).ThenByDescending(i => i.Origem).ThenByDescending(i => i.RegraAcessoTransacao);

                foreach (TcsTransacaoAccess transactionItem in listaTransacao)
                {
                    List<TcsTransacaoSecurity> transacoesListaAux = transacoesLista.Where(i => i.UidTransacao == transactionItem.UidTransacao).ToList();

                    if (previousTransactionItem.UidTransacao != transactionItem.UidTransacao)
                        previousTransactionItem = transactionItem;

                    if (transacoesListaAux.Count() > 0)
                    {
                        TcsTransacaoSecurity transacao = transacoesListaAux.First();
                        ValidateAccess(transacao, transactionItem.Origem, transactionItem.RegraAcessoTransacao, previousTransactionItem.RegraAcessoTransacao);
                    }

                    previousTransactionItem = transactionItem;
                }

                transacaoSecurity = transacoesLista.ToList();
            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message);
            }

            return from result in transacaoSecurity
                   select result;
        }

        [Query(HasSideEffects = true)]
        public IEnumerable<TcsTransacaoSecurity> GetTcsTransacaoByClassName(Guid uidUsuario, string modules, string className)
        {
            TcsAutorizacao.TcsAutorizacaoDomainService ds = new TcsAutorizacao.TcsAutorizacaoDomainService();

            //UidTransacao
            List<Guid> transacoes = this.GetTcsTransacaoAcesso(className, className);

            List<Guid> modulos = (from result in modules.Split(new string[] { "," }, StringSplitOptions.None)
                                  select Guid.Parse(result)).ToList();

            //TcsTransacaoMenu - TcsTransacaoMenuAutorizacao
            var mMenu = (
                            (from result in this.GetTcsTransacaoMenuChildNoAssociations().Where(i => !i.Inativo && transacoes.Contains(i.UidTransacao) && modulos.Contains(i.UidModulo))
                             select result.UidModuloMenu).ToList().Union
                            (from result in ds.GetTcsTransacaoMenuAutorizacaoNoAssociations().Where(i => !i.Inativo && transacoes.Contains(i.UidTransacao) && modulos.Contains(i.UidModulo))
                             select result.UidModuloMenu).ToList()
                         ).FirstOrDefault();

            if (mMenu == null)
                return new List<TcsTransacaoSecurity>();
            else
                return GetTcsTransacaoByUserAccess(uidUsuario, mMenu).Where(e => e.ClasseNome == className);
        }

        [Query(HasSideEffects = true)]
        public List<TcsTransacaoDependente> GetTransacaoDependente(Guid uidTransacao)
        {
            TcsAutorizacao.TcsAutorizacaoDomainService ds = new TcsAutorizacao.TcsAutorizacaoDomainService();

            return
                (from result in ds.GetTcsTransacaoDependenteAutorizacaoNoAssociations().Where(i => i.UidTransacao == uidTransacao)
                 select new TcsTransacaoDependente()
                 {
                     CompartilhaBoPrincipal = result.CompartilhaBoPrincipal,
                     ExecutaPesquisa = result.ExecutaPesquisa,
                     LxPosicaoDaTransacao = result.LxPosicaoDaTransacao,
                     LxTipoLayout = result.LxTipoLayout,
                     MostraBotaoAdicao = result.MostraBotaoAdicao,
                     MostraBotaoEdicao = result.MostraBotaoEdicao,
                     MostraBotaoExclusao = result.MostraBotaoExclusao,
                     MostraBotaoImpressao = result.MostraBotaoImpressao,
                     MostraBotaoLayout = result.MostraBotaoLayout,
                     MostraBotaoLimpa = result.MostraBotaoLimpa,
                     MostraBotaoNavegacao = result.MostraBotaoNavegacao,
                     MostraBotaoPesquisa = result.MostraBotaoPesquisa,
                     MostraBotaoPesquisaEsp = result.MostraBotaoPesquisaEsp,
                     PossuiToolbar = result.PossuiToolbar,
                     PossuiVisaoTabular = result.PossuiVisaoTabular,
                     PropriedadesDoDetalhe = result.PropriedadesDoDetalhe,
                     PropriedadesDoMestre = result.PropriedadesDoMestre,
                     UidTransacao = result.UidTransacao,
                     UidTransacaoDependente = result.UidTransacaoDependente,
                     UidTransacaoRelacionada = result.UidTransacaoRelacionada,
                     UsaFiltrosDoBoPrincipal = result.UsaFiltrosDoBoPrincipal,
                     Visivel = result.Visivel,
                     ClasseNome = result.ClasseNome,
                     DescTransacao = result.DescTransacao
                 }).ToList().Union
                (from result in this.GetTcsTransacaoDependenteNoAssociations().Where(i => i.UidTransacao == uidTransacao)
                 select result).ToList();
        }

        [Invoke(HasSideEffects = true)]
        private void ValidateAccess(TcsTransacaoSecurity transacao, int currentOrigem, int currentAccessRule, int previousAccessRule)
        {
            int origemTransacao = transacao.Origem;

            if (currentOrigem > transacao.Origem)
                return;
            else
                transacao.Origem = currentOrigem;

            //if (origemTransacao != transacao.Origem && (transacao.AcessoBloqueado || transacao.AcessoTotal))
            if (origemTransacao != transacao.Origem)
                ResetAccess(transacao, false);

            switch (currentAccessRule)
            {
                case 1:
                    if (origemTransacao != transacao.Origem || (origemTransacao == transacao.Origem && previousAccessRule != 2))
                    {
                        ResetAccess(transacao, false);
                        transacao.AcessoBloqueado = true;
                    }
                    break;

                case 2:
                    ResetAccess(transacao, true);
                    transacao.AcessoBloqueado = false;
                    break;

                case 3:
                    transacao.Pesquisar = true;
                    break;

                case 4:
                    transacao.Incluir = true;
                    break;

                case 5:
                    transacao.Alterar = true;
                    break;

                case 6:
                    transacao.Excluir = true;
                    break;

                case 7:
                    transacao.PesquisaEspecial = true;
                    break;

                case 8:
                    transacao.Imprimir = true;
                    break;

                case 9:
                    transacao.Exportar = true;
                    break;

                case 10:
                    transacao.CriarRelatorio = true;
                    break;

                case 11:
                    transacao.Layout = true;
                    break;

                case 12:
                    transacao.CriarPesquisa = true;
                    break;

                case 99:
                    break;
            }
        }

        [Invoke(HasSideEffects = true)]
        private void ResetAccess(TcsTransacaoSecurity transacao, bool access)
        {
            transacao.AcessoBloqueado = access;
            transacao.AcessoTotal = access;
            transacao.Pesquisar = access;
            transacao.Incluir = access;
            transacao.Alterar = access;
            transacao.Excluir = access;
            transacao.PesquisaEspecial = access;
            transacao.Imprimir = access;
            transacao.Exportar = access;
            transacao.CriarRelatorio = access;
            transacao.CriarPesquisa = access;
            transacao.Layout = access;
        }

        [Invoke(HasSideEffects = true)]
        private IQueryable<Int32> GetTcsPerfilUsuario(Guid uidUsuario)
        {
            return from result1 in this.DbContext.TCS_USUARIO_PERFIL
                   let result = result1.TCS_PERFIL
                   where !result.INATIVO && result1.UID_USUARIO == uidUsuario
                   select result.ID_PERFIL;
        }

        [Invoke(HasSideEffects = true)]
        private List<Guid> GetTcsTransacaoAcesso(string transaction, string boName)
        {
            TcsAutorizacaoDomainService ds = new TcsAutorizacaoDomainService();

            List<string> boList = boName.IsNull() ? new List<string>() : boName.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            return
                (from result in ds.GetTcsTransacao(transaction, boName).Select(i => i.UidTransacao)
                 select result
                ).ToList().Union
                (from result in this.DbContext.TCS_TRANSACAO
                 where !result.INATIVO && (result.CLASSE_NOME == transaction || boList.Contains(result.CLASSE_NOME))
                 select result.UID_TRANSACAO
                ).ToList();
        }

        [Query(HasSideEffects = false)]
        public IEnumerable<TcsTransacaoSecurity> GetTcsTransacaoByUserAccessJson(Guid uidUsuario, Guid uidModuloMenu)
        {
            return GetTcsTransacaoByUserAccess(uidUsuario, uidModuloMenu);
        }

        [Query(HasSideEffects = true)]
        public IQueryable<TcsTransacao> GetBusinessUidObjeto(string classeNome)
        {
            TcsAutorizacaoDomainService ds = new TcsAutorizacaoDomainService();


            var query =
                from result in ds.GetTcsTransacao(classeNome, null)
                select new TcsTransacao
                {
                    UidObjeto = result.UidObjeto,
                    DescObjeto = result.DescObjeto
                };

            var query2 = 
                    from result in this.GetTcsTransacaoNoAssociations().Where(i => !i.Inativo && i.ClasseNome == classeNome)
                    select result;

            return
                ((
                from result in ds.GetTcsTransacao(classeNome, null)
                select new TcsTransacao
                {
                    UidObjeto = result.UidObjeto,
                    DescObjeto = result.DescObjeto
                }).ToList().Union
                 (
                    from result in this.GetTcsTransacaoNoAssociations().Where(i => !i.Inativo && i.ClasseNome == classeNome)
                    select result
                 ).ToList()).AsQueryable();
        }
    }

    public class TcsTransacaoSecurity
    {
        public TcsTransacaoSecurity()
        {
            AcessoBloqueado = false;
            AcessoTotal = false;
            Pesquisar = false;
            Incluir = false;
            Alterar = false;
            Excluir = false;
            PesquisaEspecial = false;
            Imprimir = false;
            Exportar = false;
            CriarRelatorio = false;
            CriarPesquisa = false;
            Layout = false;
            Origem = 99;
        }

        [Key]
        public Guid UidTransacao { get; set; }
        public Guid UidObjeto { get; set; }
        public string DescObjeto { get; set; }
        public string CodTransacao { get; set; }
        public string DescTransacao { get; set; }
        public int LxTipoTransacao { get; set; }
        public string ClasseNome { get; set; }
        public string ClasseNomeObjeto { get; set; }
        public bool AcessoBloqueado { get; set; }
        public bool AcessoTotal { get; set; }
        public bool Pesquisar { get; set; }
        public bool Incluir { get; set; }
        public bool Alterar { get; set; }
        public bool Excluir { get; set; }
        public bool PesquisaEspecial { get; set; }
        public bool Imprimir { get; set; }
        public bool Exportar { get; set; }
        public bool CriarRelatorio { get; set; }
        public bool CriarPesquisa { get; set; }
        public bool Layout { get; set; }
        public int Origem { get; set; }
        public int OrdemNavegacao { get; set; }

        private string GetSvcPath()
        {
            if (this.ClasseNomeObjeto.IsNullOrEmpty())
                return String.Empty;
            else
            {
                string classeNomeObjeto = (this.ClasseNomeObjeto + "#").Left("." + this.ClasseNomeObjeto.Right(".") + "#");
                string className = classeNomeObjeto.Right(".");
                if (!className.IsNullOrEmpty())
                    return classeNomeObjeto.Replace(".", "-") + "-" + className + "DomainService.svc";
                else
                    return String.Empty;
            }
        }

        private string _SvcPath;
        public string SvcPath
        {
            get
            {
                if (_SvcPath != (GetSvcPath()))
                    _SvcPath = GetSvcPath();
                return _SvcPath;
            }
            set
            {
                if (this._SvcPath != value)
                    this._SvcPath = value;
            }
        }
    }

    public class TcsTransacaoAccess
    {
        public TcsTransacaoAccess()
        {
        }

        public TcsTransacaoAccess(Guid uidTransacao, int regraAcessoTransacao, int origem)
        {
            UidTransacao = uidTransacao;
            RegraAcessoTransacao = regraAcessoTransacao;
            Origem = origem;
        }

        public Guid UidTransacao { get; set; }
        public int RegraAcessoTransacao { get; set; }
        public int Origem { get; set; }

        //1 -> Tcs_Usuario_Regra_Transacao
        //2 -> Tcs_Perfil_Regra_Transacao
        //3 -> Tcs_Usuario_Regra_Modulo
        //4 -> Tcs_perfil_Regra_Modulo
    }
}
