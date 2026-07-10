using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using static Linx.Operacional.BM.Rules.Loja.RegraLoja;

namespace Linx.Operacional.BM.Rules.Loja
{
    public class RepositorioLoja
    {

        private LinxOperacional contexto = null;

        public RepositorioLoja(LinxOperacional contexto)
        {
            this.contexto = contexto;
            this.contexto.Configuration.AutoDetectChangesEnabled = true;
        }

        public LJV_LOJA ResolveLoja(LJV_LOJA ljvLoja, BM.Loja loja)
        {

#if DEBUG
            contexto.Configuration.ProxyCreationEnabled = true;
            contexto.Configuration.LazyLoadingEnabled = true;
#endif

            if (ljvLoja == null)
                throw new Exception("[LJV_LOJA] não encontrada no contexto de atualização. \n ***Crítica gerada por [ResolveLoja]***");

            LJV_LOJA ljv = null;

            if (ljvLoja.ID_LOJA > 0)
            {
                ljv = contexto.LJV_LOJA.Include("TBC_FILIAL")
                                       .Include("TBC_FILIAL.TBC_PFJ")
                                       .Include("LJV_CANAL_VENDA")
                                       .Include("TBC_BANDEIRA_REDE")
                                       .Include("TBC_REGIAO_COMERCIAL")
                                       .Include("STK_DEPOSITO")
                                       .Where(w => (w.ID_LOJA == ljvLoja.ID_LOJA)).FirstOrDefault();
            }
            if (!String.IsNullOrEmpty(ljvLoja.COD_LOJA) && ljv == null)
                ljv = contexto.LJV_LOJA.Include("TBC_FILIAL")
                                       .Include("TBC_FILIAL.TBC_PFJ")
                                       .Include("LJV_CANAL_VENDA")
                                       .Include("TBC_BANDEIRA_REDE")
                                       .Include("TBC_REGIAO_COMERCIAL")
                                       .Include("STK_DEPOSITO")
                                       .Where(w => (w.TBC_FILIAL.TBC_PFJ.CNPJ_CPF == loja.CNPJ_CPF)
                                                && (w.COD_LOJA == ljvLoja.COD_LOJA)
                                                && (w.LJV_CANAL_VENDA.COD_CANAL_VENDA == loja.CodCanalVenda)
                                                && (w.ID_BANDEIRA_REDE == null || (w.ID_BANDEIRA_REDE != null && w.TBC_BANDEIRA_REDE.COD_BANDEIRA_REDE == loja.CodBandeiraRede))
                                                && (w.ID_REGIAO_COMERCIAL == null || (w.ID_REGIAO_COMERCIAL != null && w.TBC_REGIAO_COMERCIAL.COD_REGIAO_COMERCIAL == loja.CodRegiaoComercial))
                                                && (w.ID_STK_DEPOSITO == null || (w.ID_STK_DEPOSITO != null && w.STK_DEPOSITO.COD_DEPOSITO == loja.CodDeposito))
                                              ).FirstOrDefault();

            if (loja.ExcluirLoja != null ? (bool)loja.ExcluirLoja : false)
            {
                if (ljv != null)
                {
                    try
                    {
                        this.Delete(ljv);
                        this.SaveChanges();
                    }
                    catch (Exception err) { throw new Exception("Não foi possível excluir a loja."); }
                }
            }
            else
            {
                string mensagem = "";

                if (!String.IsNullOrEmpty(loja.CNPJ_CPF))
                {
                    var filial = new BM.Rules.Filial.RepositorioFilial(this.contexto).GetPFJ(loja.CNPJ_CPF);
                    if (filial != null)
                    {
                        ljvLoja.ID_FILIAL_PFJ = filial.TBC_FILIAL_LISTA.ID_FILIAL_PFJ;
                        ljvLoja.ID_LINX = filial.TBC_FILIAL_LISTA.ID_LINX;
                        ljvLoja.ID_GPECON = filial.TBC_FILIAL_LISTA.ID_GPECON;
                    }
                    else mensagem += "Filial não encontrada\n";
                }
                else mensagem += "Não foi informado o CNPJ da loja\n";

                if (!String.IsNullOrEmpty(loja.CodCanalVenda))
                {
                    var canalvenda = this.GetCanalVenda(loja.CodCanalVenda);
                    if (canalvenda != null) ljvLoja.ID_LJV_CANAL_VENDA = canalvenda.ID_LJV_CANAL_VENDA;
                    else mensagem += "Código do canal da loja não localizado\n";
                }
                else mensagem += "Não foi informado o Código do canal da loja\n";

                if (!String.IsNullOrEmpty(loja.CodBandeiraRede))
                {
                    var bandrede = this.GetBandeiraRede(loja.CodBandeiraRede);
                    if (bandrede != null) ljvLoja.ID_BANDEIRA_REDE = bandrede.ID_BANDEIRA_REDE;
                }

                if (!String.IsNullOrEmpty(loja.CodRegiaoComercial))
                {
                    var regioaocomercial = this.GetRegiaoComercial(loja.CodRegiaoComercial);
                    if (regioaocomercial != null) ljvLoja.ID_REGIAO_COMERCIAL = regioaocomercial.ID_REGIAO_COMERCIAL;
                }

                if (!String.IsNullOrEmpty(loja.CodDeposito))
                {
                    var deposito = this.GetDeposito(loja.CodDeposito);
                    if (deposito != null) ljvLoja.ID_STK_DEPOSITO = deposito.ID_STK_DEPOSITO;
                }

                if (ljv == null && ljvLoja != null)
                {
                    //nova loja          

                    if (ljvLoja.COD_LOJA.IsNullOrEmpty())
                        mensagem += "Não foi informado o Código da loja\n";

                    if (ljvLoja.DESC_LOJA.IsNullOrEmpty())
                        mensagem += "Não foi informada a Descrição da loja\n";

                    if (!mensagem.IsNullOrEmpty())
                        throw new Exception("Inserção da Loja: " + mensagem);

                    ljvLoja.DATA_CADASTRO = DateTime.Now;
                    ljvLoja.DATA_ATUALIZACAO = DateTime.Now;

                    contexto.LJV_LOJA.Add(ljvLoja);
                    contexto.SaveChanges();
                    ljv = ljvLoja;
                }
                else
                {
                    // altera loja 
                    if ((ljv.ID_FILIAL_PFJ != ljvLoja.ID_FILIAL_PFJ) ||
                        (!ljvLoja.COD_LOJA.IsNullOrEmpty() && ljv.COD_LOJA != ljvLoja.COD_LOJA) ||
                        (!ljvLoja.DESC_LOJA.IsNullOrEmpty() && ljv.DESC_LOJA != ljvLoja.DESC_LOJA) ||
                        (ljv.ID_LJV_CANAL_VENDA != ljvLoja.ID_LJV_CANAL_VENDA) ||
                        (ljv.INDICA_FRANQUIA != ljvLoja.INDICA_FRANQUIA) ||
                        (ljv.INDICA_ECOMMERCE != ljv.INDICA_ECOMMERCE) ||
                        (ljvLoja.ID_BANDEIRA_REDE != null && ljv.ID_BANDEIRA_REDE != ljvLoja.ID_BANDEIRA_REDE) ||
                        (ljvLoja.ID_STK_DEPOSITO != null && ljv.ID_STK_DEPOSITO != ljvLoja.ID_STK_DEPOSITO) ||
                        (ljvLoja.ID_REGIAO_COMERCIAL != null && ljv.ID_REGIAO_COMERCIAL != ljvLoja.ID_REGIAO_COMERCIAL) ||
                        (ljv.INDICA_SOMENTE_CRM != ljvLoja.INDICA_SOMENTE_CRM)
                        )
                    {
                        ljv.DATA_ATUALIZACAO = DateTime.Now;

                        ljv.ID_FILIAL_PFJ = ljvLoja.ID_FILIAL_PFJ;
                        if (!ljvLoja.COD_LOJA.IsNullOrEmpty()) ljv.COD_LOJA = ljvLoja.COD_LOJA;
                        if (!ljvLoja.DESC_LOJA.IsNullOrEmpty()) ljv.DESC_LOJA = ljvLoja.DESC_LOJA;
                        ljv.ID_LJV_CANAL_VENDA = ljvLoja.ID_LJV_CANAL_VENDA;
                        ljv.INDICA_FRANQUIA = ljvLoja.INDICA_FRANQUIA;
                        ljv.INDICA_ECOMMERCE = ljv.INDICA_ECOMMERCE;
                        if (ljvLoja.ID_BANDEIRA_REDE != null) ljv.ID_BANDEIRA_REDE = ljvLoja.ID_BANDEIRA_REDE;
                        if (ljvLoja.ID_STK_DEPOSITO != null) ljv.ID_STK_DEPOSITO = ljvLoja.ID_STK_DEPOSITO;
                        if (ljvLoja.ID_REGIAO_COMERCIAL != null) ljv.ID_REGIAO_COMERCIAL = ljvLoja.ID_REGIAO_COMERCIAL;
                        ljv.INDICA_SOMENTE_CRM = ljvLoja.INDICA_SOMENTE_CRM;
                    }

                    contexto.SaveChanges();
                    ljvLoja = ljv;
                }
            }

            return ljvLoja;
        }

