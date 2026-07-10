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
using Linx.Framework.BV.Modulo;

namespace Linx.Framework.BV.Transacao
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TransacaoDomainService
    {
        [Query(HasSideEffects = true)]
        public IEnumerable<TcsTransacaoSecurity> GetTcsTransacaoByUserAccess(Guid uidUsuario, Int64 idModuloMenu, Dictionary<string, string> headers = null)
        {
            Int64 idUsuario = BusinessUserServiceHelper.GetCurrentUserId(headers).GetValueOrDefault();
            try
            {
                TcsTransacaoSecurity modulePermissions = new TcsTransacaoSecurity();

                AutorizacaoDomainService dsAutorizacao = new AutorizacaoDomainService(headers);
                TransacaoAutorizacao.TransacaoAutorizacaoDomainService ds = new TransacaoAutorizacao.TransacaoAutorizacaoDomainService(headers);
                ModuloAutorizacao.ModuloAutorizacaoDomainService ModuloAutorizacaoDs = new ModuloAutorizacao.ModuloAutorizacaoDomainService(headers);

                //Validate User (Inativo - Vigência)
                dsAutorizacao.ValidateUserAccess(uidUsuario);

                //TcsModuloAutorizacao - TcsModulo
                Int64 idModulo =
                    (
                        (from result in ModuloAutorizacaoDs.GetTcsModuloMenuAutorizacaoNoAssociations().Where(i => i.IdModuloMenu == idModuloMenu && !i.InativoModulo)
                         select result.IdModulo).ToList().Union
                        (from result in this.DbContext.TCS_MODULO_MENU
                         join result1 in this.DbContext.TCS_MODULO on result.ID_MODULO equals result1.ID_MODULO
                         where result.ID_MODULO_MENU == idModuloMenu && !result1.INATIVO
                         select result.ID_MODULO).ToList()
                     ).FirstOrDefault();

                if (idModulo.IsNull())
                    return new List<TcsTransacaoSecurity>();

                List<Int64> tcsPerfil = this.GetTcsPerfilUsuario(idUsuario, headers);

                //TcsUsuarioRegraModulo - TcsPerfilRegraModulo
                List<TcsModuloAccess> listaModulo =
                    (
                        (from result in this.DbContext.TCS_USUARIO_REGRA_MODULO
                         where result.TCS_USUARIO.UID_USUARIO == uidUsuario && result.ID_MODULO == idModulo
                         select new TcsModuloAccess()
                         {
                             IdModulo = result.ID_MODULO,
                             RegraAcesso = result.LX_REGRA_ACESSO_MODULO,
                             Origem = 3
                         }
                         ).Union
                        (from result in this.DbContext.TCS_PERFIL_REGRA_MODULO
                         join result1 in tcsPerfil on result.TCS_PERFIL.ID_PERFIL equals result1
                         where result.ID_MODULO == idModulo
                         select new TcsModuloAccess()
                         {
                             IdModulo = result.ID_MODULO,
                             RegraAcesso = result.LX_REGRA_ACESSO_MODULO,
                             Origem = 4
                         }
                        )
                    ).OrderBy(i => i.IdModulo).ThenByDescending(i => i.Origem).ThenByDescending(i => i.RegraAcesso).ToList();

                TcsModuloAccess previousItem = new TcsModuloAccess();

                foreach (TcsModuloAccess item in listaModulo)
                {
                    ValidateAccess(modulePermissions, item.Origem, item.RegraAcesso, previousItem.RegraAcesso);
                    previousItem = item;
                }

                //TcsTransacao - TcsTransacaoAutorizacao
                // 1 -> Todos / 2 -> ERP / 4 -> Excel / 6 -> ERP App
                int[] lxTipoTransacaoPermitido = new int[] { 1, 2, 4, 6, 7, 8 };

                var tcsTransacaoMenu =
                    (from result in this.GetTcsTransacaoMenuNoAssociations().Where(i => !i.Inativo && i.IdModuloMenu == idModuloMenu)
                     select new { IdTransacao = result.IdTransacao, OrdemNavegacao = result.OrdemNavegacao }).ToList().
                     Union(
                     (from result in ds.GetTcsTransacaoMenuAutorizacao().Where(i => !i.Inativo && i.IdModuloMenu == idModuloMenu)
                      select new { IdTransacao = result.IdTransacao, OrdemNavegacao = result.OrdemNavegacao }).ToList()
                     );

                List<Int64> transacoes =
                    (from result in tcsTransacaoMenu
                     select result.IdTransacao).ToList();

                var tcsTransacaoLocal =
                    (from result in this.GetTcsTransacaoNoAssociations().Where(i => !i.Inativo && transacoes.Contains(i.IdTransacao) && lxTipoTransacaoPermitido.Contains(i.LxTipoTransacao))
                     select new
                     {
                         IdTransacao = result.IdTransacao,
                         CodTransacao = result.CodTransacao,
                         DescTransacao = result.DescTransacao,
                         NomeCurto = result.NomeCurto,
                         ClasseNome = result.ClasseNome,
                         LxTipoTransacao = result.LxTipoTransacao,
                         IdObjeto = result.IdObjeto,
                         DescObjeto = result.DescObjeto,
                         ClasseNomeObjeto = result.ClasseNomeObjeto,
                         Icone = result.Icone,
                         LxCorFundo = result.LxCorFundo,
                         NomeTabela = "TCS_TRANSACAO",
                         Tags = result.Tag
                     }).ToList();

                var tcsTransacaoAutorizacao =
                    (from result in ds.GetTcsTransacaoAutorizacaoNoAssociations().Where(i => !i.Inativo && transacoes.Contains(i.IdTransacao) && lxTipoTransacaoPermitido.Contains(i.LxTipoTransacao))
                     select new
                     {
                         IdTransacao = result.IdTransacao,
                         CodTransacao = result.CodTransacao,
                         DescTransacao = result.DescTransacao,
                         NomeCurto = result.NomeCurto,
                         ClasseNome = result.ClasseNome,
                         LxTipoTransacao = result.LxTipoTransacao,
                         IdObjeto = result.IdObjeto,
                         DescObjeto = result.DescObjeto,
                         ClasseNomeObjeto = result.ObjetoClasseNome,
                         Icone = result.Icone,
                         LxCorFundo = result.LxCorFundo,
                         NomeTabela = "TCS_TRANSACAO_AUTORIZACAO",
                         Tags = result.Tag
                     }).ToList();

                //Tag de transação do Usuário
                TransacaoTag.TransacaoTagDomainService dsTransacaoTag = new TransacaoTag.TransacaoTagDomainService();
                var tcsUsuarioTransacaoTag = dsTransacaoTag.GetTcsTransacaoTagNoAssociations().Where(i => transacoes.Contains(i.IdTransacao)).ToList();

                List<TcsTransacaoSecurity> transacoesLista =
                    (
                     (from result in tcsTransacaoLocal
                      join result1 in tcsTransacaoMenu on result.IdTransacao equals result1.IdTransacao
                      select new TcsTransacaoSecurity()
                      {
                          IdTransacao = result.IdTransacao,
                          CodTransacao = result.CodTransacao,
                          DescTransacao = result.DescTransacao,
                          NomeCurto = result.NomeCurto,
                          ClasseNome = result.ClasseNome,
                          LxTipoTransacao = result.LxTipoTransacao,
                          IdObjeto = result.IdObjeto,
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
                          Origem = modulePermissions.Origem,
                          Icone = result.Icone,
                          LxCorFundo = result.LxCorFundo,
                          NomeTabela = result.NomeTabela,
                          Tags = UpdateTag(tcsUsuarioTransacaoTag, result.IdTransacao, result.Tags)
                      }).ToList().Union
                     (from result in tcsTransacaoAutorizacao
                      join result1 in tcsTransacaoMenu on result.IdTransacao equals result1.IdTransacao
                      select new TcsTransacaoSecurity()
                      {
                          IdTransacao = result.IdTransacao,
                          CodTransacao = result.CodTransacao,
                          DescTransacao = result.DescTransacao,
                          NomeCurto = result.NomeCurto,
                          ClasseNome = result.ClasseNome,
                          LxTipoTransacao = result.LxTipoTransacao,
                          IdObjeto = result.IdObjeto,
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
                          Origem = modulePermissions.Origem,
                          Icone = result.Icone,
                          LxCorFundo = result.LxCorFundo,
                          NomeTabela = result.NomeTabela,
                          Tags = UpdateTag(tcsUsuarioTransacaoTag, result.IdTransacao, result.Tags)
                      }).ToList()
                    );

                transacoes =
                    (from result in transacoesLista
                     select result.IdTransacao).Distinct().ToList();

                //TcsUsuarioRegraTransacao - TcsPerfilRegraTransacao
                var transacaoRegraLista =
                    (
                        (from result in this.DbContext.TCS_USUARIO_REGRA_TRANSACAO
                         join result1 in transacoes on result.ID_TRANSACAO equals result1
                         where result.TCS_USUARIO.UID_USUARIO == uidUsuario
                         select new TcsTransacaoAccess()
                         {
                             IdTransacao = result.ID_TRANSACAO,
                             RegraAcessoTransacao = result.LX_REGRA_ACESSO_TRANSACAO,
                             Origem = 1
                         }
                         ).ToList().Union
                        (from result in this.DbContext.TCS_PERFIL_REGRA_TRANSACAO
                         join result1 in tcsPerfil on result.TCS_PERFIL.ID_PERFIL equals result1
                         join result2 in transacoes on result.ID_TRANSACAO equals result2
                         select new TcsTransacaoAccess()
                         {
                             IdTransacao = result.ID_TRANSACAO,
                             RegraAcessoTransacao = result.LX_REGRA_ACESSO_TRANSACAO,
                             Origem = 2
                         }).ToList()
                     ).OrderBy(i => i.IdTransacao).ThenByDescending(i => i.Origem).ThenByDescending(i => i.RegraAcessoTransacao).ToList();

                TcsTransacaoAccess previousTransactionItem = new TcsTransacaoAccess();

                foreach (TcsTransacaoAccess transactionItem in transacaoRegraLista)
                {
                    List<TcsTransacaoSecurity> transacoesListaAux = transacoesLista.Where(i => i.IdTransacao == transactionItem.IdTransacao).ToList();

                    if (previousTransactionItem.IdTransacao != transactionItem.IdTransacao)
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
                throw new DomainException(oException.Message, oException.InnerException);
            }

        }

        private string UpdateTag(List<TransacaoTag.TcsTransacaoTag> transacaoTags, Int64 idTransacao, string tag)
        {
            string tagUsuario = transacaoTags.Where(i => i.IdTransacao == idTransacao).Select(i => i.Tag).FirstOrDefault();
            if (!tagUsuario.IsNullOrEmpty())
            {
                tag = tag + (tag.IsNullOrEmpty() ? "" : ",") + tagUsuario;
            }
            return tag;
        }

        [Query(HasSideEffects = true)]
        public IEnumerable<TcsTransacaoSecurity> GetBoAccess(Guid uidUsuario, string boName, string transaction)
        {
            TcsModuloAccess previousItem = new TcsModuloAccess();
            TcsTransacaoSecurity modulePermissions = new TcsTransacaoSecurity();
            List<TcsModuloAccess> allowedModules = new List<TcsModuloAccess>();
            TcsTransacaoAccess previousTransactionItem = new TcsTransacaoAccess();
            List<TcsTransacaoSecurity> transacaoSecurity = new List<TcsTransacaoSecurity>();
            List<TcsTransacaoSecurity> access = new List<TcsTransacaoSecurity>();
            Int64 idUsuario = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();

            try
            {
                string cacheKey = string.Format("UserAccess_{0}_{1}_{2}_{3}", BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault(), uidUsuario, boName.IsNullOrEmpty() ? "" : boName, transaction.IsNullOrEmpty() ? "" : transaction);
                List<TcsTransacaoSecurity> cache = WebCacheHelper.GetWebCache<List<TcsTransacaoSecurity>>(cacheKey);

                if (!cache.IsNull())
                {
                    access = cache;
                }
                else
                {
                    AutorizacaoDomainService dsAutorizacao = new AutorizacaoDomainService();
                    TransacaoAutorizacao.TransacaoAutorizacaoDomainService ds = new TransacaoAutorizacao.TransacaoAutorizacaoDomainService();
                    ModuloAutorizacao.ModuloAutorizacaoDomainService ModuloAutorizacaoDs = new ModuloAutorizacao.ModuloAutorizacaoDomainService();

                    //Validate User (Inativo - Vigência)
                    dsAutorizacao.ValidateUserAccess(uidUsuario);

                    //TcsTransacao - TcsTransacaoAutorizacao
                    List<Int64> transacoes = this.GetTcsTransacaoAcesso(transaction, boName);

                    if (transacoes.IsNull())
                        return transacaoSecurity;

                    //TcsPerfil
                    List<Int64> tcsPerfil = this.GetTcsPerfilUsuario(idUsuario);

                    //TcsModulo - TcsModuloAutorizacao
                    List<Int64> tcsTransacaoMenu =
                        (from result in this.DbContext.TCS_TRANSACAO_MENU
                         where !result.INATIVO && transacoes.Contains(result.ID_TRANSACAO)
                         select result.ID_MODULO_MENU).ToList().Union(
                        (from result in ds.GetTcsTransacaoMenuAutorizacao().Where(i => !i.Inativo && transacoes.Contains(i.IdTransacao))
                         select result.IdModuloMenu).ToList()).Distinct().ToList();

                    List<Int64> tcsModuloMenu =
                        ((from result in this.DbContext.TCS_MODULO_MENU
                          where tcsTransacaoMenu.Contains(result.ID_MODULO_MENU)
                          select result.ID_MODULO).ToList().Union
                        (from result in ModuloAutorizacaoDs.GetTcsModuloMenuAutorizacaoNoAssociations().Where(i => tcsTransacaoMenu.Contains(i.IdModuloMenu))
                         select result.IdModulo).ToList());

                    List<Int64> modulos =
                        ((
                        (from result in this.DbContext.TCS_MODULO
                         where !result.INATIVO && tcsModuloMenu.Contains(result.ID_MODULO)
                         select result.ID_MODULO
                        ).ToList().Union
                        (from result in ModuloAutorizacaoDs.GetTcsModuloAutorizacaoNoAssociations().Where(i => !i.Inativo && tcsModuloMenu.Contains(i.IdModulo))
                         select result.IdModulo
                        ).ToList().Union
                         (from result in ModuloAutorizacaoDs.GetTcsModuloMenuAutorizacaoNoAssociations().Where(i => tcsTransacaoMenu.Contains(i.IdModulo) && !i.InativoModulo)
                          select result.IdModulo).ToList()).Union
                        (from result in ds.GetTcsTransacaoMenuAutorizacaoNoAssociations().Where(i => transacoes.Contains(i.IdTransacao) && !i.InativoModulo && !i.Inativo)
                         select result.IdModulo).ToList());

                    //TcsUsuarioRegraModulo - TcsPerfilRegraModulo
                    List<TcsModuloAccess> listaModulo =
                        ((from result in this.DbContext.TCS_USUARIO_REGRA_MODULO
                          where modulos.Contains(result.ID_MODULO) && result.TCS_USUARIO.UID_USUARIO == uidUsuario
                          select new TcsModuloAccess() { IdModulo = 0, RegraAcesso = result.LX_REGRA_ACESSO_MODULO, Origem = 3 }
                         ).Union
                         (from result in this.DbContext.TCS_PERFIL_REGRA_MODULO
                          where tcsPerfil.Contains(result.TCS_PERFIL.ID_PERFIL) && modulos.Contains(result.ID_MODULO)
                          select new TcsModuloAccess() { IdModulo = 0, RegraAcesso = result.LX_REGRA_ACESSO_MODULO, Origem = 4 }
                         )).OrderByDescending(i => i.Origem).OrderByDescending(i => i.RegraAcesso).ToList();

                    foreach (TcsModuloAccess item in listaModulo)
                    {
                        ValidateAccess(modulePermissions, item.Origem, item.RegraAcesso, previousItem.RegraAcesso);
                        previousItem = item;
                    }

                    List<TcsTransacaoSecurity> transacoesLista =
                        (from result in transacoes
                         select new TcsTransacaoSecurity()
                         {
                             IdTransacao = 0,
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
                    List<TcsTransacaoAccess> listaTransacao =
                        ((from result in this.DbContext.TCS_USUARIO_REGRA_TRANSACAO
                          where transacoes.Contains(result.ID_TRANSACAO) && result.TCS_USUARIO.UID_USUARIO == uidUsuario
                          select new TcsTransacaoAccess() { IdTransacao = 0, RegraAcessoTransacao = result.LX_REGRA_ACESSO_TRANSACAO, Origem = 1 }
                        ).Union
                        (from result in this.DbContext.TCS_PERFIL_REGRA_TRANSACAO
                         where transacoes.Contains(result.ID_TRANSACAO) && tcsPerfil.Contains(result.TCS_PERFIL.ID_PERFIL)
                         select new TcsTransacaoAccess() { IdTransacao = 0, RegraAcessoTransacao = result.LX_REGRA_ACESSO_TRANSACAO, Origem = 2 }
                        )).OrderBy(i => i.IdTransacao).ThenByDescending(i => i.Origem).ThenByDescending(i => i.RegraAcessoTransacao).ToList();

                    foreach (TcsTransacaoAccess transactionItem in listaTransacao)
                    {
                        List<TcsTransacaoSecurity> transacoesListaAux = transacoesLista.Where(i => i.IdTransacao == transactionItem.IdTransacao).ToList();

                        if (previousTransactionItem.IdTransacao != transactionItem.IdTransacao)
                            previousTransactionItem = transactionItem;

                        if (transacoesListaAux.Count() > 0)
                        {
                            TcsTransacaoSecurity transacao = transacoesListaAux.First();
                            ValidateAccess(transacao, transactionItem.Origem, transactionItem.RegraAcessoTransacao, previousTransactionItem.RegraAcessoTransacao);
                        }

                        previousTransactionItem = transactionItem;
                    }

                    transacaoSecurity = transacoesLista.ToList();

                    access = (from result in transacaoSecurity
                              select result).ToList();

                    WebCacheHelper.UpdateWebCache(cacheKey, access, 720); //Expiração em 30 dias

                }
            }
            catch (Exception oException)
            {
                throw new DomainException(oException.Message, oException.InnerException);
            }

            return access;

        }

        [Query(HasSideEffects = true)]
        public IEnumerable<TcsTransacaoSecurity> GetTcsTransacaoByClassName(Guid uidUsuario, string modules, string className)
        {
            TransacaoAutorizacao.TransacaoAutorizacaoDomainService ds = new TransacaoAutorizacao.TransacaoAutorizacaoDomainService();

            //UidTransacao
            List<Int64> transacoes = this.GetTcsTransacaoAcesso(className, className);

            List<Int64> modulos = (from result in modules.Split(new string[] { "," }, StringSplitOptions.None)
                                   select Convert.ToInt64(result)).ToList();

            //TcsTransacaoMenu - TcsTransacaoMenuAutorizacao
            var mMenu = (
                            (from result in this.GetTcsTransacaoMenuChildNoAssociations().Where(i => !i.Inativo && transacoes.Contains(i.IdTransacao) && modulos.Contains(i.IdModulo))
                             select result.IdModuloMenu).ToList().Union
                            (from result in ds.GetTcsTransacaoMenuAutorizacaoNoAssociations().Where(i => !i.Inativo && transacoes.Contains(i.IdTransacao) && modulos.Contains(i.IdModulo))
                             select result.IdModuloMenu).ToList()
                         ).FirstOrDefault();

            if (mMenu == null)
                return new List<TcsTransacaoSecurity>();
            else
                return GetTcsTransacaoByUserAccess(uidUsuario, mMenu).Where(e => e.ClasseNome == className);
        }

        [Query(HasSideEffects = true)]
        public List<TcsTransacaoDependente> GetTransacaoDependente(Int64 idTransacao)
        {
            TransacaoAutorizacao.TransacaoAutorizacaoDomainService ds = new TransacaoAutorizacao.TransacaoAutorizacaoDomainService();

            return
                (from result in ds.GetTcsTransacaoDependenteAutorizacaoNoAssociations().Where(i => i.IdTransacao == idTransacao)
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
                     IdTransacao = result.IdTransacao,
                     IdTransacaoDependente = result.IdTransacaoDependente,
                     IdTransacaoRelacionada = result.IdTransacaoRelacionada,
                     UsaFiltrosDoBoPrincipal = result.UsaFiltrosDoBoPrincipal,
                     Visivel = result.Visivel,
                     ClasseNome = result.ClasseNome,
                     DescTransacao = result.DescTransacao
                 }).ToList().Union
                (from result in this.GetTcsTransacaoDependenteNoAssociations().Where(i => i.IdTransacao == idTransacao)
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

                case 13:
                    ResetAccess(transacao, false);
                    transacao.AcessoBloqueado = true;
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
        protected internal List<Int64> GetTcsPerfilUsuario(Int64 idUsuario, Dictionary<string, string> headers = null)
        {
            Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService(headers);
            return dsUsuario.GetTcsUsuarioPerfilNoAssociations().Where(i => !i.Inativo && i.IdUsuario == idUsuario).Select(i => i.IdPerfil).ToList();
        }

        [Invoke(HasSideEffects = true)]
        private List<Int64> GetTcsTransacaoAcesso(string transaction, string boName)
        {
            ObjetoAutorizacao.ObjetoAutorizacaoDomainService ds = new ObjetoAutorizacao.ObjetoAutorizacaoDomainService();
            TransacaoAutorizacao.TransacaoAutorizacaoDomainService dsTransacaoAut = new TransacaoAutorizacao.TransacaoAutorizacaoDomainService();
            AutorizacaoDomainService dsAutorizacao = new AutorizacaoDomainService();

            List<string> boList = Utils.GetObjectClassName(boName);

            List<Int64> lstUidObjeto =
                (from result in ds.GetTcsObjetoAutorizacaoNoAssociations() where boList.Contains(result.ClasseNome) select result.IdObjeto).ToList().Union
                (from result in this.DbContext.TCS_OBJETO where boList.Contains(result.CLASSE_NOME) select result.ID_OBJETO).ToList();

            //return
            //    (from result in ds.GetTcsTransacao(transaction, boName).Select(i => i.UidTransacao)
            //     select result
            //    ).ToList().Union
            //    (from result in this.DbContext.TCS_TRANSACAO
            //     where !result.INATIVO && (result.CLASSE_NOME == transaction || boList.Contains(result.CLASSE_NOME))
            //     select result.UID_TRANSACAO
            //    ).ToList();

            return
                (from result in dsAutorizacao.GetTcsTransacao(transaction, boName).Select(i => i.IdTransacao) select result).ToList().Union
                (from result in this.DbContext.TCS_TRANSACAO where !result.INATIVO && (result.CLASSE_NOME == transaction || lstUidObjeto.Contains(result.ID_OBJETO)) select result.ID_TRANSACAO).ToList();
        }

        [Query(HasSideEffects = false)]
        public IEnumerable<TcsTransacaoSecurity> GetTcsTransacaoByUserAccessJson(Guid uidUsuario, Int64 uidModuloMenu)
        {
            return GetTcsTransacaoByUserAccess(uidUsuario, uidModuloMenu);
        }

        [Query(HasSideEffects = true)]
        public IQueryable<TcsTransacao> GetBusinessUidObjeto(string classeNome)
        {
            AutorizacaoDomainService ds = new AutorizacaoDomainService();

            var query =
                from result in ds.GetTcsTransacao(classeNome, null)
                select new TcsTransacao
                {
                    IdObjeto = result.IdObjeto,
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
                    IdObjeto = result.IdObjeto,
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
        public Int64 IdTransacao { get; set; }
        public Int64 IdObjeto { get; set; }
        public string DescObjeto { get; set; }
        public string CodTransacao { get; set; }
        public string DescTransacao { get; set; }
        public string NomeCurto { get; set; }
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
        public string Icone { get; set; }
        public int? LxCorFundo { get; set; }
        public string NomeTabela { get; set; }

        public string Tags { get; set; }

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

        public TcsTransacaoAccess(Int64 idTransacao, int regraAcessoTransacao, int origem)
        {
            IdTransacao = idTransacao;
            RegraAcessoTransacao = regraAcessoTransacao;
            Origem = origem;
        }

        public Int64 IdTransacao { get; set; }
        public int RegraAcessoTransacao { get; set; }
        public int Origem { get; set; }

        //1 -> Tcs_Usuario_Regra_Transacao
        //2 -> Tcs_Perfil_Regra_Transacao
        //3 -> Tcs_Usuario_Regra_Modulo
        //4 -> Tcs_perfil_Regra_Modulo
    }

}
