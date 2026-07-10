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
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using Linx;
using Linx.Framework.ControleSistema.BM;

namespace Linx.TCS0101.BO.TcsPastaUsuario
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class TcsPastaUsuarioDomainService
    {
        Guid uidModulo = Guid.Parse("C97DEC84-4F23-40BE-9F17-3599B06EDF01");
        const string lxConteudoOjeto = null;
        #region Gets


        public IEnumerable<TcsPastaUsuario> GetTcsPastaUsuarioByUser(string userName)
        {
            VerificaControleDeAcesso(userName);

            List<TCS_MODULO_MENU> moduloMenus = new List<TCS_MODULO_MENU>();

            var userFolder = from m in this.DbContext.TCS_MODULO_MENU
                             where m.UID_MODULO == uidModulo && m.DESC_MODULO_MENU == userName && m.UID_MODULO_MENU_SUPERIOR_FK == null
                             select m;

            if (userFolder.Count() == 0)
            {
                TCS_MODULO_MENU moduloMenu = new TCS_MODULO_MENU();
                moduloMenu.UID_MODULO_MENU = Guid.NewGuid();
                moduloMenu.UID_MODULO_MENU_SUPERIOR_FK = null;
                moduloMenu.DESC_MODULO_MENU = userName;
                moduloMenu.UID_MODULO = uidModulo;

                this.DbContext.TCS_MODULO_MENU.Add(moduloMenu);
                this.DbContext.SaveChanges();

                moduloMenus.Add(moduloMenu);
            }
            else
            {
                Action<TCS_MODULO_MENU> recursivo = null;
                recursivo = (modulo) =>
                {
                    moduloMenus.Add(modulo);

                    this.DbContext.TCS_MODULO_MENU
                        .Where(m => m.UID_MODULO == modulo.UID_MODULO
                            && m.UID_MODULO_MENU_SUPERIOR_FK == modulo.UID_MODULO_MENU)
                        .ToList().ForEach(recursivo);

                };
                userFolder.ToList().ForEach(recursivo);
            }

            var result = from m in moduloMenus
                         select new TcsPastaUsuario
                         {
                             UidPastaUsuario = m.UID_MODULO_MENU,
                             DescPastaUsuario = m.DESC_MODULO_MENU,
                             UidPastaUsuarioSuperior = m.UID_MODULO_MENU_SUPERIOR_FK,
                             TemFilhos = m.UID_MODULO_MENU_SUPERIOR_FK == null || this.DbContext.TCS_MODULO_MENU.Count(mmenu => mmenu.UID_MODULO_MENU_SUPERIOR_FK == m.UID_MODULO_MENU) > 0,
                             Usuario = userName
                         };

            return result;
        }


        [Query(HasSideEffects = false)]
        public IEnumerable<TcsDocumentoUsuario> GetTcsDocumentoUsuarioByUser(Guid uidUsuario, Guid uidPastaUsuario)
        {
            TcsAutorizacao.TcsAutorizacaoDomainService ds = new TcsAutorizacao.TcsAutorizacaoDomainService();
            var usuario = ds.GetUser(uidUsuario);

            TcsTransacao.TcsTransacaoDomainService transacaoDs = new TcsTransacao.TcsTransacaoDomainService();

            List<Guid> tcsTransacao =
                (from result in transacaoDs.GetTcsTransacaoMenuNoAssociations().Where(i => i.UidModuloMenu == uidPastaUsuario && !i.Inativo)
                 select result.UidTransacao).ToList();

            if (tcsTransacao.Count() == 0)
                return null;

            return
                from result in this.DbContext.TCS_TRANSACAO
                join result1 in this.DbContext.TCS_OBJETO on result.UID_OBJETO equals result1.UID_OBJETO
                join result2 in this.DbContext.TCS_OBJETO_CONTEUDO on result1.UID_OBJETO equals result2.UID_OBJETO_FK
                where tcsTransacao.Contains(result.UID_TRANSACAO) && result.LX_TIPO_TRANSACAO == 4 //Excel
                select new TcsDocumentoUsuario()
                    {
                        UidTransacao = result.UID_TRANSACAO,
                        UidDocumentoUsuario = result2.UID_OBJETO_CONTEUDO,
                        DescDocumentoUsuario = result.DESC_TRANSACAO,
                        UidObjeto = result.UID_OBJETO,
                        OrdemNavegacao = 0,
                        Conteudo = result2.CONTEUDO_XML
                    };
        }

        [Invoke(HasSideEffects = false)]
        public string GetDocumentoUsuarioConteudo(Guid uidDocumentoUsuario)
        {
            var doc = this.DbContext.TCS_OBJETO_CONTEUDO.FirstOrDefault(oc => oc.UID_OBJETO_CONTEUDO == uidDocumentoUsuario);
            if (doc == null)
                throw new Exception("O documento não foi encontrado na base de dados.");

            return doc.CONTEUDO_XML;
        }




        [Query(HasSideEffects = false)]
        public IEnumerable<TcsPastaUsuario> GetTcsPastaUsuarioByUserJson(string userName)
        {
            return GetTcsPastaUsuarioByUser(userName);
        }

        #endregion

        #region PastaUsuario
        [Invoke(HasSideEffects = false)]
        public Guid InsertTcsPastaUsuario(Guid uidPastaUsuarioSuperior, Guid? uidPastaUsuario, string descPastaUsuario)
        {
            TCS_MODULO_MENU moduloMenu = null;
            if (!uidPastaUsuario.HasValue)
            {
                //insercao
                moduloMenu = new TCS_MODULO_MENU()
                {
                    UID_MODULO_MENU = Guid.NewGuid(),
                    DESC_MODULO_MENU = descPastaUsuario,
                    UID_MODULO_MENU_SUPERIOR_FK = uidPastaUsuarioSuperior,
                    UID_MODULO = uidModulo,
                    ORDEM_NAVEGACAO = 0
                };
                this.DbContext.TCS_MODULO_MENU.Add(moduloMenu);
            }
            else
            {
                //update
                moduloMenu = this.DbContext.TCS_MODULO_MENU.FirstOrDefault(m => m.UID_MODULO_MENU == uidPastaUsuario.Value);
                if (moduloMenu == null) throw new Exception("A pasta solicitada não pode ser encontrada");
                moduloMenu.UID_MODULO_MENU_SUPERIOR_FK = uidPastaUsuarioSuperior;
                moduloMenu.DESC_MODULO_MENU = descPastaUsuario;
            }

            this.DbContext.SaveChanges();
            return moduloMenu.UID_MODULO_MENU;
        }


        [Invoke(HasSideEffects = false)]
        public void DeleteTcsPastaUsuario(Guid uidPastaUsuario)
        {
            TCS_MODULO_MENU moduloMenu = this.DbContext.TCS_MODULO_MENU.FirstOrDefault(m => m.UID_MODULO_MENU == uidPastaUsuario);
            if (moduloMenu == null) throw new Exception("A pasta solicitada não pode ser encontrada");
            this.DbContext.TCS_MODULO_MENU.Remove(moduloMenu);
            this.DbContext.SaveChanges();
        }
        #endregion

        #region Documentos

        [Invoke(HasSideEffects = false)]
        public Guid InsertTcsObjetoConteudo(Guid uidPastaPai, string nomeObjeto, string objetoConteudo, byte lxTipoObjeto, byte lxTipoTransacao, string classeNome)
        {
            //insert tcsObjeto
            TCS_OBJETO tcsObjeto = new TCS_OBJETO()
            {
                UID_OBJETO = Guid.NewGuid(),
                DESC_OBJETO = nomeObjeto,
                CLASSE_NOME = classeNome,
                LX_TIPO_OBJETO = lxTipoObjeto,
                PATH_OBJETO = string.Empty
            };
            this.DbContext.TCS_OBJETO.Add(tcsObjeto);

            //insert tcsTransacao
            TCS_TRANSACAO tcsTransacao = new TCS_TRANSACAO()
            {
                UID_TRANSACAO = Guid.NewGuid(),
                CLASSE_NOME = classeNome,
                COD_TRANSACAO = GetNovoCodTransacao(),
                DESC_TRANSACAO = nomeObjeto,
                LX_TIPO_TRANSACAO = lxTipoTransacao,
                UID_OBJETO = tcsObjeto.UID_OBJETO,
            };
            this.DbContext.TCS_TRANSACAO.Add(tcsTransacao);

            //insert tcsTransacaoMenu
            TCS_TRANSACAO_MENU tcsTransacaoMenu = new TCS_TRANSACAO_MENU()
            {
                UID_MODULO_MENU = uidPastaPai,
                UID_TRANSACAO = tcsTransacao.UID_TRANSACAO,
                INATIVO = false,
                ORDEM_NAVEGACAO = 0,
                SUGESTAO_LINX = false
            };
            this.DbContext.TCS_TRANSACAO_MENU.Add(tcsTransacaoMenu);

            //insert tcsObjetoConteudo
            TCS_OBJETO_CONTEUDO tcsObjetoConteudo = new TCS_OBJETO_CONTEUDO()
            {
                UID_OBJETO_FK = tcsObjeto.UID_OBJETO,
                CONTEUDO_XML = objetoConteudo,
                LX_CONTEUDO_OBJETO = lxConteudoOjeto,
                UID_OBJETO_CONTEUDO = Guid.NewGuid()
            };
            this.DbContext.TCS_OBJETO_CONTEUDO.Add(tcsObjetoConteudo);

            this.DbContext.SaveChanges();

            return tcsObjetoConteudo.UID_OBJETO_CONTEUDO;
        }

        private string GetNovoCodTransacao()
        {
            string codTransacao = "xls";
            int MaxTransacao = this.DbContext.TCS_TRANSACAO.Where(i => i.COD_TRANSACAO.ToUpper().StartsWith("XLS")).Count() + 1;
            codTransacao += (MaxTransacao + 1).ToString().PadLeft(7, '0');
            return codTransacao;
        }


        [Invoke(HasSideEffects = false)]
        public bool DeleteTcsObjetoConteudo(Guid uidObjetoConteudo)
        {
            try
            {
                var objetoConteudo = this.DbContext.TCS_OBJETO_CONTEUDO.FirstOrDefault(
                                    o => o.UID_OBJETO_CONTEUDO == uidObjetoConteudo);
                if (objetoConteudo != null)
                {
                    this.DbContext.TCS_OBJETO.Remove(objetoConteudo.TCS_OBJETO);
                    this.DbContext.SaveChanges();
                }

                return true;
            }
            catch { return false; }

        }


        [Invoke(HasSideEffects = false)]
        public bool UpdateTcsObjetoConteudo(Guid uidObjetoConteudo, string conteudo)
        {
            var objetoConteudo = this.DbContext.TCS_OBJETO_CONTEUDO.FirstOrDefault(
                                    o => o.UID_OBJETO_CONTEUDO == uidObjetoConteudo);
            if (objetoConteudo != null)
            {
                objetoConteudo.CONTEUDO_XML = conteudo;

                this.DbContext.SaveChanges();
            }

            return true;
        }
        #endregion


        #region Controle de Acesso
        private Guid VerificaControleDeAcesso(string userName)
        {
            TcsAutorizacao.TcsAutorizacaoDomainService ds = new TcsAutorizacao.TcsAutorizacaoDomainService();
            TcsAutorizacao.TcsUsuarioAcesso usuario = ds.GetTcsUsuarioAcessoNoAssociations().Where(i => i.NomeAutenticacao == userName).FirstOrDefault();

            if (usuario == null) throw new ApplicationException("Usuário não é válido");

            return usuario.UidUsuario;
        }
        #endregion


        [Invoke(HasSideEffects = false)]
        public bool ExisteDocumentoComMesmoNomeNaPasta(Guid uidPastaPai, string nomeObjeto)
        {
            return this.DbContext.TCS_TRANSACAO_MENU.Count(tm => tm.UID_MODULO_MENU.Equals(uidPastaPai)) > 0 && this.DbContext.TCS_TRANSACAO.Where(i => i.DESC_TRANSACAO == nomeObjeto).Count() > 0;
        }

        [Query(HasSideEffects = false)]
        public IEnumerable<TcsDocumentoUsuario> GetTcsDocumentoUsuarioByUserJson(Guid uidUsuario, Guid uidPastaUsuario)
        {
            return GetTcsDocumentoUsuarioByUser(uidUsuario, uidPastaUsuario);
        }

        [Invoke(HasSideEffects = false)]
        public void DeleteTcsPastaUsuarioJson(Guid uidPastaUsuario)
        {
            DeleteTcsPastaUsuario(uidPastaUsuario);
        }

        [Invoke(HasSideEffects = false)]
        public void InsertTcsPastaUsuarioJson(Guid uidPastaUsuarioSuperior, Guid? uidPastaUsuario, string descPastaUsuario)
        {
            InsertTcsPastaUsuario(uidPastaUsuarioSuperior, uidPastaUsuario, descPastaUsuario);
        }

        [Invoke(HasSideEffects = false)]
        public void UpdateTcsObjetoConteudoJson(Guid uidObjetoConteudo, string conteudo)
        {
            UpdateTcsObjetoConteudo(uidObjetoConteudo, conteudo);
        }

        [Invoke(HasSideEffects = false)]
        public void InsertTcsObjetoConteudoJson(Guid uidPastaPai, string nomeObjeto, string objetoConteudo, byte lxTipoObjeto, byte lxTipoTransacao, string classeNome)
        {
            InsertTcsObjetoConteudo(uidPastaPai, nomeObjeto, objetoConteudo, lxTipoObjeto, lxTipoTransacao, classeNome);
        }

        [Invoke(HasSideEffects = true)]
        public void DeleteTcsObjetoConteudoJson(Guid uidObjetoConteudo)
        {
            DeleteTcsObjetoConteudo(uidObjetoConteudo);
        }


    }
}