        public LJV_LOJA GetLojaById(int idLoja)
        {
            return contexto.LJV_LOJA
                .Include("TBC_FILIAL")
                .Include("TBC_FILIAL.TBC_PFJ")
                .Where(p => p.ID_LOJA == idLoja)
                .FirstOrDefault();
        }

        public void Add(LJV_CTRL_CONFERENCIA controleConferencia)
        {
            this.contexto.LJV_CTRL_CONFERENCIA.Add(controleConferencia);
        }

        public void Alter(LJV_ATENDIMENTO_PGTO atendimentoPgto)
        {
            this.contexto.Entry(atendimentoPgto).State = EntityState.Modified;
        }

        public void Alter(LJV_CTRL ctrl)
        {
            this.contexto.Entry(ctrl).State = EntityState.Modified;
        }

        public void Alter(LJV_ATENDIMENTO_ITEM atendimentoItem)
        {
            this.contexto.Entry(atendimentoItem).State = EntityState.Modified;
        }

        public void Alter(LJV_CAIXA_LANCAMENTO lancamentoCaixa)
        {
            this.contexto.Entry(lancamentoCaixa).State = EntityState.Modified;
        }

        public void Alter(LJV_CTRL_CONFERENCIA conferencia)
        {
            this.contexto.Entry(conferencia).State = EntityState.Modified;
        }

        public void Delete(LJV_LOJA loja)
        {
            this.contexto.Entry(loja).State = EntityState.Deleted;
        }

        public void SaveChanges()
        {
            this.contexto.SaveChanges();
        }

        public LJV_CANAL_VENDA GetCanalVenda(string codCanalVenda)
        {
            return this.contexto.LJV_CANAL_VENDA.Where(w => w.COD_CANAL_VENDA == codCanalVenda).FirstOrDefault();
        }

        public TBC_BANDEIRA_REDE GetBandeiraRede(string codRedeBandeira)
        {
            return this.contexto.TBC_BANDEIRA_REDE.Where(w => w.COD_BANDEIRA_REDE == codRedeBandeira).FirstOrDefault();
        }

        public TBC_REGIAO_COMERCIAL GetRegiaoComercial(string codRegiaoComercial)
        {
            return this.contexto.TBC_REGIAO_COMERCIAL.Where(w => w.COD_REGIAO_COMERCIAL == codRegiaoComercial).FirstOrDefault();
        }

        public STK_DEPOSITO GetDeposito(string codDeposito)
        {
            return this.contexto.STK_DEPOSITO.Where(w => w.COD_DEPOSITO == codDeposito).FirstOrDefault();
        }

        public LJV_ATENDIMENTO_PGTO GetAtendimentoPagamento(Int64 idPagamento)
        {
            return contexto.LJV_ATENDIMENTO_PGTO.Where(w => w.ID_ATENDIMENTO_PGTO == idPagamento).FirstOrDefault();
        }


        public LJV_ATENDIMENTO_PGTO_ALTERACAO GetAtendimentoPagamentoAlteracao(Int64 idPagamento)
        {
            return contexto.LJV_ATENDIMENTO_PGTO_ALTERACAO.Where(w => w.ID_ATENDIMENTO_PGTO == idPagamento).FirstOrDefault();

        }


        public string GetCodAdmCartao(short idAdmCartao)
        {
            return contexto.LJV_ADM_CARTAO.Where(w => w.ID_ADM_CARTAO == idAdmCartao).FirstOrDefault().COD_ADM_CARTAO;

        }

        public LJV_ATENDIMENTO_ITEM GetAtendimentoItem(Int64 idAtendimentoItem)
        {
            return contexto.LJV_ATENDIMENTO_ITEM.Where(w => w.ID_ATENDIMENTO_ITEM == idAtendimentoItem).FirstOrDefault();
        }

        public LJV_CAIXA_LANCAMENTO GetLancamento(Int64 idLancamento)
        {
            return contexto.LJV_CAIXA_LANCAMENTO.Where(w => w.ID_CAIXA_LANCAMENTO == idLancamento).FirstOrDefault();
        }

        public LJV_CTRL_CONFERENCIA GetControleDeTipoPagamento(Int64 idControle, byte? lxIdTipoPagamento, byte? lxTipoCtrlTipoPgto)
        {
            if (lxIdTipoPagamento != null)
                return contexto.LJV_CTRL_CONFERENCIA.Where(w => w.ID_CONTROLE == idControle && w.LX_ID_TIPO_PGTO == lxIdTipoPagamento).FirstOrDefault();
            else if (lxTipoCtrlTipoPgto != null && lxTipoCtrlTipoPgto == Convert.ToByte(Domains.LX_TIPO_CTRL_TIPO_PGTO.Caixa.Value))
                return contexto.LJV_CTRL_CONFERENCIA.Where(w => w.ID_CONTROLE == idControle && w.LX_TIPO_CTRL_TIPO_PGTO == 2).FirstOrDefault();
            else
                return null;
        }

        public LJV_CTRL_CONFERENCIA GetControleDeTipoPagamento(int idControleConferencia)
        {
            return contexto.LJV_CTRL_CONFERENCIA.Where(w => w.ID_CTRL_CONFERENCIA == idControleConferencia).FirstOrDefault();
        }

        public List<LJV_ATENDIMENTO_PGTO> GetPagamentosAtendimentoLoja(Int64 idControle, int idLoja, int lxIdTipoPagamento, DateTime dataAtendimento)
        {
            return contexto.LJV_ATENDIMENTO_PGTO
                .Include("LJV_TIPO_PGTO")
                .Include("LJV_ATENDIMENTO")
                .Include("LJV_ATENDIMENTO.TBC_FILIAL")
                .Include("LJV_ATENDIMENTO.TBC_FILIAL.TBC_PFJ")
                .Include("LJV_ATENDIMENTO.LJV_CAIXA_CTRL")
                .Include("LJV_ADM_CARTAO")
                .Where(w => w.LJV_ATENDIMENTO.DATA_ATENDIMENTO == dataAtendimento
                         && w.LJV_ATENDIMENTO.ID_LOJA == idLoja
                         && w.LX_ID_TIPO_PGTO == lxIdTipoPagamento
                         && w.LJV_ATENDIMENTO.ATENDIMENTO_CANCELADO == false
                         && w.LJV_ATENDIMENTO.ATENDIMENTO_CANCELADO_ECF == false
                         && w.LJV_ATENDIMENTO.LJV_CAIXA_CTRL.ID_CONTROLE == idControle)
                        .ToList();
        }

        public List<LJV_ATENDIMENTO_PGTO> GetPagamentosAtendimentoLoja(List<Int64> lIdAtendimentoPgtos)
        {
            return contexto.LJV_ATENDIMENTO_PGTO
                .Include("LJV_TIPO_PGTO")
                .Include("LJV_ATENDIMENTO")
                .Include("LJV_ATENDIMENTO.TBC_FILIAL")
                .Include("LJV_ATENDIMENTO.TBC_FILIAL.TBC_PFJ")
                .Include("LJV_ATENDIMENTO.LJV_CAIXA_CTRL")
                .Include("LJV_ADM_CARTAO")
                .Where(w => lIdAtendimentoPgtos.Contains(w.ID_ATENDIMENTO_PGTO))
                        .ToList();
        }

        public List<LJV_ATENDIMENTO_PGTO> GetPagamentosAtendimentoLojaValePresente(Int64 idControle, int idLoja, DateTime dataAtendimento)
        {
            var pagamentos = contexto.LJV_ATENDIMENTO_PGTO
               .Include("LJV_TIPO_PGTO")
               .Include("LJV_ATENDIMENTO")
               .Include("LJV_ATENDIMENTO.TBC_FILIAL")
               .Include("LJV_ATENDIMENTO.TBC_FILIAL.TBC_PFJ")
               .Include("LJV_ATENDIMENTO.LJV_CAIXA_CTRL")
               .Include("LJV_ATENDIMENTO.LJV_ATENDIMENTO_ITEM_LISTA")
               .Include("LJV_ATENDIMENTO.LJV_ATENDIMENTO_ITEM_LISTA.LJV_CAIXA_RECEBIMENTO")
               .Include("LJV_ATENDIMENTO.LJV_ATENDIMENTO_ITEM_LISTA.LJV_CAIXA_RECEBIMENTO.LJV_CAIXA_RECEBIMENTO_PGTO_LISTA")
               .Include("LJV_ADM_CARTAO")
               .Where(w => w.LJV_ATENDIMENTO.DATA_ATENDIMENTO == dataAtendimento
                        && w.LJV_ATENDIMENTO.ID_LOJA == idLoja
                        && w.LJV_ATENDIMENTO.ATENDIMENTO_CANCELADO == false
                        && w.LJV_ATENDIMENTO.ATENDIMENTO_CANCELADO_ECF == false
                        && w.LJV_ATENDIMENTO.LJV_CAIXA_CTRL.ID_CONTROLE == idControle)
                       .ToList();

            List<LJV_ATENDIMENTO_ITEM> itensComValePresente = new List<LJV_ATENDIMENTO_ITEM>();
            foreach (var pag in pagamentos) //Alterando informações somente para considerar o vale presente.
            {
                pag.LJV_ATENDIMENTO.LX_TIPO_ATENDIMENTO = 3;
                pag.LX_ID_TIPO_PGTO = 68;
                itensComValePresente = itensComValePresente.Union(pag.LJV_ATENDIMENTO.LJV_ATENDIMENTO_ITEM_LISTA.Where(w => w.LX_TIPO_ITEM == 4).ToList()).ToList();
            }
            List<Int64> atendimentos = itensComValePresente.Select(s => s.ID_ATENDIMENTO).Distinct().ToList();

            return pagamentos.Where(w => atendimentos.Contains(w.ID_ATENDIMENTO)).ToList();
        }

        public List<LJV_ATENDIMENTO_PGTO> GetPagamentosAtendimentoLojaValePresente(List<Int64> lIdAtendimentoPgtos)
        {
            var pagamentos = contexto.LJV_ATENDIMENTO_PGTO
               .Include("LJV_TIPO_PGTO")
               .Include("LJV_ATENDIMENTO")
               .Include("LJV_ATENDIMENTO.TBC_FILIAL")
               .Include("LJV_ATENDIMENTO.TBC_FILIAL.TBC_PFJ")
               .Include("LJV_ATENDIMENTO.LJV_CAIXA_CTRL")
               .Include("LJV_ATENDIMENTO.LJV_ATENDIMENTO_ITEM_LISTA")
               .Include("LJV_ATENDIMENTO.LJV_ATENDIMENTO_ITEM_LISTA.LJV_CAIXA_RECEBIMENTO")
               .Include("LJV_ATENDIMENTO.LJV_ATENDIMENTO_ITEM_LISTA.LJV_CAIXA_RECEBIMENTO.LJV_CAIXA_RECEBIMENTO_PGTO_LISTA")
               .Include("LJV_ADM_CARTAO")
               .Where(w => lIdAtendimentoPgtos.Contains(w.ID_ATENDIMENTO_PGTO))
                       .ToList();

            List<LJV_ATENDIMENTO_ITEM> itensComValePresente = new List<LJV_ATENDIMENTO_ITEM>();
            foreach (var pag in pagamentos) //Alterando informações somente para considerar o vale presente.
            {
                pag.LJV_ATENDIMENTO.LX_TIPO_ATENDIMENTO = 3;
                pag.LX_ID_TIPO_PGTO = 68;
                itensComValePresente = itensComValePresente.Union(pag.LJV_ATENDIMENTO.LJV_ATENDIMENTO_ITEM_LISTA.Where(w => w.LX_TIPO_ITEM == 4).ToList()).ToList();
            }
            List<Int64> atendimentos = itensComValePresente.Select(s => s.ID_ATENDIMENTO).Distinct().ToList();

            return pagamentos.Where(w => atendimentos.Contains(w.ID_ATENDIMENTO)).ToList();
        }

        public List<LJV_CAIXA_LANCAMENTO> GetLancamentosLojaCaixa(Int64 idControle, int idLoja, DateTime dataLancamento)
        {
            return contexto.LJV_CAIXA_LANCAMENTO
                .Include("LJV_CAIXA_RECEBIMENTO_LISTA")
                .Include("LJV_LANCAMENTO_TIPO")
                .Include("LJV_ECF_OPERACAO")
                .Include("LJV_CAIXA_CTRL")
                .Include("LJV_CAIXA_CTRL.LJV_CTRL")
                .Where(w => w.LJV_CAIXA_CTRL.LJV_CTRL.DATA_CTRL == dataLancamento
                    && w.LJV_CAIXA_CTRL.LJV_CTRL.ID_LOJA == idLoja
                    && w.CANCELADO == false
                    && w.CANCELADO_ECF == false
                    && w.LJV_CAIXA_CTRL.LJV_CTRL.ID_CONTROLE == idControle
                    && w.LJV_CAIXA_RECEBIMENTO_LISTA.Count() == 0)
                    .ToList();
        }

        public List<LJV_CAIXA_LANCAMENTO> GetLancamentosLojaCaixa(List<Int64> lidCaixaLancamento)
        {
            return contexto.LJV_CAIXA_LANCAMENTO
                .Include("LJV_CAIXA_RECEBIMENTO_LISTA")
                .Include("LJV_LANCAMENTO_TIPO")
                .Include("LJV_ECF_OPERACAO")
                .Include("LJV_CAIXA_CTRL")
                .Include("LJV_CAIXA_CTRL.LJV_CTRL")
                .Where(w => lidCaixaLancamento.Contains(w.ID_CAIXA_LANCAMENTO)
                    && w.CANCELADO == false
                    && w.CANCELADO_ECF == false
                    && w.LJV_CAIXA_RECEBIMENTO_LISTA.Count() == 0)
                    .ToList();
        }

        public short? GetIdAdministradoraCartao(string idRedeControladora, string idBandeiraTef)
        {
            short? retorno = null;

            var band = contexto.FIN_TEF_REDE_BANDEIRA
                .Where(w => w.LX_COD_TEF_REDE == idRedeControladora
                    && w.ID_COD_TEF_BANDEIRA == idBandeiraTef)
                .ToList().FirstOrDefault();

            if (band != null && band.LJV_ADM_CARTAO_LISTA != null && band.LJV_ADM_CARTAO_LISTA.Count() > 0)
            {
                if (band.LJV_ADM_CARTAO_LISTA != null && band.LJV_ADM_CARTAO_LISTA.Count() > 0)
                    retorno = band.LJV_ADM_CARTAO_LISTA.First().ID_ADM_CARTAO;
            }

            return retorno;
        }

        /// <summary>
        /// Método para excluir o controle de conferencia
        /// </summary>
        /// <param name="controleTipoPgto"></param>
        public void Excluir(LJV_CTRL_CONFERENCIA controleConferencia)
        {
            this.contexto.Entry(controleConferencia).State = System.Data.Entity.EntityState.Deleted;
        }

        public LNF_CONDICAO_PAGAMENTO GetCondicaoPagamento(int idCondicaoPagamento)
        {
            return contexto.LNF_CONDICAO_PAGAMENTO.Include("LNF_CONDICAO_PAGAMENTO_PARCELA_LISTA").Where(w => w.ID_CONDICAO_PAGAMENTO == idCondicaoPagamento).FirstOrDefault();
        }

        public LJV_CTRL_CONFERENCIA GetConferencia(int idConferencia)
        {
            return contexto.LJV_CTRL_CONFERENCIA.Where(w => w.ID_CTRL_CONFERENCIA == idConferencia).FirstOrDefault();
        }

        public List<LJV_CTRL_CONFERENCIA> GetConferencias(List<int> lstIdConferencia)
        {
            return contexto.LJV_CTRL_CONFERENCIA.Where(w => lstIdConferencia.Contains(w.ID_CTRL_CONFERENCIA)).ToList();
        }

        public List<LJV_CAIXA_LANCAMENTO> GetCaixaLancamentos(Int64 idControle, Int64? idCaixaCtrl, int? idTerminal)
        {
            var query = contexto.LJV_CAIXA_LANCAMENTO
                        .Join(contexto.LJV_CAIXA_CTRL, cl => cl.ID_CAIXA_CTRL, cc => cc.ID_CAIXA_CTRL, (cl, cc) => new
                        {
                            cl,
                            cc
                        })
                        .Join(contexto.LJV_CTRL, j1 => j1.cc.ID_CONTROLE, c => c.ID_CONTROLE, (j1, c) => new
                        {
                            j1.cl,
                            j1.cc,
                            c
                        })
                        .Join(contexto.LJV_LANCAMENTO_TIPO, j2 => j2.cl.ID_LANCAMENTO_TIPO, lt => lt.ID_LANCAMENTO_TIPO, (j2, lt) => new
                        {
                            j2.cl,
                            j2.cc,
                            j2.c,
                            lt
                        })
                        .GroupJoin(contexto.LJV_CAIXA_RECEBIMENTO, gj => gj.cl.ID_CAIXA_LANCAMENTO, cr => cr.ID_CAIXA_LANCAMENTO, (gj, cr) => new
                        {
                            gj.cl,
                            gj.cc,
                            gj.c,
                            gj.lt,
                            cr
                        })
                        .SelectMany(sm => sm.cr.DefaultIfEmpty(), (sm, cr) => new
                        {
                            sm.cl,
                            sm.cc,
                            sm.c,
                            sm.lt,
                            cr
                        })
                        .Where(w => w.cl.CANCELADO == false && w.cl.CANCELADO_ECF == false &&
                                w.lt.ID_OPERACAO_FINALIDADE != null && w.cr == null &&
                                w.c.ID_CONTROLE == idControle
                            );

            if (idCaixaCtrl != null)
                query = query.Where(w => w.cc.ID_CAIXA_CTRL == idCaixaCtrl);

            if (idTerminal != null)
                query = query.Where(w => w.cc.ID_TERMINAL == idTerminal);

            return query.Select(s => s.cl).ToList();

        }

        public List<LJV_ATENDIMENTO_PGTO> GetAtendimentoPgtos(Int64 idControle, byte lxIdTipoPgto, Int64? idCaixaCtrl, int? idTerminal, int? idAdmCartao)
        {
            var query = contexto.LJV_ATENDIMENTO
                        .Join(contexto.LJV_CAIXA_CTRL, a => a.ID_CAIXA_CTRL, cc => cc.ID_CAIXA_CTRL, (a, cc) => new
                        {
                            a,
                            cc
                        })
                        .Join(contexto.LJV_CTRL, j1 => j1.cc.ID_CONTROLE, c => c.ID_CONTROLE, (j1, c) => new
                        {
                            j1.a,
                            j1.cc,
                            c
                        })
                        .Join(contexto.LJV_ATENDIMENTO_PGTO, j2 => j2.a.ID_ATENDIMENTO, ap => ap.ID_ATENDIMENTO, (j2, ap) => new
                        {
                            j2.a,
                            j2.cc,
                            j2.c,
                            ap
                        })
                       .GroupJoin(contexto.LJV_ADM_CARTAO, gj => gj.ap.ID_ADM_CARTAO, ac => ac.ID_ADM_CARTAO, (gj, ac) => new
                       {
                           gj.a,
                           gj.ap,
                           gj.cc,
                           gj.c,
                           ac
                       })
                        .SelectMany(sm => sm.ac.DefaultIfEmpty(), (sm, ac) => new
                        {
                            sm.a,
                            sm.ap,
                            sm.cc,
                            sm.c,
                            ac
                        })
                        .Where(w => w.a.ATENDIMENTO_CANCELADO == false && w.a.ATENDIMENTO_CANCELADO_ECF == false &&
                                    w.c.ID_CONTROLE == idControle && w.ap.LX_ID_TIPO_PGTO == lxIdTipoPgto
                              );

            if (idAdmCartao != null)
                query = query.Where(w => w.ap.ID_ADM_CARTAO == idAdmCartao);

            if (idCaixaCtrl != null)
                query = query.Where(w => w.cc.ID_CAIXA_CTRL == idCaixaCtrl);

            if (idTerminal != null)
                query = query.Where(w => w.cc.ID_TERMINAL == idTerminal);

            return query.Select(s => s.ap).ToList();
        }

        //public List<LJV_ATENDIMENTO_ITEM> GetAtendimentoItemByAtendimentoPgto(List<Guid> lAtendimentoItem, Guid atendimentoPgto)
        //{
        //    var query = contexto.LJV_ATENDIMENTO_ITEM
        //               .Join(contexto.LJV_ATENDIMENTO, ai => ai.ID_ATENDIMENTO, a => a.ID_ATENDIMENTO, (ai, a) => new
        //               {
        //                   a,
        //                   ai
        //               })
        //               .Join(contexto.LJV_ATENDIMENTO_PGTO, ap => ap.a.LJV_ATENDIMENTO_PGTO_LISTA.Contains(new LJV_ATENDIMENTO_PGTO(){ID_ATENDIMENTO_PGTO = atendimentoPgto})  )
        //               //.Join(contexto.LJV_CAIXA_CTRL, j1 => j1.a.ID_CAIXA_CTRL, cc => cc.ID_CAIXA_CTRL, (j1, cc) => new
        //{
        //    j1.a,
        //    j1.ai,
        //    cc
        //})
        //.Join(contexto.LJV_CTRL, j2 => j2.cc.ID_CONTROLE, c => c.ID_CONTROLE, (j2, c) => new
        //{
        //    j2.a,
        //    j2.ai,
        //    j2.cc,
        //    c
        //})
        // .Select(s => s.a.LJV_ATENDIMENTO_PGTO_LISTA
        // .Where(w => w.a.LJV_ATENDIMENTO_PGTO_LISTA.Select(s=> s.ID_ATENDIMENTO_PGTO)
        //.Where(w => w.ai.LX_TIPO_ITEM == 4 &&
        //            w.c.ID_CONTROLE == idControle
        //        );

        //if (idCaixaCtrl != null)
        //    query = query.Where(w => w.cc.ID_CAIXA_CTRL == idCaixaCtrl);

        //if (idTerminal != null)
        //    query = query.Where(w => w.cc.ID_TERMINAL == idTerminal);

        //    return query.Select(s => s.ai).ToList();
        //}

        public List<LJV_ATENDIMENTO_ITEM> GetAtendimentoItens(Int64 idControle, Int64? idCaixaCtrl, int? idTerminal)
        {
            var query = contexto.LJV_ATENDIMENTO_ITEM
                        .Join(contexto.LJV_ATENDIMENTO, ai => ai.ID_ATENDIMENTO, a => a.ID_ATENDIMENTO, (ai, a) => new
                        {
                            a,
                            ai
                        })
                        .Join(contexto.LJV_CAIXA_CTRL, j1 => j1.a.ID_CAIXA_CTRL, cc => cc.ID_CAIXA_CTRL, (j1, cc) => new
                        {
                            j1.a,
                            j1.ai,
                            cc
                        })
                        .Join(contexto.LJV_CTRL, j2 => j2.cc.ID_CONTROLE, c => c.ID_CONTROLE, (j2, c) => new
                        {
                            j2.a,
                            j2.ai,
                            j2.cc,
                            c
                        })
                        .Where(w => w.ai.LX_TIPO_ITEM == 4 &&
                                    w.c.ID_CONTROLE == idControle
                                );

            if (idCaixaCtrl != null)
                query = query.Where(w => w.cc.ID_CAIXA_CTRL == idCaixaCtrl);

            if (idTerminal != null)
                query = query.Where(w => w.cc.ID_TERMINAL == idTerminal);

            return query.Select(s => s.ai).ToList();
        }

        public List<LJV_CTRL> GetControles(List<Int64> lstIdControles)
        {
            return contexto.LJV_CTRL.Where(w => lstIdControles.Contains(w.ID_CONTROLE)).ToList();
        }

        public bool ExisteConferenciaPendente(Int64 idControle)
        {
            byte integrado = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Integrado.Value);

            return contexto.LJV_CTRL_CONFERENCIA.Where(w => w.ID_CONTROLE == idControle && w.LX_STATUS_CONFERENCIA != integrado).Any();
        }

        public List<ConferenciaAutomatica> GetAtendimentosPgtoConferir(int IdLoja)
        {
            List<ConferenciaAutomatica> retorno = new List<ConferenciaAutomatica>();

            Byte integrado = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Integrado.Value);
            int? idGpecon = Linx.Business.Tools.UserServiceHelper.GetCurrentIdGpecon();

            //Conforme definição, o sistema sem irá buscar os atendimento com D-1 a data atual
            DateTime dataInicial = DateTime.Now.Date.AddDays(-1);
            DateTime dataFinal = DateTime.Now.Date;

            //Crio lista para buscar os tipos de pagamento para seleção

            //Cartões
            List<int> lstPagamentos = new List<int>() { 1, 4, 7, 9 };

            //Cheque
            lstPagamentos.AddRange(new List<int>() { 2, 11 });

            //Duplicata

            lstPagamentos.Add(8);

            //Aviso de débito

            lstPagamentos.AddRange(new List<int>() { 12, 13, 18 });

            //Aviso de crédito

            lstPagamentos.AddRange(new List<int>() { 5, 15 });

            //Dinheiro

            lstPagamentos.AddRange(new List<int>() { 3, 10 });

            //Venda gift Card
            lstPagamentos.Add(68);

            //Convênio e Crediário
            lstPagamentos.AddRange(new List<int>() { 6, 15 });

            //Vendas
            var queryVendas = contexto.LJV_ATENDIMENTO
                        .Join(contexto.LJV_CAIXA_CTRL, a => a.ID_CAIXA_CTRL, cc => cc.ID_CAIXA_CTRL, (a, cc) => new
                        {
                            a,
                            cc
                        })
                        .Join(contexto.LJV_CTRL, j1 => j1.cc.ID_CONTROLE, c => c.ID_CONTROLE, (j1, c) => new
                        {
                            j1.a,
                            j1.cc,
                            c
                        })
                        .Join(contexto.LJV_LOJA, j2 => j2.a.ID_LOJA, l => l.ID_LOJA, (j2, l) => new
                        {
                            j2.a,
                            j2.cc,
                            j2.c,
                            l
                        })
                        .Join(contexto.LJV_ATENDIMENTO_PGTO, j3 => j3.a.ID_ATENDIMENTO, ap => ap.ID_ATENDIMENTO, (j3, ap) => new
                        {
                            j3.a,
                            j3.cc,
                            j3.c,
                            j3.l,
                            ap
                        })
                        .Join(contexto.LJV_TIPO_PGTO, j4 => j4.ap.LX_ID_TIPO_PGTO, tp => tp.LX_ID_TIPO_PGTO, (j4, tp) => new
                        {
                            j4.a,
                            j4.cc,
                            j4.c,
                            j4.l,
                            j4.ap,
                            tp
                        })
                        .Join(contexto.LJV_TERMINAL, j5 => j5.cc.ID_TERMINAL, t => t.ID_TERMINAL, (j5, t) => new
                        {
                            j5.a,
                            j5.cc,
                            j5.c,
                            j5.l,
                            j5.ap,
                            j5.tp,
                            t
                        })
                        .Join(contexto.LJV_CTRL_CONFERENCIA
                            , j6 => new
                            {
                                j6.c.ID_CONTROLE,
                                j6.ap.LX_ID_TIPO_PGTO,
                                DATA_MOV = j6.c.DATA_CTRL
                            }
                            , conf => new
                            {
                                conf.ID_CONTROLE,
                                LX_ID_TIPO_PGTO = (byte)conf.LX_ID_TIPO_PGTO,
                                conf.DATA_MOV
                            }
                        , (j6, conf) => new
                        {
                            j6.a,
                            j6.cc,
                            j6.c,
                            j6.l,
                            j6.ap,
                            j6.tp,
                            j6.t,
                            conf
                        }
                        )
                        .GroupJoin(contexto.LJV_ADM_CARTAO, gj => gj.ap.ID_ADM_CARTAO, ac => ac.ID_ADM_CARTAO, (gj, ac) => new
                        {
                            gj.a,
                            gj.ap,
                            gj.cc,
                            gj.c,
                            gj.l,
                            gj.tp,
                            gj.t,
                            gj.conf,
                            ac
                        })
                        .SelectMany(sm => sm.ac.DefaultIfEmpty(), (sm, ac) => new
                        {
                            sm.a,
                            sm.ap,
                            sm.cc,
                            sm.c,
                            sm.l,
                            sm.tp,
                            sm.t,
                            sm.conf,
                            ac
                        })
                        .Where(w => w.c.DATA_CTRL >= dataInicial && w.c.DATA_CTRL <= dataFinal &&
                                    w.a.ID_LOJA == IdLoja && lstPagamentos.Contains(w.ap.LX_ID_TIPO_PGTO) &&
                                    (w.ap.ID_ADM_CARTAO == w.conf.ID_ADM_CARTAO) &&
                                    (w.ap.ID_BANDEIRA_TEF.Trim() == w.conf.ID_COD_TEF_BANDEIRA.Trim()) &&
                                    (w.ap.LX_COD_TEF_REDE.Trim() == w.conf.LX_COD_TEF_REDE.Trim()) &&
                                    (w.cc.ID_CAIXA_CTRL == w.conf.ID_CAIXA_CTRL || w.conf.ID_CAIXA_CTRL == null) &&
                                    (w.cc.ID_TERMINAL == w.conf.ID_TERMINAL || w.conf.ID_TERMINAL == null) &&
                                    w.ap.LX_STATUS_CONFERENCIA != 4
                              );

            queryVendas = queryVendas.Where(w => w.ap.LX_STATUS_CONFERENCIA != integrado);

            if (idGpecon != null)
                queryVendas = queryVendas.Where(w => w.a.ID_GPECON == idGpecon);

            var lstVendas = queryVendas.Select(s => new
            {
                AtendimentoCancelado = s.a.ATENDIMENTO_CANCELADO,
                IdAdmCartao = s.ac.ID_ADM_CARTAO == null ? null : (short?)s.ac.ID_ADM_CARTAO,
                IdAtendimentoPgto = s.ap.ID_ATENDIMENTO_PGTO,
                IdLoja = s.l.ID_LOJA,
                LxIdTipoPgto = s.ap.LX_ID_TIPO_PGTO,
                IdControle = s.c.ID_CONTROLE,
                DataCtrl = s.c.DATA_CTRL,
                IdCaixaCtrl = s.conf.ID_CAIXA_CTRL,
                IdCtrlConferencia = s.conf.ID_CTRL_CONFERENCIA,
                IdLinxConf = s.conf.ID_LINX,
                LxStatusConferencia = s.ap.LX_STATUS_CONFERENCIA
            }).ToList();

            if (lstVendas.Any())
            {
                var vendaAgrupada = lstVendas.GroupBy(gb => new
                {
                    DATA_MOV = gb.DataCtrl,
                    ID_ADM_CARTAO = gb.IdAdmCartao,
                    ID_CONTROLE = gb.IdControle,
                    ID_CTRL_CONFERENCIA = gb.IdCtrlConferencia,
                    ID_LINX_CONF = gb.IdLinxConf,
                    ID_LOJA = gb.IdLoja,
                    LX_ID_TIPO_PGTO = gb.LxIdTipoPgto
                }).Select(s => new
                {
                    s.Key.DATA_MOV,
                    s.Key.ID_ADM_CARTAO,
                    s.Key.ID_CONTROLE,
                    s.Key.ID_CTRL_CONFERENCIA,
                    s.Key.ID_LINX_CONF,
                    s.Key.ID_LOJA,
                    s.Key.LX_ID_TIPO_PGTO
                }).ToList();

                foreach (var venda in vendaAgrupada)
                {
                    ConferenciaAutomatica conferencia = new ConferenciaAutomatica()
                    {
                        ID_LINX_CONF = venda.ID_LINX_CONF,
                        ID_CTRL_CONFERENCIA = venda.ID_CTRL_CONFERENCIA,
                        ID_CONTROLE = venda.ID_CONTROLE,
                        LX_ID_TIPO_PGTO = venda.LX_ID_TIPO_PGTO,
                        DATA_MOV = venda.DATA_MOV,
                        ID_LOJA = venda.ID_LOJA,
                        ID_ADM_CARTAO = venda.ID_ADM_CARTAO,
                        ObsIntegracao = null,
                        Atendimento_LISTA = lstVendas.Where(w => w.AtendimentoCancelado == false &&
                                                                w.IdLinxConf == venda.ID_LINX_CONF &&
                                                                w.IdCtrlConferencia == venda.ID_CTRL_CONFERENCIA
                                                            )
                                            .Select(s => new AtendimentoPgtoConferencia { IdAtendimentoPgto = s.IdAtendimentoPgto, IdCtrlConferencia = s.IdCtrlConferencia, LxStatusConferencia = s.LxStatusConferencia, Cancelado = s.AtendimentoCancelado })
                                            .ToList(),
                        AtendimentoItem_LISTA = new List<AtendimentoItemConferencia>(),
                        LancamentoCaixa_LISTA = new List<LancamentoCaixaConferencia>(),
                    };

                    retorno.Add(conferencia);
                }
            }
            //Venda cartão presente

            var queryVendasCartaoPresente = contexto.LJV_ATENDIMENTO_ITEM
                                               .Join(contexto.LJV_ATENDIMENTO, ai => ai.ID_ATENDIMENTO, a => a.ID_ATENDIMENTO, (ai, a) => new
                                               {
                                                   a,
                                                   ai
                                               })
                                               .Join(contexto.LJV_CAIXA_CTRL, j1 => j1.a.ID_CAIXA_CTRL, cc => cc.ID_CAIXA_CTRL, (j1, cc) => new
                                               {
                                                   j1.a,
                                                   j1.ai,
                                                   cc
                                               })
                                               .Join(contexto.LJV_CTRL, j2 => j2.cc.ID_CONTROLE, c => c.ID_CONTROLE, (j2, c) => new
                                               {
                                                   j2.a,
                                                   j2.ai,
                                                   j2.cc,
                                                   c
                                               })
                                               .Join(contexto.LJV_LOJA, j3 => j3.a.ID_LOJA, l => l.ID_LOJA, (j3, l) => new
                                               {
                                                   j3.a,
                                                   j3.ai,
                                                   j3.cc,
                                                   j3.c,
                                                   l
                                               })
                                               .Join(contexto.LJV_TERMINAL, j4 => j4.cc.ID_TERMINAL, t => t.ID_TERMINAL, (j4, t) => new
                                               {
                                                   j4.a,
                                                   j4.ai,
                                                   j4.cc,
                                                   j4.c,
                                                   j4.l,
                                                   t
                                               })
                                               .Join(contexto.LJV_CTRL_CONFERENCIA
                                                    , j5 => new
                                                    {
                                                        j5.c.ID_CONTROLE,
                                                        LX_ID_TIPO_PGTO = (byte)68,
                                                        DATA_MOV = j5.c.DATA_CTRL
                                                    }
                                                    , conf => new
                                                    {
                                                        conf.ID_CONTROLE,
                                                        LX_ID_TIPO_PGTO = (byte)conf.LX_ID_TIPO_PGTO,
                                                        conf.DATA_MOV
                                                    }
                                                , (j5, conf) => new
                                                {
                                                    j5.a,
                                                    j5.ai,
                                                    j5.cc,
                                                    j5.c,
                                                    j5.l,
                                                    j5.t,
                                                    conf
                                                }
                                                )
                                               .Where(w => w.c.DATA_CTRL >= dataInicial && w.c.DATA_CTRL <= dataFinal && w.ai.LX_TIPO_ITEM == 4 &&
                                                        w.a.ID_LOJA == IdLoja &&
                                                        (w.cc.ID_CAIXA_CTRL == w.conf.ID_CAIXA_CTRL || w.conf.ID_CAIXA_CTRL == null) &&
                                                        (w.cc.ID_TERMINAL == w.conf.ID_TERMINAL || w.conf.ID_TERMINAL == null)
                                                     );

            queryVendasCartaoPresente = queryVendasCartaoPresente.Where(w => w.ai.LX_STATUS_CONFERENCIA != integrado);

            if (idGpecon != null)
                queryVendasCartaoPresente = queryVendasCartaoPresente.Where(w => w.a.ID_GPECON == idGpecon);

            var lstVendasCartaoPresente = queryVendasCartaoPresente.Select(s => new
            {
                AtendimentoCancelado = s.a.ATENDIMENTO_CANCELADO,
                IdAtendimentoPgto = s.ai.ID_ATENDIMENTO_ITEM,
                IdLoja = s.l.ID_LOJA,
                LxIdTipoPgto = (byte)68,
                IdControle = s.c.ID_CONTROLE,
                DataCtrl = s.c.DATA_CTRL,
                IdCaixaCtrl = s.conf.ID_CAIXA_CTRL,
                IdCtrlConferencia = s.conf.ID_CTRL_CONFERENCIA,
                IdLinxConf = s.conf.ID_LINX,
                LxStatusConferencia = s.ai.LX_STATUS_CONFERENCIA
            }).ToList();

            if (lstVendasCartaoPresente.Any())
            {

                var vendaPresenteAgrupada = lstVendas.GroupBy(gb => new
                {
                    DATA_MOV = gb.DataCtrl,
                    ID_CONTROLE = gb.IdControle,
                    ID_CTRL_CONFERENCIA = gb.IdCtrlConferencia,
                    ID_LINX_CONF = gb.IdLinxConf,
                    ID_LOJA = gb.IdLoja,
                    LX_ID_TIPO_PGTO = gb.LxIdTipoPgto
                }).Select(s => new
                {
                    s.Key.DATA_MOV,
                    s.Key.ID_CONTROLE,
                    s.Key.ID_CTRL_CONFERENCIA,
                    s.Key.ID_LINX_CONF,
                    s.Key.ID_LOJA,
                    s.Key.LX_ID_TIPO_PGTO
                }).ToList();

                foreach (var venda in vendaPresenteAgrupada)
                {
                    ConferenciaAutomatica conferencia = new ConferenciaAutomatica()
                    {
                        ID_LINX_CONF = venda.ID_LINX_CONF,
                        ID_CTRL_CONFERENCIA = venda.ID_CTRL_CONFERENCIA,
                        ID_CONTROLE = venda.ID_CONTROLE,
                        LX_ID_TIPO_PGTO = venda.LX_ID_TIPO_PGTO,
                        DATA_MOV = venda.DATA_MOV,
                        ID_LOJA = venda.ID_LOJA,
                        ID_ADM_CARTAO = null,
                        ObsIntegracao = null,
                        AtendimentoItem_LISTA = lstVendasCartaoPresente.Where(w => w.AtendimentoCancelado == false &&
                                                                w.IdLinxConf == venda.ID_LINX_CONF &&
                                                                w.IdCtrlConferencia == venda.ID_CTRL_CONFERENCIA
                                                            )
                                            .Select(s => new AtendimentoItemConferencia { IdAtendimentoItem = s.IdAtendimentoPgto, IdCtrlConferencia = s.IdCtrlConferencia, LxStatusConferencia = s.LxStatusConferencia, Cancelado = s.AtendimentoCancelado })
                                            .ToList(),
                        Atendimento_LISTA = new List<AtendimentoPgtoConferencia>(),
                        LancamentoCaixa_LISTA = new List<LancamentoCaixaConferencia>()
                    };

                    retorno.Add(conferencia);
                }
            }

            return retorno;
        }

        public List<ConferenciaAutomatica> GetMovimentosCaixaConferir(int IdLoja)
        {
            List<ConferenciaAutomatica> retorno = new List<ConferenciaAutomatica>();

            Byte integrado = Convert.ToByte(Domains.LX_STATUS_CONFERENCIA_CTRL.Integrado.Value);
            int? idGpecon = Linx.Business.Tools.UserServiceHelper.GetCurrentIdGpecon();

            //Conforme definição, o sistema sem irá buscar os atendimento com D-1 a data atual
            DateTime dataInicial = DateTime.Now.Date.AddDays(-1);
            DateTime dataFinal = DateTime.Now.Date;

            var queryMovimentoCaixa = contexto.LJV_CAIXA_LANCAMENTO
                                      .Join(contexto.LJV_CAIXA_CTRL, cl => cl.ID_CAIXA_CTRL, cc => cc.ID_CAIXA_CTRL, (cl, cc) => new
                                      {
                                          cl,
                                          cc
                                      })
                                      .Join(contexto.LJV_CTRL, j1 => j1.cc.ID_CONTROLE, c => c.ID_CONTROLE, (j1, c) => new
                                      {
                                          j1.cl,
                                          j1.cc,
                                          c
                                      })
                                      .Join(contexto.LJV_LANCAMENTO_TIPO, j2 => j2.cl.ID_LANCAMENTO_TIPO, lt => lt.ID_LANCAMENTO_TIPO, (j2, lt) => new
                                      {
                                          j2.cl,
                                          j2.cc,
                                          j2.c,
                                          lt
                                      })
                                      .Join(contexto.LJV_LOJA, j3 => j3.c.ID_LOJA, l => l.ID_LOJA, (j3, l) => new
                                      {
                                          j3.cl,
                                          j3.cc,
                                          j3.c,
                                          j3.lt,
                                          l
                                      })
                                      .Join(contexto.LJV_TERMINAL, j4 => j4.cc.ID_TERMINAL, t => t.ID_TERMINAL, (j4, t) => new
                                      {
                                          j4.cl,
                                          j4.cc,
                                          j4.c,
                                          j4.lt,
                                          j4.l,
                                          t
                                      })
                                      .Join(contexto.LJV_CTRL_CONFERENCIA
                                            , j5 => new
                                            {
                                                LX_TIPO_CTRL_TIPO_PGTO = (byte)2,
                                                ID_CONTROLE = j5.c.ID_CONTROLE,
                                                DATA_MOV = j5.c.DATA_CTRL,
                                            }
                                            , conf => new
                                            {
                                                conf.LX_TIPO_CTRL_TIPO_PGTO,
                                                conf.ID_CONTROLE,
                                                conf.DATA_MOV
                                            }
                                      , (j5, conf) => new
                                      {
                                          j5.cl,
                                          j5.cc,
                                          j5.c,
                                          j5.lt,
                                          j5.l,
                                          j5.t,
                                          conf
                                      })
                                      .GroupJoin(contexto.LJV_CAIXA_RECEBIMENTO, gj => gj.cl.ID_CAIXA_LANCAMENTO, cr => cr.ID_CAIXA_LANCAMENTO, (gj, cr) => new
                                      {
                                          gj.cl,
                                          gj.cc,
                                          gj.c,
                                          gj.lt,
                                          gj.l,
                                          gj.t,
                                          gj.conf,
                                          cr
                                      })
                                      .SelectMany(sm => sm.cr.DefaultIfEmpty(), (sm, cr) => new
                                      {
                                          sm.cl,
                                          sm.cc,
                                          sm.c,
                                          sm.lt,
                                          sm.l,
                                          sm.t,
                                          sm.conf,
                                          cr
                                      })
                                      .Where(w => w.cl.CANCELADO == false && w.cl.CANCELADO_ECF == false &&
                                                w.c.DATA_CTRL >= dataInicial && w.c.DATA_CTRL <= dataFinal &&
                                                w.lt.ID_OPERACAO_FINALIDADE != null && w.cr == null &&
                                                w.c.ID_LOJA == IdLoja &&
                                                (w.cc.ID_CAIXA_CTRL == w.conf.ID_CAIXA_CTRL || w.conf.ID_CAIXA_CTRL == null) &&
                                                (w.cc.ID_TERMINAL == w.conf.ID_TERMINAL || w.conf.ID_TERMINAL == null) &&
                                                w.cl.LX_STATUS_CONFERENCIA != 4
                                            );

            queryMovimentoCaixa = queryMovimentoCaixa.Where(w => w.cl.LX_STATUS_CONFERENCIA != integrado);

            if (idGpecon != null)
                queryMovimentoCaixa = queryMovimentoCaixa.Where(w => w.c.ID_GPECON == idGpecon);

            var lstCaixaLancamentos = queryMovimentoCaixa
                                   .Select(s => new
                                   {
                                       DataCtrl = s.c.DATA_CTRL,
                                       IdCaixaCtrl = s.conf.ID_CAIXA_CTRL,
                                       IdCaixaLancamento = s.cl.ID_CAIXA_LANCAMENTO,
                                       IdControle = s.c.ID_CONTROLE,
                                       IdEcfOperacao = s.cl.ID_ECF_OPERACAO,
                                       IdLancamentoTipo = s.lt.ID_LANCAMENTO_TIPO,
                                       IdLoja = s.l.ID_LOJA,
                                       IndicaSaida = s.lt.INDICA_SAIDA,
                                       LxIdTipoPgto = s.cl.LX_ID_TIPO_PGTO,
                                       LxStatusOperacao = s.c.LX_STATUS_OPERACAO,
                                       IdCtrlConferencia = s.conf.ID_CTRL_CONFERENCIA,
                                       IdLinxConf = s.conf.ID_LINX,
                                       LxStatusConferencia = s.cl.LX_STATUS_CONFERENCIA
                                   }).ToList();

            if (lstCaixaLancamentos.Any())
            {
                var lancamentoAgrupado = lstCaixaLancamentos.GroupBy(gb => new
                {
                    DATA_MOV = gb.DataCtrl,
                    ID_CONTROLE = gb.IdControle,
                    ID_CTRL_CONFERENCIA = gb.IdCtrlConferencia,
                    ID_LINX_CONF = gb.IdLinxConf,
                    ID_LOJA = gb.IdLoja,
                    LX_ID_TIPO_PGTO = gb.LxIdTipoPgto
                }).Select(s => new
                {
                    s.Key.DATA_MOV,
                    s.Key.ID_CONTROLE,
                    s.Key.ID_CTRL_CONFERENCIA,
                    s.Key.ID_LINX_CONF,
                    s.Key.ID_LOJA,
                    s.Key.LX_ID_TIPO_PGTO
                }).ToList();

                foreach (var lancamento in lancamentoAgrupado)
                {
                    ConferenciaAutomatica conferencia = new ConferenciaAutomatica()
                    {
                        ID_LINX_CONF = lancamento.ID_LINX_CONF,
                        ID_CTRL_CONFERENCIA = lancamento.ID_CTRL_CONFERENCIA,
                        ID_CONTROLE = lancamento.ID_CONTROLE,
                        LX_ID_TIPO_PGTO = lancamento.LX_ID_TIPO_PGTO,
                        DATA_MOV = lancamento.DATA_MOV,
                        ID_LOJA = lancamento.ID_LOJA,
                        ID_ADM_CARTAO = null,
                        ObsIntegracao = null,
                        AtendimentoItem_LISTA = new List<AtendimentoItemConferencia>(),
                        Atendimento_LISTA = new List<AtendimentoPgtoConferencia>(),
                        LancamentoCaixa_LISTA = lstCaixaLancamentos.Where(w => w.IdLinxConf == lancamento.ID_LINX_CONF && w.IdCtrlConferencia == lancamento.ID_CTRL_CONFERENCIA
                                                            )
                                            .Select(s => new LancamentoCaixaConferencia { IdLancamentoCaixa = s.IdCaixaLancamento, IdCtrlConferencia = s.IdCtrlConferencia, LxStatusConferencia = s.LxStatusConferencia })
                                            .ToList(),
                    };

                    retorno.Add(conferencia);
                }
            }

            return retorno;
        }
    }
}
